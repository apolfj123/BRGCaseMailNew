using System;

using System.Collections;

using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the CellCarrier table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:26 PM
	/// </summary>
	public class CellCarrier : IComparable, INotifyPropertyChanged
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
		public CellCarrier()
		{
			_iD = 0;
			_name = string.Empty;
			_txtEmailSuffix = string.Empty;
			_active = false;
		}
		public CellCarrier(int ID, string Name, string TxtEmailSuffix, bool Active )		{
			_iD = ID;
			_name = Name;
			_txtEmailSuffix = TxtEmailSuffix;
			_active = Active;
		}
		public CellCarrier Copy()
		{
			CellCarrier _cellCarrier = new CellCarrier();
			_cellCarrier.ID = _iD;
			_cellCarrier.Name = _name;
			_cellCarrier.TxtEmailSuffix = _txtEmailSuffix;
			_cellCarrier.Active = _active;
			return _cellCarrier;
		}
		#region "Private Instance Variables"
		private int _iD;
		private string _name;
		private string _txtEmailSuffix;
		private bool _active;
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

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value != _name)
				{
					this._isModified = true;
					_name = value;
					NotifyPropertyChanged("Name");
				}
			}
		}

		public string TxtEmailSuffix
		{
			get
			{
				return _txtEmailSuffix;
			}
			set
			{
				if (value != _txtEmailSuffix)
				{
					this._isModified = true;
					_txtEmailSuffix = value;
					NotifyPropertyChanged("TxtEmailSuffix");
				}
			}
		}

		public bool Active
		{
			get
			{
				return _active;
			}
			set
			{
				if (value != _active)
				{
					this._isModified = true;
					_active = value;
					NotifyPropertyChanged("Active");
				}
			}
		}

		#endregion 
		public int CompareTo(Object objCellCarrier)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((CellCarrier)objCellCarrier).ID);
		}
	}
}
