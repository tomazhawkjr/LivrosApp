using LivrosAPI.Application.Responses;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace LivrosAPI.Infrastructure
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ResponseType(typeof(ServiceResponse))]
        protected async Task<ServiceHttpResult> HandleRequest(IRequest<RetornoService> request, string? messageSucess = null)
        {
            if (!ModelState.IsValid)
                return Error(ModelState);

            var response = await _mediator.Send(request);
            return HandleResult(response);
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult HandleResult(RetornoService response, string? message = null)
        {
            if (!response.Sucesso)
            {
                if (response.TemMensagens)
                    return Error(response.GetListaMensagemToString());

                return Error("Ocorreu um erro.");
            }

            return Success(message ?? "Sucesso!", response.Value, response.ValueFile);
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Success(string message)
        {
            return new ServiceHttpResult(new ServiceResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = null,
                Message = message,
                Status = ServiceResponseStatus.Success
            });
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Success(string message, object data, byte[] dataFile)
        {
            return new ServiceHttpResult(new ServiceResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = data,
                DataFile = dataFile,
                Message = message,
                Status = ServiceResponseStatus.Success
            });
        }
        protected ServiceHttpResult Success(string message, ServiceResponseStatus status)
        {
            return new ServiceHttpResult(new ServiceResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = null,
                Message = message,
                Status = status
            });
        }


        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Success(HttpContent message)
        {
            return new ServiceHttpResult(message, HttpStatusCode.OK);
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Error(string message)
        {
            return new ServiceHttpResult(new ServiceException(message));
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Error(string message, Exception innerException)
        {
            return new ServiceHttpResult(new ServiceException(message, innerException));
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Error(ServiceException exception)
        {
            return new ServiceHttpResult(exception);
        }

        [ResponseType(typeof(ServiceResponse))]
        protected ServiceHttpResult Error(ModelStateDictionary modelState)
        {
            return new ServiceHttpResult(modelState);
        }
    }
}
