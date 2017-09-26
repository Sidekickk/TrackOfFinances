namespace FinancesTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Tag
    {
        private ICollection<User> users;
        private ICollection<AmountOfMoney> amountsOfMoney;

        public Tag()
        {
            this.users = new HashSet<User>();
            this.amountsOfMoney = new HashSet<AmountOfMoney>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<AmountOfMoney> AmountsOfMoney
        {
            get { return this.amountsOfMoney; }
            set { this.amountsOfMoney = value; }
        }
    }
}
