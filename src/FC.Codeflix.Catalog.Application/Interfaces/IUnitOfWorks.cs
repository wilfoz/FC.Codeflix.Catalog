namespace FC.Codeflix.Catalog.Application.Interfaces;
public interface IUnitOfWorks
{
    public Task Commit(CancellationToken cancellationToken);
}
