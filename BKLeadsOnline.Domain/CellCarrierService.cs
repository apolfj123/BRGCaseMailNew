using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the CellCarrier table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:28 PM
	/// </summary>
	public class CellCarrierService 
	{
		static private string _selectViewSQL  = "Select ID, Name, TxtEmailSuffix, Active from CellCarrier";
		static public  List<CellCarrier> GetAll()
		{
			List<CellCarrier> objCellCarriers = new List<CellCarrier>();
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					CellCarrier objCellCarrier = new CellCarrier();
					LoadCellCarrier(objCellCarrier, reader);
					objCellCarrier.IsModified = false;
					objCellCarriers.Add (objCellCarrier);
				}
				// always call Close when done reading.
				reader.Close();
				return objCellCarriers;
			}
		}
		static public CellCarrier GetByID(int ID)
		{
			CellCarrier objCellCarrier = new CellCarrier();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadCellCarrier(objCellCarrier, reader);
					// always call Close when done reading.
					reader.Close();
					objCellCarrier.IsModified = false;
					return objCellCarrier;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadCellCarrier(CellCarrier objCellCarrier, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objCellCarrier.ID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objCellCarrier.Name = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objCellCarrier.TxtEmailSuffix = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objCellCarrier.Active = _reader.GetBoolean(3);
			}
		}
		static public void Save(CellCarrier objCellCarrier)
		{
			if (objCellCarrier.IsModified == true)
			{
				if (objCellCarrier.ID == 0 )
				{
					Insert(objCellCarrier);
				}
				else
				{
					Update(objCellCarrier);
				}
			}
		}
		static private void Insert(CellCarrier objCellCarrier)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CellCarrierInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
				db.AddInParameter(dbCommand, "Name", DbType.String, objCellCarrier.Name);
				db.AddInParameter(dbCommand, "TxtEmailSuffix", DbType.String, objCellCarrier.TxtEmailSuffix);
				db.AddInParameter(dbCommand, "Active", DbType.Boolean, objCellCarrier.Active);
			db.ExecuteNonQuery(dbCommand);
			objCellCarrier.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(CellCarrier objCellCarrier)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CellCarrierUpdate");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objCellCarrier.ID);
			db.AddInParameter(dbCommand, "Name", DbType.String, objCellCarrier.Name);
			db.AddInParameter(dbCommand, "TxtEmailSuffix", DbType.String, objCellCarrier.TxtEmailSuffix);
			db.AddInParameter(dbCommand, "Active", DbType.Boolean, objCellCarrier.Active);
			db.ExecuteNonQuery(dbCommand);
		}
#region save, insert, update using transactions
		static public void Save(CellCarrier objCellCarrier, Database db, DbTransaction trans, bool CommitTrans)
		{
			try
			{
				if (objCellCarrier.IsModified == true)
				{
					if (objCellCarrier.ID == 0 )
					{
						Insert(objCellCarrier, db, trans);
					}
					else
					{
						Update(objCellCarrier, db, trans);
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
		static private void Insert(CellCarrier objCellCarrier, Database db, DbTransaction trans)
		{
			
			//Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CellCarrierInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
				db.AddInParameter(dbCommand, "Name", DbType.String, objCellCarrier.Name);
				db.AddInParameter(dbCommand, "TxtEmailSuffix", DbType.String, objCellCarrier.TxtEmailSuffix);
				db.AddInParameter(dbCommand, "Active", DbType.Boolean, objCellCarrier.Active);
			db.ExecuteNonQuery(dbCommand, trans);
			objCellCarrier.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(CellCarrier objCellCarrier, Database db, DbTransaction trans)
		{
			
			//Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CellCarrierUpdate");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objCellCarrier.ID);
			db.AddInParameter(dbCommand, "Name", DbType.String, objCellCarrier.Name);
			db.AddInParameter(dbCommand, "TxtEmailSuffix", DbType.String, objCellCarrier.TxtEmailSuffix);
			db.AddInParameter(dbCommand, "Active", DbType.Boolean, objCellCarrier.Active);
			db.ExecuteNonQuery(dbCommand, trans);
		}
#endregion
		static public void Delete(CellCarrier objCellCarrier)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_CellCarrierDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objCellCarrier.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
