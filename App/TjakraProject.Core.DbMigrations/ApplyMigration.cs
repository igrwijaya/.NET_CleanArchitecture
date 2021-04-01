using System;
using Microsoft.EntityFrameworkCore;
using TjakraProject.Core.Infrastructure.DataSource;

namespace TjakraProject.Core.DbMigrations
{
    public class ApplyMigration
    {
        public static void Run<TDbContext>()
            where TDbContext : CoreDbContext, new()
        {
            var dbContextFactory = new SqlDesignTimeDbContextFactory<TDbContext>();
            var dbContext = dbContextFactory.CreateDbContext();

            var migrationData = dbContext.Database.GetPendingMigrations();

            foreach (var migration in migrationData)
            {
                Console.WriteLine(migration);
            }

            dbContext.Database.Migrate();
        }
    }
}