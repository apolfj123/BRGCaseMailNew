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
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BRGCaseMailServer
{
    public class GoogleGeocoder
    {
        public static bool GeocodeDealer(Dealer _dealer)
        {
            float _lat = 0.0F;
            float _long = 0.0F;

            // minum required for geocoding with Yahoo is City and state or zip
            try
            {
                //GeocodeAddress(_dealer.StateCode + "/" + HttpUtility.UrlEncode(_dealer.City).Replace("+", "%20") + "/" + _dealer.PostalCode + "/" + HttpUtility.UrlEncode(_dealer.AddrLine1).Replace("+", "%20"), ref _lat, ref _long);
                GeocodeAddress(_dealer.AddrLine1 + ", " + _dealer.City + " " + _dealer.StateCode + " " + _dealer.PostalCode, ref _lat, ref _long);

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

            try
            {
                //var address = "123 something st, somewhere";
                //var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(location));

                //var request = WebRequest.Create(requestUri);
                //var response = request.GetResponse();
                //var xdoc = XDocument.Load(response.GetResponseStream());

                //var result = xdoc.Element("GeocodeResponse").Element("result");
                //var locationElement = result.Element("geometry").Element("location");
                //var lat = locationElement.Element("lat");
                //var lng = locationElement.Element("lng");
                

                //// Retrieve the list of geocoded locations
                ////XmlNodeList Locations = _doc.GetElementsByTagName("location");

                ////// Create a geography Point instance of the first matching location
                //// Retrieve the list of geocoded locations
                //XmlNodeList Locations = result.GetElementsByTagName("location");

                ////// Create a geography Point instance of the first matching location
                //_lat = float.Parse(Locations[0]["latLng"]["lat"].InnerText);
                //_long = float.Parse(Locations[0]["latLng"]["lng"].InnerText);

                string _req = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(location));

                XmlDocument _doc = new XmlDocument();
                _doc.Load(_req);

                // Parse the response
                // Declare the XML namespace used in the geocoded response
                //XmlNamespaceManager nsmgr = new XmlNamespaceManager(_doc.NameTable);
                //nsmgr.AddNamespace("ab", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                // Check that we received a valid response from the geocoding server
                //if (_doc.GetElementsByTagName("StatusCode")[0].InnerText != "200")
                //{
                //    throw new Exception("Didn't get correct response from geocoding server");
                //}

                // Retrieve the list of geocoded locations
                XmlNodeList Locations = _doc.GetElementsByTagName("location");

                //// Create a geography Point instance of the first matching location
                _lat = float.Parse(Locations[0]["lat"].InnerText);
                _long = float.Parse(Locations[0]["lng"].InnerText);

            
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