using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Reporting;
using System.IO;

namespace BKLeadsOnline.Domain
{
    public class ASPOSEUtils
    {

        Dealer _dealer;
        string _tempplateName;
        string mDocPath;

        public ASPOSEUtils(string binFolder, Dealer dealer, string templateName)
        {
            _tempplateName = templateName;
            _dealer = dealer;

            //this.MapPath("bin") + Path.DirectorySeparatorChar

            string basePath = new Uri(new Uri(binFolder), "../../../Common").LocalPath;
            mDocPath = Path.Combine(basePath, "Templates");
        }

        public Document Execute()
        {

            //Open the template document
            Document doc = new Document(System.IO.Path.Combine(mDocPath, _tempplateName));

            //get the dealerlogo
            DealerService.GetLogo(_dealer);

            // Set up the event handler for image fields.
            //doc.MailMerge.FieldMergingCallback = new HandleMergeImageFieldFromBlob(_dealer);

            //Example of a very simple mail merge to populate fields only once from an array of values.
            //doc.MailMerge.Execute(
            //    new string[] { "MyName", "MyTitle", "MyAddress1", "MyAddress2" },
            //    new object[] { "James Bond", "Secret Agent", "MI5 Headquarters", "Milbank, London" });

            //Mail merge BankruptcyCase details from DataTable to the document.
            //Whole document content will be repeated for each record.
            List<BankruptcyCase> _leads = BankruptcyCaseService.GetTop1000Rows();
            BankruptcyCaseMailMergeDataSource _source = new BankruptcyCaseMailMergeDataSource(_leads);

            doc.MailMerge.Execute(_source);

            return doc;
        }


    }
}
