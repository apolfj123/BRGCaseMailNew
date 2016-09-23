using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the Lookup table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/29/2008 10:15:25 PM
	/// </summary>
	public class LookupService
	{
        static private string _selectViewSQL = "Select ID, Name, Description, Active from ";

        static private void LoadLookup(Lookup objLookup, IDataReader reader)
        {
            if (reader.IsDBNull(0) != true)
            {
                objLookup.ID = reader.GetInt32(0);
            }
            if (reader.IsDBNull(1) != true)
            {
                objLookup.Name = reader.GetString(1);
            }
            if (reader.IsDBNull(2) != true)
            {
                objLookup.Description = reader.GetString(2);
            }
            if (reader.IsDBNull(3) != true)
            {
                objLookup.Active = reader.GetBoolean(3);
            }
        }
        static public ObservableCollection<Lookup> GetAllObservable(string tableName)
        {
            ObservableCollection<Lookup> objLookups = new ObservableCollection<Lookup>();
            string query = "Select ID, Name, Description, Active from " + tableName + " order by name asc";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    //the factory returns the appropriate type of lookup object based
                    //on the tableName
                    Lookup objLookup = LookupFactory.CreateLookup(tableName);
                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }
        static public List<Lookup> GetAll(string tableName, bool OrderByName)
        {
            List<Lookup> objLookups = new List<Lookup>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL;

            if (OrderByName)
                _query += tableName + " order by name asc";
            else
                _query += tableName + " order by ID asc";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    Lookup objLookup = new Lookup();
                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }
        static public ObservableCollection<Lookup> GetActiveObservable(string tableName)
        {
            ObservableCollection<Lookup> objLookups = new ObservableCollection<Lookup>();
            string query = "Select ID, Name, Description, Active from " + tableName + " where Active = 1 order by name asc";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    //the factory returns the appropriate type of lookup object based
                    //on the tableName
                    Lookup objLookup = LookupFactory.CreateLookup(tableName);
                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }
        static public List<Lookup> GetActive(string tableName)
        {
            List<Lookup> objLookups = new List<Lookup>();
            string query = "Select ID, Name, Description, Active from " + tableName + " where Active = 1 order by name asc";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    //the factory returns the appropriate type of lookup object based
                    //on the tableName
                    Lookup objLookup = LookupFactory.CreateLookup(tableName);
                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }
        static public ObservableCollection<Lookup> GetByForeignKey(string tableName, string ParentTableName, int ForeignKeyID)
        {
            ObservableCollection<Lookup> objLookups = new ObservableCollection<Lookup>();
            string query = "Select ID, Name, Description, Active from " + tableName + " where " + ParentTableName + "ID = " + ForeignKeyID + "  order by name asc";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                while (reader.Read())
                {
                    //the factory returns the appropriate type of lookup object based
                    //on the tableName
                    Lookup objLookup = LookupFactory.CreateLookup(tableName);

                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }
        static public List<Lookup> GetForForeignKey(string tableName, string ForeignKeyName, int ForeignKeyID, bool OrderByName)
        {
            List<Lookup> objLookups = new List<Lookup>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL;
            _query += tableName + " where " + ForeignKeyName + " = " + ForeignKeyID;
            if (OrderByName)
                _query += " order by name asc";
            else
                _query += " order by ID asc";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    Lookup objLookup = new Lookup();
                    LoadLookup(objLookup, reader);
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                reader.Close();
                return objLookups;
            }
        }

        static public Lookup GetByID(int ID, string tableName)
	    {
            Lookup objLookup = LookupFactory.CreateLookup(tableName);
            string query = "Select ID, Name, Description, Active from " + tableName + " where ID = " + ID;
		    Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
		    using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
		    {
			    if (reader.Read())
			    {
                    LoadLookup(objLookup, reader);
                    // always call Close when done reading.
				    reader.Close();
				    objLookup.IsModified = false;
				    return objLookup;
			    }
			    else
			    {
				    return null;
			    }
		    }
	    }
        static public Lookup GetByValue(string Name, string tableName)
        {
            Lookup objLookup = LookupFactory.CreateLookup(tableName);
            string query = "Select ID, Name, Description, Active from " + tableName + " where Name = '" + Name + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadLookup(objLookup, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objLookup.IsModified = false;
                    return objLookup;
                }
                else
                {
                    return null;
                }
            }
        }
        static public void Save(Lookup objLookup)
	    {
		    if (objLookup.IsModified == true)
		    {
			    if (objLookup.ID == 0 )
			    {
				    Insert(objLookup);
			    }
			    else
			    {
				    Update(objLookup);
			    }
		    }
	    }
		static private void Insert(Lookup objLookup)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_LookupInsert");
			db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "TableName", DbType.String, objLookup.GetType().Name);
            db.AddInParameter(dbCommand, "Name", DbType.String, objLookup.Name);
//            db.AddInParameter(dbCommand, "Description", DbType.String, objLookup.Description);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objLookup.Active);
            db.ExecuteNonQuery(dbCommand);
			objLookup.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
		}
		static private void Update(Lookup objLookup)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_LookupUpdate");
            db.AddInParameter(dbCommand, "TableName", DbType.String, objLookup.GetType().Name); 
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objLookup.ID);
			db.AddInParameter(dbCommand, "Name", DbType.String, objLookup.Name);
//            db.AddInParameter(dbCommand, "Description", DbType.String, objLookup.Description);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objLookup.Active);
            db.ExecuteNonQuery(dbCommand);
		}
		static public void Delete(Lookup objLookup)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_LookupDelete");
            db.AddInParameter(dbCommand, "TableName", DbType.String, objLookup.GetType().Name);
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objLookup.ID);
			db.ExecuteNonQuery(dbCommand);
        }

        static public List<DisplayMonth> GetDisplayMonths()
        {
            List<DisplayMonth> _months = new List<DisplayMonth>();
            for (int i = 0; i < 24; i++)
            {
                DateTime _date = DateTime.Parse(DateTime.Now.AddMonths(-1 * i).ToString());
                DisplayMonth _month = new DisplayMonth() { ID = i, Name = _date.ToString("MMM-yyyy") };
                _months.Add(_month);
            }

            return _months;
        }

        static public List<ReportColLookup> GetReportColLookups(string SQLQuery)
        {
            List<ReportColLookup> objLookups = new List<ReportColLookup>();
            //string query = "Select ID, Name, Active from " + tableName + " order by name asc";
            SQLQuery += " order by name asc";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader _reader = db.ExecuteReader(CommandType.Text, SQLQuery))
            {
                while (_reader.Read())
                {
                    //the factory returns the appropriate type of lookup object based
                    //on the tableName
                    ReportColLookup objLookup = new ReportColLookup();

                    //if (_reader.IsDBNull(0) != true)
                    //{
                    //    objLookup.ID = _reader.GetInt32(0);
                    //}
                    if (_reader.IsDBNull(0) != true)
                    {
                        objLookup.Name = _reader.GetString(0);
                    }
                    //if (_reader.IsDBNull(2) != true)
                    //{
                    //    objLookup.Status = _reader.GetString(2);
                    //}
                    objLookup.IsModified = false;
                    objLookups.Add(objLookup);
                }
                // always call Close when done reading.
                _reader.Close();
                return objLookups;
            }
        }


	}

}
