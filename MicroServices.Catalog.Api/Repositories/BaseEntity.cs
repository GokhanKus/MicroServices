using MongoDB.Bson.Serialization.Attributes;

namespace MicroServices.Catalog.Api.Repositories
{
	public class BaseEntity
	{
		[BsonElement("_id")]//mongodb'de bu _id olarak tutulsun
		public Guid Id { get; set; }

		#region Guid Id hakkinda
		//guid tarafında indexlemeyi kolaylaştıracak snow flakes algoritması kullanilacak böylelikle
		//bu primary keye sahip olan id alanlarin indexlernmesi normal guide gore daha kolay olacaktir (perf artisi saglar)
		//bu algoritmayi ilerleyen asamalarda masstransit uzerinden kullanacagiz simdilik NewId paketi uzerinden devam edelim
		//normalde Guid New guid diyerek uretilen degerler birbirinden cok random degerler olacagi icin indexlemesi zordur
		// o yuzden newid veya mastransit uzerinden guid degerler uretiyoruz ki bu degerler birbirine yakin guid degerler olacak
		// o yuzden indexlemesi kolay olur diyoruz
		#endregion
	}
}
