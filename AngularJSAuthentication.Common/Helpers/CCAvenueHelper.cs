using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace AngularJSAuthentication.Common.Helpers
{
    public class CCAvenueHelper
    {
        // GET api/<controller>

        public string GetRsaKey(string hdfcOrderId, double amount)
        {
            string vParams = string.Empty;
            string queryUrl = ConfigurationManager.AppSettings["CcAvenueRSAURL"]; //"https://test.ccavenue.com/transaction/getRSAKey";
            string merchantId = ConfigurationManager.AppSettings["CcAvenueMerchantId"];  //"222355";
            string accessCode = ConfigurationManager.AppSettings["CcAvenueAccessCode"];  //"AVCT02GF76BJ43TCJB";


            vParams += "merchant_id" + "=" + merchantId + "&";
            vParams += "order_id" + "=" + hdfcOrderId + "&";
            vParams += "amount" + "=" + amount.ToString() + "&";
            vParams += "currency" + "=" + "INR" + "&";
            vParams += "access_code" + "=" + accessCode + "&";

            String message = postPaymentRequestToGateway(queryUrl, vParams);

            return message;

        }

        private static string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {
            String message = "";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StreamWriter myWriter = null;// it will open a http connection with provided url
            WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
            objRequest.Method = "POST";
            //objRequest.ContentLength = TranRequest.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(urlParam);//send data
            myWriter.Close();//closed the myWriter object

            // Getting Response
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                message = sr.ReadToEnd();
            }

            return message;
        }
    }
}
