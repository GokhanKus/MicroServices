namespace MicroServices.Discount.Api.Repositories //name space adı bilerek bu sekilde bırakildi cunku Microservice adıyla -proje adi(Dİcount) entity adı aynıydı.
{
	public class Discount : BaseEntity
	{
		public Guid UserId { get; set; }
		public float Rate { get; set; }
		public string Code { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime Expired { get; set; }
	}
}
