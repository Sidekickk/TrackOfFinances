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

        public virtual DbSet<Tags> Tags { get; set; }

        public static FinancesTrackerDbContext Create()
        {
            return new FinancesTrackerDbContext();
        }
    }
}