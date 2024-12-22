
namespace MicroServices.Shared.Services
{
	public class IdentityServiceFake : IIdentityService
	{
		public Guid UserId => Guid.Parse("9e2fdd10-dbde-4a4b-b111-666526e9970f");
		public string UserName => "Ahmet16";
	}
}
