using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class VersioningConventions : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                //Check if route attribute is alredy defined
                var hasRoute = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);
                if (hasRoute)
                {
                    continue;
                }

                //Get the version as last part of namespace
                var version = controller.ControllerType.Namespace.Split('.').LastOrDefault();

                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = string.Format("api/{0}/{1}", version, controller.ControllerName)
                };
            }
        }
    }
}
