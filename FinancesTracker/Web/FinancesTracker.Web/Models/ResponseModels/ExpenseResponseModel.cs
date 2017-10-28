using System;

namespace FinancesTracker.Web.Models.ResponseModels
{
    public class ExpenseResponseModel
    {
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string TagName { get; set; }

        public long Money { get; set; }
    }
}