namespace FC.Codeflix.Catalog.Domain.SeedWork;
public interface IGenericRepository<T>: IRepository
{
    public Task Insert(T aggregate, CancellationToken cancellationToken);

    public Task<T?> Get(Guid id, CancellationToken cancellationToken);

    public Task Update(T aggregate, CancellationToken cancellationToken);

    public Task Delete(T aggregate, CancellationToken cancellationToken);
}
