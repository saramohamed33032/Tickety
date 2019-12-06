using FinalProject.Models;
using FinalProject.Models.TicketyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers.Admin
{
    [Authorize(Roles = "MasterAdmin , Admin")]
    public class AdminStaduimController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Staduim
        //هيضيف استادات جديده 
        public ActionResult CreateStaduim()
        {
            return View();
        }

        // POST: Staduims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStaduim([Bind(Include = "staduimID,staduimName,city,staduimLocation,isValid")] Staduim staduim)
        {
            var result = db.Staduims.FirstOrDefault(x => x.staduimName == staduim.staduimName);
            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    staduim.staduimID = Guid.NewGuid().ToString();
                    staduim.isValid = true;
                    db.Staduims.Add(staduim);
                    db.SaveChanges();
                    return RedirectToAction("getAllStadium", "Staduims");
                }
            }
            else
            {
                ViewBag.error = "There are same Staduim";
            }
            return View("Error_create");
        }

        public ActionResult OperationStaduim(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staduim staduim = db.Staduims.Find(id);
            if (staduim == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (staduim.isValid == true)
                {
                    staduim.isValid = false;
                }
                else
                {
                    staduim.isValid = true;
                }
                db.SaveChanges();
            }
            return RedirectToAction("getAllStadium", "Staduims");

        }
        //public JsonResult checkStaduimName(string staduimName)
        //{

        //    var n = db.Staduims.FirstOrDefault(a => a.staduimName == staduimName);
        //    return Json(n == null, JsonRequestBehavior.AllowGet);
        //}

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