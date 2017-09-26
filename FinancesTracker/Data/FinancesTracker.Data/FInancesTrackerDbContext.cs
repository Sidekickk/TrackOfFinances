namespace FinancesTracker.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using FinancesTracker.Data.Models;

    public class FinancesTrackerDbContext : IdentityDbContext<User>
    {
        public FinancesTrackerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<AmountOfMoney> AmountsOfMoney { get; set; }

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