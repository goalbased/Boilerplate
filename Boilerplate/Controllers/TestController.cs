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
            throw  new Exception();
            throw new CustomException(ExceptionCode.AccountBlocked);
        }
    }
}