using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the ZipGeoCode table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:28 PM
	/// </summary>
	public class ZipGeoCodeService 
	{
        static private string _selectViewSQL = "Select ID, ZipCode, ZipCodeString, City, State, County, AreaCode, Latitude, Longitude from ZipGeoCode";
        static public List<ZipGeoCode> GetAll()
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
                objZipGeoCode.County = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objZipGeoCode.AreaCode = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objZipGeoCode.Latitude = _reader.GetDouble(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objZipGeoCode.Longitude = _reader.GetDouble(8);
            }
        }
        static public ZipGeoCode GetByZip(string zip)
        {
            ZipGeoCode objZipGeoCode = new ZipGeoCode();
            string query = _selectViewSQL + " where ZipCodeString = '" + zip + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
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
        ///// <summary>
        ///// Gets the zip codes that are selected inside the dealer radius
        ///// </summary>
        ///// <param name="dealer"></param>
        ///// <returns></returns>
        //static public List<ZipGeoCode> GetForDealerInsideRadius(Dealer dealer)
        //{
        //    List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
        //    Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
        //    string _query = _selectViewSQL + " where ID in (select ZipGeoCodeID from DealerZipCode where DealerID = " + dealer.ID + ") AND ";
        //    _query += " ( dbo.DistanceBetween(" + dealer.Latitude + ", " + dealer.Longitude + ", Latitude, Longitude) <=  " + dealer.MaxDistance + ")";

        //    using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
        //    {
        //        while (reader.Read())
        //        {
        //            ZipGeoCode objZipGeoCode = new ZipGeoCode();
        //            LoadZipGeoCode(objZipGeoCode, reader);
        //            objZipGeoCode.IsModified = false;
        //            objZipGeoCodes.Add(objZipGeoCode);
        //        }
        //        // always call Close when done reading.
        //        reader.Close();
        //        return objZipGeoCodes;
        //    }
        //}
        ///// <summary>
        ///// Gets the zip codes that are selected inside the dealer radius
        ///// </summary>
        ///// <param name="DealerID"></param>
        ///// <returns></returns>
        //static public List<ZipGeoCode> GetForDealerInsideRadius(int DealerID)
        //{
        //    Dealer dealer = DealerService.GetByID(DealerID);
        //    List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
        //    Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
        //    string _query = _selectViewSQL + " where ID in (select ZipGeoCodeID from DealerZipCode where DealerID = " + dealer.ID + ") AND ";
        //    _query += " ( dbo.DistanceBetween(" + dealer.Latitude + ", " + dealer.Longitude + ", Latitude, Longitude) <=  " + dealer.MaxDistance + ")";

        //    using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
        //    {
        //        while (reader.Read())
        //        {
        //            ZipGeoCode objZipGeoCode = new ZipGeoCode();
        //            LoadZipGeoCode(objZipGeoCode, reader);
        //            objZipGeoCode.IsModified = false;
        //            objZipGeoCodes.Add(objZipGeoCode);
        //        }
        //        // always call Close when done reading.
        //        reader.Close();
        //        return objZipGeoCodes;
        //    }
        //}
        /// <summary>
        /// Gets the zip codes that are selected for the dealer
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        static public List<ZipGeoCode> GetForDealer(Dealer dealer)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesForDealerLoad");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, dealer.ID);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, dealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, dealer.Longitude);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }
        /// <summary>
        /// Gets the zip codes that are selected for the dealer
        /// </summary>
        /// <param name="DealerID"></param>
        /// <returns></returns>
        static public List<ZipGeoCode> GetForDealer(int DealerID)
        {
            Dealer dealer = DealerService.GetByID(DealerID);

            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesForDealerLoad");
            db.AddInParameter(dbCommand, "DealerID", DbType.Int32, dealer.ID);
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, dealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, dealer.Longitude);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
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
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesInRadiusLoad");
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, dealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, dealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, dealer.MaxDistance);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }

        static public List<ZipGeoCode> GetInsideRadius(int DealerID)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Dealer dealer = DealerService.GetByID(DealerID);
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesInRadiusLoad");
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, dealer.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, dealer.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, dealer.MaxDistance);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
            
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            
            }
        }

        static public List<ZipGeoCode> GetInsideRadius(double Latitude, double Longitude, double MaxDistance, DateTime? DischargeSinceDate)
        {
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesInRadiusLoad");
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, MaxDistance);
            if (DischargeSinceDate != null)
            {
                db.AddInParameter(dbCommand, "DischargeSinceDate", DbType.DateTime, DischargeSinceDate);
            }

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;

            }
        }

        static public List<ZipGeoCode> GetInsideRadius(string ZipCode, string Radius)
        {
            ZipGeoCode _zipCode = GetByZip(ZipCode);
            Double _radius = Double.Parse(Radius);
            
            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesInRadiusLoad");
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, _zipCode.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, _zipCode.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, _radius);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {

                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;

            }
        }

        static public List<ZipGeoCode> GetInsideRadius(string ZipCode, string Radius, string SinceDate)
        {
            ZipGeoCode _zipCode = GetByZip(ZipCode);
            Double _radius = Double.Parse(Radius);

            List<ZipGeoCode> objZipGeoCodes = new List<ZipGeoCode>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipGeoCodesInRadiusLoad");
            db.AddInParameter(dbCommand, "Latitude", DbType.Double, _zipCode.Latitude);
            db.AddInParameter(dbCommand, "Longitude", DbType.Double, _zipCode.Longitude);
            db.AddInParameter(dbCommand, "MaxDistance", DbType.Double, _radius);
            DateTime? DischargeSinceDate;
            try
            {
                DischargeSinceDate = DateTime.Parse(SinceDate);
            }
            catch
            {
                DischargeSinceDate = null;
            }
            if (DischargeSinceDate != null)
            {
                db.AddInParameter(dbCommand, "DischargeSinceDate", DbType.DateTime, DischargeSinceDate);
            }

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {

                while (reader.Read())
                {
                    ZipGeoCode objZipGeoCode = new ZipGeoCode();
                    LoadZipGeoCodeWithDistanceAndNumLeads(objZipGeoCode, reader);
                    objZipGeoCode.IsModified = false;
                    objZipGeoCodes.Add(objZipGeoCode);
                }
                // always call Close when done reading.
                reader.Close();
                return objZipGeoCodes;
            }
        }

        static private void LoadZipGeoCodeWithDistanceAndNumLeads(ZipGeoCode objZipGeoCode, IDataReader _reader)
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
                objZipGeoCode.County = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objZipGeoCode.AreaCode = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objZipGeoCode.Latitude = _reader.GetDouble(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objZipGeoCode.Longitude = _reader.GetDouble(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objZipGeoCode.Distance = _reader.GetDouble(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objZipGeoCode.NumLeadsAvailable = _reader.GetInt32(10);
            }
        }

        static public int GetNumLeadsForDealer(int DealerID, DateTime StartDate)
        {
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (DbCommand dbCommand = db.GetStoredProcCommand("dbo.p_CalcNumLeadsAvailable"))
            {
                db.AddOutParameter(dbCommand, "NumLeads", DbType.Int32, 4);
                if ( StartDate > DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, StartDate);
                else
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, Convert.DBNull);

                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, DealerID);
                db.ExecuteScalar(dbCommand);

                return Int32.Parse(db.GetParameterValue(dbCommand, "NumLeads").ToString());
            }
        }
	}
}
