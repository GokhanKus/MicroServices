namespace MicroServices.Catalog.Api.Features.Courses.Create
{
	public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
	{
		public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
		{
			var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

			if (!hasCategory)
			{
				return ServiceResult<Guid>.Error("Category not found.",
					$"The Category with id({request.CategoryId}) was not found", HttpStatusCode.NotFound);
			}

			var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

			if (hasCourse)
			{
				return ServiceResult<Guid>.Error("Course already exists.",
					$"The Course with name({request.Name}) already exists", HttpStatusCode.BadRequest);
			}

			var newCourse = mapper.Map<Course>(request);
			newCourse.Created = DateTime.Now;
			newCourse.Id = NewId.NextSequentialGuid();

			newCourse.Feature = new Feature
			{
				Duration = 10,
				Rating = 0,
				EducatorFullName = "Ahmet Yilmaz"
			};

			context.Courses.Add(newCourse);
			await context.SaveChangesAsync(cancellationToken);

			return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
		}
	}
}
#region CancellationToken
/*
CancellationToken Kullanılmazsa
Bu durumda, sorgu çalıştırıldığında işlem tamamlanana kadar devam eder. Ancak, bir iptal durumu 
(örneğin, istemcinin HTTP isteğini iptal etmesi veya uygulamanın zaman aşımına uğraması) olsa bile, sorgu durmaz ve kaynaklar kullanılmaya devam eder. Bu durum:

Gereksiz CPU ve bellek tüketimine yol açabilir.
İptal edilmesi gereken işlemlerin tamamlanmasını beklemek, performans sorunlarına neden olabilir.
Özellikle çok sayıda istek işlendiğinde sunucunun aşırı yüklenmesine neden olabilir.

CancellationToken Kullanılırsa
Veritabanı sorgusu iptal edilir.
Örneğin, istemci HTTP isteğini iptal ettiyse, bu sorgunun devam etmesine gerek kalmaz.
Kaynak tüketimi azalır.
Gereksiz işlemler engellendiği için sunucu daha verimli çalışır.
Hata fırlatılır.
OperationCanceledException fırlatılır ve bu, işlemin iptal edildiğini belirtir.
 */
#endregion