using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the State table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/27/2013 7:10:20 PM
	/// </summary>
	public class StateService 
	{
		static private string _selectViewSQL  = "Select StateCode, StateName, Include, ProductAvailable from State";
		static public  List<State> GetAll()
		{
			List<State> objStates = new List<State>();
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        static public List<State> GetAllIncluded()
        {
            List<State> objStates = new List<State>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        static public State GetByStateCode(string _stateCode)
		{
			State objState = new State();
            string query = _selectViewSQL + " where StateCode = '" + _stateCode + "'";
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
			if (_reader.IsDBNull(3) != true)
			{
				objState.ProductAvailable = _reader.GetBoolean(3);
			}
		}
		static public void Update(State objState)
		{
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_StateUpdate");
			db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
			db.AddInParameter(dbCommand, "StateName", DbType.String, objState.StateName);
			db.AddInParameter(dbCommand, "Include", DbType.Boolean, objState.Include);
			db.AddInParameter(dbCommand, "ProductAvailable", DbType.Boolean, objState.ProductAvailable);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
