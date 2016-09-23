using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

/* 2011
 * Byron Whitlock
 * byron@gmail.com
 * 
 * Parses court information using the pacer system
 * www.pacer.gov/documents/pacermanual.pdf
 */

namespace BRGCaseMailServer
{

    public struct CaseSearchOptions
    {

        // these are the options you requested.
        public enum date_types { filed, entered, discharged, dismissed, closed, converted };
        public enum case_types { ap, bk, mp };
        public enum chapters { seven = 7, nine = 9, eleven = 11, twelve = 12, thirteen = 13, fifteen = 15, _304 = 304 };

        public date_types date_type;
        public List<case_types> case_type;
        public List<chapters> chapter;

        public Boolean open_cases;
        public Boolean closed_cases;


        public String office; // populated from the court            


        public String txtclue; // the attornies first,last and bar id

        private DateTime _start;
        public DateTime start
        {
            set { this._start = value; }
            get { if (this._start == null) return DateTime.Today; else return _start; }
        }

        private DateTime _end;
        public DateTime end
        {
            set { this._end = value; }
            get { if (this._end == null) return DateTime.Today; else return _end; }
        }

        public Boolean party_information;
        public Boolean proseonly;

    }


    public class FindCasesResult
    {
        public String retreived_at;
        public String description;
        public String criteria;
        public int billable_pages;
        public float cost;

        // url to post to get the formatted data.
        public String url;
    }


    /* 2011
     * Byron Whitlock
     * byron@gmail.com
     * 
     * Parses court information using the pacer system
     * www.pacer.gov/documents/pacermanual.pdf
     */

        public class BankruptcyParser
        {
            public string lastError = null;


            private string court = null;
            private string user = null;
            private string pass = null;
            private string loginUrl() { return baseUrl() + "/cgi-bin/login.pl"; }
            private string caseSearchUrl() { return baseUrl() + "/cgi-bin/CaseFiled-Rpt.pl"; }
            private string nexGenLoginUrl() { return nexGenBaseUrl() + baseUrl() + @"/cgi-bin/showpage.pl?16"; }

            private Crawler crawler = new Crawler();

            public BankruptcyParser(string court, string user, string pass)
            {
                this.court = court;
                this.user = user;
                this.pass = pass;
            }

            private string baseUrl()
            {
                return "https://" + court + ".uscourts.gov";
            }

            private string nexGenBaseUrl()
            {
                //var tempstring = "https://pacer.login.uscourts.gov/csologin/login.jsf?pscCourtId=ORBK&appurl=https://ecf.orb.uscourts.gov/cgi-bin/showpage.pl?16";
                return "https://pacer.login.uscourts.gov/csologin/login.jsf?pscCourtId=" + GetCourtAcronym() + "K&appurl=";
            }

            private string GetCourtAcronym()
            {
                return court.Substring(4).ToUpper();
            }


            public void useFiddler()
            {
                crawler.useFiddler = true;
            }
            ///<summary>
            /// Warning: you will be billed for the total number of pages (this report is not subject to the 30-page limit on PACER charges).
            /// returns the data and incurres charges.
            /// returns a CSV in the form described in www.pacer.gov/documents/pacermanual.pdf page 75
            /// Case Year|Case Sequence|Chapter|Date Filed|Court Name|Last Name|First Name| Middle Name|Generation|Social Security Number|Tax Id|Street Address|Address 2|Address 3|City|State|5 Digit Zip Code|4 Digit Zip Code Extension
            ///</summary>
            public String PurchaseCases(FindCasesResult options)
            {
                if (options.url == null)
                {
                    return "No Results Found\n";
                }
                else
                {
                    return crawler.Get(options.url);
                }
            }

