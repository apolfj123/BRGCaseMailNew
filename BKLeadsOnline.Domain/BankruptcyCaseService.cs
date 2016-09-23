using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the BankruptcyCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/16/2013 7:39:23 PM
	/// </summary>
	public class BankruptcyCaseService 
	{
        static private string _selectViewSQL = "Select ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, UploadedDate, CourtName, ZipCodeString from v_BankruptcyCase";
        static private string _selectViewSQLTop1000 = "Select TOP 1000 ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, UploadedDate, CourtName, ZipCodeString from v_BankruptcyCase";
        static public List<BankruptcyCase> GetAll()
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        static public List<BankruptcyCase> GetTop1000Rows()
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLTop1000))
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
        /// <summary>
        /// Creates a new DealerMalingList
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="DealerID"></param>
        /// <param name="NumberMailings"></param>
        /// <returns></returns>
        static public List<BankruptcyCase> getMailingList(DateTime startDate, DateTime endDate, int DealerID, int NumberMailings)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            string _query = "Select TOP " + NumberMailings + " ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, UploadedDate, CourtName, ZipCodeString from v_BankruptcyCase where ";

            _query += " PostalCode in (select ZipCode from ZipGeoCode zgc inner join DealerZipCode dzc on dzc.ZipGeoCodeID = zgc.ID where dzc.DealerID = " + DealerID + " ) AND";
            _query += " DischargeDate  is not null and (DischargeDate > '" + startDate.ToString("MM/dd/yyyy") + "' and DischargeDate < '" + endDate.ToString("MM/dd/yyyy") + "')  AND ";
            _query += " ID not in (select BankruptcyCaseID from DealerMailingListCase dmlc where dmlc.DealerID = " + DealerID + ")";
            _query += " order by DischargeDate desc ";

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
        /// <summary>
        ///  Retrieves an existing DealerMailingList
        /// </summary>
        /// <param name="DealerMailingListID"></param>
        /// <param name="DealerID"></param>
        /// <param name="GetOriginalList">If false exlcude records marked "sold" and "Do not send"</param>
        /// <returns></returns>
        static public List<BankruptcyCase> getMailingList(int DealerMailingListID, int DealerID, bool GetOriginalList)
        {
            List<BankruptcyCase> objBankruptcyCases = new List<BankruptcyCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            string _query = "Select ID, CourtID, CMECF_Internal, CaseNumber4Digit, PacerImportTransactionID, Chapter, FiledOnly, FiledDate, DischargeDate, Trustee, Judge, County, Office, Fee, Asset, FirstName, MiddleName, LastName, Suffix, AddrLine1, AddrLine2, AddrLine3, City, StateCode, PostalCode, PostalLast4, Latitude, Longitude, Country, Phone, ssnLast4, CareOf, UploadedDate, CourtName, ZipCodeString from v_BankruptcyCase left Join DealerMailingListCase dmlc on dmlc.BankruptcyCaseID = v_BankruptcyCase.ID where";
            _query += " dmlc.DealerMailingListID = " + DealerMailingListID + " and DealerID=" + DealerID;
            if (GetOriginalList == false)
            {
                _query += " AND Sold=0 and DoNotSend = 0 ";
            }

            _query += " order by DischargeDate desc";

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
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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

        static public BankruptcyCase GetByCourtAnd4DigitCaseNum(int CourtID, string CaseNumber4Digit, string FirstName)
        {
            BankruptcyCase objBankruptcyCase = new BankruptcyCase();
            string query = _selectViewSQL + " where CourtID = " + CourtID + " and CaseNumber4Digit = '" + CaseNumber4Digit + "' and UPPER(FirstName) = '" + FirstName.ToUpper() + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        static public BankruptcyCase GetByCourtAndCMECF_Internal(int CourtID, int CMECF_Internal, string FirstName)
        {
            BankruptcyCase objBankruptcyCase = new BankruptcyCase();
            string query = _selectViewSQL + " where CourtID = " + CourtID + " and CMECF_Internal = " + CMECF_Internal + " and UPPER(FirstName) = '" + FirstName.ToUpper() + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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

        static private void LoadBankruptcyCase(BankruptcyCase objBankruptcyCase, IDataReader _reader)
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
                objBankruptcyCase.UploadedDate = _reader.GetDateTime(32);
            }
            if (_reader.IsDBNull(33) != true)
            {
                objBankruptcyCase.CourtName = _reader.GetString(33);
            }
            if (_reader.IsDBNull(34) != true)
            {
                objBankruptcyCase.ZipCodeString = _reader.GetString(34);
            }
        }
        static public void Save(BankruptcyCase objBankruptcyCase)
		{
			if (objBankruptcyCase.IsModified == true)
			{
				if (objBankruptcyCase.ID == 0 )
				{
					Insert(objBankruptcyCase);
				}
				else
				{
					Update(objBankruptcyCase);
				}
			}
		}
        
        //caution custom code
        static public void Insert(BankruptcyCase objBankruptcyCase)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseInsert");
            //db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
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
            if (objBankruptcyCase.UploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, objBankruptcyCase.UploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
            objBankruptcyCase.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static public void Update(BankruptcyCase objBankruptcyCase)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            if (objBankruptcyCase.UploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, objBankruptcyCase.UploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objBankruptcyCase.IsModified == true)
                {
                    if (objBankruptcyCase.ID == 0)
                    {
                        Insert(objBankruptcyCase, db, trans);
                    }
                    else
                    {
                        Update(objBankruptcyCase, db, trans);
                    }
                }
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
        static private void Insert(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            if (objBankruptcyCase.UploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, objBankruptcyCase.UploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
            objBankruptcyCase.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(BankruptcyCase objBankruptcyCase, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            if (objBankruptcyCase.UploadedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, objBankruptcyCase.UploadedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadedDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(BankruptcyCase objBankruptcyCase)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_BankruptcyCaseDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objBankruptcyCase.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
