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
        protected Repos repos = new Repos();
    }
}