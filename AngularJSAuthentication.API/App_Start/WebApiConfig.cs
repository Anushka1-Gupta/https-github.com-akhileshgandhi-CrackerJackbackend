using AngularJSAuthentication.Model;
using Microsoft.Data.Edm;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;


namespace AngularJSAuthentication.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            //config.MapHttpAttributeRoutes();
            // Web API routes
            //config.MapHttpAttributeRoutes();




            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "DefaultApi1",
               routeTemplate: "api/v1/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings
                               .DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            //}
       //     config.EnableSwagger(c =>
       //      {
       //          c.SingleApiVersion("v1", "LiveSpace APIs");
       //          c.IncludeXmlComments(string.Format(@"{0}\bin\AngularJSAuthentication.API.xml", AppDomain.CurrentDomain.BaseDirectory));
       //          c.DescribeAllEnumsAsStrings();
       //               //c.OAuth2("oauth2")
       //               //       .Description("OAuth2 Password Grant")
       //               //       .Flow("password")
       //               //       .TokenUrl("https://authwebapiapps.azurewebsites.net/oauth/token")
       //               //       .Scopes(scopes =>
       //               //       {
       //               //           scopes.Add("read", "Read access to protected resources");
       //               //           scopes.Add("write", "Write access to protected resources");
       //               //       });
       //               //c.OperationFilter<AssignOAuth2SecurityRequirements>();
       //           })
       //.EnableSwaggerUi();

       //     var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
       //     jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        // public class AssignOAuth2SecurityRequirements : IOperationFilter
        //{
        //    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        //    {
        //        // Determine if the operation has the CustomAuthorizeAttribute attribute
        //        var authorizeAttributes = apiDescription
        //            .ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>();

        //        if (!authorizeAttributes.Any())
        //            return;

        //        // Correspond each "CustomAuthorizeAttribute" role to an oauth2 scope
        //        var scopes =
        //            authorizeAttributes
        //            .SelectMany(attr => attr.Roles.Split(','))
        //            .Distinct()
        //            .ToList();

        //        // Initialize the operation.security property if it hasn't already been
        //        if (operation.security == null)
        //            operation.security = new List<IDictionary<string, IEnumerable<string>>>();

        //        var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
        //            {
        //                { "oauth2", scopes }
        //            };

        //        operation.security.Add(oAuthRequirements);
        //    }
        //}

    }
    }

