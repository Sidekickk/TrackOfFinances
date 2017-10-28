 namespace FinancesTracker.Web.Models.BindingModels.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddExpenseBindingModel
    {

        [Required]
        public long Money { get; set; }

        [Required]
        public int TagId { get; set; }


        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }      
    }
}