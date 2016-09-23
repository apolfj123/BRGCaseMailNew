using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the MailMergeStaging table.
	/// Author: Jonathan Shaw
	/// Date Created: 2/15/2012 11:23:08 AM
	/// </summary>
	public class MailMergeStagingService 
	{
		static private string _selectViewSQL  = "Select CaseNumber, CaseNumber4Digit, FiledDate, DischargedDate, FirstName, MiddleName, LastName, Suffix, Address1, Address2, City, State, PostalCode, Callsource from MailMergeStaging";
		static public  List<MailMergeStaging> GetAll()
		{
			List<MailMergeStaging> objMailMergeStagings = new List<MailMergeStaging>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					MailMergeStaging objMailMergeStaging = new MailMergeStaging();
					LoadMailMergeStaging(objMailMergeStaging, reader);
					objMailMergeStagings.Add (objMailMergeStaging);
				}
				// always call Close when done reading.
				reader.Close();
				return objMailMergeStagings;
			}
		}
		static private void LoadMailMergeStaging(MailMergeStaging objMailMergeStaging, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objMailMergeStaging.CaseNumber = _reader.GetString(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objMailMergeStaging.CaseNumber4Digit = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objMailMergeStaging.FiledDate = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objMailMergeStaging.DischargedDate = _reader.GetString(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objMailMergeStaging.FirstName = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objMailMergeStaging.MiddleName = _reader.GetString(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objMailMergeStaging.LastName = _reader.GetString(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objMailMergeStaging.Suffix = _reader.GetString(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objMailMergeStaging.Address1 = _reader.GetString(8);
			}
			if (_reader.IsDBNull(9) != true)
			{
				objMailMergeStaging.Address2 = _reader.GetString(9);
			}
			if (_reader.IsDBNull(10) != true)
			{
				objMailMergeStaging.City = _reader.GetString(10);
			}
			if (_reader.IsDBNull(11) != true)
			{
				objMailMergeStaging.State = _reader.GetString(11);
			}
			if (_reader.IsDBNull(12) != true)
			{
				objMailMergeStaging.PostalCode = _reader.GetString(12);
			}
			if (_reader.IsDBNull(13) != true)
			{
				objMailMergeStaging.Callsource = _reader.GetString(13);
			}
		}
		static public void Insert(MailMergeStaging objMailMergeStaging)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_MailMergeStagingInsert");
				db.AddInParameter(dbCommand, "CaseNumber", DbType.String, objMailMergeStaging.CaseNumber);
				db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objMailMergeStaging.CaseNumber4Digit);
				db.AddInParameter(dbCommand, "FiledDate", DbType.String, objMailMergeStaging.FiledDate);
				db.AddInParameter(dbCommand, "DischargedDate", DbType.String, objMailMergeStaging.DischargedDate);
				db.AddInParameter(dbCommand, "FirstName", DbType.String, objMailMergeStaging.FirstName);
				db.AddInParameter(dbCommand, "MiddleName", DbType.String, objMailMergeStaging.MiddleName);
				db.AddInParameter(dbCommand, "LastName", DbType.String, objMailMergeStaging.LastName);
				db.AddInParameter(dbCommand, "Suffix", DbType.String, objMailMergeStaging.Suffix);
				db.AddInParameter(dbCommand, "Address1", DbType.String, objMailMergeStaging.Address1);
				db.AddInParameter(dbCommand, "Address2", DbType.String, objMailMergeStaging.Address2);
				db.AddInParameter(dbCommand, "City", DbType.String, objMailMergeStaging.City);
				db.AddInParameter(dbCommand, "State", DbType.String, objMailMergeStaging.State);
				db.AddInParameter(dbCommand, "PostalCode", DbType.String, objMailMergeStaging.PostalCode);
				db.AddInParameter(dbCommand, "Callsource", DbType.String, objMailMergeStaging.Callsource);
			db.ExecuteNonQuery(dbCommand);
		}

        static public void InsertList(List<BankruptcyCase> _cases, string CallSource, Database db, DbTransaction trans, bool CommitTrans)
        {
            DeleteAll(db, trans, CommitTrans);
            foreach (BankruptcyCase _case in _cases)
            {
                MailMergeStaging _staging = new MailMergeStaging(_case, CallSource);
                Insert(_staging, db, trans, CommitTrans);
            }
        }
        static public void Insert(MailMergeStaging objMailMergeStaging, Database db, DbTransaction trans, bool CommitTrans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_MailMergeStagingInsert");
            db.AddInParameter(dbCommand, "CaseNumber", DbType.String, objMailMergeStaging.CaseNumber);
            db.AddInParameter(dbCommand, "CaseNumber4Digit", DbType.String, objMailMergeStaging.CaseNumber4Digit);
            db.AddInParameter(dbCommand, "FiledDate", DbType.String, objMailMergeStaging.FiledDate);
            db.AddInParameter(dbCommand, "DischargedDate", DbType.String, objMailMergeStaging.DischargedDate);
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objMailMergeStaging.FirstName);
            db.AddInParameter(dbCommand, "MiddleName", DbType.String, objMailMergeStaging.MiddleName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objMailMergeStaging.LastName);
            db.AddInParameter(dbCommand, "Suffix", DbType.String, objMailMergeStaging.Suffix);
            db.AddInParameter(dbCommand, "Address1", DbType.String, objMailMergeStaging.Address1);
            db.AddInParameter(dbCommand, "Address2", DbType.String, objMailMergeStaging.Address2);
            db.AddInParameter(dbCommand, "City", DbType.String, objMailMergeStaging.City);
            db.AddInParameter(dbCommand, "State", DbType.String, objMailMergeStaging.State);
            db.AddInParameter(dbCommand, "PostalCode", DbType.String, objMailMergeStaging.PostalCode);
            db.AddInParameter(dbCommand, "Callsource", DbType.String, objMailMergeStaging.Callsource);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        static public void DeleteAll(Database db, DbTransaction trans, bool CommitTrans)
        {
            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_MailMergeStagingDeleteAll");
            db.ExecuteNonQuery(dbCommand, trans);
        }   
     }
}
