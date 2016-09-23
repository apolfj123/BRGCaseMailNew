using System;
using System.Collections;
using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the Dealer table.
	/// Author: Jonathan Shaw
	/// Date Created: 3/27/2013 4:15:26 PM
	/// </summary>
	public class Dealer : IComparable, INotifyPropertyChanged
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
		public Dealer()
		{
			_iD = 0;
			_dealerName = string.Empty;
			_dealerNumber = 0;
			_addressLine1 = string.Empty;
			_city = string.Empty;
			_state = string.Empty;
			_zip = string.Empty;
			_phone = string.Empty;
			_fax = string.Empty;
			_latitude = 0;
			_longitude = 0;
			_maxDistance = 0;
			_dealerURL = string.Empty;
			_exportFilePath = string.Empty;
			_createdOn = System.DateTime.MinValue;
			_lastUpdate = System.DateTime.MinValue;
			_numberOfMailers = 0;
			_startDate = System.DateTime.MinValue;
			_term = string.Empty;
			//_dealerLogo = 0;
			_price = 0;
			_acknowledgedTOS = false;
			_acknowledgedTOSDate = System.DateTime.MinValue;
			_acknowledgedTOSName = string.Empty;
			//_acknowledgedTOSSignature = 0;
			_defaultDocTemplateID = 0;
			_legalDisclaimer = string.Empty;
			_active = false;
			_dealerPrimaryContactName = string.Empty;
			_dealerPrimaryContactPhone = string.Empty;
			_defaultDocTemplateDisplayName = string.Empty;
		}
		public Dealer(int ID, string DealerName, int DealerNumber, string AddressLine1, string City, string State, string Zip, string Phone, string Fax, double Latitude, double Longitude, double MaxDistance, string DealerURL, string ExportFilePath, DateTime CreatedOn, DateTime LastUpdate, int NumberOfMailers, DateTime StartDate, string Term, byte[]  DealerLogo, int Price, bool AcknowledgedTOS, DateTime AcknowledgedTOSDate, string AcknowledgedTOSName, byte[]  AcknowledgedTOSSignature, int DefaultDocTemplateID, string LegalDisclaimer, bool Active, string DealerPrimaryContactName, string DealerPrimaryContactPhone, string DefaultDocTemplateDisplayName)		{
			_iD = ID;
			_dealerName = DealerName;
			_dealerNumber = DealerNumber;
			_addressLine1 = AddressLine1;
			_city = City;
			_state = State;
			_zip = Zip;
			_phone = Phone;
			_fax = Fax;
			_latitude = Latitude;
			_longitude = Longitude;
			_maxDistance = MaxDistance;
			_dealerURL = DealerURL;
			_exportFilePath = ExportFilePath;
			_createdOn = CreatedOn;
			_lastUpdate = LastUpdate;
			_numberOfMailers = NumberOfMailers;
			_startDate = StartDate;
			_term = Term;
			_dealerLogo = DealerLogo;
			_price = Price;
			_acknowledgedTOS = AcknowledgedTOS;
			_acknowledgedTOSDate = AcknowledgedTOSDate;
			_acknowledgedTOSName = AcknowledgedTOSName;
			_acknowledgedTOSSignature = AcknowledgedTOSSignature;
			_defaultDocTemplateID = DefaultDocTemplateID;
			_legalDisclaimer = LegalDisclaimer;
			_active = Active;
			_dealerPrimaryContactName = DealerPrimaryContactName;
			_dealerPrimaryContactPhone = DealerPrimaryContactPhone;
			_defaultDocTemplateDisplayName = DefaultDocTemplateDisplayName;
		}
		public Dealer Copy()
		{
			Dealer _dealer = new Dealer();
			_dealer.ID = _iD;
			_dealer.DealerName = _dealerName;
			_dealer.DealerNumber = _dealerNumber;
			_dealer.AddressLine1 = _addressLine1;
			_dealer.City = _city;
			_dealer.State = _state;
			_dealer.Zip = _zip;
			_dealer.Phone = _phone;
			_dealer.Fax = _fax;
			_dealer.Latitude = _latitude;
			_dealer.Longitude = _longitude;
			_dealer.MaxDistance = _maxDistance;
			_dealer.DealerURL = _dealerURL;
			_dealer.ExportFilePath = _exportFilePath;
			_dealer.CreatedOn = _createdOn;
			_dealer.LastUpdate = _lastUpdate;
			_dealer.NumberOfMailers = _numberOfMailers;
			_dealer.StartDate = _startDate;
			_dealer.Term = _term;
			_dealer.DealerLogo = _dealerLogo;
			_dealer.Price = _price;
			_dealer.AcknowledgedTOS = _acknowledgedTOS;
			_dealer.AcknowledgedTOSDate = _acknowledgedTOSDate;
			_dealer.AcknowledgedTOSName = _acknowledgedTOSName;
			_dealer.AcknowledgedTOSSignature = _acknowledgedTOSSignature;
			_dealer.DefaultDocTemplateID = _defaultDocTemplateID;
			_dealer.LegalDisclaimer = _legalDisclaimer;
			_dealer.Active = _active;
			_dealer.DealerPrimaryContactName = _dealerPrimaryContactName;
			_dealer.DealerPrimaryContactPhone = _dealerPrimaryContactPhone;
			_dealer.DefaultDocTemplateDisplayName = _defaultDocTemplateDisplayName;
			return _dealer;
		}
		#region "Private Instance Variables"
		private int _iD;
		private string _dealerName;
		private int _dealerNumber;
		private string _addressLine1;
		private string _city;
		private string _state;
		private string _zip;
		private string _phone;
		private string _fax;
		private double _latitude;
		private double _longitude;
		private double _maxDistance;
		private string _dealerURL;
		private string _exportFilePath;
		private DateTime _createdOn;
		private DateTime _lastUpdate;
		private int _numberOfMailers;
		private DateTime _startDate;
		private string _term;
		private byte[] _dealerLogo;
		private decimal _price;
		private bool _acknowledgedTOS;
		private DateTime _acknowledgedTOSDate;
		private string _acknowledgedTOSName;
		private byte[] _acknowledgedTOSSignature;
		private int _defaultDocTemplateID;
		private string _legalDisclaimer;
		private bool _active;
		private string _dealerPrimaryContactName;
		private string _dealerPrimaryContactPhone;
		private string _defaultDocTemplateDisplayName;
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

		public int DealerNumber
		{
			get
			{
				return _dealerNumber;
			}
			set
			{
				if (value != _dealerNumber)
				{
					this._isModified = true;
					_dealerNumber = value;
					NotifyPropertyChanged("DealerNumber");
				}
			}
		}

		public string AddressLine1
		{
			get
			{
				return _addressLine1;
			}
			set
			{
				if (value != _addressLine1)
				{
					this._isModified = true;
					_addressLine1 = value;
					NotifyPropertyChanged("AddressLine1");
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

		public string Zip
		{
			get
			{
				return _zip;
			}
			set
			{
				if (value != _zip)
				{
					this._isModified = true;
					_zip = value;
					NotifyPropertyChanged("Zip");
				}
			}
		}

		public string Phone
		{
			get
			{
				return _phone;
			}
			set
			{
				if (value != _phone)
				{
					this._isModified = true;
					_phone = value;
					NotifyPropertyChanged("Phone");
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
					NotifyPropertyChanged("Fax");
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
					NotifyPropertyChanged("MaxDistance");
				}
			}
		}

		public string DealerURL
		{
			get
			{
				return _dealerURL;
			}
			set
			{
				if (value != _dealerURL)
				{
					this._isModified = true;
					_dealerURL = value;
					NotifyPropertyChanged("DealerURL");
				}
			}
		}

		public string ExportFilePath
		{
			get
			{
				return _exportFilePath;
			}
			set
			{
				if (value != _exportFilePath)
				{
					this._isModified = true;
					_exportFilePath = value;
					NotifyPropertyChanged("ExportFilePath");
				}
			}
		}

		public DateTime CreatedOn
		{
			get
			{
				return _createdOn;
			}
			set
			{
				if (value != _createdOn)
				{
					this._isModified = true;
					_createdOn = value;
					NotifyPropertyChanged("CreatedOn");
				}
			}
		}

		public DateTime LastUpdate
		{
			get
			{
				return _lastUpdate;
			}
			set
			{
				if (value != _lastUpdate)
				{
					this._isModified = true;
					_lastUpdate = value;
					NotifyPropertyChanged("LastUpdate");
				}
			}
		}

		public int NumberOfMailers
		{
			get
			{
				return _numberOfMailers;
			}
			set
			{
				if (value != _numberOfMailers)
				{
					this._isModified = true;
					_numberOfMailers = value;
					NotifyPropertyChanged("NumberOfMailers");
				}
			}
		}

		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				if (value != _startDate)
				{
					this._isModified = true;
					_startDate = value;
					NotifyPropertyChanged("StartDate");
				}
			}
		}

		public string Term
		{
			get
			{
				return _term;
			}
			set
			{
				if (value != _term)
				{
					this._isModified = true;
					_term = value;
					NotifyPropertyChanged("Term");
				}
			}
		}

		public byte[] DealerLogo
		{
			get
			{
				return _dealerLogo;
			}
			set
			{
				if (value != _dealerLogo)
				{
					this._isModified = true;
					_dealerLogo = value;
					NotifyPropertyChanged("DealerLogo");
				}
			}
		}

		public decimal Price
		{
			get
			{
				return _price;
			}
			set
			{
				if (value != _price)
				{
					this._isModified = true;
					_price = value;
					NotifyPropertyChanged("Price");
				}
			}
		}

		public bool AcknowledgedTOS
		{
			get
			{
				return _acknowledgedTOS;
			}
			set
			{
				if (value != _acknowledgedTOS)
				{
					this._isModified = true;
					_acknowledgedTOS = value;
					NotifyPropertyChanged("AcknowledgedTOS");
				}
			}
		}

		public DateTime AcknowledgedTOSDate
		{
			get
			{
				return _acknowledgedTOSDate;
			}
			set
			{
				if (value != _acknowledgedTOSDate)
				{
					this._isModified = true;
					_acknowledgedTOSDate = value;
					NotifyPropertyChanged("AcknowledgedTOSDate");
				}
			}
		}

		public string AcknowledgedTOSName
		{
			get
			{
				return _acknowledgedTOSName;
			}
			set
			{
				if (value != _acknowledgedTOSName)
				{
					this._isModified = true;
					_acknowledgedTOSName = value;
					NotifyPropertyChanged("AcknowledgedTOSName");
				}
			}
		}

		public byte[] AcknowledgedTOSSignature
		{
			get
			{
				return _acknowledgedTOSSignature;
			}
			set
			{
				if (value != _acknowledgedTOSSignature)
				{
					this._isModified = true;
					_acknowledgedTOSSignature = value;
					NotifyPropertyChanged("AcknowledgedTOSSignature");
				}
			}
		}

		public int DefaultDocTemplateID
		{
			get
			{
				return _defaultDocTemplateID;
			}
			set
			{
				if (value != _defaultDocTemplateID)
				{
					this._isModified = true;
					_defaultDocTemplateID = value;
					NotifyPropertyChanged("DefaultDocTemplateID");
				}
			}
		}

		public string LegalDisclaimer
		{
			get
			{
				return _legalDisclaimer;
			}
			set
			{
				if (value != _legalDisclaimer)
				{
					this._isModified = true;
					_legalDisclaimer = value;
					NotifyPropertyChanged("LegalDisclaimer");
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

		public string DealerPrimaryContactName
		{
			get
			{
				return _dealerPrimaryContactName;
			}
			set
			{
				if (value != _dealerPrimaryContactName)
				{
					this._isModified = true;
					_dealerPrimaryContactName = value;
					NotifyPropertyChanged("DealerPrimaryContactName");
				}
			}
		}

		public string DealerPrimaryContactPhone
		{
			get
			{
				return _dealerPrimaryContactPhone;
			}
			set
			{
				if (value != _dealerPrimaryContactPhone)
				{
					this._isModified = true;
					_dealerPrimaryContactPhone = value;
					NotifyPropertyChanged("DealerPrimaryContactPhone");
				}
			}
		}

		public string DefaultDocTemplateDisplayName
		{
			get
			{
				return _defaultDocTemplateDisplayName;
			}
			set
			{
				if (value != _defaultDocTemplateDisplayName)
				{
					this._isModified = true;
					_defaultDocTemplateDisplayName = value;
					NotifyPropertyChanged("DefaultDocTemplateDisplayName");
				}
			}
		}

		#endregion 

        public double MaxDistanceInMeters { get { return 1609.34 * MaxDistance; } }

        public string AcknowledgedTOSDateString
        {
            get
            {
                if (_acknowledgedTOSDate > DateTime.MinValue)
                {
                    return _acknowledgedTOSDate.ToString("MM/dd/yyyy hh:mm:ss");
                }
                else
                    return string.Empty;
            }
        }
        
        public string AcknowledgedOnTOSDateString
        {
            get
            {
                if (_acknowledgedTOSDate > DateTime.MinValue)
                {
                    return "Acknowledged On: " + _acknowledgedTOSDate.ToString("MM/dd/yyyy hh:mm tt");
                }
                else
                    return string.Empty;
            }
        }
        
        public string AcknowledgedByTOSName
        {
            get
            {
                if (_acknowledgedTOSName.Length > 0)
                {
                    return "Acknowledged By: " + _acknowledgedTOSName;
                }
                else
                    return string.Empty;
            }
        }
            
        public string PriceString
        {
            get
            {
                if (_price > 0)
                {
                    return _price.ToString("C");
                }
                else
                    return string.Empty;
            }
        }
        public string DistanceString
        {
            get
            {
                return MaxDistanceInMeters.ToString("N2");
            }
        }
        public string DealerFullAddress
        {
            get
            {
                return _addressLine1 + ", " + _city + ", " + _state + "  " + _zip;
            }
        }
		public int CompareTo(Object objDealer)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((Dealer)objDealer).ID);
		}
	}
}
