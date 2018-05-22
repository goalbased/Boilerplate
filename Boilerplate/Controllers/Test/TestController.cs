using System;
using System.Web.Http;
using Boilerplate.Attributes;
using Boilerplate.Controllers.Test.DTO;
using Boilerplate.Exceptions;

namespace Boilerplate.Controllers.Test
{
    //[ApiCorsPolicy]
    public class TestController : ApiController
    {
        [ValidateModelState]
        [HttpPost]
        public IHttpActionResult Post1(Post1Input input)
        {
            var output = new Post1Output
            {
                Result = "test " + input.Name,
                DateTime = DateTime.Now
            };

            return Ok(output);
        }

        [HttpPost]
        public string Post2([FromBody] string value)
        {
            return "Post2" + value;
        }

        [HttpGet]
        public string Name(string value)
        {
            return value + DateTime.Now;
        }

        [HttpGet]
        public string Exception()
        {
            //throw  new Exception();
            //account blocked, try 30 mins later
            //only return the error code 1002(AccountBlocked) and a "30" string to frontend in production env but in devlopment env will return one more field "message" for esaily debug
            throw new CustomException(ExceptionCode.AccountBlocked, "30");
        }
    }
}