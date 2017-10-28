namespace FinancesTracker.Web.Controllers
{
    using Data.Contracts;
    using Data.Models;
    using Models.ResponseModels;
    using Models.BindingModels.User;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class UserController : ApiController
    {
        private readonly IRepository<User> usersData;
        private readonly IRepository<Expense> expensesData;

        public UserController(IRepository<User> data, IRepository<Expense> expenses)
        {
            this.usersData = data;
            this.expensesData = expenses;
        }

        [HttpPost]
        [Route("user/addExpense")]
        public IHttpActionResult AddExpense([FromBody]AddExpenseBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = this.usersData.All().FirstOrDefault(u => u.Email == this.User.Identity.Name);

            user.TotalAmountOfMoneySpent += model.Money;

            var expense = new Expense()
            {
                Money = model.Money,
                TagId = model.TagId,
                UserId = user.Id,
                Description = model.Description,
                CreatedOn = model.CreatedOn
            };

            this.expensesData.Add(expense);

            this.expensesData.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        [Route("user/addcategory")]
        public IHttpActionResult AddCategory(string name)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
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


        // TODO : GetExpensesInMonth
        [HttpGet]
        [Route("user/getExpensesInMonth")]
        public IHttpActionResult GetExpensesInMonth(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            var startDateInMonth = DateTime.Parse(string.Format("01.{0}.{1}", date.Month, date.Year));

            var endDateInMonth = startDateInMonth.AddDays(daysInMonth - 1);

            var currentUser = usersData.All().FirstOrDefault(u => u.Email == this.User.Identity.Name);

            var result = expensesData.All()
                .Where(e => e.UserId == currentUser.Id
                    && e.CreatedOn >= startDateInMonth
                    && e.CreatedOn <= endDateInMonth
                    && e.IsDeleted == false)
                .GroupBy(x => x.Tag)
                .Select(u => new
                {
                    totalSum = u.Key.Expenses.Sum(x => x.Money),
                    percentage = (u.Key.Expenses.Sum(e => e.Money) / (double)currentUser.TotalAmountOfMoneySpent) * 100,
                    amountOfMoney = currentUser.TotalAmountOfMoneySpent
                })
                .ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("user/getAllExpensesInOneDay")]
        public IHttpActionResult GetAllExpensesInOneDay(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var result = this.expensesData.All()
                .Where(x => x.CreatedOn.Value.Year == date.Year && x.CreatedOn.Value.Month == date.Month && x.CreatedOn.Value.Day == date.Day)
                .Select(s => new ExpenseResponseModel
                {
                    CreatedOn = s.CreatedOn,
                    Description = s.Description,
                    Money = s.Money,
                    TagName = s.Tag.Name,

                }).ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("user/lastTenExpenses")]
        public IHttpActionResult GetLastTenExpenses()
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var result = this.expensesData.All()
                .OrderBy(x => x.CreatedOn)
                .Take(5)
                .Select(s => new ExpenseResponseModel
                {
                    CreatedOn = s.CreatedOn,
                    Description = s.Description,
                    Money = s.Money,
                    TagName = s.Tag.Name,

                }).ToList();

            return this.Ok(result);
        }

        // get expenses from tag in one day
        [HttpGet]
        [Route("user/tagExpenses")]
        public IHttpActionResult GetAllTagExpensesInCurrentDay(int tagId, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            return this.Ok();
        }
        // get all expenses from tag
        // get all expenses

    }
}
