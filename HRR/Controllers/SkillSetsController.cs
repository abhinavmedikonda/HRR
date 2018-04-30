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
    builder.EntitySet<SkillSet>("SkillSets");
    builder.EntitySet<Role>("Roles"); 
    builder.EntitySet<Skill>("Skills"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SkillSetsController : ODataController
    {
        private Model db = new Model();

        // GET: odata/SkillSets
        [EnableQuery]
        public IQueryable<SkillSet> GetSkillSets()
        {
            return db.SkillSets;
        }

        // GET: odata/SkillSets(5)
        [EnableQuery]
        public SingleResult<SkillSet> GetSkillSet([FromODataUri] int key)
        {
            return SingleResult.Create(db.SkillSets.Where(skillSet => skillSet.Id == key));
        }

        // PUT: odata/SkillSets(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<SkillSet> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SkillSet skillSet = await db.SkillSets.FindAsync(key);
            if (skillSet == null)
            {
                return NotFound();
            }

            patch.Put(skillSet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillSetExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(skillSet);
        }

        // POST: odata/SkillSets
        public async Task<IHttpActionResult> Post(SkillSet skillSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SkillSets.Add(skillSet);
            await db.SaveChangesAsync();

            return Created(skillSet);
        }

        // PATCH: odata/SkillSets(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<SkillSet> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SkillSet skillSet = await db.SkillSets.FindAsync(key);
            if (skillSet == null)
            {
                return NotFound();
            }

            patch.Patch(skillSet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillSetExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(skillSet);
        }

        // DELETE: odata/SkillSets(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            SkillSet skillSet = await db.SkillSets.FindAsync(key);
            if (skillSet == null)
            {
                return NotFound();
            }

            db.SkillSets.Remove(skillSet);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SkillSets(5)/Role
        [EnableQuery]
        public SingleResult<Role> GetRole([FromODataUri] int key)
        {
            return SingleResult.Create(db.SkillSets.Where(m => m.Id == key).Select(m => m.Role));
        }

        // GET: odata/SkillSets(5)/Skill
        [EnableQuery]
        public SingleResult<Skill> GetSkill([FromODataUri] int key)
        {
            return SingleResult.Create(db.SkillSets.Where(m => m.Id == key).Select(m => m.Skill));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillSetExists(int key)
        {
            return db.SkillSets.Count(e => e.Id == key) > 0;
        }
    }
}
