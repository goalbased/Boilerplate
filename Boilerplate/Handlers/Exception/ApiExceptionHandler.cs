using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Boilerplate.Exceptions;
using Boilerplate.Models.Generic;
using Newtonsoft.Json;

namespace Boilerplate.Handlers.Exception
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var errorMessage = context.ExceptionContext.Exception.ToString();

            Trace.TraceError(errorMessage);
        }
    }

    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)        
        {
            if (context.Exception is CustomException exception)
            {
                var response = new ApiResponse
                {
                    IsSuccess = false,
                    Error = new Error
                    {
                        Id = (int)exception.ExceptionCode,
                        Message = exception.ExceptionCode.ToString(),
                        MessageParameters = exception.MessageParamters
                    }
                };

                context.Result = new ApiErrorResult
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Request = context.ExceptionContext.Request,
                    Content = JsonConvert.SerializeObject(response)
                };
            }
            else
            {
                var response = new ApiResponse
                {
                    IsSuccess = false,
                    Error = new Error
                    {
                        Id = (int)ExceptionCode.SystemError,
                        Message = context.Exception.ToString()
                    }
                };

                context.Result = new ApiErrorResult
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Request = context.ExceptionContext.Request,
                    Content = JsonConvert.SerializeObject(response)
                };
            }
        }
    }

    internal class ApiErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response =
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    StatusCode = HttpStatusCode,
                    Content = new StringContent(Content),
                    RequestMessage = Request
                };

            return Task.FromResult(response);
        }
    }
}
