using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TjakraProject.Core.DbMigrations.Constants;
using TjakraProject.Core.Infrastructure.DataSource;

namespace TjakraProject.Core.DbMigrations
{
    internal class SqlDesignTimeDbContextFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : CoreDbContext, new()
    {
        public TDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public TDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<TDbContext>();
            var dbConnection = configuration[ConfigurationConstant.SqlConnectionString];

            builder.UseMySql(
                dbConnection,
                b => b.MigrationsAssembly(GetType().Namespace));

            return (TDbContext) Activator.CreateInstance(typeof(TDbContext), builder.Options);
        }
    }
}