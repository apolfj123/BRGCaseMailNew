using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the ZipGeoCode table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/26/2011 4:38:30 PM
	/// </summary>
	public class ZipGeoCode : IComparable
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
		public ZipGeoCode()
		{
			_iD = 0;
			_zipCode = 0;
			_zipCodeString = string.Empty;
			_state = string.Empty;
            _city = string.Empty;
			_latitude = 0.0;
			_longitude = 0.0;
			_casesAvailable = 0;
			_casesMarkedSold = 0;
		}
		public ZipGeoCode(int ID, int ZipCode, string ZipCodeString, string City, string State, double Latitude, double Longitude, int CasesAvailable, int CasesMarkedSold)		{
			_iD = ID;
			_zipCode = ZipCode;
			_zipCodeString = ZipCodeString;
			_city = City;
			_state = State;
			_latitude = Latitude;
			_longitude = Longitude;
			_casesAvailable = CasesAvailable;
			_casesMarkedSold = CasesMarkedSold;
		}
		public ZipGeoCode Copy()
		{
			ZipGeoCode _zipGeoCode = new ZipGeoCode();
			_zipGeoCode.ID = _iD;
			_zipGeoCode.ZipCode = _zipCode;
			_zipGeoCode.ZipCodeString = _zipCodeString;
			_zipGeoCode.City = _city;
			_zipGeoCode.State = _state;
			_zipGeoCode.Latitude = _latitude;
			_zipGeoCode.Longitude = _longitude;
			_zipGeoCode.CasesAvailable = _casesAvailable;
			_zipGeoCode.CasesMarkedSold = _casesMarkedSold;
			return _zipGeoCode;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _zipCode;
		private string _zipCodeString;
		private string _city;
		private string _state;
		private double _latitude;
		private double _longitude;
		private int _casesAvailable;
		private int _casesMarkedSold;
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

		public int ZipCode
		{
			get
			{
				return _zipCode;
			}
			set
			{
				if (value != _zipCode)
				{
					this._isModified = true;
					_zipCode = value;
				}
			}
		}

		public string ZipCodeString
		{
			get
			{
				return _zipCodeString;
			}
			set
			{
				if (value != _zipCodeString)
				{
					this._isModified = true;
					_zipCodeString = value;
				}
			}
		}

		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				if (value != _city)
				{
					this._isModified = true;
					_city = value;
				}
			}
		}

		public string State
		{
			get
			{
				return _state;
			}
			set
			{
				if (value != _state)
				{
					this._isModified = true;
					_state = value;
				}
			}
		}

		public double Latitude
		{
			get
			{
				return _latitude;
			}
			set
			{
				if (value != _latitude)
				{
					this._isModified = true;
					_latitude = value;
				}
			}
		}

		public double Longitude
		{
			get
			{
				return _longitude;
			}
			set
			{
				if (value != _longitude)
				{
					this._isModified = true;
					_longitude = value;
				}
			}
		}

		public int CasesAvailable
		{
			get
			{
				return _casesAvailable;
			}
			set
			{
				if (value != _casesAvailable)
				{
					this._isModified = true;
					_casesAvailable = value;
				}
			}
		}

		public int CasesMarkedSold
		{
			get
			{
				return _casesMarkedSold;
			}
			set
			{
				if (value != _casesMarkedSold)
				{
					this._isModified = true;
					_casesMarkedSold = value;
				}
			}
		}

		#endregion 

        private double _distance;

        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                if (value != _distance)
                {
                    this._isModified = true;
                    _distance = value;
                }
            }
        }

        public string DistanceString
        {
            get 
            {
                return Distance.ToString("N1");
            }
        }

		public int CompareTo(Object objZipGeoCode)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((ZipGeoCode)objZipGeoCode).ID);
		}
	}
}
