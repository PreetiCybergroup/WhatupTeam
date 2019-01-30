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
    public class CompaniesController : ApiController
    {
        private WhatupTeamDatabaseContext db = new WhatupTeamDatabaseContext();

        // GET: api/Companies
        public IQueryable<Company> GetCompany()
        {
            return db.Company.Where(_company => _company.IsActive == true);
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Company with id {0} does not exist",id));
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.CompanyID)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(string.Format("Company details with id {0} has been modified", id));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!CompanyExists(id))
                {
                    return Content(HttpStatusCode.NotFound, string.Format("Company with id {0} does not exist", id));
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, exception.Message);
                }
            }
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }
            try
            { 
            if (!CompanyExists(company.Name, company.Location))
            {
                db.Company.Add(company);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = company.CompanyID }, company);
            }
            else
            {
                return Content(HttpStatusCode.NoContent, "Company already exist");
            }
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
             
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Company with id {0} does not exist", id));
            }
            try
            {
             //Set IsActive to false if Company is deactivated
              company.IsActive = false;
              db.Entry(company).State = EntityState.Modified;
              db.SaveChanges();

              return Ok(string.Format("Company with id {0} has been removed",id));
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

        private bool CompanyExists(string name, string location)
        {
            return db.Company.Count(_company => _company.Name == name && _company.Location == location) > 0;
        }

        private bool CompanyExists(int id)
        {
            return db.Company.Count(_company => _company.CompanyID == id) > 0;
        }

       

      

    }
}