            ///<summary>
            ///Office - Indicates the divisional office in which the case was filed.
            ///Case type - “ap” for Adversary Proceeding, “bk” for Bankruptcy, or “mp” for Miscellaneous Proceeding.
            ///Chapter - You may choose any or all chapters to be included in the report. Use the control button on the keyboard when clicking on non-consecutive items in the list to highlight specific items.
            ///Trustee - You may choose to sort reports for cases assigned to a trustee.
            ///Date Type - You may choose to display the report according to Filed Date, Entered Date, Discharged Date, Dismissed Date, Closed Date, or Converted Date.
            ///From/to - Choose a beginning and ending date to display all cases between the dates entered. Cases displayed will be representative of the sort
            ///NOTE: Do not enter a date range that is too broad. Doing so can result in difficulty receiving the data in addition to accruing charges for a large report.
            ///June 2010 74
            ///option (Date Type) above. Enter dates in MM/DD/YYYY format.
            ///Open cases/Closed cases - Click these boxes to produce a report containing open cases, closed cases, or both (if both boxes are checked) filed within the date range selected.
            ///Party information - Click this box to display addresses for all parties listed on the report.
            ///Sort By - Multiple sort options are provided within this report. They are Sort by Filed Date, Entered Date, Case Number, Case Type, (Divisional) Office, and Trustee.
            ///</summary>
            ///<remarks>
            ///</remarks>
            public FindCasesResult FindCases(CaseSearchOptions options)
            {
                //navigate to the search page
                String formAction = baseUrl() + crawler.GetFormAction(caseSearchUrl());

                //remove the .. (alabama north version 5.0)
                formAction = formAction.Replace("..", "");

                String response = crawler.Post(formAction, createPost(options), true);
                return parseResponse(response);
            }


            private FindCasesResult parseResponse(String response)
            {
                FindCasesResult result = null;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(response);

                if (response.Length < 2500)
                {

                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//P/h3[1]");
                    lastError = "Response Too Small. " + response.Length;

                    foreach (HtmlNode node in nodes)
                    {
                        if (node.InnerText.Contains("Aborting"))
                        {
                            lastError = node.InnerText;
                        }
                    }
                    return null;
                }

                if (crawler.StripHTML(response).Contains("CM/ECF Filer or PACER Login"))
                {
                    lastError = "Login Failed";
                    return null;
                }
                // get the total number of rows.
                // this prevents errors when no results are found.
                string jsCookiePattern = "no data for the chosen selection criteria";
                Match match = Regex.Match(response, jsCookiePattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    result = new FindCasesResult();
                    return result;
                }

                // this prevents errors when no results are found.
                jsCookiePattern = "To accept charges shown below, click on the 'Continue' button";
                match = Regex.Match(response, jsCookiePattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    result = new FindCasesResult();
                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table//tr[5]//td//font"))
                    {
                        result.retreived_at = node.InnerText; break;
                    }

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table/tr[7]/td[1]/font"))
                    {
                        result.description = node.InnerText; break;
                    }

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table/tr[7]/td[2]/font"))
                    {
                        result.criteria = node.InnerText; break;
                    }

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table/tr[8]/td[1]/font"))
                    {
                        result.billable_pages = int.Parse(node.InnerText); break;
                    }

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table/tr[8]/td[2]/font"))
                    {
                        result.cost = float.Parse(node.InnerText); break;
                    }

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//form"))
                    {
                        result.url = baseUrl() + node.GetAttributeValue("action", "");
                        //correction alabama north version 5.0
                        result.url = result.url.Replace("..", "");
                    }

                }
                return result;
            }


            private String createPost(CaseSearchOptions options)
            {
                String toPost = "";

                toPost += crawler.multipartField("office", options.office);

                if (options.case_type != null)
                {
                    foreach (CaseSearchOptions.case_types t in options.case_type)
                    {
                        toPost += crawler.multipartField("case_type", t.ToString());
                    }
                }

                if (options.chapter != null)
                {
                    foreach (CaseSearchOptions.chapters t in options.chapter)
                    {
                        toPost += crawler.multipartField("chapter", ((int)t).ToString());
                    }
                }

                toPost += crawler.multipartField("trustee", "All");
                toPost += crawler.multipartField("aty_saved", "");
                toPost += crawler.multipartField("aty_orig", "");
                toPost += crawler.multipartField("filer", "");
                toPost += crawler.multipartField("txtclue", options.txtclue);
                toPost += crawler.multipartField("DateType", options.date_type.ToString());
                toPost += crawler.multipartField("StartDate", options.start.ToString("M/d/yy"));
                toPost += crawler.multipartField("EndDate", options.end.ToString("M/d/yy"));

                if (options.open_cases)
                    toPost += crawler.multipartField("open_cases", "on");

                if (options.closed_cases)
                    toPost += crawler.multipartField("closed_cases", "on");

                if (options.party_information)
                    toPost += crawler.multipartField("party_information", "on");

                if (options.proseonly)
                    toPost += crawler.multipartField("proseonly", "on");

                toPost += crawler.multipartField("EndDate", "");
                toPost += crawler.multipartField("sort1", "case number");
                toPost += crawler.multipartField("sort2", "");
                toPost += crawler.multipartField("sort3", "");
                toPost += crawler.multipartField("data_format", "data only");
                toPost += crawler.multipartEnd();

                return toPost;
            }


