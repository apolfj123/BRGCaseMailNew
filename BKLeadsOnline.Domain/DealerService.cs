using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Diagnostics;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the Dealer table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:28 PM
	/// </summary>
	public class DealerService 
	{
        static private string _selectViewSQL = "Select ID, DealerName, DealerNumber, AddressLine1, City, State, Zip, Phone, Fax, Latitude, Longitude, MaxDistance, DealerURL, ExportFilePath, CreatedOn, LastUpdate, NumberOfMailers, StartDate, Term, DealerLogo, Price, AcknowledgedTOS, AcknowledgedTOSDate, AcknowledgedTOSName, AcknowledgedTOSSignature, DefaultDocTemplateID, LegalDisclaimer, Active, DealerPrimaryContactName, DealerPrimaryContactPhone, DefaultDocTemplateDisplayName from v_Dealer";
        static private void LoadDealer(Dealer objDealer, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDealer.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDealer.DealerName = _reader.GetString(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDealer.DealerNumber = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDealer.AddressLine1 = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDealer.City = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDealer.State = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDealer.Zip = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDealer.Phone = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDealer.Fax = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDealer.Latitude = _reader.GetDouble(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDealer.Longitude = _reader.GetDouble(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDealer.MaxDistance = _reader.GetDouble(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDealer.DealerURL = _reader.GetString(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objDealer.ExportFilePath = _reader.GetString(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDealer.CreatedOn = _reader.GetDateTime(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objDealer.LastUpdate = _reader.GetDateTime(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objDealer.NumberOfMailers = _reader.GetInt32(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objDealer.StartDate = _reader.GetDateTime(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objDealer.Term = _reader.GetString(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objDealer.DealerLogo = (byte[])_reader["DealerLogo"];
            }
            if (_reader.IsDBNull(20) != true)
            {
                objDealer.Price = _reader.GetDecimal(20);
            }
            if (_reader.IsDBNull(21) != true)
            {
                objDealer.AcknowledgedTOS = _reader.GetBoolean(21);
            }
            if (_reader.IsDBNull(22) != true)
            {
                objDealer.AcknowledgedTOSDate = _reader.GetDateTime(22);
            }
            if (_reader.IsDBNull(23) != true)
            {
                objDealer.AcknowledgedTOSName = _reader.GetString(23);
            }
            if (_reader.IsDBNull(24) != true)
            {
                objDealer.AcknowledgedTOSSignature = (byte[])_reader["AcknowledgedTOSSignature"];
            }
            if (_reader.IsDBNull(25) != true)
            {
                objDealer.DefaultDocTemplateID = _reader.GetInt32(25);
            }
            if (_reader.IsDBNull(26) != true)
            {
                objDealer.LegalDisclaimer = _reader.GetString(26);
            }
            if (_reader.IsDBNull(27) != true)
            {
                objDealer.Active = _reader.GetBoolean(27);
            }
            if (_reader.IsDBNull(28) != true)
            {
                objDealer.DealerPrimaryContactName = _reader.GetString(28);
            }
            if (_reader.IsDBNull(29) != true)
            {
                objDealer.DealerPrimaryContactPhone = _reader.GetString(29);
            }
            if (_reader.IsDBNull(30) != true)
            {
                objDealer.DefaultDocTemplateDisplayName = _reader.GetString(30);
            }
        }
        
        static public Dealer GetLogo(Dealer objDealer)
        {
            //Dealer objDealer = new Dealer();
            string query = "Select DealerLogo from Dealer where ID = " + objDealer.ID;
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader _reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (_reader.Read())
                {
                    if (_reader.IsDBNull(0) != true)
                    {
                        objDealer.DealerLogo = (byte[])_reader["DealerLogo"];
                    }
                    // always call Close when done reading.
                    _reader.Close();
                    objDealer.IsModified = false;
                    return objDealer;
                }
                else
                {
                    return null;
                }
            }
        }
        static private string _selectViewSQLPartial = "Select ID, DealerName, DealerNumber, AddressLine1, City, State, Zip, Phone, Fax, Latitude, Longitude, MaxDistance, DealerURL, ExportFilePath, CreatedOn, LastUpdate, NumberOfMailers, StartDate, Term, Price, AcknowledgedTOS, AcknowledgedTOSDate, AcknowledgedTOSName, DefaultDocTemplateID, LegalDisclaimer, Active, DealerPrimaryContactName, DealerPrimaryContactPhone, DefaultDocTemplateDisplayName from v_Dealer";
        static public List<Dealer> GetAll()
        {
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial))
            {
                while (reader.Read())
                {
                    Dealer objDealer = new Dealer();
                    LoadDealerPartial(objDealer, reader);
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
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        static public List<Dealer> GetFiltered(string Name, string Zip, bool ActiveOnly)
        {
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string query = _selectViewSQL;
            query += " Where ";

            if (Name != null && Name.Length > 0)
            {
                query += " DealerName like '%" + Name + "%' and ";
            }

            //Select * from v_Dealer Where  
            //(( Zip = '27202') or ( '27202' in (select zgc.ZipCode from ZipGeoCode zgc inner join 
            //DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID and dzc.DealerID =  
            //v_Dealer.ID )))  and  Active = 1 and  1 = 1  ORDER BY Name ASC

            if (Zip != null && Zip.Length > 0)
            {
                if (Zip.Length == 5)
                {
                    query += " (( Zip = '" + Zip + "') or ( '" + Zip + "' in (select zgc.ZipCode from ZipGeoCode zgc inner join DealerZipCode dzc on zgc.ID = dzc.ZipGeoCodeID and dzc.DealerID =  v_Dealer.ID )))  and ";
                }
                else
                {
                    query += " Zip like '" + Zip + "%' and ";
                }

            }
            if (ActiveOnly == true)
            {
                query += " Active = 1 and ";
            }

            query = query + " 1 = 1  ORDER BY DealerName ASC";


            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
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

        //we're looking for interesection radii
        static public List<Dealer> GetNearbyToDealer(int DealerID)
        {

            Dealer _dealer = GetByID(DealerID);
            List<Dealer> objDealers = new List<Dealer>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            string _query = _selectViewSQLPartial + " Where ";

            _query += " ( dbo.DistanceBetween(" + _dealer.Latitude + ", " + _dealer.Longitude + ", Latitude, Longitude) < (MaxDistance +  " + _dealer.MaxDistance + ")) ";

            _query += "  and ID <> " + DealerID;

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    Dealer objDealer = new Dealer();
                    LoadDealerPartial(objDealer, reader);
                    objDealer.IsModified = false;
                    objDealers.Add(objDealer);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealers;
            }
        }
        static private void LoadDealerPartial(Dealer objDealer, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDealer.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDealer.DealerName = _reader.GetString(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDealer.DealerNumber = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDealer.AddressLine1 = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDealer.City = _reader.GetString(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDealer.State = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDealer.Zip = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDealer.Phone = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDealer.Fax = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDealer.Latitude = _reader.GetDouble(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDealer.Longitude = _reader.GetDouble(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDealer.MaxDistance = _reader.GetDouble(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDealer.DealerURL = _reader.GetString(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objDealer.ExportFilePath = _reader.GetString(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDealer.CreatedOn = _reader.GetDateTime(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objDealer.LastUpdate = _reader.GetDateTime(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objDealer.NumberOfMailers = _reader.GetInt32(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objDealer.StartDate = _reader.GetDateTime(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objDealer.Term = _reader.GetString(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objDealer.Price = _reader.GetDecimal(19);
            }        
            if (_reader.IsDBNull(20) != true)
            {
                objDealer.AcknowledgedTOS = _reader.GetBoolean(20);
            }
            if (_reader.IsDBNull(21) != true)
            {
                objDealer.AcknowledgedTOSDate = _reader.GetDateTime(21);
            }
            if (_reader.IsDBNull(22) != true)
            {
                objDealer.AcknowledgedTOSName = _reader.GetString(22);
            }
            if (_reader.IsDBNull(23) != true)
            {
                objDealer.DefaultDocTemplateID = _reader.GetInt32(23);
            }
            if (_reader.IsDBNull(24) != true)
            {
                objDealer.LegalDisclaimer = _reader.GetString(24);
            }
            if (_reader.IsDBNull(25) != true)
            {
                objDealer.Active = _reader.GetBoolean(25);
            }
            if (_reader.IsDBNull(26) != true)
            {
                objDealer.DealerPrimaryContactName = _reader.GetString(26);
            }
            if (_reader.IsDBNull(27) != true)
            {
                objDealer.DealerPrimaryContactPhone = _reader.GetString(27);
            }
            if (_reader.IsDBNull(28) != true)
            {
                objDealer.DefaultDocTemplateDisplayName = _reader.GetString(28);
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

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "DealerName", DbType.String, objDealer.DealerName);
            db.AddInParameter(dbCommand, "DealerNumber", DbType.Int32, objDealer.DealerNumber);
            db.AddInParameter(dbCommand, "AddressLine1", DbType.String, objDealer.AddressLine1);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "State", DbType.String, objDealer.State);
            db.AddInParameter(dbCommand, "Zip", DbType.String, objDealer.Zip);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objDealer.Phone);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "DealerURL", DbType.String, objDealer.DealerURL);
            db.AddInParameter(dbCommand, "ExportFilePath", DbType.String, objDealer.ExportFilePath);
            if (objDealer.CreatedOn > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, objDealer.CreatedOn);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, Convert.DBNull);
            }
            if (objDealer.LastUpdate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, objDealer.LastUpdate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "NumberOfMailers", DbType.Int32, objDealer.NumberOfMailers);
            if (objDealer.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objDealer.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Term", DbType.String, objDealer.Term);
            db.AddInParameter(dbCommand, "DealerLogo", DbType.Binary, objDealer.DealerLogo);
            db.AddInParameter(dbCommand, "Price", DbType.Decimal, objDealer.Price);
            db.AddInParameter(dbCommand, "AcknowledgedTOS", DbType.Boolean, objDealer.AcknowledgedTOS);
            if (objDealer.AcknowledgedTOSDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, objDealer.AcknowledgedTOSDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "AcknowledgedTOSName", DbType.String, objDealer.AcknowledgedTOSName);
            db.AddInParameter(dbCommand, "AcknowledgedTOSSignature", DbType.Binary, objDealer.AcknowledgedTOSSignature);
            if (objDealer.DefaultDocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, objDealer.DefaultDocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LegalDisclaimer", DbType.String, objDealer.LegalDisclaimer);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDealer.Active);
            db.ExecuteNonQuery(dbCommand);
            objDealer.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Dealer objDealer)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
            db.AddInParameter(dbCommand, "DealerName", DbType.String, objDealer.DealerName);
            db.AddInParameter(dbCommand, "DealerNumber", DbType.Int32, objDealer.DealerNumber);
            db.AddInParameter(dbCommand, "AddressLine1", DbType.String, objDealer.AddressLine1);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "State", DbType.String, objDealer.State);
            db.AddInParameter(dbCommand, "Zip", DbType.String, objDealer.Zip);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objDealer.Phone);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "DealerURL", DbType.String, objDealer.DealerURL);
            db.AddInParameter(dbCommand, "ExportFilePath", DbType.String, objDealer.ExportFilePath);
            if (objDealer.CreatedOn > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, objDealer.CreatedOn);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, Convert.DBNull);
            }
            if (objDealer.LastUpdate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, objDealer.LastUpdate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "NumberOfMailers", DbType.Int32, objDealer.NumberOfMailers);
            if (objDealer.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objDealer.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Term", DbType.String, objDealer.Term);
            db.AddInParameter(dbCommand, "DealerLogo", DbType.Binary, objDealer.DealerLogo);
            db.AddInParameter(dbCommand, "Price", DbType.Decimal, objDealer.Price);
            db.AddInParameter(dbCommand, "AcknowledgedTOS", DbType.Boolean, objDealer.AcknowledgedTOS);
            if (objDealer.AcknowledgedTOSDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, objDealer.AcknowledgedTOSDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "AcknowledgedTOSName", DbType.String, objDealer.AcknowledgedTOSName);
            db.AddInParameter(dbCommand, "AcknowledgedTOSSignature", DbType.Binary, objDealer.AcknowledgedTOSSignature);
            if (objDealer.DefaultDocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, objDealer.DefaultDocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LegalDisclaimer", DbType.String, objDealer.LegalDisclaimer);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDealer.Active);
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

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "DealerName", DbType.String, objDealer.DealerName);
            db.AddInParameter(dbCommand, "DealerNumber", DbType.Int32, objDealer.DealerNumber);
            db.AddInParameter(dbCommand, "AddressLine1", DbType.String, objDealer.AddressLine1);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "State", DbType.String, objDealer.State);
            db.AddInParameter(dbCommand, "Zip", DbType.String, objDealer.Zip);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objDealer.Phone);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "DealerURL", DbType.String, objDealer.DealerURL);
            db.AddInParameter(dbCommand, "ExportFilePath", DbType.String, objDealer.ExportFilePath);
            if (objDealer.CreatedOn > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, objDealer.CreatedOn);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, Convert.DBNull);
            }
            if (objDealer.LastUpdate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, objDealer.LastUpdate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "NumberOfMailers", DbType.Int32, objDealer.NumberOfMailers);
            if (objDealer.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objDealer.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Term", DbType.String, objDealer.Term);
            db.AddInParameter(dbCommand, "DealerLogo", DbType.Binary, objDealer.DealerLogo);
            db.AddInParameter(dbCommand, "Price", DbType.Decimal, objDealer.Price);
            db.AddInParameter(dbCommand, "AcknowledgedTOS", DbType.Boolean, objDealer.AcknowledgedTOS);
            if (objDealer.AcknowledgedTOSDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, objDealer.AcknowledgedTOSDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "AcknowledgedTOSName", DbType.String, objDealer.AcknowledgedTOSName);
            db.AddInParameter(dbCommand, "AcknowledgedTOSSignature", DbType.Binary, objDealer.AcknowledgedTOSSignature);
            if (objDealer.DefaultDocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, objDealer.DefaultDocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LegalDisclaimer", DbType.String, objDealer.LegalDisclaimer);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDealer.Active);
            db.ExecuteNonQuery(dbCommand, trans);
            objDealer.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(Dealer objDealer, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
            db.AddInParameter(dbCommand, "DealerName", DbType.String, objDealer.DealerName);
            db.AddInParameter(dbCommand, "DealerNumber", DbType.Int32, objDealer.DealerNumber);
            db.AddInParameter(dbCommand, "AddressLine1", DbType.String, objDealer.AddressLine1);
            db.AddInParameter(dbCommand, "City", DbType.String, objDealer.City);
            db.AddInParameter(dbCommand, "State", DbType.String, objDealer.State);
            db.AddInParameter(dbCommand, "Zip", DbType.String, objDealer.Zip);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objDealer.Phone);
            db.AddInParameter(dbCommand, "Fax", DbType.String, objDealer.Fax);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, objDealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, objDealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, objDealer.MaxDistance);
            db.AddInParameter(dbCommand, "DealerURL", DbType.String, objDealer.DealerURL);
            db.AddInParameter(dbCommand, "ExportFilePath", DbType.String, objDealer.ExportFilePath);
            if (objDealer.CreatedOn > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, objDealer.CreatedOn);
            }
            else
            {
                db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, Convert.DBNull);
            }
            if (objDealer.LastUpdate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, objDealer.LastUpdate);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastUpdate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "NumberOfMailers", DbType.Int32, objDealer.NumberOfMailers);
            if (objDealer.StartDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, objDealer.StartDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Term", DbType.String, objDealer.Term);
            db.AddInParameter(dbCommand, "DealerLogo", DbType.Binary, objDealer.DealerLogo);
            db.AddInParameter(dbCommand, "Price", DbType.Decimal, objDealer.Price);
            db.AddInParameter(dbCommand, "AcknowledgedTOS", DbType.Boolean, objDealer.AcknowledgedTOS);
            if (objDealer.AcknowledgedTOSDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, objDealer.AcknowledgedTOSDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "AcknowledgedTOSDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "AcknowledgedTOSName", DbType.String, objDealer.AcknowledgedTOSName);
            db.AddInParameter(dbCommand, "AcknowledgedTOSSignature", DbType.Binary, objDealer.AcknowledgedTOSSignature);
            if (objDealer.DefaultDocTemplateID > 0)
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, objDealer.DefaultDocTemplateID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DefaultDocTemplateID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "LegalDisclaimer", DbType.String, objDealer.LegalDisclaimer);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDealer.Active);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(Dealer objDealer)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerDelete");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDealer.ID);
            db.ExecuteNonQuery(dbCommand);
        }

        #region DealerZipCodes

        static public void AddDealerZipCode(DealerZipCode objDealerZipCode)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodeInsert");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerZipCode.DealerID);
            db.AddInParameter(dbCommand, "ZipGeoCodeID", DbType.Int32, objDealerZipCode.ZipGeoCodeID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteDealerZipCode(DealerZipCode objDealerZipCode)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodeDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerZipCode.DealerID);
            db.AddInParameter(dbCommand, "ZipGeoCodeID", DbType.Int32, objDealerZipCode.ZipGeoCodeID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteDealerZipCodes(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodesDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteDealerZipCodesOutsideRadius(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerZipCodesDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region GlobalDealerDocTemplates

        static public void AddGlobalDealerDocTemplate(DealerDocTemplate objGlobalDealerDocTemplate)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_GlobalDealerDocTemplateInsert");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objGlobalDealerDocTemplate.DealerID);
            db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objGlobalDealerDocTemplate.DocTemplateID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteGlobalDealerDocTemplate(DealerDocTemplate objGlobalDealerDocTemplate)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_GlobalDealerDocTemplateDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objGlobalDealerDocTemplate.DealerID);
            db.AddInParameter(dbCommand, "DocTemplateID", DbType.Int32, objGlobalDealerDocTemplate.DocTemplateID);
            db.ExecuteNonQuery(dbCommand);
        }
        static public void DeleteGlobalDealerDocTemplates(int DealerID)
        {
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_GlobalDealerDocTemplatesDelete");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
            db.ExecuteNonQuery(dbCommand);
        }

        #endregion
    }
}
