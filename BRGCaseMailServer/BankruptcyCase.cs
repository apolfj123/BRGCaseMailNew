using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using BKLeadsOnline.Domain;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the BankruptcyCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/25/2011 10:41:40 PM
	/// </summary>
	public class BankruptcyCase : IComparable, INotifyPropertyChanged
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
		public BankruptcyCase()
		{
			_iD = 0;
			_courtID = 0;
			_cMECF_Internal = 0;
			_caseNumber4Digit = string.Empty;
			_pacerImportTransactionID = 0;
			_chapter = 0;
			_filedOnly = false;
			_filedDate = System.DateTime.MinValue;
			_dischargeDate = System.DateTime.MinValue;
			_trustee = string.Empty;
			_judge = string.Empty;
			_county = string.Empty;
			_office = string.Empty;
			_fee = string.Empty;
			_asset = string.Empty;
			_firstName = string.Empty;
			_middleName = string.Empty;
			_lastName = string.Empty;
			_suffix = string.Empty;
			_addrLine1 = string.Empty;
			_addrLine2 = string.Empty;
			_addrLine3 = string.Empty;
			_city = string.Empty;
			_stateCode = string.Empty;
			_postalCode = 0;
			_postalLast4 = string.Empty;
			_latitude = 0;
            _longitude = 0;
			_country = string.Empty;
			_phone = string.Empty;
			_ssnLast4 = string.Empty;
			_careOf = string.Empty;
			_dontSend = false;
			_returned = false;
			_soldCount = 0;
			_lastSoldDate = System.DateTime.MinValue;
			_bKLeadsOnlineUploadedDate = System.DateTime.MinValue;
			_courtName = string.Empty;
		}
		public BankruptcyCase(int ID, int CourtID, int CMECF_Internal, string CaseNumber4Digit, int PacerImportTransactionID, int Chapter, bool FiledOnly, DateTime FiledDate, DateTime DischargeDate, string Trustee, string Judge, string County, string Office, string Fee, string Asset, string FirstName, string MiddleName, string LastName, string Suffix, string AddrLine1, string AddrLine2, string AddrLine3, string City, string StateCode, int PostalCode, string PostalLast4, double Latitude, double Longitude, string Country, string Phone, string ssnLast4, string CareOf, bool DontSend, bool Returned, int SoldCount, DateTime LastSoldDate, DateTime BKLeadsOnlineUploadedDate, string CourtName)		{
			_iD = ID;
			_courtID = CourtID;
			_cMECF_Internal = CMECF_Internal;
			_caseNumber4Digit = CaseNumber4Digit;
			_pacerImportTransactionID = PacerImportTransactionID;
			_chapter = Chapter;
			_filedOnly = FiledOnly;
			_filedDate = FiledDate;
			_dischargeDate = DischargeDate;
			_trustee = Trustee;
			_judge = Judge;
			_county = County;
			_office = Office;
			_fee = Fee;
			_asset = Asset;
			_firstName = FirstName;
			_middleName = MiddleName;
			_lastName = LastName;
			_suffix = Suffix;
			_addrLine1 = AddrLine1;
			_addrLine2 = AddrLine2;
			_addrLine3 = AddrLine3;
			_city = City;
			_stateCode = StateCode;
			_postalCode = PostalCode;
			_postalLast4 = PostalLast4;
			_latitude = Latitude;
			_longitude = Longitude;
			_country = Country;
			_phone = Phone;
			_ssnLast4 = ssnLast4;
			_careOf = CareOf;
			_dontSend = DontSend;
			_returned = Returned;
			_soldCount = SoldCount;
			_lastSoldDate = LastSoldDate;
			_bKLeadsOnlineUploadedDate = BKLeadsOnlineUploadedDate;
			_courtName = CourtName;
		}
		public BankruptcyCase Copy()
		{
			BankruptcyCase _bankruptcyCase = new BankruptcyCase();
			_bankruptcyCase.ID = _iD;
			_bankruptcyCase.CourtID = _courtID;
			_bankruptcyCase.CMECF_Internal = _cMECF_Internal;
			_bankruptcyCase.CaseNumber4Digit = _caseNumber4Digit;
			_bankruptcyCase.PacerImportTransactionID = _pacerImportTransactionID;
			_bankruptcyCase.Chapter = _chapter;
			_bankruptcyCase.FiledOnly = _filedOnly;
			_bankruptcyCase.FiledDate = _filedDate;
			_bankruptcyCase.DischargeDate = _dischargeDate;
			_bankruptcyCase.Trustee = _trustee;
			_bankruptcyCase.Judge = _judge;
			_bankruptcyCase.County = _county;
			_bankruptcyCase.Office = _office;
			_bankruptcyCase.Fee = _fee;
			_bankruptcyCase.Asset = _asset;
			_bankruptcyCase.FirstName = _firstName;
			_bankruptcyCase.MiddleName = _middleName;
			_bankruptcyCase.LastName = _lastName;
			_bankruptcyCase.Suffix = _suffix;
			_bankruptcyCase.AddrLine1 = _addrLine1;
			_bankruptcyCase.AddrLine2 = _addrLine2;
			_bankruptcyCase.AddrLine3 = _addrLine3;
			_bankruptcyCase.City = _city;
			_bankruptcyCase.StateCode = _stateCode;
			_bankruptcyCase.PostalCode = _postalCode;
			_bankruptcyCase.PostalLast4 = _postalLast4;
			_bankruptcyCase.Latitude = _latitude;
			_bankruptcyCase.Longitude = _longitude;
			_bankruptcyCase.Country = _country;
			_bankruptcyCase.Phone = _phone;
			_bankruptcyCase.ssnLast4 = _ssnLast4;
			_bankruptcyCase.CareOf = _careOf;
			_bankruptcyCase.DontSend = _dontSend;
			_bankruptcyCase.Returned = _returned;
			_bankruptcyCase.SoldCount = _soldCount;
			_bankruptcyCase.LastSoldDate = _lastSoldDate;
			_bankruptcyCase.BKLeadsOnlineUploadedDate = _bKLeadsOnlineUploadedDate;
			_bankruptcyCase.CourtName = _courtName;
			return _bankruptcyCase;
		}
        public void CopyToBKCase(BKLeadsOnline.Domain.BankruptcyCase _bankruptcyCase)
		{
			_bankruptcyCase.ID = _iD;
			_bankruptcyCase.CourtID = _courtID;
			_bankruptcyCase.CMECF_Internal = _cMECF_Internal;
			_bankruptcyCase.CaseNumber4Digit = _caseNumber4Digit;
            _bankruptcyCase.PacerImportTransactionID = _pacerImportTransactionID;
			_bankruptcyCase.Chapter = _chapter;
			_bankruptcyCase.FiledOnly = _filedOnly;
			_bankruptcyCase.FiledDate = _filedDate;
			_bankruptcyCase.DischargeDate = _dischargeDate;
			_bankruptcyCase.Trustee = _trustee;
			_bankruptcyCase.Judge = _judge;
			_bankruptcyCase.County = _county;
			_bankruptcyCase.Office = _office;
			_bankruptcyCase.Fee = _fee;
			_bankruptcyCase.Asset = _asset;
			_bankruptcyCase.FirstName = _firstName;
			_bankruptcyCase.MiddleName = _middleName;
			_bankruptcyCase.LastName = _lastName;
			_bankruptcyCase.Suffix = _suffix;
			_bankruptcyCase.AddrLine1 = _addrLine1;
			_bankruptcyCase.AddrLine2 = _addrLine2;
			_bankruptcyCase.AddrLine3 = _addrLine3;
			_bankruptcyCase.City = _city;
			_bankruptcyCase.StateCode = _stateCode;
			_bankruptcyCase.PostalCode = _postalCode;
			_bankruptcyCase.PostalLast4 = _postalLast4;
			_bankruptcyCase.Latitude = _latitude;
			_bankruptcyCase.Longitude = _longitude;
			_bankruptcyCase.Country = _country;
			_bankruptcyCase.Phone = _phone;
			_bankruptcyCase.ssnLast4 = _ssnLast4;
			_bankruptcyCase.CareOf = _careOf;
			_bankruptcyCase.CourtName = _courtName;
		}
        
		#region "Private Instance Variables"
		private int _iD;
		private int _courtID;
		private int _cMECF_Internal;
		private string _caseNumber4Digit;
		private int _pacerImportTransactionID;
		private int _chapter;
		private bool _filedOnly;
		private DateTime _filedDate;
		private DateTime _dischargeDate;
		private string _trustee;
		private string _judge;
		private string _county;
		private string _office;
		private string _fee;
		private string _asset;
		private string _firstName;
		private string _middleName;
		private string _lastName;
		private string _suffix;
		private string _addrLine1;
		private string _addrLine2;
		private string _addrLine3;
		private string _city;
		private string _stateCode;
		private int _postalCode;
		private string _postalLast4;
		private double _latitude;
		private double _longitude;
		private string _country;
		private string _phone;
		private string _ssnLast4;
		private string _careOf;
		private bool _dontSend;
		private bool _returned;
		private int _soldCount;
		private DateTime _lastSoldDate;
		private DateTime _bKLeadsOnlineUploadedDate;
		private string _courtName;
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

		public int CourtID
		{
			get
			{
				return _courtID;
			}
			set
			{
				if (value != _courtID)
				{
					this._isModified = true;
					_courtID = value;
					NotifyPropertyChanged("CourtID");
				}
			}
		}

		public int CMECF_Internal
		{
			get
			{
				return _cMECF_Internal;
			}
			set
			{
				if (value != _cMECF_Internal)
				{
					this._isModified = true;
					_cMECF_Internal = value;
					NotifyPropertyChanged("CMECF_Internal");
				}
			}
		}

		public string CaseNumber4Digit
		{
			get
			{
				return _caseNumber4Digit;
			}
			set
			{
				if (value != _caseNumber4Digit)
				{
					this._isModified = true;
					_caseNumber4Digit = value;
					NotifyPropertyChanged("CaseNumber4Digit");
				}
			}
		}

		public int PacerImportTransactionID
		{
			get
			{
				return _pacerImportTransactionID;
			}
			set
			{
				if (value != _pacerImportTransactionID)
				{
					this._isModified = true;
					_pacerImportTransactionID = value;
					NotifyPropertyChanged("PacerImportTransactionID");
				}
			}
		}

		public int Chapter
		{
			get
			{
				return _chapter;
			}
			set
			{
				if (value != _chapter)
				{
					this._isModified = true;
					_chapter = value;
					NotifyPropertyChanged("Chapter");
				}
			}
		}

		public bool FiledOnly
		{
			get
			{
				return _filedOnly;
			}
			set
			{
				if (value != _filedOnly)
				{
					this._isModified = true;
					_filedOnly = value;
					NotifyPropertyChanged("FiledOnly");
				}
			}
		}

		public DateTime FiledDate
		{
			get
			{
				return _filedDate;
			}
			set
			{
				if (value != _filedDate)
				{
					this._isModified = true;
					_filedDate = value;
					NotifyPropertyChanged("FiledDate");
				}
			}
		}

		public DateTime DischargeDate
		{
			get
			{
				return _dischargeDate;
			}
			set
			{
				if (value != _dischargeDate)
				{
					this._isModified = true;
					_dischargeDate = value;
					NotifyPropertyChanged("DischargeDate");
				}
			}
		}

		public string Trustee
		{
			get
			{
				return _trustee;
			}
			set
			{
				if (value != _trustee)
				{
					this._isModified = true;
					_trustee = value;
					NotifyPropertyChanged("Trustee");
				}
			}
		}

		public string Judge
		{
			get
			{
				return _judge;
			}
			set
			{
				if (value != _judge)
				{
					this._isModified = true;
					_judge = value;
					NotifyPropertyChanged("Judge");
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

		public string Office
		{
			get
			{
				return _office;
			}
			set
			{
				if (value != _office)
				{
					this._isModified = true;
					_office = value;
					NotifyPropertyChanged("Office");
				}
			}
		}

		public string Fee
		{
			get
			{
				return _fee;
			}
			set
			{
				if (value != _fee)
				{
					this._isModified = true;
					_fee = value;
					NotifyPropertyChanged("Fee");
				}
			}
		}

		public string Asset
		{
			get
			{
				return _asset;
			}
			set
			{
				if (value != _asset)
				{
					this._isModified = true;
					_asset = value;
					NotifyPropertyChanged("Asset");
				}
			}
		}

		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				if (value != _firstName)
				{
					this._isModified = true;
					_firstName = value;
					NotifyPropertyChanged("FirstName");
				}
			}
		}

		public string MiddleName
		{
			get
			{
				return _middleName;
			}
			set
			{
				if (value != _middleName)
				{
					this._isModified = true;
					_middleName = value;
					NotifyPropertyChanged("MiddleName");
				}
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				if (value != _lastName)
				{
					this._isModified = true;
					_lastName = value;
					NotifyPropertyChanged("LastName");
				}
			}
		}

		public string Suffix
		{
			get
			{
				return _suffix;
			}
			set
			{
				if (value != _suffix)
				{
					this._isModified = true;
					_suffix = value;
					NotifyPropertyChanged("Suffix");
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
					NotifyPropertyChanged("AddrLine1");
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
					NotifyPropertyChanged("AddrLine2");
				}
			}
		}

		public string AddrLine3
		{
			get
			{
				return _addrLine3;
			}
			set
			{
				if (value != _addrLine3)
				{
					this._isModified = true;
					_addrLine3 = value;
					NotifyPropertyChanged("AddrLine3");
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

		public int PostalCode
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
					NotifyPropertyChanged("PostalCode");
				}
			}
		}

		public string PostalLast4
		{
			get
			{
				return _postalLast4;
			}
			set
			{
				if (value != _postalLast4)
				{
					this._isModified = true;
					_postalLast4 = value;
					NotifyPropertyChanged("PostalLast4");
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
					NotifyPropertyChanged("Country");
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

		public string ssnLast4
		{
			get
			{
				return _ssnLast4;
			}
			set
			{
				if (value != _ssnLast4)
				{
					this._isModified = true;
					_ssnLast4 = value;
					NotifyPropertyChanged("ssnLast4");
				}
			}
		}

		public string CareOf
		{
			get
			{
				return _careOf;
			}
			set
			{
				if (value != _careOf)
				{
					this._isModified = true;
					_careOf = value;
					NotifyPropertyChanged("CareOf");
				}
			}
		}

		public bool DontSend
		{
			get
			{
				return _dontSend;
			}
			set
			{
				if (value != _dontSend)
				{
					this._isModified = true;
					_dontSend = value;
					NotifyPropertyChanged("DontSend");
				}
			}
		}

		public bool Returned
		{
			get
			{
				return _returned;
			}
			set
			{
				if (value != _returned)
				{
					this._isModified = true;
					_returned = value;
					NotifyPropertyChanged("Returned");
				}
			}
		}

		public int SoldCount
		{
			get
			{
				return _soldCount;
			}
			set
			{
				if (value != _soldCount)
				{
					this._isModified = true;
					_soldCount = value;
					NotifyPropertyChanged("SoldCount");
				}
			}
		}

		public DateTime LastSoldDate
		{
			get
			{
				return _lastSoldDate;
			}
			set
			{
				if (value != _lastSoldDate)
				{
					this._isModified = true;
					_lastSoldDate = value;
					NotifyPropertyChanged("LastSoldDate");
				}
			}
		}

		public DateTime BKLeadsOnlineUploadedDate
		{
			get
			{
				return _bKLeadsOnlineUploadedDate;
			}
			set
			{
				if (value != _bKLeadsOnlineUploadedDate)
				{
					this._isModified = true;
					_bKLeadsOnlineUploadedDate = value;
					NotifyPropertyChanged("BKLeadsOnlineUploadedDate");
				}
			}
		}

		public string CourtName
		{
			get
			{
				return _courtName;
			}
			set
			{
				if (value != _courtName)
				{
					this._isModified = true;
					_courtName = value;
					NotifyPropertyChanged("CourtName");
				}
			}
		}

		#endregion 
        public BankruptcyCase(PacerImportData _row)
        {
            _iD = 0;
            _courtID = _row.CourtID;

            try
            {
                //this is what sharp used for the casenumber.  
                _cMECF_Internal = Int32.Parse(_row.CMECF);
            }
            catch 
            { }

            _caseNumber4Digit = _row.CaseNumber4Digit;
            _pacerImportTransactionID = _row.PacerImportTransactionID;
            _chapter = _row.Chapter;
            _filedOnly = !_row.DischargedCases;

            if (_row.FiledDate != null)
                _filedDate = (DateTime)_row.FiledDate;
            if (_row.DischargedDate != null)
                _dischargeDate = (DateTime)_row.DischargedDate;

            _trustee = _row.TrusteeLastName;
            _judge = _row.JudgeLastName;
            _county = _row.CountyFiled;
            _office = _row.OfficeName;
            _fee = _row.FilingFeePaymentStatus;
            _asset = _row.AssetsInCase;
            _firstName = _row.PartyFirstName;
            _middleName = _row.PartyMiddleName;
            _lastName = _row.PartyLastName;
            if (_firstName.ToLower().Contains("test") || _lastName.ToLower().Contains("test"))
            {
                throw new Exception("Test Data in production");    
            }
            
            //_suffix = 
            _addrLine1 = _row.PartyAddressLine1;
            _addrLine2 = _row.PartyAddressLine2;
            _addrLine3 = _row.PartyAddressLine3;
            _city = _row.PartyCity;
            _stateCode = _row.PartyState;
            //if this fails let the error percolate so the transaction can be rolled back
            _postalCode = Int32.Parse(_row.PartyZipCode.Substring(0, 5));
            //_postalLast4 = string.Empty;
            _country = _row.PartyCountry;
            _phone = _row.PartyPhone;
            try
            {
                if (_row.PartySSNo == "xxx-xx-2939")
                    Debug.WriteLine(_row.PartySSNo);
                _ssnLast4 = _row.PartySSNo.Substring(_row.PartySSNo.LastIndexOf('-') + 1, 4);
            }
            catch 
            { 
            }
            //todo: hospital in Care of
            //_careOf = _row.
            //_dontSend = false;
            //_returned = false;
            //_soldCount = 0;
            //_lastSoldDate = System.DateTime.MinValue;
        }
		
        public int CompareTo(Object objBankruptcyCase)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((BankruptcyCase)objBankruptcyCase).ID);
		}
        public string FullName
        {
            get
            {
                return _firstName + ' ' + _lastName;
            }
        }
        public string DischargeDateString
        {
            get
            {
                if (_dischargeDate != null)
                {
                    return _dischargeDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string FileDateString
        {
            get
            {
                if (_filedDate != null)
                {
                    return _filedDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string[] ToMailingStringArray(string callsource)
        {
            //string[] _columnHeadings = new string[] { "FirstName", "MiddleName", "FiledDate", "DischargedDate", "LastName", "Suffix", "Address1", "Address2", "City", "State", "PostalCode", "Callsource" };

            if (LastName == "Oliver")
            {
                Debug.WriteLine(LastName);
            }

            _firstName = Capitalize(_firstName).Replace(',', ' ').Trim();
            _lastName = Capitalize(_lastName).Replace(',', ' ').Trim();
            _city = Capitalize(_city).Replace(',', ' ').Trim();

            if (AddrLine1.Contains(","))
            {
                string[] address = AddrLine1.Split(',');
                string[] _returnString = null;

                if (address.Length == 2)
                {
                    address[0] = Capitalize(address[0]);
                    address[1] = Capitalize(address[1]);
                    if (AddrLine2 != null && AddrLine2.Length > 0)
                    {
                        AddrLine2 = Capitalize(AddrLine2);
                    }

                    _returnString = new string[] { CMECF_Internal.ToString(), CaseNumber4Digit, FileDateString, DischargeDateString, FirstName.Replace(",", "").Replace("\"", "'"), MiddleName.Replace(",", "").Replace("\"", "'"), LastName.Replace(",", "").Replace("\"", "'"), Suffix.Replace(",", ""), address[0].Replace("\"", "'"), address[1].Replace("\"", "'"), City, StateCode, PostalCodeString, callsource };
                }
                else if (address.Length == 3)
                {
                    address[0] = Capitalize(address[0]);
                    address[1] = Capitalize(address[1]);
                    address[2] = Capitalize(address[2]);
                    if (AddrLine2 != null && AddrLine2.Length > 0)
                    {
                        AddrLine2 = Capitalize(AddrLine2);
                    }
                    _returnString = new string[] { CMECF_Internal.ToString(), CaseNumber4Digit, FileDateString, DischargeDateString, FirstName.Replace(",", ""), MiddleName.Replace(",", ""), LastName.Replace(",", ""), Suffix.Replace(",", ""), address[0], address[1] + " " + address[2], City, StateCode, PostalCodeString, callsource };
                }
                else
                { 
                    throw new FormatException("The address for: " + _firstName + " " + _lastName + " is not valid: " + AddrLine1 + " wrong number of commas! Please lookup the record and correct the address");
                }

                return _returnString;
            }
            else
            {
                if (_addrLine1 != null && _addrLine1.Length > 0)
                {
                    _addrLine1 = Capitalize(_addrLine1);
                }
                if (_addrLine2 != null && _addrLine2.Length > 0)
                {
                    _addrLine2 = Capitalize(_addrLine2);
                }

                string[] _returnString = new string[] { CMECF_Internal.ToString(), CaseNumber4Digit.Replace(",", ""), FileDateString, DischargeDateString, FirstName.Replace(",", "").Replace("\"", "'"), MiddleName.Replace(",", "").Replace("\"", "'"), LastName.Replace(",", "").Replace("\"", "'"), Suffix.Replace(",", ""), AddrLine1.Replace("\"", "'"), _addrLine2.Replace(",", "").Replace("\"", "'"), City.Replace(",", "").Replace("\"", "'"), StateCode.Replace(",", ""), PostalCodeString.Replace(",", ""), callsource };
                return _returnString;
            }
        }
        public string PostalCodeString
        {
            get
            {
                return _postalCode.ToString("D5");
            }
            set
            {
                this._isModified = true;
                _postalCode = Int32.Parse(value.ToString());
            }
        }
        public string Capitalize (string toCapitalize)
        {
            string _returnString;


            if (toCapitalize != null && toCapitalize.Length > 0)
            {
                try
                {
                    if (toCapitalize.Trim().Contains(" "))
                    {
                        string[] _array = toCapitalize.Trim().Split(' ');

                        _returnString = string.Empty;
                        for (int i = 0; i < _array.Length; i++)
                        {
                            if (_array[i].Contains(" 29th"))
                            {
                                Debug.WriteLine(_array[i]);
                            }
                            if (_array[i].Length > 0 && _array[i] != " ")
                            {
                                _returnString += _array[i].Substring(0, 1).ToUpper() + _array[i].Substring(1, _array[i].Length - 1).ToLower() + " ";
                            }
                        }
                        _returnString = _returnString.Trim();
                        return _returnString;
                    }
                    else
                    {
                        _returnString = toCapitalize.Trim().Substring(0, 1).ToUpper() + toCapitalize.Substring(1, toCapitalize.Length - 1).ToLower();
                        return _returnString;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw ex;
                }
            }
            else
            {
                return string.Empty;
            }
        }
	}
}
