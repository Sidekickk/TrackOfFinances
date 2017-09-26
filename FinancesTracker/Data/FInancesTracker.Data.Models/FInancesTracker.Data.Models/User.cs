namespace FinancesTracker.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<Tag> tags;
        private ICollection<AmountOfMoney> amountsOfMoney;

        public User()
        {
            this.tags = new HashSet<Tag>();
            this.amountsOfMoney = new HashSet<AmountOfMoney>();
        }

        [MaxLength(100)]
        public string FristName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public long TotalAmountOfMoneySpent { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<AmountOfMoney> AmountsOfMoney
        {
            get { return this.amountsOfMoney; }
            set { this.amountsOfMoney = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}