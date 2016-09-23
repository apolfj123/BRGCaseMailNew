using System;

using System.Collections;
using System.Diagnostics;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the PacerTempRawImportData table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/17/2011 9:22:27 AM
	/// </summary>
	public class PacerTempRawImportData : IComparable
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
		public PacerTempRawImportData()
		{
			_caseNumber2Digit = string.Empty;
			_caseNumber4Digit = string.Empty;
			_cMECF = string.Empty;
			_chapter = 0;
			_previousChapter = string.Empty;
			_conversionDate = System.DateTime.MinValue;
			_dischargedDate = System.DateTime.MinValue;
			_dismissedDate = System.DateTime.MinValue;
			_filedDate = System.DateTime.MinValue;
			_enteredDate = System.DateTime.MinValue;
			_closedDate = System.DateTime.MinValue;
			_disposition = string.Empty;
			_caseType = string.Empty;
			_trusteeLastName = string.Empty;
			_judgeLastName = string.Empty;
			_countyFiled = string.Empty;
			_officeName = string.Empty;
			_filingFeePaymentStatus = string.Empty;
			_assetsInCase = string.Empty;
			_caseDisposition = string.Empty;
			_jointDebtorDischargeDate = System.DateTime.MinValue;
			_jointDebtorDismissalDate = System.DateTime.MinValue;
			_jointDebtorDispositionCode = string.Empty;
			_jointDebtorDisposition = string.Empty;
			_divisionalOfficeFiled = string.Empty;
			_adversaryProceedingSeqNo = string.Empty;
			_adversaryProceedingYear = string.Empty;
			_adversaryProceedingDisposition = string.Empty;
			_partyFirstName = string.Empty;
			_partyMiddleName = string.Empty;
			_partyLastName = string.Empty;
			_partyGeneration = string.Empty;
			_partyOffice = string.Empty;
			_partyAddressLine1 = string.Empty;
			_partyAddressLine2 = string.Empty;
			_partyAddressLine3 = string.Empty;
			_partyCity = string.Empty;
			_partyState = string.Empty;
			_partyZipCode = string.Empty;
			_partyCountry = string.Empty;
			_partyPhone = string.Empty;
			_partySSNo = string.Empty;
			_partyTaxID = string.Empty;
			_partyCaseSeqNo = 0;
			_partyCaseRole = string.Empty;
			_partyTerminatedDate = System.DateTime.MinValue;
			_caseTitle = string.Empty;
			_attorneyFirstName = string.Empty;
			_attorneyMiddleName = string.Empty;
			_attorneyLastName = string.Empty;
			_attorneyGeneration = string.Empty;
			_attorneyOffice = string.Empty;
			_attorneyOfficeLine1 = string.Empty;
			_attorneyOfficeLine2 = string.Empty;
			_attorneyOfficeLine3 = string.Empty;
			_attorneyCity = string.Empty;
			_attorneyState = string.Empty;
			_attorneyZipCode = string.Empty;
			_attorneyCountry = string.Empty;
			_attorneyPhone = string.Empty;
			_nullcolumn = string.Empty;
		}
        public PacerTempRawImportData(string _line, PacerFileFormat _format)
        {
            string[] _fields = _line.Split('|');

            if (!(_fields.Length == _format.NumberColumns + 1 || _fields.Length == _format.NumberColumns + 2 || _fields.Length == _format.NumberColumns + 3))
            { 
                throw new FormatException("Error: File Does Not Match Import Specification! Expected " + (_format.NumberColumns +1).ToString() +  " fields, found  " + _fields.Length + " fields.");
            }
            
            try
            {
                switch (_format.NumberColumns)
                {

                    //4.1.0 & 5.0 (69)
                    case 60:
                    case 68:
                    case 69:
                        _caseNumber2Digit = _fields[0];
                        _caseNumber4Digit = _fields[1];
                        
                        Debug.WriteLine(_fields[1]);
                        
                        _cMECF = _fields[2];
                        if (_fields[3].Length > 0)
                        {
                            try
                            {
                                int indexOfChapter = _fields[3].IndexOf(".");
                                if(indexOfChapter == -1)
                                {
                                    _chapter = Int32.Parse(_fields[3]);
                                }
                                else
                                {
                                    _chapter = Int32.Parse(_fields[3].Substring(0, indexOfChapter));
                                }

                                
                            }
                            catch
                            { }
                        }
                        _previousChapter = _fields[4];
                        if (_fields[5].Length > 0)
                        {
                            try
                            {
                                _conversionDate = System.DateTime.Parse(_fields[5]);
                            }
                            catch { }
                        }
                        if (_fields[6].Length > 0)
                        {
                            try
                            {
                                _dischargedDate = System.DateTime.Parse(_fields[6]);
                            }
                            catch { }
                        }
                        if (_fields[7].Length > 0)
                        {
                            try
                            {
                                _dismissedDate = System.DateTime.Parse(_fields[7]);
                            }
                            catch { }
                        }
                        if (_fields[8].Length > 0)
                        {
                            try
                            {
                                _filedDate = System.DateTime.Parse(_fields[8]);
                            }
                            catch { }
                        }
                        if (_fields[9].Length > 0)
                        {
                            try
                            {
                                _enteredDate = System.DateTime.Parse(_fields[9]);
                            }
                            catch { }
                        }
                        if (_fields[10].Length > 0)
                        {
                            try
                            {
                                _closedDate = System.DateTime.Parse(_fields[10]);
                            }
                            catch { }
                        }
                        _disposition = _fields[11];
                        _caseType = _fields[12];
                        _trusteeLastName = _fields[13];
                        _judgeLastName = _fields[14];
                        _countyFiled = _fields[15];
                        _officeName = _fields[16];
                        _filingFeePaymentStatus = _fields[17];
                        _assetsInCase = _fields[18];
                        _caseDisposition = _fields[19];
                        if (_fields[20].Length > 0)
                        {
                            try
                            {
                                _jointDebtorDischargeDate = System.DateTime.Parse(_fields[20]);
                            }
                            catch { }
                        }
                        if (_fields[21].Length > 0)
                        {
                            try
                            {
                                _jointDebtorDismissalDate = System.DateTime.Parse(_fields[21]);
                            }
                            catch { }
                        }
                        _jointDebtorDispositionCode = _fields[22];
                        _jointDebtorDisposition = _fields[23];
                        _divisionalOfficeFiled = _fields[24];
                        _adversaryProceedingSeqNo = _fields[25];
                        _adversaryProceedingYear = _fields[26];
                        _adversaryProceedingDisposition = _fields[27];
                        _partyFirstName = _fields[28];
                        _partyMiddleName = _fields[29];
                        _partyLastName = _fields[30];
                        _partyGeneration = _fields[31];
                        _partyOffice = _fields[32];
                        _partyAddressLine1 = _fields[33];
                        _partyAddressLine2 = _fields[34];
                        _partyAddressLine3 = _fields[35];
                        _partyCity = _fields[36];
                        _partyState = _fields[37];
                        _partyZipCode = _fields[38];
                        _partyCountry = _fields[39];
                        _partyPhone = _fields[40];
                        _partySSNo = _fields[41];
                        _partyTaxID = _fields[42];
                        if (_fields[43].Length > 0)
                        {
                            try
                            {
                                _partyCaseSeqNo = Int32.Parse(_fields[43]);
                            }
                            catch { }
                        }
                        _partyCaseRole = _fields[44];
                        if (_fields[45].Length > 0)
                        {
                            try
                            {
                                _partyTerminatedDate = System.DateTime.Parse(_fields[45]);
                            }
                            catch { }
                        }
                        _caseTitle = _fields[46];
                        _attorneyFirstName = _fields[47];
                        _attorneyMiddleName = _fields[48];
                        _attorneyLastName = _fields[49];
                        _attorneyGeneration = _fields[50];
                        _attorneyOffice = _fields[51];
                        _attorneyOfficeLine1 = _fields[52];
                        _attorneyOfficeLine2 = _fields[53];
                        _attorneyOfficeLine3 = _fields[54];
                        _attorneyCity = _fields[55];
                        _attorneyState = _fields[56];
                        _attorneyZipCode = _fields[57];
                        _attorneyCountry = _fields[58];
                        _attorneyPhone = _fields[59];
                    break;
                    case 47:
                        //V3.3.x, 3.4.x
                        _caseNumber2Digit = _fields[0];
                        _caseNumber4Digit = _fields[1];
                        _cMECF = _fields[2];
                        if (_fields[3].Length >0)
                        {
                            try
                            {
                                _chapter = Int32.Parse(_fields[3]);
                            }
                            catch { }
                        }
                        _previousChapter = _fields[4];
                        if (_fields[5].Length > 0)
                        {
                            try
                            {
                                _conversionDate = System.DateTime.Parse(_fields[5]);
                            }
                            catch { }
                        }
                        if (_fields[6].Length > 0)
                        {
                            try
                            {
                                _dischargedDate = System.DateTime.Parse(_fields[6]);
                            }
                            catch { }
                        }
                        if (_fields[7].Length > 0)
                        {
                            try
                            {
                                _dismissedDate = System.DateTime.Parse(_fields[7]);
                            }
                            catch { }
                        }
                        if (_fields[8].Length > 0)
                        {
                            try
                            {
                                _filedDate = System.DateTime.Parse(_fields[8]);
                            }
                            catch { }
                        }
                        if (_fields[9].Length > 0)
                        {
                            try
                            {
                                _enteredDate = System.DateTime.Parse(_fields[9]);
                            }
                            catch { }
                        }
                        if (_fields[10].Length > 0)
                        {
                            try
                            {
                                _closedDate = System.DateTime.Parse(_fields[10]);
                            }
                            catch { }
                        }
                        _disposition = _fields[11];
                        _caseType = _fields[12];
                        _trusteeLastName = _fields[13];
                        _judgeLastName = _fields[14];
                        _countyFiled = _fields[15];
                        _officeName = _fields[16];
                        _filingFeePaymentStatus = _fields[17];
                        _assetsInCase = _fields[18];
                        _caseDisposition = _fields[19];
                        if (_fields[20].Length > 0)
                        {
                            try
                            {
                                _jointDebtorDischargeDate = System.DateTime.Parse(_fields[20]);
                            }
                            catch { }
                        }
                        if (_fields[21].Length > 0)
                        {
                            try
                            {
                                _jointDebtorDismissalDate = System.DateTime.Parse(_fields[21]);
                            }
                            catch { }
                        }
                        _jointDebtorDispositionCode = _fields[22];
                        _jointDebtorDisposition = _fields[23];
                        _divisionalOfficeFiled = _fields[24];
                        _adversaryProceedingSeqNo = _fields[25];
                        _adversaryProceedingYear = _fields[26];
                        _adversaryProceedingDisposition = _fields[27];
                        _partyFirstName = _fields[28];
                        _partyMiddleName = _fields[29];
                        _partyLastName = _fields[30];
                        _partyGeneration = _fields[31];
                        _partyOffice = _fields[32];
                        _partyAddressLine1 = _fields[33];
                        _partyAddressLine2 = _fields[34];
                        _partyAddressLine3 = _fields[35];
                        _partyCity = _fields[36];
                        _partyState = _fields[37];
                        _partyZipCode = _fields[38];
                        _partyCountry = _fields[39];
                        _partyPhone = _fields[40];
                        _partySSNo = _fields[41];
                        _partyTaxID = _fields[42];
                        if (_fields[43].Length > 0)
                        {
                            try
                            {
                            _partyCaseSeqNo = Int32.Parse(_fields[43]);
                            }
                            catch { }
                        }
                        _partyCaseRole = _fields[44];
                        if (_fields[45].Length > 0)
                        {
                            try
                            {
                                _partyTerminatedDate = System.DateTime.Parse(_fields[45]);
                            }
                            catch { }
                        }
                        _caseTitle = _fields[46];
                        break;
                    default:
                        break;
                }
                    

            }
            catch (Exception ex)
            { throw new Exception("THe parsing of the downloaded file into rows returned: " + ex.ToString()); }
        }
        public PacerTempRawImportData(string CaseNumber2Digit, string CaseNumber4Digit, string CMECF, int Chapter, string PreviousChapter, DateTime ConversionDate, DateTime DischargedDate, DateTime DismissedDate, DateTime FiledDate, DateTime EnteredDate, DateTime ClosedDate, string Disposition, string CaseType, string TrusteeLastName, string JudgeLastName, string CountyFiled, string OfficeName, string FilingFeePaymentStatus, string AssetsInCase, string CaseDisposition, DateTime JointDebtorDischargeDate, DateTime JointDebtorDismissalDate, string JointDebtorDispositionCode, string JointDebtorDisposition, string DivisionalOfficeFiled, string AdversaryProceedingSeqNo, string AdversaryProceedingYear, string AdversaryProceedingDisposition, string PartyFirstName, string PartyMiddleName, string PartyLastName, string PartyGeneration, string PartyOffice, string PartyAddressLine1, string PartyAddressLine2, string PartyAddressLine3, string PartyCity, string PartyState, string PartyZipCode, string PartyCountry, string PartyPhone, string PartySSNo, string PartyTaxID, int PartyCaseSeqNo, string PartyCaseRole, DateTime PartyTerminatedDate, string CaseTitle, string AttorneyFirstName, string AttorneyMiddleName, string AttorneyLastName, string AttorneyGeneration, string AttorneyOffice, string AttorneyOfficeLine1, string AttorneyOfficeLine2, string AttorneyOfficeLine3, string AttorneyCity, string AttorneyState, string AttorneyZipCode, string AttorneyCountry, string AttorneyPhone, string nullcolumn)
        {
			_caseNumber2Digit = CaseNumber2Digit;
			_caseNumber4Digit = CaseNumber4Digit;
			_cMECF = CMECF;
			_chapter = Chapter;
			_previousChapter = PreviousChapter;
			_conversionDate = ConversionDate;
			_dischargedDate = DischargedDate;
			_dismissedDate = DismissedDate;
			_filedDate = FiledDate;
			_enteredDate = EnteredDate;
			_closedDate = ClosedDate;
			_disposition = Disposition;
			_caseType = CaseType;
			_trusteeLastName = TrusteeLastName;
			_judgeLastName = JudgeLastName;
			_countyFiled = CountyFiled;
			_officeName = OfficeName;
			_filingFeePaymentStatus = FilingFeePaymentStatus;
			_assetsInCase = AssetsInCase;
			_caseDisposition = CaseDisposition;
			_jointDebtorDischargeDate = JointDebtorDischargeDate;
			_jointDebtorDismissalDate = JointDebtorDismissalDate;
			_jointDebtorDispositionCode = JointDebtorDispositionCode;
			_jointDebtorDisposition = JointDebtorDisposition;
			_divisionalOfficeFiled = DivisionalOfficeFiled;
			_adversaryProceedingSeqNo = AdversaryProceedingSeqNo;
			_adversaryProceedingYear = AdversaryProceedingYear;
			_adversaryProceedingDisposition = AdversaryProceedingDisposition;
			_partyFirstName = PartyFirstName;
			_partyMiddleName = PartyMiddleName;
			_partyLastName = PartyLastName;
			_partyGeneration = PartyGeneration;
			_partyOffice = PartyOffice;
			_partyAddressLine1 = PartyAddressLine1;
			_partyAddressLine2 = PartyAddressLine2;
			_partyAddressLine3 = PartyAddressLine3;
			_partyCity = PartyCity;
			_partyState = PartyState;
			_partyZipCode = PartyZipCode;
			_partyCountry = PartyCountry;
			_partyPhone = PartyPhone;
			_partySSNo = PartySSNo;
			_partyTaxID = PartyTaxID;
			_partyCaseSeqNo = PartyCaseSeqNo;
			_partyCaseRole = PartyCaseRole;
			_partyTerminatedDate = PartyTerminatedDate;
			_caseTitle = CaseTitle;
			_attorneyFirstName = AttorneyFirstName;
			_attorneyMiddleName = AttorneyMiddleName;
			_attorneyLastName = AttorneyLastName;
			_attorneyGeneration = AttorneyGeneration;
			_attorneyOffice = AttorneyOffice;
			_attorneyOfficeLine1 = AttorneyOfficeLine1;
			_attorneyOfficeLine2 = AttorneyOfficeLine2;
			_attorneyOfficeLine3 = AttorneyOfficeLine3;
			_attorneyCity = AttorneyCity;
			_attorneyState = AttorneyState;
			_attorneyZipCode = AttorneyZipCode;
			_attorneyCountry = AttorneyCountry;
			_attorneyPhone = AttorneyPhone;
			_nullcolumn = nullcolumn;
		}
		public PacerTempRawImportData Copy()
		{
			PacerTempRawImportData _pacerTempRawImportData = new PacerTempRawImportData();
			_pacerTempRawImportData.CaseNumber2Digit = _caseNumber2Digit;
			_pacerTempRawImportData.CaseNumber4Digit = _caseNumber4Digit;
			_pacerTempRawImportData.CMECF = _cMECF;
			_pacerTempRawImportData.Chapter = _chapter;
			_pacerTempRawImportData.PreviousChapter = _previousChapter;
			_pacerTempRawImportData.ConversionDate = _conversionDate;
			_pacerTempRawImportData.DischargedDate = _dischargedDate;
			_pacerTempRawImportData.DismissedDate = _dismissedDate;
			_pacerTempRawImportData.FiledDate = _filedDate;
			_pacerTempRawImportData.EnteredDate = _enteredDate;
			_pacerTempRawImportData.ClosedDate = _closedDate;
			_pacerTempRawImportData.Disposition = _disposition;
			_pacerTempRawImportData.CaseType = _caseType;
			_pacerTempRawImportData.TrusteeLastName = _trusteeLastName;
			_pacerTempRawImportData.JudgeLastName = _judgeLastName;
			_pacerTempRawImportData.CountyFiled = _countyFiled;
			_pacerTempRawImportData.OfficeName = _officeName;
			_pacerTempRawImportData.FilingFeePaymentStatus = _filingFeePaymentStatus;
			_pacerTempRawImportData.AssetsInCase = _assetsInCase;
			_pacerTempRawImportData.CaseDisposition = _caseDisposition;
			_pacerTempRawImportData.JointDebtorDischargeDate = _jointDebtorDischargeDate;
			_pacerTempRawImportData.JointDebtorDismissalDate = _jointDebtorDismissalDate;
			_pacerTempRawImportData.JointDebtorDispositionCode = _jointDebtorDispositionCode;
			_pacerTempRawImportData.JointDebtorDisposition = _jointDebtorDisposition;
			_pacerTempRawImportData.DivisionalOfficeFiled = _divisionalOfficeFiled;
			_pacerTempRawImportData.AdversaryProceedingSeqNo = _adversaryProceedingSeqNo;
			_pacerTempRawImportData.AdversaryProceedingYear = _adversaryProceedingYear;
			_pacerTempRawImportData.AdversaryProceedingDisposition = _adversaryProceedingDisposition;
			_pacerTempRawImportData.PartyFirstName = _partyFirstName;
			_pacerTempRawImportData.PartyMiddleName = _partyMiddleName;
			_pacerTempRawImportData.PartyLastName = _partyLastName;
			_pacerTempRawImportData.PartyGeneration = _partyGeneration;
			_pacerTempRawImportData.PartyOffice = _partyOffice;
			_pacerTempRawImportData.PartyAddressLine1 = _partyAddressLine1;
			_pacerTempRawImportData.PartyAddressLine2 = _partyAddressLine2;
			_pacerTempRawImportData.PartyAddressLine3 = _partyAddressLine3;
			_pacerTempRawImportData.PartyCity = _partyCity;
			_pacerTempRawImportData.PartyState = _partyState;
			_pacerTempRawImportData.PartyZipCode = _partyZipCode;
			_pacerTempRawImportData.PartyCountry = _partyCountry;
			_pacerTempRawImportData.PartyPhone = _partyPhone;
			_pacerTempRawImportData.PartySSNo = _partySSNo;
			_pacerTempRawImportData.PartyTaxID = _partyTaxID;
			_pacerTempRawImportData.PartyCaseSeqNo = _partyCaseSeqNo;
			_pacerTempRawImportData.PartyCaseRole = _partyCaseRole;
			_pacerTempRawImportData.PartyTerminatedDate = _partyTerminatedDate;
			_pacerTempRawImportData.CaseTitle = _caseTitle;
			_pacerTempRawImportData.AttorneyFirstName = _attorneyFirstName;
			_pacerTempRawImportData.AttorneyMiddleName = _attorneyMiddleName;
			_pacerTempRawImportData.AttorneyLastName = _attorneyLastName;
			_pacerTempRawImportData.AttorneyGeneration = _attorneyGeneration;
			_pacerTempRawImportData.AttorneyOffice = _attorneyOffice;
			_pacerTempRawImportData.AttorneyOfficeLine1 = _attorneyOfficeLine1;
			_pacerTempRawImportData.AttorneyOfficeLine2 = _attorneyOfficeLine2;
			_pacerTempRawImportData.AttorneyOfficeLine3 = _attorneyOfficeLine3;
			_pacerTempRawImportData.AttorneyCity = _attorneyCity;
			_pacerTempRawImportData.AttorneyState = _attorneyState;
			_pacerTempRawImportData.AttorneyZipCode = _attorneyZipCode;
			_pacerTempRawImportData.AttorneyCountry = _attorneyCountry;
			_pacerTempRawImportData.AttorneyPhone = _attorneyPhone;
			_pacerTempRawImportData.nullcolumn = _nullcolumn;
			return _pacerTempRawImportData;
		}
		#region "Private Instance Variables"
		private string _caseNumber2Digit;
		private string _caseNumber4Digit;
		private string _cMECF;
		private int _chapter;
		private string _previousChapter;
		private DateTime _conversionDate;
		private DateTime _dischargedDate;
		private DateTime _dismissedDate;
		private DateTime _filedDate;
		private DateTime _enteredDate;
		private DateTime _closedDate;
		private string _disposition;
		private string _caseType;
		private string _trusteeLastName;
		private string _judgeLastName;
		private string _countyFiled;
		private string _officeName;
		private string _filingFeePaymentStatus;
		private string _assetsInCase;
		private string _caseDisposition;
		private DateTime _jointDebtorDischargeDate;
		private DateTime _jointDebtorDismissalDate;
		private string _jointDebtorDispositionCode;
		private string _jointDebtorDisposition;
		private string _divisionalOfficeFiled;
		private string _adversaryProceedingSeqNo;
		private string _adversaryProceedingYear;
		private string _adversaryProceedingDisposition;
		private string _partyFirstName;
		private string _partyMiddleName;
		private string _partyLastName;
		private string _partyGeneration;
		private string _partyOffice;
		private string _partyAddressLine1;
		private string _partyAddressLine2;
		private string _partyAddressLine3;
		private string _partyCity;
		private string _partyState;
		private string _partyZipCode;
		private string _partyCountry;
		private string _partyPhone;
		private string _partySSNo;
		private string _partyTaxID;
		private int _partyCaseSeqNo;
		private string _partyCaseRole;
		private DateTime _partyTerminatedDate;
		private string _caseTitle;
		private string _attorneyFirstName;
		private string _attorneyMiddleName;
		private string _attorneyLastName;
		private string _attorneyGeneration;
		private string _attorneyOffice;
		private string _attorneyOfficeLine1;
		private string _attorneyOfficeLine2;
		private string _attorneyOfficeLine3;
		private string _attorneyCity;
		private string _attorneyState;
		private string _attorneyZipCode;
		private string _attorneyCountry;
		private string _attorneyPhone;
		private string _nullcolumn;
		#endregion 
		#region "Public Properties"
		public string CaseNumber2Digit
		{
			get
			{
				return _caseNumber2Digit;
			}
			set
			{
				if (value != _caseNumber2Digit)
				{
					this._isModified = true;
					_caseNumber2Digit = value;
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
				}
			}
		}

		public string CMECF
		{
			get
			{
				return _cMECF;
			}
			set
			{
				if (value != _cMECF)
				{
					this._isModified = true;
					_cMECF = value;
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
				}
			}
		}

		public string PreviousChapter
		{
			get
			{
				return _previousChapter;
			}
			set
			{
				if (value != _previousChapter)
				{
					this._isModified = true;
					_previousChapter = value;
				}
			}
		}

		public DateTime ConversionDate
		{
			get
			{
				return _conversionDate;
			}
			set
			{
				if (value != _conversionDate)
				{
					this._isModified = true;
					_conversionDate = value;
				}
			}
		}

		public DateTime DischargedDate
		{
			get
			{
				return _dischargedDate;
			}
			set
			{
				if (value != _dischargedDate)
				{
					this._isModified = true;
					_dischargedDate = value;
				}
			}
		}

		public DateTime DismissedDate
		{
			get
			{
				return _dismissedDate;
			}
			set
			{
				if (value != _dismissedDate)
				{
					this._isModified = true;
					_dismissedDate = value;
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
				}
			}
		}

		public DateTime EnteredDate
		{
			get
			{
				return _enteredDate;
			}
			set
			{
				if (value != _enteredDate)
				{
					this._isModified = true;
					_enteredDate = value;
				}
			}
		}

		public DateTime ClosedDate
		{
			get
			{
				return _closedDate;
			}
			set
			{
				if (value != _closedDate)
				{
					this._isModified = true;
					_closedDate = value;
				}
			}
		}

		public string Disposition
		{
			get
			{
				return _disposition;
			}
			set
			{
				if (value != _disposition)
				{
					this._isModified = true;
					_disposition = value;
				}
			}
		}

		public string CaseType
		{
			get
			{
				return _caseType;
			}
			set
			{
				if (value != _caseType)
				{
					this._isModified = true;
					_caseType = value;
				}
			}
		}

		public string TrusteeLastName
		{
			get
			{
				return _trusteeLastName;
			}
			set
			{
				if (value != _trusteeLastName)
				{
					this._isModified = true;
					_trusteeLastName = value;
				}
			}
		}

		public string JudgeLastName
		{
			get
			{
				return _judgeLastName;
			}
			set
			{
				if (value != _judgeLastName)
				{
					this._isModified = true;
					_judgeLastName = value;
				}
			}
		}

		public string CountyFiled
		{
			get
			{
				return _countyFiled;
			}
			set
			{
				if (value != _countyFiled)
				{
					this._isModified = true;
					_countyFiled = value;
				}
			}
		}

		public string OfficeName
		{
			get
			{
				return _officeName;
			}
			set
			{
				if (value != _officeName)
				{
					this._isModified = true;
					_officeName = value;
				}
			}
		}

		public string FilingFeePaymentStatus
		{
			get
			{
				return _filingFeePaymentStatus;
			}
			set
			{
				if (value != _filingFeePaymentStatus)
				{
					this._isModified = true;
					_filingFeePaymentStatus = value;
				}
			}
		}

		public string AssetsInCase
		{
			get
			{
				return _assetsInCase;
			}
			set
			{
				if (value != _assetsInCase)
				{
					this._isModified = true;
					_assetsInCase = value;
				}
			}
		}

		public string CaseDisposition
		{
			get
			{
				return _caseDisposition;
			}
			set
			{
				if (value != _caseDisposition)
				{
					this._isModified = true;
					_caseDisposition = value;
				}
			}
		}

		public DateTime JointDebtorDischargeDate
		{
			get
			{
				return _jointDebtorDischargeDate;
			}
			set
			{
				if (value != _jointDebtorDischargeDate)
				{
					this._isModified = true;
					_jointDebtorDischargeDate = value;
				}
			}
		}

		public DateTime JointDebtorDismissalDate
		{
			get
			{
				return _jointDebtorDismissalDate;
			}
			set
			{
				if (value != _jointDebtorDismissalDate)
				{
					this._isModified = true;
					_jointDebtorDismissalDate = value;
				}
			}
		}

		public string JointDebtorDispositionCode
		{
			get
			{
				return _jointDebtorDispositionCode;
			}
			set
			{
				if (value != _jointDebtorDispositionCode)
				{
					this._isModified = true;
					_jointDebtorDispositionCode = value;
				}
			}
		}

		public string JointDebtorDisposition
		{
			get
			{
				return _jointDebtorDisposition;
			}
			set
			{
				if (value != _jointDebtorDisposition)
				{
					this._isModified = true;
					_jointDebtorDisposition = value;
				}
			}
		}

		public string DivisionalOfficeFiled
		{
			get
			{
				return _divisionalOfficeFiled;
			}
			set
			{
				if (value != _divisionalOfficeFiled)
				{
					this._isModified = true;
					_divisionalOfficeFiled = value;
				}
			}
		}

		public string AdversaryProceedingSeqNo
		{
			get
			{
				return _adversaryProceedingSeqNo;
			}
			set
			{
				if (value != _adversaryProceedingSeqNo)
				{
					this._isModified = true;
					_adversaryProceedingSeqNo = value;
				}
			}
		}

		public string AdversaryProceedingYear
		{
			get
			{
				return _adversaryProceedingYear;
			}
			set
			{
				if (value != _adversaryProceedingYear)
				{
					this._isModified = true;
					_adversaryProceedingYear = value;
				}
			}
		}

		public string AdversaryProceedingDisposition
		{
			get
			{
				return _adversaryProceedingDisposition;
			}
			set
			{
				if (value != _adversaryProceedingDisposition)
				{
					this._isModified = true;
					_adversaryProceedingDisposition = value;
				}
			}
		}

		public string PartyFirstName
		{
			get
			{
				return _partyFirstName;
			}
			set
			{
				if (value != _partyFirstName)
				{
					this._isModified = true;
					_partyFirstName = value;
				}
			}
		}

		public string PartyMiddleName
		{
			get
			{
				return _partyMiddleName;
			}
			set
			{
				if (value != _partyMiddleName)
				{
					this._isModified = true;
					_partyMiddleName = value;
				}
			}
		}

		public string PartyLastName
		{
			get
			{
				return _partyLastName;
			}
			set
			{
				if (value != _partyLastName)
				{
					this._isModified = true;
					_partyLastName = value;
				}
			}
		}

		public string PartyGeneration
		{
			get
			{
				return _partyGeneration;
			}
			set
			{
				if (value != _partyGeneration)
				{
					this._isModified = true;
					_partyGeneration = value;
				}
			}
		}

		public string PartyOffice
		{
			get
			{
				return _partyOffice;
			}
			set
			{
				if (value != _partyOffice)
				{
					this._isModified = true;
					_partyOffice = value;
				}
			}
		}

		public string PartyAddressLine1
		{
			get
			{
				return _partyAddressLine1;
			}
			set
			{
				if (value != _partyAddressLine1)
				{
					this._isModified = true;
					_partyAddressLine1 = value;
				}
			}
		}

		public string PartyAddressLine2
		{
			get
			{
				return _partyAddressLine2;
			}
			set
			{
				if (value != _partyAddressLine2)
				{
					this._isModified = true;
					_partyAddressLine2 = value;
				}
			}
		}

		public string PartyAddressLine3
		{
			get
			{
				return _partyAddressLine3;
			}
			set
			{
				if (value != _partyAddressLine3)
				{
					this._isModified = true;
					_partyAddressLine3 = value;
				}
			}
		}

		public string PartyCity
		{
			get
			{
				return _partyCity;
			}
			set
			{
				if (value != _partyCity)
				{
					this._isModified = true;
					_partyCity = value;
				}
			}
		}

		public string PartyState
		{
			get
			{
				return _partyState;
			}
			set
			{
				if (value != _partyState)
				{
					this._isModified = true;
					_partyState = value;
				}
			}
		}

		public string PartyZipCode
		{
			get
			{
				return _partyZipCode;
			}
			set
			{
				if (value != _partyZipCode)
				{
					this._isModified = true;
					_partyZipCode = value;
				}
			}
		}

		public string PartyCountry
		{
			get
			{
				return _partyCountry;
			}
			set
			{
				if (value != _partyCountry)
				{
					this._isModified = true;
					_partyCountry = value;
				}
			}
		}

		public string PartyPhone
		{
			get
			{
				return _partyPhone;
			}
			set
			{
				if (value != _partyPhone)
				{
					this._isModified = true;
					_partyPhone = value;
				}
			}
		}

		public string PartySSNo
		{
			get
			{
				return _partySSNo;
			}
			set
			{
				if (value != _partySSNo)
				{
					this._isModified = true;
					_partySSNo = value;
				}
			}
		}

		public string PartyTaxID
		{
			get
			{
				return _partyTaxID;
			}
			set
			{
				if (value != _partyTaxID)
				{
					this._isModified = true;
					_partyTaxID = value;
				}
			}
		}

		public int PartyCaseSeqNo
		{
			get
			{
				return _partyCaseSeqNo;
			}
			set
			{
				if (value != _partyCaseSeqNo)
				{
					this._isModified = true;
					_partyCaseSeqNo = value;
				}
			}
		}

		public string PartyCaseRole
		{
			get
			{
				return _partyCaseRole;
			}
			set
			{
				if (value != _partyCaseRole)
				{
					this._isModified = true;
					_partyCaseRole = value;
				}
			}
		}

		public DateTime PartyTerminatedDate
		{
			get
			{
				return _partyTerminatedDate;
			}
			set
			{
				if (value != _partyTerminatedDate)
				{
					this._isModified = true;
					_partyTerminatedDate = value;
				}
			}
		}

		public string CaseTitle
		{
			get
			{
				return _caseTitle;
			}
			set
			{
				if (value != _caseTitle)
				{
					this._isModified = true;
					_caseTitle = value;
				}
			}
		}

		public string AttorneyFirstName
		{
			get
			{
				return _attorneyFirstName;
			}
			set
			{
				if (value != _attorneyFirstName)
				{
					this._isModified = true;
					_attorneyFirstName = value;
				}
			}
		}

		public string AttorneyMiddleName
		{
			get
			{
				return _attorneyMiddleName;
			}
			set
			{
				if (value != _attorneyMiddleName)
				{
					this._isModified = true;
					_attorneyMiddleName = value;
				}
			}
		}

		public string AttorneyLastName
		{
			get
			{
				return _attorneyLastName;
			}
			set
			{
				if (value != _attorneyLastName)
				{
					this._isModified = true;
					_attorneyLastName = value;
				}
			}
		}

		public string AttorneyGeneration
		{
			get
			{
				return _attorneyGeneration;
			}
			set
			{
				if (value != _attorneyGeneration)
				{
					this._isModified = true;
					_attorneyGeneration = value;
				}
			}
		}

		public string AttorneyOffice
		{
			get
			{
				return _attorneyOffice;
			}
			set
			{
				if (value != _attorneyOffice)
				{
					this._isModified = true;
					_attorneyOffice = value;
				}
			}
		}

		public string AttorneyOfficeLine1
		{
			get
			{
				return _attorneyOfficeLine1;
			}
			set
			{
				if (value != _attorneyOfficeLine1)
				{
					this._isModified = true;
					_attorneyOfficeLine1 = value;
				}
			}
		}

		public string AttorneyOfficeLine2
		{
			get
			{
				return _attorneyOfficeLine2;
			}
			set
			{
				if (value != _attorneyOfficeLine2)
				{
					this._isModified = true;
					_attorneyOfficeLine2 = value;
				}
			}
		}

		public string AttorneyOfficeLine3
		{
			get
			{
				return _attorneyOfficeLine3;
			}
			set
			{
				if (value != _attorneyOfficeLine3)
				{
					this._isModified = true;
					_attorneyOfficeLine3 = value;
				}
			}
		}

		public string AttorneyCity
		{
			get
			{
				return _attorneyCity;
			}
			set
			{
				if (value != _attorneyCity)
				{
					this._isModified = true;
					_attorneyCity = value;
				}
			}
		}

		public string AttorneyState
		{
			get
			{
				return _attorneyState;
			}
			set
			{
				if (value != _attorneyState)
				{
					this._isModified = true;
					_attorneyState = value;
				}
			}
		}

		public string AttorneyZipCode
		{
			get
			{
				return _attorneyZipCode;
			}
			set
			{
				if (value != _attorneyZipCode)
				{
					this._isModified = true;
					_attorneyZipCode = value;
				}
			}
		}

		public string AttorneyCountry
		{
			get
			{
				return _attorneyCountry;
			}
			set
			{
				if (value != _attorneyCountry)
				{
					this._isModified = true;
					_attorneyCountry = value;
				}
			}
		}

		public string AttorneyPhone
		{
			get
			{
				return _attorneyPhone;
			}
			set
			{
				if (value != _attorneyPhone)
				{
					this._isModified = true;
					_attorneyPhone = value;
				}
			}
		}

		public string nullcolumn
		{
			get
			{
				return _nullcolumn;
			}
			set
			{
				if (value != _nullcolumn)
				{
					this._isModified = true;
					_nullcolumn = value;
				}
			}
		}

		#endregion 
		public int CompareTo(Object objPacerTempRawImportData)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
            return this.CaseNumber2Digit.CompareTo(((PacerTempRawImportData)objPacerTempRawImportData).CaseNumber2Digit);
		}
	}
}
