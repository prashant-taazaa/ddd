using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Extensions;

namespace todo.api.Filters
{
    public class GlobalRequestResponseLogFilter : IActionFilter
    {
        private readonly ILogger<GlobalRequestResponseLogFilter> _logger;
        public GlobalRequestResponseLogFilter(ILogger<GlobalRequestResponseLogFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var request = JsonConvert.SerializeObject(new
            {
                Path = context.HttpContext.Request.Path.Value,
                Method = context.HttpContext.Request.Method,
                UserId = context.HttpContext.GetUserSid(),
                AuthToken= context.HttpContext.Request.Headers.ContainsKey("Authorization")?
                context.HttpContext.Request.Headers["Authorization"].FirstOrDefault():string.Empty
               // Body = streamToString(context.HttpContext.Request.Body)
            });

            _logger.LogInformation(request);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var response = JsonConvert.SerializeObject(new
            {
                Path = context.HttpContext.Request.Path.Value,
                Method = context.HttpContext.Request.Method,
                UserId = context.HttpContext.GetUserSid(),
                AuthToken = context.HttpContext.Request.Headers.ContainsKey("Authorization") ?
                 context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() : string.Empty,
                ResponseStatus = context.HttpContext.Response.StatusCode
                //Response = streamToString(context.HttpContext.Response.Body)
            });

            _logger.LogInformation(response);
        }

        private string streamToString(Stream stream)
        {
            // convert stream to string
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            return text;
        }
    }
}
