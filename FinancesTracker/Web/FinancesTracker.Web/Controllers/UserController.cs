namespace FinancesTracker.Web.Controllers
{
    using Data.Contracts;
    using Data.Models;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class UserController : ApiController
    {
        private readonly IRepository<User> usersData;
        private readonly IRepository<Expense> expensesData;
        private const string def = "";

        public UserController(IRepository<User> data, IRepository<Expense> expenses)
        {
            this.usersData = data;
            this.expensesData = expenses;
        }

        [HttpPost]
        [Route("user/addExpense")]
        public IHttpActionResult AddExpense(long money, int tagId, string description = def)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this.usersData.All().FirstOrDefault(u => u.Email == this.User.Identity.Name);

            var expense = new Expense()
            {
                Money = money,
                TagId = tagId,
                UserId = user.Id,
                Description = description,
                CreatedOn = DateTime.Now
            };

            this.expensesData.Add(expense);

            this.expensesData.SaveChanges();

            return this.Ok("Successfully added expense...");
        }

        [HttpPost]
        [Route("user/addcategory")]
        public IHttpActionResult AddCategory(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = new Tag()
            {
                Name = name,
                CreatedOn = DateTime.Now
            };

            var user = this.usersData.All().FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var isTagExist = user.Tags.FirstOrDefault(x => x.Name == name) != null ? true : false;

            if (isTagExist)
            {
                return this.BadRequest("This tag already existing..");
            }

            user.Tags.Add(tag);

            this.usersData.SaveChanges();

            return this.Ok("Successfully added category");
        }


        // TODO : GetExpensesInMonth in progres
        [HttpGet]
        [Route("user/getExpensesInMonth")]
        public IHttpActionResult GetExpensesInMonth(string date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime currentDate = DateTime.Parse(date);

            var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            var startDateInMonth = DateTime.Parse(string.Format("01.{0}.{1}", currentDate.Month, currentDate.Year));

            var endDateInMonth = DateTime.Parse(string.Format("{0}.{1}.{2}", daysInMonth, currentDate.Month, currentDate.Year));

            var currentUserId = usersData.All().FirstOrDefault(u => u.Email == this.User.Identity.Name).Id;

            var result = expensesData.All()
                .Where(e => e.UserId == currentUserId
                    && e.CreatedOn >= startDateInMonth
                    && e.CreatedOn <= endDateInMonth
                    && e.IsDeleted == false)
                .GroupBy(x => x.Tag)
                .Select(u => new
                {
                    totalSum = u.Key.Expenses.Sum(x => x.Money),
                })
                .ToList();

            return this.Ok(result);
        }
    }
}
