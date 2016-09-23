using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BRGCaseMailServer.SalesForceWebReference;
using System.Web.Services.Protocols;
using System.Reflection;

namespace BRGCaseMailServer
{
    public class SalesForceQueries
    {
        #region Account Queries

        static string _accountQuery = "select Id, Name, BRGCaseMail_User_ID__c, BRGCaseMail_Password__c, BRGCaseMail_Active__c, BRGCaseMailAdmin__c, Dealership_Email__c, X2nd_Email__c,  Dealership_Text_Address__c, X2nd_Dealership_Text_Address__c, Active__c,  GM__c, Contact_1__c, Contact_2__c, Contact_3__c, Contact_4__c, Phone, Primary_Contact_Phone__c, Primary_Contact__c from Account";
        public static List<SalesForceWebReference.Account> getAccounts(SforceService _binding)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_accountQuery + " order by Name asc");

            List<Account> _accounts = new List<Account>();

            for (int i = 0; i < qr.records.Length; i++)
            {
                _accounts.Add(((Account)qr.records[i]));

                //Debug.WriteLine(((Account)qr.records[i]).Name);
            }

            return _accounts;
        }
        public static List<SalesForceWebReference.Account> getAccounts(SforceService _binding, string Id, string DealerName, bool ActiveOnly)
        {
            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");
            string query = _accountQuery;

            if (DealerName.Length > 0 || Id.Length > 0 || ActiveOnly == true)
            {
                query += " Where ";

                if (DealerName.Length > 0)
                {
                    query += " Name like '%" + DealerName + "%' ";

                    if (Id.Length > 0)
                    {
                        query += " AND Id = '" + Id + "' ";
                    }
                    if (ActiveOnly)
                    {
                        query += " AND BRGCaseMail_Active__c = true";
                    }
                }
                else if (Id.Length > 0)
                {
                    query += " Id = '" + Id + "' ";

                    if (ActiveOnly)
                    {
                        query += " AND BRGCaseMail_Active__c = true";
                    }
                }
                else if (ActiveOnly)
                {
                    query += " BRGCaseMail_Active__c = true ";
                }

            }

            query += " order by Name asc";

            qr = _binding.query(query);

            List<Account> _accounts = new List<Account>();

            if (qr.records != null)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _accounts.Add(((Account)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }
            return _accounts;
        }
        public static List<SalesForceWebReference.Account> getBRGCaseMailActiveAccounts(SforceService _binding)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_accountQuery + " where BRGCaseMail_Active__c = true order by Name asc");

            List<Account> _accounts = new List<Account>();

            for (int i = 0; i < qr.records.Length; i++)
            {
                _accounts.Add(((Account)qr.records[i]));

                //Debug.WriteLine(((Account)qr.records[i]).Name);
            }

            return _accounts;
        }
        public static SalesForceWebReference.Account getBRGCaseMailAccount(SforceService _binding, string UserID, string Password)
        {
            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_accountQuery + " where BRGCaseMail_Password__c = '" + Password + "' and BRGCaseMail_User_ID__c = '" + UserID + "'");

            //List<Account> _accounts = new List<Account>();

            if (qr.records != null)
            {
                if (qr.records.Length > 0)
                {
                    return ((Account)qr.records[0]);
                }
                else
                {
                    return null;
                }
            }
            else
                return null;

        }
        public static SalesForceWebReference.Account getBRGCaseMailAccount(SforceService _binding, string Id)
        {
            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_accountQuery + " where Id = '" + Id + "'");

            //List<Account> _accounts = new List<Account>();

            if (qr.records.Length > 0)
            {
                return ((Account)qr.records[0]);
            }

            else
                return null;
        }
        public static void ChangePassword(SforceService _binding, string Id, string NewPassword)
        {

            Account _account = new Account();

            _account.Id = Id;
            _account.BRGCaseMail_Password__c = NewPassword;

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { _account });
        }
        #endregion

        #region Application Queries

        static string _appQuery = "Select Access_Auth_Code__c, Account_City__c, Account_Fax__c, Account_Phone__c, Account_Primary_Contact__c, Account_State__c, Account_Street_1__c, Account_Street_2__c, Account_Zip_Code__c, Account__c, Active_Dealer__c, Active__c, Additional_Comments__c, Address_Apt_No__c, Amount_Owed__c, App_Taken_By__c, App__c,  Application_Type__c, Appointment_Date__c, Appointment_Time__c, BDC_Nation_Internal_Comments__c, BRG_Created_Date__c, BRG_Credit_App_ID__c, BRG_Dealership_Contacts__c, Bankruptcy_Discharge_Date__c, Certificate_Number__c, City__c, CoApp_Date_Of_Birth__c, CoApp_Employer__c, CoApp_First_Name__c, CoApp_Gross_Monthly_Income__c, CoApp_Hourly_or_Salary__c, CoApp_Last_Name__c, CoApp_Middle_Initial__c, CoApp_Position__c, CoApp_Prev_Employer__c, CoApp_Prev_Position__c, CoApp_Prev_Time_at_Job_Mo__c, CoApp_SSN__c, CoApp_SS__c, CoApp_Time_at_Job_Months__c, CoApp_Work_Phone__c, CreatedById, CreatedDate, Credit_App_SPECIAL_HANDLING__c, Date_Of_Birth__c, Dealer_Lead_Number__c, Dealer_Name_Formula__c, BRGCaseMail_DateTime_Last_Emailed__c, BRGCaseMail_DateTime_Last_Text__c,  BRGCaseMail_Gross_Profit__c, BRGCaseMail_Last_Viewed__c, BRGCaseMail_Lead_Status__c, BRGCaseMail_Number_Emails_Sent__c, BRGCaseMail_Number_Texts_Sent__c,  BRGCaseMail_OK_to_send__c, Dealership_Email__c, BRGCaseMail_Resend_via_Email_Text__c, Do_Not_Call__c, Email_Opt_Out__c, Email__c, Employer__c, First_Name__c, Follow_Up_Date__c, Gross_Monthly_Income__c, Home_Phone__c, Hourly_or_Salary__c, How_did_you_hear_about_us__c, IsDeleted, Is_your_mortgage_current__c, LastActivityDate, LastModifiedById, LastModifiedDate, Last_Name__c, Lead_Generated_By__c, Legacy_Created_Date__c, Live_Call_Transfer__c, Make__c, May_We_Pull_Credit__c, Middle_Initial__c, Mobile_Phone__c, Model__c, Monthly_Amount_1__c, Monthly_Amount_2__c, Months_at_Prev_Resdn__c, Mother_s_Maiden_Name__c, Name, New_Appointment_Date__c, New_Follow_Up_Date__c, Other_Income_1__c, Other_Income_2__c, OwnerId, Position__c, Prev_Address_Apt_No__c, Prev_Apt_Unit__c, Prev_City__c, Prev_Employer__c, Prev_Position__c, Prev_State__c, Prev_Street_Address__c, Prev_Time_at_Job_Months__c, Prev_Zip_Code__c, RecordTypeId, Rent_Mortg_Amount__c, Rent_or_Own__c, Res_Length_Months__c, SSN__c, Social_Security_Number__c, Sold_App__c, Sold_Date__c, Source_of_Lead__c, Special_Handling_1__c, Special_Handling_2__c, Stage__c, State__c, Street_Address__c, Time_at_Job_Months__c, Trade_In__c, Unit_Apt__c, Work_Phone__c, Year__c, Zip_Code__c, Id  from Dealer_Lead__c";

        public static void UpdateLastEmailedDate(SforceService _binding, string LeadID)
        {
            Dealer_Lead__c application = new Dealer_Lead__c();

            application.Id = LeadID;
            application.BRGCaseMail_DateTime_Last_Emailed__c = DateTime.Now;
            application.BRGCaseMail_DateTime_Last_Emailed__cSpecified = true;
            if (application.BRGCaseMail_Number_Emails_Sent__c != null)
            {
                application.BRGCaseMail_Number_Emails_Sent__c = application.BRGCaseMail_Number_Emails_Sent__c + 1;
            }
            else
            {
                application.BRGCaseMail_Number_Emails_Sent__c = 1;
            }

            application.BRGCaseMail_Number_Emails_Sent__cSpecified = true;

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { application });
        }
        public static void UpdateLastTextDate(SforceService _binding, string LeadID)
        {

            Dealer_Lead__c application = new Dealer_Lead__c();

            application.Id = LeadID;
            application.BRGCaseMail_DateTime_Last_Text__c = DateTime.Now;
            application.BRGCaseMail_DateTime_Last_Text__cSpecified = true;
            application.BRGCaseMail_Number_Texts_Sent__c = application.BRGCaseMail_Number_Texts_Sent__c + 1;
            application.BRGCaseMail_Number_Texts_Sent__cSpecified = true;

            if (application.BRGCaseMail_Number_Texts_Sent__c != null)
            {
                application.BRGCaseMail_Number_Texts_Sent__c += 1;
            }
            else
            {
                application.BRGCaseMail_Number_Texts_Sent__c = 1;
            }

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { application });
        }

        public static void UpdateApplicationStatusAndGrossProfit(SforceService _binding, double _grossProfit, string Status, string LeadID)
        {

            Dealer_Lead__c application = new Dealer_Lead__c();

            application.Id = LeadID;

            if (_grossProfit > 0)
            {
                application.BRGCaseMail_Gross_Profit__c = _grossProfit;
                application.BRGCaseMail_Gross_Profit__cSpecified = true;
            }

            application.BRGCaseMail_Lead_Status__c = Status;

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { application });
        }

        public static void UpdateLastViewedAndStatus(SforceService _binding, string Status, string LeadID)
        {

            Dealer_Lead__c application = new Dealer_Lead__c();

            application.Id = LeadID;
            application.BRGCaseMail_Lead_Status__c = Status;
            application.BRGCaseMail_Last_Viewed__c = DateTime.Now;
            application.BRGCaseMail_Last_Viewed__cSpecified = true;

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { application });
        }

        public static void UpdateStatusToFresh(SforceService _binding, string LeadID)
        {

            Dealer_Lead__c application = new Dealer_Lead__c();

            application.Id = LeadID;
            application.BRGCaseMail_Lead_Status__c = "FRESH";
            application.BRGCaseMail_Resend_via_Email_Text__c = false;
            application.BRGCaseMail_Resend_via_Email_Text__cSpecified = true;

            //call update passing an array of object  

            // assuming that you've already established an enterprise WSDL binding  

            SaveResult[] saveResults = _binding.update(
                new sObject[] { application });
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getApplications(SforceService _binding, string AccountId)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_appQuery + " where Account__c = '" + AccountId + "' and  BRGCaseMail_OK_to_send__c = true  order by CreatedDate desc");

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null && qr.records.Length > 0)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }
            return _applications;
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getApplications(SforceService _binding, string AccountId, DateTime SinceDate)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            string _query2 = _appQuery + " where Account__c = '" + AccountId + "' and CreatedDate > " + SinceDate.ToString("yyyy-MM-dd") + "T01:01:01Z and BRGCaseMail_OK_to_send__c = true order by CreatedDate desc";

            qr = _binding.query(_query2);

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }

            return _applications;
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getApplications(SforceService _binding, string AccountId, string LastName)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            string _query2 = _appQuery + " where Account__c = '" + AccountId + "' and Name like '%" + LastName + "%' and BRGCaseMail_OK_to_send__c = true  order by CreatedDate desc";

            qr = _binding.query(_query2);

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }

            return _applications;
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getApplications(SforceService _binding, string AccountId, string StartDate, string EndDate, string StatusOptions)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            string _query2 = _appQuery + " where Account__c = '" + AccountId + "' and BRGCaseMail_OK_to_send__c = true ";


            if (StartDate.Length > 0)
            {
                _query2 += " and CreatedDate > " + StartDate + "T01:01:01Z";
            }

            if (EndDate.Length > 0)
            {
                _query2 += " and CreatedDate < " + DateTime.Parse(EndDate).AddDays(1).ToString("yyyy-MM-dd") + "T01:01:01Z";
            }


            if (StatusOptions.Length > 0)
            {
                string[] StatusOptionsArray = StatusOptions.Split(';');

                _query2 += " and ( ";


                int i = 0;

                foreach (string s in StatusOptionsArray)
                {
                    i++;
                    if (s.Length > 0)
                    {
                        if (i > 1)
                        {
                            _query2 += " and ";
                        }
                        _query2 += " BRGCaseMail_Lead_Status__c = '" + s.TrimStart(' ') + "'";
                    }
                }

                _query2 += " ) ";

                _query2 += " order by CreatedDate desc ";

            }


            qr = _binding.query(_query2);

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }

            return _applications;
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getApplications(SforceService _binding)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            //object _dealerLead = new SalesForceWebReference.Dealer_Lead__c();

            //Type _type = _dealerLead.GetType();

            //PropertyInfo[] props = _type.GetProperties();

            //string _query = "Select ";

            //foreach (PropertyInfo p in props)
            //{
            //    if (p.Name != "fieldsToNull" && p.Name)
            //    {
            //        _query += p.Name + ", ";
            //    }
            //}           

            qr = _binding.query(_appQuery + " where BRGCaseMail_OK_to_send__c = true ");

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null && qr.records.Length > 0)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }
            return _applications;
        }

        public static List<SalesForceWebReference.Dealer_Lead__c> getUnsentApplications(SforceService _binding, string AccountId)
        {

            QueryResult qr = null;

            //SalesForceWebReference.Account _account;

            ////qr = _binding.query("select Id, Account__c, Name, FirstName__c, LastName__c, CreatedDate from  from Dealer_Lead_c");

            qr = _binding.query(_appQuery + " where Account__c = '" + AccountId + "' and (( BRGCaseMail_Lead_Status__c = 'UNSENT' or BRGCaseMail_Resend_via_Email_Text__c = true ) and BRGCaseMail_OK_to_send__c = true )");

            List<Dealer_Lead__c> _applications = new List<Dealer_Lead__c>();

            if (qr.records != null && qr.records.Length > 0)
            {
                for (int i = 0; i < qr.records.Length; i++)
                {
                    _applications.Add(((Dealer_Lead__c)qr.records[i]));

                    //Debug.WriteLine(((Account)qr.records[i]).Name);
                }
            }

            return _applications;
        }

        public static SalesForceWebReference.Dealer_Lead__c getApplication(SforceService _binding, string ApplicationID)
        {

            QueryResult qr = null;

            qr = _binding.query(_appQuery + " Where Id = '" + ApplicationID + "'");

            if (qr.records != null && qr.records.Length > 0)
            {
                return ((Dealer_Lead__c)qr.records[0]);
            }
            else
                return null;
        }

        #endregion

    }
}
