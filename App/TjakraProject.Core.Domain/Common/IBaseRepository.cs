namespace TjakraProject.Core.Domain.Common
{
    public interface IBaseRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        
    }
}