using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the State table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/5/2011 1:16:56 PM
	/// </summary>
	public class StateService 
	{
		static private string _selectViewSQL  = "Select StateCode, StateName, Include from State";
		static public  List<State> GetAll()
		{
			List<State> objStates = new List<State>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					State objState = new State();
					LoadState(objState, reader);
					objState.IsModified = false;
					objStates.Add (objState);
				}
				// always call Close when done reading.
				reader.Close();
				return objStates;
			}
		}

        static public List<State> GetActive()
        {
            List<State> objStates = new List<State>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " where Include = 1 order by StateCode asc"))
            {
                while (reader.Read())
                {
                    State objState = new State();
                    LoadState(objState, reader);
                    objState.IsModified = false;
                    objStates.Add(objState);
                }
                // always call Close when done reading.
                reader.Close();
                return objStates;
            }
        }

        static public List<State> GetWithinDistance( float Latitude, float longitude, float distance)
        {
            List<State> objStates = new List<State>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            
            string _query = "Select StateCode, StateName, Include from State where Include = 1 and ";
            _query += " (select count(*) from ZipGeoCode zgc where zgc.State = State.StateCode and ";
            _query += " dbo.DistanceBetween(" + Latitude + ", " + longitude + ", zgc.Latitude, zgc.Longitude) <  " + distance + ") > 0 order by StateCode asc ";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    State objState = new State();
                    LoadState(objState, reader);
                    objState.IsModified = false;
                    objStates.Add(objState);
                }
                
                // always call Close when done reading.
                reader.Close();
                return objStates;
            }
        }
        static public State GetByStateCode(string StateCode)
		{
			State objState = new State();
			string query = _selectViewSQL + " where StateCode = '" + StateCode + "'";
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadState(objState, reader);
					// always call Close when done reading.
					reader.Close();
					objState.IsModified = false;
					return objState;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadState(State objState, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objState.StateCode = _reader.GetString(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objState.StateName = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objState.Include = _reader.GetBoolean(2);
			}
		}

 
//        static public void Save(State objState)
//        {
//            if (objState.IsModified == true)
//            {
//                if (objState.ID == 0 )
//                {
//                    Insert(objState);
//                }
//                else
//                {
//                    Update(objState);
//                }
//            }
//        }
//        static private void Insert(State objState)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateInsert");
//                db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
//                db.AddInParameter(dbCommand, "StateName", DbType.String, objState.StateName);
//                db.AddInParameter(dbCommand, "Include", DbType.Boolean, objState.Include);
//            db.ExecuteNonQuery(dbCommand);
//            objState.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(State objState)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateUpdate");
//            db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
//            db.AddInParameter(dbCommand, "StateName", DbType.String, objState.StateName);
//            db.AddInParameter(dbCommand, "Include", DbType.Boolean, objState.Include);
//            db.ExecuteNonQuery(dbCommand);
//        }
//#region save, insert, update using transactions
//        static public void Save(State objState, Database db, DbTransaction trans, bool CommitTrans)
//        {
//            try
//            {
//                if (objState.IsModified == true)
//                {
//                    if (objState.ID == 0 )
//                    {
//                        Insert(objState, db, trans);
//                    }
//                    else
//                    {
//                        Update(objState, db, trans);
//                    }
//                }
//                if (CommitTrans == true)
//                {
//                    trans.Commit();
//                }
//            }
//            catch (Exception e)
//            {
//                if (CommitTrans == true)
//                {
//                    //we were hoping to commit the transaction which means we're at the end so
//                    //Roll back the transaction. Otherwise just rethrow the error and let the next
//                    //higher level catch it and rollback
//                    trans.Rollback();
//                }
//            //and then rethrow the error
//            throw (e);
//            }
//        }
//        static private void Insert(State objState, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateInsert");
//                db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
//                db.AddInParameter(dbCommand, "StateName", DbType.String, objState.StateName);
//                db.AddInParameter(dbCommand, "Include", DbType.Boolean, objState.Include);
//            db.ExecuteNonQuery(dbCommand, trans);
//            objState.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(State objState, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateUpdate");
//            db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
//            db.AddInParameter(dbCommand, "StateName", DbType.String, objState.StateName);
//            db.AddInParameter(dbCommand, "Include", DbType.Boolean, objState.Include);
//            db.ExecuteNonQuery(dbCommand, trans);
//        }
//#endregion
        //static public void Delete(State objState)
        //{
			
        //    Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
        //    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateDelete");
        //    db.AddInParameter(dbCommand, "ID", DbType.Int32, objState.ID);
        //    db.ExecuteNonQuery(dbCommand);
        //}
	}
}
