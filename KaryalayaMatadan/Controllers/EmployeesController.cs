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
    public class EmployeesController : Controller
    {
        private KaryalayaMatadanContext db = new KaryalayaMatadanContext();
        EmployeeService employeeService = new EmployeeService();

        // GET: Employees
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = employeeService.GetAllEmployee();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employeeService.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                employeeService.EditEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                employeeService.DeleteEmployee(id);
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
