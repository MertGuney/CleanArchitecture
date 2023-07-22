namespace CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCacheAsync(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
    }
}
