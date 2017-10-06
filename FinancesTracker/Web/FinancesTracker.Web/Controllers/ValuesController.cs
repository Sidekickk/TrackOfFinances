using FinancesTracker.Data;
using FinancesTracker.Data.Contracts;
using FinancesTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinancesTracker.Web.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private readonly IRepository<User> users;

        public ValuesController(IRepository<User> data)
        {
            this.users = data;
        }

        // GET api/values
        [HttpGet]
        [Route("api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/values")]
        public IEnumerable<string> Get(string str)
        {
            var currentUser = this.users.All().FirstOrDefault(u => u.Email == this.User.Identity.Name);
            return new string[] { "value1", "value2" + currentUser.Id };
        }
    }
}
