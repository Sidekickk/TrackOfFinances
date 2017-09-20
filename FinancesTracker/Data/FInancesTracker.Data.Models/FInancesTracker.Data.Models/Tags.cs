namespace FinancesTracker.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Tags
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

    }
}
