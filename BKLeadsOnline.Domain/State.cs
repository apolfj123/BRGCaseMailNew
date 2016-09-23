using System;

using System.Collections;

using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the State table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/27/2013 7:10:13 PM
	/// </summary>
	public class State : INotifyPropertyChanged
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
		public State()
		{
			_stateCode = string.Empty;
			_stateName = string.Empty;
			_include = false;
			_productAvailable = false;
		}
		public State(string StateCode, string StateName, bool Include, bool ProductAvailable )		{
			_stateCode = StateCode;
			_stateName = StateName;
			_include = Include;
			_productAvailable = ProductAvailable;
		}
		public State Copy()
		{
			State _state = new State();
			_state.StateCode = _stateCode;
			_state.StateName = _stateName;
			_state.Include = _include;
			_state.ProductAvailable = _productAvailable;
			return _state;
		}
		#region "Private Instance Variables"
		private string _stateCode;
		private string _stateName;
		private bool _include;
		private bool _productAvailable;
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

		public string StateName
		{
			get
			{
				return _stateName;
			}
			set
			{
				if (value != _stateName)
				{
					this._isModified = true;
					_stateName = value;
					NotifyPropertyChanged("StateName");
				}
			}
		}

		public bool Include
		{
			get
			{
				return _include;
			}
			set
			{
				if (value != _include)
				{
					this._isModified = true;
					_include = value;
					NotifyPropertyChanged("Include");
				}
			}
		}

		public bool ProductAvailable
		{
			get
			{
				return _productAvailable;
			}
			set
			{
				if (value != _productAvailable)
				{
					this._isModified = true;
					_productAvailable = value;
					NotifyPropertyChanged("ProductAvailable");
				}
			}
		}

		#endregion 
	}
}
