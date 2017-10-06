namespace FinancesTracker.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long Money { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
