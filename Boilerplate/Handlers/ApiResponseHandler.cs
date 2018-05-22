using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Boilerplate.Exceptions;
using Boilerplate.Models.Generic;

namespace Boilerplate.Handlers
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options) return await base.SendAsync(request, cancellationToken);

            var response = await base.SendAsync(request, cancellationToken);

            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return GenerateResponse(request, response);
                }
                else
                {
                    return GenerateBadRequestResponse(request, response);
                }
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
                IsSuccess = true,
                Result = actionResponsed
            };

            var generiApiResponse =
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent<ApiResponse>(customResponse, new JsonMediaTypeFormatter()),
                    RequestMessage = request
                };


            return generiApiResponse;
        }

        private static HttpResponseMessage GenerateBadRequestResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            response.TryGetContentValue(out HttpError actionResponsed);
            var errors = new List<string>();
            var modelStateValues = actionResponsed.ModelState.Values;
            foreach (var modelStateValue in modelStateValues)
            {
                var t = (string[]) modelStateValue;
                errors.AddRange(t.ToList());
            }
            var customResponse = new ApiResponse
            {
                IsSuccess = false,
                Error = new Error
                {
                    Id = (int)ExceptionCode.InvalidateInput,
                    Message = actionResponsed.Message,
                    MessageParameters = errors,
                }
            };

            var generiApiResponse =
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new ObjectContent<ApiResponse>(customResponse, new JsonMediaTypeFormatter()),
                    RequestMessage = request
                };


            return generiApiResponse;
        }
    }
}