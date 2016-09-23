using System;

using System.Collections;

using System.ComponentModel;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the NCOAImport table.
	/// Author: ATP
	/// Date Created: 10/10/2012 5:09:52 PM
	/// </summary>
	public class NCOAImport : IComparable, INotifyPropertyChanged
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
		public NCOAImport()
		{
			_cASEID = 0;
			_iNPUTFIRST = string.Empty;
			_iNPUTMIDDLE = string.Empty;
			_iNPUTLAST = string.Empty;
			_iNPUTSUFFIX = string.Empty;
			_aDDRESS1 = string.Empty;
			_aDDRESS2 = string.Empty;
			_cITY = string.Empty;
			_sTATE = string.Empty;
			_zIP = string.Empty;
			_nEWADDRESS_FLAG = string.Empty;
			_mRT_FLAG = string.Empty;
			_mAILTO1_ADDRESS = string.Empty;
			_mAILTO2_ADDRESS = string.Empty;
			_mAILTO_CITY = string.Empty;
			_mAILTO_STATE = string.Empty;
			_mAILTO_ZIP = string.Empty;
			_mAIL_ZIP4 = string.Empty;
			_mAILTO_DPBC = string.Empty;
			_mAIL_CARRIER_ROUTE = string.Empty;
			_dPVCONFIRMATION_INDICATOR = string.Empty;
			_nCOA_MATCH_TYPE = string.Empty;
			_nCOA_MOVE_DATE = string.Empty;
			_dELIVERY_CODE = string.Empty;
			_zIP4_UPDATES = string.Empty;
			_mAILABILITY_SCORE = string.Empty;
		}
		public NCOAImport(int CASEID, string INPUTFIRST, string INPUTMIDDLE, string INPUTLAST, string INPUTSUFFIX, string ADDRESS1, string ADDRESS2, string CITY, string STATE, string ZIP, string NEWADDRESS_FLAG, string MRT_FLAG, string MAILTO1_ADDRESS, string MAILTO2_ADDRESS, string MAILTO_CITY, string MAILTO_STATE, string MAILTO_ZIP, string MAIL_ZIP4, string MAILTO_DPBC, string MAIL_CARRIER_ROUTE, string DPVCONFIRMATION_INDICATOR, string NCOA_MATCH_TYPE, string NCOA_MOVE_DATE, string DELIVERY_CODE, string ZIP4_UPDATES, string MAILABILITY_SCORE)		{
			_cASEID = CASEID;
			_iNPUTFIRST = INPUTFIRST;
			_iNPUTMIDDLE = INPUTMIDDLE;
			_iNPUTLAST = INPUTLAST;
			_iNPUTSUFFIX = INPUTSUFFIX;
			_aDDRESS1 = ADDRESS1;
			_aDDRESS2 = ADDRESS2;
			_cITY = CITY;
			_sTATE = STATE;
			_zIP = ZIP;
			_nEWADDRESS_FLAG = NEWADDRESS_FLAG;
			_mRT_FLAG = MRT_FLAG;
			_mAILTO1_ADDRESS = MAILTO1_ADDRESS;
			_mAILTO2_ADDRESS = MAILTO2_ADDRESS;
			_mAILTO_CITY = MAILTO_CITY;
			_mAILTO_STATE = MAILTO_STATE;
			_mAILTO_ZIP = MAILTO_ZIP;
			_mAIL_ZIP4 = MAIL_ZIP4;
			_mAILTO_DPBC = MAILTO_DPBC;
			_mAIL_CARRIER_ROUTE = MAIL_CARRIER_ROUTE;
			_dPVCONFIRMATION_INDICATOR = DPVCONFIRMATION_INDICATOR;
			_nCOA_MATCH_TYPE = NCOA_MATCH_TYPE;
			_nCOA_MOVE_DATE = NCOA_MOVE_DATE;
			_dELIVERY_CODE = DELIVERY_CODE;
			_zIP4_UPDATES = ZIP4_UPDATES;
			_mAILABILITY_SCORE = MAILABILITY_SCORE;
		}
		public NCOAImport Copy()
		{
			NCOAImport _nCOAImport = new NCOAImport();
			_nCOAImport.CASEID = _cASEID;
			_nCOAImport.INPUTFIRST = _iNPUTFIRST;
			_nCOAImport.INPUTMIDDLE = _iNPUTMIDDLE;
			_nCOAImport.INPUTLAST = _iNPUTLAST;
			_nCOAImport.INPUTSUFFIX = _iNPUTSUFFIX;
			_nCOAImport.ADDRESS1 = _aDDRESS1;
			_nCOAImport.ADDRESS2 = _aDDRESS2;
			_nCOAImport.CITY = _cITY;
			_nCOAImport.STATE = _sTATE;
			_nCOAImport.ZIP = _zIP;
			_nCOAImport.NEWADDRESS_FLAG = _nEWADDRESS_FLAG;
			_nCOAImport.MRT_FLAG = _mRT_FLAG;
			_nCOAImport.MAILTO1_ADDRESS = _mAILTO1_ADDRESS;
			_nCOAImport.MAILTO2_ADDRESS = _mAILTO2_ADDRESS;
			_nCOAImport.MAILTO_CITY = _mAILTO_CITY;
			_nCOAImport.MAILTO_STATE = _mAILTO_STATE;
			_nCOAImport.MAILTO_ZIP = _mAILTO_ZIP;
			_nCOAImport.MAIL_ZIP4 = _mAIL_ZIP4;
			_nCOAImport.MAILTO_DPBC = _mAILTO_DPBC;
			_nCOAImport.MAIL_CARRIER_ROUTE = _mAIL_CARRIER_ROUTE;
			_nCOAImport.DPVCONFIRMATION_INDICATOR = _dPVCONFIRMATION_INDICATOR;
			_nCOAImport.NCOA_MATCH_TYPE = _nCOA_MATCH_TYPE;
			_nCOAImport.NCOA_MOVE_DATE = _nCOA_MOVE_DATE;
			_nCOAImport.DELIVERY_CODE = _dELIVERY_CODE;
			_nCOAImport.ZIP4_UPDATES = _zIP4_UPDATES;
			_nCOAImport.MAILABILITY_SCORE = _mAILABILITY_SCORE;
			return _nCOAImport;
		}
		#region "Private Instance Variables"
		private int _cASEID;
		private string _iNPUTFIRST;
		private string _iNPUTMIDDLE;
		private string _iNPUTLAST;
		private string _iNPUTSUFFIX;
		private string _aDDRESS1;
		private string _aDDRESS2;
		private string _cITY;
		private string _sTATE;
		private string _zIP;
		private string _nEWADDRESS_FLAG;
		private string _mRT_FLAG;
		private string _mAILTO1_ADDRESS;
		private string _mAILTO2_ADDRESS;
		private string _mAILTO_CITY;
		private string _mAILTO_STATE;
		private string _mAILTO_ZIP;
		private string _mAIL_ZIP4;
		private string _mAILTO_DPBC;
		private string _mAIL_CARRIER_ROUTE;
		private string _dPVCONFIRMATION_INDICATOR;
		private string _nCOA_MATCH_TYPE;
		private string _nCOA_MOVE_DATE;
		private string _dELIVERY_CODE;
		private string _zIP4_UPDATES;
		private string _mAILABILITY_SCORE;
		#endregion 
		#region "Public Properties"
		public int CASEID
		{
			get
			{
				return _cASEID;
			}
			set
			{
				if (value != _cASEID)
				{
					this._isModified = true;
					_cASEID = value;
					NotifyPropertyChanged("CASEID");
				}
			}
		}

		public string INPUTFIRST
		{
			get
			{
				return _iNPUTFIRST;
			}
			set
			{
				if (value != _iNPUTFIRST)
				{
					this._isModified = true;
					_iNPUTFIRST = value;
					NotifyPropertyChanged("INPUTFIRST");
				}
			}
		}

		public string INPUTMIDDLE
		{
			get
			{
				return _iNPUTMIDDLE;
			}
			set
			{
				if (value != _iNPUTMIDDLE)
				{
					this._isModified = true;
					_iNPUTMIDDLE = value;
					NotifyPropertyChanged("INPUTMIDDLE");
				}
			}
		}

		public string INPUTLAST
		{
			get
			{
				return _iNPUTLAST;
			}
			set
			{
				if (value != _iNPUTLAST)
				{
					this._isModified = true;
					_iNPUTLAST = value;
					NotifyPropertyChanged("INPUTLAST");
				}
			}
		}

		public string INPUTSUFFIX
		{
			get
			{
				return _iNPUTSUFFIX;
			}
			set
			{
				if (value != _iNPUTSUFFIX)
				{
					this._isModified = true;
					_iNPUTSUFFIX = value;
					NotifyPropertyChanged("INPUTSUFFIX");
				}
			}
		}

		public string ADDRESS1
		{
			get
			{
				return _aDDRESS1;
			}
			set
			{
				if (value != _aDDRESS1)
				{
					this._isModified = true;
					_aDDRESS1 = value;
					NotifyPropertyChanged("ADDRESS1");
				}
			}
		}

		public string ADDRESS2
		{
			get
			{
				return _aDDRESS2;
			}
			set
			{
				if (value != _aDDRESS2)
				{
					this._isModified = true;
					_aDDRESS2 = value;
					NotifyPropertyChanged("ADDRESS2");
				}
			}
		}

		public string CITY
		{
			get
			{
				return _cITY;
			}
			set
			{
				if (value != _cITY)
				{
					this._isModified = true;
					_cITY = value;
					NotifyPropertyChanged("CITY");
				}
			}
		}

		public string STATE
		{
			get
			{
				return _sTATE;
			}
			set
			{
				if (value != _sTATE)
				{
					this._isModified = true;
					_sTATE = value;
					NotifyPropertyChanged("STATE");
				}
			}
		}

		public string ZIP
		{
			get
			{
				return _zIP;
			}
			set
			{
				if (value != _zIP)
				{
					this._isModified = true;
					_zIP = value;
					NotifyPropertyChanged("ZIP");
				}
			}
		}

		public string NEWADDRESS_FLAG
		{
			get
			{
				return _nEWADDRESS_FLAG;
			}
			set
			{
				if (value != _nEWADDRESS_FLAG)
				{
					this._isModified = true;
					_nEWADDRESS_FLAG = value;
					NotifyPropertyChanged("NEWADDRESS_FLAG");
				}
			}
		}

		public string MRT_FLAG
		{
			get
			{
				return _mRT_FLAG;
			}
			set
			{
				if (value != _mRT_FLAG)
				{
					this._isModified = true;
					_mRT_FLAG = value;
					NotifyPropertyChanged("MRT_FLAG");
				}
			}
		}

		public string MAILTO1_ADDRESS
		{
			get
			{
				return _mAILTO1_ADDRESS;
			}
			set
			{
				if (value != _mAILTO1_ADDRESS)
				{
					this._isModified = true;
					_mAILTO1_ADDRESS = value;
					NotifyPropertyChanged("MAILTO1_ADDRESS");
				}
			}
		}

		public string MAILTO2_ADDRESS
		{
			get
			{
				return _mAILTO2_ADDRESS;
			}
			set
			{
				if (value != _mAILTO2_ADDRESS)
				{
					this._isModified = true;
					_mAILTO2_ADDRESS = value;
					NotifyPropertyChanged("MAILTO2_ADDRESS");
				}
			}
		}

		public string MAILTO_CITY
		{
			get
			{
				return _mAILTO_CITY;
			}
			set
			{
				if (value != _mAILTO_CITY)
				{
					this._isModified = true;
					_mAILTO_CITY = value;
					NotifyPropertyChanged("MAILTO_CITY");
				}
			}
		}

		public string MAILTO_STATE
		{
			get
			{
				return _mAILTO_STATE;
			}
			set
			{
				if (value != _mAILTO_STATE)
				{
					this._isModified = true;
					_mAILTO_STATE = value;
					NotifyPropertyChanged("MAILTO_STATE");
				}
			}
		}

		public string MAILTO_ZIP
		{
			get
			{
				return _mAILTO_ZIP;
			}
			set
			{
				if (value != _mAILTO_ZIP)
				{
					this._isModified = true;
					_mAILTO_ZIP = value;
					NotifyPropertyChanged("MAILTO_ZIP");
				}
			}
		}

		public string MAIL_ZIP4
		{
			get
			{
				return _mAIL_ZIP4;
			}
			set
			{
				if (value != _mAIL_ZIP4)
				{
					this._isModified = true;
					_mAIL_ZIP4 = value;
					NotifyPropertyChanged("MAIL_ZIP4");
				}
			}
		}

		public string MAILTO_DPBC
		{
			get
			{
				return _mAILTO_DPBC;
			}
			set
			{
				if (value != _mAILTO_DPBC)
				{
					this._isModified = true;
					_mAILTO_DPBC = value;
					NotifyPropertyChanged("MAILTO_DPBC");
				}
			}
		}

		public string MAIL_CARRIER_ROUTE
		{
			get
			{
				return _mAIL_CARRIER_ROUTE;
			}
			set
			{
				if (value != _mAIL_CARRIER_ROUTE)
				{
					this._isModified = true;
					_mAIL_CARRIER_ROUTE = value;
					NotifyPropertyChanged("MAIL_CARRIER_ROUTE");
				}
			}
		}

		public string DPVCONFIRMATION_INDICATOR
		{
			get
			{
				return _dPVCONFIRMATION_INDICATOR;
			}
			set
			{
				if (value != _dPVCONFIRMATION_INDICATOR)
				{
					this._isModified = true;
					_dPVCONFIRMATION_INDICATOR = value;
					NotifyPropertyChanged("DPVCONFIRMATION_INDICATOR");
				}
			}
		}

		public string NCOA_MATCH_TYPE
		{
			get
			{
				return _nCOA_MATCH_TYPE;
			}
			set
			{
				if (value != _nCOA_MATCH_TYPE)
				{
					this._isModified = true;
					_nCOA_MATCH_TYPE = value;
					NotifyPropertyChanged("NCOA_MATCH_TYPE");
				}
			}
		}

		public string NCOA_MOVE_DATE
		{
			get
			{
				return _nCOA_MOVE_DATE;
			}
			set
			{
				if (value != _nCOA_MOVE_DATE)
				{
					this._isModified = true;
					_nCOA_MOVE_DATE = value;
					NotifyPropertyChanged("NCOA_MOVE_DATE");
				}
			}
		}

		public string DELIVERY_CODE
		{
			get
			{
				return _dELIVERY_CODE;
			}
			set
			{
				if (value != _dELIVERY_CODE)
				{
					this._isModified = true;
					_dELIVERY_CODE = value;
					NotifyPropertyChanged("DELIVERY_CODE");
				}
			}
		}

		public string ZIP4_UPDATES
		{
			get
			{
				return _zIP4_UPDATES;
			}
			set
			{
				if (value != _zIP4_UPDATES)
				{
					this._isModified = true;
					_zIP4_UPDATES = value;
					NotifyPropertyChanged("ZIP4_UPDATES");
				}
			}
		}

		public string MAILABILITY_SCORE
		{
			get
			{
				return _mAILABILITY_SCORE;
			}
			set
			{
				if (value != _mAILABILITY_SCORE)
				{
					this._isModified = true;
					_mAILABILITY_SCORE = value;
					NotifyPropertyChanged("MAILABILITY_SCORE");
				}
			}
		}

		#endregion 
		public int CompareTo(Object objNCOAImport)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.CASEID.CompareTo(((NCOAImport)objNCOAImport).CASEID);
		}
	}
}