            public bool Login()
            {


                crawler.referrer = loginUrl();
                try
                {
                    String toPost = "";
                    toPost += crawler.multipartField("login", user);
                    toPost += crawler.multipartField("key", pass);
                    toPost += crawler.multipartField("clcode", "");
                    toPost += crawler.multipartField("redaction", "on");
                    toPost += crawler.multipartField("button1", "Login");
                    toPost += crawler.multipartEnd();


                    String response = crawler.Post(loginUrl(), toPost, true);

                    if (response.Substring(0, 300).Contains("login attempt failed"))
                        lastError = "Your ECF or PACER login attempt failed.  Either your login name or password is incorrect.";

                    else if (response.Contains("DisplayMenu.pl?Reports"))
                    {
                        return loadJavascriptCookies(response);

                    }

                    else
                        lastError = "An unknown error has occured";
                }
                catch (Exception e)
                {
                    lastError = "Your ECF or PACER login attempt failed.\n" + e.Message;
                }
                return false;
            }

            public bool NextGenLogin()
            {
                crawler.referrer = nexGenLoginUrl();
                try
                {
                    String toPost = "";
                    toPost += crawler.multipartField("login:loginName", user);
                    toPost += crawler.multipartField("login:password", pass);
                    toPost += crawler.multipartField("login:clientCode", "");
                    toPost += crawler.multipartField("redaction", "on");
                    toPost += crawler.multipartField("button1", "Login");
                    toPost += crawler.multipartEnd();


                    String response = crawler.Post(nexGenLoginUrl(), toPost, true);

                    if (response.Substring(0, 300).Contains("login attempt failed"))
                        lastError = "Your ECF or PACER login attempt failed.  Either your login name or password is incorrect.";

                    else if (response.Contains("DisplayMenu.pl?Reports"))
                    {
                        return loadJavascriptCookies(response);

                    }

                    else
                        lastError = "An unknown error has occured";
                }
                catch (Exception e)
                {
                    lastError = "Your ECF or PACER login attempt failed.\n" + e.Message;
                }
                return false;
            }

            // 
            private bool loadJavascriptCookies(String response)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(response);
                // string jsCookiePattern = "document.cookie=\"([a-z]+)=\\\\\"(.+?)?\\\\\";";
                string jsCookiePattern = "document.cookie=\"([a-zA-Z_0-9]+)=([^;]+);?";
                bool found = false;

                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//script");
                foreach (HtmlNode node in nodes)
                {
                    String javascript = node.InnerText;

                    string[] lines = javascript.Split('\n');
                    // iterate line by line.
                    foreach (string line in lines)
                    {
                        Match match = Regex.Match(line, jsCookiePattern, RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            string key = match.Groups[1].ToString();
                            string value = match.Groups[2].ToString();

                            //remove backquotes
                            value = value.Replace("\\", "");
                            value = value.Replace("\"", "");
                            Cookie cookie = new Cookie(key, value, "/", ".uscourts.gov");
                            crawler.cookieContainer.Add(cookie);

                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    lastError = "Could not load cookies";
                    return false;
                }
                return true;

                /* <SCRIPT LANGUAGE="JavaScript">
               3 document.cookie="PacerUser=\"bs250301302916224 ZIbaTkFeA8g\"; path=/; domain=.uscourts.gov; secure;";
               4 if ("PacerPref=\"receipt=Y\"; path=/ ; domain=.uscourts.gov".length > 0) {
               5 document.cookie="PacerPref=\"receipt=Y\"; path=/ ; domain=.uscourts.gov; secure;";
               6 }
               7 if ("PacerClient=\"\"; path=/ ; domain=.uscourts.gov".length > 0) {
               8 document.cookie="PacerClient=\"\"; path=/ ; domain=.uscourts.gov; secure;";
               9 }
               10 if ("ClientDesc=\"\"; path=/ ; domain=.uscourts.gov".length > 0) {
               11 document.cookie="ClientDesc=\"\"; path=/ ; domain=.uscourts.gov; secure;";
               12 }
               13 if ("".length > 0) {
               14 location.assign("");
               15 }
               16 </SCRIPT>*/
            }

            
        }
 
