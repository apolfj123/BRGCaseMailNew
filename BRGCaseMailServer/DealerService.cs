using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the Dealer table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/18/2011 4:53:49 PM
	/// </summary>
	public class DealerService 
	{
        static private string _selectViewSQL = "Select ID, CompanyName, AddrLine1, AddrLine2, City, StateCode, PostalCode, Country, PhoneNumber, Fax, email, WebSite, ContactForFlyers, ContactForAccounts, CurrentCustomer, CallSource, MaxDistance, Comment, Latitude, Longitude, DefaultNumberMailings from Dealer";
        static public List<Dealer> GetAll()
        {
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    Dealer objDealer = new Dealer();
                    LoadDealer(objDealer, reader);
                    objDealer.IsModified = false;
                    objDealers.Add(objDealer);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealers;
            }
        }
        static public Dealer GetByID(int ID)
        {
            Dealer objDealer = new Dealer();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDealer(objDealer, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDealer.IsModified = false;
                    return objDealer;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadDealer(Dealer objDealer, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDealer.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDealer.CompanyName = _reader.GetString(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDealer.AddrLine1 = _reader.GetString(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDealer.AddrLine2 = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDealer.City = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDealer.StateCode = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDealer.PostalCode = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDealer.Country = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDealer.PhoneNumber = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDealer.Fax = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDealer.email = _reader.GetString(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDealer.WebSite = _reader.GetString(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDealer.ContactForFlyers = _reader.GetString(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objDealer.ContactForAccounts = _reader.GetString(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDealer.CurrentCustomer = _reader.GetBoolean(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objDealer.CallSource = _reader.GetString(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objDealer.MaxDistance = _reader.GetDouble(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objDealer.Comment = _reader.GetString(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objDealer.Latitude = _reader.GetDouble(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objDealer.Longitude = _reader.GetDouble(19);
            }
            if (_reader.IsDBNull(20) != true)
            {
                objDealer.DefaultNumberMailings = _reader.GetInt32(20);
            }
        }
        static public List<Dealer> GetFiltered(string Name, bool CurrentOnly)
        {
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = _selectViewSQL + " Where ";
            if (Name.Length > 0)
            {
                _query += " CompanyName like '%" + Name + "%' and ";
            }
            if (CurrentOnly == true)
            {
                _query += " CurrentCustomer = 1 and ";
            }
            
            _query += " 1=1 ";

            _query += " order by CompanyName asc ";
            

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    Dealer objDealer = new Dealer();
                    LoadDealer(objDealer, reader);
                    objDealer.IsModified = false;
                    objDealers.Add(objDealer);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealers;
            }
        }
        static public List<Dealer> GetInRadius(double lat, double lng, double milesRadius)
        {
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");

            string _query = _selectViewSQL + " Where ";
            
            _query += " ( dbo.DistanceBetween(" + lat + ", " + lng + ", Latitude, Longitude) <  " + milesRadius + ") ";

            _query += " and CurrentCustomer = 1 ";
             
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    Dealer objDealer = new Dealer();
                    LoadDealer(objDealer, reader);
                    objDealer.IsModified = false;
                    objDealers.Add(objDealer);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealers;
            }
        }
        static public void Save(Dealer objDealer)
        {
            if (objDealer.IsModified == true)
            {
                if (objDealer.ID == 0)
                {
                    Insert(objDealer);
                }
                else
                {
                    Update(objDealer);
                }
            }
        }
        static private void Insert(Dealer objDealer)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "CompanyName", DbType.String, objDealer.CompanyName);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objDealer.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objDealer.AddrLine2);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objDealer.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.String, objDealer.PostalCode);
            db.AddInParameter(dbCommand, "Country", DbType.String, objDealer.Country);
            db.AddInParameter(dbCommand, "PhoneNumber", DbType.String, objDealer.PhoneNumber);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "email", DbType.String, objDealer.email);
            db.AddInParameter(dbCommand, "WebSite", DbType.String, objDealer.WebSite);
            db.AddInParameter(dbCommand, "ContactForFlyers", DbType.String, objDealer.ContactForFlyers);
            db.AddInParameter(dbCommand, "ContactForAccounts", DbType.String, objDealer.ContactForAccounts);
            db.AddInParameter(dbCommand, "CurrentCustomer", DbType.Boolean, objDealer.CurrentCustomer);
            db.AddInParameter(dbCommand, "CallSource", DbType.String, objDealer.CallSource);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDealer.Comment);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "DefaultNumberMailings", DbType.Int32, objDealer.DefaultNumberMailings);
            db.ExecuteNonQuery(dbCommand);
            objDealer.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Dealer objDealer)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
            db.AddInParameter(dbCommand, "CompanyName", DbType.String, objDealer.CompanyName);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objDealer.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objDealer.AddrLine2);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objDealer.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.String, objDealer.PostalCode);
            db.AddInParameter(dbCommand, "Country", DbType.String, objDealer.Country);
            db.AddInParameter(dbCommand, "PhoneNumber", DbType.String, objDealer.PhoneNumber);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "email", DbType.String, objDealer.email);
            db.AddInParameter(dbCommand, "WebSite", DbType.String, objDealer.WebSite);
            db.AddInParameter(dbCommand, "ContactForFlyers", DbType.String, objDealer.ContactForFlyers);
            db.AddInParameter(dbCommand, "ContactForAccounts", DbType.String, objDealer.ContactForAccounts);
            db.AddInParameter(dbCommand, "CurrentCustomer", DbType.Boolean, objDealer.CurrentCustomer);
            db.AddInParameter(dbCommand, "CallSource", DbType.String, objDealer.CallSource);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDealer.Comment);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "DefaultNumberMailings", DbType.Int32, objDealer.DefaultNumberMailings);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(Dealer objDealer, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objDealer.IsModified == true)
                {
                    if (objDealer.ID == 0)
                    {
                        Insert(objDealer, db, trans);
                    }
                    else
                    {
                        Update(objDealer, db, trans);
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
        static private void Insert(Dealer objDealer, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "CompanyName", DbType.String, objDealer.CompanyName);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objDealer.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objDealer.AddrLine2);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objDealer.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.String, objDealer.PostalCode);
            db.AddInParameter(dbCommand, "Country", DbType.String, objDealer.Country);
            db.AddInParameter(dbCommand, "PhoneNumber", DbType.String, objDealer.PhoneNumber);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "email", DbType.String, objDealer.email);
            db.AddInParameter(dbCommand, "WebSite", DbType.String, objDealer.WebSite);
            db.AddInParameter(dbCommand, "ContactForFlyers", DbType.String, objDealer.ContactForFlyers);
            db.AddInParameter(dbCommand, "ContactForAccounts", DbType.String, objDealer.ContactForAccounts);
            db.AddInParameter(dbCommand, "CurrentCustomer", DbType.Boolean, objDealer.CurrentCustomer);
            db.AddInParameter(dbCommand, "CallSource", DbType.String, objDealer.CallSource);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDealer.Comment);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "DefaultNumberMailings", DbType.Int32, objDealer.DefaultNumberMailings);
            db.ExecuteNonQuery(dbCommand, trans);
            objDealer.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Dealer objDealer, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
            db.AddInParameter(dbCommand, "CompanyName", DbType.String, objDealer.CompanyName);
            db.AddInParameter(dbCommand, "AddrLine1", DbType.String, objDealer.AddrLine1);
            db.AddInParameter(dbCommand, "AddrLine2", DbType.String, objDealer.AddrLine2);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "StateCode", DbType.String, objDealer.StateCode);
            db.AddInParameter(dbCommand, "PostalCode", DbType.String, objDealer.PostalCode);
            db.AddInParameter(dbCommand, "Country", DbType.String, objDealer.Country);
            db.AddInParameter(dbCommand, "PhoneNumber", DbType.String, objDealer.PhoneNumber);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "email", DbType.String, objDealer.email);
            db.AddInParameter(dbCommand, "WebSite", DbType.String, objDealer.WebSite);
            db.AddInParameter(dbCommand, "ContactForFlyers", DbType.String, objDealer.ContactForFlyers);
            db.AddInParameter(dbCommand, "ContactForAccounts", DbType.String, objDealer.ContactForAccounts);
            db.AddInParameter(dbCommand, "CurrentCustomer", DbType.Boolean, objDealer.CurrentCustomer);
            db.AddInParameter(dbCommand, "CallSource", DbType.String, objDealer.CallSource);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDealer.Comment);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "DefaultNumberMailings", DbType.Int32, objDealer.DefaultNumberMailings);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(Dealer objDealer)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
			db.ExecuteNonQuery(dbCommand);
        }

        #region DealerZipCodes
        
        static public void AddDealerZipCode(DealerZipCode objDealerZipCode)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodeInsert");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerZipCode.DealerID);
            db.AddInParameter(dbCommand, "ZipGeoCodeID", DbType.Int32, objDealerZipCode.ZipGeoCodeID);
            db.ExecuteNonQuery(dbCommand);
        }
        
        static public void DeleteDealerZipCode(DealerZipCode objDealerZipCode)
        {

            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodeDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerZipCode.DealerID);
            db.AddInParameter(dbCommand, "ZipGeoCodeID", DbType.Int32, objDealerZipCode.ZipGeoCodeID);
            db.ExecuteNonQuery(dbCommand);
        }

        static public void DeleteDealerZipCodes(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodesDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);
        }

        #endregion
    }
}
