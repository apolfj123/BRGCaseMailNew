using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the DealerMailingListCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/21/2013 4:06:10 PM
	/// </summary>
	public class DealerMailingListCaseService 
	{
        static private string _selectViewSQL = "Select DealerMailingListID, BankruptcyCaseID, DealerID, Sold, SoldDate, GrossProfit, DoNotSend, FirstName, LastName, AddrLine1, AddrLine2, City, StateCode, PostalCode, DischargeDate from v_DealerMailingListCase";
        static public List<DealerMailingListCase> GetAll()
        {
            List<DealerMailingListCase> objDealerMailingListCases = new List<DealerMailingListCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    DealerMailingListCase objDealerMailingListCase = new DealerMailingListCase();
                    LoadDealerMailingListCase(objDealerMailingListCase, reader);
                    objDealerMailingListCase.IsModified = false;
                    objDealerMailingListCases.Add(objDealerMailingListCase);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingListCases;
            }
        }
        static private void LoadDealerMailingListCase(DealerMailingListCase objDealerMailingListCase, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDealerMailingListCase.DealerMailingListID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDealerMailingListCase.BankruptcyCaseID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDealerMailingListCase.DealerID = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDealerMailingListCase.Sold = _reader.GetBoolean(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDealerMailingListCase.SoldDate = _reader.GetDateTime(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDealerMailingListCase.GrossProfit = _reader.GetDecimal(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDealerMailingListCase.DoNotSend = _reader.GetBoolean(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDealerMailingListCase.FirstName = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDealerMailingListCase.LastName = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDealerMailingListCase.AddrLine1 = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDealerMailingListCase.AddrLine2 = _reader.GetString(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDealerMailingListCase.City = _reader.GetString(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDealerMailingListCase.StateCode = _reader.GetString(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objDealerMailingListCase.PostalCode = _reader.GetInt32(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDealerMailingListCase.DischargeDate = _reader.GetDateTime(14);
            }
        }
        static public DealerMailingListCase GetByDealerAndCaseID(int BankruptcyCaseID, int DealerID)
        {
            DealerMailingListCase objDealerMailingListCase = new DealerMailingListCase();
            string query = _selectViewSQL + " where BankruptcyCaseID = " + BankruptcyCaseID + " and DealerID = " + DealerID;
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDealerMailingListCase(objDealerMailingListCase, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDealerMailingListCase.IsModified = false;
                    return objDealerMailingListCase;
                }
                else
                {
                    return null;
                }
            }
        }
        static public List<DealerMailingListCase> GetByDealerSalesReport(int DealerID, DateTime StartDate, DateTime EndDate)
        {
            List<DealerMailingListCase> objDealerMailingListCases = new List<DealerMailingListCase>();
            string query = _selectViewSQL + " where DealerID = " + DealerID + " and SoldDate between '" + StartDate.ToShortDateString() + "' and '" + EndDate.ToShortDateString() + "' order by SoldDate asc ";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    DealerMailingListCase objDealerMailingListCase = new DealerMailingListCase();
                    LoadDealerMailingListCase(objDealerMailingListCase, reader);
                    objDealerMailingListCase.IsModified = false;
                    objDealerMailingListCases.Add(objDealerMailingListCase);
                }

                // always call Close when done reading.
                reader.Close();
                return objDealerMailingListCases;
            }
        }
        
        static public List<DealerMailingListCase> GetForMailingList(int DealerMailingListID, bool IncludeMarkedAsSoldAndRemoves)
        {
            List<DealerMailingListCase> objDealerMailingListCases = new List<DealerMailingListCase>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL + " where DealerMailingListID = " + DealerMailingListID;
            if (IncludeMarkedAsSoldAndRemoves == false)
            {
                _query += " and DoNotSend = 0 and Sold=0 ";
            }
            _query += " order by LastName asc ";
            
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query ))
            {
                while (reader.Read())
                {
                    DealerMailingListCase objDealerMailingListCase = new DealerMailingListCase();
                    LoadDealerMailingListCase(objDealerMailingListCase, reader);
                    objDealerMailingListCase.IsModified = false;
                    objDealerMailingListCases.Add(objDealerMailingListCase);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingListCases;
            }
        }
        static public List<DealerMailingListCase> GetFiltered(int DealerID, string FirstName, string LastName, string ZipCode, int DealerMailingListID, bool DoNotSend, bool Sold, DateTime? StartDischargeDate, DateTime? EndDischargeDate)
        {
            List<DealerMailingListCase> objDealerMailingListCases = new List<DealerMailingListCase>();
            
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL + " where ";
            
            if (FirstName.Length > 0)
            {
                _query += " FirstName like '" + FirstName + "%' and ";
            }

            if (LastName.Length > 0)
            {
                _query += " LastName like '" + LastName + "%' and ";
            }

            if (DealerID > 0)
            {
                _query += " DealerID = " + DealerID + " and ";
            }

            if (DealerMailingListID > 0)
            {
                _query += " DealerMailingListID = " + DealerMailingListID + " and ";
            }

            if (DoNotSend == true)
            {
                _query += " DoNotSend = 1 and ";
            }

            if (Sold == true)
            {
                _query += " Sold = 1 and ";
            }

            if (ZipCode.Length > 0)
            {
                _query += " PostalCode = " + ZipCode + " and ";
            }

            if (StartDischargeDate != null)
            {
                _query += " DischargeDate > '" + DateTime.Parse(StartDischargeDate.ToString()).ToShortDateString() + "' and ";
            }

            if (EndDischargeDate != null)
            {
                _query += " DischargeDate < '" + DateTime.Parse(EndDischargeDate.ToString()).ToShortDateString() + "' and ";
            }

            _query = _query + " 1 = 1  ORDER BY LastName ASC";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    DealerMailingListCase objDealerMailingListCase = new DealerMailingListCase();
                    LoadDealerMailingListCase(objDealerMailingListCase, reader);
                    objDealerMailingListCase.IsModified = false;
                    objDealerMailingListCases.Add(objDealerMailingListCase);
                }
                // always call Close when done reading.
                reader.Close();
                return objDealerMailingListCases;
            }
        }        
        static public void Insert(DealerMailingListCase objDealerMailingListCase)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseInsert");
			if (objDealerMailingListCase.DealerMailingListID > 0)
			{
				db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, objDealerMailingListCase.DealerMailingListID);
			}
			else
			{
				db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, Convert.DBNull);
			}
			if (objDealerMailingListCase.BankruptcyCaseID > 0)
			{
				db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, objDealerMailingListCase.BankruptcyCaseID);
			}
			else
			{
				db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, Convert.DBNull);
			}
			if (objDealerMailingListCase.DealerID > 0)
			{
				db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingListCase.DealerID);
			}
			else
			{
				db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
			}
			db.AddInParameter(dbCommand, "Sold", DbType.Boolean, objDealerMailingListCase.Sold);
			
            if (objDealerMailingListCase.SoldDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "SoldDate", DbType.DateTime, objDealerMailingListCase.SoldDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "SoldDate", DbType.DateTime, Convert.DBNull);
			}
            db.AddInParameter(dbCommand, "GrossProfit", DbType.Decimal, objDealerMailingListCase.GrossProfit);
            db.AddInParameter(dbCommand, "DoNotSend", DbType.Boolean, objDealerMailingListCase.DoNotSend);
			db.ExecuteNonQuery(dbCommand);
		}
        
        static public void Update(DealerMailingListCase objDealerMailingListCase)
		{
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseUpdate");
			
            if (objDealerMailingListCase.DealerID > 0)
			{
				db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDealerMailingListCase.DealerID);
			}
			else
			{
				db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
			}

            if (objDealerMailingListCase.BankruptcyCaseID > 0)
			{
				db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, objDealerMailingListCase.BankruptcyCaseID);
			}
			else
			{
				db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, Convert.DBNull);
			}
			db.AddInParameter(dbCommand, "Sold", DbType.Boolean, objDealerMailingListCase.Sold);
			if (objDealerMailingListCase.SoldDate > DateTime.MinValue)
			{
				db.AddInParameter(dbCommand, "SoldDate", DbType.DateTime, objDealerMailingListCase.SoldDate);
			}
			else
			{
				db.AddInParameter(dbCommand, "SoldDate", DbType.DateTime, Convert.DBNull);
			}
            db.AddInParameter(dbCommand, "GrossProfit", DbType.Decimal, objDealerMailingListCase.GrossProfit);
			db.AddInParameter(dbCommand, "DoNotSend", DbType.Boolean, objDealerMailingListCase.DoNotSend);
			db.ExecuteNonQuery(dbCommand);
		}
        static public void Delete(DealerMailingListCase objDealerMailingListCase)
		{
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DealerMailingListCaseDelete");
            db.AddInParameter(dbCommand, "DealerMailingListID", DbType.Int32, objDealerMailingListCase.DealerMailingListID);
            db.AddInParameter(dbCommand, "BankruptcyCaseID", DbType.Int32, objDealerMailingListCase.BankruptcyCaseID);
			db.ExecuteNonQuery(dbCommand);
		}
    }
}
