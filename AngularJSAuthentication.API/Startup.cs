using AngularJSAuthentication.API.App_Start;
using AngularJSAuthentication.API.Providers;
using AngularJSAuthentication.Infrastructure;
using AngularJSAuthentication.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin.Security;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using System.Web.Optimization;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))]

namespace AngularJSAuthentication.API
{
    public class Startup
    {
        string Url = ConfigurationManager.AppSettings["BaseURL"];
        public static string MLExePath = ConfigurationManager.AppSettings["MLExePath"];
        public static string CloudName = ConfigurationManager.AppSettings["CloudName"];
        public static string APIKey = ConfigurationManager.AppSettings["APIKey"];
        public static string APISecret = ConfigurationManager.AppSettings["APISecret"];
        public static string smsauthKey = ConfigurationManager.AppSettings["SMSKey"];
        public static string FreezedAsignmentCopyFilePath = ConfigurationManager.AppSettings["FreezedAsignmentCopyFilePath"];

        
    }
}