using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the DealerMailingList table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/9/2011 10:57:59 AM
	/// </summary>
	public class DealerMailingList : IComparable
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
		public DealerMailingList()
		{
			_iD = 0;
			_dealerID = 0;
			_creationDate = System.DateTime.MinValue;
			_startFilterDate = System.DateTime.MinValue;
			_endFilterDate = System.DateTime.MinValue;
			_filedCasesOnly = false;
			_numberCases = 0;
			_markedAsSold = false;
			_description = string.Empty;
			_templateFilePath = string.Empty;
			_csvFilePath = string.Empty;
			_mailMergeFilePath = string.Empty;
		}
		public DealerMailingList(int ID, int DealerID, DateTime CreationDate, DateTime StartFilterDate, DateTime EndFilterDate, bool FiledCasesOnly, int NumberCases, bool MarkedAsSold, string Description, string TemplateFilePath, string CsvFilePath, string MailMergeFilePath)		{
			_iD = ID;
			_dealerID = DealerID;
			_creationDate = CreationDate;
			_startFilterDate = StartFilterDate;
			_endFilterDate = EndFilterDate;
			_filedCasesOnly = FiledCasesOnly;
			_numberCases = NumberCases;
			_markedAsSold = MarkedAsSold;
			_description = Description;
			_templateFilePath = TemplateFilePath;
			_csvFilePath = CsvFilePath;
			_mailMergeFilePath = MailMergeFilePath;
		}
		public DealerMailingList Copy()
		{
			DealerMailingList _dealerMailingList = new DealerMailingList();
			_dealerMailingList.ID = _iD;
			_dealerMailingList.DealerID = _dealerID;
			_dealerMailingList.CreationDate = _creationDate;
			_dealerMailingList.StartFilterDate = _startFilterDate;
			_dealerMailingList.EndFilterDate = _endFilterDate;
			_dealerMailingList.FiledCasesOnly = _filedCasesOnly;
			_dealerMailingList.NumberCases = _numberCases;
			_dealerMailingList.MarkedAsSold = _markedAsSold;
			_dealerMailingList.Description = _description;
			_dealerMailingList.TemplateFilePath = _templateFilePath;
			_dealerMailingList.CsvFilePath = _csvFilePath;
			_dealerMailingList.MailMergeFilePath = _mailMergeFilePath;
			return _dealerMailingList;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _dealerID;
		private DateTime _creationDate;
		private DateTime _startFilterDate;
		private DateTime _endFilterDate;
		private bool _filedCasesOnly;
		private int _numberCases;
		private bool _markedAsSold;
		private string _description;
		private string _templateFilePath;
		private string _csvFilePath;
		private string _mailMergeFilePath;
        private bool _trackingLetterRecieved;
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

		public int DealerID
		{
			get
			{
				return _dealerID;
			}
			set
			{
				if (value != _dealerID)
				{
					this._isModified = true;
					_dealerID = value;
				}
			}
		}

		public DateTime CreationDate
		{
			get
			{
				return _creationDate;
			}
			set
			{
				if (value != _creationDate)
				{
					this._isModified = true;
					_creationDate = value;
				}
			}
		}

		public DateTime StartFilterDate
		{
			get
			{
				return _startFilterDate;
			}
			set
			{
				if (value != _startFilterDate)
				{
					this._isModified = true;
					_startFilterDate = value;
				}
			}
		}

		public DateTime EndFilterDate
		{
			get
			{
				return _endFilterDate;
			}
			set
			{
				if (value != _endFilterDate)
				{
					this._isModified = true;
					_endFilterDate = value;
				}
			}
		}

		public bool FiledCasesOnly
		{
			get
			{
				return _filedCasesOnly;
			}
			set
			{
				if (value != _filedCasesOnly)
				{
					this._isModified = true;
					_filedCasesOnly = value;
				}
			}
		}

		public int NumberCases
		{
			get
			{
				return _numberCases;
			}
			set
			{
				if (value != _numberCases)
				{
					this._isModified = true;
					_numberCases = value;
				}
			}
		}

		public bool MarkedAsSold
		{
			get
			{
				return _markedAsSold;
			}
			set
			{
				if (value != _markedAsSold)
				{
					this._isModified = true;
					_markedAsSold = value;
				}
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				if (value != _description)
				{
					this._isModified = true;
					_description = value;
				}
			}
		}

		public string TemplateFilePath
		{
			get
			{
				return _templateFilePath;
			}
			set
			{
				if (value != _templateFilePath)
				{
					this._isModified = true;
					_templateFilePath = value;
				}
			}
		}

		public string CsvFilePath
		{
			get
			{
				return _csvFilePath;
			}
			set
			{
				if (value != _csvFilePath)
				{
					this._isModified = true;
					_csvFilePath = value;
				}
			}
		}

		public string MailMergeFilePath
		{
			get
			{
				return _mailMergeFilePath;
			}
			set
			{
				if (value != _mailMergeFilePath)
				{
					this._isModified = true;
					_mailMergeFilePath = value;
				}
			}
		}

		public bool TrackingLetterRecieved
		{
			get
			{
				return _trackingLetterRecieved;
			}
			set
			{
				if (value != _trackingLetterRecieved)
				{
					this._isModified = true;
					_trackingLetterRecieved = value;
				}
			}
		}

        #endregion 
		
        public int CompareTo(Object objDealerMailingList)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((DealerMailingList)objDealerMailingList).ID);
		}
        public string CreationDateString
        {
            get
            {
                return _creationDate.ToString("MM/dd/yyyy");
            }
        }
        public string StartFilterDateString
        {
            get
            {
                return _startFilterDate.ToString("MM/dd/yyyy");
            }
        }
        public string EndFilterDateString
        {
            get
            {
                return _endFilterDate.ToString("MM/dd/yyyy");
            }
        }
        public string TemplateFileName
        {
            get
            {
                if (_templateFilePath.Length > 0 && _templateFilePath.IndexOf("\\") > 0)
                {
                    return _templateFilePath.Substring(_templateFilePath.LastIndexOf("\\") + 1, _templateFilePath.Length - _templateFilePath.LastIndexOf("\\") - 1);
                }
                else
                    return null;
            }
        }
    }
}
