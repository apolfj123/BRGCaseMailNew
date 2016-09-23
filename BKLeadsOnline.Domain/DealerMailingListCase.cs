using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the DealerMailingListCase table.
	/// Author: Jonathan Shaw
	/// Date Created: 2/14/2014 10:20:51 AM
	/// </summary>
	public class DealerMailingListCase : IComparable, INotifyPropertyChanged
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
		public DealerMailingListCase()
		{
			_dealerMailingListID = 0;
			_bankruptcyCaseID = 0;
			_dealerID = 0;
			_sold = false;
			_soldDate = System.DateTime.MinValue;
			_grossProfit = 0;
			_doNotSend = false;
			_firstName = string.Empty;
			_lastName = string.Empty;
			_addrLine1 = string.Empty;
			_addrLine2 = string.Empty;
			_city = string.Empty;
			_stateCode = string.Empty;
			_postalCode = 0;
			_dischargeDate = System.DateTime.MinValue;
		}
		public DealerMailingListCase(int DealerMailingListID, int BankruptcyCaseID, int DealerID, bool Sold, DateTime SoldDate, int GrossProfit, bool DoNotSend, string FirstName, string LastName, string AddrLine1, string AddrLine2, string City, string StateCode, int PostalCode, DateTime DischargeDate)		{
			_dealerMailingListID = DealerMailingListID;
			_bankruptcyCaseID = BankruptcyCaseID;
			_dealerID = DealerID;
			_sold = Sold;
			_soldDate = SoldDate;
			_grossProfit = GrossProfit;
			_doNotSend = DoNotSend;
			_firstName = FirstName;
			_lastName = LastName;
			_addrLine1 = AddrLine1;
			_addrLine2 = AddrLine2;
			_city = City;
			_stateCode = StateCode;
			_postalCode = PostalCode;
			_dischargeDate = DischargeDate;
		}
		public DealerMailingListCase Copy()
		{
			DealerMailingListCase _dealerMailingListCase = new DealerMailingListCase();
			_dealerMailingListCase.DealerMailingListID = _dealerMailingListID;
			_dealerMailingListCase.BankruptcyCaseID = _bankruptcyCaseID;
			_dealerMailingListCase.DealerID = _dealerID;
			_dealerMailingListCase.Sold = _sold;
			_dealerMailingListCase.SoldDate = _soldDate;
			_dealerMailingListCase.GrossProfit = _grossProfit;
			_dealerMailingListCase.DoNotSend = _doNotSend;
			_dealerMailingListCase.FirstName = _firstName;
			_dealerMailingListCase.LastName = _lastName;
			_dealerMailingListCase.AddrLine1 = _addrLine1;
			_dealerMailingListCase.AddrLine2 = _addrLine2;
			_dealerMailingListCase.City = _city;
			_dealerMailingListCase.StateCode = _stateCode;
			_dealerMailingListCase.PostalCode = _postalCode;
			_dealerMailingListCase.DischargeDate = _dischargeDate;
			return _dealerMailingListCase;
		}
		#region "Private Instance Variables"
		private int _dealerMailingListID;
		private int _bankruptcyCaseID;
		private int _dealerID;
		private bool _sold;
		private DateTime _soldDate;
		private decimal _grossProfit;
		private bool _doNotSend;
		private string _firstName;
		private string _lastName;
		private string _addrLine1;
		private string _addrLine2;
		private string _city;
		private string _stateCode;
		private int _postalCode;
		private DateTime _dischargeDate;
		#endregion 
		#region "Public Properties"
		public int DealerMailingListID
		{
			get
			{
				return _dealerMailingListID;
			}
			set
			{
				if (value != _dealerMailingListID)
				{
					this._isModified = true;
					_dealerMailingListID = value;
					NotifyPropertyChanged("DealerMailingListID");
				}
			}
		}

		public int BankruptcyCaseID
		{
			get
			{
				return _bankruptcyCaseID;
			}
			set
			{
				if (value != _bankruptcyCaseID)
				{
					this._isModified = true;
					_bankruptcyCaseID = value;
					NotifyPropertyChanged("BankruptcyCaseID");
				}
			}
		}

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
					NotifyPropertyChanged("DealerID");
				}
			}
		}

		public bool Sold
		{
			get
			{
				return _sold;
			}
			set
			{
				if (value != _sold)
				{
					this._isModified = true;
					_sold = value;
					NotifyPropertyChanged("Sold");
				}
			}
		}

		public DateTime SoldDate
		{
			get
			{
				return _soldDate;
			}
			set
			{
				if (value != _soldDate)
				{
					this._isModified = true;
					_soldDate = value;
					NotifyPropertyChanged("SoldDate");
				}
			}
		}

		public decimal GrossProfit
		{
			get
			{
				return _grossProfit;
			}
			set
			{
				if (value != _grossProfit)
				{
					this._isModified = true;
					_grossProfit = value;
					NotifyPropertyChanged("GrossProfit");
				}
			}
		}

		public bool DoNotSend
		{
			get
			{
				return _doNotSend;
			}
			set
			{
				if (value != _doNotSend)
				{
					this._isModified = true;
					_doNotSend = value;
					NotifyPropertyChanged("DoNotSend");
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

		#endregion 


        public string SoldDateString
        {
            get
            {
                if (SoldDate > DateTime.MinValue)
                {
                    return SoldDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string GrossProfitString {
            get
            {
                if (GrossProfit > 0)
                {
                    return GrossProfit.ToString("C");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string DischargeDateString
        {
            get 
            {
                if (DischargeDate > DateTime.MinValue)
                {
                    return DischargeDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string LeadCityStateZip
        {
            get
            {
                return Capitalize(_city) + ", " + _stateCode + " " + _postalCode;
            }
        }

        public string LeadFullName
        {
            get
            {
                return Capitalize(_firstName) + " "  + Capitalize(_lastName);
            }
        }

        public string Capitalize(string toCapitalize)
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

        public int CompareTo(Object objDealerMailingListCase)
        {
            //sort by ID ascending - this is used by the default sort mechanisc
            return this.BankruptcyCaseID.CompareTo(((DealerMailingListCase)objDealerMailingListCase).BankruptcyCaseID);
        }
	}
}
