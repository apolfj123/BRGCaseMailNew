using System;
using System.Collections;
using System.ComponentModel;

namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class representing a single row in the DealerDocTemplate table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/28/2013 9:06:37 AM
	/// </summary>
	public class DealerDocTemplate
	{
		public DealerDocTemplate()
		{
			_dealerID = 0;
			_docTemplateID = 0;
		}
		public DealerDocTemplate(int DealerID, int DocTemplateID)		{
			_dealerID = DealerID;
			_docTemplateID = DocTemplateID;
		}
		public DealerDocTemplate Copy()
		{
			DealerDocTemplate _dealerDocTemplate = new DealerDocTemplate();
			_dealerDocTemplate.DealerID = _dealerID;
			_dealerDocTemplate.DocTemplateID = _docTemplateID;
			return _dealerDocTemplate;
		}
		#region "Private Instance Variables"
		private int _dealerID;
		private int _docTemplateID;
		#endregion 
		#region "Public Properties"
public int DealerID
{
	get
	{
		return _dealerID;
	}
	set
	{
		_dealerID = value;
	}
}

public int DocTemplateID
{
	get
	{
		return _docTemplateID;
	}
	set
	{
		_docTemplateID = value;
	}
}

		#endregion 
	}
}
