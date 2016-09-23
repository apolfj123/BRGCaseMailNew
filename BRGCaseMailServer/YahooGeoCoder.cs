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

namespace BRGCaseMailServer
{
    public class YahooGeoCoder
    {
        public static bool GeocodeDealer(Dealer _dealer)
        {
            float _lat = 0.0F;
            float _long = 0.0F;

            // minum required for geocoding with Yahoo is City and state or zip
            try
            {
                GeocodeAddress(_dealer.AddrLine1.Replace(' ', '+') + ",+" + _dealer.City.Replace(' ', '+') + ",+" + _dealer.StateCode + ",+" + _dealer.PostalCode, ref _lat, ref _long);
                _dealer.Latitude = _lat;
                _dealer.Longitude = _long;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool GeocodeCase(BankruptcyCase _case)
        {
            float _lat = 0.0F;
            float _long = 0.0F;

            // minum required for geocoding with Yahoo is City and state or zip
            try
            {
                GeocodeAddress(_case.AddrLine1.Replace(' ', '+') + ",+" + _case.City.Replace(' ', '+') + ",+" + _case.StateCode + ",+" + _case.PostalCode, ref _lat, ref _long);
                _case.Latitude = _lat;
                _case.Longitude = _long;
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

                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(BuildYahooUrl(location));
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                XmlDocument _doc = new XmlDocument();
                _doc.Load(BuildYahooUrl(location));

                // Parse the response

                // Set up namespace manager for XPath   
                XmlNamespaceManager ns = new XmlNamespaceManager(_doc.NameTable);
                ns.AddNamespace("yplace", "http://where.yahooapis.com/geocode");
                XmlNodeList nodes = _doc.SelectNodes("/ResultSet/Result/latitude", ns);


                _lat = float.Parse(nodes[0].InnerText);

                XmlNodeList nodes2 = _doc.SelectNodes("/ResultSet/Result/longitude", ns);


                _long = float.Parse(nodes2[0].InnerText);
                
                //XmlNodeList _nodeList = _doc.SelectNodes("//ResultSet");

                //_precision = _doc.ChildNodes[1].ChildNodes[0].Attributes["precision"].InnerText;
                                
                //foreach (XmlNode _node in nodes)
                //{
                //    //if (_node.NodeType == XmlNodeType.Attribute && _node.Name == "precision" )
                //    //{
                //    //    _precision = _node.Attributes["precision"].Value;
                //    //}
                //    if (_node.Name == "Latitude")
                //    {
                //        _lat = float.Parse(_node.InnerText);
                //    }
                //    if (_node.Name == "Longitude")
                //    {
                //        _long = float.Parse(_node.InnerText);
                //        //last thing we are interestd in
                        
                //    }
                //}
                
            }
            catch(Exception ex)
            {
                               
            }
        }        
        
        private static string BuildYahooUrl(string location)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ConfigurationManager.AppSettings["YahooGeocodeUrl"]);
            sb.Append(location);
            sb.AppendFormat("&appid={0}", ConfigurationManager.AppSettings["YahooConsumerKey"]);

            //sb.AppendFormat("?appid={0}",
            //    ConfigurationManager.AppSettings["YahooAppID"],
            //    HttpContext.Current.Server.UrlEncode(location));
            return sb.ToString();
        }

    }
}
