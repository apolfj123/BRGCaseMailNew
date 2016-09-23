using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the State table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/5/2011 1:16:55 PM
	/// </summary>
	public class State
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
		public State()
		{
			_stateCode = string.Empty;
			_stateName = string.Empty;
			_include = false;
		}
		public State(string StateCode, string StateName, bool Include )		{
			_stateCode = StateCode;
			_stateName = StateName;
			_include = Include;
		}
		public State Copy()
		{
			State _state = new State();
			_state.StateCode = _stateCode;
			_state.StateName = _stateName;
			_state.Include = _include;
			return _state;
		}
		#region "Private Instance Variables"
		private string _stateCode;
		private string _stateName;
		private bool _include;
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
				}
			}
		}

		#endregion 
        //public int CompareTo(Object objState)
        //{
        //    //sort by ID ascending - this is used by the default sort mechanisc
        //    return this.ID.CompareTo(((State)objState).ID);
        //}

        public string StateDescription
        {
            get
            {
                return _stateCode + ": " + _stateName;
            }
        }
	}
}
