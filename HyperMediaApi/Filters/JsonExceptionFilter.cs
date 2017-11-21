using HyperMediaApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyperMediaApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        public JsonExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if(env.IsDevelopment())
            {
                error.Detail = context.Exception.StackTrace;
                error.Message = context.Exception.Message;
            }
            else
            {
                error.Detail = "An server error occured";
                error.Detail = context.Exception.Message;
            }
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
