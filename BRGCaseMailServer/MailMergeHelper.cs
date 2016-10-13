using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BRGCaseMailServer;
using System.IO;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using System.Diagnostics;

namespace BRGCaseMailServer
{
    public class MailMergeHelper
    {        
        private Dealer _dealer;
        private DealerMailingList _dealerMailingList;

        public MailMergeHelper(Dealer dealer, DealerMailingList dealerMailingList)
        {
            _dealer = dealer;
            _dealerMailingList = dealerMailingList;
        }

        public void ProcessWordMailMerge(bool SaveMailingList)
        {
            //OBJECT OF MISSING "NULL VALUE"
            Object oMissing = System.Reflection.Missing.Value;

            //OBJECTS OF FALSE AND TRUE
            Object oTrue = true;
            Object oFalse = false;

            //CREATING OBJECTS OF WORD AND DOCUMENT
            Microsoft.Office.Interop.Word.Application oWordApp = new Microsoft.Office.Interop.Word.Application();

            //SETTING THE VISIBILITY TO TRUE
            oWordApp.Visible = true;

            //THE LOCATION OF THE TEMPLATE FILE ON THE MACHINE
            Object oTemplatePath = _dealerMailingList.TemplateFilePath;

            //ADDING A NEW DOCUMENT FROM A TEMPLATE
            Microsoft.Office.Interop.Word.Document oWordDoc = oWordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
            Microsoft.Office.Interop.Word.MailMerge oWordMailMerge = oWordDoc.MailMerge;
            oWordDoc.MailMerge.OpenDataSource(_dealerMailingList.CsvFilePath);

            oWordDoc.MailMerge.Execute();

            oWordMailMerge.Application.ActiveDocument.SaveAs(ConfigurationManager.AppSettings.Get("MailMergeDocFilePath") 
                + _dealer.CompanyName.Replace(" ", "_") 
                + "_" 
                + _dealerMailingList.StartFilterDate.Month 
                + _dealerMailingList.StartFilterDate.Day 
                + _dealerMailingList.StartFilterDate.Year 
                + "_" 
                + _dealerMailingList.EndFilterDate.Month 
                + _dealerMailingList.EndFilterDate.Day 
                + _dealerMailingList.EndFilterDate.Year 
                + "_" 
                + _dealerMailingList.ID.ToString() 
                + "_" + _dealerMailingList.TemplateFileName.Replace(" ", "_").Substring(0, _dealerMailingList.TemplateFileName.Replace(" ", "_").IndexOf(".")) 
                + ".docx");

            _dealerMailingList.MailMergeFilePath = ConfigurationManager.AppSettings.Get("MailMergeDocFilePath") + _dealer.CompanyName.Replace(" ", "_") + "_" + _dealerMailingList.StartFilterDate.Month + _dealerMailingList.StartFilterDate.Day + _dealerMailingList.StartFilterDate.Year + "_" + _dealerMailingList.EndFilterDate.Month + _dealerMailingList.EndFilterDate.Day + _dealerMailingList.EndFilterDate.Year + "_" + _dealerMailingList.ID.ToString() + "_" + _dealerMailingList.TemplateFileName.Replace(" ", "_").Substring(0, _dealerMailingList.TemplateFileName.Replace(" ", "_").IndexOf(".")) + ".docx";

            if (SaveMailingList)
            {
                DealerMailingListService.Save(_dealerMailingList);
            }
            oWordApp.Quit(oFalse, oMissing, oMissing);
        }


        public void ProcessMailingList(bool processMailMerge)
        {
            List<BankruptcyCase> _mailingCases = null;

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();

            try
            {
                // check the ID of the mailing list.  If it is > 0 we know we are reprocessing a list so reload he old list minus the inactive people
                if (_dealerMailingList.ID > 0)
                {
                    //get the old list for this mailing
                    _mailingCases = BankruptcyCaseService.getMailingList(_dealerMailingList.ID);
                    BankruptcyCaseService.UpdateLastSoldDateForMailingList(_dealerMailingList.ID, db, trans, false);

                    //update the count as it could be less because of "do not send"'s and returned mail
                    _dealerMailingList.NumberCases = _mailingCases.Count();
                    _dealerMailingList.CreationDate = DateTime.Now;
                    DealerMailingListService.Save(_dealerMailingList, db, trans, false);
                }
                else
                {
                    //get a new list for this mailing
                    DealerMailingListService.Save(_dealerMailingList, db, trans, false); 
                
                    //get the mailing list and update the last sold date
                    _mailingCases = BankruptcyCaseService.getMailingList(_dealerMailingList, db, trans, false);
                }
            
                string filePath = ConfigurationManager.AppSettings.Get("CSVFilePath") + _dealer.CompanyName.Replace(" ", "_") + "_" + _dealerMailingList.StartFilterDate.Month + _dealerMailingList.StartFilterDate.Day + _dealerMailingList.StartFilterDate.Year + "_" + _dealerMailingList.EndFilterDate.Month + _dealerMailingList.EndFilterDate.Day + _dealerMailingList.EndFilterDate.Year + "_" + _dealerMailingList.ID.ToString() + ".csv";
                string delimiter = ",";
                string[] _columnHeadings = new string[] { "CaseNumber", "Case Number - 4 Digit", "FiledDate", "DischargedDate", "FirstName", "MiddleName", "LastName", "Suffix", "Address1", "Address2", "City", "State", "PostalCode", "Callsource" };
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(delimiter, _columnHeadings));

                //put in the trackng letter:

                string[] _trackingLetter = new string[] { "000000", "000000", "","", "Bankruptcy Resource Group", "", "", "", "10830 SW Tualatin Sherwood RD", "", "Tualatin", "OR", "97062", _dealer.CompanyName + ": " + DateTime.Now.ToShortDateString() + ";  ID: " + _dealerMailingList.ID };
                sb.AppendLine(string.Join(delimiter, _trackingLetter));

                foreach (BankruptcyCase _case in _mailingCases)
                {
                    //do this in a stored procedure in the "getMailingList" for speed.
                    //    _case.LastSoldDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    //    _case.SoldCount += 1;
                    //    BankruptcyCaseService.Save(_case, db, trans, false) ;
                    try
                    {
                        sb.AppendLine(string.Join(delimiter, _case.ToMailingStringArray(_dealer.CallSource)));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                    //do this in a stored procedure in the "getMailingList" for speed.
                    //    DealerMailingListService.AddDealerMailingListCase(_dealerMailingList.ID, _case.ID, db, trans, false);
                }

                File.WriteAllText(filePath, sb.ToString());

                //MailMergeStagingService.InsertList(_mailingCases, _dealer.CallSource, db, trans, false);

                _dealerMailingList.CsvFilePath = filePath;

                //create Mail merge
                if (processMailMerge)
                {
                    ProcessWordMailMerge(false);
                }

                DealerMailingListService.Save(_dealerMailingList, db, trans, false);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

    }
}
