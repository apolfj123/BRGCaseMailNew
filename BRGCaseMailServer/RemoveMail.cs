using System;

using System.Collections;

namespace BRGCaseMailServer
{
	/// <summary>
	/// A Class representing a single row in the RemoveMail table.
	/// Author: Jonathan Shaw
	/// Date Created: 2/16/2012 2:09:58 PM
	/// </summary>
	public class RemoveMail : IComparable
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
		public RemoveMail()
		{
			_iD = 0;
			_indexOnMailServer = 0;
			_msgDateTime = System.DateTime.MinValue;
			_toAddress = string.Empty;
			_fromAddress = string.Empty;
			_subject = string.Empty;
			_body = string.Empty;
			_processed = false;
			_foundAndRemoved = false;
		}
		public RemoveMail(int ID, int IndexOnMailServer, DateTime MsgDateTime, string ToAddress, string FromAddress, string Subject, string Body, bool Processed, bool FoundAndRemoved)		{
			_iD = ID;
			_indexOnMailServer = IndexOnMailServer;
			_msgDateTime = MsgDateTime;
			_toAddress = ToAddress;
			_fromAddress = FromAddress;
			_subject = Subject;
			_body = Body;
			_processed = Processed;
			_foundAndRemoved = FoundAndRemoved;
		}
		public RemoveMail Copy()
		{
			RemoveMail _removeMail = new RemoveMail();
			_removeMail.ID = _iD;
			_removeMail.IndexOnMailServer = _indexOnMailServer;
			_removeMail.MsgDateTime = _msgDateTime;
			_removeMail.ToAddress = _toAddress;
			_removeMail.FromAddress = _fromAddress;
			_removeMail.Subject = _subject;
			_removeMail.Body = _body;
			_removeMail.Processed = _processed;
			_removeMail.FoundAndRemoved = _foundAndRemoved;
			return _removeMail;
		}
		#region "Private Instance Variables"
		private int _iD;
		private int _indexOnMailServer;
		private DateTime _msgDateTime;
		private string _toAddress;
		private string _fromAddress;
		private string _subject;
		private string _body;
		private bool _processed;
		private bool _foundAndRemoved;
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

		public int IndexOnMailServer
		{
			get
			{
				return _indexOnMailServer;
			}
			set
			{
				if (value != _indexOnMailServer)
				{
					this._isModified = true;
					_indexOnMailServer = value;
				}
			}
		}

		public DateTime MsgDateTime
		{
			get
			{
				return _msgDateTime;
			}
			set
			{
				if (value != _msgDateTime)
				{
					this._isModified = true;
					_msgDateTime = value;
				}
			}
		}

		public string ToAddress
		{
			get
			{
				return _toAddress;
			}
			set
			{
				if (value != _toAddress)
				{
					this._isModified = true;
					_toAddress = value;
				}
			}
		}

		public string FromAddress
		{
			get
			{
				return _fromAddress;
			}
			set
			{
				if (value != _fromAddress)
				{
					this._isModified = true;
					_fromAddress = value;
				}
			}
		}

		public string Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				if (value != _subject)
				{
					this._isModified = true;
					_subject = value;
				}
			}
		}

		public string Body
		{
			get
			{
				return _body;
			}
			set
			{
				if (value != _body)
				{
					this._isModified = true;
					_body = value;
				}
			}
		}

		public bool Processed
		{
			get
			{
				return _processed;
			}
			set
			{
				if (value != _processed)
				{
					this._isModified = true;
					_processed = value;
				}
			}
		}

		public bool FoundAndRemoved
		{
			get
			{
				return _foundAndRemoved;
			}
			set
			{
				if (value != _foundAndRemoved)
				{
					this._isModified = true;
					_foundAndRemoved = value;
				}
			}
		}

		#endregion 
		public int CompareTo(Object objRemoveMail)
		{
			//sort by ID ascending - this is used by the default sort mechanisc
			return this.ID.CompareTo(((RemoveMail)objRemoveMail).ID);
		}
	}
}
