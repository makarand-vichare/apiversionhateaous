namespace HyperMediaApi.V2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiVersion( "2.0" )]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values?api-version=2.0
        [HttpGet]
        public string Get()
        {
            return $"Controller = {GetType().Name}\nVersion = {HttpContext.GetRequestedApiVersion()}";
        }
    }
}
