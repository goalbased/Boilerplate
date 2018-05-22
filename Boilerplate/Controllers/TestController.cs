using System;
using System.Web.Http;
using Boilerplate.Exceptions;

namespace Boilerplate.Controllers
{
    //[ApiCorsPolicy]
    public class TestController : ApiController
    {
        [HttpPost]
        public string Post1([FromBody] string value)
        {
            return "Post1" + value;
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