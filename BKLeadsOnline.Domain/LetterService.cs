using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the Letter table.
	/// Author: Jonathan Shaw
	/// Date Created: 8/8/2013 6:10:28 PM
	/// </summary>
	public class LetterService 
	{
		static private string _selectViewSQL  = "Select ID, Name, Description, Active from Letter";
		static public  List<Letter> GetAll()
		{
			List<Letter> objLetters = new List<Letter>();
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
			{
				while (reader.Read())
				{
					Letter objLetter = new Letter();
					LoadLetter(objLetter, reader);
					objLetter.IsModified = false;
					objLetters.Add (objLetter);
				}
				// always call Close when done reading.
				reader.Close();
				return objLetters;
			}
		}
        static public List<Letter> GetForDealer(int DealerID)
        {
            List<Letter> objLetters = new List<Letter>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL + " where ID in (select letterID from DocTemplate dt inner join DealerDocTemplate ddt on ddt.DocTemplateID = dt.ID where ddt.DealerID = " + DealerID + ")"  ))
            {
                while (reader.Read())
                {
                    Letter objLetter = new Letter();
                    LoadLetter(objLetter, reader);
                    objLetter.IsModified = false;
                    objLetters.Add(objLetter);
                }
                // always call Close when done reading.
                reader.Close();
                return objLetters;
            }
        }
        static public Letter GetByID(int ID)
		{
			Letter objLetter = new Letter();
			string query = _selectViewSQL + " where ID = " + ID;
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
			{
				if (reader.Read())
				{
					LoadLetter(objLetter, reader);
					// always call Close when done reading.
					reader.Close();
					objLetter.IsModified = false;
					return objLetter;
				}
				else
				{
					return null;
				}
			}
		}
		static private void LoadLetter(Letter objLetter, IDataReader _reader)
		{
			if (_reader.IsDBNull(0) != true)
			{
				objLetter.ID = _reader.GetInt32(0);
			}
			if (_reader.IsDBNull(1) != true)
			{
				objLetter.Name = _reader.GetString(1);
			}
			if (_reader.IsDBNull(2) != true)
			{
				objLetter.Description = _reader.GetString(2);
			}
			if (_reader.IsDBNull(3) != true)
			{
				objLetter.Active = _reader.GetBoolean(3);
			}
		}
	}
}
