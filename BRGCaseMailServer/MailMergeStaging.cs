using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the MailMergeStaging table.
	/// Author: Jonathan Shaw
	/// Date Created: 2/15/2012 11:23:00 AM
	/// </summary>
	public class MailMergeStaging
	{
		public MailMergeStaging()
		{
			_caseNumber = string.Empty;
			_caseNumber4Digit = string.Empty;
			_filedDate = string.Empty;
			_dischargedDate = string.Empty;
			_firstName = string.Empty;
			_middleName = string.Empty;
			_lastName = string.Empty;
			_suffix = string.Empty;
			_address1 = string.Empty;
			_address2 = string.Empty;
			_city = string.Empty;
			_state = string.Empty;
			_postalCode = string.Empty;
			_callsource = string.Empty;
		}
		public MailMergeStaging(string CaseNumber, string CaseNumber4Digit, string FiledDate, string DischargedDate, string FirstName, string MiddleName, string LastName, string Suffix, string Address1, string Address2, string City, string State, string PostalCode, string Callsource)		{
			_caseNumber = CaseNumber;
			_caseNumber4Digit = CaseNumber4Digit;
			_filedDate = FiledDate;
			_dischargedDate = DischargedDate;
			_firstName = FirstName;
			_middleName = MiddleName;
			_lastName = LastName;
			_suffix = Suffix;
			_address1 = Address1;
			_address2 = Address2;
			_city = City;
			_state = State;
			_postalCode = PostalCode;
			_callsource = Callsource;
		}
        public MailMergeStaging(BankruptcyCase _case, string callSource)
        {
            _caseNumber = _case.CMECF_Internal.ToString();
            _caseNumber4Digit = _case.CaseNumber4Digit;
            _filedDate = _case.FileDateString;
            _dischargedDate = _case.DischargeDateString;
            _firstName = _case.FirstName;
            _middleName = _case.MiddleName;
            _lastName = _case.LastName;
            _suffix = _case.Suffix;
            _address1 = _case.AddrLine1;
            _address2 = _case.AddrLine2;
            _city = _case.City;
            _state = _case.StateCode;
            _postalCode = _case.PostalCodeString;
            _callsource = callSource;
        }
        public MailMergeStaging Copy()
		{
			MailMergeStaging _mailMergeStaging = new MailMergeStaging();
			_mailMergeStaging.CaseNumber = _caseNumber;
			_mailMergeStaging.CaseNumber4Digit = _caseNumber4Digit;
			_mailMergeStaging.FiledDate = _filedDate;
			_mailMergeStaging.DischargedDate = _dischargedDate;
			_mailMergeStaging.FirstName = _firstName;
			_mailMergeStaging.MiddleName = _middleName;
			_mailMergeStaging.LastName = _lastName;
			_mailMergeStaging.Suffix = _suffix;
			_mailMergeStaging.Address1 = _address1;
			_mailMergeStaging.Address2 = _address2;
			_mailMergeStaging.City = _city;
			_mailMergeStaging.State = _state;
			_mailMergeStaging.PostalCode = _postalCode;
			_mailMergeStaging.Callsource = _callsource;
			return _mailMergeStaging;
		}
		#region "Private Instance Variables"
		private string _caseNumber;
		private string _caseNumber4Digit;
		private string _filedDate;
		private string _dischargedDate;
		private string _firstName;
		private string _middleName;
		private string _lastName;
		private string _suffix;
		private string _address1;
		private string _address2;
		private string _city;
		private string _state;
		private string _postalCode;
		private string _callsource;
		#endregion 
		#region "Public Properties"
public string CaseNumber
{
	get
	{
		return _caseNumber;
	}
	set
	{
		_caseNumber = value;
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
		_caseNumber4Digit = value;
	}
}

public string FiledDate
{
	get
	{
		return _filedDate;
	}
	set
	{
		_filedDate = value;
	}
}

public string DischargedDate
{
	get
	{
		return _dischargedDate;
	}
	set
	{
		_dischargedDate = value;
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
		_firstName = value;
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
		_middleName = value;
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
		_lastName = value;
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
		_suffix = value;
	}
}

public string Address1
{
	get
	{
		return _address1;
	}
	set
	{
		_address1 = value;
	}
}

public string Address2
{
	get
	{
		return _address2;
	}
	set
	{
		_address2 = value;
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
		_city = value;
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
		_state = value;
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
		_postalCode = value;
	}
}

public string Callsource
{
	get
	{
		return _callsource;
	}
	set
	{
		_callsource = value;
	}
}

		#endregion 
	}
}
