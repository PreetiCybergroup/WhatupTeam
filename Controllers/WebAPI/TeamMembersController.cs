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
    public class TeamMembersController : ApiController
    {
        private WhatupTeamDatabaseContext db = new WhatupTeamDatabaseContext();

        // GET: api/ProjectTeam
        public IQueryable<TeamMember> GetTeamMembers()
        {
            return db.TeamMembers;
        }

        // GET: api/TeamMember/5
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult GetTeamMember(int id)
        {
            TeamMember TeamMember = db.TeamMembers.Find(id);
            if (TeamMember == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Team Member with id {0} does not exist", id));
            }

            return Ok(TeamMember);
        }

        // PUT: api/TeamMember/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeamMember(int id, TeamMember TeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != TeamMember.TeamMemberID)
            {
                return BadRequest();
            }

            db.Entry(TeamMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(string.Format("Team details with id {0} has been modified", id));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!TeamMemberExists(id))
                {
                    return Content(HttpStatusCode.NotFound, string.Format("Team Member with id {0} does not exist", id));
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, exception.Message);
                }
            }

            
        }

        // POST: api/TeamMember
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult PostTeamMember(TeamMember TeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamMembers.Add(TeamMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = TeamMember.TeamMemberID }, TeamMember);
        }

        // DELETE: api/TeamMember/5
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult DeleteTeamMember(int id)
        {
            TeamMember TeamMember = db.TeamMembers.Find(id);
            if (TeamMember == null)
            {
                return Content(HttpStatusCode.NotFound, string.Format("Team member with id {0} does not exist", id));
            }

            try
            { 
            db.TeamMembers.Remove(TeamMember);
            db.SaveChanges();
                return Ok(string.Format("Company with id {0} has been removed", id));
            }
            catch (Exception ex)
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

        private bool TeamMemberExists(int id)
        {
            return db.TeamMembers.Count(e => e.TeamMemberID == id) > 0;
        }
    }
}