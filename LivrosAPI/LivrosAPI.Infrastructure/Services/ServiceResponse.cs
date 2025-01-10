using System.Net;

namespace LivrosAPI.Infrastructure.Services
{
    public class ServiceResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public byte[] DataFile { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ServiceResponseStatus Status { get; set; }
    }

    public enum ServiceResponseStatus
    {
        Success = 1,
        Error = 0
    }

}
