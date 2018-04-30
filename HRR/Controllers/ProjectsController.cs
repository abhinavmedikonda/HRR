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
using HRR.Models;

namespace HRR.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using HRR;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Project>("Projects");
    builder.EntitySet<Role>("Roles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProjectsController : ODataController
    {
        private Model db = new Model();

        // GET: odata/Projects
        [EnableQuery]
        public IEnumerable<ProjectModel> GetProjects()
        {
            IEnumerable<ProjectModel> projectModels;

            ModelFactory modelFactory = new ModelFactory();

            projectModels = modelFactory.Map(db.Projects.ToList());

            return projectModels;
        }

        // GET: odata/Projects(5)
        [EnableQuery]
        public SingleResult<Project> GetProject([FromODataUri] short key)
        {
            return SingleResult.Create(db.Projects.Where(project => project.Id == key));
        }

        // PUT: odata/Projects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Project> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project project = await db.Projects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            patch.Put(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(project);
        }

        // POST: odata/Projects
        public async Task<IHttpActionResult> Post(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return Created(project);
        }

        // PATCH: odata/Projects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Project> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project project = await db.Projects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            patch.Patch(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(project);
        }

        // DELETE: odata/Projects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Project project = await db.Projects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Projects(5)/Roles
        [EnableQuery]
        public IQueryable<Role> GetRoles([FromODataUri] short key)
        {
            return db.Projects.Where(m => m.Id == key).SelectMany(m => m.Roles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(short key)
        {
            return db.Projects.Count(e => e.Id == key) > 0;
        }
    }
}
