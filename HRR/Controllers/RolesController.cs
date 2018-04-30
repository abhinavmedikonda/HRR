using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using HRR;

namespace HRR.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using HRR;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Role>("Roles");
    builder.EntitySet<Project>("Projects"); 
    builder.EntitySet<SkillSet>("SkillSets"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RolesController : ODataController
    {
        private Model db = new Model();

        // GET: odata/Roles
        [EnableQuery]
        public IQueryable<Role> GetRoles()
        {
            return db.Roles;
        }

        // GET: odata/Roles(5)
        [EnableQuery]
        public SingleResult<Role> GetRole([FromODataUri] short key)
        {
            return SingleResult.Create(db.Roles.Where(role => role.Id == key));
        }

        // PUT: odata/Roles(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Role> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Role role = await db.Roles.FindAsync(key);
            if (role == null)
            {
                return NotFound();
            }

            patch.Put(role);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(role);
        }

        // POST: odata/Roles
        public async Task<IHttpActionResult> Post(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(role);
            await db.SaveChangesAsync();

            return Created(role);
        }

        // PATCH: odata/Roles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Role> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Role role = await db.Roles.FindAsync(key);
            if (role == null)
            {
                return NotFound();
            }

            patch.Patch(role);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(role);
        }

        // DELETE: odata/Roles(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Role role = await db.Roles.FindAsync(key);
            if (role == null)
            {
                return NotFound();
            }

            db.Roles.Remove(role);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Roles(5)/Project
        [EnableQuery]
        public SingleResult<Project> GetProject([FromODataUri] short key)
        {
            return SingleResult.Create(db.Roles.Where(m => m.Id == key).Select(m => m.Project));
        }

        // GET: odata/Roles(5)/SkillSets
        [EnableQuery]
        public IQueryable<SkillSet> GetSkillSets([FromODataUri] short key)
        {
            return db.Roles.Where(m => m.Id == key).SelectMany(m => m.SkillSets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(short key)
        {
            return db.Roles.Count(e => e.Id == key) > 0;
        }
    }
}
