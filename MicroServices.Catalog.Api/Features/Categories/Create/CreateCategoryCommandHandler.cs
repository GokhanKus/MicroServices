namespace MicroServices.Catalog.Api.Features.Categories.Create
{
	public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
	{
		public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var existCategory = await context.Categories.AnyAsync(c => c.Name == request.Name, cancellationToken: cancellationToken);

			if (existCategory)
			{
				return ServiceResult<CreateCategoryResponse>.Error("Category Name already exists",
					$"The category name '{request.Name}' already exists", HttpStatusCode.BadRequest);
			}

			var category = new Category
			{
				Name = request.Name,
				Id = NewId.NextSequentialGuid()
			};

			await context.AddAsync(category, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);

			return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), $"<empty>");

		}
	}
}
#region MediatR
/*
MediatR Nedir?
MediatR, .NET uygulamalarında mediator (arabulucu) tasarım desenini uygulamak için kullanılan bir kütüphanedir.
Bu desen, uygulama katmanları arasındaki doğrudan bağımlılıkları azaltır ve bileşenlerin birbiriyle dolaylı iletişim kurmasını sağlar.

MediatR, özellikle CQRS (Command Query Responsibility Segregation) desenine uygun olarak komutlar ve sorgular için yaygın bir şekilde kullanılır.

MediatR Neden Kullanılır?
1. Katmanlar Arası Gevşek Bağımlılık Sağlar
Uygulama bileşenlerinin doğrudan birbirine bağlı olması yerine, tüm iletişim MediatR aracılığıyla yapılır. Bu, bağımlılıkların daha kolay yönetilmesini sağlar.
Örneğin, bir kontrolcü (Controller) iş mantığını doğrudan çağırmak yerine, bir komut (Command) veya sorgu (Query) gönderir. MediatR, doğru işleyiciyi çağırır.
2. Kodun Daha Temiz ve Modüler Olmasını Sağlar
İş mantığı ve kontrolcü mantığı birbirinden ayrılır.
Her komut veya sorgu, kendi işleyicisi (handler) ile eşleşir ve sorumluluklar daha net tanımlanır.
3. CQRS ve SOLID İlkelerine Uyum
CQRS: Komutlar (yazma işlemleri) ve sorgular (okuma işlemleri) ayrı ayrı tanımlanabilir.
SOLID İlkeleri: Özellikle Tek Sorumluluk İlkesi (Single Responsibility Principle) ve Açık-Kapalı İlkesi (Open/Closed Principle) için uygundur.
4. Test Edilebilirlik
İş mantığı işleyicilerde olduğu için, bu mantık kolayca birim testlere tabi tutulabilir. Kontrolcüler ve diğer bileşenler bağımsız olarak test edilebilir.
5. Bakımı Kolaylaştırır
MediatR kullanarak uygulama kodunu daha iyi organize edebilir, büyük ve karmaşık projelerde kodun anlaşılabilir ve sürdürülebilir olmasını sağlayabilirsiniz.
MediatR Nasıl Çalışır?
MediatR'ın çalışma prensibi şudur:

İstek (Request) Gönderme

Komut (Command) veya sorgu (Query) nesnesi tanımlanır.
Bu nesne, MediatR kullanılarak gönderilir.
İşleyici (Handler)

MediatR, gönderilen isteğe uygun olan işleyiciyi bulur.
İşleyici, isteği işler ve bir sonuç döner.

Özet
MediatR, farklı katmanlar arasında doğrudan bağımlılıkları azaltmak ve uygulamanın daha temiz bir mimariyle geliştirilmesini sağlamak için kullanılır.
Özellikle CQRS deseninde komutlar (Commands) ve sorgular (Queries) işleyiciler aracılığıyla işlenir.
Kodun test edilebilirliği, bakımı ve modülerliği açısından çok faydalıdır.
Bu nedenle MediatR, modern .NET projelerinde yaygın bir şekilde tercih edilir.
 */
#endregion