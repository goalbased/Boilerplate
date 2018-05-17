using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Boilerplate.Attributes;

namespace Boilerplate.Controllers
{
    [ApiCorsPolicy]
    public class TestController : ApiController
    {
        public string Get(string value)
        {
            return value + DateTime.Now;
        }

        public string Post([FromBody]string value)
        {
            return value + DateTime.Now;
        }
    }
}
