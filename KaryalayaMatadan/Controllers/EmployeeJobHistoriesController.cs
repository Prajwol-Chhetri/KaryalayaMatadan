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
    public class EmployeeJobHistoriesController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        EmployeeJobHistoryService employeeJobHistoryService = new EmployeeJobHistoryService();

        // GET: EmployeeJobHistories
        public ActionResult Index()
        {
            IEnumerable<EmployeeJobHistory> employeeJobHistories = employeeJobHistoryService.GetEmployeeJobHistories();
            return View(employeeJobHistories);
        }

        // GET: EmployeeJobHistories/Details/5
        public ActionResult Details(int id)
        {
            EmployeeJobHistory employeeJobHistory = employeeJobHistoryService.GetEmployeeJobHistoryById(id);
            return View(employeeJobHistory);
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
