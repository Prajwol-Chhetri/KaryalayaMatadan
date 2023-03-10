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
    public class DepartmentsController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        DepartmentService departmentService = new DepartmentService();

        // GET: Departments
        public ActionResult Index()
        {
            IEnumerable<Department> departments = departmentService.GetAllDepartment();
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int id)
        {
            Department department = departmentService.GetDepartmentById(id);
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            try
            {
                departmentService.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            Department department = departmentService.GetDepartmentById(id);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            try
            {
                departmentService.EditDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            Department department = departmentService.GetDepartmentById(id);
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                departmentService.DeleteDepartment(id);
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
