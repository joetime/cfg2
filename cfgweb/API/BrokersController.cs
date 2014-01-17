using cfglib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cfgweb.API
{
    public class BrokersController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<Lookup> Get()
        {
            return repos.Brokers();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Broker Get(int id)
        {
            return repos.Broker(id);
            //return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}