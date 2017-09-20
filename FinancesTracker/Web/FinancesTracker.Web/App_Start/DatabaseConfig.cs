using FinancesTracker.Data;
using FinancesTracker.Data.Migrations;
using System.Data.Entity;

namespace FinancesTracker.Web
{
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FinancesTrackerDbContext, Configuration>());
            FinancesTrackerDbContext.Create().Database.Initialize(true);
        }
    }
}