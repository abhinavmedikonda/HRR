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
using System.Web.Http.OData.Routing;
using HRR;

namespace HRR.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using HRR;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Skill>("Skills");
    builder.EntitySet<SkillSet>("SkillSets"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SkillsController : ODataController
    {
        private Model db = new Model();

        // GET: odata/Skills
        [EnableQuery]
        public IQueryable<Skill> GetSkills()
        {
            return db.Skills;
        }

        // GET: odata/Skills(5)
        [EnableQuery]
        public SingleResult<Skill> GetSkill([FromODataUri] short key)
        {
            return SingleResult.Create(db.Skills.Where(skill => skill.Id == key));
        }

        // PUT: odata/Skills(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Skill> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Skill skill = await db.Skills.FindAsync(key);
            if (skill == null)
            {
                return NotFound();
            }

            patch.Put(skill);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(skill);
        }

        // POST: odata/Skills
        public async Task<IHttpActionResult> Post(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skills.Add(skill);
            await db.SaveChangesAsync();

            return Created(skill);
        }

        // PATCH: odata/Skills(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Skill> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Skill skill = await db.Skills.FindAsync(key);
            if (skill == null)
            {
                return NotFound();
            }

            patch.Patch(skill);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(skill);
        }

        // DELETE: odata/Skills(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Skill skill = await db.Skills.FindAsync(key);
            if (skill == null)
            {
                return NotFound();
            }

            db.Skills.Remove(skill);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Skills(5)/SkillSets
        [EnableQuery]
        public IQueryable<SkillSet> GetSkillSets([FromODataUri] short key)
        {
            return db.Skills.Where(m => m.Id == key).SelectMany(m => m.SkillSets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillExists(short key)
        {
            return db.Skills.Count(e => e.Id == key) > 0;
        }
    }
}
