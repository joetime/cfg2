using cfglib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace cfgweb.API
{
    public class BaseApiController : ApiController
    {
        protected Repos repos;

        public BaseApiController()
        {
            // use the userName for audits
            string username = (User.Identity.Name == "")
                ? "API"
                : User.Identity.Name;
            
            repos = new Repos(username);
        }
    }
}