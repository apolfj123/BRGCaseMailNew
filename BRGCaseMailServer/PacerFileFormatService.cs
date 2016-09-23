using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the PacerFileFormat table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/27/2011 11:32:28 AM
	/// </summary>
	public class PacerFileFormatService 
	{
		static private string _selectViewSQL  = "Select ID, PacerFileVersion, NumberColumns from PacerFileFormat";
		static public  List<PacerFileFormat> GetAll()
		{
			List<PacerFileFormat> objPacerFileFormats = new List<PacerFileFormat>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					PacerFileFormat objPacerFileFormat = new PacerFileFormat();
					LoadPacerFileFormat(objPacerFileFormat, reader);
					objPacerFileFormat.IsModified = false;
					objPacerFileFormats.Add (objPacerFileFormat);
				}
				// always call Close when done reading.
				reader.Close();
				return objPacerFileFormats;
			}
		}
		static public PacerFileFormat GetByID(int ID)
		{
			PacerFileFormat objPacerFileFormat = new PacerFileFormat();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadPacerFileFormat(objPacerFileFormat, reader);
					// always call Close when done reading.
					reader.Close();
					objPacerFileFormat.IsModified = false;
					return objPacerFileFormat;
				}
				else
				{
					return null;
				}
			}
		}
        static public PacerFileFormat GetByVersionString(string _version)
        {
            PacerFileFormat objPacerFileFormat = new PacerFileFormat();
            string query = _selectViewSQL + " where PacerFileVersion = '" + _version + "'";
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadPacerFileFormat(objPacerFileFormat, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objPacerFileFormat.IsModified = false;
                    return objPacerFileFormat;
                }
                else
                {
                    return null;
                }
            }
        }
		static private void LoadPacerFileFormat(PacerFileFormat objPacerFileFormat, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objPacerFileFormat.ID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objPacerFileFormat.PacerFileVersion = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objPacerFileFormat.NumberColumns = _reader.GetInt32(2);
			}
		}
		static public void Save(PacerFileFormat objPacerFileFormat)
		{
			if (objPacerFileFormat.IsModified == true)
			{
				if (objPacerFileFormat.ID == 0 )
				{
					Insert(objPacerFileFormat);
				}
				else
				{
					Update(objPacerFileFormat);
				}
			}
		}
		static private void Insert(PacerFileFormat objPacerFileFormat)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
				db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormat.PacerFileVersion);
				db.AddInParameter(dbCommand, "NumberColumns", DbType.Int32, objPacerFileFormat.NumberColumns);
			db.ExecuteNonQuery(dbCommand);
			objPacerFileFormat.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(PacerFileFormat objPacerFileFormat)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatUpdate");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerFileFormat.ID);
			db.AddInParameter(dbCommand, "PacerFileVersion", DbType.String, objPacerFileFormat.PacerFileVersion);
			db.AddInParameter(dbCommand, "NumberColumns", DbType.Int32, objPacerFileFormat.NumberColumns);
			db.ExecuteNonQuery(dbCommand);
		}
		static public void Delete(PacerFileFormat objPacerFileFormat)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_PacerFileFormatDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objPacerFileFormat.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
