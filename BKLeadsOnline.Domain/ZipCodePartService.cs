using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the ZipCodePart table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:28 PM
	/// </summary>
	public class ZipCodePartService 
	{
		static private string _selectViewSQL  = "Select StateCode, ZipPart from ZipCodePart";
		static public  List<ZipCodePart> GetAll()
		{
			List<ZipCodePart> objZipCodeParts = new List<ZipCodePart>();
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					ZipCodePart objZipCodePart = new ZipCodePart();
					LoadZipCodePart(objZipCodePart, reader);
					objZipCodePart.IsModified = false;
					objZipCodeParts.Add (objZipCodePart);
				}
				// always call Close when done reading.
				reader.Close();
				return objZipCodeParts;
			}
		}
		static public ZipCodePart GetByID(int ID)
		{
			ZipCodePart objZipCodePart = new ZipCodePart();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadZipCodePart(objZipCodePart, reader);
					// always call Close when done reading.
					reader.Close();
					objZipCodePart.IsModified = false;
					return objZipCodePart;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadZipCodePart(ZipCodePart objZipCodePart, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objZipCodePart.StateCode = _reader.GetString(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objZipCodePart.ZipPart = _reader.GetString(1);
			}
		}
//        static public void Save(ZipCodePart objZipCodePart)
//        {
//            if (objZipCodePart.IsModified == true)
//            {
//                if (objZipCodePart.ID == 0 )
//                {
//                    Insert(objZipCodePart);
//                }
//                else
//                {
//                    Update(objZipCodePart);
//                }
//            }
//        }
//        static private void Insert(ZipCodePart objZipCodePart)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodePartInsert");
//                db.AddInParameter(dbCommand, "StateCode", DbType.String, objZipCodePart.StateCode);
//                db.AddInParameter(dbCommand, "ZipPart", DbType.String, objZipCodePart.ZipPart);
//            db.ExecuteNonQuery(dbCommand);
//            objZipCodePart.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(ZipCodePart objZipCodePart)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodePartUpdate");
//            db.AddInParameter(dbCommand, "StateCode", DbType.String, objZipCodePart.StateCode);
//            db.AddInParameter(dbCommand, "ZipPart", DbType.String, objZipCodePart.ZipPart);
//            db.ExecuteNonQuery(dbCommand);
//        }
//#region save, insert, update using transactions
//        static public void Save(ZipCodePart objZipCodePart, Database db, DbTransaction trans, bool CommitTrans)
//        {
//            try
//            {
//                if (objZipCodePart.IsModified == true)
//                {
//                    if (objZipCodePart.ID == 0 )
//                    {
//                        Insert(objZipCodePart, db, trans);
//                    }
//                    else
//                    {
//                        Update(objZipCodePart, db, trans);
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
//        static private void Insert(ZipCodePart objZipCodePart, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodePartInsert");
//                db.AddInParameter(dbCommand, "StateCode", DbType.String, objZipCodePart.StateCode);
//                db.AddInParameter(dbCommand, "ZipPart", DbType.String, objZipCodePart.ZipPart);
//            db.ExecuteNonQuery(dbCommand, trans);
//            objZipCodePart.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(ZipCodePart objZipCodePart, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodePartUpdate");
//            db.AddInParameter(dbCommand, "StateCode", DbType.String, objZipCodePart.StateCode);
//            db.AddInParameter(dbCommand, "ZipPart", DbType.String, objZipCodePart.ZipPart);
//            db.ExecuteNonQuery(dbCommand, trans);
//        }
//#endregion
//        static public void Delete(ZipCodePart objZipCodePart)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodePartDelete");
//            db.AddInParameter(dbCommand, "ID", DbType.Int32, objZipCodePart.ID);
//            db.ExecuteNonQuery(dbCommand);
//        }
	}
}
