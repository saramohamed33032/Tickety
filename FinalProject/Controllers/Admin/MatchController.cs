using FinalProject.Models;
using FinalProject.Models.TicketyModel;
using FinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers.Admin
{

    [Authorize(Roles = "MasterAdmin , Admin")]
    public class MatchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Match
        //هنا هيضيف ماتش بين فريقين ويحدد الدجري والسعر لكل دجري ويحدد الملعب اللي هيتلعب عليه 
        public ActionResult Index()
        {
            return View(db.Matches.Where(a => a.isValid == true).ToList());
        }
        public ActionResult UnVisibleMatch()
        {
            return View(db.Matches.Where(a => a.isValid == false).ToList());
        }
        public ActionResult AddMatch()
        {
            ViewBag.HomeTeamId = new SelectList(db.Teams, "teamID", "teamName");
            ViewBag.GuestTeamId = new SelectList(db.Teams, "teamID", "teamName");
            ViewBag.staduimID = new SelectList(db.Staduims, "staduimID", "staduimName");
            return View();
        }
        public ActionResult MatchDetails(string id)
        {
            MatchDegreeVM vm = new MatchDegreeVM();
            vm.Match = db.Matches.FirstOrDefault(a => a.matchID == id);
            vm.Degree = db.Degrees.Where(a => a.matchID == id).ToList();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMatch(Match match, double PriceA, int NoA, double PriceB, int NoB, double PriceC, int NoC)
        {
            var staduim = db.Matches.Where(a => a.matchDate == match.matchDate).FirstOrDefault(a => a.staduimID == match.staduimID);

            if (ModelState.IsValid && staduim == null && match.HomeTeamId != match.GuestTeamId)
            {
                match.matchID = Guid.NewGuid().ToString();
                db.Matches.Add(match);
                Degree d1 = new Degree();
                Degree d2 = new Degree();
                Degree d3 = new Degree();
                d1.matchID = d2.matchID = d3.matchID = match.matchID;
                d1.ticketClass = "A";
                d1.degreePrice = PriceA;
                d1.NO_Seat = NoA;
                d2.ticketClass = "B";
                d2.degreePrice = PriceB;
                d2.NO_Seat = NoB;
                d3.ticketClass = "C";
                d3.degreePrice = PriceC;
                d3.NO_Seat = NoC;
                db.Degrees.Add(d1);
                db.Degrees.Add(d2);
                db.Degrees.Add(d3);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            if (staduim != null)
            {
                ViewBag.msg = "There Are Match in " + match.matchDate.Date.ToString("yyyy-MM-dd") + " at " + staduim.Staduim.staduimName + " Staduim";
            }
            if (match.HomeTeamId == match.GuestTeamId)
            {
                ViewBag.msg = "Same Team";
            }
            ViewBag.HomeTeamId = new SelectList(db.Teams, "teamID", "teamName");
            ViewBag.GuestTeamId = new SelectList(db.Teams, "teamID", "teamName");
            ViewBag.staduimID = new SelectList(db.Staduims, "staduimID", "staduimName");
            return View(match);
        }
        public ActionResult DeleteMatch(string id)
        {
            var match = db.Matches.FirstOrDefault(a => a.matchID == id);
            if (match.isValid == false)
            {
                match.isValid = true;
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(UnVisibleMatch));
            }
            match.isValid = false;
            db.Entry(match).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult EditMatch(string id)
        {
            ViewBag.staduimID = new SelectList(db.Staduims, "staduimID", "staduimName");
            return View(db.Matches.FirstOrDefault(a => a.matchID == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMatch(Match match)
        {
            var staduim = db.Matches.Where(a => a.matchDate == match.matchDate).FirstOrDefault(a => a.staduimID == match.staduimID);
            if (ModelState.IsValid&&((staduim != null && match.matchID == staduim.matchID)|| staduim == null))
            {
                if (staduim != null && match.matchID == staduim.matchID)
                {
                    staduim.matchTime = match.matchTime;
                }
                else if(staduim==null)
                {
                    db.Entry(match).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.msg = "There Are Match in " + match.matchDate.Date.ToString("yyyy-MM-dd") + " at " + staduim.Staduim.staduimName + " Staduim";

            ViewBag.staduimID = new SelectList(db.Staduims, "staduimID", "staduimName");
            return View(match);
        }
    }
}