using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the ZipCodePart table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/5/2011 1:52:13 PM
	/// </summary>
	public class ZipCodePart 
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
				}
			}
		}

		#endregion 
	}
}
