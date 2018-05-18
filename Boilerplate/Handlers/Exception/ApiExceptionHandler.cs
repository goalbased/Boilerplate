using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

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

    public class ApiExceptionHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "This is generic exception response"
            };

            return Task.FromResult(0);
        }
    }

    internal class TextPlainErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response =
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(Content),
                    RequestMessage = Request
                };

            return Task.FromResult(response);
        }
    }
}