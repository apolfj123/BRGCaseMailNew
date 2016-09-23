using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the DealerZipCode table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/6/2011 4:33:52 PM
	/// </summary>
	public class DealerZipCode 
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
		public DealerZipCode()
		{
			_dealerID = 0;
			_zipGeoCodeID = 0;
		}
		public DealerZipCode(int DealerID, int ZipGeoCodeID)		{
			_dealerID = DealerID;
			_zipGeoCodeID = ZipGeoCodeID;
		}
		public DealerZipCode Copy()
		{
			DealerZipCode _dealerZipCode = new DealerZipCode();
			_dealerZipCode.DealerID = _dealerID;
			_dealerZipCode.ZipGeoCodeID = _zipGeoCodeID;
			return _dealerZipCode;
		}
		#region "Private Instance Variables"
		private int _dealerID;
		private int _zipGeoCodeID;
		#endregion 
		#region "Public Properties"
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

		public int ZipGeoCodeID
		{
			get
			{
				return _zipGeoCodeID;
			}
			set
			{
				if (value != _zipGeoCodeID)
				{
					this._isModified = true;
					_zipGeoCodeID = value;
				}
			}
		}

		#endregion 
		
	}
}
