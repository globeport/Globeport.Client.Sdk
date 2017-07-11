using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Exceptions;

namespace Globeport.Shared.Library.ApiModel
{
    public class ApiError
    {
        public string Type { get; set; }

        public string Message { get; set; }

        public string Content { get; set; }

        public ApiError()
        {
        }

        public ApiError(string type)
        {
            Type = type;
        }

        public ApiError(string type, string message, string content = null)
            : this(type)
        {
            Message = message;
            Content = content;
        }

        public ApiError(Exception e)
        {
            Type = e.GetType().Name;
            Message = e.Message;
            if (e is ApiException)
            {
                Content = ((ApiException)e).Content;
            }
        }

        public T GetContent<T>()
        {
            return Content.Deserialize<T>();
        }

        public ApiException ToException()
        {
            switch(Type)
            {
                case nameof(MismatchedEndpointsException):
                    return new MismatchedEndpointsException(Content);
                case nameof(ValidationException):
                    return new ValidationException(Content);
                case nameof(UnsupportedClientException):
                    return new UnsupportedClientException();
            }
            return new ApiException(Message, Content);
        }

        public bool IsException<T>() where T : Exception
        {
            return Type == typeof(T).Name;
        }
    }
}
