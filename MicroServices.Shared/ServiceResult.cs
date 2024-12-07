using MediatR;
using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace MicroServices.Shared
{
	//kullanirken her seferinde bunu kalitim olarak almayalim kısaltalim refactor edelim IRequest<ServiceResult<T>>;
	public interface IRequestByServiceResult<T>:IRequest<ServiceResult<T>>;
	public interface IRequestByServiceResult:IRequest<ServiceResult>;
	public class ServiceResult
	{
		[JsonIgnore] public HttpStatusCode Status { get; set; }
		public ProblemDetails? Fail { get; set; }
		[JsonIgnore] public bool IsSuccess => Fail is null;
		[JsonIgnore] public bool IsFail => !IsSuccess;


		//update veya delete basarili olursu success no content donulur
		public static ServiceResult SuccessAsNoContent()
		{
			return new ServiceResult
			{
				Status = HttpStatusCode.NoContent
			};
		}
		public static ServiceResult ErrorAsNotFound()
		{
			return new ServiceResult
			{
				Status = HttpStatusCode.NotFound,
				Fail = new ProblemDetails
				{
					//zaten 404 oldugu icin fail'in body'si doldurulmasa da olur
					Title = "Error",
					Detail = "the requested resource was not found"
				}
			};
		}

		public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
		{
			return new ServiceResult
			{
				Status = status,
				Fail = problemDetails
			};
		}
		public static ServiceResult Error(string title, string description, HttpStatusCode status)
		{
			return new ServiceResult
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Detail = description,
					Title = title,
					Status = status.GetHashCode()
				}
			};
		}
		public static ServiceResult Error(string title, HttpStatusCode status)
		{
			return new ServiceResult
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Title = title,
					Status = status.GetHashCode()
				}
			};
		}
		public static ServiceResult ErrorFromProblemDetails(ApiException exception)
		{
			if (string.IsNullOrEmpty(exception.Content))
			{
				return new ServiceResult()
				{
					Fail = new ProblemDetails
					{
						Title = exception.Message
					},
					Status = exception.StatusCode
				};
			}
			var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return new ServiceResult
			{
				Fail = problemDetails,
				Status = exception.StatusCode
			};
		}
		public static ServiceResult ErrorFromValidation(IDictionary<string, object> errors)
		{
			return new ServiceResult
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails
				{
					Title = "Validation errors occured",
					Detail = "Please check the errors property for more details",
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}
	}
	public class ServiceResult<T> : ServiceResult
	{
		public T? Data { get; set; }
		[JsonIgnore] public string? UrlAsCreated { get; set; }
		public static ServiceResult<T> SuccessAsOk(T data)
		{
			return new ServiceResult<T>
			{
				Status = HttpStatusCode.OK,
				Data = data
			};
		}

		//201 => Created => response's body Header => location == api/products/3
		public static ServiceResult<T> SuccessAsCreated(T data, string url)
		{
			return new ServiceResult<T>
			{
				Status = HttpStatusCode.Created,
				Data = data,
				UrlAsCreated = url
			};
		}
		public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
		{
			return new ServiceResult<T>
			{
				Status = status,
				Fail = problemDetails
			};
		}
		public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
		{
			return new ServiceResult<T>
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Detail = description,
					Title = title,
					Status = status.GetHashCode()
				}
			};
		}
		public new static ServiceResult<T> Error(string title, HttpStatusCode status)
		{
			return new ServiceResult<T>
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Title = title,
					Status = status.GetHashCode()
				}
			};
		}
		public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
		{
			if (string.IsNullOrEmpty(exception.Content))
			{
				return new ServiceResult<T>
				{
					Fail = new ProblemDetails
					{
						Title = exception.Message
					},
					Status = exception.StatusCode
				};
			}
			var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return new ServiceResult<T>
			{
				Fail = problemDetails,
				Status = exception.StatusCode
			};
		}
		public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object> errors)
		{
			return new ServiceResult<T>
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails
				{
					Title = "Validation errors occured",
					Detail = "Please check the errors property for more details",
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}
	}
}
#region Virtual/Override/Polymorphism yerine "new" keywordu 
/*
Override Yerine new Kullanımı:
Override kullanmak için metodun virtual olarak tanımlanması ve türetilmiş sınıfta override anahtar sözcüğüyle yeniden tanımlanması gerekir.
Ancak burada metodlar static olduğundan, virtual/override kullanımı mümkün değildir çünkü static üyelerde polimorfizm desteklenmez.
new keyword'ü ile, türetilmiş sınıfta aynı isimli metodun taban sınıftakinden bağımsız bir şekilde yeniden tanımlandığı ifade edilir.

Neden new Kullanılmış?
ServiceResult<T> sınıfında, hata durumlarında kullanılan metodlar (Error) türetilmiş sınıfa özgü bir şekilde çalışıyor. 
Bu metodlar, dönüş tipi olarak ServiceResult<T> döndürdükleri için taban sınıftakilerden ayrılıyorlar.
Eğer new kullanılmazsa, taban sınıftaki metodlar gölgelenir (shadowing). Bu durum, türetilmiş sınıftan çağrı yapıldığında açık bir şekilde uyarı almanıza neden olabilir.
new kullanımı, "Bu metodun taban sınıftaki metodla aynı olmadığını ve onu geçersiz kılmadığını (override etmediğini),
tamamen bağımsız bir uygulama sunduğunu" belirtmek için bir yoldur.
 */
#endregion