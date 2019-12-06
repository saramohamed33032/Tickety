using FinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers.Admin
{
    [Authorize(Roles = "MasterAdmin")]
    public class SuperAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SuperAdmin
        public ActionResult AllUser()
        {
            return View(db.Users.ToList());
        }
        public ActionResult OperationUser(string id)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            var adminRole = db.Roles.FirstOrDefault(a => a.Name == "Admin");
            var userRole = db.Roles.FirstOrDefault(a => a.Name == "user");
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (userManager.IsInRole(id,"Admin"))
            {
                userManager.AddToRole(user.Id, "user");
                userManager.RemoveFromRole(id, "Admin");
            }
            else if (userManager.IsInRole(id, "user"))
            {
                userManager.AddToRole(user.Id, "Admin");
                userManager.RemoveFromRole(id, "user");
            }

            return RedirectToAction(nameof(AllUser));
        }
    }
}