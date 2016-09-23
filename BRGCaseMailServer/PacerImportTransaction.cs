using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;
using System.Diagnostics;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the PacerImportTransaction table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/17/2011 10:42:30 AM
	/// </summary>
	public class PacerImportTransaction : IComparable
	{
        CaseSearchOptions o;
        FindCasesResult _result = null;
        BankruptcyParser _bankruptcyParser;
        Court _court;

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
		public PacerImportTransaction()
		{
			_iD = 0;
			_courtID = 0;
			_pacerFileFormatID = 0;
			_billablePages = 0;
			_cost = 0;
			_dischargedCases = false;
			_searchCriteria = string.Empty;
			_startDate = System.DateTime.MinValue;
			_endDate = System.DateTime.MinValue;
			_lineCount = 0;
			_totalCases = 0;
			_insertedCases = 0;
			_updatedCases = 0;
			_filePath = string.Empty;
			_downloadTimeStamp = System.DateTime.MinValue;
			_importStatus = string.Empty;
			_importMessage = string.Empty;
			_pacerFileVersion = string.Empty;
			_pacerFileFormatNumColumns = 0;
			_courtName = string.Empty;
		}
		public PacerImportTransaction(int ID, int CourtID, int PacerFileFormatID, int BillablePages, int Cost, bool DischargedCases, string SearchCriteria, DateTime StartDate, DateTime EndDate, int LineCount, int TotalCases, int InsertedCases, int UpdatedCases, string FilePath, DateTime DownloadTimeStamp, string ImportStatus, string ImportMessage, string PacerFileVersion, int PacerFileFormatNumColumns, string CourtName)		{
			_iD = ID;
			_courtID = CourtID;
			_pacerFileFormatID = PacerFileFormatID;
			_billablePages = BillablePages;
			_cost = Cost;
			_dischargedCases = DischargedCases;
			_searchCriteria = SearchCriteria;
			_startDate = StartDate;
			_endDate = EndDate;
			_lineCount = LineCount;
			_totalCases = TotalCases;
			_insertedCases = InsertedCases;
			_updatedCases = UpdatedCases;
			_filePath = FilePath;
			_downloadTimeStamp = DownloadTimeStamp;
			_importStatus = ImportStatus;
			_importMessage = ImportMessage;
			_pacerFileVersion = PacerFileVersion;
			_pacerFileFormatNumColumns = PacerFileFormatNumColumns;
			_courtName = CourtName;
		}
		public PacerImportTransaction Copy()
		{
			PacerImportTransaction _pacerImportTransaction = new PacerImportTransaction();
			_pacerImportTransaction.ID = _iD;
			_pacerImportTransaction.CourtID = _courtID;
			_pacerImportTransaction.PacerFileFormatID = _pacerFileFormatID;
			_pacerImportTransaction.BillablePages = _billablePages;
			_pacerImportTransaction.Cost = _cost;
			_pacerImportTransaction.DischargedCases = _dischargedCases;
			_pacerImportTransaction.SearchCriteria = _searchCriteria;
			_pacerImportTransaction.StartDate = _startDate;
			_pacerImportTransaction.EndDate = _endDate;
			_pacerImportTransaction.LineCount = _lineCount;
			_pacerImportTransaction.TotalCases = _totalCases;
			_pacerImportTransaction.InsertedCases = _insertedCases;
			_pacerImportTransaction.UpdatedCases = _updatedCases;
			_pacerImportTransaction.FilePath = _filePath;
			_pacerImportTransaction.DownloadTimeStamp = _downloadTimeStamp;
			_pacerImportTransaction.ImportStatus = _importStatus;
			_pacerImportTransaction.ImportMessage = _importMessage;
			_pacerImportTransaction.PacerFileVersion = _pacerFileVersion;
			_pacerImportTransaction.PacerFileFormatNumColumns = _pacerFileFormatNumColumns;
			_pacerImportTransaction.CourtName = _courtName;
			return _pacerImportTransaction;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _courtID;
		private int _pacerFileFormatID;
		private int _billablePages;
		private decimal _cost;
		private bool _dischargedCases;
		private string _searchCriteria;
		private DateTime _startDate;
		private DateTime _endDate;
		private int _lineCount;
		private int _totalCases;
		private int _insertedCases;
		private int _updatedCases;
		private string _filePath;
		private DateTime _downloadTimeStamp;
		private string _importStatus;
		private string _importMessage;
		private string _pacerFileVersion;
		private int _pacerFileFormatNumColumns;
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

		public int PacerFileFormatID
		{
			get
			{
				return _pacerFileFormatID;
			}
			set
			{
				if (value != _pacerFileFormatID)
				{
					this._isModified = true;
					_pacerFileFormatID = value;
				}
			}
		}

		public int BillablePages
		{
			get
			{
				return _billablePages;
			}
			set
			{
				if (value != _billablePages)
				{
					this._isModified = true;
					_billablePages = value;
				}
			}
		}

		public decimal Cost
		{
			get
			{
				return _cost;
			}
			set
			{
				if (value != _cost)
				{
					this._isModified = true;
					_cost = value;
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

		public string SearchCriteria
		{
			get
			{
				return _searchCriteria;
			}
			set
			{
				if (value != _searchCriteria)
				{
					this._isModified = true;
					_searchCriteria = value;
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
				}
			}
		}

		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				if (value != _endDate)
				{
					this._isModified = true;
					_endDate = value;
				}
			}
		}

		public int LineCount
		{
			get
			{
				return _lineCount;
			}
			set
			{
				if (value != _lineCount)
				{
					this._isModified = true;
					_lineCount = value;
				}
			}
		}

		public int TotalCases
		{
			get
			{
				return _totalCases;
			}
			set
			{
				if (value != _totalCases)
				{
					this._isModified = true;
					_totalCases = value;
				}
			}
		}

		public int InsertedCases
		{
			get
			{
				return _insertedCases;
			}
			set
			{
				if (value != _insertedCases)
				{
					this._isModified = true;
					_insertedCases = value;
				}
			}
		}

		public int UpdatedCases
		{
			get
			{
				return _updatedCases;
			}
			set
			{
				if (value != _updatedCases)
				{
					this._isModified = true;
					_updatedCases = value;
				}
			}
		}

		public string FilePath
		{
			get
			{
				return _filePath;
			}
			set
			{
				if (value != _filePath)
				{
					this._isModified = true;
					_filePath = value;
				}
			}
		}

		public DateTime DownloadTimeStamp
		{
			get
			{
				return _downloadTimeStamp;
			}
			set
			{
				if (value != _downloadTimeStamp)
				{
					this._isModified = true;
					_downloadTimeStamp = value;
				}
			}
		}

		public string ImportStatus
		{
			get
			{
				return _importStatus;
			}
			set
			{
				if (value != _importStatus)
				{
					this._isModified = true;
					_importStatus = value;
				}
			}
		}

		public string ImportMessage
		{
			get
			{
				return _importMessage;
			}
			set
			{
				if (value != _importMessage)
				{
					this._isModified = true;
					_importMessage = value;
				}
			}
		}

		public string PacerFileVersion
		{
			get
			{
				return _pacerFileVersion;
			}
			set
			{
				if (value != _pacerFileVersion)
				{
					this._isModified = true;
					_pacerFileVersion = value;
				}
			}
		}

		public int PacerFileFormatNumColumns
		{
			get
			{
				return _pacerFileFormatNumColumns;
			}
			set
			{
				if (value != _pacerFileFormatNumColumns)
				{
					this._isModified = true;
					_pacerFileFormatNumColumns = value;
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
		public int CompareTo(Object objPacerImportTransaction)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((PacerImportTransaction)objPacerImportTransaction).ID);
		}

        #region derived Properties
        public string DischargedCasesYesNo
        {
            get
            {
                if (DischargedCases == true)
                {
                    return "Discharged( 7 & 13 )";
                }
                else
                {
                    return "Filed( 7 )";
                }
            }
        }        
        #endregion

        public Court Court
        {
            get
            {
                return _court;
            }
            set
            {
                if (value != _court)
                {
                    this._isModified = true;
                    _court = value;
                    _courtID = _court.ID;
                }
            }
        }

        public bool QueryNewCases()
        {
            o = new CaseSearchOptions();

            string user = System.Configuration.ConfigurationManager.AppSettings.Get("PACERuser");
            string pass = System.Configuration.ConfigurationManager.AppSettings.Get("PACERPassword");

            _court = CourtService.GetByID(_courtID);

            _bankruptcyParser = new BankruptcyParser(_court.URLAbbrv, user, pass);            

            // Login to the system

            if (!_bankruptcyParser.Login())
            {
                _importMessage = "Could Not Login: " + _bankruptcyParser.lastError;
                _importStatus = "FAILURE";

                _court = CourtService.GetByID(_courtID);

                _bankruptcyParser = new BankruptcyParser(_court.URLAbbrv, user, pass);

                if (!_bankruptcyParser.NextGenLogin())
                {
                    return false;
                }
                else{
                    return true;
                }
            }
            
            
            
            // setup the search paramters
            o.open_cases = true;
            o.closed_cases = true;
            o.party_information = true;

            //set the chapters and discharged/filed
            o.chapter = new List<CaseSearchOptions.chapters>();
            if (_dischargedCases == true)
            {
                o.date_type = CaseSearchOptions.date_types.discharged;
                o.chapter.Add(CaseSearchOptions.chapters.seven);
                o.chapter.Add(CaseSearchOptions.chapters.thirteen);
            }
            else
            {
                o.date_type = CaseSearchOptions.date_types.filed;
                o.chapter.Add(CaseSearchOptions.chapters.seven);
            }

            //set the case type
            o.case_type = new List<CaseSearchOptions.case_types>();
            o.case_type.Add(CaseSearchOptions.case_types.bk);
            
            //Set the start and end dates.
            o.start = DateTime.Parse(_startDate.ToString());
            o.end = DateTime.Parse(_endDate.ToString());

            _result = _bankruptcyParser.FindCases(o);

            if (_result == null)
            {
                _importMessage = "Could Not Find Cases:" + _bankruptcyParser.lastError;
                _importStatus = "FAILURE";
                PacerImportTransactionService.Save(this);
                return false;
            }
            else
            {
                this.BillablePages = _result.billable_pages;
                this.Cost = Decimal.Parse(_result.cost.ToString());
                SearchCriteria = _result.criteria;
                DownloadTimeStamp = DateTime.Now;
                PacerImportTransactionService.Save(this);

                //don't set the status and message yet as we still have to import...
                return true;
            }
            
        }

        public bool Reprocess(bool GeoCodeAddresses)
        {
            //ceck if we've established conection and queried for the data

            if (this.FilePath.Length > 0)
            {
            
                try
                {
                    
                    // Read the file as one string.
                    System.IO.StreamReader myFile = new System.IO.StreamReader(this.FilePath);
                    string _rawData = myFile.ReadToEnd();
                    myFile.Close();

                    if (_rawData == null || _rawData.Length == 0)
                    {
                        _importStatus = "NO DATA";
                        return false;
                    }

                    //delete all imported data but don;t bother deleting bankruptcy case data as it will get written over
                    //if already exists
                    PacerImportDataService.DeleteForTransaction(this.ID);

                    //get the court
                    //Court _court = CourtService.GetByID(this.CourtID);
                    //this.PacerFileFormatID = _format.ID; 

                    PacerFileFormat _format = PacerFileFormatService.GetByID(this.PacerFileFormatID);

                    //on a reprocess we use the current format for the court
                    if (BRGCaseMailServer.PacerImportTransactionService.ProcessManualImportTransaction(this, _format) == true)
                    {
                        //process imported lines
                        int rows = PacerImportTransactionService.ProcessImportedLineItems(this, GeoCodeAddresses);
                        if (_court == null)
                        {
                            _court = CourtService.GetByID(this.CourtID);
                        }
                        if (this._dischargedCases == true)
                        {
                            _court.LastPacerLoadDischargeDate = this.EndDate;
                        }
                        else
                        {
                            _court.LastPacerLoadFileDate = this.EndDate;
                        }
                        
                        CourtService.Save((Court)_court);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    this.ImportMessage = ex.Message;
                    this.ImportStatus = "FAILURE";
                    PacerImportTransactionService.Save(this);
                    return false;
                }
            }
            else
            {
                PacerImportTransactionService.Save(this);
                return false; //end   if (_status == true || _result != null)
            }
        }

        public bool DownloadNewCases(bool GeoCodeAddresses)
        {
            //ceck if we've established conection and queried for the data
            
            bool _status = false;

            //we may be calling Downloadnewcases without the intermediate prompt that QueryCases allows.\
            //if so call it first.
            if (_result == null)
            {
                _status = QueryNewCases();
            }

            if (_status == true || _result != null)
            {
                try
                {
                    string _rawData = _bankruptcyParser.PurchaseCases(_result);
                    if (_rawData == null || _rawData.Length == 0)
                    {
                        _importStatus = "NO DATA";
                        return false;
                    }
                    //_transaction.RawData = "text";

                    //string _rawData = "text";

                    //create the filepath
                    string newPath = System.IO.Path.Combine(ConfigurationManager.AppSettings.Get("PACERFilePath"), _court.FilePrefix);
                    System.IO.Directory.CreateDirectory(newPath);

                    _filePath = newPath + @"\" + _court.FilePrefix + _startDate.ToString("MMddyyyy") + "_" + _endDate.ToString("MMddyyyy") + ".txt";

                    System.IO.File.WriteAllText(_filePath, _rawData);

                    PacerImportTransactionService.Save(this);

                    //if (_court == null)
                    //{
                    //    _court = CourtService.GetByID(this.CourtID);
                    //}

                    // ZipGeoCodeService.UpdateStatsForState(_court.StateCode);

                    //execute the dts package
                    //Load DTSX

                    //Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();
                    //Package package = app.LoadPackage(System.Configuration.ConfigurationManager.AppSettings.Get("PACERImportDTSXFile"), null);

                    ////Specify Excel Connection From DTSX Connection Manager
                    //Debug.WriteLine(package.Connections["SourceConnectionFlatFile"].ConnectionString);
                    //package.Connections["SourceConnectionFlatFile"].ConnectionString = _filePath;

                    //Debug.WriteLine(package.Connections["DestinationConnectionOLEDB"].ConnectionString);
                    //package.Connections["DestinationConnectionOLEDB"].ConnectionString = ConfigurationManager.AppSettings["DTSConnectionString"];

                    ////    //Execute DTSX.
                    //Microsoft.SqlServer.Dts.Runtime.DTSExecResult results = package.Execute();

                    //if (results == DTSExecResult.Success)
                    //{
                    //    //yay!
                    //    BRGCaseMailServer.PacerImportTransactionService.ProcessImportTransaction(this);
                    //}
                    //else
                    //{
                    //    string _message = string.Empty;
                    //    foreach (DtsError _err in package.Errors)
                    //    {
                    //        _message += _err.Description + ".  ";

                    //    }
                    //    _message += "Please see you system adminstrator. ";

                    //    //dts did not work so load the file and parse manually
                    //    _importMessage = "SSIS Import Failed with the error:" + _message + ".  Trying Manaul import...";

                    if (BRGCaseMailServer.PacerImportTransactionService.ProcessManualImportTransaction(this) == true)
                    {
                        //process imported lines
                        int rows = PacerImportTransactionService.ProcessImportedLineItems(this, GeoCodeAddresses);
                        if (_court == null)
                        {
                            _court = CourtService.GetByID(this.CourtID);
                        }
                        if (this._dischargedCases == true)
                        {
                            _court.LastPacerLoadDischargeDate = this.EndDate;
                        }
                        else
                        {
                            _court.LastPacerLoadFileDate = this.EndDate;
                        }

                        CourtService.Save((Court)_court);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _importMessage = ex.Message;
                    _importStatus = "FAILURE";
                    PacerImportTransactionService.Save(this); 
                    return false;
                }
            }
            else
            {
                PacerImportTransactionService.Save(this);
                return false; //end   if (_status == true || _result != null)
            }
        }
        
        /// <summary>
        /// Checks to see if there are prior downloads that overlap the transaction search parameters
        /// </summary>
        public void CheckForPriorOverlappingDownloads()
        {
            List<PacerImportTransaction> _transactions = PacerImportTransactionService.GetOverLappingTransactions(this);
            if (_transactions.Count > 0)
            { 
                throw new Exception("The Date Range for the Court: " + this.CourtName + " overlaps previous SUCCESSFUL import transactions of this type.  You are trying to purchase duplicate data!");
            }
        }
    }
}
