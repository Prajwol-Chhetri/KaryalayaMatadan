using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaryalayaMatadan.Data;
using KaryalayaMatadan.Models;
using KaryalayaMatadan.Services;

namespace KaryalayaMatadan.Controllers
{
    public class MonthlyCandidatesController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        MonthlyCandidateService monthlyCandidateService = new MonthlyCandidateService();

        // GET: MonthlyCandidates
        public ActionResult Index(string searchYear, string searchMonth)
        {
            IEnumerable<MonthlyCandidate> monthlyCandidates = monthlyCandidateService.GetMonthlyCandidates();
            // applying search filter on employees
            if (!(String.IsNullOrEmpty(searchYear) || String.IsNullOrEmpty(searchMonth)))
            {
                monthlyCandidates = monthlyCandidates.Where(s => s.VoteYear == Convert.ToInt32(searchYear) && s.VoteMonth == Convert.ToInt32(searchMonth));
            }
            else
            {
                monthlyCandidates = new List<MonthlyCandidate>();
            }
            return View(monthlyCandidates);
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
