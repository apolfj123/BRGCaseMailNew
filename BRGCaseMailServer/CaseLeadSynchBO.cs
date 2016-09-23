using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using BKLeadsOnline;

namespace BRGCaseMailServer
{
    public class CaseLeadSynchBO
    {
        int _numErrors;
        EventLog _eventLog;
        bool TraceOn;

        #region  attributes       
        public int  NumErrors
		{
			get
			{
				return _numErrors;
			}
		}
        #endregion

        /// <summary>
        /// constructor - sets the SFForce Binding and event log
        /// </summary>
        
        public CaseLeadSynchBO()
        {
            _numErrors = 0;
            _eventLog = new EventLog("Application");
            _eventLog.Source = "BKLeadsService";
            //create te salesforce binding
            TraceOn = ConfigurationManager.AppSettings["Trace"] == "true" ? true : false;  
        }
 
        #region public methods
        /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
        public int CheckLeads()
        {
            //string mimeType;
            //string extension;
            //Encoding encoding;
            MailMessage msg;
            bool blnSent = false;

            try
            {
                if (TraceOn)
                {
                    _eventLog.WriteEntry("Entering CheckLeads()");
                }

                //get all cases that are less than two years old that have a blank uploaded date
                List<BRGCaseMailServer.BankruptcyCase> _casesToUpload = BRGCaseMailServer.BankruptcyCaseService.GetUploadableCases();

                if (TraceOn)
                {
                    _eventLog.WriteEntry("Number Of Cases to upload" + _casesToUpload.Count);
                }                

                int CaseInserted = 0;
                int CasesUpdated = 0;
                int TotalCases = 0;

                #region for each case/lead
                
                foreach (BRGCaseMailServer.BankruptcyCase _bRGcase in _casesToUpload)
                {
                    try
                    {
                        BKLeadsOnline.Domain.BankruptcyCase _bKCase = null;
                        
                        //does the case already exist on the online DB - this could happen if the local case was modifiedb by the import so the upload date was reset?
                        //BKLeadsOnline.Domain.BankruptcyCase _bKCase = BKLeadsOnline.Domain.BankruptcyCaseService.GetByID(_bRGcase.ID);
                        //don;t rely jsut on the ID as imports could be processed multiple times adding new duplciate cases.
                        try
                        {
                            _bKCase = BKLeadsOnline.Domain.BankruptcyCaseService.GetByCourtAnd4DigitCaseNum(_bRGcase.CourtID, _bRGcase.CaseNumber4Digit, _bRGcase.FirstName);
                        }
                        catch { }

                        if (_bKCase == null)
                        {
                            //try and get by the CMECF Internal - sometimes the 4 digit case number is not present
                            try
                            {
                                _bKCase = BKLeadsOnline.Domain.BankruptcyCaseService.GetByCourtAndCMECF_Internal(_bRGcase.CourtID, _bRGcase.CMECF_Internal, _bRGcase.FirstName);
                            }
                            catch { }
                        }

                        //ID's will always match if the sych is working correctly as the ID is not generated on the online database
                        _bKCase = BKLeadsOnline.Domain.BankruptcyCaseService.GetByID(_bRGcase.ID);
 
                       if (_bKCase == null)
                       {
                           //new case
                           _bKCase = new BKLeadsOnline.Domain.BankruptcyCase();
                            //copy any contents sometimes we get the same record twice from the court once on filed, once on discharged.
                            _bRGcase.CopyToBKCase(_bKCase);
                           _bKCase.UploadedDate = DateTime.Now;
                            BKLeadsOnline.Domain.BankruptcyCaseService.Insert(_bKCase);
                            CaseInserted++;
                            TotalCases++;
                            if (TraceOn)
                            {
                                _eventLog.WriteEntry(CaseInserted + " inserted cases  of: " + TotalCases + " cases processed of " + _casesToUpload.Count + " to upload.  Case #:" + _bKCase.CaseNumber4Digit );
                            }                
                       }
                       else
                       {
                            //copy any contents that may have changed - sometimes we get the same record twice from the court once on filed, once on discharged.
                           _bRGcase.CopyToBKCase(_bKCase);
                           _bKCase.UploadedDate = DateTime.Now;
                           CasesUpdated++;
                           TotalCases++;
                           BKLeadsOnline.Domain.BankruptcyCaseService.Update(_bKCase);
                           if (TraceOn)
                           {
                               _eventLog.WriteEntry(CasesUpdated + " updated cases  of: " + TotalCases + " cases processed of " + _casesToUpload.Count + " to upload.  Case #:" + _bKCase.CaseNumber4Digit);
                           }
                       }

                       //change the upload date on the BRGCaseMail case
                       _bRGcase.BKLeadsOnlineUploadedDate = DateTime.Now;
                       BRGCaseMailServer.BankruptcyCaseService.Save(_bRGcase, false);

                    }
                    //an email exception on one account does nto make the whole loop fail.
                    catch (Exception objE)
                    {
                        try
                        {
                            LogAndEmailException(objE);
                        }
                        catch { }
                    }
                }

                if (TraceOn)
                {
                    _eventLog.WriteEntry("Number Of Cases uploaded" + _casesToUpload.Count);
                }                
                #endregion


                return _numErrors;
            }
            catch (Exception objE1)
            {
                LogAndEmailException(objE1);
                return _numErrors;
            }
        }
        
