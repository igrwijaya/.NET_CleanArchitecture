using TjakraProject.Core.Domain.Common;
using TjakraProject.Core.Infrastructure.DataSource;

namespace TjakraProject.Core.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        public BaseRepository(CoreDbContext context)
        {
            Context = context;
        }

        protected CoreDbContext Context { get; }
    }
}