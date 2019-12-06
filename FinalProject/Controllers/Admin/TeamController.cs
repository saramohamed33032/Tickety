using FinalProject.Models;
using FinalProject.Models.TicketyModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers.Admin
{
    [AllowAnonymous]
    public class TeamController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Team
        //هيضيف فرق جديده عندنا ويعدل ف القديمه مثلا 
        public ActionResult Index()
        {
            return View(db.Teams.Where(a => a.isValid == true).ToList());
        }

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult UnVisibleTeam()
        {
            return View(db.Teams.Where(a => a.isValid == false).ToList());
        }

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult AddTeam()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult AddTeam(Team team, HttpPostedFileBase teamLogo)
        {
            var check = db.Teams.FirstOrDefault(a => a.teamName == team.teamName);
            if (team.isValid && check == null)
            {
                team.teamID = Guid.NewGuid().ToString();
                team.isValid = true;
                string path = Server.MapPath("~/img/");
                teamLogo.SaveAs(path + teamLogo.FileName);
                team.teamLogo = teamLogo.FileName;
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.sameTeam = "This name is reserved";
            return View(team);
        }

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult DeleteTeam(string id)
        {
            var team = db.Teams.FirstOrDefault(a => a.teamID == id);
            if (team.isValid == false)
            {
                team.isValid = true;
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(UnVisibleTeam));
            }
                team.isValid = false;
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult EditTeamName(string id)
        {
            var team = db.Teams.FirstOrDefault(a => a.teamID == id);
            return View(team);
        }

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult EditTeamLogo(string id)
        {
            var team = db.Teams.FirstOrDefault(a => a.teamID == id);
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "MasterAdmin , Admin")]
        public ActionResult EditTeam(Team team, HttpPostedFileBase teamLogo)
        {
            var T = db.Teams.FirstOrDefault(a => a.teamID == team.teamID);
            if (team.isValid)
            {
                if (team.teamLogo != null)
                {
                    string path = Server.MapPath("~/img/");
                    teamLogo.SaveAs(path + teamLogo.FileName);
                    T.teamLogo = teamLogo.FileName;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    T.teamName = team.teamName;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}