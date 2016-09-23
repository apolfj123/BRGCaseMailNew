using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the PacerTempRawImportData table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/17/2011 9:22:29 AM
	/// </summary>
	public class PacerTempRawImportDataService 
	{
		static private string _selectViewSQL  = "Select CaseNumber2Digit, CaseNumber4Digit, CMECF, Chapter, PreviousChapter, ConversionDate, DischargedDate, DismissedDate, FiledDate, EnteredDate, ClosedDate, Disposition, CaseType, TrusteeLastName, JudgeLastName, CountyFiled, OfficeName, FilingFeePaymentStatus, AssetsInCase, CaseDisposition, JointDebtorDischargeDate, JointDebtorDismissalDate, JointDebtorDispositionCode, JointDebtorDisposition, DivisionalOfficeFiled, AdversaryProceedingSeqNo, AdversaryProceedingYear, AdversaryProceedingDisposition, PartyFirstName, PartyMiddleName, PartyLastName, PartyGeneration, PartyOffice, PartyAddressLine1, PartyAddressLine2, PartyAddressLine3, PartyCity, PartyState, PartyZipCode, PartyCountry, PartyPhone, PartySSNo, PartyTaxID, PartyCaseSeqNo, PartyCaseRole, PartyTerminatedDate, CaseTitle, AttorneyFirstName, AttorneyMiddleName, AttorneyLastName, AttorneyGeneration, AttorneyOffice, AttorneyOfficeLine1, AttorneyOfficeLine2, AttorneyOfficeLine3, AttorneyCity, AttorneyState, AttorneyZipCode, AttorneyCountry, AttorneyPhone, nullcolumn from PacerTempRawImportData";
		static public  List<PacerTempRawImportData> GetAll()
		{
			List<PacerTempRawImportData> objPacerTempRawImportDatas = new List<PacerTempRawImportData>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					PacerTempRawImportData objPacerTempRawImportData = new PacerTempRawImportData();
					LoadPacerTempRawImportData(objPacerTempRawImportData, reader);
					objPacerTempRawImportData.IsModified = false;
					objPacerTempRawImportDatas.Add (objPacerTempRawImportData);
				}
				// always call Close when done reading.
				reader.Close();
				return objPacerTempRawImportDatas;
			}
		}
		static public PacerTempRawImportData GetByID(int ID)
		{
			PacerTempRawImportData objPacerTempRawImportData = new PacerTempRawImportData();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadPacerTempRawImportData(objPacerTempRawImportData, reader);
					// always call Close when done reading.
					reader.Close();
					objPacerTempRawImportData.IsModified = false;
					return objPacerTempRawImportData;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadPacerTempRawImportData(PacerTempRawImportData objPacerTempRawImportData, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objPacerTempRawImportData.CaseNumber2Digit = _reader.GetString(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objPacerTempRawImportData.CaseNumber4Digit = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objPacerTempRawImportData.CMECF = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objPacerTempRawImportData.Chapter = _reader.GetInt32(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objPacerTempRawImportData.PreviousChapter = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objPacerTempRawImportData.ConversionDate = _reader.GetDateTime(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objPacerTempRawImportData.DischargedDate = _reader.GetDateTime(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objPacerTempRawImportData.DismissedDate = _reader.GetDateTime(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objPacerTempRawImportData.FiledDate = _reader.GetDateTime(8);
			}
			if (_reader.IsDBNull(9) != true)
			{
				objPacerTempRawImportData.EnteredDate = _reader.GetDateTime(9);
			}
			if (_reader.IsDBNull(10) != true)
			{
				objPacerTempRawImportData.ClosedDate = _reader.GetDateTime(10);
			}
			if (_reader.IsDBNull(11) != true)
			{
				objPacerTempRawImportData.Disposition = _reader.GetString(11);
			}
			if (_reader.IsDBNull(12) != true)
			{
				objPacerTempRawImportData.CaseType = _reader.GetString(12);
			}
			if (_reader.IsDBNull(13) != true)
			{
				objPacerTempRawImportData.TrusteeLastName = _reader.GetString(13);
			}
			if (_reader.IsDBNull(14) != true)
			{
				objPacerTempRawImportData.JudgeLastName = _reader.GetString(14);
			}
			if (_reader.IsDBNull(15) != true)
			{
				objPacerTempRawImportData.CountyFiled = _reader.GetString(15);
			}
			if (_reader.IsDBNull(16) != true)
			{
				objPacerTempRawImportData.OfficeName = _reader.GetString(16);
			}
			if (_reader.IsDBNull(17) != true)
			{
				objPacerTempRawImportData.FilingFeePaymentStatus = _reader.GetString(17);
			}
			if (_reader.IsDBNull(18) != true)
			{
				objPacerTempRawImportData.AssetsInCase = _reader.GetString(18);
			}
			if (_reader.IsDBNull(19) != true)
			{
				objPacerTempRawImportData.CaseDisposition = _reader.GetString(19);
			}
			if (_reader.IsDBNull(20) != true)
			{
				objPacerTempRawImportData.JointDebtorDischargeDate = _reader.GetDateTime(20);
			}
			if (_reader.IsDBNull(21) != true)
			{
				objPacerTempRawImportData.JointDebtorDismissalDate = _reader.GetDateTime(21);
			}
			if (_reader.IsDBNull(22) != true)
			{
				objPacerTempRawImportData.JointDebtorDispositionCode = _reader.GetString(22);
			}
			if (_reader.IsDBNull(23) != true)
			{
				objPacerTempRawImportData.JointDebtorDisposition = _reader.GetString(23);
			}
			if (_reader.IsDBNull(24) != true)
			{
				objPacerTempRawImportData.DivisionalOfficeFiled = _reader.GetString(24);
			}
			if (_reader.IsDBNull(25) != true)
			{
				objPacerTempRawImportData.AdversaryProceedingSeqNo = _reader.GetString(25);
			}
			if (_reader.IsDBNull(26) != true)
			{
				objPacerTempRawImportData.AdversaryProceedingYear = _reader.GetString(26);
			}
			if (_reader.IsDBNull(27) != true)
			{
				objPacerTempRawImportData.AdversaryProceedingDisposition = _reader.GetString(27);
			}
			if (_reader.IsDBNull(28) != true)
			{
				objPacerTempRawImportData.PartyFirstName = _reader.GetString(28);
			}
			if (_reader.IsDBNull(29) != true)
			{
				objPacerTempRawImportData.PartyMiddleName = _reader.GetString(29);
			}
			if (_reader.IsDBNull(30) != true)
			{
				objPacerTempRawImportData.PartyLastName = _reader.GetString(30);
			}
			if (_reader.IsDBNull(31) != true)
			{
				objPacerTempRawImportData.PartyGeneration = _reader.GetString(31);
			}
			if (_reader.IsDBNull(32) != true)
			{
				objPacerTempRawImportData.PartyOffice = _reader.GetString(32);
			}
			if (_reader.IsDBNull(33) != true)
			{
				objPacerTempRawImportData.PartyAddressLine1 = _reader.GetString(33);
			}
			if (_reader.IsDBNull(34) != true)
			{
				objPacerTempRawImportData.PartyAddressLine2 = _reader.GetString(34);
			}
			if (_reader.IsDBNull(35) != true)
			{
				objPacerTempRawImportData.PartyAddressLine3 = _reader.GetString(35);
			}
			if (_reader.IsDBNull(36) != true)
			{
				objPacerTempRawImportData.PartyCity = _reader.GetString(36);
			}
			if (_reader.IsDBNull(37) != true)
			{
				objPacerTempRawImportData.PartyState = _reader.GetString(37);
			}
			if (_reader.IsDBNull(38) != true)
			{
				objPacerTempRawImportData.PartyZipCode = _reader.GetString(38);
			}
			if (_reader.IsDBNull(39) != true)
			{
				objPacerTempRawImportData.PartyCountry = _reader.GetString(39);
			}
			if (_reader.IsDBNull(40) != true)
			{
				objPacerTempRawImportData.PartyPhone = _reader.GetString(40);
			}
			if (_reader.IsDBNull(41) != true)
			{
				objPacerTempRawImportData.PartySSNo = _reader.GetString(41);
			}
			if (_reader.IsDBNull(42) != true)
			{
				objPacerTempRawImportData.PartyTaxID = _reader.GetString(42);
			}
			if (_reader.IsDBNull(43) != true)
			{
				objPacerTempRawImportData.PartyCaseSeqNo = _reader.GetInt32(43);
			}
			if (_reader.IsDBNull(44) != true)
			{
				objPacerTempRawImportData.PartyCaseRole = _reader.GetString(44);
			}
			if (_reader.IsDBNull(45) != true)
			{
				objPacerTempRawImportData.PartyTerminatedDate = _reader.GetDateTime(45);
			}
			if (_reader.IsDBNull(46) != true)
			{
				objPacerTempRawImportData.CaseTitle = _reader.GetString(46);
			}
			if (_reader.IsDBNull(47) != true)
			{
				objPacerTempRawImportData.AttorneyFirstName = _reader.GetString(47);
			}
			if (_reader.IsDBNull(48) != true)
			{
				objPacerTempRawImportData.AttorneyMiddleName = _reader.GetString(48);
			}
			if (_reader.IsDBNull(49) != true)
			{
				objPacerTempRawImportData.AttorneyLastName = _reader.GetString(49);
			}
			if (_reader.IsDBNull(50) != true)
			{
				objPacerTempRawImportData.AttorneyGeneration = _reader.GetString(50);
			}
			if (_reader.IsDBNull(51) != true)
			{
				objPacerTempRawImportData.AttorneyOffice = _reader.GetString(51);
			}
			if (_reader.IsDBNull(52) != true)
			{
				objPacerTempRawImportData.AttorneyOfficeLine1 = _reader.GetString(52);
			}
			if (_reader.IsDBNull(53) != true)
			{
				objPacerTempRawImportData.AttorneyOfficeLine2 = _reader.GetString(53);
			}
			if (_reader.IsDBNull(54) != true)
			{
				objPacerTempRawImportData.AttorneyOfficeLine3 = _reader.GetString(54);
			}
			if (_reader.IsDBNull(55) != true)
			{
				objPacerTempRawImportData.AttorneyCity = _reader.GetString(55);
			}
			if (_reader.IsDBNull(56) != true)
			{
				objPacerTempRawImportData.AttorneyState = _reader.GetString(56);
			}
			if (_reader.IsDBNull(57) != true)
			{
				objPacerTempRawImportData.AttorneyZipCode = _reader.GetString(57);
			}
			if (_reader.IsDBNull(58) != true)
			{
				objPacerTempRawImportData.AttorneyCountry = _reader.GetString(58);
			}
			if (_reader.IsDBNull(59) != true)
			{
				objPacerTempRawImportData.AttorneyPhone = _reader.GetString(59);
			}
			if (_reader.IsDBNull(60) != true)
			{
				objPacerTempRawImportData.nullcolumn = _reader.GetString(60);
			}
		}
        //static public void Save(PacerTempRawImportData objPacerTempRawImportData)
        //{
        //    if (objPacerTempRawImportData.IsModified == true)
        //    {
        //        if (objPacerTempRawImportData.ID == 0 )
        //        {
        //            Insert(objPacerTempRawImportData);
        //        }
        //        else
        //        {
        //            Update(objPacerTempRawImportData);
        //        }
        //    }
        //}
		static public void Insert(PacerTempRawImportData objPacerTempRawImportData)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerTempRawImportDataInsert");
				db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerTempRawImportData.CaseNumber2Digit);
				db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerTempRawImportData.CaseNumber4Digit);
				db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerTempRawImportData.CMECF);
				db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerTempRawImportData.Chapter);
				db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerTempRawImportData.PreviousChapter);
			if (objPacerTempRawImportData.ConversionDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerTempRawImportData.ConversionDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.DischargedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerTempRawImportData.DischargedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.DismissedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerTempRawImportData.DismissedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.FiledDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerTempRawImportData.FiledDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.EnteredDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerTempRawImportData.EnteredDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.ClosedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerTempRawImportData.ClosedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerTempRawImportData.Disposition);
				db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerTempRawImportData.CaseType);
				db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerTempRawImportData.TrusteeLastName);
				db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerTempRawImportData.JudgeLastName);
				db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerTempRawImportData.CountyFiled);
				db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerTempRawImportData.OfficeName);
				db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerTempRawImportData.FilingFeePaymentStatus);
				db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerTempRawImportData.AssetsInCase);
				db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerTempRawImportData.CaseDisposition);
			if (objPacerTempRawImportData.JointDebtorDischargeDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDischargeDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.JointDebtorDismissalDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDismissalDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerTempRawImportData.JointDebtorDispositionCode);
				db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerTempRawImportData.JointDebtorDisposition);
				db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerTempRawImportData.DivisionalOfficeFiled);
				db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerTempRawImportData.AdversaryProceedingSeqNo);
				db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerTempRawImportData.AdversaryProceedingYear);
				db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerTempRawImportData.AdversaryProceedingDisposition);
				db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerTempRawImportData.PartyFirstName);
				db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerTempRawImportData.PartyMiddleName);
				db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerTempRawImportData.PartyLastName);
				db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerTempRawImportData.PartyGeneration);
				db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerTempRawImportData.PartyOffice);
				db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerTempRawImportData.PartyAddressLine1);
				db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerTempRawImportData.PartyAddressLine2);
				db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerTempRawImportData.PartyAddressLine3);
				db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerTempRawImportData.PartyCity);
				db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerTempRawImportData.PartyState);
				db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerTempRawImportData.PartyZipCode);
				db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerTempRawImportData.PartyCountry);
				db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerTempRawImportData.PartyPhone);
				db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerTempRawImportData.PartySSNo);
				db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, objPacerTempRawImportData.PartyTaxID);
				db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerTempRawImportData.PartyCaseSeqNo);
				db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerTempRawImportData.PartyCaseRole);
			if (objPacerTempRawImportData.PartyTerminatedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerTempRawImportData.PartyTerminatedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerTempRawImportData.CaseTitle);
				db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerTempRawImportData.AttorneyFirstName);
				db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerTempRawImportData.AttorneyMiddleName);
				db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerTempRawImportData.AttorneyLastName);
				db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerTempRawImportData.AttorneyGeneration);
				db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerTempRawImportData.AttorneyOffice);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine1);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine2);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine3);
				db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerTempRawImportData.AttorneyCity);
				db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerTempRawImportData.AttorneyState);
				db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerTempRawImportData.AttorneyZipCode);
				db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerTempRawImportData.AttorneyCountry);
				db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerTempRawImportData.AttorneyPhone);
				db.AddInParameter(dbCommand, "nullcolumn", DbType.String, objPacerTempRawImportData.nullcolumn);
			db.ExecuteNonQuery(dbCommand);
			//objPacerTempRawImportData.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
        //static private void Update(PacerTempRawImportData objPacerTempRawImportData)
        //{
			
        //    Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
        //    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerTempRawImportDataUpdate");
        //    db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerTempRawImportData.CaseNumber2Digit);
        //    db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerTempRawImportData.CaseNumber4Digit);
        //    db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerTempRawImportData.CMECF);
        //    db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerTempRawImportData.Chapter);
        //    db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerTempRawImportData.PreviousChapter);
        //    if (objPacerTempRawImportData.ConversionDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerTempRawImportData.ConversionDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.DischargedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerTempRawImportData.DischargedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.DismissedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerTempRawImportData.DismissedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.FiledDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerTempRawImportData.FiledDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.EnteredDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerTempRawImportData.EnteredDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.ClosedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerTempRawImportData.ClosedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerTempRawImportData.Disposition);
        //    db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerTempRawImportData.CaseType);
        //    db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerTempRawImportData.TrusteeLastName);
        //    db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerTempRawImportData.JudgeLastName);
        //    db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerTempRawImportData.CountyFiled);
        //    db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerTempRawImportData.OfficeName);
        //    db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerTempRawImportData.FilingFeePaymentStatus);
        //    db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerTempRawImportData.AssetsInCase);
        //    db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerTempRawImportData.CaseDisposition);
        //    if (objPacerTempRawImportData.JointDebtorDischargeDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDischargeDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.JointDebtorDismissalDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDismissalDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerTempRawImportData.JointDebtorDispositionCode);
        //    db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerTempRawImportData.JointDebtorDisposition);
        //    db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerTempRawImportData.DivisionalOfficeFiled);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerTempRawImportData.AdversaryProceedingSeqNo);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerTempRawImportData.AdversaryProceedingYear);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerTempRawImportData.AdversaryProceedingDisposition);
        //    db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerTempRawImportData.PartyFirstName);
        //    db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerTempRawImportData.PartyMiddleName);
        //    db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerTempRawImportData.PartyLastName);
        //    db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerTempRawImportData.PartyGeneration);
        //    db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerTempRawImportData.PartyOffice);
        //    db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerTempRawImportData.PartyAddressLine1);
        //    db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerTempRawImportData.PartyAddressLine2);
        //    db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerTempRawImportData.PartyAddressLine3);
        //    db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerTempRawImportData.PartyCity);
        //    db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerTempRawImportData.PartyState);
        //    db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerTempRawImportData.PartyZipCode);
        //    db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerTempRawImportData.PartyCountry);
        //    db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerTempRawImportData.PartyPhone);
        //    db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerTempRawImportData.PartySSNo);
        //    if (objPacerTempRawImportData.PartyTaxID > 0)
        //    {
        //        db.AddInParameter(dbCommand, "PartyTaxID", DbType.Int32, objPacerTempRawImportData.PartyTaxID);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "PartyTaxID", DbType.Int32, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerTempRawImportData.PartyCaseSeqNo);
        //    db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerTempRawImportData.PartyCaseRole);
        //    if (objPacerTempRawImportData.PartyTerminatedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerTempRawImportData.PartyTerminatedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerTempRawImportData.CaseTitle);
        //    db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerTempRawImportData.AttorneyFirstName);
        //    db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerTempRawImportData.AttorneyMiddleName);
        //    db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerTempRawImportData.AttorneyLastName);
        //    db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerTempRawImportData.AttorneyGeneration);
        //    db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerTempRawImportData.AttorneyOffice);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine1);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine2);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine3);
        //    db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerTempRawImportData.AttorneyCity);
        //    db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerTempRawImportData.AttorneyState);
        //    db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerTempRawImportData.AttorneyZipCode);
        //    db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerTempRawImportData.AttorneyCountry);
        //    db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerTempRawImportData.AttorneyPhone);
        //    db.AddInParameter(dbCommand, "nullcolumn", DbType.String, objPacerTempRawImportData.nullcolumn);
        //    db.ExecuteNonQuery(dbCommand);
        //}
