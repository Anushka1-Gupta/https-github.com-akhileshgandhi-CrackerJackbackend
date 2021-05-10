using AngularJSAuthentication.Common.Constants;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace AngularJSAuthentication.Common.Helpers
{
    public class GoogleDirectionsHelper
    {
        public static double CalculateDistance(GeoCoordinate Origin, GeoCoordinate Destination)
        {
            double Miles = 0;
            string Ans = string.Empty;
            string xmlResult = null;
            //Pass request to google api with orgin and destination details
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + Origin.Latitude + "," + Origin.Longitude + "&destinations=" + Destination.Latitude + "," + Destination.Longitude + "&mode=driving&units=imperial&language=us-en&sensor=false&key=" + AppConstants.GoogleMapKey);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Get response as stream from httpwebresponse
            StreamReader resStream = new StreamReader(response.GetResponseStream());

            //Create instance for xml document
            XmlDocument doc = new XmlDocument();

            //Load response stream in to xml result
            xmlResult = resStream.ReadToEnd();

            //Load xmlResult variable value into xml documnet
            doc.LoadXml(xmlResult);


            try
            {

                XmlNodeList xnList = doc.SelectNodes("/DistanceMatrixResponse");
                foreach (XmlNode xn in xnList)
                {
                    if (xn["status"].InnerText.ToString() == "OK")
                    {
                        string KM = doc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/distance/text").InnerText;
                        // KM = KM.Replace("km", "");
                        // Miles =Convert.ToDouble(KM) * 0.621371192;
                        if (KM.Contains("mi"))
                        {
                            KM = KM.Replace("mi", "");
                            Miles = Convert.ToDouble(KM);
                        }
                        else
                        {
                            KM = KM.Replace("ft", "");
                            Miles = Convert.ToDouble(KM) * 0.000189394;
                        }
                        //Ans = Math.Round(Miles, 2).ToString();
                        //if (double.Parse(Ans) < 10)
                        //{
                        //    Ans = "0" + Ans;
                        //}
                        //DtAns.Rows.Add(dt.Rows[RowID]["RowID"].ToString(), dt.Rows[RowID]["Account"].ToString(), dt.Rows[RowID]["Latitude"].ToString(), dt.Rows[RowID]["Longtitude"].ToString(), dt.Rows[RowID]["Address"].ToString(), Ans);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Miles;
        }


        public static List<KeyValuePair<string, double>> CalculateDistance(string source, string destinationList)
        {
            List<KeyValuePair<string, double>> destinationDistances = new List<KeyValuePair<string, double>>();
            double Miles = 0;
            string Ans = string.Empty;
            string xmlResult = null;
            //Pass request to google api with orgin and destination details
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + source + "&destinations=" + destinationList + "&mode=driving&units=imperial&language=us-en&sensor=false&key=" + AppConstants.GoogleMapKey);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Get response as stream from httpwebresponse
            StreamReader resStream = new StreamReader(response.GetResponseStream());

            //Create instance for xml document
            XmlDocument doc = new XmlDocument();

            //Load response stream in to xml result
            xmlResult = resStream.ReadToEnd();

            //Load xmlResult variable value into xml documnet
            doc.LoadXml(xmlResult);


            try
            {

                XmlNodeList xnList = doc.SelectNodes("/DistanceMatrixResponse");
                foreach (XmlNode xn in xnList)
                {
                    if (xn["status"].InnerText.ToString() == "OK")
                    {
                        XmlNodeList destinationNodesList = doc.SelectNodes("/DistanceMatrixResponse/row/element/distance/text");
                        int i = 0;
                        var destinations = destinationList.Split('|').ToList();
                        foreach (var item in destinationNodesList)
                        {
                            string KM = ((XmlElement)item).InnerText;//item.SelectSingleNode("/DistanceMatrixResponse/row/element/distance/text").InnerText;
                            // KM = KM.Replace("km", "");
                            // Miles =Convert.ToDouble(KM) * 0.621371192;
                            if (KM.Contains("mi"))
                            {
                                KM = KM.Replace("mi", "");
                                Miles = Convert.ToDouble(KM);
                            }
                            else
                            {
                                KM = KM.Replace("ft", "");
                                Miles = Convert.ToDouble(KM) * 0.000189394;
                            }

                            destinationDistances.Add(new KeyValuePair<string, double>(destinations[i++], Miles));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return destinationDistances;
        }
    }
}
