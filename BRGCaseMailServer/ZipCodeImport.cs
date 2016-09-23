using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the ZipCodeImport table.
	/// Author: Jonathan Shaw
	/// Date Created: 10/5/2011 5:40:48 PM
	/// </summary>
	public class ZipCodeImport
	{
		public ZipCodeImport()
		{
			_zipCode = string.Empty;
			_city = string.Empty;
			_state = string.Empty;
			_county = string.Empty;
			_areaCode = string.Empty;
			_cityType = string.Empty;
			_cityAliasAbbreviation = string.Empty;
			_cityAliasName = string.Empty;
			_latitude = string.Empty;
			_longitude = string.Empty;
			_timeZone = string.Empty;
			_elevation = string.Empty;
			_countyFIPS = string.Empty;
			_dayLightSaving = string.Empty;
			_preferredLastLineKey = string.Empty;
			_classificationCode = string.Empty;
			_multiCounty = string.Empty;
			_stateFIPS = string.Empty;
			_cityStateKey = string.Empty;
			_cityAliasCode = string.Empty;
			_primaryRecord = string.Empty;
			_cityMixedCase = string.Empty;
			_cityAliasMixedCase = string.Empty;
			_stateANSI = string.Empty;
			_countyANSI = string.Empty;
			_facilityCode = string.Empty;
			_cityDeliveryIndicator = string.Empty;
			_carrierRouteRateSortation = string.Empty;
			_financeNumber = string.Empty;
			_uniqueZIPName = string.Empty;
		}
        public ZipCodeImport(string _line)
        {
            string[] _fields = _line.Split(',');
            _zipCode = _fields[0];
            _city = _fields[1];
            _state = _fields[2];
            _county = _fields[3];
            _areaCode = _fields[4];
            _cityType = _fields[5];
            _cityAliasAbbreviation = _fields[6];
            _cityAliasName = _fields[7];
            _latitude = _fields[8];
            _longitude = _fields[9];
            _timeZone = _fields[10];
            _elevation = _fields[11];
            _countyFIPS = _fields[12];
            _dayLightSaving = _fields[13];
            _preferredLastLineKey = _fields[14];
            _classificationCode = _fields[15];
            _multiCounty = _fields[16];
            _stateFIPS = _fields[17];
            _cityStateKey = _fields[18];
            _cityAliasCode = _fields[19];
            _primaryRecord = _fields[20];
            _cityMixedCase = _fields[21];
            _cityAliasMixedCase = _fields[22];
            _stateANSI = _fields[23];
            _countyANSI = _fields[24];
            _facilityCode = _fields[25];
            _cityDeliveryIndicator = _fields[26];
            _carrierRouteRateSortation = _fields[27];
            _financeNumber = _fields[28];
            _uniqueZIPName = _fields[29];
        }
		public ZipCodeImport(string ZipCode, string City, string State, string County, string AreaCode, string CityType, string CityAliasAbbreviation, string CityAliasName, string Latitude, string Longitude, string TimeZone, string Elevation, string CountyFIPS, string DayLightSaving, string PreferredLastLineKey, string ClassificationCode, string MultiCounty, string StateFIPS, string CityStateKey, string CityAliasCode, string PrimaryRecord, string CityMixedCase, string CityAliasMixedCase, string StateANSI, string CountyANSI, string FacilityCode, string CityDeliveryIndicator, string CarrierRouteRateSortation, string FinanceNumber, string UniqueZIPName)		{
			_zipCode = ZipCode;
			_city = City;
			_state = State;
			_county = County;
			_areaCode = AreaCode;
			_cityType = CityType;
			_cityAliasAbbreviation = CityAliasAbbreviation;
			_cityAliasName = CityAliasName;
			_latitude = Latitude;
			_longitude = Longitude;
			_timeZone = TimeZone;
			_elevation = Elevation;
			_countyFIPS = CountyFIPS;
			_dayLightSaving = DayLightSaving;
			_preferredLastLineKey = PreferredLastLineKey;
			_classificationCode = ClassificationCode;
			_multiCounty = MultiCounty;
			_stateFIPS = StateFIPS;
			_cityStateKey = CityStateKey;
			_cityAliasCode = CityAliasCode;
			_primaryRecord = PrimaryRecord;
			_cityMixedCase = CityMixedCase;
			_cityAliasMixedCase = CityAliasMixedCase;
			_stateANSI = StateANSI;
			_countyANSI = CountyANSI;
			_facilityCode = FacilityCode;
			_cityDeliveryIndicator = CityDeliveryIndicator;
			_carrierRouteRateSortation = CarrierRouteRateSortation;
			_financeNumber = FinanceNumber;
			_uniqueZIPName = UniqueZIPName;
		}
		public ZipCodeImport Copy()
		{
			ZipCodeImport _zipCodeImport = new ZipCodeImport();
			_zipCodeImport.ZipCode = _zipCode;
			_zipCodeImport.City = _city;
			_zipCodeImport.State = _state;
			_zipCodeImport.County = _county;
			_zipCodeImport.AreaCode = _areaCode;
			_zipCodeImport.CityType = _cityType;
			_zipCodeImport.CityAliasAbbreviation = _cityAliasAbbreviation;
			_zipCodeImport.CityAliasName = _cityAliasName;
			_zipCodeImport.Latitude = _latitude;
			_zipCodeImport.Longitude = _longitude;
			_zipCodeImport.TimeZone = _timeZone;
			_zipCodeImport.Elevation = _elevation;
			_zipCodeImport.CountyFIPS = _countyFIPS;
			_zipCodeImport.DayLightSaving = _dayLightSaving;
			_zipCodeImport.PreferredLastLineKey = _preferredLastLineKey;
			_zipCodeImport.ClassificationCode = _classificationCode;
			_zipCodeImport.MultiCounty = _multiCounty;
			_zipCodeImport.StateFIPS = _stateFIPS;
			_zipCodeImport.CityStateKey = _cityStateKey;
			_zipCodeImport.CityAliasCode = _cityAliasCode;
			_zipCodeImport.PrimaryRecord = _primaryRecord;
			_zipCodeImport.CityMixedCase = _cityMixedCase;
			_zipCodeImport.CityAliasMixedCase = _cityAliasMixedCase;
			_zipCodeImport.StateANSI = _stateANSI;
			_zipCodeImport.CountyANSI = _countyANSI;
			_zipCodeImport.FacilityCode = _facilityCode;
			_zipCodeImport.CityDeliveryIndicator = _cityDeliveryIndicator;
			_zipCodeImport.CarrierRouteRateSortation = _carrierRouteRateSortation;
			_zipCodeImport.FinanceNumber = _financeNumber;
			_zipCodeImport.UniqueZIPName = _uniqueZIPName;
			return _zipCodeImport;
		}
		#region "Private Instance Variables"
		private string _zipCode;
		private string _city;
		private string _state;
		private string _county;
		private string _areaCode;
		private string _cityType;
		private string _cityAliasAbbreviation;
		private string _cityAliasName;
		private string _latitude;
		private string _longitude;
		private string _timeZone;
		private string _elevation;
		private string _countyFIPS;
		private string _dayLightSaving;
		private string _preferredLastLineKey;
		private string _classificationCode;
		private string _multiCounty;
		private string _stateFIPS;
		private string _cityStateKey;
		private string _cityAliasCode;
		private string _primaryRecord;
		private string _cityMixedCase;
		private string _cityAliasMixedCase;
		private string _stateANSI;
		private string _countyANSI;
		private string _facilityCode;
		private string _cityDeliveryIndicator;
		private string _carrierRouteRateSortation;
		private string _financeNumber;
		private string _uniqueZIPName;
		#endregion 
		#region "Public Properties"
public string ZipCode
{
	get
	{
		return _zipCode;
	}
	set
	{
		_zipCode = value;
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

public string County
{
	get
	{
		return _county;
	}
	set
	{
		_county = value;
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
		_areaCode = value;
	}
}

public string CityType
{
	get
	{
		return _cityType;
	}
	set
	{
		_cityType = value;
	}
}

public string CityAliasAbbreviation
{
	get
	{
		return _cityAliasAbbreviation;
	}
	set
	{
		_cityAliasAbbreviation = value;
	}
}

public string CityAliasName
{
	get
	{
		return _cityAliasName;
	}
	set
	{
		_cityAliasName = value;
	}
}

public string Latitude
{
	get
	{
		return _latitude;
	}
	set
	{
		_latitude = value;
	}
}

public string Longitude
{
	get
	{
		return _longitude;
	}
	set
	{
		_longitude = value;
	}
}

public string TimeZone
{
	get
	{
		return _timeZone;
	}
	set
	{
		_timeZone = value;
	}
}

public string Elevation
{
	get
	{
		return _elevation;
	}
	set
	{
		_elevation = value;
	}
}

public string CountyFIPS
{
	get
	{
		return _countyFIPS;
	}
	set
	{
		_countyFIPS = value;
	}
}

public string DayLightSaving
{
	get
	{
		return _dayLightSaving;
	}
	set
	{
		_dayLightSaving = value;
	}
}

public string PreferredLastLineKey
{
	get
	{
		return _preferredLastLineKey;
	}
	set
	{
		_preferredLastLineKey = value;
	}
}

public string ClassificationCode
{
	get
	{
		return _classificationCode;
	}
	set
	{
		_classificationCode = value;
	}
}

public string MultiCounty
{
	get
	{
		return _multiCounty;
	}
	set
	{
		_multiCounty = value;
	}
}

public string StateFIPS
{
	get
	{
		return _stateFIPS;
	}
	set
	{
		_stateFIPS = value;
	}
}

public string CityStateKey
{
	get
	{
		return _cityStateKey;
	}
	set
	{
		_cityStateKey = value;
	}
}

public string CityAliasCode
{
	get
	{
		return _cityAliasCode;
	}
	set
	{
		_cityAliasCode = value;
	}
}

public string PrimaryRecord
{
	get
	{
		return _primaryRecord;
	}
	set
	{
		_primaryRecord = value;
	}
}

public string CityMixedCase
{
	get
	{
		return _cityMixedCase;
	}
	set
	{
		_cityMixedCase = value;
	}
}

public string CityAliasMixedCase
{
	get
	{
		return _cityAliasMixedCase;
	}
	set
	{
		_cityAliasMixedCase = value;
	}
}

public string StateANSI
{
	get
	{
		return _stateANSI;
	}
	set
	{
		_stateANSI = value;
	}
}

public string CountyANSI
{
	get
	{
		return _countyANSI;
	}
	set
	{
		_countyANSI = value;
	}
}

public string FacilityCode
{
	get
	{
		return _facilityCode;
	}
	set
	{
		_facilityCode = value;
	}
}

public string CityDeliveryIndicator
{
	get
	{
		return _cityDeliveryIndicator;
	}
	set
	{
		_cityDeliveryIndicator = value;
	}
}

public string CarrierRouteRateSortation
{
	get
	{
		return _carrierRouteRateSortation;
	}
	set
	{
		_carrierRouteRateSortation = value;
	}
}

public string FinanceNumber
{
	get
	{
		return _financeNumber;
	}
	set
	{
		_financeNumber = value;
	}
}

public string UniqueZIPName
{
	get
	{
		return _uniqueZIPName;
	}
	set
	{
		_uniqueZIPName = value;
	}
}

		#endregion 
	}
}
