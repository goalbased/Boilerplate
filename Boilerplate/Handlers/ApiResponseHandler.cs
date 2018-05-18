using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Boilerplate.Models.Generic;

namespace Boilerplate.Handlers
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
               return await base.SendAsync(request, cancellationToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            try
            {
                return GenerateResponse(request, response);
            }
            catch (System.Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static HttpResponseMessage GenerateResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            response.TryGetContentValue(out object actionResponsed);

            var customResponse = new ApiResponse
            {
                IsSuccess= true,
                Result = actionResponsed
            };

            var generiApiResponse =
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent<ApiResponse>(customResponse, new JsonMediaTypeFormatter()),
                    RequestMessage = request
                };


            return generiApiResponse;
        }
    }
}