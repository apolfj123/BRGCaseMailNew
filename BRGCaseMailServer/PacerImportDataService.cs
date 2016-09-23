using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the PacerImportData table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2011 1:00:59 AM
	/// </summary>
	public class PacerImportDataService 
	{
        static private string _selectViewSQL = "Select ID, Imported, PacerImportTransactionID, CaseNumber2Digit, CaseNumber4Digit, CMECF, Chapter, PreviousChapter, ConversionDate, DischargedDate, DismissedDate, FiledDate, EnteredDate, ClosedDate, Disposition, CaseType, TrusteeLastName, JudgeLastName, CountyFiled, OfficeName, FilingFeePaymentStatus, AssetsInCase, CaseDisposition, JointDebtorDischargeDate, JointDebtorDismissalDate, JointDebtorDispositionCode, JointDebtorDisposition, DivisionalOfficeFiled, AdversaryProceedingSeqNo, AdversaryProceedingYear, AdversaryProceedingDisposition, PartyFirstName, PartyMiddleName, PartyLastName, PartyGeneration, PartyOffice, PartyAddressLine1, PartyAddressLine2, PartyAddressLine3, PartyCity, PartyState, PartyZipCode, PartyCountry, PartyPhone, PartySSNo, PartyTaxID, PartyCaseSeqNo, PartyCaseRole, PartyTerminatedDate, CaseTitle, AttorneyFirstName, AttorneyMiddleName, AttorneyLastName, AttorneyGeneration, AttorneyOffice, AttorneyOfficeLine1, AttorneyOfficeLine2, AttorneyOfficeLine3, AttorneyCity, AttorneyState, AttorneyZipCode, AttorneyCountry, AttorneyPhone, CourtID, DischargedCases, CourtName from v_PacerImportData";
        static public List<PacerImportData> GetAll()
        {
            List<PacerImportData> objPacerImportDatas = new List<PacerImportData>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    PacerImportData objPacerImportData = new PacerImportData();
                    LoadPacerImportData(objPacerImportData, reader);
                    objPacerImportData.IsModified = false;
                    objPacerImportDatas.Add(objPacerImportData);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerImportDatas;
            }
        }
        static public PacerImportData GetByID(int ID)
        {
            PacerImportData objPacerImportData = new PacerImportData();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadPacerImportData(objPacerImportData, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objPacerImportData.IsModified = false;
                    return objPacerImportData;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadPacerImportData(PacerImportData objPacerImportData, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objPacerImportData.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objPacerImportData.Imported = _reader.GetBoolean(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objPacerImportData.PacerImportTransactionID = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objPacerImportData.CaseNumber2Digit = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objPacerImportData.CaseNumber4Digit = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objPacerImportData.CMECF = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objPacerImportData.Chapter = _reader.GetInt32(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objPacerImportData.PreviousChapter = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objPacerImportData.ConversionDate = _reader.GetDateTime(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objPacerImportData.DischargedDate = _reader.GetDateTime(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objPacerImportData.DismissedDate = _reader.GetDateTime(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objPacerImportData.FiledDate = _reader.GetDateTime(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objPacerImportData.EnteredDate = _reader.GetDateTime(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objPacerImportData.ClosedDate = _reader.GetDateTime(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objPacerImportData.Disposition = _reader.GetString(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objPacerImportData.CaseType = _reader.GetString(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objPacerImportData.TrusteeLastName = _reader.GetString(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objPacerImportData.JudgeLastName = _reader.GetString(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objPacerImportData.CountyFiled = _reader.GetString(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objPacerImportData.OfficeName = _reader.GetString(19);
            }
            if (_reader.IsDBNull(20) != true)
            {
                objPacerImportData.FilingFeePaymentStatus = _reader.GetString(20);
            }
            if (_reader.IsDBNull(21) != true)
            {
                objPacerImportData.AssetsInCase = _reader.GetString(21);
            }
            if (_reader.IsDBNull(22) != true)
            {
                objPacerImportData.CaseDisposition = _reader.GetString(22);
            }
            if (_reader.IsDBNull(23) != true)
            {
                objPacerImportData.JointDebtorDischargeDate = _reader.GetDateTime(23);
            }
            if (_reader.IsDBNull(24) != true)
            {
                objPacerImportData.JointDebtorDismissalDate = _reader.GetDateTime(24);
            }
            if (_reader.IsDBNull(25) != true)
            {
                objPacerImportData.JointDebtorDispositionCode = _reader.GetString(25);
            }
            if (_reader.IsDBNull(26) != true)
            {
                objPacerImportData.JointDebtorDisposition = _reader.GetString(26);
            }
            if (_reader.IsDBNull(27) != true)
            {
                objPacerImportData.DivisionalOfficeFiled = _reader.GetString(27);
            }
            if (_reader.IsDBNull(28) != true)
            {
                objPacerImportData.AdversaryProceedingSeqNo = _reader.GetString(28);
            }
            if (_reader.IsDBNull(29) != true)
            {
                objPacerImportData.AdversaryProceedingYear = _reader.GetString(29);
            }
            if (_reader.IsDBNull(30) != true)
            {
                objPacerImportData.AdversaryProceedingDisposition = _reader.GetString(30);
            }
            if (_reader.IsDBNull(31) != true)
            {
                objPacerImportData.PartyFirstName = _reader.GetString(31);
            }
            if (_reader.IsDBNull(32) != true)
            {
                objPacerImportData.PartyMiddleName = _reader.GetString(32);
            }
            if (_reader.IsDBNull(33) != true)
            {
                objPacerImportData.PartyLastName = _reader.GetString(33);
            }
            if (_reader.IsDBNull(34) != true)
            {
                objPacerImportData.PartyGeneration = _reader.GetString(34);
            }
            if (_reader.IsDBNull(35) != true)
            {
                objPacerImportData.PartyOffice = _reader.GetString(35);
            }
            if (_reader.IsDBNull(36) != true)
            {
                objPacerImportData.PartyAddressLine1 = _reader.GetString(36);
            }
            if (_reader.IsDBNull(37) != true)
            {
                objPacerImportData.PartyAddressLine2 = _reader.GetString(37);
            }
            if (_reader.IsDBNull(38) != true)
            {
                objPacerImportData.PartyAddressLine3 = _reader.GetString(38);
            }
            if (_reader.IsDBNull(39) != true)
            {
                objPacerImportData.PartyCity = _reader.GetString(39);
            }
            if (_reader.IsDBNull(40) != true)
            {
                objPacerImportData.PartyState = _reader.GetString(40);
            }
            if (_reader.IsDBNull(41) != true)
            {
                objPacerImportData.PartyZipCode = _reader.GetString(41);
            }
            if (_reader.IsDBNull(42) != true)
            {
                objPacerImportData.PartyCountry = _reader.GetString(42);
            }
            if (_reader.IsDBNull(43) != true)
            {
                objPacerImportData.PartyPhone = _reader.GetString(43);
            }
            if (_reader.IsDBNull(44) != true)
            {
                objPacerImportData.PartySSNo = _reader.GetString(44);
            }
            if (_reader.IsDBNull(45) != true)
            {
                objPacerImportData.PartyTaxID = _reader.GetString(45);
            }
            if (_reader.IsDBNull(46) != true)
            {
                objPacerImportData.PartyCaseSeqNo = _reader.GetInt32(46);
            }
            if (_reader.IsDBNull(47) != true)
            {
                objPacerImportData.PartyCaseRole = _reader.GetString(47);
            }
            if (_reader.IsDBNull(48) != true)
            {
                objPacerImportData.PartyTerminatedDate = _reader.GetDateTime(48);
            }
            if (_reader.IsDBNull(49) != true)
            {
                objPacerImportData.CaseTitle = _reader.GetString(49);
            }
            if (_reader.IsDBNull(50) != true)
            {
                objPacerImportData.AttorneyFirstName = _reader.GetString(50);
            }
            if (_reader.IsDBNull(51) != true)
            {
                objPacerImportData.AttorneyMiddleName = _reader.GetString(51);
            }
            if (_reader.IsDBNull(52) != true)
            {
                objPacerImportData.AttorneyLastName = _reader.GetString(52);
            }
            if (_reader.IsDBNull(53) != true)
            {
                objPacerImportData.AttorneyGeneration = _reader.GetString(53);
            }
            if (_reader.IsDBNull(54) != true)
            {
                objPacerImportData.AttorneyOffice = _reader.GetString(54);
            }
            if (_reader.IsDBNull(55) != true)
            {
                objPacerImportData.AttorneyOfficeLine1 = _reader.GetString(55);
            }
            if (_reader.IsDBNull(56) != true)
            {
                objPacerImportData.AttorneyOfficeLine2 = _reader.GetString(56);
            }
            if (_reader.IsDBNull(57) != true)
            {
                objPacerImportData.AttorneyOfficeLine3 = _reader.GetString(57);
            }
            if (_reader.IsDBNull(58) != true)
            {
                objPacerImportData.AttorneyCity = _reader.GetString(58);
            }
            if (_reader.IsDBNull(59) != true)
            {
                objPacerImportData.AttorneyState = _reader.GetString(59);
            }
            if (_reader.IsDBNull(60) != true)
            {
                objPacerImportData.AttorneyZipCode = _reader.GetString(60);
            }
            if (_reader.IsDBNull(61) != true)
            {
                objPacerImportData.AttorneyCountry = _reader.GetString(61);
            }
            if (_reader.IsDBNull(62) != true)
            {
                objPacerImportData.AttorneyPhone = _reader.GetString(62);
            }
            if (_reader.IsDBNull(63) != true)
            {
                objPacerImportData.CourtID = _reader.GetInt32(63);
            }
            if (_reader.IsDBNull(64) != true)
            {
                objPacerImportData.DischargedCases = _reader.GetBoolean(64);
            }
            if (_reader.IsDBNull(65) != true)
            {
                objPacerImportData.CourtName = _reader.GetString(65);
            }
        }
        static public List<PacerImportData> GetForImportTransaction(int PacerImportTransactionID)
        {
            List<PacerImportData> objPacerImportDatas = new List<PacerImportData>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " where PacerImportTransactionID = " + PacerImportTransactionID + " order by CaseNumber4Digit, PartyAddressLine1, PartyLastName asc "))
            {
                while (reader.Read())
                {
                    PacerImportData objPacerImportData = new PacerImportData();
                    LoadPacerImportData(objPacerImportData, reader);
                    objPacerImportData.IsModified = false;
                    objPacerImportDatas.Add(objPacerImportData);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerImportDatas;
            }
        }
        static public void Save(PacerImportData objPacerImportData)
        {
            if (objPacerImportData.IsModified == true)
            {
                if (objPacerImportData.ID == 0)
                {
                    Insert(objPacerImportData);
                }
                else
                {
                    Update(objPacerImportData);
                }
            }
        }
        static private void Insert(PacerImportData objPacerImportData)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "Imported", DbType.Boolean, objPacerImportData.Imported);
            if (objPacerImportData.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportData.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerImportData.CaseNumber2Digit);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerImportData.CaseNumber4Digit);
            db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerImportData.CMECF);
            db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerImportData.Chapter);
            db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerImportData.PreviousChapter);
            if (objPacerImportData.ConversionDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerImportData.ConversionDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DischargedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerImportData.DischargedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DismissedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerImportData.DismissedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerImportData.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.EnteredDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerImportData.EnteredDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.ClosedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerImportData.ClosedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerImportData.Disposition);
            db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerImportData.CaseType);
            db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerImportData.TrusteeLastName);
            db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerImportData.JudgeLastName);
            db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerImportData.CountyFiled);
            db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerImportData.OfficeName);
            db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerImportData.FilingFeePaymentStatus);
            db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerImportData.AssetsInCase);
            db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerImportData.CaseDisposition);
            if (objPacerImportData.JointDebtorDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerImportData.JointDebtorDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.JointDebtorDismissalDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerImportData.JointDebtorDismissalDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerImportData.JointDebtorDispositionCode);
            db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerImportData.JointDebtorDisposition);
            db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerImportData.DivisionalOfficeFiled);
            db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerImportData.AdversaryProceedingSeqNo);
            db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerImportData.AdversaryProceedingYear);
            db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerImportData.AdversaryProceedingDisposition);
            db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerImportData.PartyFirstName);
            db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerImportData.PartyMiddleName);
            db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerImportData.PartyLastName);
            db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerImportData.PartyGeneration);
            db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerImportData.PartyOffice);
            db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerImportData.PartyAddressLine1);
            db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerImportData.PartyAddressLine2);
            db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerImportData.PartyAddressLine3);
            db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerImportData.PartyCity);
            db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerImportData.PartyState);
            db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerImportData.PartyZipCode);
            db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerImportData.PartyCountry);
            db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerImportData.PartyPhone);
            db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerImportData.PartySSNo);
            db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, Convert.DBNull);
            db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerImportData.PartyCaseSeqNo);
            db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerImportData.PartyCaseRole);
            if (objPacerImportData.PartyTerminatedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerImportData.PartyTerminatedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerImportData.CaseTitle);
            db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerImportData.AttorneyFirstName);
            db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerImportData.AttorneyMiddleName);
            db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerImportData.AttorneyLastName);
            db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerImportData.AttorneyGeneration);
            db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerImportData.AttorneyOffice);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerImportData.AttorneyOfficeLine1);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerImportData.AttorneyOfficeLine2);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerImportData.AttorneyOfficeLine3);
            db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerImportData.AttorneyCity);
            db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerImportData.AttorneyState);
            db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerImportData.AttorneyZipCode);
            db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerImportData.AttorneyCountry);
            db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerImportData.AttorneyPhone);
            db.ExecuteNonQuery(dbCommand);
            objPacerImportData.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(PacerImportData objPacerImportData)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportData.ID);
            db.AddInParameter(dbCommand, "Imported", DbType.Boolean, objPacerImportData.Imported);
            if (objPacerImportData.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportData.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerImportData.CaseNumber2Digit);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerImportData.CaseNumber4Digit);
            db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerImportData.CMECF);
            db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerImportData.Chapter);
            db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerImportData.PreviousChapter);
            if (objPacerImportData.ConversionDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerImportData.ConversionDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DischargedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerImportData.DischargedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DismissedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerImportData.DismissedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerImportData.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.EnteredDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerImportData.EnteredDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.ClosedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerImportData.ClosedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerImportData.Disposition);
            db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerImportData.CaseType);
            db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerImportData.TrusteeLastName);
            db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerImportData.JudgeLastName);
            db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerImportData.CountyFiled);
            db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerImportData.OfficeName);
            db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerImportData.FilingFeePaymentStatus);
            db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerImportData.AssetsInCase);
            db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerImportData.CaseDisposition);
            if (objPacerImportData.JointDebtorDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerImportData.JointDebtorDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.JointDebtorDismissalDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerImportData.JointDebtorDismissalDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerImportData.JointDebtorDispositionCode);
            db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerImportData.JointDebtorDisposition);
            db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerImportData.DivisionalOfficeFiled);
            db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerImportData.AdversaryProceedingSeqNo);
            db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerImportData.AdversaryProceedingYear);
            db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerImportData.AdversaryProceedingDisposition);
            db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerImportData.PartyFirstName);
            db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerImportData.PartyMiddleName);
            db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerImportData.PartyLastName);
            db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerImportData.PartyGeneration);
            db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerImportData.PartyOffice);
            db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerImportData.PartyAddressLine1);
            db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerImportData.PartyAddressLine2);
            db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerImportData.PartyAddressLine3);
            db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerImportData.PartyCity);
            db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerImportData.PartyState);
            db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerImportData.PartyZipCode);
            db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerImportData.PartyCountry);
            db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerImportData.PartyPhone);
            db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerImportData.PartySSNo);
            db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, Convert.DBNull);
            db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerImportData.PartyCaseSeqNo);
            db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerImportData.PartyCaseRole);
            if (objPacerImportData.PartyTerminatedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerImportData.PartyTerminatedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerImportData.CaseTitle);
            db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerImportData.AttorneyFirstName);
            db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerImportData.AttorneyMiddleName);
            db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerImportData.AttorneyLastName);
            db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerImportData.AttorneyGeneration);
            db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerImportData.AttorneyOffice);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerImportData.AttorneyOfficeLine1);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerImportData.AttorneyOfficeLine2);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerImportData.AttorneyOfficeLine3);
            db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerImportData.AttorneyCity);
            db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerImportData.AttorneyState);
            db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerImportData.AttorneyZipCode);
            db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerImportData.AttorneyCountry);
            db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerImportData.AttorneyPhone);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void Delete(PacerImportData objPacerImportData)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataDelete");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportData.ID);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(PacerImportData objPacerImportData, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objPacerImportData.IsModified == true)
                {
                    if (objPacerImportData.ID == 0)
                    {
                        Insert(objPacerImportData, db, trans);
                    }
                    else
                    {
                        Update(objPacerImportData, db, trans);
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
        static private void Insert(PacerImportData objPacerImportData, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "Imported", DbType.Boolean, objPacerImportData.Imported);
            if (objPacerImportData.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportData.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerImportData.CaseNumber2Digit);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerImportData.CaseNumber4Digit);
            db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerImportData.CMECF);
            db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerImportData.Chapter);
            db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerImportData.PreviousChapter);
            if (objPacerImportData.ConversionDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerImportData.ConversionDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DischargedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerImportData.DischargedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DismissedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerImportData.DismissedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerImportData.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.EnteredDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerImportData.EnteredDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.ClosedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerImportData.ClosedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerImportData.Disposition);
            db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerImportData.CaseType);
            db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerImportData.TrusteeLastName);
            db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerImportData.JudgeLastName);
            db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerImportData.CountyFiled);
            db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerImportData.OfficeName);
            db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerImportData.FilingFeePaymentStatus);
            db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerImportData.AssetsInCase);
            db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerImportData.CaseDisposition);
            if (objPacerImportData.JointDebtorDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerImportData.JointDebtorDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.JointDebtorDismissalDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerImportData.JointDebtorDismissalDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerImportData.JointDebtorDispositionCode);
            db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerImportData.JointDebtorDisposition);
            db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerImportData.DivisionalOfficeFiled);
            db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerImportData.AdversaryProceedingSeqNo);
            db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerImportData.AdversaryProceedingYear);
            db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerImportData.AdversaryProceedingDisposition);
            db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerImportData.PartyFirstName);
            db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerImportData.PartyMiddleName);
            db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerImportData.PartyLastName);
            db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerImportData.PartyGeneration);
            db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerImportData.PartyOffice);
            db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerImportData.PartyAddressLine1);
            db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerImportData.PartyAddressLine2);
            db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerImportData.PartyAddressLine3);
            db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerImportData.PartyCity);
            db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerImportData.PartyState);
            db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerImportData.PartyZipCode);
            db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerImportData.PartyCountry);
            db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerImportData.PartyPhone);
            db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerImportData.PartySSNo);
            db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, objPacerImportData.PartyTaxID);
            db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerImportData.PartyCaseSeqNo);
            db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerImportData.PartyCaseRole);
            if (objPacerImportData.PartyTerminatedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerImportData.PartyTerminatedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerImportData.CaseTitle);
            db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerImportData.AttorneyFirstName);
            db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerImportData.AttorneyMiddleName);
            db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerImportData.AttorneyLastName);
            db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerImportData.AttorneyGeneration);
            db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerImportData.AttorneyOffice);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerImportData.AttorneyOfficeLine1);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerImportData.AttorneyOfficeLine2);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerImportData.AttorneyOfficeLine3);
            db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerImportData.AttorneyCity);
            db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerImportData.AttorneyState);
            db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerImportData.AttorneyZipCode);
            db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerImportData.AttorneyCountry);
            db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerImportData.AttorneyPhone);
            db.ExecuteNonQuery(dbCommand, trans);
            objPacerImportData.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(PacerImportData objPacerImportData, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportData.ID);
            db.AddInParameter(dbCommand, "Imported", DbType.Boolean, objPacerImportData.Imported);
            if (objPacerImportData.PacerImportTransactionID > 0)
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportData.PacerImportTransactionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerImportData.CaseNumber2Digit);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerImportData.CaseNumber4Digit);
            db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerImportData.CMECF);
            db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerImportData.Chapter);
            db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerImportData.PreviousChapter);
            if (objPacerImportData.ConversionDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerImportData.ConversionDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DischargedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerImportData.DischargedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.DismissedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerImportData.DismissedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.FiledDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerImportData.FiledDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.EnteredDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerImportData.EnteredDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.ClosedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerImportData.ClosedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerImportData.Disposition);
            db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerImportData.CaseType);
            db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerImportData.TrusteeLastName);
            db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerImportData.JudgeLastName);
            db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerImportData.CountyFiled);
            db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerImportData.OfficeName);
            db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerImportData.FilingFeePaymentStatus);
            db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerImportData.AssetsInCase);
            db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerImportData.CaseDisposition);
            if (objPacerImportData.JointDebtorDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerImportData.JointDebtorDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportData.JointDebtorDismissalDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerImportData.JointDebtorDismissalDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerImportData.JointDebtorDispositionCode);
            db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerImportData.JointDebtorDisposition);
            db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerImportData.DivisionalOfficeFiled);
            db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerImportData.AdversaryProceedingSeqNo);
            db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerImportData.AdversaryProceedingYear);
            db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerImportData.AdversaryProceedingDisposition);
            db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerImportData.PartyFirstName);
            db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerImportData.PartyMiddleName);
            db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerImportData.PartyLastName);
            db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerImportData.PartyGeneration);
            db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerImportData.PartyOffice);
            db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerImportData.PartyAddressLine1);
            db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerImportData.PartyAddressLine2);
            db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerImportData.PartyAddressLine3);
            db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerImportData.PartyCity);
            db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerImportData.PartyState);
            db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerImportData.PartyZipCode);
            db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerImportData.PartyCountry);
            db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerImportData.PartyPhone);
            db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerImportData.PartySSNo);
            db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, objPacerImportData.PartyTaxID);
            db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerImportData.PartyCaseSeqNo);
            db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerImportData.PartyCaseRole);
            if (objPacerImportData.PartyTerminatedDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerImportData.PartyTerminatedDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerImportData.CaseTitle);
            db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerImportData.AttorneyFirstName);
            db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerImportData.AttorneyMiddleName);
            db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerImportData.AttorneyLastName);
            db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerImportData.AttorneyGeneration);
            db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerImportData.AttorneyOffice);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerImportData.AttorneyOfficeLine1);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerImportData.AttorneyOfficeLine2);
            db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerImportData.AttorneyOfficeLine3);
            db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerImportData.AttorneyCity);
            db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerImportData.AttorneyState);
            db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerImportData.AttorneyZipCode);
            db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerImportData.AttorneyCountry);
            db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerImportData.AttorneyPhone);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion

        static public void DeleteForTransaction(int PacerImportTransactionID )
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportDataDeleteForTransaction");
            db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, PacerImportTransactionID);
            db.ExecuteNonQuery(dbCommand);
        }

	}
}
