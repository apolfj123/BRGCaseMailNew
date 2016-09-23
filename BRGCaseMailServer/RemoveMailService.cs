using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
    /// <summary>
    /// A Class interacting with the RemoveMail table.
    /// Author: Jonathan Shaw
    /// Date Created: 2/16/2012 2:10:00 PM
    /// </summary>
    public class RemoveMailService
    {
        static private string _selectViewSQL = "Select ID, IndexOnMailServer, MsgDateTime, ToAddress, FromAddress, Subject, Body, Processed, FoundAndRemoved from RemoveMail";
        static public List<RemoveMail> GetAll()
        {
            List<RemoveMail> objRemoveMails = new List<RemoveMail>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " order by MsgDateTime desc "))
            {
                while (reader.Read())
                {
                    RemoveMail objRemoveMail = new RemoveMail();
                    LoadRemoveMail(objRemoveMail, reader);
                    objRemoveMail.IsModified = false;
                    objRemoveMails.Add(objRemoveMail);
                }
                // always call Close when done reading.
                reader.Close();
                return objRemoveMails;
            }
        }
        static public List<RemoveMail> GetSince(DateTime _date)
        {
            List<RemoveMail> objRemoveMails = new List<RemoveMail>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " Where MsgDateTime > '" + _date.ToShortDateString() + "' order by MsgDateTime desc "))
            {
                while (reader.Read())
                {
                    RemoveMail objRemoveMail = new RemoveMail();
                    LoadRemoveMail(objRemoveMail, reader);
                    objRemoveMail.IsModified = false;
                    objRemoveMails.Add(objRemoveMail);
                }
                // always call Close when done reading.
                reader.Close();
                return objRemoveMails;
            }
        }
        static public RemoveMail GetByID(int ID)
        {
            RemoveMail objRemoveMail = new RemoveMail();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadRemoveMail(objRemoveMail, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objRemoveMail.IsModified = false;
                    return objRemoveMail;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadRemoveMail(RemoveMail objRemoveMail, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objRemoveMail.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objRemoveMail.IndexOnMailServer = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objRemoveMail.MsgDateTime = _reader.GetDateTime(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objRemoveMail.ToAddress = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objRemoveMail.FromAddress = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objRemoveMail.Subject = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objRemoveMail.Body = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objRemoveMail.Processed = _reader.GetBoolean(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objRemoveMail.FoundAndRemoved = _reader.GetBoolean(8);
            }
        }
        static public void Save(RemoveMail objRemoveMail)
        {
            if (objRemoveMail.IsModified == true)
            {
                if (objRemoveMail.ID == 0)
                {
                    Insert(objRemoveMail);
                }
                else
                {
                    Update(objRemoveMail);
                }
            }
        }
        static private void Insert(RemoveMail objRemoveMail)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_RemoveMailInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "IndexOnMailServer", DbType.Int32, objRemoveMail.IndexOnMailServer);
            if (objRemoveMail.MsgDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, objRemoveMail.MsgDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ToAddress", DbType.String, objRemoveMail.ToAddress);
            db.AddInParameter(dbCommand, "FromAddress", DbType.String, objRemoveMail.FromAddress);
            db.AddInParameter(dbCommand, "Subject", DbType.String, objRemoveMail.Subject);
            db.AddInParameter(dbCommand, "Body", DbType.String, objRemoveMail.Body);
            db.AddInParameter(dbCommand, "Processed", DbType.Boolean, objRemoveMail.Processed);
            db.AddInParameter(dbCommand, "FoundAndRemoved", DbType.Boolean, objRemoveMail.FoundAndRemoved);
            db.ExecuteNonQuery(dbCommand);
            objRemoveMail.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(RemoveMail objRemoveMail)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_RemoveMailUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objRemoveMail.ID);
            db.AddInParameter(dbCommand, "IndexOnMailServer", DbType.Int32, objRemoveMail.IndexOnMailServer);
            if (objRemoveMail.MsgDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, objRemoveMail.MsgDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ToAddress", DbType.String, objRemoveMail.ToAddress);
            db.AddInParameter(dbCommand, "FromAddress", DbType.String, objRemoveMail.FromAddress);
            db.AddInParameter(dbCommand, "Subject", DbType.String, objRemoveMail.Subject);
            db.AddInParameter(dbCommand, "Body", DbType.String, objRemoveMail.Body);
            db.AddInParameter(dbCommand, "Processed", DbType.Boolean, objRemoveMail.Processed);
            db.AddInParameter(dbCommand, "FoundAndRemoved", DbType.Boolean, objRemoveMail.FoundAndRemoved);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(RemoveMail objRemoveMail, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objRemoveMail.IsModified == true)
                {
                    if (objRemoveMail.ID == 0)
                    {
                        Insert(objRemoveMail, db, trans);
                    }
                    else
                    {
                        Update(objRemoveMail, db, trans);
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
        static private void Insert(RemoveMail objRemoveMail, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_RemoveMailInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "IndexOnMailServer", DbType.Int32, objRemoveMail.IndexOnMailServer);
            if (objRemoveMail.MsgDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, objRemoveMail.MsgDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ToAddress", DbType.String, objRemoveMail.ToAddress);
            db.AddInParameter(dbCommand, "FromAddress", DbType.String, objRemoveMail.FromAddress);
            db.AddInParameter(dbCommand, "Subject", DbType.String, objRemoveMail.Subject);
            db.AddInParameter(dbCommand, "Body", DbType.String, objRemoveMail.Body);
            db.AddInParameter(dbCommand, "Processed", DbType.Boolean, objRemoveMail.Processed);
            db.AddInParameter(dbCommand, "FoundAndRemoved", DbType.Boolean, objRemoveMail.FoundAndRemoved);
            db.ExecuteNonQuery(dbCommand, trans);
            objRemoveMail.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(RemoveMail objRemoveMail, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_RemoveMailUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objRemoveMail.ID);
            db.AddInParameter(dbCommand, "IndexOnMailServer", DbType.Int32, objRemoveMail.IndexOnMailServer);
            if (objRemoveMail.MsgDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, objRemoveMail.MsgDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "MsgDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ToAddress", DbType.String, objRemoveMail.ToAddress);
            db.AddInParameter(dbCommand, "FromAddress", DbType.String, objRemoveMail.FromAddress);
            db.AddInParameter(dbCommand, "Subject", DbType.String, objRemoveMail.Subject);
            db.AddInParameter(dbCommand, "Body", DbType.String, objRemoveMail.Body);
            db.AddInParameter(dbCommand, "Processed", DbType.Boolean, objRemoveMail.Processed);
            db.AddInParameter(dbCommand, "FoundAndRemoved", DbType.Boolean, objRemoveMail.FoundAndRemoved);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(RemoveMail objRemoveMail)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_RemoveMailDelete");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objRemoveMail.ID);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}
