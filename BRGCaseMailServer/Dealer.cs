using System;

using System.Collections;
using System.ComponentModel;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the Dealer table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/18/2011 4:53:45 PM
	/// </summary>
    public class Dealer : IComparable, INotifyPropertyChanged
	{

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
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
		public Dealer()
		{
			_iD = 0;
			_companyName = string.Empty;
			_addrLine1 = string.Empty;
			_addrLine2 = string.Empty;
			_city = string.Empty;
			_stateCode = string.Empty;
			_postalCode = string.Empty;
			_country = string.Empty;
			_phoneNumber = string.Empty;
			_fax = string.Empty;
			_email = string.Empty;
			_webSite = string.Empty;
			_contactForFlyers = string.Empty;
			_contactForAccounts = string.Empty;
			_currentCustomer = false;
			_callSource = string.Empty;
			_maxDistance = 0;
			_comment = string.Empty;
            _latitude = null;
            _longitude = null;
			_defaultNumberMailings = 0;
		}

		public Dealer(int ID, string CompanyName, string AddrLine1, string AddrLine2, string City, string StateCode, string PostalCode, string Country, string PhoneNumber, string Fax, string email, string WebSite, string ContactForFlyers, string ContactForAccounts, bool CurrentCustomer, string CallSource, double MaxDistance, string Comment, double Latitude, double Longitude, int DefaultNumberMailings)		{
			_iD = ID;
			_companyName = CompanyName;
			_addrLine1 = AddrLine1;
			_addrLine2 = AddrLine2;
			_city = City;
			_stateCode = StateCode;
			_postalCode = PostalCode;
			_country = Country;
			_phoneNumber = PhoneNumber;
			_fax = Fax;
			_email = email;
			_webSite = WebSite;
			_contactForFlyers = ContactForFlyers;
			_contactForAccounts = ContactForAccounts;
			_currentCustomer = CurrentCustomer;
			_callSource = CallSource;
			_maxDistance = MaxDistance;
			_comment = Comment;
			_latitude = Latitude;
			_longitude = Longitude;
			_defaultNumberMailings = DefaultNumberMailings;
		}
		public Dealer Copy()
		{
			Dealer _dealer = new Dealer();
			_dealer.ID = _iD;
			_dealer.CompanyName = _companyName;
			_dealer.AddrLine1 = _addrLine1;
			_dealer.AddrLine2 = _addrLine2;
			_dealer.City = _city;
			_dealer.StateCode = _stateCode;
			_dealer.PostalCode = _postalCode;
			_dealer.Country = _country;
			_dealer.PhoneNumber = _phoneNumber;
			_dealer.Fax = _fax;
			_dealer.email = _email;
			_dealer.WebSite = _webSite;
			_dealer.ContactForFlyers = _contactForFlyers;
			_dealer.ContactForAccounts = _contactForAccounts;
			_dealer.CurrentCustomer = _currentCustomer;
			_dealer.CallSource = _callSource;
			_dealer.MaxDistance = _maxDistance;
			_dealer.Comment = _comment;
			_dealer.Latitude = _latitude;
			_dealer.Longitude = _longitude;
			_dealer.DefaultNumberMailings = _defaultNumberMailings;
			return _dealer;
		}
		
        #region "Private Instance Variables"
		private int _iD;
		private string _companyName;
		private string _addrLine1;
		private string _addrLine2;
		private string _city;
		private string _stateCode;
		private string _postalCode;
		private string _country;
		private string _phoneNumber;
		private string _fax;
		private string _email;
		private string _webSite;
		private string _contactForFlyers;
		private string _contactForAccounts;
		private bool _currentCustomer;
		private string _callSource;
		private double _maxDistance;
		private string _comment;
		private double? _latitude;
		private double? _longitude;
		private int _defaultNumberMailings;
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

		public string CompanyName
		{
			get
			{
				return _companyName;
			}
			set
			{
				if (value != _companyName)
				{
					this._isModified = true;
					_companyName = value;
				}
			}
		}

		public string AddrLine1
		{
			get
			{
				return _addrLine1;
			}
			set
			{
				if (value != _addrLine1)
				{
					this._isModified = true;
					_addrLine1 = value;
				}
			}
		}

		public string AddrLine2
		{
			get
			{
				return _addrLine2;
			}
			set
			{
				if (value != _addrLine2)
				{
					this._isModified = true;
					_addrLine2 = value;
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

		public string PostalCode
		{
			get
			{
				return _postalCode;
			}
			set
			{
				if (value != _postalCode)
				{
					this._isModified = true;
					_postalCode = value;
				}
			}
		}

		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				if (value != _country)
				{
					this._isModified = true;
					_country = value;
				}
			}
		}

		public string PhoneNumber
		{
			get
			{
				return _phoneNumber;
			}
			set
			{
				if (value != _phoneNumber)
				{
					this._isModified = true;
					_phoneNumber = value;
				}
			}
		}

		public string Fax
		{
			get
			{
				return _fax;
			}
			set
			{
				if (value != _fax)
				{
					this._isModified = true;
					_fax = value;
				}
			}
		}

		public string email
		{
			get
			{
				return _email;
			}
			set
			{
				if (value != _email)
				{
					this._isModified = true;
					_email = value;
				}
			}
		}

		public string WebSite
		{
			get
			{
				return _webSite;
			}
			set
			{
				if (value != _webSite)
				{
					this._isModified = true;
					_webSite = value;
				}
			}
		}

		public string ContactForFlyers
		{
			get
			{
				return _contactForFlyers;
			}
			set
			{
				if (value != _contactForFlyers)
				{
					this._isModified = true;
					_contactForFlyers = value;
				}
			}
		}

		public string ContactForAccounts
		{
			get
			{
				return _contactForAccounts;
			}
			set
			{
				if (value != _contactForAccounts)
				{
					this._isModified = true;
					_contactForAccounts = value;
				}
			}
		}

		public bool CurrentCustomer
		{
			get
			{
				return _currentCustomer;
			}
			set
			{
				if (value != _currentCustomer)
				{
					this._isModified = true;
					_currentCustomer = value;
				}
			}
		}

		public string CallSource
		{
			get
			{
				return _callSource;
			}
			set
			{
				if (value != _callSource)
				{
					this._isModified = true;
					_callSource = value;
				}
			}
		}

		public double MaxDistance
		{
			get
			{
				return _maxDistance;
			}
			set
			{
				if (value != _maxDistance)
				{
					this._isModified = true;
					_maxDistance = value;
				}
			}
		}

		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				if (value != _comment)
				{
					this._isModified = true;
					_comment = value;
				}
			}
		}

		public double? Latitude
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

		public double? Longitude
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

		public int DefaultNumberMailings
		{
			get
			{
				return _defaultNumberMailings;
			}
			set
			{
				if (value != _defaultNumberMailings)
				{
					this._isModified = true;
					_defaultNumberMailings = value;
				}
			}
		}

		#endregion 
		public int CompareTo(Object objDealer)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((Dealer)objDealer).ID);
		}
	}
}
        