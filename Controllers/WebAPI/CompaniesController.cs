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
using static WhatupTeam.Models.Entities.ActionResponseEnum;

namespace WhatupTeam.Controllers.WebAPI
{
    public class CompaniesController : ApiController
    {
        private WhatupTeamDatabaseContext db = new WhatupTeamDatabaseContext();

        // GET: api/Companies
        public IQueryable<Company> GetCompany()
        {
            return db.Company;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public ActionResponse PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return ActionResponse.BadRequest;
            }

            if (id != company.CompanyID)
            {
                return ActionResponse.BadRequest;
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return ApiResponse.getResponse("Companies") ? ActionResponse.Update : ActionResponse.Error;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return ActionResponse.NotFound;
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public ActionResponse PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
               return ActionResponse.BadRequest;
            }

            if (!CompanyExists(company.Name, company.Location))
            {
                db.Company.Add(company);
                db.SaveChanges();

                return ApiResponse.getResponse("Companies") ? ActionResponse.AddNew : ActionResponse.Error;
            }
            else
            {
              return ActionResponse.Error;
            }
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public ActionResponse DeleteCompany(int id)
        {
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return ActionResponse.NotFound;
            }

            //Set IsActive to false if Company is deactivated
            company.IsActive = false;
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();

            return ApiResponse.getResponse("Companies") ? ActionResponse.Delete : ActionResponse.Error;
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