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
    [Route("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly IRepository<User> usersData;

        public CategoriesController(IRepository<User> data)
        {
            this.usersData = data;
        }


        public IHttpActionResult GetAllCategoriesByUserId(string id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = this.usersData.GetById(id);

            if (user == null)
            {
                return this.BadRequest("There is no user with such Id");
            }

            var categories = user
                .Tags
                .Select(s => new
                {
                    Name = s.Name,
                }).ToList();

            if (categories == null)
            {
                return this.NotFound();
            }

            return this.Ok(categories);
        }

    }
}
