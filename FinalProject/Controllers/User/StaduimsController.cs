using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using FinalProject.Models.TicketyModel;
namespace FinalProject.Controllers.User
{
    [AllowAnonymous]
    public class StaduimsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Staduims
        public ActionResult getAllStadium()
        {
            return View(db.Staduims.OrderBy(x=>x.isValid).ToList());
        }
        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}