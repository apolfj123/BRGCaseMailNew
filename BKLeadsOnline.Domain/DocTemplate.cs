using System;
using System.Collections;
using System.ComponentModel;
using Aspose.Words;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words.Reporting;
using System.IO;
using System.Configuration;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the DocTemplate table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2013 1:11:27 PM
	/// </summary>
	public class DocTemplate : IComparable, INotifyPropertyChanged
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

        public DocTemplate()
		{
			_iD = 0;
			_dealerID = 0;
			_letterID = 0;
			_displayName = string.Empty;
			_uploadDate = System.DateTime.MinValue;
			//_wordDoc = 0;
			_wordContentType = string.Empty;
			_wordFileSize = 0;
			//_largePic = 0;
			_largePicContentType = string.Empty;
			_largePicSize = 0;
			//_thumbnailPic = 0;
			_thumbnailPicContentType = string.Empty;
			_thumbnailPicSize = 0;
			_color = false;
			_grayscale = false;
			_textOnly = false;
			_useDealerLogo = false;
			_secondNotice = false;
			_comment = string.Empty;
			_active = false;
			_dealerName = string.Empty;
			_letterName = string.Empty;
            _examplePdfDoc = null;
            _examplePdfDoc = null;
		}
		public DocTemplate(int ID, int DealerID, int LetterID, string DisplayName, DateTime UploadDate, byte[]  WordDoc, string WordContentType, int WordFileSize, byte[]  LargePic, string LargePicContentType, int LargePicSize, byte[]  ThumbnailPic, string ThumbnailPicContentType, int ThumbnailPicSize, bool Color, bool Grayscale, bool TextOnly, bool UseDealerLogo, bool SecondNotice, string Comment, bool Active, string DealerName, string LetterName)		{
			_iD = ID;
			_dealerID = DealerID;
			_letterID = LetterID;
			_displayName = DisplayName;
			_uploadDate = UploadDate;
			_wordDoc = WordDoc;
			_wordContentType = WordContentType;
			_wordFileSize = WordFileSize;
			_largePic = LargePic;
			_largePicContentType = LargePicContentType;
			_largePicSize = LargePicSize;
			_thumbnailPic = ThumbnailPic;
			_thumbnailPicContentType = ThumbnailPicContentType;
			_thumbnailPicSize = ThumbnailPicSize;
			_color = Color;
			_grayscale = Grayscale;
			_textOnly = TextOnly;
			_useDealerLogo = UseDealerLogo;
			_secondNotice = SecondNotice;
			_comment = Comment;
			_active = Active;
			_dealerName = DealerName;
			_letterName = LetterName;
		}
		public DocTemplate Copy()
		{
			DocTemplate _docTemplate = new DocTemplate();
			_docTemplate.ID = _iD;
			_docTemplate.DealerID = _dealerID;
			_docTemplate.LetterID = _letterID;
			_docTemplate.DisplayName = _displayName;
			_docTemplate.UploadDate = _uploadDate;
			_docTemplate.WordDoc = _wordDoc;
			_docTemplate.WordContentType = _wordContentType;
			_docTemplate.WordFileSize = _wordFileSize;
			_docTemplate.LargePic = _largePic;
			_docTemplate.LargePicContentType = _largePicContentType;
			_docTemplate.LargePicSize = _largePicSize;
			_docTemplate.ThumbnailPic = _thumbnailPic;
			_docTemplate.ThumbnailPicContentType = _thumbnailPicContentType;
			_docTemplate.ThumbnailPicSize = _thumbnailPicSize;
			_docTemplate.Color = _color;
			_docTemplate.Grayscale = _grayscale;
			_docTemplate.TextOnly = _textOnly;
			_docTemplate.UseDealerLogo = _useDealerLogo;
			_docTemplate.SecondNotice = _secondNotice;
			_docTemplate.Comment = _comment;
			_docTemplate.Active = _active;
			_docTemplate.DealerName = _dealerName;
			_docTemplate.LetterName = _letterName;
			return _docTemplate;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _dealerID;
		private int _letterID;
		private string _displayName;
		private DateTime _uploadDate;
		private byte[] _wordDoc;
		private string _wordContentType;
		private int _wordFileSize;
		private byte[] _largePic;
		private string _largePicContentType;
		private int _largePicSize;
		private byte[] _thumbnailPic;
		private string _thumbnailPicContentType;
		private int _thumbnailPicSize;
		private bool _color;
		private bool _grayscale;
		private bool _textOnly;
		private bool _useDealerLogo;
		private bool _secondNotice;
		private string _comment;
		private bool _active;
		private string _dealerName;
		private string _letterName;
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

		public int LetterID
		{
			get
			{
				return _letterID;
			}
			set
			{
				if (value != _letterID)
				{
					this._isModified = true;
					_letterID = value;
					NotifyPropertyChanged("LetterID");
				}
			}
		}

		public string DisplayName
		{
			get
			{
				return _displayName;
			}
			set
			{
				if (value != _displayName)
				{
					this._isModified = true;
					_displayName = value;
					NotifyPropertyChanged("DisplayName");
				}
			}
		}

        public string DisplayNamePDF
        {
            get
            {
                return _displayName.Substring(0, _displayName.Length-4) + ".pdf";
            }
        }

		public DateTime UploadDate
		{
			get
			{
				return _uploadDate;
			}
			set
			{
				if (value != _uploadDate)
				{
					this._isModified = true;
					_uploadDate = value;
					NotifyPropertyChanged("UploadDate");
				}
			}
		}

		public byte[] WordDoc
		{
			get
			{
				return _wordDoc;
			}
			set
			{
				if (value != _wordDoc)
				{
					this._isModified = true;
					_wordDoc = value;
					NotifyPropertyChanged("WordDoc");
				}
			}
		}

		public string WordContentType
		{
			get
			{
				return _wordContentType;
			}
			set
			{
				if (value != _wordContentType)
				{
					this._isModified = true;
					_wordContentType = value;
					NotifyPropertyChanged("WordContentType");
				}
			}
		}

		public int WordFileSize
		{
			get
			{
				return _wordFileSize;
			}
			set
			{
				if (value != _wordFileSize)
				{
					this._isModified = true;
					_wordFileSize = value;
					NotifyPropertyChanged("WordFileSize");
				}
			}
		}

		public byte[] LargePic
		{
			get
			{
				return _largePic;
			}
			set
			{
				if (value != _largePic)
				{
					this._isModified = true;
					_largePic = value;
					NotifyPropertyChanged("LargePic");
				}
			}
		}

		public string LargePicContentType
		{
			get
			{
				return _largePicContentType;
			}
			set
			{
				if (value != _largePicContentType)
				{
					this._isModified = true;
					_largePicContentType = value;
					NotifyPropertyChanged("LargePicContentType");
				}
			}
		}

		public int LargePicSize
		{
			get
			{
				return _largePicSize;
			}
			set
			{
				if (value != _largePicSize)
				{
					this._isModified = true;
					_largePicSize = value;
					NotifyPropertyChanged("LargePicSize");
				}
			}
		}

		public byte[] ThumbnailPic
		{
			get
			{
				return _thumbnailPic;
			}
			set
			{
				if (value != _thumbnailPic)
				{
					this._isModified = true;
					_thumbnailPic = value;
					NotifyPropertyChanged("ThumbnailPic");
				}
			}
		}

		public string ThumbnailPicContentType
		{
			get
			{
				return _thumbnailPicContentType;
			}
			set
			{
				if (value != _thumbnailPicContentType)
				{
					this._isModified = true;
					_thumbnailPicContentType = value;
					NotifyPropertyChanged("ThumbnailPicContentType");
				}
			}
		}

		public int ThumbnailPicSize
		{
			get
			{
				return _thumbnailPicSize;
			}
			set
			{
				if (value != _thumbnailPicSize)
				{
					this._isModified = true;
					_thumbnailPicSize = value;
					NotifyPropertyChanged("ThumbnailPicSize");
				}
			}
		}

		public bool Color
		{
			get
			{
				return _color;
			}
			set
			{
				if (value != _color)
				{
					this._isModified = true;
					_color = value;
					NotifyPropertyChanged("Color");
				}
			}
		}

		public bool Grayscale
		{
			get
			{
				return _grayscale;
			}
			set
			{
				if (value != _grayscale)
				{
					this._isModified = true;
					_grayscale = value;
					NotifyPropertyChanged("Grayscale");
				}
			}
		}

		public bool TextOnly
		{
			get
			{
				return _textOnly;
			}
			set
			{
				if (value != _textOnly)
				{
					this._isModified = true;
					_textOnly = value;
					NotifyPropertyChanged("TextOnly");
				}
			}
		}

		public bool UseDealerLogo
		{
			get
			{
				return _useDealerLogo;
			}
			set
			{
				if (value != _useDealerLogo)
				{
					this._isModified = true;
					_useDealerLogo = value;
					NotifyPropertyChanged("UseDealerLogo");
				}
			}
		}

		public bool SecondNotice
		{
			get
			{
				return _secondNotice;
			}
			set
			{
				if (value != _secondNotice)
				{
					this._isModified = true;
					_secondNotice = value;
					NotifyPropertyChanged("SecondNotice");
				}
			}
		}

		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				if (value != _comment)
				{
					this._isModified = true;
					_comment = value;
					NotifyPropertyChanged("Comment");
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

		public string LetterName
		{
			get
			{
				return _letterName;
			}
			set
			{
				if (value != _letterName)
				{
					this._isModified = true;
					_letterName = value;
					NotifyPropertyChanged("LetterName");
				}
			}
		}

		#endregion 
 
        #region Additional (derived) properties
        private byte[] _examplePdfDoc;
        private byte[] _exampleWordDoc;
        private byte[] _mailngListDoc;

        //if the Doc Template is dealer specific (DealerID in DB record) then we use the DealerID to load the _mergeDealer in any merges.
        //if the Doc Template is NOT dealer specific (DealerID NOT in DB record) we look for this _mergeDealer to be set specifically before any merges.
        //if the merge dealer is NOT set we use use an exmample dealer.
        private Dealer _mergeDealer;
        public Dealer MergeDealer
        {
            get 
            {
                return _mergeDealer;
            }
            set 
            {
                _mergeDealer = value;
            }
        }

        public string ExamplePDFDocDisplayName
        {
            get 
            { 
                if (_displayName.Length > 0)
                {
                    return (_displayName.Substring(0, _displayName.IndexOf(".") + 1) + "pdf");
                }
                else
                {
                    return "example.pdf";
                }
            }
        }
        public string ExampleWordDocDisplayName
        {
            get
            {
                if (_displayName.Length > 0)
                {
                    return (_displayName.Substring(0, _displayName.IndexOf(".") + 1) + "doc");
                }
                else
                {
                    return "example.doc";
                }
            }
        }

        public string NameAndComment
        {
            get 
            {
                if (_displayName.Length > 0 && _comment.Length > 0)
                {
                    return (_displayName.Substring(0, _displayName.IndexOf(".") + 1) + ": " + _comment);
                }
                else
                {
                    return _displayName.Substring(0, _displayName.IndexOf("."));
                }
            }
        }
        
        /// <summary>
        /// Converts the Word Document byte array intoa pdf document byte array
        /// </summary>

        public byte[] ExamplePDFDoc
        {
            get
            {
                if (WordDoc == null || WordFileSize == 0)
                {
                    return null;
                }
                else if (_wordDoc != null && _wordFileSize > 0 && _examplePdfDoc == null)
                {
                    //use ASPOSE to create PDF doc from Word byte array with no merging
                    //do an on the fly conversion to PDF
                    MemoryStream inStream = new MemoryStream(_wordDoc);

                    //do we have a dealerID?
                    if (_dealerID > 0)
                    {
                        _mergeDealer = DealerService.GetByID(_dealerID);
                    }
                    else if (_mergeDealer == null)
                    {
                        //we're trying to do a merge and have no dealer so create an example
                        _mergeDealer = new Dealer() { DealerName = "John Doe Automobiles", AddressLine1 = "1001 Highway 1.", City = "Anytown", State = "KS", Zip = "10001", Phone = "(888) 888-8888", DealerPrimaryContactName="Joe Salesman", DealerURL="www.johndoeauto.com" };
                    }
                    else 
                    { 
                        //do nothing hte _mergedealer has been set on the class externally
                    }

                    //we also need a bankruptcy case or actully a list of cases with one item
                    List<BankruptcyCase> _cases = new List<BankruptcyCase>();
                    _cases.Add(new BankruptcyCase(){ FirstName="Jane", LastName="Doe", AddrLine1="1 Main St.", City="Anytown", StateCode="KS", ZipCodeString="1001"});

                    // Load the stream into a new document object.
                    Aspose.Words.Document _asposeDoc = new Document(inStream);

                    //Set up the event handler for image fields.
                    _asposeDoc.MailMerge.FieldMergingCallback = new HandleMergeImageFieldFromBlob(_mergeDealer);

                    //Example of a very simple mail merge to populate fields only once from an array of values.


                    //does the dealer have a legal disclaimer?
                    string _legalDisclaimer;
                    if (_mergeDealer.LegalDisclaimer.Length > 0)
                    {
                        _legalDisclaimer = _mergeDealer.LegalDisclaimer;
                    }
                    else
                    {
                        _legalDisclaimer = _mergeDealer.DealerName +  ConfigurationManager.AppSettings["LegalDisclaimer"];
                    }

                    _asposeDoc.MailMerge.Execute(
                        new string[] { "DealerName", "DealerFullAddress", "DealerPhoneNumber", "DealerPrimaryContactName", "DealerURL", "LegalDisclaimer", "DealerAddress", "DealerCity", "DealerState", "DealerZipCode", "MailingDate" },
                        new object[] { _mergeDealer.DealerName, _mergeDealer.DealerFullAddress, _mergeDealer.Phone, _mergeDealer.DealerPrimaryContactName, _mergeDealer.DealerURL, _legalDisclaimer, _mergeDealer.AddressLine1, _mergeDealer.City, _mergeDealer.State, _mergeDealer.Zip, DateTime.Now.ToShortDateString() });

                    //det the dealer logo if the dealer is form the DB
                    if (_mergeDealer.ID > 0)
                    {
                        DealerService.GetLogo(_mergeDealer);
                    }

                    BankruptcyCaseMailMergeDataSource _source = new BankruptcyCaseMailMergeDataSource(_cases);

                    _asposeDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;

                    //execute the mail merge
                    _asposeDoc.MailMerge.Execute(_source);

                    //send back a converted PDF
                    MemoryStream outStream = new MemoryStream();
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Pdf);
                    _examplePdfDoc = outStream.ToArray();

                    return _examplePdfDoc;
                }
                else
                {
                    return _examplePdfDoc;
                }
            }
        }

        public byte[] ExampleWordDoc
        {
            get
            {
                if (WordDoc == null || WordFileSize == 0)
                {
                    return null;
                }
                else if (_wordDoc != null && _wordFileSize > 0 && _exampleWordDoc == null)
                {
                    //use ASPOSE to create Word doc from Word Template byte array with no merging
                    MemoryStream inStream = new MemoryStream(_wordDoc);

                    //do we have a dealerID?
                    if (_dealerID > 0)
                    {
                        _mergeDealer = DealerService.GetByID(_dealerID);
                    }
                    else if (_mergeDealer == null)
                    {
                        //we're trying to do a merge and have no dealer so create an example
                        _mergeDealer = new Dealer() { DealerName = "John Doe Automobiles", AddressLine1 = "1001 Highway 1.", City = "Anytown", State = "KS", Zip = "10001", Phone = "(888) 888-8888", DealerPrimaryContactName = "Joe Salesman", DealerURL = "www.johndoeauto.com" };
                    }
                    else
                    {
                        //do nothing hte _mergedealer has been set on the class externally
                    }

                    //we also need a bankruptcy case or actully a list of cases with one item
                    List<BankruptcyCase> _cases = new List<BankruptcyCase>();
                    _cases.Add(new BankruptcyCase() { FirstName = "Jane", LastName = "Doe", AddrLine1 = "1 Main St.", City = "Anytown", StateCode = "KS", ZipCodeString = "1001" });

                    // Load the stream into a new document object.
                    Aspose.Words.Document _asposeDoc = new Document(inStream);

                    //Set up the event handler for image fields.
                    _asposeDoc.MailMerge.FieldMergingCallback = new HandleMergeImageFieldFromBlob(_mergeDealer);

                    //Example of a very simple mail merge to populate fields only once from an array of values.
                    //does the dealer have a legal disclaimer?
                    
                    string _legalDisclaimer;
                    if (_mergeDealer.LegalDisclaimer.Length > 0)
                    {
                        _legalDisclaimer = _mergeDealer.LegalDisclaimer;
                    }
                    else
                    {
                        _legalDisclaimer = _mergeDealer.DealerName + ConfigurationManager.AppSettings["LegalDisclaimer"];
                    }

                    _asposeDoc.MailMerge.Execute(
                        new string[] { "DealerName", "DealerFullAddress", "DealerPhoneNumber", "DealerPrimaryContactName", "DealerURL", "LegalDisclaimer", "DealerAddress", "DealerCity", "DealerState", "DealerZipCode", "MailingDate" },
                        new object[] { _mergeDealer.DealerName, _mergeDealer.DealerFullAddress, _mergeDealer.Phone, _mergeDealer.DealerPrimaryContactName, _mergeDealer.DealerURL, _legalDisclaimer, _mergeDealer.AddressLine1, _mergeDealer.City, _mergeDealer.State, _mergeDealer.Zip, DateTime.Now.ToShortDateString() });

                    //det the dealer logo if the dealer is form the DB
                    if (_mergeDealer.ID > 0)
                    {
                        DealerService.GetLogo(_mergeDealer);
                    }

                    BankruptcyCaseMailMergeDataSource _source = new BankruptcyCaseMailMergeDataSource(_cases);
                    _asposeDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;

                    //execute the mail merge
                    _asposeDoc.MailMerge.Execute(_source);

                    //send back a converted PDF
                    MemoryStream outStream = new MemoryStream();
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Doc);
                    _exampleWordDoc = outStream.ToArray();

                    return _exampleWordDoc;
                }
                else
                {
                    return _exampleWordDoc;
                }
            }
        }

        public byte[] MailingListDoc
        {
            get 
            {
                return _mailngListDoc;
            }
        }

        #endregion

        /// <summary>
        /// If no paramters are passed other than the dealer then we get the new leads since last login.
        /// If this is the first login or default we get the most recent default number of mailings
        /// </summary>
        /// <param name="_dealer"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="NumberOfMailings"></param>
        /// <returns></returns>
        
        public byte[] CreateMailingList(Dealer _dealer, DateTime StartDate, DateTime EndDate, int NumberOfMailings, bool UseWord)
        {
            DateTime _startDate = DateTime.MinValue;
            DateTime _endDate = DateTime.MinValue;
            int _numberOfMailings;
            _mergeDealer = _dealer;

            if (WordDoc == null || WordFileSize == 0)
            {
                return null;
            }
                //the options are
            else //1.  Start and End Date are null requested NumberOfMailings = 0   - get most recent
            {
                if ((StartDate == null || StartDate == DateTime.MinValue ) && NumberOfMailings == 0)
                {
                    //get all data since the last mailinglist
                    List<DealerMailingList> _lists = DealerMailingListService.GetForDealer(_dealer.ID);
                    if (_lists.Count > 0)
                    {
                        //_startDate = DateTime.Parse(_lists[0].EndFilterDate.ToString("MM/dd/yyyy")).AddDays(1);
                        _startDate = DealerMailingListService.GetMaxDischargedDateForDealerMailingList(_dealer.ID);
                    }
                    else
                    {
                        _startDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")).AddYears(-2);
                    }
                }
                else if (StartDate > DateTime.MinValue)
                {
                    _startDate = DateTime.Parse(StartDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    _startDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")).AddYears(-2);
                }

                if (EndDate == null || EndDate == DateTime.MinValue)
                {
                    _endDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else
                {
                    _endDate = DateTime.Parse(EndDate.ToString("MM/dd/yyyy"));
                }

                if (NumberOfMailings == 0)
                {
                    //use the default number from the dealer
                    _numberOfMailings = _dealer.NumberOfMailers;
                }
                else
                {
                    _numberOfMailings = NumberOfMailings;
                }

                //use ASPOSE to create PDF doc from Word byte array with no merging
                //do an on the fly conversion to PDF
                MemoryStream inStream = new MemoryStream(_wordDoc);

                //we also need a bankruptcy case or actully a list of cases with one item
                List<BankruptcyCase> _cases = BankruptcyCaseService.getMailingList(_startDate, _endDate, _mergeDealer.ID, _numberOfMailings);

                if (_cases.Count == 0)
                {
                    return null;
                }

                //_cases.Add(new BankruptcyCase() { FirstName = "Jane", LastName = "Doe", AddrLine1 = "1 Main St.", City = "Anytown", StateCode = "KS", ZipCodeString = "1001" });

                // Load the stream into a new document object.
                Aspose.Words.Document _asposeDoc = new Document(inStream);

                //Set up the event handler for image fields.
                _asposeDoc.MailMerge.FieldMergingCallback = new HandleMergeImageFieldFromBlob(_mergeDealer);

                //Example of a very simple mail merge to populate fields only once from an array of values.
                //does the dealer have a legal disclaimer?

                string _legalDisclaimer;
                if (_mergeDealer.LegalDisclaimer.Length > 0)
                {
                    _legalDisclaimer = _mergeDealer.LegalDisclaimer;
                }
                else
                {
                    _legalDisclaimer = _mergeDealer.DealerName + ConfigurationManager.AppSettings["LegalDisclaimer"];
                }

                _asposeDoc.MailMerge.Execute(
                    new string[] { "DealerName", "DealerFullAddress", "DealerPhoneNumber", "DealerPrimaryContactName", "DealerURL", "LegalDisclaimer", "DealerAddress", "DealerCity", "DealerState",  "DealerZipCode", "MailingDate" },
                    new object[] { _mergeDealer.DealerName, _mergeDealer.DealerFullAddress, _mergeDealer.Phone, _mergeDealer.DealerPrimaryContactName, _mergeDealer.DealerURL, _legalDisclaimer, _mergeDealer.AddressLine1, _mergeDealer.City, _mergeDealer.State, _mergeDealer.Zip, DateTime.Now.ToShortDateString() });

                //det the dealer logo if the dealer is form the DB
                if (_mergeDealer.ID > 0)
                {
                    DealerService.GetLogo(_mergeDealer);
                }

                BankruptcyCaseMailMergeDataSource _source = new BankruptcyCaseMailMergeDataSource(_cases);
                _asposeDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;

                //execute the mail merge
                _asposeDoc.MailMerge.Execute(_source);

                //send back a converted PDF
                MemoryStream outStream = new MemoryStream();
                if (UseWord)
                {
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Doc);
                }
                else 
                {
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Pdf);
                }
                _mailngListDoc = outStream.ToArray();

                //save the dealer mailinglist in the database
                DealerMailingList _list = new DealerMailingList() { CreationDate = DateTime.Now, DealerID = _mergeDealer.ID, DocTemplateID = this.ID, StartFilterDate = _startDate, EndFilterDate = _endDate, NumberCases = _cases.Count };
                DealerMailingListService.Save(_list);
                foreach (BankruptcyCase _case in _cases)
                {
                    DealerMailingListCaseService.Insert(new DealerMailingListCase() { BankruptcyCaseID = _case.ID, DealerID = _mergeDealer.ID, DealerMailingListID = _list.ID });
                }

                return _mailngListDoc;
            }            
        }
        public byte[] CreateMailingList(int MailingListID, bool UseWord)
        {
            DealerMailingList _originalMailingList;
            Dealer _mergeDealer;

            _originalMailingList = DealerMailingListService.GetByID(MailingListID);
            _mergeDealer = DealerService.GetByID(_originalMailingList.DealerID);

            if (WordDoc == null || WordFileSize == 0)
            {
                return null;
            }
            else //if (_wordDoc != null && _wordFileSize > 0 )
            {

                //use ASPOSE to create PDF doc from Word byte array with no merging
                //do an on the fly conversion to PDF
                MemoryStream inStream = new MemoryStream(_wordDoc);

                //we also need a bankruptcy case or actully a list of cases with one item
                List<BankruptcyCase> _cases = BankruptcyCaseService.getMailingList(_originalMailingList.ID, _mergeDealer.ID, false);
                //_cases.Add(new BankruptcyCase() { FirstName = "Jane", LastName = "Doe", AddrLine1 = "1 Main St.", City = "Anytown", StateCode = "KS", ZipCodeString = "1001" });

                // Load the stream into a new document object.
                Aspose.Words.Document _asposeDoc = new Document(inStream);

                //Set up the event handler for image fields.
                _asposeDoc.MailMerge.FieldMergingCallback = new HandleMergeImageFieldFromBlob(_mergeDealer);

                //Example of a very simple mail merge to populate fields only once from an array of values.
                //does the dealer have a legal disclaimer?

                string _legalDisclaimer;
                if (_mergeDealer.LegalDisclaimer.Length > 0)
                {
                    _legalDisclaimer = _mergeDealer.LegalDisclaimer;
                }
                else
                {
                    _legalDisclaimer = _mergeDealer.DealerName + ConfigurationManager.AppSettings["LegalDisclaimer"];
                }

                _asposeDoc.MailMerge.Execute(
                    new string[] { "DealerName", "DealerFullAddress", "DealerPhoneNumber", "DealerPrimaryContactName", "DealerURL", "LegalDisclaimer", "DealerAddress", "DealerCity", "DealerState", "DealerZipCode", "MailingDate" },
                    new object[] { _mergeDealer.DealerName, _mergeDealer.DealerFullAddress, _mergeDealer.Phone, _mergeDealer.DealerPrimaryContactName, _mergeDealer.DealerURL, _legalDisclaimer, _mergeDealer.AddressLine1, _mergeDealer.City, _mergeDealer.State, _mergeDealer.Zip, DateTime.Now.ToShortDateString() });

                //det the dealer logo if the dealer is form the DB
                if (_mergeDealer.ID > 0)
                {
                    DealerService.GetLogo(_mergeDealer);
                }

                BankruptcyCaseMailMergeDataSource _source = new BankruptcyCaseMailMergeDataSource(_cases);
                _asposeDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;

                //execute the mail merge
                _asposeDoc.MailMerge.Execute(_source);

                //send back a converted PDF
                MemoryStream outStream = new MemoryStream();
                //_asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Doc);
                if (UseWord)
                {
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Doc);
                }
                else 
                {
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Pdf);
                }

                _mailngListDoc = outStream.ToArray();
                return _mailngListDoc;
            }
        }
        public int CompareTo(Object objDocTemplate)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((DocTemplate)objDocTemplate).ID);
		}
    }

    /// <summary>
    /// A custom mail merge data source that you implement to allow Aspose.Words 
    /// to mail merge data from your BankruptcyCase objects into Microsoft Word documents.
    /// </summary>
    
    internal class BankruptcyCaseMailMergeDataSource : IMailMergeDataSource
    {
        public BankruptcyCaseMailMergeDataSource(List<BankruptcyCase> BankruptcyCases)
        {
            _lstBankruptcyCases = BankruptcyCases;

            // When the data source is initialized, it must be positioned before the first record.
            mRecordIndex = -1;
        }

        /// <summary>
        /// The name of the data source. Used by Aspose.Words only when executing mail merge with repeatable regions.
        /// </summary>
        public string TableName
        {
            get { return "BankruptcyCase"; }
        }

        /// <summary>
        /// Aspose.Words calls this method to get a value for every data field.
        /// </summary>
        public bool GetValue(string fieldName, out object fieldValue)
        {
            switch (fieldName)
            {
                case "FullName":
                    fieldValue = _lstBankruptcyCases[mRecordIndex].LeadFullName;
                    return true;
                case "Address":
                    fieldValue = _lstBankruptcyCases[mRecordIndex].LeadAddress;
                    return true;
                case "City":
                    fieldValue = _lstBankruptcyCases[mRecordIndex].City;
                    return true;
                case "StateCode":
                    fieldValue = _lstBankruptcyCases[mRecordIndex].StateCode;
                    return true;
                case "Zip":
                    fieldValue = _lstBankruptcyCases[mRecordIndex].ZipCodeString;
                    return true;
                default:
                    // A field with this name was not found, 
                    // return false to the Aspose.Words mail merge engine.
                    fieldValue = null;
                    return false;
            }
        }

        /// <summary>
        /// A standard implementation for moving to a next record in a collection.
        /// </summary>
        public bool MoveNext()
        {
            if (!IsEof)
                mRecordIndex++;

            return (!IsEof);
        }

        public IMailMergeDataSource GetChildDataSource(string tableName)
        {
            return null;
        }

        private bool IsEof
        {
            get { return (mRecordIndex >= _lstBankruptcyCases.Count); }
        }

        private readonly List<BankruptcyCase> _lstBankruptcyCases;
        private int mRecordIndex;
    }

    internal class HandleMergeImageFieldFromBlob : IFieldMergingCallback
    {
        Dealer _dealer;
        public HandleMergeImageFieldFromBlob(Dealer dealer)
        {
            _dealer = dealer;
        }
        void IFieldMergingCallback.FieldMerging(FieldMergingArgs args)
        {
            // Do nothing.
        }

        /// <summary>
        /// This is called when mail merge engine encounters Image:XXX merge field in the document.
        /// You have a chance to return an Image object, file name or a stream that contains the image.
        /// </summary>
        void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs e)
        {
            if (_dealer.DealerLogo != null)
            {
                // The field value is a byte array, just cast it and create a stream on it.
                MemoryStream imageStream = new MemoryStream(_dealer.DealerLogo);
                // Now the mail merge engine will retrieve the image from the stream.
                e.ImageStream = imageStream;
            }
        }
    }

}
