using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the PacerFileFormatField table.
	/// Author: Jonathan Shaw
	/// Date Created: 9/26/2011 11:48:49 AM
	/// </summary>
	public class PacerFileFormatField : IComparable
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
		public PacerFileFormatField()
		{
			_iD = 0;
			_pacerFileFormatID = 0;
			_pacerFieldID = 0;
			_fieldOrder = 0;
			_pacerFileVersion = string.Empty;
			_pacerFieldName = string.Empty;
		}
		public PacerFileFormatField(int ID, int PacerFileFormatID, int PacerFieldID, int FieldOrder, string PacerFileVersion, string PacerFieldName)		{
			_iD = ID;
			_pacerFileFormatID = PacerFileFormatID;
			_pacerFieldID = PacerFieldID;
			_fieldOrder = FieldOrder;
			_pacerFileVersion = PacerFileVersion;
			_pacerFieldName = PacerFieldName;
		}
		public PacerFileFormatField Copy()
		{
			PacerFileFormatField _pacerFileFormatField = new PacerFileFormatField();
			_pacerFileFormatField.ID = _iD;
			_pacerFileFormatField.PacerFileFormatID = _pacerFileFormatID;
			_pacerFileFormatField.PacerFieldID = _pacerFieldID;
			_pacerFileFormatField.FieldOrder = _fieldOrder;
			_pacerFileFormatField.PacerFileVersion = _pacerFileVersion;
			_pacerFileFormatField.PacerFieldName = _pacerFieldName;
			return _pacerFileFormatField;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _pacerFileFormatID;
		private int _pacerFieldID;
		private int _fieldOrder;
		private string _pacerFileVersion;
		private string _pacerFieldName;
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

		public int PacerFieldID
		{
			get
			{
				return _pacerFieldID;
			}
			set
			{
				if (value != _pacerFieldID)
				{
					this._isModified = true;
					_pacerFieldID = value;
				}
			}
		}

		public int FieldOrder
		{
			get
			{
				return _fieldOrder;
			}
			set
			{
				if (value != _fieldOrder)
				{
					this._isModified = true;
					_fieldOrder = value;
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

		public string PacerFieldName
		{
			get
			{
				return _pacerFieldName;
			}
			set
			{
				if (value != _pacerFieldName)
				{
					this._isModified = true;
					_pacerFieldName = value;
				}
			}
		}

		#endregion 
		public int CompareTo(Object objPacerFileFormatField)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((PacerFileFormatField)objPacerFileFormatField).ID);
		}
	}
}
