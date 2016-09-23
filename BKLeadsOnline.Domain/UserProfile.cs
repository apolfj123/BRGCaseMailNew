using System;

using System.Collections;

using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the UserProfile table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/23/2013 1:18:48 PM
	/// </summary>
	public class UserProfile : IComparable, INotifyPropertyChanged
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
		public UserProfile()
		{
			_iD = 0;
			_dealerID = 0;
			_isAdmin = false;
			_isPrimaryContact = false;
			_positionID = 0;
			_applicationLogin = string.Empty;
			_password = string.Empty;
			_firstName = string.Empty;
			_lastName = string.Empty;
			_phone = string.Empty;
			_cell = string.Empty;
			_email = string.Empty;
			_sendToEmail = false;
			_textAddress = string.Empty;
			_sendToText = false;
			_userCookie = string.Empty;
			_limitToOneComputer = false;
			_lastLoginDateTime = System.DateTime.MinValue;
			_active = false;
			_securityQuestion1ID = 0;
			_securityQuestion2ID = 0;
			_securityAnswer1 = string.Empty;
			_securityAnswer2 = string.Empty;
			_passwordResetGUID = string.Empty;
			_passwordResetTimestamp = System.DateTime.MinValue;
			_lastPasswordChange = System.DateTime.MinValue;
			_dealerName = string.Empty;
			_positionName = string.Empty;
			_securityQuestion1Name = string.Empty;
			_securityQuestion2Name = string.Empty;
		}
		public UserProfile(int ID, int DealerID, bool IsAdmin, bool IsPrimaryContact, int PositionID, string ApplicationLogin, string Password, string FirstName, string LastName, string Phone, string Cell, string Email, bool SendToEmail, string TextAddress, bool SendToText, string UserCookie, bool LimitToOneComputer, DateTime LastLoginDateTime, bool Active, int SecurityQuestion1ID, int SecurityQuestion2ID, string SecurityAnswer1, string SecurityAnswer2, string PasswordResetGUID, DateTime PasswordResetTimestamp, DateTime LastPasswordChange, string DealerName, string PositionName, string SecurityQuestion1Name, string SecurityQuestion2Name)		{
			_iD = ID;
			_dealerID = DealerID;
			_isAdmin = IsAdmin;
			_isPrimaryContact = IsPrimaryContact;
			_positionID = PositionID;
			_applicationLogin = ApplicationLogin;
			_password = Password;
			_firstName = FirstName;
			_lastName = LastName;
			_phone = Phone;
			_cell = Cell;
			_email = Email;
			_sendToEmail = SendToEmail;
			_textAddress = TextAddress;
			_sendToText = SendToText;
			_userCookie = UserCookie;
			_limitToOneComputer = LimitToOneComputer;
			_lastLoginDateTime = LastLoginDateTime;
			_active = Active;
			_securityQuestion1ID = SecurityQuestion1ID;
			_securityQuestion2ID = SecurityQuestion2ID;
			_securityAnswer1 = SecurityAnswer1;
			_securityAnswer2 = SecurityAnswer2;
			_passwordResetGUID = PasswordResetGUID;
			_passwordResetTimestamp = PasswordResetTimestamp;
			_lastPasswordChange = LastPasswordChange;
			_dealerName = DealerName;
			_positionName = PositionName;
			_securityQuestion1Name = SecurityQuestion1Name;
			_securityQuestion2Name = SecurityQuestion2Name;
		}
		public UserProfile Copy()
		{
			UserProfile _userProfile = new UserProfile();
			_userProfile.ID = _iD;
			_userProfile.DealerID = _dealerID;
			_userProfile.IsAdmin = _isAdmin;
			_userProfile.IsPrimaryContact = _isPrimaryContact;
			_userProfile.PositionID = _positionID;
			_userProfile.ApplicationLogin = _applicationLogin;
			_userProfile.Password = _password;
			_userProfile.FirstName = _firstName;
			_userProfile.LastName = _lastName;
			_userProfile.Phone = _phone;
			_userProfile.Cell = _cell;
			_userProfile.Email = _email;
			_userProfile.SendToEmail = _sendToEmail;
			_userProfile.TextAddress = _textAddress;
			_userProfile.SendToText = _sendToText;
			_userProfile.UserCookie = _userCookie;
			_userProfile.LimitToOneComputer = _limitToOneComputer;
			_userProfile.LastLoginDateTime = _lastLoginDateTime;
			_userProfile.Active = _active;
			_userProfile.SecurityQuestion1ID = _securityQuestion1ID;
			_userProfile.SecurityQuestion2ID = _securityQuestion2ID;
			_userProfile.SecurityAnswer1 = _securityAnswer1;
			_userProfile.SecurityAnswer2 = _securityAnswer2;
			_userProfile.PasswordResetGUID = _passwordResetGUID;
			_userProfile.PasswordResetTimestamp = _passwordResetTimestamp;
			_userProfile.LastPasswordChange = _lastPasswordChange;
			_userProfile.DealerName = _dealerName;
			_userProfile.PositionName = _positionName;
			_userProfile.SecurityQuestion1Name = _securityQuestion1Name;
			_userProfile.SecurityQuestion2Name = _securityQuestion2Name;
			return _userProfile;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _dealerID;
		private bool _isAdmin;
		private bool _isPrimaryContact;
		private int _positionID;
		private string _applicationLogin;
		private string _password;
		private string _firstName;
		private string _lastName;
		private string _phone;
		private string _cell;
		private string _email;
		private bool _sendToEmail;
		private string _textAddress;
		private bool _sendToText;
		private string _userCookie;
		private bool _limitToOneComputer;
		private DateTime _lastLoginDateTime;
		private bool _active;
		private int _securityQuestion1ID;
		private int _securityQuestion2ID;
		private string _securityAnswer1;
		private string _securityAnswer2;
		private string _passwordResetGUID;
		private DateTime _passwordResetTimestamp;
		private DateTime _lastPasswordChange;
		private string _dealerName;
		private string _positionName;
		private string _securityQuestion1Name;
		private string _securityQuestion2Name;
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

		public bool IsAdmin
		{
			get
			{
				return _isAdmin;
			}
			set
			{
				if (value != _isAdmin)
				{
					this._isModified = true;
					_isAdmin = value;
					NotifyPropertyChanged("IsAdmin");
				}
			}
		}

		public bool IsPrimaryContact
		{
			get
			{
				return _isPrimaryContact;
			}
			set
			{
				if (value != _isPrimaryContact)
				{
					this._isModified = true;
					_isPrimaryContact = value;
					NotifyPropertyChanged("IsPrimaryContact");
				}
			}
		}

		public int PositionID
		{
			get
			{
				return _positionID;
			}
			set
			{
				if (value != _positionID)
				{
					this._isModified = true;
					_positionID = value;
					NotifyPropertyChanged("PositionID");
				}
			}
		}

		public string ApplicationLogin
		{
			get
			{
				return _applicationLogin;
			}
			set
			{
				if (value != _applicationLogin)
				{
					this._isModified = true;
					_applicationLogin = value;
					NotifyPropertyChanged("ApplicationLogin");
				}
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				if (value != _password)
				{
					this._isModified = true;
					_password = value;
					NotifyPropertyChanged("Password");
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

		public string Cell
		{
			get
			{
				return _cell;
			}
			set
			{
				if (value != _cell)
				{
					this._isModified = true;
					_cell = value;
					NotifyPropertyChanged("Cell");
				}
			}
		}

		public string Email
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
					NotifyPropertyChanged("Email");
				}
			}
		}

		public bool SendToEmail
		{
			get
			{
				return _sendToEmail;
			}
			set
			{
				if (value != _sendToEmail)
				{
					this._isModified = true;
					_sendToEmail = value;
					NotifyPropertyChanged("SendToEmail");
				}
			}
		}

		public string TextAddress
		{
			get
			{
				return _textAddress;
			}
			set
			{
				if (value != _textAddress)
				{
					this._isModified = true;
					_textAddress = value;
					NotifyPropertyChanged("TextAddress");
				}
			}
		}

		public bool SendToText
		{
			get
			{
				return _sendToText;
			}
			set
			{
				if (value != _sendToText)
				{
					this._isModified = true;
					_sendToText = value;
					NotifyPropertyChanged("SendToText");
				}
			}
		}

		public string UserCookie
		{
			get
			{
				return _userCookie;
			}
			set
			{
				if (value != _userCookie)
				{
					this._isModified = true;
					_userCookie = value;
					NotifyPropertyChanged("UserCookie");
				}
			}
		}

		public bool LimitToOneComputer
		{
			get
			{
				return _limitToOneComputer;
			}
			set
			{
				if (value != _limitToOneComputer)
				{
					this._isModified = true;
					_limitToOneComputer = value;
					NotifyPropertyChanged("LimitToOneComputer");
				}
			}
		}

		public DateTime LastLoginDateTime
		{
			get
			{
				return _lastLoginDateTime;
			}
			set
			{
				if (value != _lastLoginDateTime)
				{
					this._isModified = true;
					_lastLoginDateTime = value;
					NotifyPropertyChanged("LastLoginDateTime");
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

		public int SecurityQuestion1ID
		{
			get
			{
				return _securityQuestion1ID;
			}
			set
			{
				if (value != _securityQuestion1ID)
				{
					this._isModified = true;
					_securityQuestion1ID = value;
					NotifyPropertyChanged("SecurityQuestion1ID");
				}
			}
		}

		public int SecurityQuestion2ID
		{
			get
			{
				return _securityQuestion2ID;
			}
			set
			{
				if (value != _securityQuestion2ID)
				{
					this._isModified = true;
					_securityQuestion2ID = value;
					NotifyPropertyChanged("SecurityQuestion2ID");
				}
			}
		}

		public string SecurityAnswer1
		{
			get
			{
				return _securityAnswer1;
			}
			set
			{
				if (value != _securityAnswer1)
				{
					this._isModified = true;
					_securityAnswer1 = value;
					NotifyPropertyChanged("SecurityAnswer1");
				}
			}
		}

		public string SecurityAnswer2
		{
			get
			{
				return _securityAnswer2;
			}
			set
			{
				if (value != _securityAnswer2)
				{
					this._isModified = true;
					_securityAnswer2 = value;
					NotifyPropertyChanged("SecurityAnswer2");
				}
			}
		}

		public string PasswordResetGUID
		{
			get
			{
				return _passwordResetGUID;
			}
			set
			{
				if (value != _passwordResetGUID)
				{
					this._isModified = true;
					_passwordResetGUID = value;
					NotifyPropertyChanged("PasswordResetGUID");
				}
			}
		}

		public DateTime PasswordResetTimestamp
		{
			get
			{
				return _passwordResetTimestamp;
			}
			set
			{
				if (value != _passwordResetTimestamp)
				{
					this._isModified = true;
					_passwordResetTimestamp = value;
					NotifyPropertyChanged("PasswordResetTimestamp");
				}
			}
		}

		public DateTime LastPasswordChange
		{
			get
			{
				return _lastPasswordChange;
			}
			set
			{
				if (value != _lastPasswordChange)
				{
					this._isModified = true;
					_lastPasswordChange = value;
					NotifyPropertyChanged("LastPasswordChange");
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

		public string PositionName
		{
			get
			{
				return _positionName;
			}
			set
			{
				if (value != _positionName)
				{
					this._isModified = true;
					_positionName = value;
					NotifyPropertyChanged("PositionName");
				}
			}
		}

		public string SecurityQuestion1Name
		{
			get
			{
				return _securityQuestion1Name;
			}
			set
			{
				if (value != _securityQuestion1Name)
				{
					this._isModified = true;
					_securityQuestion1Name = value;
					NotifyPropertyChanged("SecurityQuestion1Name");
				}
			}
		}

		public string SecurityQuestion2Name
		{
			get
			{
				return _securityQuestion2Name;
			}
			set
			{
				if (value != _securityQuestion2Name)
				{
					this._isModified = true;
					_securityQuestion2Name = value;
					NotifyPropertyChanged("SecurityQuestion2Name");
				}
			}
		}

		#endregion 
		public int CompareTo(Object objUserProfile)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((UserProfile)objUserProfile).ID);
		}

        public Dealer Dealer
        {
            get
            {
                if (_dealer == null && _dealerID > 0)
                {
                    return BKLeadsOnline.Domain.DealerService.GetByID(_dealerID);
                }
                else
                {
                    return _dealer;
                }
            }
        }

        private Dealer _dealer;
        public string Name
        {
            get
            {
                return _firstName + " " + _lastName;
            }
        }
        public string NameLast2First
        {
            get
            {
                return _lastName + ", " + _firstName;
            }
        }

	}
}
