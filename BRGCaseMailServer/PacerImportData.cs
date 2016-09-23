using System;
using System.Collections;


namespace BRGCaseMailServer
{
    /// <summary>
    /// A Class representing a single row in the PacerImportData table.
    /// Author: Jonathan Shaw
    /// Date Created: 4/17/2011 1:00:55 AM
    /// </summary>
    public class PacerImportData : IComparable
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
        //public SolidColorBrush background { get { switch (this.severity) { case Severity.Error: return new SolidColorBrush(Colors.Red); case Severity.Warning: return new SolidColorBrush(Colors.Yellow); default: throw new Exception("severity out of bounds"); } } } 

        public PacerImportData()
        {
            _iD = 0;
            _imported = false;
            _pacerImportTransactionID = 0;
            _caseNumber2Digit = string.Empty;
            _caseNumber4Digit = string.Empty;
            _cMECF = string.Empty;
            _chapter = 0;
            _previousChapter = string.Empty;
            _disposition = string.Empty;
            _caseType = string.Empty;
            _trusteeLastName = string.Empty;
            _judgeLastName = string.Empty;
            _countyFiled = string.Empty;
            _officeName = string.Empty;
            _filingFeePaymentStatus = string.Empty;
            _assetsInCase = string.Empty;
            _caseDisposition = string.Empty;
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
            _courtID = 0;
            _dischargedCases = false;
            _courtName = string.Empty;
        }
        public PacerImportData(int ID, bool Imported, int PacerImportTransactionID, string CaseNumber2Digit, string CaseNumber4Digit, string CMECF, int Chapter, string PreviousChapter, DateTime? ConversionDate, DateTime? DischargedDate, DateTime? DismissedDate, DateTime? FiledDate, DateTime? EnteredDate, DateTime? ClosedDate, string Disposition, string CaseType, string TrusteeLastName, string JudgeLastName, string CountyFiled, string OfficeName, string FilingFeePaymentStatus, string AssetsInCase, string CaseDisposition, DateTime? JointDebtorDischargeDate, DateTime? JointDebtorDismissalDate, string JointDebtorDispositionCode, string JointDebtorDisposition, string DivisionalOfficeFiled, string AdversaryProceedingSeqNo, string AdversaryProceedingYear, string AdversaryProceedingDisposition, string PartyFirstName, string PartyMiddleName, string PartyLastName, string PartyGeneration, string PartyOffice, string PartyAddressLine1, string PartyAddressLine2, string PartyAddressLine3, string PartyCity, string PartyState, string PartyZipCode, string PartyCountry, string PartyPhone, string PartySSNo, string PartyTaxID, int PartyCaseSeqNo, string PartyCaseRole, DateTime? PartyTerminatedDate, string CaseTitle, string AttorneyFirstName, string AttorneyMiddleName, string AttorneyLastName, string AttorneyGeneration, string AttorneyOffice, string AttorneyOfficeLine1, string AttorneyOfficeLine2, string AttorneyOfficeLine3, string AttorneyCity, string AttorneyState, string AttorneyZipCode, string AttorneyCountry, string AttorneyPhone, int CourtID, bool DischargedCases, string CourtName)
        {
            _iD = ID;
            _imported = Imported;
            _pacerImportTransactionID = PacerImportTransactionID;
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
            _courtID = CourtID;
            _dischargedCases = DischargedCases;
            _courtName = CourtName;
        }
        public PacerImportData Copy()
        {
            PacerImportData _pacerImportData = new PacerImportData();
            _pacerImportData.ID = _iD;
            _pacerImportData.Imported = _imported;
            _pacerImportData.PacerImportTransactionID = _pacerImportTransactionID;
            _pacerImportData.CaseNumber2Digit = _caseNumber2Digit;
            _pacerImportData.CaseNumber4Digit = _caseNumber4Digit;
            _pacerImportData.CMECF = _cMECF;
            _pacerImportData.Chapter = _chapter;
            _pacerImportData.PreviousChapter = _previousChapter;
            _pacerImportData.ConversionDate = _conversionDate;
            _pacerImportData.DischargedDate = _dischargedDate;
            _pacerImportData.DismissedDate = _dismissedDate;
            _pacerImportData.FiledDate = _filedDate;
            _pacerImportData.EnteredDate = _enteredDate;
            _pacerImportData.ClosedDate = _closedDate;
            _pacerImportData.Disposition = _disposition;
            _pacerImportData.CaseType = _caseType;
            _pacerImportData.TrusteeLastName = _trusteeLastName;
            _pacerImportData.JudgeLastName = _judgeLastName;
            _pacerImportData.CountyFiled = _countyFiled;
            _pacerImportData.OfficeName = _officeName;
            _pacerImportData.FilingFeePaymentStatus = _filingFeePaymentStatus;
            _pacerImportData.AssetsInCase = _assetsInCase;
            _pacerImportData.CaseDisposition = _caseDisposition;
            _pacerImportData.JointDebtorDischargeDate = _jointDebtorDischargeDate;
            _pacerImportData.JointDebtorDismissalDate = _jointDebtorDismissalDate;
            _pacerImportData.JointDebtorDispositionCode = _jointDebtorDispositionCode;
            _pacerImportData.JointDebtorDisposition = _jointDebtorDisposition;
            _pacerImportData.DivisionalOfficeFiled = _divisionalOfficeFiled;
            _pacerImportData.AdversaryProceedingSeqNo = _adversaryProceedingSeqNo;
            _pacerImportData.AdversaryProceedingYear = _adversaryProceedingYear;
            _pacerImportData.AdversaryProceedingDisposition = _adversaryProceedingDisposition;
            _pacerImportData.PartyFirstName = _partyFirstName;
            _pacerImportData.PartyMiddleName = _partyMiddleName;
            _pacerImportData.PartyLastName = _partyLastName;
            _pacerImportData.PartyGeneration = _partyGeneration;
            _pacerImportData.PartyOffice = _partyOffice;
            _pacerImportData.PartyAddressLine1 = _partyAddressLine1;
            _pacerImportData.PartyAddressLine2 = _partyAddressLine2;
            _pacerImportData.PartyAddressLine3 = _partyAddressLine3;
            _pacerImportData.PartyCity = _partyCity;
            _pacerImportData.PartyState = _partyState;
            _pacerImportData.PartyZipCode = _partyZipCode;
            _pacerImportData.PartyCountry = _partyCountry;
            _pacerImportData.PartyPhone = _partyPhone;
            _pacerImportData.PartySSNo = _partySSNo;
            _pacerImportData.PartyTaxID = _partyTaxID;
            _pacerImportData.PartyCaseSeqNo = _partyCaseSeqNo;
            _pacerImportData.PartyCaseRole = _partyCaseRole;
            _pacerImportData.PartyTerminatedDate = _partyTerminatedDate;
            _pacerImportData.CaseTitle = _caseTitle;
            _pacerImportData.AttorneyFirstName = _attorneyFirstName;
            _pacerImportData.AttorneyMiddleName = _attorneyMiddleName;
            _pacerImportData.AttorneyLastName = _attorneyLastName;
            _pacerImportData.AttorneyGeneration = _attorneyGeneration;
            _pacerImportData.AttorneyOffice = _attorneyOffice;
            _pacerImportData.AttorneyOfficeLine1 = _attorneyOfficeLine1;
            _pacerImportData.AttorneyOfficeLine2 = _attorneyOfficeLine2;
            _pacerImportData.AttorneyOfficeLine3 = _attorneyOfficeLine3;
            _pacerImportData.AttorneyCity = _attorneyCity;
            _pacerImportData.AttorneyState = _attorneyState;
            _pacerImportData.AttorneyZipCode = _attorneyZipCode;
            _pacerImportData.AttorneyCountry = _attorneyCountry;
            _pacerImportData.AttorneyPhone = _attorneyPhone;
            _pacerImportData.CourtID = _courtID;
            _pacerImportData.DischargedCases = _dischargedCases;
            _pacerImportData.CourtName = _courtName;
            return _pacerImportData;
        }
        #region "Private Instance Variables"
        private int _iD;
        private bool _imported;
        private int _pacerImportTransactionID;
        private string _caseNumber2Digit;
        private string _caseNumber4Digit;
        private string _cMECF;
        private int _chapter;
        private string _previousChapter;
        private DateTime? _conversionDate;
        private DateTime? _dischargedDate;
        private DateTime? _dismissedDate;
        private DateTime? _filedDate;
        private DateTime? _enteredDate;
        private DateTime? _closedDate;
        private string _disposition;
        private string _caseType;
        private string _trusteeLastName;
        private string _judgeLastName;
        private string _countyFiled;
        private string _officeName;
        private string _filingFeePaymentStatus;
        private string _assetsInCase;
        private string _caseDisposition;
        private DateTime? _jointDebtorDischargeDate;
        private DateTime? _jointDebtorDismissalDate;
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
        private DateTime? _partyTerminatedDate;
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
        private int _courtID;
        private bool _dischargedCases;
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
                }
            }
        }

        public bool Imported
        {
            get
            {
                return _imported;
            }
            set
            {
                if (value != _imported)
                {
                    this._isModified = true;
                    _imported = value;
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
                }
            }
        }

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

        public DateTime? ConversionDate
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

        public DateTime? DischargedDate
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

        public DateTime? DismissedDate
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

        public DateTime? FiledDate
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

        public DateTime? EnteredDate
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

        public DateTime? ClosedDate
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

        public DateTime? JointDebtorDischargeDate
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

        public DateTime? JointDebtorDismissalDate
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

        public DateTime? PartyTerminatedDate
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
                }
            }
        }

        public bool DischargedCases
        {
            get
            {
                return _dischargedCases;
            }
            set
            {
                if (value != _dischargedCases)
                {
                    this._isModified = true;
                    _dischargedCases = value;
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
                }
            }
        }

        #endregion
        public int CompareTo(Object objPacerImportData)
        {
            //sort by ID ascending - this is used by the default sort mechanisc
            return this.ID.CompareTo(((PacerImportData)objPacerImportData).ID);
        }


        public bool CareOfOK
        {
            get
            {
                if ((((_partyAddressLine1.ToUpper().Contains("HOSPITAL")) ||
                    (_partyAddressLine1.ToUpper().Contains("INMATE")) ||
                    (_partyAddressLine1.ToUpper().Contains("PRISON")) ||
                    (_partyAddressLine1.ToUpper().Contains("CORRECTION")) ||
                    (_partyAddressLine1.ToUpper().Contains("NURSING")))
                    ||
                ((_partyAddressLine2.ToUpper().Contains("HOSPITAL")) ||
                    (_partyAddressLine2.ToUpper().Contains("INMATE")) ||
                    (_partyAddressLine2.ToUpper().Contains("PRISON")) ||
                    (_partyAddressLine2.ToUpper().Contains("CORRECTION")) ||
                    (_partyAddressLine2.ToUpper().Contains("NURSING")))
                    ||
                    ((_partyAddressLine3.ToUpper().Contains("HOSPITAL")) ||
                    (_partyAddressLine3.ToUpper().Contains("INMATE")) ||
                    (_partyAddressLine3.ToUpper().Contains("PRISON")) ||
                    (_partyAddressLine3.ToUpper().Contains("CORRECTION")) ||
                    (_partyAddressLine3.ToUpper().Contains("NURSING"))))
                    &&
                    ((_partyAddressLine1.ToUpper().Contains("C/O") || _partyAddressLine1.ToUpper().Contains("CARE OF")) ||
                     (_partyAddressLine2.ToUpper().Contains("C/O") || _partyAddressLine1.ToUpper().Contains("CARE OF")) ||
                     (_partyAddressLine3.ToUpper().Contains("C/O") || _partyAddressLine1.ToUpper().Contains("CARE OF"))))
                {
                    return false;
                }
                else
                    return true;
            }
        }
    }
}