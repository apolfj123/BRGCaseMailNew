using System;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using System.Text;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the DealerMailingList table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/21/2013 2:39:21 PM
	/// </summary>
	public class DealerMailingList : IComparable, INotifyPropertyChanged
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
		public DealerMailingList()
		{
			_iD = 0;
			_dealerID = 0;
			_creationDate = System.DateTime.MinValue;
			_startFilterDate = System.DateTime.MinValue;
			_endFilterDate = System.DateTime.MinValue;
			_numberCases = 0;
			_printed = false;
			_docTemplateID = 0;
			_dealerName = string.Empty;
			_docTemplateDisplayName = string.Empty;
		}
		public DealerMailingList(int ID, int DealerID, DateTime CreationDate, DateTime StartFilterDate, DateTime EndFilterDate, int NumberCases, bool Printed, int DocTemplateID, string DealerName, string DocTemplateDisplayName)		{
			_iD = ID;
			_dealerID = DealerID;
			_creationDate = CreationDate;
			_startFilterDate = StartFilterDate;
			_endFilterDate = EndFilterDate;
			_numberCases = NumberCases;
			_printed = Printed;
			_docTemplateID = DocTemplateID;
			_dealerName = DealerName;
			_docTemplateDisplayName = DocTemplateDisplayName;
		}
		public DealerMailingList Copy()
		{
			DealerMailingList _dealerMailingList = new DealerMailingList();
			_dealerMailingList.ID = _iD;
			_dealerMailingList.DealerID = _dealerID;
			_dealerMailingList.CreationDate = _creationDate;
			_dealerMailingList.StartFilterDate = _startFilterDate;
			_dealerMailingList.EndFilterDate = _endFilterDate;
			_dealerMailingList.NumberCases = _numberCases;
			_dealerMailingList.Printed = _printed;
			_dealerMailingList.DocTemplateID = _docTemplateID;
			_dealerMailingList.DealerName = _dealerName;
			_dealerMailingList.DocTemplateDisplayName = _docTemplateDisplayName;
			return _dealerMailingList;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _dealerID;
		private DateTime _creationDate;
		private DateTime _startFilterDate;
		private DateTime _endFilterDate;
		private int _numberCases;
		private bool _printed;
		private int _docTemplateID;
		private string _dealerName;
		private string _docTemplateDisplayName;
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
					NotifyPropertyChanged("DealerID");
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
					NotifyPropertyChanged("CreationDate");
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
					NotifyPropertyChanged("StartFilterDate");
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
					NotifyPropertyChanged("EndFilterDate");
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
					NotifyPropertyChanged("NumberCases");
				}
			}
		}

		public bool Printed
		{
			get
			{
				return _printed;
			}
			set
			{
				if (value != _printed)
				{
					this._isModified = true;
					_printed = value;
					NotifyPropertyChanged("Printed");
				}
			}
		}

		public int DocTemplateID
		{
			get
			{
				return _docTemplateID;
			}
			set
			{
				if (value != _docTemplateID)
				{
					this._isModified = true;
					_docTemplateID = value;
					NotifyPropertyChanged("DocTemplateID");
				}
			}
		}

		public string DealerName
		{
			get
			{
				return _dealerName;
			}
			set
			{
				if (value != _dealerName)
				{
					this._isModified = true;
					_dealerName = value;
					NotifyPropertyChanged("DealerName");
				}
			}
		}

		public string DocTemplateDisplayName
		{
			get
			{
				return _docTemplateDisplayName;
			}
			set
			{
				if (value != _docTemplateDisplayName)
				{
					this._isModified = true;
					_docTemplateDisplayName = value;
					NotifyPropertyChanged("DocTemplateDisplayName");
				}
			}
		}

		#endregion 
		
        public int CompareTo(Object objDealerMailingList)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((DealerMailingList)objDealerMailingList).ID);
		}
        public string StartFilterDateString
        {
            get
            {
                return StartFilterDate.ToString("MM/dd/yyyy");
            }
        }
        public string EndFilterDateString
        {
            get
            {
                return EndFilterDate.ToString("MM/dd/yyyy");
            }
        }
        public string Name
        {
            get 
            {
                if (CreationDate > DateTime.MinValue)
                {
                    return NumberCases + " Records.  Created:" + CreationDate.ToShortDateString() + " From:" + StartFilterDate.ToShortDateString() + " To:" + EndFilterDate.ToShortDateString() + " using " + DocTemplateDisplayName;
                }
                else
                {
                    //the default pic in the dropdown
                    return "[Select...]";
                }
            }
        }
        public string ToCSV()
        {
            List<BankruptcyCase> _cases = BankruptcyCaseService.getMailingList(this.ID, this.DealerID, false);
            
            StringBuilder builder = new StringBuilder();
            List<string> columnNames = new List<string>();
            List<string> rows = new List<string>();
            columnNames.Add("Lead Full Name");
            columnNames.Add("Address");
            columnNames.Add("City");
            columnNames.Add("State");
            columnNames.Add("Zip");

            builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");

            foreach (BankruptcyCase _case in _cases)
            {
                List<string> currentRow = new List<string>();

                currentRow.Add(_case.LeadFullName);
                currentRow.Add(_case.LeadAddress.Replace(',', ' ').Trim()); 
                currentRow.Add(_case.City); 
                currentRow.Add(_case.StateCode); 
                currentRow.Add(_case.PostalCode.ToString());
                rows.Add(string.Join(",", currentRow.ToArray()));
            }

            builder.Append(string.Join("\n", rows.ToArray()));

            return builder.ToString();
        }
	}
}
