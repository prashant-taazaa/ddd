using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace todo.api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into GlobalExceptionFilter");
        }
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                   ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                    ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                    : getErrorCode(context.Exception.GetType());
            string errorMessage = context.Exception.Message;
            string stackTrace = context.Exception.StackTrace;


            string body = string.Empty;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";


            var error = JsonConvert.SerializeObject(new { 
            ErrorMessage = errorMessage,
            StackTrace= stackTrace,
            StatusCode = (int)statusCode,
            RequestPath = context.HttpContext?.Request.Path.ToString(),
            RequestHeaders = context.HttpContext?.Request.QueryString.ToString(),
            RequestBody = body,
            RequestMethod = context.HttpContext?.Request.Method
            });


            #region Logging  
            _logger.LogError(errorMessage);
            #endregion Logging  

            response.WriteAsync(error);
        }

        private HttpStatusCode getErrorCode(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case Exceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case Exceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case Exceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case Exceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case Exceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case Exceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case Exceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case Exceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case Exceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case Exceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case Exceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case Exceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }

    public enum Exceptions
    {
        NullReferenceException = 1,
        FileNotFoundException = 2,
        OverflowException = 3,
        OutOfMemoryException = 4,
        InvalidCastException = 5,
        ObjectDisposedException = 6,
        UnauthorizedAccessException = 7,
        NotImplementedException = 8,
        NotSupportedException = 9,
        InvalidOperationException = 10,
        TimeoutException = 11,
        ArgumentException = 12,
        FormatException = 13,
        StackOverflowException = 14,
        SqlException = 15,
        IndexOutOfRangeException = 16,
        IOException = 17
    }
}
