using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the PacerFileFormat table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/27/2011 11:32:27 AM
	/// </summary>
	public class PacerFileFormat : IComparable
	{
		private bool _isModified;
		public bool IsModified
		{
			get
			{
				return _isModified;
			}
			set
			{
				_isModified = value;
			}
		}
		public PacerFileFormat()
		{
			_iD = 0;
			_pacerFileVersion = string.Empty;
			_numberColumns = 0;
		}
		public PacerFileFormat(int ID, string PacerFileVersion, int NumberColumns)		
        {
			_iD = ID;
			_pacerFileVersion = PacerFileVersion;
			_numberColumns = NumberColumns;
		}
		public PacerFileFormat Copy()
		{
			PacerFileFormat _pacerFileFormat = new PacerFileFormat();
			_pacerFileFormat.ID = _iD;
			_pacerFileFormat.PacerFileVersion = _pacerFileVersion;
			_pacerFileFormat.NumberColumns = _numberColumns;
			return _pacerFileFormat;
		}
		#region "Private Instance Variables"
		private int _iD;
		private string _pacerFileVersion;
		private int _numberColumns;
		#endregion 
		#region "Public Properties"
		public int ID
		{
			get
			{
				return _iD;
			}
			set
			{
				if (value != _iD)
				{
					this._isModified = true;
					_iD = value;
				}
			}
		}

		public string PacerFileVersion
		{
			get
			{
				return _pacerFileVersion;
			}
			set
			{
				if (value != _pacerFileVersion)
				{
					this._isModified = true;
					_pacerFileVersion = value;
				}
			}
		}

		public int NumberColumns
		{
			get
			{
				return _numberColumns;
			}
			set
			{
				if (value != _numberColumns)
				{
					this._isModified = true;
					_numberColumns = value;
				}
			}
		}

		#endregion 
		public int CompareTo(Object objPacerFileFormat)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((PacerFileFormat)objPacerFileFormat).ID);
		}

        public string PacerFileVersionString
        {
            get
            {
                return _pacerFileVersion + ": " + NumberColumns + " columns"; ;
            }
        }
    
    }
}
