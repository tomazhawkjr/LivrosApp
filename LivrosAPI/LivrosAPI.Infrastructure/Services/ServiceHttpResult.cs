using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace LivrosAPI.Infrastructure.Services
{
    public class ServiceHttpResult : IActionResult
    {
        private readonly HttpStatusCode _statusCode;
        private readonly HttpRequestMessage _httpRequest;
        private readonly HttpContent _httpContent;
        private ServiceResponse _serviceResponse;

        public ServiceResponse ServiceResponse
        {
            get => _serviceResponse;
            private set => _serviceResponse = value;
        }

        public ServiceHttpResult(ServiceResponse response)
        {
            _serviceResponse = new ServiceResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = response,
                DataFile = response.DataFile,
                Status = ServiceResponseStatus.Success,
                Message = response.Message
            };

            _statusCode = response.StatusCode;
            _httpContent = null;
        }

        public ServiceHttpResult()
        {
            ServiceResponse = null;
            _statusCode = 0;
            _httpRequest = null;
            _httpContent = null;
        }

        public ServiceHttpResult(ServiceException response)
        {
            _serviceResponse = new ServiceResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = response,
                Status = ServiceResponseStatus.Error,
                Message = response.Message
            };

            _statusCode = _serviceResponse.StatusCode;
            _httpContent = null;
        }

        public ServiceHttpResult(ModelStateDictionary modelState)
        {
            var errors = modelState.Select(x => x.Value.Errors)
                .Where(y => y.Count > 0)
                .Select(u => u.FirstOrDefault()?.ErrorMessage)
                .ToList();


            _serviceResponse = new ServiceResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = errors,
                Status = ServiceResponseStatus.Error,
                Message = errors.FirstOrDefault() ?? "Formato de requisição inválido"
            };

            _statusCode = _serviceResponse.StatusCode;
            _httpContent = null;
        }

        public ServiceHttpResult(HttpContent content, HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
            _httpContent = content;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = _httpContent ?? new ObjectContent<ServiceResponse>(_serviceResponse, new JsonMediaTypeFormatter(), new MediaTypeHeaderValue("application/json")),
                StatusCode = _statusCode,
                RequestMessage = _httpRequest
            };

            return Task.FromResult(response);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_serviceResponse.Data)
            {
                StatusCode = _serviceResponse.Status == ServiceResponseStatus.Success ? 200 : 500
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
