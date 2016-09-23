using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Net;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics; 

namespace BRGCaseMailServer
{
    public class BingGeoCoder
    {
        public static bool GeocodeDealer(Dealer _dealer)
        {
            float _lat = 0.0F;
            float _long = 0.0F;

            // minum required for geocoding with Yahoo is City and state or zip
            try
            {
                GeocodeAddress(_dealer.StateCode + "/" + HttpUtility.UrlEncode(_dealer.City).Replace("+", "%20") + "/" + _dealer.PostalCode + "/" + HttpUtility.UrlEncode(_dealer.AddrLine1).Replace("+", "%20"), ref _lat, ref _long);
                _dealer.Latitude = _lat;
                _dealer.Longitude = _long;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void GeocodeAddress(string location, ref float _lat, ref float _long)
        {
            _lat = 0.0F;
            _long = 0.0F;
            string _precision = string.Empty;
            string key = ConfigurationManager.AppSettings["BingGeoCodeKey"];

            try
            {
                string _req = "http://dev.virtualearth.net/REST/v1/Locations/US/" + location + "?output=xml&key=" + key;
                
                //HttpWebRequest _webReq = (HttpWebRequest)WebRequest.Create(_req);
                //_webReq.Method = "GET";
  
                ////response
                //HttpWebResponse WebResp = (HttpWebResponse)_webReq.GetResponse();
                //Stream WebRespStream = WebResp.GetResponseStream();
                //StreamReader _webStremReader = new StreamReader(WebRespStream);

                //string _respJSON = _webStremReader.ReadToEnd();
                //JObject _jsonObject = JObject.Parse(_respJSON);
                //JArray _respArray = (JArray)_jsonObject["ResourceSet"];
                ////get the status of the reposnse.
                ////string respStatus = _jsonObject["StatusCode"].ToString().Trim();
                ////get lat/long
                //JArray _resultArray = (JArray)_respArray[0]["resources"];
                //if (_resultArray.Count >= 1)
                //{
                //    JArray _geoArray = (JArray)_resultArray[0]["point"]["coordinates"];

                //    _lat = float.Parse(_geoArray[0].ToString());
                //    _long = float.Parse(_geoArray[1].ToString());
                //}

                XmlDocument _doc = new XmlDocument();
                _doc.Load(_req);

                // Parse the response

                // Declare the XML namespace used in the geocoded response
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(_doc.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                // Check that we received a valid response from the geocoding server
                if (_doc.GetElementsByTagName("StatusCode")[0].InnerText != "200")
                {
                    throw new Exception("Didn't get correct response from geocoding server");
                }

                // Retrieve the list of geocoded locations
                XmlNodeList Locations = _doc.GetElementsByTagName("Location");

                // Create a geography Point instance of the first matching location
                _lat = float.Parse(Locations[0]["Point"]["Latitude"].InnerText);
                _long = float.Parse(Locations[0]["Point"]["Longitude"].InnerText);
            
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static bool GeocodeCase(BankruptcyCase _case)
        {
            float _lat = 0.0F;
            float _long = 0.0F;

            // minum required for geocoding with Yahoo is City and state or zip
            try
            {
                GeocodeAddress(_case.StateCode + "/" + HttpUtility.UrlEncode(_case.City).Replace("+", "%20") + "/" + _case.PostalCode + "/" + HttpUtility.UrlEncode(_case.AddrLine1).Replace("+", "%20"), ref _lat, ref _long);
                _case.Latitude = _lat;
                _case.Longitude = _long;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
