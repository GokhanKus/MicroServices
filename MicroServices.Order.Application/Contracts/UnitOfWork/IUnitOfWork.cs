using System.Threading;

namespace MicroServices.Order.Application.Contracts.UnitOfWorks
{
	public interface IUnitOfWork
	{
		Task BeginTransactionAsync(CancellationToken cancellationToken = default);
		Task CommitTransactionAsync(CancellationToken cancellationToken = default);
		Task<int> CommitAsync(CancellationToken cancellationToken = default);
	}
}
