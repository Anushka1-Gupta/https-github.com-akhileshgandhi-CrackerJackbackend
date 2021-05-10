using AngularJSAuthentication.Common.Constants;
using Newtonsoft.Json;
using Nito.AsyncEx;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AngularJSAuthentication.Common.Helpers
{
    public class GeoHelper
    {
        public static double Distance(GeoCoordinate source, GeoCoordinate destination)
        {
            if (destination == null)
                return 0;

            var sCoord = source;
            var eCoord = destination;

            double distance = GoogleDirectionsHelper.CalculateDistance(sCoord, eCoord);

            return distance;
        }

        public static double AerialDistance(GeoCoordinate source, GeoCoordinate destination)
        {
            if (destination == null)
                return 0;

            double distance = source.GetDistanceTo(destination) / 1000;//* 0.00062137;
            return distance;
        }

        public void GetLatLong(string locationStr, out decimal? lat, out decimal? lng)
        {
            locationStr = HttpContext.Current.Server.UrlEncode(locationStr);
            lat = null;
            lng = null;
            GeoCode geoCode;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync("https://maps.google.com/maps/api/geocode/json?key=" + AppConstants.GoogleMapKey + "&address=" + locationStr).Result;

                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;

                geoCode = JsonConvert.DeserializeObject<GeoCode>(responseBody);
                Result resultFirstOrDefault = geoCode.results?.FirstOrDefault();
                if (resultFirstOrDefault != null)
                {
                    lat = resultFirstOrDefault.geometry.location.lat;
                    lng = resultFirstOrDefault.geometry.location.lng;
                }

            }
            catch (System.Exception ex)
            {
                geoCode = new GeoCode();
            }
        }

        public void GetLatLongResult(string locationStr, out Result resultFirstOrDefault)
        {
            locationStr = HttpContext.Current.Server.UrlEncode(locationStr);

            resultFirstOrDefault = null;

            GeoCode geoCode;
            try
            {
                using (GenericRestHttpClient<GeoCode, string> memberClient
                    = new GenericRestHttpClient<GeoCode, string>("https://maps.google.com",
                    "/maps/api/geocode/json?key=" + AppConstants.GoogleMapKey + "&address=" + locationStr, null)) //AIzaSyBIoVMQPWUr5IjMi5f3mexwOhCA17d093A 
                {                    

                    geoCode = AsyncContext.Run(() => memberClient.GetAsync());

                    resultFirstOrDefault = geoCode.results?.FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                geoCode = new GeoCode();
            }
        }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Northeast
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Southwest
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Location
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Northeast2
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Southwest2
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Viewport
    {
        public Northeast2 northeast { get; set; }
        public Southwest2 southwest { get; set; }
    }

    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class GeoCode
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
