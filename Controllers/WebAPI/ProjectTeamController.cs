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
    public class ProjectTeamController : ApiController
    {
        private WhatupTeamDatabaseContext db = new WhatupTeamDatabaseContext();

        // GET: api/ProjectTeam
        public IQueryable<ProjectTeam> GetProjectTeams()
        {
            return db.ProjectTeams;
        }

        // GET: api/ProjectTeam/5
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult GetProjectTeam(int id)
        {
            ProjectTeam projectTeam = db.ProjectTeams.Find(id);
            if (projectTeam == null)
            {
                return NotFound();
            }

            return Ok(projectTeam);
        }

        // PUT: api/ProjectTeam/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectTeam(int id, ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTeam.ProjectTeamID)
            {
                return BadRequest();
            }

            db.Entry(projectTeam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectTeam
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult PostProjectTeam(ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectTeams.Add(projectTeam);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectTeam.ProjectTeamID }, projectTeam);
        }

        // DELETE: api/ProjectTeam/5
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult DeleteProjectTeam(int id)
        {
            ProjectTeam projectTeam = db.ProjectTeams.Find(id);
            if (projectTeam == null)
            {
                return NotFound();
            }

            db.ProjectTeams.Remove(projectTeam);
            db.SaveChanges();

            return Ok(projectTeam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTeamExists(int id)
        {
            return db.ProjectTeams.Count(e => e.ProjectTeamID == id) > 0;
        }
    }
}