using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaryalayaMatadan.Data;
using Oracle.ManagedDataAccess.Client;
using KaryalayaMatadan.Models;
using KaryalayaMatadan.Services;

namespace KaryalayaMatadan.Controllers
{
    public class JobHistoriesController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        JobHistoryService jobHistoryService = new JobHistoryService();

        // GET: JobHistories
        public ActionResult Index()
        {
            IEnumerable<JobHistory> jobHistories = jobHistoryService.GetAllJobHistory();
            return View(jobHistories);
        }

        // GET: JobHistories/Details/5
        public ActionResult Details(int id)
        {
            JobHistory employee = jobHistoryService.GetJobHistoryById(id);
            return View(employee);
        }

        // GET: JobHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobHistory employee)
        {
            try
            {
                jobHistoryService.AddJobHistory(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: JobHistories/Edit/5
        public ActionResult Edit(int id)
        {
            JobHistory employee = jobHistoryService.GetJobHistoryById(id);
            return View(employee);
        }

        // POST: JobHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobHistory employee)
        {
            try
            {
                jobHistoryService.EditJobHistory(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: JobHistories/Delete/5
        public ActionResult Delete(int id)
        {
            JobHistory employee = jobHistoryService.GetJobHistoryById(id);
            return View(employee);
        }

        // POST: JobHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                jobHistoryService.DeleteJobHistory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
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
