using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the DealerMailingList table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/9/2011 10:58:02 AM
	/// </summary>
	public class DealerMailingListService 
	{
        static private string _selectViewSQL = "Select ID, DealerID, CreationDate, StartFilterDate, EndFilterDate, FiledCasesOnly, NumberCases, MarkedAsSold, Description, TemplateFilePath, CsvFilePath, MailMergeFilePath, TrackingLetterRecieved from DealerMailingList";
        static public List<DealerMailingList> GetAll()
        {
            List<DealerMailingList> objDealerMailingLists = new List<DealerMailingList>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    DealerMailingList objDealerMailingList = new DealerMailingList();
                    LoadDealerMailingList(objDealerMailingList, reader);
                    objDealerMailingList.IsModified = false;
                    objDealerMailingLists.Add(objDealerMailingList);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingLists;
            }
        }
        static public DealerMailingList GetByID(int ID)
        {
            DealerMailingList objDealerMailingList = new DealerMailingList();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDealerMailingList(objDealerMailingList, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDealerMailingList.IsModified = false;
                    return objDealerMailingList;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadDealerMailingList(DealerMailingList objDealerMailingList, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDealerMailingList.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDealerMailingList.DealerID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDealerMailingList.CreationDate = _reader.GetDateTime(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDealerMailingList.StartFilterDate = _reader.GetDateTime(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDealerMailingList.EndFilterDate = _reader.GetDateTime(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDealerMailingList.FiledCasesOnly = _reader.GetBoolean(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDealerMailingList.NumberCases = _reader.GetInt32(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDealerMailingList.MarkedAsSold = _reader.GetBoolean(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDealerMailingList.Description = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDealerMailingList.TemplateFilePath = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDealerMailingList.CsvFilePath = _reader.GetString(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDealerMailingList.MailMergeFilePath = _reader.GetString(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDealerMailingList.TrackingLetterRecieved = _reader.GetBoolean(12);
            }
        }
        static public List<DealerMailingList> GetForDealer(int _dealerID)
        {
            List<DealerMailingList> objDealerMailingLists = new List<DealerMailingList>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " where DealerID = " + _dealerID + " order by CreationDate desc " ))
            {
                while (reader.Read())
                {
                    DealerMailingList objDealerMailingList = new DealerMailingList();
                    LoadDealerMailingList(objDealerMailingList, reader);
                    objDealerMailingList.IsModified = false;
                    objDealerMailingLists.Add(objDealerMailingList);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingLists;
            }
        }
        static public List<DealerMailingList> GetByCsvFilePath(string CsvFilePath)
        {
            List<DealerMailingList> objDealerMailingLists = new List<DealerMailingList>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " where CsvFilePath = '" + CsvFilePath + "'"))
            {
                while (reader.Read())
                {
                    DealerMailingList objDealerMailingList = new DealerMailingList();
                    LoadDealerMailingList(objDealerMailingList, reader);
                    objDealerMailingList.IsModified = false;
                    objDealerMailingLists.Add(objDealerMailingList);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingLists;
            }
        }
        static public void Save(DealerMailingList objDealerMailingList)
		{
			if (objDealerMailingList.IsModified == true)
			{
				if (objDealerMailingList.ID == 0 )
				{
					Insert(objDealerMailingList);
				}
				else
				{
					Update(objDealerMailingList);
				}
			}
		}
        static private void Insert(DealerMailingList objDealerMailingList)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objDealerMailingList.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingList.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDealerMailingList.CreationDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, objDealerMailingList.CreationDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.StartFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, objDealerMailingList.StartFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.EndFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, objDealerMailingList.EndFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "FiledCasesOnly", DbType.Boolean, objDealerMailingList.FiledCasesOnly);
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "MarkedAsSold", DbType.Boolean, objDealerMailingList.MarkedAsSold);
            db.AddInParameter(dbCommand, "Description", DbType.String, objDealerMailingList.Description);
            db.AddInParameter(dbCommand, "TemplateFilePath", DbType.String, objDealerMailingList.TemplateFilePath);
            db.AddInParameter(dbCommand, "CsvFilePath", DbType.String, objDealerMailingList.CsvFilePath);
            db.AddInParameter(dbCommand, "MailMergeFilePath", DbType.String, objDealerMailingList.MailMergeFilePath);
            db.AddInParameter(dbCommand, "TrackingLetterRecieved", DbType.Boolean, objDealerMailingList.TrackingLetterRecieved);
            db.ExecuteNonQuery(dbCommand);
            objDealerMailingList.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DealerMailingList objDealerMailingList)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealerMailingList.ID);
            if (objDealerMailingList.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingList.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDealerMailingList.CreationDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, objDealerMailingList.CreationDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.StartFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, objDealerMailingList.StartFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.EndFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, objDealerMailingList.EndFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "FiledCasesOnly", DbType.Boolean, objDealerMailingList.FiledCasesOnly);
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "MarkedAsSold", DbType.Boolean, objDealerMailingList.MarkedAsSold);
            db.AddInParameter(dbCommand, "Description", DbType.String, objDealerMailingList.Description);
            db.AddInParameter(dbCommand, "TemplateFilePath", DbType.String, objDealerMailingList.TemplateFilePath);
            db.AddInParameter(dbCommand, "CsvFilePath", DbType.String, objDealerMailingList.CsvFilePath);
            db.AddInParameter(dbCommand, "MailMergeFilePath", DbType.String, objDealerMailingList.MailMergeFilePath);
            db.AddInParameter(dbCommand, "TrackingLetterRecieved", DbType.Boolean, objDealerMailingList.TrackingLetterRecieved);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(DealerMailingList objDealerMailingList, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objDealerMailingList.IsModified == true)
                {
                    if (objDealerMailingList.ID == 0)
                    {
                        Insert(objDealerMailingList, db, trans);
                    }
                    else
                    {
                        Update(objDealerMailingList, db, trans);
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
        static private void Insert(DealerMailingList objDealerMailingList, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objDealerMailingList.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingList.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDealerMailingList.CreationDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, objDealerMailingList.CreationDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.StartFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, objDealerMailingList.StartFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.EndFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, objDealerMailingList.EndFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "FiledCasesOnly", DbType.Boolean, objDealerMailingList.FiledCasesOnly);
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "MarkedAsSold", DbType.Boolean, objDealerMailingList.MarkedAsSold);
            db.AddInParameter(dbCommand, "Description", DbType.String, objDealerMailingList.Description);
            db.AddInParameter(dbCommand, "TemplateFilePath", DbType.String, objDealerMailingList.TemplateFilePath);
            db.AddInParameter(dbCommand, "CsvFilePath", DbType.String, objDealerMailingList.CsvFilePath);
            db.AddInParameter(dbCommand, "MailMergeFilePath", DbType.String, objDealerMailingList.MailMergeFilePath);
            db.AddInParameter(dbCommand, "TrackingLetterRecieved", DbType.Boolean, objDealerMailingList.TrackingLetterRecieved);
            db.ExecuteNonQuery(dbCommand, trans);
            objDealerMailingList.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DealerMailingList objDealerMailingList, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealerMailingList.ID);
            if (objDealerMailingList.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingList.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDealerMailingList.CreationDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, objDealerMailingList.CreationDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreationDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.StartFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, objDealerMailingList.StartFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartFilterDate", DbType.DateTime, Convert.DBNull);
            }
            if (objDealerMailingList.EndFilterDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, objDealerMailingList.EndFilterDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndFilterDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "FiledCasesOnly", DbType.Boolean, objDealerMailingList.FiledCasesOnly);
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "MarkedAsSold", DbType.Boolean, objDealerMailingList.MarkedAsSold);
            db.AddInParameter(dbCommand, "Description", DbType.String, objDealerMailingList.Description);
            db.AddInParameter(dbCommand, "TemplateFilePath", DbType.String, objDealerMailingList.TemplateFilePath);
            db.AddInParameter(dbCommand, "CsvFilePath", DbType.String, objDealerMailingList.CsvFilePath);
            db.AddInParameter(dbCommand, "MailMergeFilePath", DbType.String, objDealerMailingList.MailMergeFilePath);
            db.AddInParameter(dbCommand, "TrackingLetterRecieved", DbType.Boolean, objDealerMailingList.TrackingLetterRecieved);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(DealerMailingList objDealerMailingList)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealerMailingList.ID);
			db.ExecuteNonQuery(dbCommand);
		}

        static public void AddDealerMailingListCase(int DealerMailingListID, int BankruptcyCaseID)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseAdd");
            db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, DealerMailingListID);
            db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, BankruptcyCaseID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteDealerMailingListCase(int DealerMailingListID, int BankruptcyCaseID)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseDelete");
            db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, DealerMailingListID);
            db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, BankruptcyCaseID);
            db.ExecuteNonQuery(dbCommand);
        }

        #region save, insert, update using transactions
        static public void AddDealerMailingListCase(int DealerMailingListID, int BankruptcyCaseID, Database db, DbTransaction trans, bool CommitTrans)
        {
            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseAdd");
            db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, DealerMailingListID);
            db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, BankruptcyCaseID);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        static public void DeleteDealerMailingListCase(int DealerMailingListID, int BankruptcyCaseID, Database db, DbTransaction trans, bool CommitTrans)
        {
            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseDelete");
            db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, DealerMailingListID);
            db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, BankruptcyCaseID);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion

	}
}