#region save, insert, update using transactions
        //static public void Save(PacerTempRawImportData objPacerTempRawImportData, Database db, DbTransaction trans, bool CommitTrans)
        //{
        //    try
        //    {
        //        if (objPacerTempRawImportData.IsModified == true)
        //        {
        //            if (objPacerTempRawImportData.ID == 0 )
        //            {
        //                Insert(objPacerTempRawImportData, db, trans);
        //            }
        //            else
        //            {
        //                Update(objPacerTempRawImportData, db, trans);
        //            }
        //        }
        //        if (CommitTrans == true)
        //        {
        //            trans.Commit();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (CommitTrans == true)
        //        {
        //            //we were hoping to commit the transaction which means we're at the end so
        //            //Roll back the transaction. Otherwise just rethrow the error and let the next
        //            //higher level catch it and rollback
        //            trans.Rollback();
        //        }
        //    //and then rethrow the error
        //    throw (e);
        //    }
        //}
		static public void Insert(PacerTempRawImportData objPacerTempRawImportData, Database db, DbTransaction trans)
		{
			
			//Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerTempRawImportDataInsert");
				db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerTempRawImportData.CaseNumber2Digit);
				db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerTempRawImportData.CaseNumber4Digit);
				db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerTempRawImportData.CMECF);
				db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerTempRawImportData.Chapter);
				db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerTempRawImportData.PreviousChapter);
			if (objPacerTempRawImportData.ConversionDate > DateTime.MinValue)
			{
                try
                {
                    db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerTempRawImportData.ConversionDate);
                }
                catch { /* do nothing igonre it */}
			}
			else
			{
				db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.DischargedDate > DateTime.MinValue)
			{
                try
                {
                    db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerTempRawImportData.DischargedDate);
                }
                catch { /* do nothing igonre it */}
			}
			else
			{
				db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.DismissedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerTempRawImportData.DismissedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.FiledDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerTempRawImportData.FiledDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.EnteredDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerTempRawImportData.EnteredDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.ClosedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerTempRawImportData.ClosedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerTempRawImportData.Disposition);
				db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerTempRawImportData.CaseType);
				db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerTempRawImportData.TrusteeLastName);
				db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerTempRawImportData.JudgeLastName);
				db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerTempRawImportData.CountyFiled);
				db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerTempRawImportData.OfficeName);
				db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerTempRawImportData.FilingFeePaymentStatus);
				db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerTempRawImportData.AssetsInCase);
				db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerTempRawImportData.CaseDisposition);
			if (objPacerTempRawImportData.JointDebtorDischargeDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDischargeDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
			}
			if (objPacerTempRawImportData.JointDebtorDismissalDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDismissalDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerTempRawImportData.JointDebtorDispositionCode);
				db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerTempRawImportData.JointDebtorDisposition);
				db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerTempRawImportData.DivisionalOfficeFiled);
				db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerTempRawImportData.AdversaryProceedingSeqNo);
				db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerTempRawImportData.AdversaryProceedingYear);
				db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerTempRawImportData.AdversaryProceedingDisposition);
				db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerTempRawImportData.PartyFirstName);
				db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerTempRawImportData.PartyMiddleName);
				db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerTempRawImportData.PartyLastName);
				db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerTempRawImportData.PartyGeneration);
				db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerTempRawImportData.PartyOffice);
				db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerTempRawImportData.PartyAddressLine1);
				db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerTempRawImportData.PartyAddressLine2);
				db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerTempRawImportData.PartyAddressLine3);
				db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerTempRawImportData.PartyCity);
				db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerTempRawImportData.PartyState);
				db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerTempRawImportData.PartyZipCode);
				db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerTempRawImportData.PartyCountry);
				db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerTempRawImportData.PartyPhone);
				db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerTempRawImportData.PartySSNo);
				db.AddInParameter(dbCommand, "PartyTaxID", DbType.String, objPacerTempRawImportData.PartyTaxID);
				db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerTempRawImportData.PartyCaseSeqNo);
				db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerTempRawImportData.PartyCaseRole);
			if (objPacerTempRawImportData.PartyTerminatedDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerTempRawImportData.PartyTerminatedDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerTempRawImportData.CaseTitle);
				db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerTempRawImportData.AttorneyFirstName);
				db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerTempRawImportData.AttorneyMiddleName);
				db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerTempRawImportData.AttorneyLastName);
				db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerTempRawImportData.AttorneyGeneration);
				db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerTempRawImportData.AttorneyOffice);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine1);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine2);
				db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine3);
				db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerTempRawImportData.AttorneyCity);
				db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerTempRawImportData.AttorneyState);
				db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerTempRawImportData.AttorneyZipCode);
				db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerTempRawImportData.AttorneyCountry);
				db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerTempRawImportData.AttorneyPhone);
				db.AddInParameter(dbCommand, "nullcolumn", DbType.String, objPacerTempRawImportData.nullcolumn);
			db.ExecuteNonQuery(dbCommand, trans);
			//objPacerTempRawImportData.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
        //static private void Update(PacerTempRawImportData objPacerTempRawImportData, Database db, DbTransaction trans)
        //{
			
        //    //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
        //    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerTempRawImportDataUpdate");
        //    db.AddInParameter(dbCommand, "CaseNumber2Digit", DbType.String, objPacerTempRawImportData.CaseNumber2Digit);
        //    db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objPacerTempRawImportData.CaseNumber4Digit);
        //    db.AddInParameter(dbCommand, "CMECF", DbType.String, objPacerTempRawImportData.CMECF);
        //    db.AddInParameter(dbCommand, "Chapter", DbType.Int32, objPacerTempRawImportData.Chapter);
        //    db.AddInParameter(dbCommand, "PreviousChapter", DbType.String, objPacerTempRawImportData.PreviousChapter);
        //    if (objPacerTempRawImportData.ConversionDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, objPacerTempRawImportData.ConversionDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "ConversionDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.DischargedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, objPacerTempRawImportData.DischargedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "DischargedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.DismissedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, objPacerTempRawImportData.DismissedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "DismissedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.FiledDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, objPacerTempRawImportData.FiledDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "FiledDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.EnteredDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, objPacerTempRawImportData.EnteredDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "EnteredDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.ClosedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, objPacerTempRawImportData.ClosedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "Disposition", DbType.String, objPacerTempRawImportData.Disposition);
        //    db.AddInParameter(dbCommand, "CaseType", DbType.String, objPacerTempRawImportData.CaseType);
        //    db.AddInParameter(dbCommand, "TrusteeLastName", DbType.String, objPacerTempRawImportData.TrusteeLastName);
        //    db.AddInParameter(dbCommand, "JudgeLastName", DbType.String, objPacerTempRawImportData.JudgeLastName);
        //    db.AddInParameter(dbCommand, "CountyFiled", DbType.String, objPacerTempRawImportData.CountyFiled);
        //    db.AddInParameter(dbCommand, "OfficeName", DbType.String, objPacerTempRawImportData.OfficeName);
        //    db.AddInParameter(dbCommand, "FilingFeePaymentStatus", DbType.String, objPacerTempRawImportData.FilingFeePaymentStatus);
        //    db.AddInParameter(dbCommand, "AssetsInCase", DbType.String, objPacerTempRawImportData.AssetsInCase);
        //    db.AddInParameter(dbCommand, "CaseDisposition", DbType.String, objPacerTempRawImportData.CaseDisposition);
        //    if (objPacerTempRawImportData.JointDebtorDischargeDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDischargeDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDischargeDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    if (objPacerTempRawImportData.JointDebtorDismissalDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, objPacerTempRawImportData.JointDebtorDismissalDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "JointDebtorDismissalDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "JointDebtorDispositionCode", DbType.String, objPacerTempRawImportData.JointDebtorDispositionCode);
        //    db.AddInParameter(dbCommand, "JointDebtorDisposition", DbType.String, objPacerTempRawImportData.JointDebtorDisposition);
        //    db.AddInParameter(dbCommand, "DivisionalOfficeFiled", DbType.String, objPacerTempRawImportData.DivisionalOfficeFiled);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingSeqNo", DbType.String, objPacerTempRawImportData.AdversaryProceedingSeqNo);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingYear", DbType.String, objPacerTempRawImportData.AdversaryProceedingYear);
        //    db.AddInParameter(dbCommand, "AdversaryProceedingDisposition", DbType.String, objPacerTempRawImportData.AdversaryProceedingDisposition);
        //    db.AddInParameter(dbCommand, "PartyFirstName", DbType.String, objPacerTempRawImportData.PartyFirstName);
        //    db.AddInParameter(dbCommand, "PartyMiddleName", DbType.String, objPacerTempRawImportData.PartyMiddleName);
        //    db.AddInParameter(dbCommand, "PartyLastName", DbType.String, objPacerTempRawImportData.PartyLastName);
        //    db.AddInParameter(dbCommand, "PartyGeneration", DbType.String, objPacerTempRawImportData.PartyGeneration);
        //    db.AddInParameter(dbCommand, "PartyOffice", DbType.String, objPacerTempRawImportData.PartyOffice);
        //    db.AddInParameter(dbCommand, "PartyAddressLine1", DbType.String, objPacerTempRawImportData.PartyAddressLine1);
        //    db.AddInParameter(dbCommand, "PartyAddressLine2", DbType.String, objPacerTempRawImportData.PartyAddressLine2);
        //    db.AddInParameter(dbCommand, "PartyAddressLine3", DbType.String, objPacerTempRawImportData.PartyAddressLine3);
        //    db.AddInParameter(dbCommand, "PartyCity", DbType.String, objPacerTempRawImportData.PartyCity);
        //    db.AddInParameter(dbCommand, "PartyState", DbType.String, objPacerTempRawImportData.PartyState);
        //    db.AddInParameter(dbCommand, "PartyZipCode", DbType.String, objPacerTempRawImportData.PartyZipCode);
        //    db.AddInParameter(dbCommand, "PartyCountry", DbType.String, objPacerTempRawImportData.PartyCountry);
        //    db.AddInParameter(dbCommand, "PartyPhone", DbType.String, objPacerTempRawImportData.PartyPhone);
        //    db.AddInParameter(dbCommand, "PartySSNo", DbType.String, objPacerTempRawImportData.PartySSNo);
        //    if (objPacerTempRawImportData.PartyTaxID > 0)
        //    {
        //        db.AddInParameter(dbCommand, "PartyTaxID", DbType.Int32, objPacerTempRawImportData.PartyTaxID);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "PartyTaxID", DbType.Int32, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "PartyCaseSeqNo", DbType.Int32, objPacerTempRawImportData.PartyCaseSeqNo);
        //    db.AddInParameter(dbCommand, "PartyCaseRole", DbType.String, objPacerTempRawImportData.PartyCaseRole);
        //    if (objPacerTempRawImportData.PartyTerminatedDate > DateTime.MinValue)
        //    {
        //        db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, objPacerTempRawImportData.PartyTerminatedDate);
        //    }
        //    else
        //    {
        //        db.AddInParameter(dbCommand, "PartyTerminatedDate", DbType.DateTime, Convert.DBNull);
        //    }
        //    db.AddInParameter(dbCommand, "CaseTitle", DbType.String, objPacerTempRawImportData.CaseTitle);
        //    db.AddInParameter(dbCommand, "AttorneyFirstName", DbType.String, objPacerTempRawImportData.AttorneyFirstName);
        //    db.AddInParameter(dbCommand, "AttorneyMiddleName", DbType.String, objPacerTempRawImportData.AttorneyMiddleName);
        //    db.AddInParameter(dbCommand, "AttorneyLastName", DbType.String, objPacerTempRawImportData.AttorneyLastName);
        //    db.AddInParameter(dbCommand, "AttorneyGeneration", DbType.String, objPacerTempRawImportData.AttorneyGeneration);
        //    db.AddInParameter(dbCommand, "AttorneyOffice", DbType.String, objPacerTempRawImportData.AttorneyOffice);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine1", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine1);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine2", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine2);
        //    db.AddInParameter(dbCommand, "AttorneyOfficeLine3", DbType.String, objPacerTempRawImportData.AttorneyOfficeLine3);
        //    db.AddInParameter(dbCommand, "AttorneyCity", DbType.String, objPacerTempRawImportData.AttorneyCity);
        //    db.AddInParameter(dbCommand, "AttorneyState", DbType.String, objPacerTempRawImportData.AttorneyState);
        //    db.AddInParameter(dbCommand, "AttorneyZipCode", DbType.String, objPacerTempRawImportData.AttorneyZipCode);
        //    db.AddInParameter(dbCommand, "AttorneyCountry", DbType.String, objPacerTempRawImportData.AttorneyCountry);
        //    db.AddInParameter(dbCommand, "AttorneyPhone", DbType.String, objPacerTempRawImportData.AttorneyPhone);
        //    db.AddInParameter(dbCommand, "nullcolumn", DbType.String, objPacerTempRawImportData.nullcolumn);
        //    db.ExecuteNonQuery(dbCommand, trans);
        //}
#endregion
        //static public void Delete(PacerTempRawImportData objPacerTempRawImportData)
        //{
			
        //    Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
        //    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerTempRawImportDataDelete");
        //    db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerTempRawImportData.ID);
        //    db.ExecuteNonQuery(dbCommand);
        //}
	}
}
