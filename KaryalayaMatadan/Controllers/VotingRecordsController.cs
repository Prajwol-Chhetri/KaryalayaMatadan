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
    public class VotingRecordsController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        VotingRecordService votingRecordService = new VotingRecordService();

        // GET: VotingRecords
        public ActionResult Index(string searchString)
        {
            IEnumerable<VotingRecord> votingRecords = votingRecordService.GetVotingRecords();
            // applying search filter on employees
            if (!String.IsNullOrEmpty(searchString))
            {
                votingRecords = votingRecords.Where(s => s.VoterID == Convert.ToInt32(searchString));
            }
            return View(votingRecords);
        }

        // GET: VotingRecords/Details/5
        public ActionResult Details(int id)
        {
            VotingRecord votingRecord = votingRecordService.GetVotingRecordById(id);
            return View(votingRecord);
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
