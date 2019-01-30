using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WhatupTeam.Models.DatabaseAccess;
using WhatupTeam.Models.Entities;

namespace WhatupTeam.Controllers.WebAPI
{
    public class employeeController : ApiController
    {
        private WhatupTeamDatabaseContext db = new WhatupTeamDatabaseContext();

        // GET: api/employee
        public IQueryable<Employees> Getemployee()
        {
            return db.Employees.Where(emp=>emp.IsActive == true);
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult GetEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Employee with id {0} does not exist", id));
            }

            return Ok(employees);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(int id, Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != employees.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(string.Format("Employee details with id {0} has been modified", id));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!EmployeeExists(id))
                {
                    return Content(HttpStatusCode.NotFound, string.Format("Company with id {0} does not exist", id));
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, exception.Message);
                }
            }
        }

        // POST: api/Employees
        [ResponseType(typeof(Employees))]
        public IHttpActionResult PostEmployees(Employees employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
            if (!EmployeeExists(employee.FirstName, employee.UserName, employee.CompanyID))
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
            }
            else
                {
                    return Content(HttpStatusCode.NoContent, "Employee already exist");
                }
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);

            }
            
        }

        // DELETE: api/employee/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult DeleteEmployees(int id)
        {
            Employees employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Employee with id {0} does not exist", id));
            }
            try
            { 
             // set IsActive to false to remove Employee
            employee.IsActive = false;
            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
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

        private bool EmployeeExists(string name, string userName, int companyID)
        {
           return db.Employees.Count(emp => emp.FirstName == name && emp.UserName == userName && emp.CompanyID == companyID) > 0;
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(emp => emp.EmployeeID == id) > 0;
        }
    }
}