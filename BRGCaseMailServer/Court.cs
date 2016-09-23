using System;

using System.Collections;
using System.ComponentModel;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the Court table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2011 12:37:08 AM
	/// </summary>
public class Court : IComparable, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
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
		public Court()
		{
			_iD = 0;
			_pacerFileFormatID = 0;
			_stateCode = string.Empty;
			_courtName = string.Empty;
			_uRLAbbrv = string.Empty;
			_uRLFull = string.Empty;
			_filePrefix = string.Empty;
			_lastPacerLoadDischargeDate = System.DateTime.MinValue;
			_lastPacerLoadFileDate = System.DateTime.MinValue;
			_pacerFileVersion = string.Empty;
			_pacerFileVersionNumColumns = 0;
		}
		public Court(int ID, int PacerFileFormatID, string StateCode, string CourtName, string URLAbbrv, string URLFull, string FilePrefix, DateTime LastPacerLoadDischargeDate, DateTime LastPacerLoadFileDate, string PacerFileVersion, int PacerFileVersionNumColumns)		{
			_iD = ID;
			_pacerFileFormatID = PacerFileFormatID;
			_stateCode = StateCode;
			_courtName = CourtName;
			_uRLAbbrv = URLAbbrv;
			_uRLFull = URLFull;
			_filePrefix = FilePrefix;
			_lastPacerLoadDischargeDate = LastPacerLoadDischargeDate;
			_lastPacerLoadFileDate = LastPacerLoadFileDate;
			_pacerFileVersion = PacerFileVersion;
			_pacerFileVersionNumColumns = PacerFileVersionNumColumns;
		}
		public Court Copy()
		{
			Court _court = new Court();
			_court.ID = _iD;
			_court.PacerFileFormatID = _pacerFileFormatID;
			_court.StateCode = _stateCode;
			_court.CourtName = _courtName;
			_court.URLAbbrv = _uRLAbbrv;
			_court.URLFull = _uRLFull;
			_court.FilePrefix = _filePrefix;
			_court.LastPacerLoadDischargeDate = _lastPacerLoadDischargeDate;
			_court.LastPacerLoadFileDate = _lastPacerLoadFileDate;
			_court.PacerFileVersion = _pacerFileVersion;
			_court.PacerFileVersionNumColumns = _pacerFileVersionNumColumns;
			return _court;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _pacerFileFormatID;
		private string _stateCode;
		private string _courtName;
		private string _uRLAbbrv;
		private string _uRLFull;
		private string _filePrefix;
		private DateTime _lastPacerLoadDischargeDate;
		private DateTime _lastPacerLoadFileDate;
		private string _pacerFileVersion;
		private int _pacerFileVersionNumColumns;
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
					NotifyPropertyChanged("ID");
				}
			}
		}

		public int PacerFileFormatID
		{
			get
			{
				return _pacerFileFormatID;
			}
			set
			{
				if (value != _pacerFileFormatID)
				{
					this._isModified = true;
					_pacerFileFormatID = value;
					NotifyPropertyChanged("PacerFileFormatID");
				}
			}
		}

		public string StateCode
		{
			get
			{
				return _stateCode;
			}
			set
			{
				if (value != _stateCode)
				{
					this._isModified = true;
					_stateCode = value;
					NotifyPropertyChanged("StateCode");
				}
			}
		}

		public string CourtName
		{
			get
			{
				return _courtName;
			}
			set
			{
				if (value != _courtName)
				{
					this._isModified = true;
					_courtName = value;
					NotifyPropertyChanged("CourtName");
				}
			}
		}

		public string URLAbbrv
		{
			get
			{
				return _uRLAbbrv;
			}
			set
			{
				if (value != _uRLAbbrv)
				{
					this._isModified = true;
					_uRLAbbrv = value;
					NotifyPropertyChanged("URLAbbrv");
				}
			}
		}

		public string URLFull
		{
			get
			{
				return _uRLFull;
			}
			set
			{
				if (value != _uRLFull)
				{
					this._isModified = true;
					_uRLFull = value;
					NotifyPropertyChanged("URLFull");
				}
			}
		}

		public string FilePrefix
		{
			get
			{
				return _filePrefix;
			}
			set
			{
				if (value != _filePrefix)
				{
					this._isModified = true;
					_filePrefix = value;
					NotifyPropertyChanged("FilePrefix");
				}
			}
		}

		public DateTime LastPacerLoadDischargeDate
		{
			get
			{
				return _lastPacerLoadDischargeDate;
			}
			set
			{
				if (value != _lastPacerLoadDischargeDate)
				{
					this._isModified = true;
					_lastPacerLoadDischargeDate = value;
					NotifyPropertyChanged("LastPacerLoadDischargeDate");
				}
			}
		}

		public DateTime LastPacerLoadFileDate
		{
			get
			{
				return _lastPacerLoadFileDate;
			}
			set
			{
				if (value != _lastPacerLoadFileDate)
				{
					this._isModified = true;
					_lastPacerLoadFileDate = value;
					NotifyPropertyChanged("LastPacerLoadFileDate");
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
					NotifyPropertyChanged("PacerFileVersion");
				}
			}
		}

		public int PacerFileVersionNumColumns
		{
			get
			{
				return _pacerFileVersionNumColumns;
			}
			set
			{
				if (value != _pacerFileVersionNumColumns)
				{
					this._isModified = true;
					_pacerFileVersionNumColumns = value;
					NotifyPropertyChanged("PacerFileVersionNumColumns");
				}
			}
		}

		#endregion 
        
        public int CompareTo(Object objCourt)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((Court)objCourt).ID);
		}
        public string CourtAndLastDischargeDate
        {
            get
            {
                if (_lastPacerLoadDischargeDate != null)
                {
                    return _courtName + ": " + ((DateTime)_lastPacerLoadDischargeDate).ToShortDateString();
                }
                else
                    return _courtName;
            }
        }
        public string CourtAndFileVersion
        {
            get
            {
                if (PacerFileVersion == null)
                {
                    return _courtName;
                }
                else
                {
                    return _courtName + ": " + PacerFileVersion;
                }
            }
        }   
        public string LastPacerLoadDischargeDateString
        {
            get
            {
                if (_lastPacerLoadDischargeDate != null && _lastPacerLoadDischargeDate > DateTime.MinValue)
                {
                    return ((DateTime)_lastPacerLoadDischargeDate).ToShortDateString();
                }
                else
                    return string.Empty;
            }
        }    
        public string LastPacerLoadFileDateString
        {
            get
            {
                if (_lastPacerLoadFileDate != null && _lastPacerLoadFileDate > DateTime.MinValue)
                {
                    return ((DateTime)_lastPacerLoadFileDate).ToShortDateString();
                }
                else
                    return string.Empty;
            }
        }    
    }
}
