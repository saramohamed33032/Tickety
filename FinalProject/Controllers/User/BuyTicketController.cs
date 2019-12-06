using FinalProject.Models;
using FinalProject.Models.TicketyModel;
using FinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Stripe;
using Microsoft.AspNet.Identity;

namespace FinalProject.Controllers.User
{
    [Authorize]
    public class BuyTicketController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ByeTicket
        public ActionResult Index()
        {
            return View(db.Matches.ToList());
        }
        public ActionResult ticketDetails(string id)
        {
           var degree = db.Degrees.FirstOrDefault(a => a.matchID == id);
            ViewBag.classA = db.Degrees.Where(a => a.matchID == id).FirstOrDefault(a => a.ticketClass == "A").degreePrice;
            ViewBag.classB = db.Degrees.Where(a => a.matchID == id).FirstOrDefault(a => a.ticketClass == "B").degreePrice;
            ViewBag.classC = db.Degrees.Where(a => a.matchID == id).FirstOrDefault(a => a.ticketClass == "C").degreePrice;
            return View(degree);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(Degree degree)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket();
                ticket.ticketID = Guid.NewGuid().ToString();
                ticket.ticketClass = degree.ticketClass;
                ticket.matchID = degree.matchID;
                ticket.Seat_Number = db.Degrees.Where(a => a.matchID == degree.matchID).FirstOrDefault(a => a.ticketClass == degree.ticketClass).NO_Seat;
                ticket.UserID = "01712fdb-b5c7-41d7-ba89-df90b12852b1";
                db.Tickets.Add(ticket);
                db.SaveChanges();
                var newdegree = db.Degrees.Where(a => a.matchID == degree.matchID).FirstOrDefault(a => a.ticketClass == degree.ticketClass);
                newdegree.NO_Seat = newdegree.NO_Seat - 1;
                db.SaveChanges();
                return RedirectToAction(nameof(printTicket),new { id=ticket.ticketID});
            }
            return RedirectToAction(nameof(ticketDetails),new { id=degree.matchID});
        }
        public ActionResult printTicket(string id)
        {
            return View(db.Tickets.FirstOrDefault(a=>a.ticketID==id));
        }
        public ActionResult printTicketPDF(string id)
        {
            var print = new PartialViewAsPdf(db.Tickets.FirstOrDefault(a => a.ticketID == id));
            return print;
        }

        /////////////////////////////////////////////////////////////////////////
        public ActionResult Pay(Ticket tk)
        {
            if (tk.ticketClass == null)
            {
               return RedirectToAction(nameof(ticketDetails), new { id = tk.matchID });
            }
            var ticket = db.Degrees.Where(x => x.matchID == tk.matchID).FirstOrDefault(z => z.ticketClass == tk.ticketClass).NO_Seat;
            if (ticket<1)
            {
                if (ticket < 1 && tk.ticketClass != null)
                {
                    TempData["message"] = "This degree Finished";
                }
                return RedirectToAction(nameof(ticketDetails),new {id=tk.matchID });
            }
            return View(tk);
        }
        [HttpPost]
        public ActionResult Pay(Ticket tkData, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_gwjhtnAjFBGu9WVgCU7ROMd300hBGhn82R";
            //sk_test_gwjhtnAjFBGu9WVgCU7ROMd300hBGhn82R
            // Token is created using Checkout or Elements!
            // Get the payment token submitted by the form:

            var token = stripeToken; //

            //strCurrentUserId = User.Identity.GetUserId()
            // GET TICKET PRICE

            var price = db.Degrees.Where(x => x.matchID == tkData.matchID && x.ticketClass == tkData.ticketClass).FirstOrDefault().degreePrice;
            

            var options = new ChargeCreateOptions
            {
                Amount = (long)(price+1000),
                Currency = "usd",
                Description = "TICKET charge",
                Source = token,
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            if (charge.Status == "succeeded")
            {

                //Models.TicketyModel.Ticket ob = new Models.TicketyModel.Ticket
                //{ ticketClass =tkData.ticketClass , matchID=tkData.matchID ,
                // UserID = User.Identity.GetUserId() ,Seat_Number=1   };
                //db.Tickets.Add(ob);
                //db.SaveChanges();
                TempData["flaq"] = "payment sucess thanks for purchasing";
                Ticket ticket = new Ticket();
                ticket.ticketID = Guid.NewGuid().ToString();
                ticket.ticketClass = tkData.ticketClass;
                ticket.matchID = tkData.matchID;
                ticket.Seat_Number = db.Degrees.Where(a => a.matchID == tkData.matchID).FirstOrDefault(a => a.ticketClass == tkData.ticketClass).NO_Seat;
                ticket.UserID = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                var newdegree = db.Degrees.Where(a => a.matchID == tkData.matchID).FirstOrDefault(a => a.ticketClass == tkData.ticketClass);
                newdegree.NO_Seat = newdegree.NO_Seat - 1;
                db.SaveChanges();
                TempData["flaq"] = "payment sucess thanks for purchasing";
                return RedirectToAction(nameof(printTicket), new { id = ticket.ticketID });
            }
            else
            {
                TempData["flaq"] = "payment failed";
                return View(tkData);
            }
        }

        /////////////////////////////////////////////////////////////////////////
    }
}