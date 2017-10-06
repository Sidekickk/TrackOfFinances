namespace FinancesTracker.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using FinancesTracker.Data.Models;
    using Contracts;

    public class FinancesTrackerDbContext : IdentityDbContext<User> , IFinancesTrackerDbContext
    {
        public FinancesTrackerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Expense> Expenses { get; set; }

        public static FinancesTrackerDbContext Create()
        {
            return new FinancesTrackerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(t => t.Tags)
                .WithMany(u => u.Users)
                .Map(ut => 
                {
                    ut.ToTable("UserTag");
                    ut.MapLeftKey("UserId");
                    ut.MapRightKey("TagsId");
                });
        }
    }
}