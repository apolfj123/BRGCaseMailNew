using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.SqlServer.Dts.Runtime;
using System.Configuration;


namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class interacting with the ZipCodeImport table.
	/// Author: Jonathan Shaw
	/// Date Created: 10/5/2011 5:40:58 PM
	/// </summary>
	public class ZipCodeImportService 
	{
		static private string _selectViewSQL  = "Select ZipCode, City, State, County, AreaCode, CityType, CityAliasAbbreviation, CityAliasName, Latitude, Longitude, TimeZone, Elevation, CountyFIPS, DayLightSaving, PreferredLastLineKey, ClassificationCode, MultiCounty, StateFIPS, CityStateKey, CityAliasCode, PrimaryRecord, CityMixedCase, CityAliasMixedCase, StateANSI, CountyANSI, FacilityCode, CityDeliveryIndicator, CarrierRouteRateSortation, FinanceNumber, UniqueZIPName from ZipCodeImport";
		static public  List<ZipCodeImport> GetAll()
		{
			List<ZipCodeImport> objZipCodeImports = new List<ZipCodeImport>();
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					ZipCodeImport objZipCodeImport = new ZipCodeImport();
					LoadZipCodeImport(objZipCodeImport, reader);
					objZipCodeImports.Add (objZipCodeImport);
				}
				// always call Close when done reading.
				reader.Close();
				return objZipCodeImports;
			}
		}
		static private void LoadZipCodeImport(ZipCodeImport objZipCodeImport, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objZipCodeImport.ZipCode = _reader.GetString(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objZipCodeImport.City = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objZipCodeImport.State = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objZipCodeImport.County = _reader.GetString(3);
			}
			if (_reader.IsDBNull(4) != true)
			{
				objZipCodeImport.AreaCode = _reader.GetString(4);
			}
			if (_reader.IsDBNull(5) != true)
			{
				objZipCodeImport.CityType = _reader.GetString(5);
			}
			if (_reader.IsDBNull(6) != true)
			{
				objZipCodeImport.CityAliasAbbreviation = _reader.GetString(6);
			}
			if (_reader.IsDBNull(7) != true)
			{
				objZipCodeImport.CityAliasName = _reader.GetString(7);
			}
			if (_reader.IsDBNull(8) != true)
			{
				objZipCodeImport.Latitude = _reader.GetString(8);
			}
			if (_reader.IsDBNull(9) != true)
			{
				objZipCodeImport.Longitude = _reader.GetString(9);
			}
			if (_reader.IsDBNull(10) != true)
			{
				objZipCodeImport.TimeZone = _reader.GetString(10);
			}
			if (_reader.IsDBNull(11) != true)
			{
				objZipCodeImport.Elevation = _reader.GetString(11);
			}
			if (_reader.IsDBNull(12) != true)
			{
				objZipCodeImport.CountyFIPS = _reader.GetString(12);
			}
			if (_reader.IsDBNull(13) != true)
			{
				objZipCodeImport.DayLightSaving = _reader.GetString(13);
			}
			if (_reader.IsDBNull(14) != true)
			{
				objZipCodeImport.PreferredLastLineKey = _reader.GetString(14);
			}
			if (_reader.IsDBNull(15) != true)
			{
				objZipCodeImport.ClassificationCode = _reader.GetString(15);
			}
			if (_reader.IsDBNull(16) != true)
			{
				objZipCodeImport.MultiCounty = _reader.GetString(16);
			}
			if (_reader.IsDBNull(17) != true)
			{
				objZipCodeImport.StateFIPS = _reader.GetString(17);
			}
			if (_reader.IsDBNull(18) != true)
			{
				objZipCodeImport.CityStateKey = _reader.GetString(18);
			}
			if (_reader.IsDBNull(19) != true)
			{
				objZipCodeImport.CityAliasCode = _reader.GetString(19);
			}
			if (_reader.IsDBNull(20) != true)
			{
				objZipCodeImport.PrimaryRecord = _reader.GetString(20);
			}
			if (_reader.IsDBNull(21) != true)
			{
				objZipCodeImport.CityMixedCase = _reader.GetString(21);
			}
			if (_reader.IsDBNull(22) != true)
			{
				objZipCodeImport.CityAliasMixedCase = _reader.GetString(22);
			}
			if (_reader.IsDBNull(23) != true)
			{
				objZipCodeImport.StateANSI = _reader.GetString(23);
			}
			if (_reader.IsDBNull(24) != true)
			{
				objZipCodeImport.CountyANSI = _reader.GetString(24);
			}
			if (_reader.IsDBNull(25) != true)
			{
				objZipCodeImport.FacilityCode = _reader.GetString(25);
			}
			if (_reader.IsDBNull(26) != true)
			{
				objZipCodeImport.CityDeliveryIndicator = _reader.GetString(26);
			}
			if (_reader.IsDBNull(27) != true)
			{
				objZipCodeImport.CarrierRouteRateSortation = _reader.GetString(27);
			}
			if (_reader.IsDBNull(28) != true)
			{
				objZipCodeImport.FinanceNumber = _reader.GetString(28);
			}
			if (_reader.IsDBNull(29) != true)
			{
				objZipCodeImport.UniqueZIPName = _reader.GetString(29);
			}
		}
		static public void Insert(ZipCodeImport objZipCodeImport)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodeImportInsert");
				db.AddInParameter(dbCommand, "ZipCode", DbType.String, objZipCodeImport.ZipCode);
				db.AddInParameter(dbCommand, "City", DbType.String, objZipCodeImport.City);
				db.AddInParameter(dbCommand, "State", DbType.String, objZipCodeImport.State);
				db.AddInParameter(dbCommand, "County", DbType.String, objZipCodeImport.County);
				db.AddInParameter(dbCommand, "AreaCode", DbType.String, objZipCodeImport.AreaCode);
				db.AddInParameter(dbCommand, "CityType", DbType.String, objZipCodeImport.CityType);
				db.AddInParameter(dbCommand, "CityAliasAbbreviation", DbType.String, objZipCodeImport.CityAliasAbbreviation);
				db.AddInParameter(dbCommand, "CityAliasName", DbType.String, objZipCodeImport.CityAliasName);
				db.AddInParameter(dbCommand, "Latitude", DbType.String, objZipCodeImport.Latitude);
				db.AddInParameter(dbCommand, "Longitude", DbType.String, objZipCodeImport.Longitude);
				db.AddInParameter(dbCommand, "TimeZone", DbType.String, objZipCodeImport.TimeZone);
				db.AddInParameter(dbCommand, "Elevation", DbType.String, objZipCodeImport.Elevation);
				db.AddInParameter(dbCommand, "CountyFIPS", DbType.String, objZipCodeImport.CountyFIPS);
				db.AddInParameter(dbCommand, "DayLightSaving", DbType.String, objZipCodeImport.DayLightSaving);
				db.AddInParameter(dbCommand, "PreferredLastLineKey", DbType.String, objZipCodeImport.PreferredLastLineKey);
				db.AddInParameter(dbCommand, "ClassificationCode", DbType.String, objZipCodeImport.ClassificationCode);
				db.AddInParameter(dbCommand, "MultiCounty", DbType.String, objZipCodeImport.MultiCounty);
				db.AddInParameter(dbCommand, "StateFIPS", DbType.String, objZipCodeImport.StateFIPS);
				db.AddInParameter(dbCommand, "CityStateKey", DbType.String, objZipCodeImport.CityStateKey);
				db.AddInParameter(dbCommand, "CityAliasCode", DbType.String, objZipCodeImport.CityAliasCode);
				db.AddInParameter(dbCommand, "PrimaryRecord", DbType.String, objZipCodeImport.PrimaryRecord);
				db.AddInParameter(dbCommand, "CityMixedCase", DbType.String, objZipCodeImport.CityMixedCase);
				db.AddInParameter(dbCommand, "CityAliasMixedCase", DbType.String, objZipCodeImport.CityAliasMixedCase);
				db.AddInParameter(dbCommand, "StateANSI", DbType.String, objZipCodeImport.StateANSI);
				db.AddInParameter(dbCommand, "CountyANSI", DbType.String, objZipCodeImport.CountyANSI);
				db.AddInParameter(dbCommand, "FacilityCode", DbType.String, objZipCodeImport.FacilityCode);
				db.AddInParameter(dbCommand, "CityDeliveryIndicator", DbType.String, objZipCodeImport.CityDeliveryIndicator);
				db.AddInParameter(dbCommand, "CarrierRouteRateSortation", DbType.String, objZipCodeImport.CarrierRouteRateSortation);
				db.AddInParameter(dbCommand, "FinanceNumber", DbType.String, objZipCodeImport.FinanceNumber);
				db.AddInParameter(dbCommand, "UniqueZIPName", DbType.String, objZipCodeImport.UniqueZIPName);
			db.ExecuteNonQuery(dbCommand);
		}
		static public void DeleteAll()
		{
			Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ZipCodeImportsDelete");
			db.ExecuteNonQuery(dbCommand);
		}

        static public string ProcessZipCodeFile(string FilePath)
        {

            string _message = null;
            //use this when the SSIS import fails.
            Database db = DatabaseFactory.CreateDatabase("BRGCaseMail");
            
            try
            {
                //ZipCodeImportService.DeleteAll();

                //// Read and display lines from the file until the end of
                //// the file is reached.            
                //using (TextReader tr = File.OpenText(FilePath))
                //{
                //    String line;
                //    int i=0;

                //    while ((line = tr.ReadLine()) != null)
                //    {
                //        i++;
                //        ZipCodeImport _data = new ZipCodeImport(line);
                //        //skip first line
                //        if (_data.ZipCode != "ZipCode")
                //        {
                //            Insert(_data);
                //        }
                //   }

                //        System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ImportZipCodeData");
                //        db.ExecuteNonQuery(dbCommand);

                //        return true;
                //    }

                //execute the dts package
                //Load DTSX

                Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();
                Package package = app.LoadPackage(System.Configuration.ConfigurationManager.AppSettings.Get("ZipCodeImportDTSXFile"), null);

                //Specify Excel Connection From DTSX Connection Manager
                Debug.WriteLine(package.Connections["SourceConnectionFlatFile"].ConnectionString);
                package.Connections["SourceConnectionFlatFile"].ConnectionString = FilePath;

                Debug.WriteLine(package.Connections["DestinationConnectionOLEDB"].ConnectionString);
                package.Connections["DestinationConnectionOLEDB"].ConnectionString = ConfigurationManager.AppSettings["DTSConnectionString"];

                //    //Execute DTSX.
                Microsoft.SqlServer.Dts.Runtime.DTSExecResult results = package.Execute();

                if (results == DTSExecResult.Success)
                {
                    //yay!
                    System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_ImportZipCodeData");
                    db.ExecuteNonQuery(dbCommand);

                    return "ZipCode Import Success!";
                }
                else
                {
                    _message = string.Empty;
                    foreach (DtsError _err in package.Errors)
                    {
                        _message += _err.Description + ".  ";

                    }
                    _message += "Please see you system adminstrator. ";

                    return _message;
                }

            }
            catch (Exception ex)
            {
                _message += "Error: " + ex.ToString() + "Please see your system adminstrator. ";
                return _message;
            }
        }
	}
}
