using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        // GET: test
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}