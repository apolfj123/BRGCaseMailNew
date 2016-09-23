using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the NCOAImport table.
	/// Author: ATP
	/// Date Created: 10/10/2012 5:09:55 PM
	/// </summary>
	public class NCOAImportService 
	{
		static private string _selectViewSQL  = "Select CASEID, INPUTFIRST, INPUTMIDDLE, INPUTLAST, INPUTSUFFIX, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, NEWADDRESS_FLAG, MRT_FLAG, MAILTO1_ADDRESS, MAILTO2_ADDRESS, MAILTO_CITY, MAILTO_STATE, MAILTO_ZIP, MAIL_ZIP4, MAILTO_DPBC, MAIL_CARRIER_ROUTE, DPVCONFIRMATION_INDICATOR, NCOA_MATCH_TYPE, NCOA_MOVE_DATE, DELIVERY_CODE, ZIP4_UPDATES, MAILABILITY_SCORE from NCOAImport";
		static public  List<NCOAImport> GetAll()
		{
			List<NCOAImport> objNCOAImports = new List<NCOAImport>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					NCOAImport objNCOAImport = new NCOAImport();
					LoadNCOAImport(objNCOAImport, reader);
					objNCOAImport.IsModified = false;
					objNCOAImports.Add (objNCOAImport);
				}
				// always call Close when done reading.
				reader.Close();
				return objNCOAImports;
			}
		}
		static public NCOAImport GetByID(int CASEID)
		{
			NCOAImport objNCOAImport = new NCOAImport();
			string query = _selectViewSQL + " where CASEID = " + CASEID;
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadNCOAImport(objNCOAImport, reader);
					// always call Close when done reading.
					reader.Close();
					objNCOAImport.IsModified = false;
					return objNCOAImport;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadNCOAImport(NCOAImport objNCOAImport, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objNCOAImport.CASEID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objNCOAImport.INPUTFIRST = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objNCOAImport.INPUTMIDDLE = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objNCOAImport.INPUTLAST = _reader.GetString(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objNCOAImport.INPUTSUFFIX = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objNCOAImport.ADDRESS1 = _reader.GetString(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objNCOAImport.ADDRESS2 = _reader.GetString(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objNCOAImport.CITY = _reader.GetString(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objNCOAImport.STATE = _reader.GetString(8);
			}
			if (_reader.IsDBNull(9) != true)
			{
				objNCOAImport.ZIP = _reader.GetString(9);
			}
			if (_reader.IsDBNull(10) != true)
			{
				objNCOAImport.NEWADDRESS_FLAG = _reader.GetString(10);
			}
			if (_reader.IsDBNull(11) != true)
			{
				objNCOAImport.MRT_FLAG = _reader.GetString(11);
			}
			if (_reader.IsDBNull(12) != true)
			{
				objNCOAImport.MAILTO1_ADDRESS = _reader.GetString(12);
			}
			if (_reader.IsDBNull(13) != true)
			{
				objNCOAImport.MAILTO2_ADDRESS = _reader.GetString(13);
			}
			if (_reader.IsDBNull(14) != true)
			{
				objNCOAImport.MAILTO_CITY = _reader.GetString(14);
			}
			if (_reader.IsDBNull(15) != true)
			{
				objNCOAImport.MAILTO_STATE = _reader.GetString(15);
			}
			if (_reader.IsDBNull(16) != true)
			{
				objNCOAImport.MAILTO_ZIP = _reader.GetString(16);
			}
			if (_reader.IsDBNull(17) != true)
			{
				objNCOAImport.MAIL_ZIP4 = _reader.GetString(17);
			}
			if (_reader.IsDBNull(18) != true)
			{
				objNCOAImport.MAILTO_DPBC = _reader.GetString(18);
			}
			if (_reader.IsDBNull(19) != true)
			{
				objNCOAImport.MAIL_CARRIER_ROUTE = _reader.GetString(19);
			}
			if (_reader.IsDBNull(20) != true)
			{
				objNCOAImport.DPVCONFIRMATION_INDICATOR = _reader.GetString(20);
			}
			if (_reader.IsDBNull(21) != true)
			{
				objNCOAImport.NCOA_MATCH_TYPE = _reader.GetString(21);
			}
			if (_reader.IsDBNull(22) != true)
			{
				objNCOAImport.NCOA_MOVE_DATE = _reader.GetString(22);
			}
			if (_reader.IsDBNull(23) != true)
			{
				objNCOAImport.DELIVERY_CODE = _reader.GetString(23);
			}
			if (_reader.IsDBNull(24) != true)
			{
				objNCOAImport.ZIP4_UPDATES = _reader.GetString(24);
			}
			if (_reader.IsDBNull(25) != true)
			{
				objNCOAImport.MAILABILITY_SCORE = _reader.GetString(25);
			}
		}
		static public void Save(NCOAImport objNCOAImport)
		{
			if (objNCOAImport.IsModified == true)
			{
				//if (objNCOAImport.CASEID == 0 )
				//{
					Insert(objNCOAImport);
				//}
				//else
				//{
				//    Update(objNCOAImport);
				//}
			}
		}
		static private void Insert(NCOAImport objNCOAImport)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_NCOAImportInsert");
			if (objNCOAImport.CASEID > 0)
			{
				db.AddInParameter(dbCommand, "CASEID", DbType.Int32, objNCOAImport.CASEID);
			}
			else
			{
				db.AddInParameter(dbCommand, "CASEID", DbType.Int32, Convert.DBNull);
			}
				db.AddInParameter(dbCommand, "INPUTFIRST", DbType.String, objNCOAImport.INPUTFIRST);
				db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.String, objNCOAImport.INPUTMIDDLE);
			//if (objNCOAImport.INPUTMIDDLE > 0)
			//{
			//    db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.Int32, objNCOAImport.INPUTMIDDLE);
			//}
			//else
			//{
			//    db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.Int32, Convert.DBNull);
			//}
				db.AddInParameter(dbCommand, "INPUTLAST", DbType.String, objNCOAImport.INPUTLAST);
				db.AddInParameter(dbCommand, "INPUTSUFFIX", DbType.String, objNCOAImport.INPUTSUFFIX);
				db.AddInParameter(dbCommand, "ADDRESS1", DbType.String, objNCOAImport.ADDRESS1);
				db.AddInParameter(dbCommand, "ADDRESS2", DbType.String, objNCOAImport.ADDRESS2);
				db.AddInParameter(dbCommand, "CITY", DbType.String, objNCOAImport.CITY);
				db.AddInParameter(dbCommand, "STATE", DbType.String, objNCOAImport.STATE);
				db.AddInParameter(dbCommand, "ZIP", DbType.String, objNCOAImport.ZIP);
				db.AddInParameter(dbCommand, "NEWADDRESS_FLAG", DbType.String, objNCOAImport.NEWADDRESS_FLAG);
				db.AddInParameter(dbCommand, "MRT_FLAG", DbType.String, objNCOAImport.MRT_FLAG);
				db.AddInParameter(dbCommand, "MAILTO1_ADDRESS", DbType.String, objNCOAImport.MAILTO1_ADDRESS);
				db.AddInParameter(dbCommand, "MAILTO2_ADDRESS", DbType.String, objNCOAImport.MAILTO2_ADDRESS);
				db.AddInParameter(dbCommand, "MAILTO_CITY", DbType.String, objNCOAImport.MAILTO_CITY);
				db.AddInParameter(dbCommand, "MAILTO_STATE", DbType.String, objNCOAImport.MAILTO_STATE);
				db.AddInParameter(dbCommand, "MAILTO_ZIP", DbType.String, objNCOAImport.MAILTO_ZIP);
				db.AddInParameter(dbCommand, "MAIL_ZIP4", DbType.String, objNCOAImport.MAIL_ZIP4);
				db.AddInParameter(dbCommand, "MAILTO_DPBC", DbType.String, objNCOAImport.MAILTO_DPBC);
				db.AddInParameter(dbCommand, "MAIL_CARRIER_ROUTE", DbType.String, objNCOAImport.MAIL_CARRIER_ROUTE);
				db.AddInParameter(dbCommand, "DPVCONFIRMATION_INDICATOR", DbType.String, objNCOAImport.DPVCONFIRMATION_INDICATOR);
				db.AddInParameter(dbCommand, "NCOA_MATCH_TYPE", DbType.String, objNCOAImport.NCOA_MATCH_TYPE);
				db.AddInParameter(dbCommand, "NCOA_MOVE_DATE", DbType.String, objNCOAImport.NCOA_MOVE_DATE);
				db.AddInParameter(dbCommand, "DELIVERY_CODE", DbType.String, objNCOAImport.DELIVERY_CODE);
				db.AddInParameter(dbCommand, "ZIP4_UPDATES", DbType.String, objNCOAImport.ZIP4_UPDATES);
				db.AddInParameter(dbCommand, "MAILABILITY_SCORE", DbType.String, objNCOAImport.MAILABILITY_SCORE);
			db.ExecuteNonQuery(dbCommand);
			objNCOAImport.CASEID = Int32.Parse(db.GetParameterValue(dbCommand, "CASEID").ToString());
		}
		static private void Update(NCOAImport objNCOAImport)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_NCOAImportUpdate");
			if (objNCOAImport.CASEID > 0)
			{
				db.AddInParameter(dbCommand, "CASEID", DbType.Int32, objNCOAImport.CASEID);
			}
			else
			{
				db.AddInParameter(dbCommand, "CASEID", DbType.Int32, Convert.DBNull);
			}
			db.AddInParameter(dbCommand, "INPUTFIRST", DbType.String, objNCOAImport.INPUTFIRST);
			db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.String, objNCOAImport.INPUTMIDDLE);
			//if (objNCOAImport.INPUTMIDDLE > 0)
			//{
			//    db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.Int32, objNCOAImport.INPUTMIDDLE);
			//}
			//else
			//{
			//    db.AddInParameter(dbCommand, "INPUTMIDDLE", DbType.Int32, Convert.DBNull);
			//}
			db.AddInParameter(dbCommand, "INPUTLAST", DbType.String, objNCOAImport.INPUTLAST);
			db.AddInParameter(dbCommand, "INPUTSUFFIX", DbType.String, objNCOAImport.INPUTSUFFIX);
			db.AddInParameter(dbCommand, "ADDRESS1", DbType.String, objNCOAImport.ADDRESS1);
			db.AddInParameter(dbCommand, "ADDRESS2", DbType.String, objNCOAImport.ADDRESS2);
			db.AddInParameter(dbCommand, "CITY", DbType.String, objNCOAImport.CITY);
			db.AddInParameter(dbCommand, "STATE", DbType.String, objNCOAImport.STATE);
			db.AddInParameter(dbCommand, "ZIP", DbType.String, objNCOAImport.ZIP);
			db.AddInParameter(dbCommand, "NEWADDRESS_FLAG", DbType.String, objNCOAImport.NEWADDRESS_FLAG);
			db.AddInParameter(dbCommand, "MRT_FLAG", DbType.String, objNCOAImport.MRT_FLAG);
			db.AddInParameter(dbCommand, "MAILTO1_ADDRESS", DbType.String, objNCOAImport.MAILTO1_ADDRESS);
			db.AddInParameter(dbCommand, "MAILTO2_ADDRESS", DbType.String, objNCOAImport.MAILTO2_ADDRESS);
			db.AddInParameter(dbCommand, "MAILTO_CITY", DbType.String, objNCOAImport.MAILTO_CITY);
			db.AddInParameter(dbCommand, "MAILTO_STATE", DbType.String, objNCOAImport.MAILTO_STATE);
			db.AddInParameter(dbCommand, "MAILTO_ZIP", DbType.String, objNCOAImport.MAILTO_ZIP);
			db.AddInParameter(dbCommand, "MAIL_ZIP4", DbType.String, objNCOAImport.MAIL_ZIP4);
			db.AddInParameter(dbCommand, "MAILTO_DPBC", DbType.String, objNCOAImport.MAILTO_DPBC);
			db.AddInParameter(dbCommand, "MAIL_CARRIER_ROUTE", DbType.String, objNCOAImport.MAIL_CARRIER_ROUTE);
			db.AddInParameter(dbCommand, "DPVCONFIRMATION_INDICATOR", DbType.String, objNCOAImport.DPVCONFIRMATION_INDICATOR);
			db.AddInParameter(dbCommand, "NCOA_MATCH_TYPE", DbType.String, objNCOAImport.NCOA_MATCH_TYPE);
			db.AddInParameter(dbCommand, "NCOA_MOVE_DATE", DbType.String, objNCOAImport.NCOA_MOVE_DATE);
			db.AddInParameter(dbCommand, "DELIVERY_CODE", DbType.String, objNCOAImport.DELIVERY_CODE);
			db.AddInParameter(dbCommand, "ZIP4_UPDATES", DbType.String, objNCOAImport.ZIP4_UPDATES);
			db.AddInParameter(dbCommand, "MAILABILITY_SCORE", DbType.String, objNCOAImport.MAILABILITY_SCORE);
			db.ExecuteNonQuery(dbCommand);
		}
		static public void Delete(NCOAImport objNCOAImport)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_NCOAImportDelete");
			db.AddInParameter(dbCommand, "CASEID", DbType.Int32, objNCOAImport.CASEID);
			db.ExecuteNonQuery(dbCommand);
		}

		/// <summary>
		/// ATP: Delete all rows from table
		/// </summary>
		static public void Delete()
		{

			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_NCOAImportDeleteAll");
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
