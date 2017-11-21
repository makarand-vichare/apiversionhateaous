using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.v2
{
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<DateTime> Get()
        {
            return new DateTime[] { DateTime.Now, DateTime.Now.AddDays(1) };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public DateTime Get(int id)
        {
            return DateTime.Now;
        }

        // POST api/values

    }


  
}
