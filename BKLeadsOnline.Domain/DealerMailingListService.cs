using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the DealerMailingList table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/21/2013 2:39:23 PM
	/// </summary>
	public class DealerMailingListService 
	{
		static private string _selectViewSQL  = "Select ID, DealerID, CreationDate, StartFilterDate, EndFilterDate, NumberCases, Printed, DocTemplateID, DealerName, DocTemplateDisplayName from v_DealerMailingList";
		static public  List<DealerMailingList> GetAll()
		{
			List<DealerMailingList> objDealerMailingLists = new List<DealerMailingList>();
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					DealerMailingList objDealerMailingList = new DealerMailingList();
					LoadDealerMailingList(objDealerMailingList, reader);
					objDealerMailingList.IsModified = false;
					objDealerMailingLists.Add (objDealerMailingList);
				}
				// always call Close when done reading.
				reader.Close();
				return objDealerMailingLists;
			}
		}
        
        //get the existing mailing lists
        static public List<DealerMailingList> GetForDealer(int DealerID)
        {
            List<DealerMailingList> objDealerMailingLists = new List<DealerMailingList>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " Where DealerID = " + DealerID + " Order by EndFilterDate desc " ))
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
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        
        static public DateTime GetMaxDischargedDateForDealerMailingList(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_GetMaxDischargedDateForDealerMailingList");
            db.AddOutParameter(dbCommand, "MaxDate", DbType.DateTime, 8);
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);

            try
            {
                return DateTime.Parse(db.GetParameterValue(dbCommand, "MaxDate").ToString());
            }
            catch
            {
                return DateTime.MinValue;
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
				objDealerMailingList.NumberCases = _reader.GetInt32(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objDealerMailingList.Printed = _reader.GetBoolean(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objDealerMailingList.DocTemplateID = _reader.GetInt32(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objDealerMailingList.DealerName = _reader.GetString(8);
			}
			if (_reader.IsDBNull(9) != true)
			{
				objDealerMailingList.DocTemplateDisplayName = _reader.GetString(9);
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

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "Printed", DbType.Boolean, objDealerMailingList.Printed);
            if (objDealerMailingList.DocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objDealerMailingList.DocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
            objDealerMailingList.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DealerMailingList objDealerMailingList)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "Printed", DbType.Boolean, objDealerMailingList.Printed);
            if (objDealerMailingList.DocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objDealerMailingList.DocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, Convert.DBNull);
            }
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

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "Printed", DbType.Boolean, objDealerMailingList.Printed);
            if (objDealerMailingList.DocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objDealerMailingList.DocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
            objDealerMailingList.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DealerMailingList objDealerMailingList, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            db.AddInParameter(dbCommand, "NumberCases", DbType.Int32, objDealerMailingList.NumberCases);
            db.AddInParameter(dbCommand, "Printed", DbType.Boolean, objDealerMailingList.Printed);
            if (objDealerMailingList.DocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objDealerMailingList.DocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(DealerMailingList objDealerMailingList)
		{
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealerMailingList.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
