using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TjakraProject.Core.Application.Service;
using TjakraProject.Core.Domain.Common;
using TjakraProject.Core.Domain.Event;

namespace TjakraProject.Core.Infrastructure.DataSource
{
    public class CoreDbContext: IdentityDbContext<IdentityUser>
    {
        #region Fields

        private readonly ISessionUserService _sessionUserService;
        private readonly IDomainEventService _domainEventService;

        #endregion

        #region Constructors

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
        {
            
        }

        public CoreDbContext(
            DbContextOptions<CoreDbContext> options, 
            ISessionUserService sessionUserService, 
            IDomainEventService domainEventService)
            : base(options)
        {
            _sessionUserService = sessionUserService;
            _domainEventService = domainEventService;
        }

        #endregion

        #region Public Methods

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<CoreEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _sessionUserService.UserId;
                        entry.Entity.CreatedDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _sessionUserService.UserId;
                        entry.Entity.LastModifiedDateTime = DateTime.UtcNow;
                        break;
                    
                    case EntityState.Detached:
                        break;
                    
                    case EntityState.Unchanged:
                        break;
                    
                    case EntityState.Deleted:
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        #endregion

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        #endregion

        #region Private Methods

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IDomainEventRoot>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

                if (domainEventEntity == null)
                {
                    break;
                }

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }

        #endregion

    }
}