using Landlyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Landlyst.Controllers
{
    public class HomeController : Controller
    {
        //Get the index site, where you can choose your room features
        public ActionResult Index()
        {
            return View();
        }
    }
}