using FinalProject.Models;
using FinalProject.Models.TicketyModel;
using FinalProject.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index(string reg)
        {
            //IndexVM matchTeam = new IndexVM();
            //ApplicationDbContext dbContext = new ApplicationDbContext();
            // var teams= dbContext.Teams.ToList();
            //var matches = dbContext.Matches.ToList();
            //ViewBag.isregsted = reg;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}