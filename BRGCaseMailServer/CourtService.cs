using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the Court table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2011 12:37:10 AM
	/// </summary>
	public class CourtService 
	{
        static private string _selectViewSQL = "Select ID, PacerFileFormatID, StateCode, CourtName, URLAbbrv, URLFull, FilePrefix, LastPacerLoadDischargeDate, LastPacerLoadFileDate, PacerFileVersion, PacerFileVersionNumColumns from v_Court";
        static public List<Court> GetAll()
        {
            List<Court> objCourts = new List<Court>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    Court objCourt = new Court();
                    LoadCourt(objCourt, reader);
                    objCourt.IsModified = false;
                    objCourts.Add(objCourt);
                }
                // always call Close when done reading.
                reader.Close();
                return objCourts;
            }
        }
        static public Court GetByID(int ID)
        {
            Court objCourt = new Court();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadCourt(objCourt, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objCourt.IsModified = false;
                    return objCourt;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadCourt(Court objCourt, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objCourt.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objCourt.PacerFileFormatID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objCourt.StateCode = _reader.GetString(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objCourt.CourtName = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objCourt.URLAbbrv = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objCourt.URLFull = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objCourt.FilePrefix = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objCourt.LastPacerLoadDischargeDate = _reader.GetDateTime(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objCourt.LastPacerLoadFileDate = _reader.GetDateTime(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objCourt.PacerFileVersion = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objCourt.PacerFileVersionNumColumns = _reader.GetInt32(10);
            }
        }
        static public Court Refresh(Court objCourt)
        {
            string query = _selectViewSQL + " where ID = " + objCourt.ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadCourt(objCourt, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objCourt.IsModified = false;
                    return objCourt;
                }
                else
                {
                    return null;
                }
            }
        }
        
        static public Court GetByECFCourtString(string ECFCourtString)
        {
            Court objCourt = new Court();
            
            string query;
            if (ECFCourtString.Contains("-"))
                query = _selectViewSQL + " where CourtName like '" + ECFCourtString.Substring(0, ECFCourtString.IndexOf("-")-4) + "%'";
            else
                query = _selectViewSQL + " where CourtName = '" + ECFCourtString + "'";

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadCourt(objCourt, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objCourt.IsModified = false;
                    return objCourt;
                }
                else
                {
                    return null;
                }
            }
        }
        static public string CheckECFVersions()
        {
            ECFVersionByCourt _version = new ECFVersionByCourt();

            List<ECFVersion> _versions = _version.GetBankruptcyCourtVersions();

            int iUnknownFormat = 0;
            int iUnknownCourt = 0;
            int iUpdated = 0;
            int iMatch = 0;

            foreach (ECFVersion _ecf in _versions)
            {
                PacerFileFormat _format = PacerFileFormatService.GetByVersionString(_ecf.versionString);
                if (_format == null)
                {
                    iUnknownFormat++;
                }
                else
                {
                    Court _icourt = CourtService.GetByECFCourtString(_ecf.courtName);

                    //if ( _icourt.CourtName == "Illinois Central")
                    //    Debug.WriteLine(_icourt.CourtAndFileVersion);

                    if (_icourt != null && _icourt.PacerFileFormatID == _format.ID)
                    {
                        iMatch++;
                    }
                    else if (_icourt != null)
                    {

                        _icourt.PacerFileFormatID = _format.ID;
                        _icourt.PacerFileVersion = _format.PacerFileVersion;
                        CourtService.Save(_icourt);
                        iUpdated++;
                    }
                    else
                    {
                        iUnknownCourt++;
                    }
                }
            }

            return(iUpdated + " courts updated, " + iMatch + " court versions matched, " + iUnknownFormat + " pacer file formats unknown, " + iUnknownCourt + " Courts unknown ");
        
        }
        
        static public void Save(Court objCourt)
        {
            if (objCourt.IsModified == true)
            {
                if (objCourt.ID == 0)
                {
                    Insert(objCourt);
                }
                else
                {
                    Update(objCourt);
                }
            }
        }
        static private void Insert(Court objCourt)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CourtInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objCourt.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objCourt.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objCourt.StateCode);
            db.AddInParameter(dbCommand, "CourtName", DbType.String, objCourt.CourtName);
            db.AddInParameter(dbCommand, "URLAbbrv", DbType.String, objCourt.URLAbbrv);
            db.AddInParameter(dbCommand, "URLFull", DbType.String, objCourt.URLFull);
            db.AddInParameter(dbCommand, "FilePrefix", DbType.String, objCourt.FilePrefix);
            if (objCourt.LastPacerLoadDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, objCourt.LastPacerLoadDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objCourt.LastPacerLoadFileDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, objCourt.LastPacerLoadFileDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
            objCourt.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Court objCourt)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CourtUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objCourt.ID);
            if (objCourt.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objCourt.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objCourt.StateCode);
            db.AddInParameter(dbCommand, "CourtName", DbType.String, objCourt.CourtName);
            db.AddInParameter(dbCommand, "URLAbbrv", DbType.String, objCourt.URLAbbrv);
            db.AddInParameter(dbCommand, "URLFull", DbType.String, objCourt.URLFull);
            db.AddInParameter(dbCommand, "FilePrefix", DbType.String, objCourt.FilePrefix);
            if (objCourt.LastPacerLoadDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, objCourt.LastPacerLoadDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objCourt.LastPacerLoadFileDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, objCourt.LastPacerLoadFileDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(Court objCourt, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objCourt.IsModified == true)
                {
                    if (objCourt.ID == 0)
                    {
                        Insert(objCourt, db, trans);
                    }
                    else
                    {
                        Update(objCourt, db, trans);
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
        static private void Insert(Court objCourt, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CourtInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objCourt.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objCourt.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objCourt.StateCode);
            db.AddInParameter(dbCommand, "CourtName", DbType.String, objCourt.CourtName);
            db.AddInParameter(dbCommand, "URLAbbrv", DbType.String, objCourt.URLAbbrv);
            db.AddInParameter(dbCommand, "URLFull", DbType.String, objCourt.URLFull);
            db.AddInParameter(dbCommand, "FilePrefix", DbType.String, objCourt.FilePrefix);
            if (objCourt.LastPacerLoadDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, objCourt.LastPacerLoadDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objCourt.LastPacerLoadFileDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, objCourt.LastPacerLoadFileDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
            objCourt.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Court objCourt, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CourtUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objCourt.ID);
            if (objCourt.PacerFileFormatID > 0)
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objCourt.PacerFileFormatID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objCourt.StateCode);
            db.AddInParameter(dbCommand, "CourtName", DbType.String, objCourt.CourtName);
            db.AddInParameter(dbCommand, "URLAbbrv", DbType.String, objCourt.URLAbbrv);
            db.AddInParameter(dbCommand, "URLFull", DbType.String, objCourt.URLFull);
            db.AddInParameter(dbCommand, "FilePrefix", DbType.String, objCourt.FilePrefix);
            if (objCourt.LastPacerLoadDischargeDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, objCourt.LastPacerLoadDischargeDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadDischargeDate", DbType.DateTime, Convert.DBNull);
            }
            if (objCourt.LastPacerLoadFileDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, objCourt.LastPacerLoadFileDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPacerLoadFileDate", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(Court objCourt)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CourtDelete");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objCourt.ID);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}
