namespace FinancesTracker.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using FinancesTracker.Data.Models;

    public class FinancesTrackerDbContext : IdentityDbContext<User>
    {
        public FinancesTrackerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FinancesTrackerDbContext Create()
        {
            return new FinancesTrackerDbContext();
        }
    }
}