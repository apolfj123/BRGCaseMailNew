using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the PacerFileFormatField table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/26/2011 11:48:51 AM
	/// </summary>
	public class PacerFileFormatFieldService 
	{
		static private string _selectViewSQL  = "Select ID, PacerFileFormatID, PacerFieldID, FieldOrder, PacerFileVersion, PacerFieldName from v_PacerFileFormatField";
		static public  List<PacerFileFormatField> GetAll()
		{
			List<PacerFileFormatField> objPacerFileFormatFields = new List<PacerFileFormatField>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					PacerFileFormatField objPacerFileFormatField = new PacerFileFormatField();
					LoadPacerFileFormatField(objPacerFileFormatField, reader);
					objPacerFileFormatField.IsModified = false;
					objPacerFileFormatFields.Add (objPacerFileFormatField);
				}
				// always call Close when done reading.
				reader.Close();
				return objPacerFileFormatFields;
			}
		}
        static public List<PacerFileFormatField> GetForPacerFileFormat(int PacerFileFormatID)
        {
            List<PacerFileFormatField> objPacerFileFormatFields = new List<PacerFileFormatField>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " Where PacerFileFormatID = " + PacerFileFormatID + " order by FieldOrder asc"))
            {
                while (reader.Read())
                {
                    PacerFileFormatField objPacerFileFormatField = new PacerFileFormatField();
                    LoadPacerFileFormatField(objPacerFileFormatField, reader);
                    objPacerFileFormatField.IsModified = false;
                    objPacerFileFormatFields.Add(objPacerFileFormatField);
                }
                // always call Close when done reading.
                reader.Close();
                return objPacerFileFormatFields;
            }
        }
		static public PacerFileFormatField GetByID(int ID)
		{
			PacerFileFormatField objPacerFileFormatField = new PacerFileFormatField();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadPacerFileFormatField(objPacerFileFormatField, reader);
					// always call Close when done reading.
					reader.Close();
					objPacerFileFormatField.IsModified = false;
					return objPacerFileFormatField;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadPacerFileFormatField(PacerFileFormatField objPacerFileFormatField, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objPacerFileFormatField.ID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objPacerFileFormatField.PacerFileFormatID = _reader.GetInt32(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objPacerFileFormatField.PacerFieldID = _reader.GetInt32(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objPacerFileFormatField.FieldOrder = _reader.GetInt32(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objPacerFileFormatField.PacerFileVersion = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objPacerFileFormatField.PacerFieldName = _reader.GetString(5);
			}
		}
		static public void Save(PacerFileFormatField objPacerFileFormatField)
		{
			if (objPacerFileFormatField.IsModified == true)
			{
				if (objPacerFileFormatField.ID == 0 )
				{
					Insert(objPacerFileFormatField);
				}
				else
				{
					Update(objPacerFileFormatField);
				}
			}
		}
		static private void Insert(PacerFileFormatField objPacerFileFormatField)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatFieldInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
			if (objPacerFileFormatField.PacerFileFormatID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerFileFormatField.PacerFileFormatID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
			}
			if (objPacerFileFormatField.PacerFieldID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, objPacerFileFormatField.PacerFieldID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "FieldOrder", DbType.Int32, objPacerFileFormatField.FieldOrder);
				db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormatField.PacerFileVersion);
				db.AddInParameter(dbCommand, "PacerFieldName", DbType.String, objPacerFileFormatField.PacerFieldName);
			db.ExecuteNonQuery(dbCommand);
			objPacerFileFormatField.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(PacerFileFormatField objPacerFileFormatField)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatFieldUpdate");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerFileFormatField.ID);
			if (objPacerFileFormatField.PacerFileFormatID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerFileFormatField.PacerFileFormatID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
			}
			if (objPacerFileFormatField.PacerFieldID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, objPacerFileFormatField.PacerFieldID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, Convert.DBNull);
			}
			db.AddInParameter(dbCommand, "FieldOrder", DbType.Int32, objPacerFileFormatField.FieldOrder);
			db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormatField.PacerFileVersion);
			db.AddInParameter(dbCommand, "PacerFieldName", DbType.String, objPacerFileFormatField.PacerFieldName);
			db.ExecuteNonQuery(dbCommand);
		}
#region save, insert, update using transactions
		static public void Save(PacerFileFormatField objPacerFileFormatField, Database db, DbTransaction trans, bool CommitTrans)
		{
			try
			{
				if (objPacerFileFormatField.IsModified == true)
				{
					if (objPacerFileFormatField.ID == 0 )
					{
						Insert(objPacerFileFormatField, db, trans);
					}
					else
					{
						Update(objPacerFileFormatField, db, trans);
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
		static private void Insert(PacerFileFormatField objPacerFileFormatField, Database db, DbTransaction trans)
		{
			
			//Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatFieldInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
			if (objPacerFileFormatField.PacerFileFormatID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerFileFormatField.PacerFileFormatID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
			}
			if (objPacerFileFormatField.PacerFieldID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, objPacerFileFormatField.PacerFieldID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "FieldOrder", DbType.Int32, objPacerFileFormatField.FieldOrder);
				db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormatField.PacerFileVersion);
				db.AddInParameter(dbCommand, "PacerFieldName", DbType.String, objPacerFileFormatField.PacerFieldName);
			db.ExecuteNonQuery(dbCommand, trans);
			objPacerFileFormatField.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(PacerFileFormatField objPacerFileFormatField, Database db, DbTransaction trans)
		{
			
			//Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatFieldUpdate");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerFileFormatField.ID);
			if (objPacerFileFormatField.PacerFileFormatID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, objPacerFileFormatField.PacerFileFormatID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFileFormatID", DbType.Int32, Convert.DBNull);
			}
			if (objPacerFileFormatField.PacerFieldID > 0)
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, objPacerFileFormatField.PacerFieldID);
			}
			else
			{
				db.AddInParameter(dbCommand, "PacerFieldID", DbType.Int32, Convert.DBNull);
			}
			db.AddInParameter(dbCommand, "FieldOrder", DbType.Int32, objPacerFileFormatField.FieldOrder);
			db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormatField.PacerFileVersion);
			db.AddInParameter(dbCommand, "PacerFieldName", DbType.String, objPacerFileFormatField.PacerFieldName);
			db.ExecuteNonQuery(dbCommand, trans);
		}
#endregion
		static public void Delete(PacerFileFormatField objPacerFileFormatField)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatFieldDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerFileFormatField.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
