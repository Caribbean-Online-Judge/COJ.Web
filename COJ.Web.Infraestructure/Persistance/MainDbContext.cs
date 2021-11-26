
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;

namespace COJ.Web.Infrestructure.Data;

public class MainDbContext : DbContext

{

    #region Entities

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountToken> AccountTokens { get; set; }
    public DbSet<AccountPermission> AccountPermissions { get; set; }
    public DbSet<AccountSettings> AccountSettings { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Locale> Locales { get; set; }
    #endregion

    #region Constructors
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }
    #endregion


    #region Override Functions
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    #endregion

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            //Current Date time in UTC
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((IBaseEntity)entity.Entity).CreatedAt = now;
            }
            else if (entity.State == EntityState.Modified)
            {
                ((IBaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}