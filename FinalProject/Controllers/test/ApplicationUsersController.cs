using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

using FinalProject.Models.ViewModels;
using FinalProject.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;

namespace FinalProject.Controllers.test
{
    [Authorize]
    public class ApplicationUsersController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        public FileResult show()
        {
            var y = User.Identity.GetUserId();
            var img = db.Users.FirstOrDefault(a => a.Id == y).ProfileImage;
            if (img == null)
            {
                return File("~/img/noimgprofile.jpg","image.jpg");
            }
            string path = Server.MapPath("~/img/");
            return File(path + img, "image.png,image.jpg,image.jpeg");
        }
        public ActionResult profile()
        {
            // User.Identity.GetUserId();
            var y = User.Identity.GetUserId();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
             ProfileVM profileVM = new ProfileVM();
            
            ApplicationUser applicationUser = db.Users.Where(x => x.Id == y).FirstOrDefault();
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            // return View("Details",applicationUser);
            profileVM.applicationUser = applicationUser;
            return View( profileVM);
        }
        public ActionResult myTicket()
        {
            // User.Identity.GetUserId();
            var y = User.Identity.GetUserId();
            var ticket = db.Tickets.Where(a => a.UserID == y).ToList();
            return View(ticket);
        }
        public ActionResult changePhoto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> changePhoto(HttpPostedFileBase j)
        {
            string path = Server.MapPath("~/img/");
            j.SaveAs(path + j.FileName);
            var id = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindById(id);
            currentUser.ProfileImage = j.FileName;
            await manager.UpdateAsync(currentUser);
            var ctx = store.Context;
            ctx.SaveChanges();
            return RedirectToAction(nameof(profile));
        }
        [HttpGet]
        public ActionResult changeEmail()
        {
            // User.Identity.GetUserId();
            var y = User.Identity.GetUserId(); 
            EditEmail ed = new EditEmail();
            ed.oldEmail = db.Users.Where(x => x.Id == y).FirstOrDefault().Email;
            ProfileVM profileVM = new ProfileVM();
            profileVM.EditEmail = ed;

            return PartialView(profileVM);
        }
    
        [HttpGet]
        public ActionResult Addphone()
        {
            return PartialView();
        }
       
        //public ActionResult Addphone(ProfileVM model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // User.Identity.GetUserId();
        //    var y = User.Identity.GetUserId(); ;
        //    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
        //    var manager = new UserManager<ApplicationUser>(store);

        //    var currentUser = manager.FindById(y);
        //    currentUser.PhoneNumber = model.editPhone.Phone;

        //    return PartialView();
        //}
        [HttpGet]
        public ActionResult changePhone()
        {
            // User.Identity.GetUserId();
            var y = User.Identity.GetUserId();

            EditPhone ed = new EditPhone();
            ProfileVM profileVM = new ProfileVM();
            ed.oldPhone = db.Users.Where(x => x.Id == y).FirstOrDefault().PhoneNumber;
            profileVM.editPhone = ed;
            return PartialView(profileVM);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> changePhone(ProfileVM model,string oldPhone)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           // User.Identity.GetUserId();
            var y = User.Identity.GetUserId(); ;
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            
            var currentUser = manager.FindById(y);
            currentUser.PhoneNumber= model.editPhone.Phone;
            await manager.UpdateAsync(currentUser);
            var ctx = store.Context;
            ctx.SaveChanges();
            TempData["msg"] = "Phone Added successfully";
            return RedirectToAction("verifyPhone");
        }


        public ActionResult verifyPhone()
        {
            VerifyPhone tel = new VerifyPhone();
            var y = User.Identity.GetUserId();
            tel.Phone = db.Users.Where(x => x.Id == y).FirstOrDefault().PhoneNumber;

            ProfileVM profileVM = new ProfileVM();
            profileVM.VerifyPhone = tel;
            return PartialView(profileVM);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //send sms to phonenumber
        public ActionResult verifyPhone(string phone)
        {
            Sms obj = new Sms();
            obj.PhoneNumber = phone; 
            if (obj.sendsms())
                return RedirectToAction("verifyCode", new { phone = phone });
            else
                TempData["msg"] = "code not sent resend code";
            return PartialView();
        }


        public ActionResult verifyCode(string phone)
        {
            TempData["phone"] = phone;
            return PartialView();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //send sms to phonenumber
        public ActionResult verifyCode(ProfileVM code, string phone)
        {
            Sms obj = new Sms();
            obj.PhoneNumber = phone; 
            if (obj.verify(code.verifyCode.verifycode) == true)
            {

                // User.Identity.GetUserId();
                var y = User.Identity.GetUserId();
                var tel = db.Users.Where(x => x.Id == y).FirstOrDefault();
                tel.PhoneNumberConfirmed = true;
                db.SaveChanges();

                TempData["msg"] = "verification successfully";
                return PartialView("~/Views/PartialViews/_MassagTouser.cshtml");
            }

            else
                TempData["msg"] = "wrong code ";
            return RedirectToAction("verifyCode", new { phone=phone});
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
