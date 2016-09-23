using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the PacerImportTransaction table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2011 12:46:44 AM
	/// </summary>
	public class PacerImportTransactionService 
	{
        static private string _selectViewSQL = "Select ID, CourtID, PacerFileFormatID, BillablePages, Cost, DischargedCases, SearchCriteria, StartDate, EndDate, LineCount, TotalCases, InsertedCases, UpdatedCases, FilePath, DownloadTimeStamp, ImportStatus, ImportMessage, PacerFileVersion, PacerFileFormatNumColumns, CourtName from v_PacerImportTransaction";
        static public List<PacerImportTransaction> GetAll()
        {
            List<PacerImportTransaction> objPacerImportTransactions = new List<PacerImportTransaction>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    PacerImportTransaction objPacerImportTransaction = new PacerImportTransaction();
                    LoadPacerImportTransaction(objPacerImportTransaction, reader);
                    objPacerImportTransaction.IsModified = false;
                    objPacerImportTransactions.Add(objPacerImportTransaction);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerImportTransactions;
            }
        }
        static public PacerImportTransaction GetByID(int ID)
        {
            PacerImportTransaction objPacerImportTransaction = new PacerImportTransaction();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadPacerImportTransaction(objPacerImportTransaction, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objPacerImportTransaction.IsModified = false;
                    return objPacerImportTransaction;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadPacerImportTransaction(PacerImportTransaction objPacerImportTransaction, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objPacerImportTransaction.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objPacerImportTransaction.CourtID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objPacerImportTransaction.PacerFileFormatID = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objPacerImportTransaction.BillablePages = _reader.GetInt32(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objPacerImportTransaction.Cost = _reader.GetDecimal(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objPacerImportTransaction.DischargedCases = _reader.GetBoolean(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objPacerImportTransaction.SearchCriteria = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objPacerImportTransaction.StartDate = _reader.GetDateTime(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objPacerImportTransaction.EndDate = _reader.GetDateTime(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objPacerImportTransaction.LineCount = _reader.GetInt32(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objPacerImportTransaction.TotalCases = _reader.GetInt32(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objPacerImportTransaction.InsertedCases = _reader.GetInt32(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objPacerImportTransaction.UpdatedCases = _reader.GetInt32(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objPacerImportTransaction.FilePath = _reader.GetString(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objPacerImportTransaction.DownloadTimeStamp = _reader.GetDateTime(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objPacerImportTransaction.ImportStatus = _reader.GetString(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objPacerImportTransaction.ImportMessage = _reader.GetString(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objPacerImportTransaction.PacerFileVersion = _reader.GetString(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objPacerImportTransaction.PacerFileFormatNumColumns = _reader.GetInt32(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objPacerImportTransaction.CourtName = _reader.GetString(19);
            }
        }
        static public List<PacerImportTransaction> GetForCourt(int CourtID)
        {
            List<PacerImportTransaction> objPacerImportTransactions = new List<PacerImportTransaction>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " Where CourtID = " + CourtID + " order by EndDate Desc "))
            {
                while (reader.Read())
                {
                    PacerImportTransaction objPacerImportTransaction = new PacerImportTransaction();
                    LoadPacerImportTransaction(objPacerImportTransaction, reader);
                    objPacerImportTransaction.IsModified = false;
                    objPacerImportTransactions.Add(objPacerImportTransaction);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerImportTransactions;
            }
        }
        static public List<PacerImportTransaction> GetOverLappingTransactions(PacerImportTransaction testPacerImportTransaction)
        {
            List<PacerImportTransaction> objPacerImportTransactions = new List<PacerImportTransaction>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = _selectViewSQL + " Where CourtID = " + testPacerImportTransaction.CourtID;
            _query += " and ( (StartDate <= '" + testPacerImportTransaction.StartDate + "' and EndDate >= '" + testPacerImportTransaction.StartDate + "') or";
            _query += "       (StartDate >= '" + testPacerImportTransaction.StartDate + "' and EndDate <= '" + testPacerImportTransaction.EndDate + "') or ";
            _query += "       (StartDate >= '" + testPacerImportTransaction.StartDate + "' and StartDate <= '" + testPacerImportTransaction.EndDate + "') )";

            //ignore the failures.
            _query += " and ImportStatus = 'SUCCESS' ";

            if (testPacerImportTransaction.DischargedCases == true)
            {
                _query += " and (DischargedCases = 1)";
            }
            else
            {
                _query += " and (DischargedCases = 0)";            
            }

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    PacerImportTransaction objPacerImportTransaction = new PacerImportTransaction();
                    LoadPacerImportTransaction(objPacerImportTransaction, reader);
                    objPacerImportTransaction.IsModified = false;
                    objPacerImportTransactions.Add(objPacerImportTransaction);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerImportTransactions;
            }
        
        }

        static public void Save(PacerImportTransaction objPacerImportTransaction)
		{
			if (objPacerImportTransaction.IsModified == true)
			{
				if (objPacerImportTransaction.ID == 0 )
				{
					Insert(objPacerImportTransaction);
				}
				else
				{
					Update(objPacerImportTransaction);
				}
			}
		}
        static private void Insert(PacerImportTransaction objPacerImportTransaction)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportTransactionInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objPacerImportTransaction.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objPacerImportTransaction.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            if (objPacerImportTransaction.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerImportTransaction.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "BillablePages", DbType.Int32, objPacerImportTransaction.BillablePages);
            db.AddInParameter(dbCommand, "Cost", DbType.Decimal, objPacerImportTransaction.Cost);
            db.AddInParameter(dbCommand, "DischargedCases", DbType.Boolean, objPacerImportTransaction.DischargedCases);
            db.AddInParameter(dbCommand, "SearchCriteria", DbType.String, objPacerImportTransaction.SearchCriteria);
            if (objPacerImportTransaction.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objPacerImportTransaction.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportTransaction.EndDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, objPacerImportTransaction.EndDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LineCount", DbType.Int32, objPacerImportTransaction.LineCount);
            db.AddInParameter(dbCommand, "TotalCases", DbType.Int32, objPacerImportTransaction.TotalCases);
            db.AddInParameter(dbCommand, "InsertedCases", DbType.Int32, objPacerImportTransaction.InsertedCases);
            db.AddInParameter(dbCommand, "UpdatedCases", DbType.Int32, objPacerImportTransaction.UpdatedCases);
            db.AddInParameter(dbCommand, "FilePath", DbType.String, objPacerImportTransaction.FilePath);
            if (objPacerImportTransaction.DownloadTimeStamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, objPacerImportTransaction.DownloadTimeStamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ImportStatus", DbType.String, objPacerImportTransaction.ImportStatus);
            db.AddInParameter(dbCommand, "ImportMessage", DbType.String, objPacerImportTransaction.ImportMessage);
            db.ExecuteNonQuery(dbCommand);
            objPacerImportTransaction.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(PacerImportTransaction objPacerImportTransaction)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportTransactionUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportTransaction.ID);
            if (objPacerImportTransaction.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objPacerImportTransaction.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            if (objPacerImportTransaction.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerImportTransaction.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "BillablePages", DbType.Int32, objPacerImportTransaction.BillablePages);
            db.AddInParameter(dbCommand, "Cost", DbType.Decimal, objPacerImportTransaction.Cost);
            db.AddInParameter(dbCommand, "DischargedCases", DbType.Boolean, objPacerImportTransaction.DischargedCases);
            db.AddInParameter(dbCommand, "SearchCriteria", DbType.String, objPacerImportTransaction.SearchCriteria);
            if (objPacerImportTransaction.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objPacerImportTransaction.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportTransaction.EndDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, objPacerImportTransaction.EndDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LineCount", DbType.Int32, objPacerImportTransaction.LineCount);
            db.AddInParameter(dbCommand, "TotalCases", DbType.Int32, objPacerImportTransaction.TotalCases);
            db.AddInParameter(dbCommand, "InsertedCases", DbType.Int32, objPacerImportTransaction.InsertedCases);
            db.AddInParameter(dbCommand, "UpdatedCases", DbType.Int32, objPacerImportTransaction.UpdatedCases);
            db.AddInParameter(dbCommand, "FilePath", DbType.String, objPacerImportTransaction.FilePath);
            if (objPacerImportTransaction.DownloadTimeStamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, objPacerImportTransaction.DownloadTimeStamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ImportStatus", DbType.String, objPacerImportTransaction.ImportStatus);
            db.AddInParameter(dbCommand, "ImportMessage", DbType.String, objPacerImportTransaction.ImportMessage);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(PacerImportTransaction objPacerImportTransaction, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objPacerImportTransaction.IsModified == true)
                {
                    if (objPacerImportTransaction.ID == 0)
                    {
                        Insert(objPacerImportTransaction, db, trans);
                    }
                    else
                    {
                        Update(objPacerImportTransaction, db, trans);
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
        static private void Insert(PacerImportTransaction objPacerImportTransaction, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportTransactionInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objPacerImportTransaction.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objPacerImportTransaction.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            if (objPacerImportTransaction.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerImportTransaction.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "BillablePages", DbType.Int32, objPacerImportTransaction.BillablePages);
            db.AddInParameter(dbCommand, "Cost", DbType.Decimal, objPacerImportTransaction.Cost);
            db.AddInParameter(dbCommand, "DischargedCases", DbType.Boolean, objPacerImportTransaction.DischargedCases);
            db.AddInParameter(dbCommand, "SearchCriteria", DbType.String, objPacerImportTransaction.SearchCriteria);
            if (objPacerImportTransaction.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objPacerImportTransaction.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportTransaction.EndDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, objPacerImportTransaction.EndDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LineCount", DbType.Int32, objPacerImportTransaction.LineCount);
            db.AddInParameter(dbCommand, "TotalCases", DbType.Int32, objPacerImportTransaction.TotalCases);
            db.AddInParameter(dbCommand, "InsertedCases", DbType.Int32, objPacerImportTransaction.InsertedCases);
            db.AddInParameter(dbCommand, "UpdatedCases", DbType.Int32, objPacerImportTransaction.UpdatedCases);
            db.AddInParameter(dbCommand, "FilePath", DbType.String, objPacerImportTransaction.FilePath);
            if (objPacerImportTransaction.DownloadTimeStamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, objPacerImportTransaction.DownloadTimeStamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ImportStatus", DbType.String, objPacerImportTransaction.ImportStatus);
            db.AddInParameter(dbCommand, "ImportMessage", DbType.String, objPacerImportTransaction.ImportMessage);
            db.ExecuteNonQuery(dbCommand, trans);
            objPacerImportTransaction.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(PacerImportTransaction objPacerImportTransaction, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportTransactionUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportTransaction.ID);
            if (objPacerImportTransaction.CourtID > 0)
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, objPacerImportTransaction.CourtID);
            }
            else
            {
                db.AddInParameter(dbCommand, "CourtID", DbType.Int32, Convert.DBNull);
            }
            if (objPacerImportTransaction.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerImportTransaction.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "BillablePages", DbType.Int32, objPacerImportTransaction.BillablePages);
            db.AddInParameter(dbCommand, "Cost", DbType.Decimal, objPacerImportTransaction.Cost);
            db.AddInParameter(dbCommand, "DischargedCases", DbType.Boolean, objPacerImportTransaction.DischargedCases);
            db.AddInParameter(dbCommand, "SearchCriteria", DbType.String, objPacerImportTransaction.SearchCriteria);
            if (objPacerImportTransaction.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objPacerImportTransaction.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            if (objPacerImportTransaction.EndDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, objPacerImportTransaction.EndDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LineCount", DbType.Int32, objPacerImportTransaction.LineCount);
            db.AddInParameter(dbCommand, "TotalCases", DbType.Int32, objPacerImportTransaction.TotalCases);
            db.AddInParameter(dbCommand, "InsertedCases", DbType.Int32, objPacerImportTransaction.InsertedCases);
            db.AddInParameter(dbCommand, "UpdatedCases", DbType.Int32, objPacerImportTransaction.UpdatedCases);
            db.AddInParameter(dbCommand, "FilePath", DbType.String, objPacerImportTransaction.FilePath);
            if (objPacerImportTransaction.DownloadTimeStamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, objPacerImportTransaction.DownloadTimeStamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "DownloadTimeStamp", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ImportStatus", DbType.String, objPacerImportTransaction.ImportStatus);
            db.AddInParameter(dbCommand, "ImportMessage", DbType.String, objPacerImportTransaction.ImportMessage);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(PacerImportTransaction objPacerImportTransaction)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerImportTransactionDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerImportTransaction.ID);
			db.ExecuteNonQuery(dbCommand);
		}
        static public void ProcessImportTransaction(PacerImportTransaction objPacerImportTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ImportRawPACERData");
            db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportTransaction.ID);
            db.ExecuteNonQuery(dbCommand);

            ///TODO: ProcessImportTransaction Cases
        }
        static public bool ProcessManualImportTransaction(PacerImportTransaction objPacerImportTransaction)
        {
            //use this when the SSIS import fails.
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            if (objPacerImportTransaction.PacerFileFormatID == 0)
            {
                throw new ArgumentException("Pacer File Format not specified!");
            }


            PacerFileFormat _format = PacerFileFormatService.GetByID(objPacerImportTransaction.PacerFileFormatID);

            // Read and display lines from the file until the end of
            // the file is reached.
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();

            try
            {

                using (TextReader tr = File.OpenText(objPacerImportTransaction.FilePath))
                {
                    String line;
                    int i=0;

                    while ((line = tr.ReadLine()) != null)
                    {
                        PacerTempRawImportData _data = new PacerTempRawImportData(line, _format);
                        PacerTempRawImportDataService.Insert(_data, db, trans);
                        if (i == 1167)
                            Debug.WriteLine("line:" + i.ToString() + " " + _data.CaseNumber4Digit);
                        i++;
                        Debug.WriteLine("line:" + i.ToString() + " " + _data.CaseNumber4Digit);
                    }

                    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ImportRawPACERData");
                    db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportTransaction.ID);
                    db.ExecuteNonQuery(dbCommand,trans);

                    trans.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                objPacerImportTransaction.ImportMessage = "Import Failed. Error:  " + ex.Message ;
                objPacerImportTransaction.ImportStatus = "FAILURE";
                trans.Rollback();
                
                //Save trans we're done.
                PacerImportTransactionService.Save(objPacerImportTransaction);
                return false;
            }

        }
        static public bool ProcessManualImportTransaction(PacerImportTransaction objPacerImportTransaction, PacerFileFormat _format)
        {
            //use this when the SSIS import fails.
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            if (objPacerImportTransaction.PacerFileFormatID == 0)
            {
                throw new ArgumentException("Pacer File Format not specified!");
            }

            // Read and display lines from the file until the end of
            // the file is reached.
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();

            try
            {

                using (TextReader tr = File.OpenText(objPacerImportTransaction.FilePath))
                {
                    String line;
                    int i = 0;

                    while ((line = tr.ReadLine()) != null)
                    {
                        try
                        {
                            PacerTempRawImportData _data = new PacerTempRawImportData(line, _format);
                            PacerTempRawImportDataService.Insert(_data, db, trans);
                            if (i == 3102)
                                Debug.WriteLine("line:" + i.ToString() + " " + _data.CaseNumber4Digit);
                            i++;
                            Debug.WriteLine("line:" + i.ToString() + " " + _data.CaseNumber4Digit);
                        }
                        catch (System.Data.SqlTypes.SqlTypeException ex)
                        {
                            if (ex.Message == "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.")
                            {
                                Debug.WriteLine(ex.ToString());  
                                //then ignore this line it's got bad dates.
                            }
                        }
                        
                    }

                    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ImportRawPACERData");
                    db.AddInParameter(dbCommand, "PacerImportTransactionID", DbType.Int32, objPacerImportTransaction.ID);
                    db.ExecuteNonQuery(dbCommand, trans);

                    trans.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                objPacerImportTransaction.ImportMessage = "Import Failed. Error:  " + ex.Message;
                objPacerImportTransaction.ImportStatus = "FAILURE";
                trans.Rollback();

                //Save trans we're done.
                PacerImportTransactionService.Save(objPacerImportTransaction);
                return false;
            }

        }
        static public int ProcessImportedLineItems(PacerImportTransaction objPacerImportTransaction, bool GeocodeLocations)
        {
            List<PacerImportData> _data = PacerImportDataService.GetForImportTransaction(objPacerImportTransaction.ID);
            PacerImportData _previousRow = null;
            int insertedRowCount = 0;
            int updatedRowCount = 0;
            int formatErrors = 0;

            string StateCode = null;

            foreach (PacerImportData _row in _data)
            {
                if (_previousRow == null)
                {
                    //first row - is this row a debtor  - if this row fails we just exit with failure because the
                    //file format is most likely incorrect.
                    if (((_row.PartyCaseRole.ToUpper() == "DEBTOR") || (_row.PartyCaseRole.ToUpper() == "CO-DEBTOR") || (_row.PartyCaseRole.ToUpper() == "JOINT DEBTOR")) && _row.CareOfOK)
                    {
                        _row.Imported = true;
                        PacerImportDataService.Save(_row);
                        try
                        {
                            BankruptcyCase _case = new BankruptcyCase(_row);

                            Debug.WriteLine(_case.CaseNumber4Digit);
                            if (GeocodeLocations)
                                YahooGeoCoder.GeocodeCase(_case);
                            _case.IsModified = true;

                            int returnvalue = BankruptcyCaseService.Save(_case, true);

                            if (returnvalue == 1)
                                insertedRowCount++;
                            else if (returnvalue == 2)
                                updatedRowCount++;
                        }
                        catch (Exception ex)
                        {
                            formatErrors++;
                        }
                    }
                    _previousRow = _row;
                }
                else //previousrow != null  if this row fails we do not exit with failure because the
                // it's just a bad row.
                {
                    if (_row.PartyAddressLine1 != _previousRow.PartyAddressLine1 && ((_row.PartyCaseRole.ToUpper() == "DEBTOR") || (_row.PartyCaseRole.ToUpper() == "CO-DEBTOR") || (_row.PartyCaseRole.ToUpper() == "JOINT DEBTOR")) && _row.CareOfOK)
                    {

                        try
                        {
                            //debtor at different address
                            _row.Imported = true;
                            PacerImportDataService.Save(_row);
                        
                            BankruptcyCase _case = new BankruptcyCase(_row);
                        
                            if (GeocodeLocations) 
                                YahooGeoCoder.GeocodeCase(_case);
                            if (insertedRowCount == 318)
                                Debug.WriteLine(_case.CaseNumber4Digit);
                            if (_case.LastName == "Babakatis")
                                Debug.WriteLine(_case.CaseNumber4Digit);
                            _case.IsModified = true;
                            int returnvalue = BankruptcyCaseService.Save(_case, true);

                            if (returnvalue == 1)
                            {
                                insertedRowCount++;
                                if (insertedRowCount == 1654)
                                {
                                    Debug.WriteLine(insertedRowCount);
                                }
                            }
                            else if (returnvalue == 2)
                                updatedRowCount++;
                        }
                        catch (Exception e)
                        {
                            formatErrors++;
                        }
                    } 
                                        
                    _previousRow = _row;
                }
            }

            // update counts for 
            objPacerImportTransaction.InsertedCases = insertedRowCount;
            objPacerImportTransaction.LineCount = _data.Count;
            objPacerImportTransaction.TotalCases = insertedRowCount + updatedRowCount;
            if (objPacerImportTransaction.ImportMessage.Length > 0)
                //we already have an import string - parse the result to add the previous total
                try
                {
                    if (objPacerImportTransaction.ImportMessage.IndexOf("Unique Cases Added") > 0)
                    {
                        int previoustotal = Int32.Parse(objPacerImportTransaction.ImportMessage.Substring(0, objPacerImportTransaction.ImportMessage.IndexOf("Unique Cases Added") - 1));
                        objPacerImportTransaction.ImportMessage = previoustotal + insertedRowCount + " Unique Cases Added!  " + updatedRowCount + " Cases Updated!  " + formatErrors + " format Errors.";
                    }
                    else
                    {
                        objPacerImportTransaction.ImportMessage = insertedRowCount + " Unique Cases Added!  " + updatedRowCount + " Cases Updated!  " + formatErrors + " format Errors.";
                    }
                }
                catch 
                { 
                
                }
            else
                objPacerImportTransaction.ImportMessage = insertedRowCount + " Unique Cases Added!  " + updatedRowCount + " Cases Updated!  " + formatErrors + " format Errors.";
            objPacerImportTransaction.ImportStatus = "SUCCESS";
            Save(objPacerImportTransaction);

            Court _court = CourtService.GetByID(objPacerImportTransaction.CourtID);
            ZipGeoCodeService.UpdateStatsForState(_court.StateCode);
            return insertedRowCount;
        
        }


    }
}