        #endregion

        #region private methods        
        
        private void LogAndEmailException(Exception objE)
        {
            try
            {
                _eventLog.WriteEntry("BK Lead Service Error Occurred:" + objE.Message + "  Number Errors:" + NumErrors.ToString());
                _eventLog.WriteEntry("BK Lead Service Error Occurred Inner Exception:" + objE.InnerException.Message + "  Number Errors:" + NumErrors.ToString());
                //_eventLog.WriteEntry("timer1_Tick() Exception");
                //catch the error but keep the service processing
                //ExceptionManager.Publish(objE);

                string emailSubject = "BK Lead Service Error Occurred! Error Count:" + NumErrors.ToString();

                string emailRecipient = ConfigurationManager.AppSettings["AdministratorEmail"];

                var sb = new StringBuilder();

                var message = objE.Message;

                while (objE != null)
                {
                    sb.AppendFormat("Message: {0}\n", message);
                    sb.AppendFormat("Source: {0}\n", objE.Source);
                    sb.AppendFormat("TargetSite: {0}\n", objE.TargetSite);
                    sb.AppendFormat("StackTrace: {0}", objE.StackTrace);
                    sb.AppendFormat("\n\n");

                    objE = objE.InnerException;
                }

                if (ConfigurationManager.AppSettings["SMTPServer"] != null && ConfigurationManager.AppSettings["SMTPServer"].Length > 0)
                {
                    var m = new MailMessage(ConfigurationManager.AppSettings["SMTPFromAddress"], emailRecipient, emailSubject, sb.ToString());

                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"], Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                    if (ConfigurationManager.AppSettings["SMTPUserID"] != null && ConfigurationManager.AppSettings["SMTPUserID"].Length > 0)
                    {
                        smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserID"], ConfigurationManager.AppSettings["SMTPPassword"]);
                    }
                    try
                    {
                        smtp.Send(m);
                    }
                    catch (SmtpException ex2)
                    {
                        _eventLog.WriteEntry("BK Lead Service Error Trying to Email the error:" + objE.Message + " with the error:" + ex2.Message + " Number Errors:" + NumErrors.ToString());
                    }
                }

                _numErrors++;
            }
            catch (Exception ex)
            {
                _eventLog.WriteEntry("BK Lead Service Error Trying to Email the error:" + objE.Message + " with the error:" + ex.Message + " Number Errors:" + NumErrors.ToString());
            }
        }
        
        #endregion

    }
}
