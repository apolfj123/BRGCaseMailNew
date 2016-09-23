using System;

using System.Collections;

using System.ComponentModel;
using System.Diagnostics;


namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the BankruptcyCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/16/2013 7:39:22 PM
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
			_uploadedDate = System.DateTime.MinValue;
			_courtName = string.Empty;
			_zipCodeString = string.Empty;
		}
		public BankruptcyCase(int ID, int CourtID, int CMECF_Internal, string CaseNumber4Digit, int PacerImportTransactionID, int Chapter, bool FiledOnly, DateTime FiledDate, DateTime DischargeDate, string Trustee, string Judge, string County, string Office, string Fee, string Asset, string FirstName, string MiddleName, string LastName, string Suffix, string AddrLine1, string AddrLine2, string AddrLine3, string City, string StateCode, int PostalCode, string PostalLast4, double Latitude, double Longitude, string Country, string Phone, string ssnLast4, string CareOf, DateTime UploadedDate, string CourtName, string ZipCodeString)		{
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
			_uploadedDate = UploadedDate;
			_courtName = CourtName;
			_zipCodeString = ZipCodeString;
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
			_bankruptcyCase.UploadedDate = _uploadedDate;
			_bankruptcyCase.CourtName = _courtName;
			_bankruptcyCase.ZipCodeString = _zipCodeString;
			return _bankruptcyCase;
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
		private DateTime _uploadedDate;
		private string _courtName;
		private string _zipCodeString;
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

		public DateTime UploadedDate
		{
			get
			{
				return _uploadedDate;
			}
			set
			{
				if (value != _uploadedDate)
				{
					this._isModified = true;
					_uploadedDate = value;
					NotifyPropertyChanged("UploadedDate");
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

		#endregion 
		public int CompareTo(Object objBankruptcyCase)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((BankruptcyCase)objBankruptcyCase).ID);
		}

        public string LeadAddress
        {
            get
            {
                 if (_addrLine2 != null && _addrLine2.Length > 0)
                 {
                    return _addrLine1 + " " + _addrLine2;
                 }
                 else
                 {
                    return _addrLine1;
                 }
                     
            }
        }

        public string LeadCityStateZip
        {
            get
            {
                 return Capitalize(_city) + ", " + _stateCode + " " + _zipCodeString;
            }
        }

        public string LeadFullName
        {
            get
            {
                if (_middleName != null && _middleName.Length > 0)
                {
                    return Capitalize(_firstName) + " " + Capitalize(_middleName) + " " + Capitalize(_lastName);
                }
                else
                    return Capitalize(_firstName) + " " + Capitalize(_lastName);
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
