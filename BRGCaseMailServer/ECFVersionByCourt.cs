using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace BRGCaseMailServer
{
    public class ECFVersion
    {
        public string courtType;     // U.S. Bankruptcy Courts
        public string courtName;     // Georgia Southern - ECF 
        public string version;       // 3.4.0
        public string versionString; // CM/ECF-BK V3.4.0
    }

    // this class finds the ACER or Case Management/Electronic Case Files (CM/ECF) software version by court
    public class ECFVersionByCourt
    {
        private string url = "http://www.pacer.gov/psco/cgi-bin/links.pl";
        private Crawler crawler = new Crawler();

        /// <summary>
        /// RETURNS BANKRUPTCY COURTS versions ONLY.
        /// </summary>
        /// <returns></returns>
        public List<ECFVersion> GetBankruptcyCourtVersions()
        {
            List<ECFVersion> versions = new List<ECFVersion>();
            foreach (ECFVersion version in GetCourtVersions())
            {
                if (Regex.Match(version.courtType, "Bankruptcy",RegexOptions.IgnoreCase).Success)
                {
                    versions.Add(version);
                }
            }
            return versions;
        }

        public List<ECFVersion> GetCourtVersions()
        {
            List<ECFVersion> versions = new List<ECFVersion>();

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(crawler.Get(url));

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@class='dataTable']");
            string courtType = "Unknown";

            foreach (HtmlNode node in nodes)
            {
                string[] sections = Regex.Split(node.InnerHtml, "(<br>[ \t\r\n]*)+");
                if (sections != null)
                {
                    foreach (string section in sections)
                    {
                        HtmlDocument doc2 = new HtmlDocument();
                        doc2.LoadHtml(section);

                        HtmlNodeCollection courtTypeNode = doc2.DocumentNode.SelectNodes("//h4");
                        if (courtTypeNode != null && courtTypeNode.Count > 0)
                        {
                            courtType = courtTypeNode[0].InnerText;
                        }

                        HtmlNodeCollection nameNode = doc2.DocumentNode.SelectNodes("//a[@class='jtip']");
                        HtmlNodeCollection versionNode = doc2.DocumentNode.SelectNodes("//img[@class='jtip']");

                        if (versionNode != null && versionNode.Count > 0 &&
                            nameNode != null && nameNode.Count > 0)
                        {
                            ECFVersion version = new ECFVersion();

                            version.courtType = courtType;
                            version.courtName = nameNode[0].InnerText;
                            version.versionString = versionNode[0].Attributes["alt"].Value.ToString();
                            version.version = Regex.Replace(version.versionString, "[^0-9.]", string.Empty);
                            versions.Add(version);
                        }

                    }
                }

            }
            return versions;
        }

        public ECFVersion GetBankruptcyCourtVersion(string CourtName)
        {
            ECFVersion _version = new ECFVersion();

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(crawler.Get(url));

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@class='dataTable']");
            string courtType = "Unknown";

            foreach (HtmlNode node in nodes)
            {
                string[] sections = Regex.Split(node.InnerHtml, "(<br>[ \t\r\n]*)+");
                if (sections != null)
                {
                    foreach (string section in sections)
                    {
                        HtmlDocument doc2 = new HtmlDocument();
                        doc2.LoadHtml(section);

                        HtmlNodeCollection courtTypeNode = doc2.DocumentNode.SelectNodes("//h4");
                        if (courtTypeNode != null && courtTypeNode.Count > 0)
                        {
                            courtType = courtTypeNode[0].InnerText;
                        }

                        HtmlNodeCollection nameNode = doc2.DocumentNode.SelectNodes("//a[@class='jtip']");
                        HtmlNodeCollection versionNode = doc2.DocumentNode.SelectNodes("//img[@class='jtip']");

                        if (versionNode != null && versionNode.Count > 0 &&
                            nameNode != null && nameNode.Count > 0 )
                        {

                            _version.courtType = courtType;
                            _version.courtName = nameNode[0].InnerText;
                            _version.versionString = versionNode[0].Attributes["alt"].Value.ToString();
                            _version.version = Regex.Replace(_version.versionString, "[^0-9.]", string.Empty);

                            if (  Regex.Match(_version.courtType, "Bankruptcy",RegexOptions.IgnoreCase).Success)
                            {
                                if (CourtName.Length < _version.courtName.Length &&  _version.courtName.Substring(0, CourtName.Length) == CourtName)
                                    return _version;
                            }
                        }
                    }
                }

            }

            return null;
        }

    }
}
