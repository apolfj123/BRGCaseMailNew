using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the ZipGeoCode table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/26/2011 4:38:32 PM
	/// </summary>
	public class ZipGeoCodeService 
	{
        static private string _selectViewSQL = "Select ID, ZipCode, ZipCodeString, City, State, Latitude, Longitude, CasesAvailable, CasesMarkedSold from ZipGeoCode";
        static public List<ZipGeoCode> GetAll()
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        static public ZipGeoCode GetByID(int ID)
        {
            ZipGeoCode objZipGeoCode = new ZipGeoCode();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadZipGeoCode(objZipGeoCode, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objZipGeoCode.IsModified = false;
                    return objZipGeoCode;
                }
                else
                {
                    return null;
                }
            }
        }
		static private void LoadZipGeoCode(ZipGeoCode objZipGeoCode, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objZipGeoCode.ID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objZipGeoCode.ZipCode = _reader.GetInt32(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objZipGeoCode.ZipCodeString = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objZipGeoCode.City = _reader.GetString(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objZipGeoCode.State = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objZipGeoCode.Latitude = _reader.GetDouble(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objZipGeoCode.Longitude = _reader.GetDouble(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objZipGeoCode.CasesAvailable = _reader.GetInt32(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objZipGeoCode.CasesMarkedSold = _reader.GetInt32(8);
			}
		}
        static private void LoadZipGeoCodeWithDistance(ZipGeoCode objZipGeoCode, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objZipGeoCode.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objZipGeoCode.ZipCode = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objZipGeoCode.ZipCodeString = _reader.GetString(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objZipGeoCode.City = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objZipGeoCode.State = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objZipGeoCode.Latitude = _reader.GetDouble(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objZipGeoCode.Longitude = _reader.GetDouble(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objZipGeoCode.CasesAvailable = _reader.GetInt32(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objZipGeoCode.CasesMarkedSold = _reader.GetInt32(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objZipGeoCode.Distance = _reader.GetDouble(9);
            }
        }
        static public List<ZipGeoCode> GetForDealer(  int DealerID )
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = _selectViewSQL + " where ID in (select ZipGeoCodeID from DealerZipCode where DealerID = " + DealerID + ") ";
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        static public List<ZipGeoCode> GetForDealerInsideRadius(Dealer dealer)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = _selectViewSQL + " where ID in (select ZipGeoCodeID from DealerZipCode where DealerID = " + dealer.ID + ") AND ";
            _query += " ( dbo.DistanceBetween(" + dealer.Latitude + ", " + dealer.Longitude + ", Latitude, Longitude) <=  " + dealer.MaxDistance + ")";
            
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        static public List<ZipGeoCode> GetInsideRadius(Dealer dealer)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = _selectViewSQL + " where  ";
            _query += " ( dbo.DistanceBetween(" + dealer.Latitude + ", " + dealer.Longitude + ", Latitude, Longitude) <=  " + dealer.MaxDistance + ")";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        static public List<ZipGeoCode> GetForState(string state)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = _selectViewSQL + " where state = '" + state + "' order by ZipCode asc";
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        static public List<ZipGeoCode> GetForState(string state, float latitude, float longitude, float distance)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = "Select ID, ZipCode, ZipCodeString, City, State, Latitude, Longitude, CasesAvailable, CasesMarkedSold, dbo.DistanceBetween(" + latitude + ", " + longitude + ", Latitude, Longitude) as Distance from ZipGeoCode ";
            _query += " where State = '" + state + "' and ";
            _query += " dbo.DistanceBetween(" + latitude + ", " + longitude + ", Latitude, Longitude) <  " + distance + " order by ZipCode asc ";
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistance(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        
        static public List<ZipGeoCode> GetForZipPart(string zippart)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = _selectViewSQL + " where ZipCodeString like '" + zippart + "%' order by ZipCode asc";
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCode(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        
        static public List<ZipGeoCode> GetForZipPart(string zippart, float latitude, float longitude, float distance)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            string _query = "Select ID, ZipCode, ZipCodeString, City, State, Latitude, Longitude, CasesAvailable, CasesMarkedSold, dbo.DistanceBetween(" + latitude + ", " + longitude + ", Latitude, Longitude) as Distance from ZipGeoCode ";
            _query += " where ZipCodeString like '" + zippart + "%' and ";
            _query += " dbo.DistanceBetween(" + latitude + ", " + longitude + ", Latitude, Longitude) <  " + distance + " order by ZipCode asc ";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistance(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }

        static public void UpdateStatsForState(State objState)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UpdateZipCodeStats");
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objState.StateCode);
            db.ExecuteNonQuery(dbCommand);
        }

        static public void UpdateStatsForState(string StateCode)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UpdateZipCodeStats");
            dbCommand.CommandTimeout = 300;
            db.AddInParameter(dbCommand, "StateCode", DbType.String, StateCode);
            db.ExecuteNonQuery(dbCommand);
        }
       
        static public void UpdateStatsForZipPart(string ZipPart)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UpdateZipCodeStatsForZipPart");
            db.AddInParameter(dbCommand, "ZipPart", DbType.String, ZipPart);
            db.ExecuteNonQuery(dbCommand);
        }

        static public void UpdateStatsForDealer(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UpdateZipCodeStatsForDealer");
            dbCommand.CommandTimeout = 300;
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);
        }

        //        static public void Save(ZipGeoCode objZipGeoCode)
//        {
//            if (objZipGeoCode.IsModified == true)
//            {
//                if (objZipGeoCode.ID == 0 )
//                {
//                    Insert(objZipGeoCode);
//                }
//                else
//                {
//                    Update(objZipGeoCode);
//                }
//            }
//        }
//        static private void Insert(ZipGeoCode objZipGeoCode)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodeInsert");
//            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
//                db.AddInParameter(dbCommand, "ZipCode", DbType.String, objZipGeoCode.ZipCode);
//                db.AddInParameter(dbCommand, "City", DbType.String, objZipGeoCode.City);
//                db.AddInParameter(dbCommand, "State", DbType.String, objZipGeoCode.State);
//                db.AddInParameter(dbCommand, "County", DbType.String, objZipGeoCode.County);
//                db.AddInParameter(dbCommand, "AreaCode", DbType.String, objZipGeoCode.AreaCode);
//                db.AddInParameter(dbCommand, "Latitude", DbType.Double, objZipGeoCode.Latitude);
//                db.AddInParameter(dbCommand, "Longitude", DbType.Double, objZipGeoCode.Longitude);
//                db.AddInParameter(dbCommand, "CasesAvailable", DbType.Int32, objZipGeoCode.CasesAvailable);
//                db.AddInParameter(dbCommand, "CasesMarkedSold", DbType.Int32, objZipGeoCode.CasesMarkedSold);
//            db.ExecuteNonQuery(dbCommand);
//            objZipGeoCode.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(ZipGeoCode objZipGeoCode)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodeUpdate");
//            db.AddInParameter(dbCommand, "ID", DbType.Int32, objZipGeoCode.ID);
//            db.AddInParameter(dbCommand, "ZipCode", DbType.String, objZipGeoCode.ZipCode);
//            db.AddInParameter(dbCommand, "City", DbType.String, objZipGeoCode.City);
//            db.AddInParameter(dbCommand, "State", DbType.String, objZipGeoCode.State);
//            db.AddInParameter(dbCommand, "County", DbType.String, objZipGeoCode.County);
//            db.AddInParameter(dbCommand, "AreaCode", DbType.String, objZipGeoCode.AreaCode);
//            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objZipGeoCode.Latitude);
//            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objZipGeoCode.Longitude);
//            db.AddInParameter(dbCommand, "CasesAvailable", DbType.Int32, objZipGeoCode.CasesAvailable);
//            db.AddInParameter(dbCommand, "CasesMarkedSold", DbType.Int32, objZipGeoCode.CasesMarkedSold);
//            db.ExecuteNonQuery(dbCommand);
//        }
//#region save, insert, update using transactions
//        static public void Save(ZipGeoCode objZipGeoCode, Database db, DbTransaction trans, bool CommitTrans)
//        {
//            try
//            {
//                if (objZipGeoCode.IsModified == true)
//                {
//                    if (objZipGeoCode.ID == 0 )
//                    {
//                        Insert(objZipGeoCode, db, trans);
//                    }
//                    else
//                    {
//                        Update(objZipGeoCode, db, trans);
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
//        static private void Insert(ZipGeoCode objZipGeoCode, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodeInsert");
//            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
//                db.AddInParameter(dbCommand, "ZipCode", DbType.String, objZipGeoCode.ZipCode);
//                db.AddInParameter(dbCommand, "City", DbType.String, objZipGeoCode.City);
//                db.AddInParameter(dbCommand, "State", DbType.String, objZipGeoCode.State);
//                db.AddInParameter(dbCommand, "County", DbType.String, objZipGeoCode.County);
//                db.AddInParameter(dbCommand, "AreaCode", DbType.String, objZipGeoCode.AreaCode);
//                db.AddInParameter(dbCommand, "Latitude", DbType.Double, objZipGeoCode.Latitude);
//                db.AddInParameter(dbCommand, "Longitude", DbType.Double, objZipGeoCode.Longitude);
//                db.AddInParameter(dbCommand, "CasesAvailable", DbType.Int32, objZipGeoCode.CasesAvailable);
//                db.AddInParameter(dbCommand, "CasesMarkedSold", DbType.Int32, objZipGeoCode.CasesMarkedSold);
//            db.ExecuteNonQuery(dbCommand, trans);
//            objZipGeoCode.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
//        }
//        static private void Update(ZipGeoCode objZipGeoCode, Database db, DbTransaction trans)
//        {
			
//            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodeUpdate");
//            db.AddInParameter(dbCommand, "ID", DbType.Int32, objZipGeoCode.ID);
//            db.AddInParameter(dbCommand, "ZipCode", DbType.String, objZipGeoCode.ZipCode);
//            db.AddInParameter(dbCommand, "City", DbType.String, objZipGeoCode.City);
//            db.AddInParameter(dbCommand, "State", DbType.String, objZipGeoCode.State);
//            db.AddInParameter(dbCommand, "County", DbType.String, objZipGeoCode.County);
//            db.AddInParameter(dbCommand, "AreaCode", DbType.String, objZipGeoCode.AreaCode);
//            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objZipGeoCode.Latitude);
//            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objZipGeoCode.Longitude);
//            db.AddInParameter(dbCommand, "CasesAvailable", DbType.Int32, objZipGeoCode.CasesAvailable);
//            db.AddInParameter(dbCommand, "CasesMarkedSold", DbType.Int32, objZipGeoCode.CasesMarkedSold);
//            db.ExecuteNonQuery(dbCommand, trans);
//        }
//#endregion
//        static public void Delete(ZipGeoCode objZipGeoCode)
//        {
			
//            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
//            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodeDelete");
//            db.AddInParameter(dbCommand, "ID", DbType.Int32, objZipGeoCode.ID);
//            db.ExecuteNonQuery(dbCommand);
//        }
	
    }
}