        // Class to assist with page scraping.
        // Class to assist with page scraping.
        public class Crawler
        {

            public Boolean useFiddler = false;

            public CookieContainer cookieContainer = null;

            // the referer for the first request.
            public String referrer = "http://google.com";

            private const String multipartBoundry = "441338019695";

            public Crawler()
            {
                cookieContainer = new CookieContainer();
            }


            private HttpWebRequest getWebRequest(String url)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                req.Headers.Add("Accept-Language", "en-us,en;q=0.5");
                req.Headers.Add("Accept-Encoding", "gzip,deflate");
                req.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");

                req.Headers.Add("Keep-Alive", "115");

                // funky shizzle... http://haacked.com/archive/2004/05/15/http-web-request-expect-100-continue.aspx
                System.Net.ServicePointManager.Expect100Continue = false;

                req.KeepAlive = true;

                req.Referer = referrer; // looks like Microsoft made a spelling error lol 
                referrer = url; // save referrer for next request.
                req.CookieContainer = cookieContainer;


                return req;
            }

            private String getWebResponse(HttpWebRequest req)
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return reader.ReadToEnd();
            }


            /*
            * Byron Whitlock 
            * 12/20/2008
            * Does an HTTP POST to URL using the URL encoded toPost String.
            * if cookie is an empty string, any subsequent posts retain cookies from previous posts.
            * Will automatically use referrer from the previous request. to override, use referrer class variable
            */
            public String Post(String URL, String toPost)
            {
                return Post(URL, toPost, false);
            }

            public String Post(String URL, String toPost, Boolean multipart)
            {
                if (multipart)
                {
                    return PostWithJavascriptEnabled(URL, toPost, "multipart/form-data; boundary=---------------------------" + multipartBoundry);
                }
                else
                {
                    return Post(URL, toPost, "application/x-www-form-urlencoded");
                }
            }

            private String Post(String url, String toPost, String contentType)
            {
                HttpWebRequest req = getWebRequest(url);
                req.Method = "POST";
                req.ContentType = contentType;

                StreamWriter streamWriter = new StreamWriter(req.GetRequestStream());
                streamWriter.Write(toPost);
                streamWriter.Close();

                return getWebResponse(req);
            }

            private String PostWithJavascriptEnabled(String url, String toPost, String contentType)
            {
                HttpWebRequest req = getWebRequest(url);
                req.CookieContainer = new CookieContainer();
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
                req.Method = "POST";
                req.ContentType = contentType;

                StreamWriter streamWriter = new StreamWriter(req.GetRequestStream());
                streamWriter.Write(toPost);
                streamWriter.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                // Print the properties of each cookie.
                foreach (Cookie cook in res.Cookies)
                {
                    Console.WriteLine("Cookie:");
                    Console.WriteLine("{0} = {1}", cook.Name, cook.Value);
                    Console.WriteLine("Domain: {0}", cook.Domain);
                    Console.WriteLine("Path: {0}", cook.Path);
                    Console.WriteLine("Port: {0}", cook.Port);
                    Console.WriteLine("Secure: {0}", cook.Secure);

                    Console.WriteLine("When issued: {0}", cook.TimeStamp);
                    Console.WriteLine("Expires: {0} (expired? {1})",
                        cook.Expires, cook.Expired);
                    Console.WriteLine("Don't save: {0}", cook.Discard);
                    Console.WriteLine("Comment: {0}", cook.Comment);
                    Console.WriteLine("Uri for comments: {0}", cook.CommentUri);
                    Console.WriteLine("Version: RFC {0}", cook.Version == 1 ? "2109" : "2965");

                    // Show the string representation of the cookie.
                    Console.WriteLine("String: {0}", cook.ToString());
                }

                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return reader.ReadToEnd();
            }

