
namespace LivrosAPI.Infrastructure.Services
{
    [Serializable]
    public sealed class ServiceException : Exception
    {
        public ServiceResponseStatus Status => ServiceResponseStatus.Error;

        public ServiceException() { }
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, object data) : base(message)
        {
            Data.Add("ApiExceptionData", data);
        }
        public ServiceException(string message, Exception inner) : base(message, inner) { }

        private ServiceException(
           System.Runtime.Serialization.SerializationInfo info,
           System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
