using System.Configuration;

namespace AngularJSAuthentication.Common.Constants
{
    public class DbConstants
    {
        public static string AuthContextDbConnection = ConfigurationManager.ConnectionStrings["authcontext"].ConnectionString;


        public static string URL {
            get { return ConfigurationManager.AppSettings["URL"]; }
        }

    }
}