            public String Get(String url)
            {
                HttpWebRequest req = getWebRequest(url);
                req.Method = "GET";
                return getWebResponse(req);
            }

            public string multipartField(string name, String value)
            {
                if (value == null)
                {
                    value = "";
                }
                string field = "";

                field += "-----------------------------" + multipartBoundry + "\r\n";
                field += "Content-Disposition: form-data; name=\"" + name + "\"\r\n";
                field += "\r\n";
                field += value + "\r\n";

                return field;
            }

            public string multipartEnd()
            {
                string field = "";
                field += "-----------------------------" + multipartBoundry + "--\r\n";
                return field;
            }


            public String GetFormAction(string url)
            {
                String html = Get(url);
                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(html);
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//form"))
                {
                    return node.GetAttributeValue("action", "");
                }
                return "";
            }



            // FROM:
            // ISerializable - Roy Osherove's Blog
            // http://weblogs.asp.net/rosherove/archive/2003/05/13/6963.aspx
            public string StripHTML(string htmlString)
            {

                //This pattern Matches everything found inside html tags;
                //(.|\n) - > Look for any character or a new line
                // *?  -> 0 or more occurences, and make a non-greedy search meaning
                //That the match will stop at the first available '>' it sees, and not at the last one
                //(if it stopped at the last one we could have overlooked
                //nested HTML tags inside a bigger HTML tag..)
                // Thanks to Oisin and Hugh Brown for helping on this one...
                string pattern = @"<(.|\n)*?>";
                return Regex.Replace(htmlString, pattern, string.Empty);

            }

        }

}

//HOw to use the Bankruptcy Parser
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BankruptcyParser
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string user = "bs2503";
//            string pass = "25mgow!b";
//            // courts can be found at http://www.pacer.gov/cgi-bin/links.pl
//            string court = "ecf.alsb";

//            BankruptcyParser bankruptcy = new BankruptcyParser(court, user, pass);
//           // bankruptcy.useFiddler();


//            // Login to the system
//            Console.WriteLine("Logging in " + user);
//            if (!bankruptcy.Login())
//            {
//                Console.WriteLine("Could Not Login: " + bankruptcy.lastError);
//                return;
//            }                       

//            // setup the search paramters
//            CaseSearchOptions o = new CaseSearchOptions();
//            o.open_cases = true;
//            o.closed_cases = false;
//            o.start = DateTime.Parse("04/15/2011");
//            o.end = DateTime.Parse("04/15/2011");
//            o.chapter = new List<CaseSearchOptions.chapters>();
//            o.chapter.Add(CaseSearchOptions.chapters.eleven);
//            o.chapter.Add(CaseSearchOptions.chapters.thirteen);

//            // get the cost and number of pages
//            Console.WriteLine("Performing search " + user);
//            FindCasesResult result = bankruptcy.FindCases(o);

//            if (result == null)
//            {
//                Console.WriteLine("Could Not Find Cases: " + bankruptcy.lastError);
//                return;
//            }


//            Console.WriteLine("Search Criteria: " + result.criteria);
//            Console.WriteLine("Billable Pages: " + result.billable_pages);
//            Console.WriteLine("Cost: " + result.cost);
//            Console.WriteLine("");            
//            Console.WriteLine("Press Y <ENTER> to retrieve data.");
//            Console.WriteLine("Cost is " + result.cost);

//            string input = Console.ReadLine();
//            if (input.ToUpper() == "Y")
//            {
//                Console.WriteLine("Downloading data");
//                String text = bankruptcy.PurchaseCases(result);
//                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


//                Console.WriteLine("------------- CSV DATA -----------------");         
//                Console.Write(text);                
//                Console.WriteLine("----------------------------------------");         

//                Console.WriteLine("Press any key to continue");
//                Console.ReadLine();
//            }            
//        }
//    }
//}
