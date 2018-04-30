using HRR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRR.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<ProjectModel> Get()
        {
            ModelFactory modelFactory = new ModelFactory();

            using (Model model = new Model())
            {
                return modelFactory.Map(model.Projects.ToList());
                //return model.Projects.ToList();
            }


            //return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/values/Projects")]
        public IEnumerable<ProjectModel> Projects()
        {
            ModelFactory modelFactory = new ModelFactory();

            using (Model model = new Model())
            {
                return modelFactory.Map(model.Projects.ToList());
                //return model.Projects.ToList();
            }
        }

        [HttpGet]
        [Route("api/values/Roles")]
        public IEnumerable<RoleModel> Roles()
        {
            ModelFactory modelFactory = new ModelFactory();

            using (Model model = new Model())
            {
                return modelFactory.Map(model.Roles.ToList());
                //return model.Projects.ToList();
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
