using System;

using System.Collections;

using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the ZipCodePart table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:26 PM
	/// </summary>
	public class ZipCodePart : IComparable, INotifyPropertyChanged
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
		public ZipCodePart()
		{
			_stateCode = string.Empty;
			_zipPart = string.Empty;
		}
		public ZipCodePart(string StateCode, string ZipPart)		{
			_stateCode = StateCode;
			_zipPart = ZipPart;
		}
		public ZipCodePart Copy()
		{
			ZipCodePart _zipCodePart = new ZipCodePart();
			_zipCodePart.StateCode = _stateCode;
			_zipCodePart.ZipPart = _zipPart;
			return _zipCodePart;
		}
		#region "Private Instance Variables"
		private string _stateCode;
		private string _zipPart;
		#endregion 
		#region "Public Properties"
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

		public string ZipPart
		{
			get
			{
				return _zipPart;
			}
			set
			{
				if (value != _zipPart)
				{
					this._isModified = true;
					_zipPart = value;
					NotifyPropertyChanged("ZipPart");
				}
			}
		}

		#endregion 
		public int CompareTo(Object objZipCodePart)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this._zipPart.CompareTo(((ZipCodePart)objZipCodePart).ZipPart);
		}
	}
}
