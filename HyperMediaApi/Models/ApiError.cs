﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HyperMediaApi.Models
{
    public class ApiError
    {
        public string Message { get; set; }

        public string Detail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }
    }
}
