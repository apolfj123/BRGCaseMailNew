using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the BankruptcyCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/25/2011 10:59:01 PM
	/// </summary>
    public class BankruptcyCaseService
    {
        static private string _selectViewSQLRestricted = "Select TOP 1000 ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, " + 
            "Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, " + 
            "AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, " + 
            "DontSend, Returned, SoldCount, LastSoldDate, BKLeadsOnlineUploadedDate, CourtName " + 
            "from v_BankruptcyCase";

        static private string _selectViewSQL = "Select ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, " 
            + "FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, " 
            + "AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, DontSend, Returned, " 
            + "SoldCount, LastSoldDate, BKLeadsOnlineUploadedDate, CourtName " 
            + "from v_BankruptcyCase";

        static private void LoadBankruptcyCase(BankruptcyCase objBankruptcyCase, IDataReader _reader)
        {
            try
            {
                if (_reader.IsDBNull(0) != true)
                {
                    objBankruptcyCase.ID = _reader.GetInt32(0);
                }
                if (_reader.IsDBNull(1) != true)
                {
                    objBankruptcyCase.CourtID = _reader.GetInt32(1);
                }
                if (_reader.IsDBNull(2) != true)
                {
                    objBankruptcyCase.CMECF_Internal = _reader.GetInt32(2);
                }
                if (_reader.IsDBNull(3) != true)
                {
                    objBankruptcyCase.CaseNumber4Digit = _reader.GetString(3);
                    if (objBankruptcyCase.CaseNumber4Digit == "2008-01516-BGC13")
                    {
                        Debug.WriteLine("2008-01516-BGC13");
                    }
                }
                if (_reader.IsDBNull(4) != true)
                {
                    objBankruptcyCase.PacerImportTransactionID = _reader.GetInt32(4);
                }
                if (_reader.IsDBNull(5) != true)
                {
                    objBankruptcyCase.Chapter = _reader.GetInt16(5);
                }
                if (_reader.IsDBNull(6) != true)
                {
                    objBankruptcyCase.FiledOnly = _reader.GetBoolean(6);
                }
                if (_reader.IsDBNull(7) != true)
                {
                    objBankruptcyCase.FiledDate = _reader.GetDateTime(7);
                }
                if (_reader.IsDBNull(8) != true)
                {
                    objBankruptcyCase.DischargeDate = _reader.GetDateTime(8);
                }
                if (_reader.IsDBNull(9) != true)
                {
                    objBankruptcyCase.Trustee = _reader.GetString(9);
                }
                if (_reader.IsDBNull(10) != true)
                {
                    objBankruptcyCase.Judge = _reader.GetString(10);
                }
                if (_reader.IsDBNull(11) != true)
                {
                    objBankruptcyCase.County = _reader.GetString(11);
                }
                if (_reader.IsDBNull(12) != true)
                {
                    objBankruptcyCase.Office = _reader.GetString(12);
                }
                if (_reader.IsDBNull(13) != true)
                {
                    objBankruptcyCase.Fee = _reader.GetString(13);
                }
                if (_reader.IsDBNull(14) != true)
                {
                    objBankruptcyCase.Asset = _reader.GetString(14);
                }
                if (_reader.IsDBNull(15) != true)
                {
                    objBankruptcyCase.FirstName = _reader.GetString(15);
                }
                if (_reader.IsDBNull(16) != true)
                {
                    objBankruptcyCase.MiddleName = _reader.GetString(16);
                }
                if (_reader.IsDBNull(17) != true)
                {
                    objBankruptcyCase.LastName = _reader.GetString(17);
                }
                if (_reader.IsDBNull(18) != true)
                {
                    objBankruptcyCase.Suffix = _reader.GetString(18);
                }
                if (_reader.IsDBNull(19) != true)
                {
                    objBankruptcyCase.AddrLine1 = _reader.GetString(19);
                }
                if (_reader.IsDBNull(20) != true)
                {
                    objBankruptcyCase.AddrLine2 = _reader.GetString(20);
                }
                if (_reader.IsDBNull(21) != true)
                {
                    objBankruptcyCase.AddrLine3 = _reader.GetString(21);
                }
                if (_reader.IsDBNull(22) != true)
                {
                    objBankruptcyCase.City = _reader.GetString(22);
                }
                if (_reader.IsDBNull(23) != true)
                {
                    objBankruptcyCase.StateCode = _reader.GetString(23);
                }
                if (_reader.IsDBNull(24) != true)
                {
                    objBankruptcyCase.PostalCode = _reader.GetInt32(24);
                }
                if (_reader.IsDBNull(25) != true)
                {
                    objBankruptcyCase.PostalLast4 = _reader.GetString(25);
                }
                if (_reader.IsDBNull(26) != true)
                {
                    objBankruptcyCase.Latitude = _reader.GetDouble(26);
                }
                if (_reader.IsDBNull(27) != true)
                {
                    objBankruptcyCase.Longitude = _reader.GetDouble(27);
                }
                if (_reader.IsDBNull(28) != true)
                {
                    objBankruptcyCase.Country = _reader.GetString(28);
                }
                if (_reader.IsDBNull(29) != true)
                {
                    objBankruptcyCase.Phone = _reader.GetString(29);
                }
                if (_reader.IsDBNull(30) != true)
                {
                    objBankruptcyCase.ssnLast4 = _reader.GetString(30);
                }
                if (_reader.IsDBNull(31) != true)
                {
                    objBankruptcyCase.CareOf = _reader.GetString(31);
                }
                if (_reader.IsDBNull(32) != true)
                {
                    objBankruptcyCase.DontSend = _reader.GetBoolean(32);
                }
                if (_reader.IsDBNull(33) != true)
                {
                    objBankruptcyCase.Returned = _reader.GetBoolean(33);
                }
                if (_reader.IsDBNull(34) != true)
                {
                    objBankruptcyCase.SoldCount = _reader.GetInt16(34);
                }
                if (_reader.IsDBNull(35) != true)
                {
                    objBankruptcyCase.LastSoldDate = _reader.GetDateTime(35);
                }
                if (_reader.IsDBNull(36) != true)
                {
                    objBankruptcyCase.BKLeadsOnlineUploadedDate = _reader.GetDateTime(36);
                }
                if (_reader.IsDBNull(37) != true)
                {
                    objBankruptcyCase.CourtName = _reader.GetString(37);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public List<BankruptcyCase> GetAll()
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }
                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }
        }
        static public List<BankruptcyCase> GetFiltered(string PacerImportTransactionID, string CaseNumber, string DebtorFirstName, string DebtorLastName, int CourtID, string PostalCode,  bool UnsoldOnly)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = _selectViewSQLRestricted + " where ";

            if (PacerImportTransactionID.Length > 0)
            {
                _query += " PacerImportTransactionID = " + PacerImportTransactionID + " and ";
            }

            if (CaseNumber.Length > 0)
            {
                _query += " CaseNumber4Digit like '%" + CaseNumber + "%' and ";
            }

            if (DebtorFirstName.Length > 0)
            {
                _query += " FirstName like '" + DebtorFirstName + "%' and ";
            }

            if (DebtorLastName.Length > 0)
            {
                _query += " LastName like '" + DebtorLastName + "%' and ";
            }

            if (PostalCode.Length == 5)
            {
                _query += " PostalCode = " + PostalCode + " and ";
            }
    
            if (CourtID > 0)
            {
                _query += " CourtID = " + CourtID + " and ";
            }

            if (UnsoldOnly)
            {
                _query += " SoldCount = 0 and ";
            }

            _query += " 1=1 ";
           
            using ( IDataReader reader = db.ExecuteReader(CommandType.Text, _query) )
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }
                
                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }
        }
        static public List<BankruptcyCase> getMailingList(DateTime startDate, DateTime endDate, int DealerID, bool FiledOnly, int DefaultNumberMailings)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = "Select TOP " + DefaultNumberMailings + " ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, DontSend, Returned, SoldCount, LastSoldDate, BKLeadsOnlineUploadedDate, CourtName from v_BankruptcyCase where ";

            _query += "( LastSoldDate is null OR LastSoldDate < '" + DateTime.Now.AddDays(-90).ToString("MM/dd/yyyy") + "' ) AND ";

            _query += "( DontSend = 0 and Returned = 0  ) AND ";
            
            _query += " PostalCode in (select ZipCode from ZipGeoCode zgc inner join DealerZipCode dzc on dzc.ZipGeoCodeID = zgc.ID where dzc.DealerID = " + DealerID + " ) AND";


            if (FiledOnly)
            {
                _query += " DischargeDate  is null and (FiledDate > '" + startDate.ToString("MM/dd/yyyy") + "' and FiledDate < '" + endDate.ToString("MM/dd/yyyy") + "') ";
                _query += " order by FiledDate desc ";
            }
            else 
            {
                _query += " DischargeDate  is not null and (DischargeDate > '" + startDate.ToString("MM/dd/yyyy") + "' and DischargeDate < '" + endDate.ToString("MM/dd/yyyy") + "') ";
                _query += " order by DischargeDate desc ";
            }

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }
                
                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }

            //now insert the rows into the





        }
        static public List<BankruptcyCase> getMailingList(int DealerMailingListID)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = "Select ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, " 
                + "Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, " 
                + "PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, DontSend, Returned, SoldCount, LastSoldDate, " 
                + "BKLeadsOnlineUploadedDate,  CourtName " 
                + "from v_BankruptcyCase inner join DealerMailingListCase " 
                + "on v_BankruptcyCase.ID = DealerMailingListCase.BankruptcyCaseID  " 
                + "where ";

            _query += " DealerMailingListCase.DealerMailingListID = " + DealerMailingListID + " AND ";

            _query += "( DontSend = 0 and Returned = 0  ) ";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }

                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }
        }
        static public List<BankruptcyCase> getMailingList(DealerMailingList objDealerMailingList, Database db, DbTransaction trans, bool CommitTrans)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            try
            {
                //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

                string _query = "INSERT DealerMailingListCase (DealerMailinglistID, BankruptcyCaseID) select TOP  " + objDealerMailingList.NumberCases + " " + objDealerMailingList.ID + ", ID  from BankruptcyCase  where ";

                _query += "( LastSoldDate is null OR LastSoldDate < '" + DateTime.Now.AddDays(-90).ToString("MM/dd/yyyy") + "' ) AND ";

                _query += "( DontSend = 0 and Returned = 0  ) AND ";

                _query += " PostalCode in (select ZipCode from ZipGeoCode zgc inner join DealerZipCode dzc on dzc.ZipGeoCodeID = zgc.ID where dzc.DealerID = " + objDealerMailingList.DealerID + " ) AND";


                if (objDealerMailingList.FiledCasesOnly)
                {
                    _query += " DischargeDate  is null and (FiledDate > '" + objDealerMailingList.StartFilterDate.ToString("MM/dd/yyyy") + "' and FiledDate < '" + objDealerMailingList.EndFilterDate.ToString("MM/dd/yyyy") + "') ";
                    _query += " order by FiledDate desc ";
                }
                else
                {
                    _query += " DischargeDate  is not null and (DischargeDate > '" + objDealerMailingList.StartFilterDate.ToString("MM/dd/yyyy") + "' and DischargeDate < '" + objDealerMailingList.EndFilterDate.ToString("MM/dd/yyyy") + "') ";
                    _query += " order by DischargeDate desc ";
                }

                System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_GetMailingListAndMarkAsSold");
                db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, objDealerMailingList.ID);
                db.AddInParameter(dbCommand, "InsertQueryString", DbType.String, _query);

                using (IDataReader reader = db.ExecuteReader(dbCommand, trans))
                {
                    while (reader.Read())
                    {
                        BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                        LoadBankruptcyCase(objBankruptcyCase, reader);
                        objBankruptcyCase.IsModified = false;
                        objBankruptcyCases.Add(objBankruptcyCase);
                    }

                    // always call Close when done reading.
                    reader.Close();
                    return objBankruptcyCases;
                }
            }
            catch (Exception e)
            {
                if (CommitTrans == true)
                {
                    //we were hoping to commit the transaction which means we're at the end so
                    //Roll back the transaction. Otherwise just rethrow the error and let the next
                    //higher level catch it and rollback
                    trans.Rollback();
                }
                //and then rethrow the error
                throw (e);
            }


        }
        /// <summary>
        /// gets all the cases that have an uploaded date of null
        /// </summary>
        /// <returns></returns>
        static public List<BankruptcyCase> GetUploadableCases()
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = _selectViewSQL 
                + " where BKLeadsOnlineUploadedDate is null AND (DischargeDate is null OR DischargeDate > '" 
                + DateTime.Now.AddYears(-4).ToShortDateString() + "')  ";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }
                
                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }

        }
     
        
        static public BankruptcyCase GetByID(int ID)
        {
            BankruptcyCase objBankruptcyCase = new BankruptcyCase();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objBankruptcyCase.IsModified = false;
                    return objBankruptcyCase;
                }
                else
                {
                    return null;
                }
            }
        }
        static public int getTotalAvailableCount(int DealerID, DateTime startDate, DateTime endDate, bool filedOnly)
        {
            int _total;

            //select count(*) from BankruptcyCase where 
            //DischargeDate is not null and
            //FiledDate between '06/01/2009' and '09/01/2011' and
            //PostalCode in 
            //(select ZipCode from ZipGeoCode zgc inner join 
            //DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID
            //where DealerID = 27) and
            //(LastSoldDate is null or
            //LastSoldDate < '06/01/2011')

            try
            {
                
                //select count(distinct(BankruptcyCase.ID)) as total from BankruptcyCase
                //inner join ZipGeoCode zgc on BankruptcyCase.PostalCode = zgc.ZipCode
                //inner join DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID
                //where ( DontSend = 0 and Returned = 0  ) AND  
                //DischargeDate between '08/02/2011' and '10/31/2011' and  

                
                //TO MATCH THE SUMMARIZATION CODE THE query should be equivalent to :
                //(select COUNT(distinct(bc.ID)) from BankruptcyCase bc where bc.PostalCode = ZipGeoCode.ZipCode
                //and (bc.LastSoldDate is null or bc.LastSoldDate <= DATEADD(day,-90,CAST(FLOOR(CAST(getdate() AS FLOAT)) AS DATETIME))) 
                //and (bc.DischargeDate >= DATEADD(year,-2,CAST(FLOOR(CAST(getdate() AS FLOAT)) AS DATETIME)))
                //)

                //dzc.DealerID = 373

                string query = "select count(distinct(BankruptcyCase.ID)) as total from BankruptcyCase ";
                query += "inner join ZipGeoCode zgc on BankruptcyCase.PostalCode = zgc.ZipCode ";
                query += "inner join DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID ";
                query += " where ( DontSend = 0 and Returned = 0  ) AND ";

                if (filedOnly == true)
                {
                    query += " DischargeDate is null and ";
                    query += " FiledDate between '" + startDate.ToString("MM/dd/yyyy") + "' and '" + endDate.ToString("MM/dd/yyyy") + "' and ";
                }
                else
                {
                    query += " DischargeDate between '" + startDate.ToString("MM/dd/yyyy") + "' and '" + endDate.ToString("MM/dd/yyyy") + "' and ";
                }

                query += " dzc.DealerID = " + DealerID;

                Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
                using (IDataReader _reader = db.ExecuteReader(CommandType.Text, query))
                {
                    if (_reader.Read())
                    {
                        if (_reader.IsDBNull(0) != true)
                        {
                            _total = _reader.GetInt32(0);
                        }
                        else
                        {
                            _total = 0;
                        }
                    
                        // always call Close when done reading.
                        _reader.Close();
                        return _total;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        
        }
        static public int getTotalUnsoldCount(int DealerID, DateTime startDate, DateTime endDate, bool filedOnly)
        {
            int _total;
            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            //System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseInsert");
            //db.AddOutParameter(dbCommand, "Count", DbType.Int32, 4);
            //db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            //db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
            //db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);            
            //db.AddInParameter(dbCommand, "FiledOnly", DbType.Boolean, FileOnly);
            //db.ExecuteNonQuery(dbCommand);
            //return (Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString()));

            //select count(*) from BankruptcyCase where 
            //DischargeDate is not null and
            //FiledDate between '06/01/2009' and '09/01/2011' and
            //PostalCode in 
            //(select ZipCode from ZipGeoCode zgc inner join 
            //DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID
            //where DealerID = 27) and
            //(LastSoldDate is null or
            //LastSoldDate < '06/01/2011')

            try
            {
                string query = "select count(distinct(BankruptcyCase.ID)) as total from BankruptcyCase ";
                query += "inner join ZipGeoCode zgc on BankruptcyCase.PostalCode = zgc.ZipCode ";
                query += "inner join DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID ";
                query += " where ( DontSend = 0 and Returned = 0  ) AND ";

                if (filedOnly == true)
                {
                    query += " DischargeDate is null and ";
                    query += " FiledDate between '" + startDate.ToString("MM/dd/yyyy") + "' and '" + endDate.ToString("MM/dd/yyyy") + "' and ";
                }
                else
                {
                    query += " DischargeDate between '" + startDate.ToString("MM/dd/yyyy") + "' and '" + endDate.ToString("MM/dd/yyyy") + "' and ";
                }

                query += " dzc.DealerID = " + DealerID + " and ";

                query += " (LastSoldDate is null or LastSoldDate < '" + DateTime.Now.AddDays(-90).ToString("MM/dd/yyyy") + "') ";

                Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
                using (IDataReader _reader = db.ExecuteReader(CommandType.Text, query))
                {
                    if (_reader.Read())
                    {
                        if (_reader.IsDBNull(0) != true)
                        {
                            _total = _reader.GetInt32(0);
                        }
                        else
                        {
                            _total = 0;
                        }

                        // always call Close when done reading.
                        _reader.Close();
                        return _total;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        static public void Delete(BankruptcyCase objBankruptcyCase)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseDelete");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objBankruptcyCase.ID);
            db.ExecuteNonQuery(dbCommand);
        }
        static private void Insert(BankruptcyCase objBankruptcyCase)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objBankruptcyCase.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objBankruptcyCase.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CMECF_Internal", DbType.Int32, objBankruptcyCase.CMECF_Internal);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objBankruptcyCase.CaseNumber4Digit);
            if (objBankruptcyCase.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objBankruptcyCase.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Chapter", DbType.Int16, objBankruptcyCase.Chapter);
            db.AddInParameter(dbCommand, "FiledOnly", DbType.Boolean, objBankruptcyCase.FiledOnly);
            if (objBankruptcyCase.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objBankruptcyCase.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.DischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, objBankruptcyCase.DischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Trustee", DbType.String, objBankruptcyCase.Trustee);
            db.AddInParameter(dbCommand, "Judge", DbType.String, objBankruptcyCase.Judge);
            db.AddInParameter(dbCommand, "County", DbType.String, objBankruptcyCase.County);
            db.AddInParameter(dbCommand, "Office", DbType.String, objBankruptcyCase.Office);
            db.AddInParameter(dbCommand, "Fee", DbType.String, objBankruptcyCase.Fee);
            db.AddInParameter(dbCommand, "Asset", DbType.String, objBankruptcyCase.Asset);
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objBankruptcyCase.FirstName);
            db.AddInParameter(dbCommand, "MiddleName", DbType.String, objBankruptcyCase.MiddleName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objBankruptcyCase.LastName);
            db.AddInParameter(dbCommand, "Suffix", DbType.String, objBankruptcyCase.Suffix);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objBankruptcyCase.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objBankruptcyCase.AddrLine2);
            db.AddInParameter(dbCommand, "AddrLine3", DbType.String, objBankruptcyCase.AddrLine3);
            db.AddInParameter(dbCommand, "City", DbType.String, objBankruptcyCase.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objBankruptcyCase.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.Int32, objBankruptcyCase.PostalCode);
            db.AddInParameter(dbCommand, "PostalLast4", DbType.String, objBankruptcyCase.PostalLast4);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objBankruptcyCase.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objBankruptcyCase.Longitude);
            db.AddInParameter(dbCommand, "Country", DbType.String, objBankruptcyCase.Country);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objBankruptcyCase.Phone);
            db.AddInParameter(dbCommand, "ssnLast4", DbType.String, objBankruptcyCase.ssnLast4);
            db.AddInParameter(dbCommand, "CareOf", DbType.String, objBankruptcyCase.CareOf);
            db.AddInParameter(dbCommand, "DontSend", DbType.Boolean, objBankruptcyCase.DontSend);
            db.AddInParameter(dbCommand, "Returned", DbType.Boolean, objBankruptcyCase.Returned);
            db.AddInParameter(dbCommand, "SoldCount", DbType.Int16, objBankruptcyCase.SoldCount);
            if (objBankruptcyCase.LastSoldDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, objBankruptcyCase.LastSoldDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.BKLeadsOnlineUploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, objBankruptcyCase.BKLeadsOnlineUploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
            objBankruptcyCase.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(BankruptcyCase objBankruptcyCase)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
                System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseUpdate");
                db.AddInParameter(dbCommand, "ID", DbType.Int32, objBankruptcyCase.ID);
                if (objBankruptcyCase.CourtID > 0)
                {
                    db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objBankruptcyCase.CourtID);
                }
                else
                {
                    db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
                }
                db.AddInParameter(dbCommand, "CMECF_Internal", DbType.Int32, objBankruptcyCase.CMECF_Internal);
                db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objBankruptcyCase.CaseNumber4Digit);
                if (objBankruptcyCase.PacerImportTransactionID > 0)
                {
                    db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objBankruptcyCase.PacerImportTransactionID);
                }
                else
                {
                    db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
                }
                db.AddInParameter(dbCommand, "Chapter", DbType.Int16, objBankruptcyCase.Chapter);
                db.AddInParameter(dbCommand, "FiledOnly", DbType.Boolean, objBankruptcyCase.FiledOnly);
                if (objBankruptcyCase.FiledDate > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objBankruptcyCase.FiledDate);
                }
                else
                {
                    db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
                }
                if (objBankruptcyCase.DischargeDate > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, objBankruptcyCase.DischargeDate);
                }
                else
                {
                    db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, Convert.DBNull);
                }
                db.AddInParameter(dbCommand, "Trustee", DbType.String, objBankruptcyCase.Trustee);
                db.AddInParameter(dbCommand, "Judge", DbType.String, objBankruptcyCase.Judge);
                db.AddInParameter(dbCommand, "County", DbType.String, objBankruptcyCase.County);
                db.AddInParameter(dbCommand, "Office", DbType.String, objBankruptcyCase.Office);
                db.AddInParameter(dbCommand, "Fee", DbType.String, objBankruptcyCase.Fee);
                db.AddInParameter(dbCommand, "Asset", DbType.String, objBankruptcyCase.Asset);
                db.AddInParameter(dbCommand, "FirstName", DbType.String, objBankruptcyCase.FirstName);
                db.AddInParameter(dbCommand, "MiddleName", DbType.String, objBankruptcyCase.MiddleName);
                db.AddInParameter(dbCommand, "LastName", DbType.String, objBankruptcyCase.LastName);
                db.AddInParameter(dbCommand, "Suffix", DbType.String, objBankruptcyCase.Suffix);
                db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objBankruptcyCase.AddrLine1);
                db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objBankruptcyCase.AddrLine2);
                db.AddInParameter(dbCommand, "AddrLine3", DbType.String, objBankruptcyCase.AddrLine3);
                db.AddInParameter(dbCommand, "City", DbType.String, objBankruptcyCase.City);
                db.AddInParameter(dbCommand, "StateCode", DbType.String, objBankruptcyCase.StateCode);
                db.AddInParameter(dbCommand, "PostalCode", DbType.Int32, objBankruptcyCase.PostalCode);
                db.AddInParameter(dbCommand, "PostalLast4", DbType.String, objBankruptcyCase.PostalLast4);
                db.AddInParameter(dbCommand, "Latitude", DbType.Double, objBankruptcyCase.Latitude);
                db.AddInParameter(dbCommand, "Longitude", DbType.Double, objBankruptcyCase.Longitude);
                db.AddInParameter(dbCommand, "Country", DbType.String, objBankruptcyCase.Country);
                db.AddInParameter(dbCommand, "Phone", DbType.String, objBankruptcyCase.Phone);
                db.AddInParameter(dbCommand, "ssnLast4", DbType.String, objBankruptcyCase.ssnLast4);
                db.AddInParameter(dbCommand, "CareOf", DbType.String, objBankruptcyCase.CareOf);
                db.AddInParameter(dbCommand, "DontSend", DbType.Boolean, objBankruptcyCase.DontSend);
                db.AddInParameter(dbCommand, "Returned", DbType.Boolean, objBankruptcyCase.Returned);
                db.AddInParameter(dbCommand, "SoldCount", DbType.Int16, objBankruptcyCase.SoldCount);
                if (objBankruptcyCase.LastSoldDate > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, objBankruptcyCase.LastSoldDate);
                }
                else
                {
                    db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, Convert.DBNull);
                }
                if (objBankruptcyCase.BKLeadsOnlineUploadedDate > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, objBankruptcyCase.BKLeadsOnlineUploadedDate);
                }
                else
                {
                    db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, Convert.DBNull);
                }
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region save, insert, update using transactions
        static private void Insert(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objBankruptcyCase.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objBankruptcyCase.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CMECF_Internal", DbType.Int32, objBankruptcyCase.CMECF_Internal);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objBankruptcyCase.CaseNumber4Digit);
            if (objBankruptcyCase.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objBankruptcyCase.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Chapter", DbType.Int16, objBankruptcyCase.Chapter);
            db.AddInParameter(dbCommand, "FiledOnly", DbType.Boolean, objBankruptcyCase.FiledOnly);
            if (objBankruptcyCase.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objBankruptcyCase.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.DischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, objBankruptcyCase.DischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Trustee", DbType.String, objBankruptcyCase.Trustee);
            db.AddInParameter(dbCommand, "Judge", DbType.String, objBankruptcyCase.Judge);
            db.AddInParameter(dbCommand, "County", DbType.String, objBankruptcyCase.County);
            db.AddInParameter(dbCommand, "Office", DbType.String, objBankruptcyCase.Office);
            db.AddInParameter(dbCommand, "Fee", DbType.String, objBankruptcyCase.Fee);
            db.AddInParameter(dbCommand, "Asset", DbType.String, objBankruptcyCase.Asset);
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objBankruptcyCase.FirstName);
            db.AddInParameter(dbCommand, "MiddleName", DbType.String, objBankruptcyCase.MiddleName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objBankruptcyCase.LastName);
            db.AddInParameter(dbCommand, "Suffix", DbType.String, objBankruptcyCase.Suffix);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objBankruptcyCase.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objBankruptcyCase.AddrLine2);
            db.AddInParameter(dbCommand, "AddrLine3", DbType.String, objBankruptcyCase.AddrLine3);
            db.AddInParameter(dbCommand, "City", DbType.String, objBankruptcyCase.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objBankruptcyCase.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.Int32, objBankruptcyCase.PostalCode);
            db.AddInParameter(dbCommand, "PostalLast4", DbType.String, objBankruptcyCase.PostalLast4);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objBankruptcyCase.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objBankruptcyCase.Longitude);
            db.AddInParameter(dbCommand, "Country", DbType.String, objBankruptcyCase.Country);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objBankruptcyCase.Phone);
            db.AddInParameter(dbCommand, "ssnLast4", DbType.String, objBankruptcyCase.ssnLast4);
            db.AddInParameter(dbCommand, "CareOf", DbType.String, objBankruptcyCase.CareOf);
            db.AddInParameter(dbCommand, "DontSend", DbType.Boolean, objBankruptcyCase.DontSend);
            db.AddInParameter(dbCommand, "Returned", DbType.Boolean, objBankruptcyCase.Returned);
            db.AddInParameter(dbCommand, "SoldCount", DbType.Int16, objBankruptcyCase.SoldCount);
            if (objBankruptcyCase.LastSoldDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, objBankruptcyCase.LastSoldDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.BKLeadsOnlineUploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, objBankruptcyCase.BKLeadsOnlineUploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
            objBankruptcyCase.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objBankruptcyCase.ID);
            if (objBankruptcyCase.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objBankruptcyCase.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CMECF_Internal", DbType.Int32, objBankruptcyCase.CMECF_Internal);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objBankruptcyCase.CaseNumber4Digit);
            if (objBankruptcyCase.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objBankruptcyCase.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Chapter", DbType.Int16, objBankruptcyCase.Chapter);
            db.AddInParameter(dbCommand, "FiledOnly", DbType.Boolean, objBankruptcyCase.FiledOnly);
            if (objBankruptcyCase.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objBankruptcyCase.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.DischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, objBankruptcyCase.DischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargeDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Trustee", DbType.String, objBankruptcyCase.Trustee);
            db.AddInParameter(dbCommand, "Judge", DbType.String, objBankruptcyCase.Judge);
            db.AddInParameter(dbCommand, "County", DbType.String, objBankruptcyCase.County);
            db.AddInParameter(dbCommand, "Office", DbType.String, objBankruptcyCase.Office);
            db.AddInParameter(dbCommand, "Fee", DbType.String, objBankruptcyCase.Fee);
            db.AddInParameter(dbCommand, "Asset", DbType.String, objBankruptcyCase.Asset);
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objBankruptcyCase.FirstName);
            db.AddInParameter(dbCommand, "MiddleName", DbType.String, objBankruptcyCase.MiddleName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objBankruptcyCase.LastName);
            db.AddInParameter(dbCommand, "Suffix", DbType.String, objBankruptcyCase.Suffix);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objBankruptcyCase.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objBankruptcyCase.AddrLine2);
            db.AddInParameter(dbCommand, "AddrLine3", DbType.String, objBankruptcyCase.AddrLine3);
            db.AddInParameter(dbCommand, "City", DbType.String, objBankruptcyCase.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objBankruptcyCase.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.Int32, objBankruptcyCase.PostalCode);
            db.AddInParameter(dbCommand, "PostalLast4", DbType.String, objBankruptcyCase.PostalLast4);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objBankruptcyCase.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objBankruptcyCase.Longitude);
            db.AddInParameter(dbCommand, "Country", DbType.String, objBankruptcyCase.Country);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objBankruptcyCase.Phone);
            db.AddInParameter(dbCommand, "ssnLast4", DbType.String, objBankruptcyCase.ssnLast4);
            db.AddInParameter(dbCommand, "CareOf", DbType.String, objBankruptcyCase.CareOf);
            db.AddInParameter(dbCommand, "DontSend", DbType.Boolean, objBankruptcyCase.DontSend);
            db.AddInParameter(dbCommand, "Returned", DbType.Boolean, objBankruptcyCase.Returned);
            db.AddInParameter(dbCommand, "SoldCount", DbType.Int16, objBankruptcyCase.SoldCount);
            if (objBankruptcyCase.LastSoldDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, objBankruptcyCase.LastSoldDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastSoldDate", DbType.DateTime, Convert.DBNull);
            }
            if (objBankruptcyCase.BKLeadsOnlineUploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, objBankruptcyCase.BKLeadsOnlineUploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "BKLeadsOnlineUploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        
        //warning overridden code
        static public BankruptcyCase GetByCaseNumber(int CMECF_Internal, string FirstName, string SSNLast4, int CourtID)
        {
            try
            {
                BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                string query;

                if (SSNLast4 != null && SSNLast4.Length > 0)
                {
                    query = _selectViewSQL + " where CMECF_Internal = " + CMECF_Internal + " AND (FirstName = '" + FirstName.Replace("'", "''") + "' OR SSNLast4 = '" + SSNLast4 + "') AND  CourtID = " + CourtID;
                }
                else
                {
                    query = _selectViewSQL + " where CMECF_Internal = " + CMECF_Internal + " AND (FirstName = '" + FirstName + "') AND  CourtID = " + CourtID;
                }

                Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
                using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
                {
                    if (reader.Read())
                    {
                        LoadBankruptcyCase(objBankruptcyCase, reader);
                        // always call Close when done reading.
                        reader.Close();
                        objBankruptcyCase.IsModified = false;
                        return objBankruptcyCase;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

        static public BankruptcyCase GetBy4DigitCaseNumber(string CaseNumber4Digit, string FirstName, string SSNLast4, int CourtID)
        {
            BankruptcyCase objBankruptcyCase = new BankruptcyCase();
            string query = _selectViewSQL + " where CaseNumber4Digit = '" + CaseNumber4Digit + "' AND (FirstName = '" + FirstName + "' OR SSNLast4 = '" + SSNLast4 + "') AND CourtID = " + CourtID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objBankruptcyCase.IsModified = false;
                    return objBankruptcyCase;
                }
                else
                {
                    return null;
                }
            }
        }
        static public int Save(BankruptcyCase objBankruptcyCase, bool resetBKLeadsOnlineUploadedDate)
        {
            try
            {
                if (objBankruptcyCase.IsModified == true)
                {
                    if (objBankruptcyCase.ID == 0)
                    {
                        //this is a new case, the only place we get new cases from is an import
                        //so check we don;t already have this case...

                        //we nned to check for the first name or social so we get the right record on joint accounts and ignore updates from this import transaction as sometimes there
                        //are duplicate rows only with the attorney changed.
                        BankruptcyCase _case = GetByCaseNumber(objBankruptcyCase.CMECF_Internal, objBankruptcyCase.FirstName, objBankruptcyCase.ssnLast4, objBankruptcyCase.CourtID);
                        Debug.WriteLine(objBankruptcyCase.CMECF_Internal);
                        if (_case == null)
                        {
                            _case = GetBy4DigitCaseNumber(objBankruptcyCase.CaseNumber4Digit, objBankruptcyCase.FirstName, objBankruptcyCase.ssnLast4, objBankruptcyCase.CourtID);
                        }

                        if (_case != null)
                        {
                            objBankruptcyCase.ID = _case.ID;
                            objBankruptcyCase.SoldCount = _case.SoldCount;
                            objBankruptcyCase.LastSoldDate = _case.LastSoldDate;
                            objBankruptcyCase.DontSend = _case.DontSend;
                            objBankruptcyCase.Returned = _case.Returned;
                            if (resetBKLeadsOnlineUploadedDate == true)
                            {
                                //reset the uploaddate so the BKLeadsSysnch will pick up
                                objBankruptcyCase.BKLeadsOnlineUploadedDate = DateTime.MinValue;
                            }
                            else
                                objBankruptcyCase.BKLeadsOnlineUploadedDate = _case.BKLeadsOnlineUploadedDate;

                            Update(objBankruptcyCase);
                            if (_case.PacerImportTransactionID != objBankruptcyCase.PacerImportTransactionID)
                            {
                                //a real update
                                return 2;
                            }
                            else
                            {
                                return 3; // - duplicate row for person usually means that the person has changed their lawyer.
                            }
                        }
                        else
                        {
                            Insert(objBankruptcyCase);
                            return 1;
                        }
                    }
                    else
                    {
                        Update(objBankruptcyCase);
                        return 2;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(objBankruptcyCase.CaseNumber4Digit);
                if (ex.Message.Contains("Cannot insert duplicate key row in object 'dbo.BankruptcyCase' with unique index 'idx_CMECF'"))
                {
                    return 0;
                }
                else
                {
                    throw ex;
                }
            }
        }
        static public int Save(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans, bool CommitTrans, bool resetBKLeadsOnlineUploadedDate)
        {
            try
            {
                if (objBankruptcyCase.IsModified == true)
                {
                    if (objBankruptcyCase.ID == 0)
                    {
                        //this is a new case, the only place we get new cases from is an import
                        //so check we don;t already have this case...
                        //we nned to check for the first name or social so we get the right record on joint accounts
                        BankruptcyCase _case = GetByCaseNumber(objBankruptcyCase.CMECF_Internal, objBankruptcyCase.FirstName, objBankruptcyCase.ssnLast4, objBankruptcyCase.CourtID);
                        if (_case == null)
                        {
                            _case = GetBy4DigitCaseNumber(objBankruptcyCase.CaseNumber4Digit, objBankruptcyCase.FirstName, objBankruptcyCase.ssnLast4, objBankruptcyCase.CourtID);
                        }

                        if (_case != null)
                        {
                            objBankruptcyCase.ID = _case.ID;
                            objBankruptcyCase.SoldCount = _case.SoldCount;
                            objBankruptcyCase.LastSoldDate = _case.LastSoldDate;
                            objBankruptcyCase.DontSend = _case.DontSend;
                            objBankruptcyCase.Returned = _case.Returned;
                            if (resetBKLeadsOnlineUploadedDate == true)
                            {
                                //reset the uploaddate so the BKLeadsSysnch will pick up
                                objBankruptcyCase.BKLeadsOnlineUploadedDate = DateTime.MinValue;
                            }

                            Update(objBankruptcyCase, db, trans);
                            if (CommitTrans == true)
                            {
                                trans.Commit();
                            }
                            return 2;
                        }
                        else
                        {
                            Insert(objBankruptcyCase, db, trans);
                            if (CommitTrans == true)
                            {
                                trans.Commit();
                            }
                            return 1;
                        }
                    }
                    else
                    {
                        Update(objBankruptcyCase, db, trans);
                        if (CommitTrans == true)
                        {
                            trans.Commit();
                        }
                        return 2;
                    }
                }
                if (CommitTrans == true)
                {
                    trans.Commit();
                }
                return 0;
            }
            catch (Exception e)
            {
                if (CommitTrans == true)
                {
                    //we were hoping to commit the transaction which means we're at the end so
                    //Roll back the transaction. Otherwise just rethrow the error and let the next
                    //higher level catch it and rollback
                    trans.Rollback();
                }
                //and then rethrow the error
                throw (e);
            }
        }

        static public string ProcessRemoveFile(string FilePath)
        {
            int _successFullUpdates = 0;
            int _notFound = 0;
            int _errors = 0;
                
            string ErrorResultString = "Problems occurred on lines: ";

            using (TextReader tr = File.OpenText(FilePath))
            {
                String line;
                int i = 0;

 
                string _lastName = string.Empty;
                string _firstName = string.Empty;
                string _middleName = string.Empty;

                int _zip = 0;

                while ((line = tr.ReadLine()) != null)
                {
                    //ignore the first line
                    try
                    {
                        if (i > 0)
                        {
                            if (line.Contains("\""))
                            {
                                //lastname, firstName

                                _lastName = line.Substring(1, line.IndexOf(",") -1).Trim();
                                _firstName = line.Substring(line.IndexOf(",") + 1, line.LastIndexOf("\"") - (line.IndexOf(",")+1)).Trim();
                                _zip = Int32.Parse(line.Substring(line.LastIndexOf(",") + 1, line.Length - (line.LastIndexOf(",") + 1)));
                            }
                            else
                            {
                                string[] _array1 = line.Split(',');
                                _zip = Int32.Parse(_array1[1]);
                                string[] _array2 = _array1[0].Split(' ');
                                _firstName = _array2[0];
                                if (_array2.Length == 3)
                                {
                                    _middleName = _array2[1];
                                    _lastName = _array2[2];
                                }
                                else
                                {
                                    _lastName = _array2[1];
                                }
                            }

                            //try and find the case throws excetions where appropriate.
                            BankruptcyCase _case = GetByNameAndZip(_firstName, _middleName, _lastName, _zip);
                            
                            _case.DontSend = true;
                            Save(_case, false);

                            _successFullUpdates++;                                    

                        }
                    }
                    catch (Exception ex)
                    {
                        _errors++;
                        ErrorResultString += "\n" + line + "   Error: " + ex.Message.ToString();
                    }
                    
                    i++;
                }
            }

            string _returnString = _successFullUpdates.ToString() + " Successful Updates, " + _errors.ToString() + " Errors ";
            if (_errors > 0)
            {
                _returnString += "\n" + ErrorResultString;
            }

            return (_returnString);

        }
        
        static public BankruptcyCase GetByNameAndZip(string firstName, string middleName, string lastName, int zip)
        {
        
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string query = _selectViewSQL + " where FirstName = '" + firstName + "' and ";
            query += " LastName = '" + lastName + "' and ";
            query += " PostalCode = " + zip + " and ";

            if (middleName.Length > 0)
            {
                query += " MiddleName = '" + middleName + "' ";
            }
            else
            {
                query += " 1=1 ";
            }

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    BankruptcyCase objBankruptcyCase = new BankruptcyCase();
                    LoadBankruptcyCase(objBankruptcyCase, reader);
                    objBankruptcyCase.IsModified = false;
                    objBankruptcyCases.Add(objBankruptcyCase);
                }
            }

            if (objBankruptcyCases.Count == 0)
            {
                if (middleName.Length > 0)
                {
                    //call recursively without using middle name
                    return GetByNameAndZip(firstName, string.Empty, lastName, zip);
                }
                
                throw new Exception(firstName + " " + lastName + " Not Found ");
            }
            else if (objBankruptcyCases.Count > 1)
            {
                throw new Exception(firstName + " " + lastName + " - Found " + objBankruptcyCases.Count + " Cases  - not updated");
            }
            else
            {
                return objBankruptcyCases[0];
            }
        }
        
        static public void UpdateLastSoldDateForMailingList(int DealerMailingListID, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
                System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UpdateDealerMailingListLastSoldDate");
                db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, DealerMailingListID);
                db.ExecuteNonQuery(dbCommand, trans);
                if (CommitTrans == true)
                {
                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                if (CommitTrans == true)
                {
                    //we were hoping to commit the transaction which means we're at the end so
                    //Roll back the transaction. Otherwise just rethrow the error and let the next
                    //higher level catch it and rollback
                    trans.Rollback();
                }
                //and then rethrow the error
                throw (e);
            }
        }

        #region NCOA
        static public List<BankruptcyCase> GetAddressVerificationList(string court, int dealerId, string state, DateTime? startDate, DateTime? endDate)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = "SELECT DISTINCT bc.ID, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, bc.City, State, PostalCode " +
                "FROM v_BankruptcyCase bc ";

                if (court != null && court.Length > 0)
                {
                    _query += "LEFT JOIN dbo.Court c ON bc.CourtId = c.Id ";
                }

                if (court != null && court.Length > 0)
                {
                    _query +=  "LEFT JOIN dbo.ZipGeoCode zgc ON bc.PostalCode = zgc.ZipCode ";
                    _query += "LEFT JOIN dbo.DealerZipCOde dzc ON zgc.ID = dzc.ZipGeoCodeId ";
                }
                _query += "WHERE ";

            //string phone = "510-290-0676";
            //int result;
            //int numberCount = 0;
            ////for (int i = 0; i <= phone.Length; i++)
            //foreach(char c in phone)
            //{
            //    if (int.TryParse(c.ToString(), out result))
            //    {
            //        // It's a number
            //        numberCount++;
            //    }
            //}
            //if (numberCount == 10 || numberCount == 11)
            // It's a US phone number

            if (court.Length > 0)
            {
                _query += " c.CourtName = '" + court + "' and ";
            }

            if (dealerId > 0)
            {
                _query += " dzc.DealerID = '" + dealerId + "' and ";
            }

            if (state.Length > 0)
            {
                _query += " bc.StateCode = '" + state + "' and ";
            }

            if (startDate != null && endDate != null)
            {
                _query += " DischargeDate BETWEEN '" + startDate + "' AND '" + endDate + "' and ";
            }

            _query += " 1=1 ";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    BankruptcyCase bc = new BankruptcyCase();
                    bc.ID = reader.GetInt32(0);
                    if (reader.IsDBNull(1) != true)
                        bc.FirstName = reader.GetString(1);
                    if (reader.IsDBNull(2) != true)
                        bc.MiddleName = reader.GetString(2); // Throws error when null
                    if (reader.IsDBNull(3) != true)
                        bc.LastName = reader.GetString(3);
                    if (reader.IsDBNull(4) != true)
                        bc.Suffix = reader.GetString(4);
                    if (reader.IsDBNull(5) != true)
                        bc.AddrLine1 = reader.GetString(5);
                    if (reader.IsDBNull(6) != true)
                        bc.AddrLine2 = reader.GetString(6);
                    if (reader.IsDBNull(7) != true)
                        bc.City = reader.GetString(7);
                    if (reader.IsDBNull(8) != true)
                        bc.StateCode = reader.GetString(8);
                    if (reader.IsDBNull(9) != true)
                        bc.PostalCode = reader.GetInt32(9);
                    bc.IsModified = false;
                    objBankruptcyCases.Add(bc);
                }

                // always call Close when done reading.
                reader.Close();
                return objBankruptcyCases;
            }
        }
        static public bool CreateAddressVerificationCSVFile(List<BankruptcyCase> cases, string saveToPath)
        {
            string tab = "\t";
            string[] _columnHeadings = new string[] { "CaseID", "FirstName", "MiddleName", "LastName", "Suffix", "Address1", "Address2", "City", "State", "PostalCode" };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(tab, _columnHeadings));

            foreach (var bc in cases)
            {
                // BankruptyCase.ID, FirstName, MiddleName, LastName, Suffix, Address1, Address2, City, State, PostalCode
                sb.Append(bc.ID + tab + bc.FirstName + tab + bc.MiddleName + tab + bc.LastName + tab + bc.Suffix + tab + bc.AddrLine1 + tab + bc.AddrLine2 + tab + bc.City + tab + bc.StateCode + tab + bc.PostalCodeString + Environment.NewLine);
            }

            try
            {
                System.IO.File.WriteAllText(saveToPath, sb.ToString());
                return true;
            }
            catch (Exception x)
            {
                x.ToString();
            }

            return false;
        }
        static public bool ProcessAddressVerificationFile(string filePath)
        {
            // Delete data in NCOAImport table before adding new file data
            NCOAImportService.Delete();

            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(filePath))
            {
                parser.SetDelimiters("\t"); // Set tab as the delimiter
                parser.CommentTokens = new[] { "CASEID" }; // Ignore header

                while (!parser.EndOfData)
                {
                    string[] data = parser.ReadFields();

                    NCOAImport import = new NCOAImport();
                    import.CASEID = Convert.ToInt32(data[0]);
                    import.INPUTFIRST = data[1].ToString();
                    import.INPUTMIDDLE = data[2].ToString();
                    import.INPUTLAST = data[3].ToString();
                    import.INPUTSUFFIX = data[4].ToString();
                    import.ADDRESS1 = data[5].ToString();
                    import.ADDRESS2 = data[6].ToString();
                    import.CITY = data[7].ToString();
                    import.STATE = data[8].ToString();
                    import.ZIP = data[9].ToString();
                    import.NEWADDRESS_FLAG = data[10].ToString();
                    import.MRT_FLAG = data[11].ToString();
                    import.MAILTO1_ADDRESS = data[12].ToString();
                    import.MAILTO2_ADDRESS = data[13].ToString();
                    import.MAILTO_CITY = data[14].ToString();
                    import.MAILTO_STATE = data[15].ToString();
                    import.MAILTO_ZIP = data[16].ToString();
                    import.MAIL_ZIP4 = data[17].ToString();
                    import.MAILTO_DPBC = data[18].ToString();
                    import.MAIL_CARRIER_ROUTE = data[19].ToString();
                    import.DPVCONFIRMATION_INDICATOR = data[20].ToString();
                    import.NCOA_MATCH_TYPE = data[21].ToString();
                    import.NCOA_MOVE_DATE = data[22].ToString();
                    import.DELIVERY_CODE = data[23].ToString();
                    import.ZIP4_UPDATES = data[24].ToString();
                    import.MAILABILITY_SCORE = data[25].ToString();

                    NCOAImportService.Save(import);
                }
            }

            // After the import from the file to the table has completed then run the SP to update the BankruptcyCase
            // with the address verification results



            return true;
        }
        #endregion

    }
}
