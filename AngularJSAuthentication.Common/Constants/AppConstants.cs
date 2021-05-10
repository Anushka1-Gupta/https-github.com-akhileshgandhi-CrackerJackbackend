using System.Configuration;

namespace AngularJSAuthentication.Common.Constants
{
    public class AppConstants
    {
        public static string GoogleMapKey = ConfigurationManager.AppSettings["GoogleMapKey"];
    }
}
