namespace FinancesTracker.Data.Contracts
{
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IFinancesTrackerDbContext
    {
        DbSet<Tag> Tags { get; set; }

        DbSet<Expense> Expenses { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
