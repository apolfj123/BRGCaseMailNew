using System;

using System.Collections;

using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the ZipGeoCode table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:26 PM
	/// </summary>
	public class ZipGeoCode : IComparable, INotifyPropertyChanged
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
		public ZipGeoCode()
		{
			_iD = 0;
			_zipCode = 0;
			_zipCodeString = string.Empty;
			_city = string.Empty;
			_state = string.Empty;
			_county = string.Empty;
			_areaCode = string.Empty;
			_latitude = 0;
			_longitude = 0;
            _numLeadsAvailable = 0;
		}
		public ZipGeoCode(int ID, int ZipCode, string ZipCodeString, string City, string State, string County, string AreaCode, double Latitude, double Longitude)		{
			_iD = ID;
			_zipCode = ZipCode;
			_zipCodeString = ZipCodeString;
			_city = City;
			_state = State;
			_county = County;
			_areaCode = AreaCode;
			_latitude = Latitude;
			_longitude = Longitude;
		}
		public ZipGeoCode Copy()
		{
			ZipGeoCode _zipGeoCode = new ZipGeoCode();
			_zipGeoCode.ID = _iD;
			_zipGeoCode.ZipCode = _zipCode;
			_zipGeoCode.ZipCodeString = _zipCodeString;
			_zipGeoCode.City = _city;
			_zipGeoCode.State = _state;
			_zipGeoCode.County = _county;
			_zipGeoCode.AreaCode = _areaCode;
			_zipGeoCode.Latitude = _latitude;
			_zipGeoCode.Longitude = _longitude;
			return _zipGeoCode;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _zipCode;
		private string _zipCodeString;
		private string _city;
		private string _state;
		private string _county;
		private string _areaCode;
		private double _latitude;
		private double _longitude;
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
					NotifyPropertyChanged("ZipCode");
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
					NotifyPropertyChanged("ZipCodeString");
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
					NotifyPropertyChanged("City");
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
					NotifyPropertyChanged("State");
				}
			}
		}

		public string County
		{
			get
			{
				return _county;
			}
			set
			{
				if (value != _county)
				{
					this._isModified = true;
					_county = value;
					NotifyPropertyChanged("County");
				}
			}
		}

		public string AreaCode
		{
			get
			{
				return _areaCode;
			}
			set
			{
				if (value != _areaCode)
				{
					this._isModified = true;
					_areaCode = value;
					NotifyPropertyChanged("AreaCode");
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
					NotifyPropertyChanged("Latitude");
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
					NotifyPropertyChanged("Longitude");
				}
			}
		}

		#endregion 
		public int CompareTo(Object objZipGeoCode)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((ZipGeoCode)objZipGeoCode).ID);
		}

        #region derived properties
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
        #endregion

        #region Additional Properties
        private int _numLeadsAvailable;
        
        public int NumLeadsAvailable
        {
            get
            {
                return _numLeadsAvailable;
            }
            set
            {
                _numLeadsAvailable = value;
            }
        }
            
        #endregion
    }
}
