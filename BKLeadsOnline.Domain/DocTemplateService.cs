using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the DocTemplate table.
	/// Author: Jonathan Shaw
	/// Date Created: 4/17/2013 1:11:30 PM
	/// </summary>
	public class DocTemplateService 
	{
        static private string _selectViewSQLPartial = "Select ID, DealerID, LetterID, DisplayName, UploadDate, WordContentType, WordFileSize, LargePicContentType, LargePicSize, ThumbnailPicContentType, ThumbnailPicSize, Color, Grayscale, TextOnly, UseDealerLogo, SecondNotice, Comment, Active, DealerName, LetterName from v_DocTemplate";
        static private string _selectViewSQL = "Select ID, DealerID, LetterID, DisplayName, UploadDate, WordDoc, WordContentType, WordFileSize, LargePic, LargePicContentType, LargePicSize, ThumbnailPic, ThumbnailPicContentType, ThumbnailPicSize, Color, Grayscale, TextOnly, UseDealerLogo, SecondNotice, Comment, Active, DealerName, LetterName from v_DocTemplate";
        static private void LoadDocTemplate(DocTemplate objDocTemplate, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objDocTemplate.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDocTemplate.DealerID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDocTemplate.LetterID = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDocTemplate.DisplayName = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDocTemplate.UploadDate = _reader.GetDateTime(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDocTemplate.WordDoc = (byte[])_reader["WordDoc"];
            }
            if (_reader.IsDBNull(6) != true)
            {
                objDocTemplate.WordContentType = _reader.GetString(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDocTemplate.WordFileSize = _reader.GetInt32(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDocTemplate.LargePic = (byte[])_reader["LargePic"];
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDocTemplate.LargePicContentType = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDocTemplate.LargePicSize = _reader.GetInt32(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDocTemplate.ThumbnailPic = (byte[])_reader["ThumbnailPic"];
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDocTemplate.ThumbnailPicContentType = _reader.GetString(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objDocTemplate.ThumbnailPicSize = _reader.GetInt32(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDocTemplate.Color = _reader.GetBoolean(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objDocTemplate.Grayscale = _reader.GetBoolean(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objDocTemplate.TextOnly = _reader.GetBoolean(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objDocTemplate.UseDealerLogo = _reader.GetBoolean(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objDocTemplate.SecondNotice = _reader.GetBoolean(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objDocTemplate.Comment = _reader.GetString(19);
            }
            if (_reader.IsDBNull(20) != true)
            {
                objDocTemplate.Active = _reader.GetBoolean(20);
            }
            if (_reader.IsDBNull(21) != true)
            {
                objDocTemplate.DealerName = _reader.GetString(21);
            }
            if (_reader.IsDBNull(22) != true)
            {
                objDocTemplate.LetterName = _reader.GetString(22);
            }
        }
        
        static public DocTemplate GetByID(int ID)
        {
            DocTemplate objDocTemplate = new DocTemplate();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDocTemplate(objDocTemplate, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDocTemplate.IsModified = false;
                    return objDocTemplate;
                }
                else
                {
                    return null;
                }
            }
        }

        static public DocTemplate GetByNameAndDealer(string Name, int DealerID)
        {
            DocTemplate objDocTemplate = new DocTemplate();
            string query = _selectViewSQL + " where DealerID = " + DealerID + " and DisplayName = '" + Name + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDocTemplate(objDocTemplate, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDocTemplate.IsModified = false;
                    return objDocTemplate;
                }
                else
                {
                    return null;
                }
            }
        }

        static public List<DocTemplate> GetAll()
        {
            List<DocTemplate> objDocTemplates = new List<DocTemplate>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial))
            {
                while (reader.Read())
                {
                    DocTemplate objDocTemplate = new DocTemplate();
                    LoadDocTemplatePartial(objDocTemplate, reader);
                    objDocTemplate.IsModified = false;
                    objDocTemplates.Add(objDocTemplate);
                }
                // always call Close when done reading.
                reader.Close();
                return objDocTemplates;
            }
        }
        static public List<DocTemplate> GetAllForDealer(int DealerID)
        {
            List<DocTemplate> objDocTemplates = new List<DocTemplate>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial + " where (DealerID = " + DealerID + ") or v_DocTemplate.ID in (select DocTemplateID from DealerDocTemplate where DealerID = " + DealerID + ") order by DisplayName asc"))
            {
                while (reader.Read())
                {
                    DocTemplate objDocTemplate = new DocTemplate();
                    LoadDocTemplatePartial(objDocTemplate, reader);
                    objDocTemplate.IsModified = false;
                    objDocTemplates.Add(objDocTemplate);
                }
                // always call Close when done reading.
                reader.Close();
                return objDocTemplates;
            }
        }
        static public List<DocTemplate> GetCustomForDealer(int DealerID)
        {
            List<DocTemplate> objDocTemplates = new List<DocTemplate>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial + " where DealerID = " + DealerID + " order by DisplayName asc"))
            {
                while (reader.Read())
                {
                    DocTemplate objDocTemplate = new DocTemplate();
                    LoadDocTemplatePartial(objDocTemplate, reader);
                    objDocTemplate.IsModified = false;
                    objDocTemplates.Add(objDocTemplate);
                }
                // always call Close when done reading.
                reader.Close();
                return objDocTemplates;
            }
        }
        static public List<DocTemplate> GetGlobalTemplates()
        {
            List<DocTemplate> objDocTemplates = new List<DocTemplate>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial + " where DealerID is null or DealerID = 0  order by DisplayName asc"))
            {
                while (reader.Read())
                {
                    DocTemplate objDocTemplate = new DocTemplate();
                    LoadDocTemplatePartial(objDocTemplate, reader);
                    objDocTemplate.IsModified = false;
                    objDocTemplates.Add(objDocTemplate);
                }
                // always call Close when done reading.
                reader.Close();
                return objDocTemplates;
            }
        }
        static public List<DocTemplate> GetGlobalTemplatesForDealer(int DealerID)
        {
            List<DocTemplate> objDocTemplates = new List<DocTemplate>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQLPartial + " where v_DocTemplate.ID in (select DocTemplateID from DealerDocTemplate where DealerID = " + DealerID + ") order by DisplayName asc "))
            {
                while (reader.Read())
                {
                    DocTemplate objDocTemplate = new DocTemplate();
                    LoadDocTemplatePartial(objDocTemplate, reader);
                    objDocTemplate.IsModified = false;
                    objDocTemplates.Add(objDocTemplate);
                }
                
                // always call Close when done reading.
                reader.Close();
                return objDocTemplates;
            
            }
        }
        static public DocTemplate GetByAttributes(int LetterID, string Color, string GrayScale, string TextOnly, string SecondNotice)
        {
            DocTemplate objDocTemplate = new DocTemplate();
            string query = _selectViewSQL + " where LetterID = " + LetterID;
            if (Color.ToLower() == "true")
                query += " and color = 1 ";
            else
                query += " and color = 0 ";

            if (GrayScale.ToLower() == "true")
                query += " and GrayScale = 1 ";
            else
                query += " and GrayScale = 0 ";

            if (TextOnly.ToLower() == "true")
                query += " and TextOnly = 1 ";
            else
                query += " and TextOnly = 0 ";

            if (SecondNotice.ToLower() == "true")
                query += " and SecondNotice = 1 ";
            else
                query += " and SecondNotice = 0 ";

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDocTemplate(objDocTemplate, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDocTemplate.IsModified = false;
                    return objDocTemplate;
                }
                else
                {
                    return null;
                }
            }        
        }
        static public DocTemplate GetByAttributes(int LetterID, bool Color, bool GrayScale, bool TextOnly, bool SecondNotice)
        {
            DocTemplate objDocTemplate = new DocTemplate();
            string query = _selectViewSQL + " where LetterID = " + LetterID;
            if (Color)
                query += " and color = 1 ";
            else
                query += " and color = 0 ";

            if (GrayScale)
                query += " and GrayScale = 1 ";
            else
                query += " and GrayScale = 0 ";

            if (TextOnly)
                query += " and TextOnly = 1 ";
            else
                query += " and TextOnly = 0 ";

            if (SecondNotice)
                query += " and SecondNotice = 1 ";
            else
                query += " and SecondNotice = 0 ";

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadDocTemplate(objDocTemplate, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objDocTemplate.IsModified = false;
                    return objDocTemplate;
                }
                else
                {
                    return null;
                }
            }
        }
        //_selectViewSQLPartial = "Select ID, DealerID, DisplayName, UploadDate, PDFFileSize, WordContentType, WordFileSize, Comment, Active, DealerName from v_DocTemplate";
        static private void LoadDocTemplatePartial(DocTemplate objDocTemplate, IDataReader _reader)
        {
            //Select ID, DealerID, LetterID, DisplayName, 
            if (_reader.IsDBNull(0) != true)
            {
                objDocTemplate.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objDocTemplate.DealerID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objDocTemplate.LetterID = _reader.GetInt32(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objDocTemplate.DisplayName = _reader.GetString(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objDocTemplate.UploadDate = _reader.GetDateTime(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objDocTemplate.WordContentType = _reader.GetString(5);
            }
            //UploadDate, WordContentType, WordFileSize, LargePicContentType, LargePicSize, ThumbnailPicContentType, ThumbnailPicSize, 
            if (_reader.IsDBNull(6) != true)
            {
                objDocTemplate.WordFileSize = _reader.GetInt32(6);
            }
            if (_reader.IsDBNull(7) != true)
            {
                objDocTemplate.LargePicContentType = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objDocTemplate.LargePicSize = _reader.GetInt32(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objDocTemplate.ThumbnailPicContentType = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objDocTemplate.ThumbnailPicSize = _reader.GetInt32(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objDocTemplate.Color = _reader.GetBoolean(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objDocTemplate.Grayscale = _reader.GetBoolean(12);
            }
            //Color, Grayscale, TextOnly, UseDealerLogo, Comment, Active, DealerName, LetterName from v_DocTemplate
            if (_reader.IsDBNull(13) != true)
            {
                objDocTemplate.TextOnly = _reader.GetBoolean(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objDocTemplate.UseDealerLogo = _reader.GetBoolean(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objDocTemplate.SecondNotice = _reader.GetBoolean(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objDocTemplate.Comment = _reader.GetString(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objDocTemplate.Active = _reader.GetBoolean(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objDocTemplate.DealerName = _reader.GetString(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objDocTemplate.LetterName = _reader.GetString(19);
            }
        }
        static public void Save(DocTemplate objDocTemplate)
		{
			if (objDocTemplate.IsModified == true)
			{
				if (objDocTemplate.ID == 0 )
				{
					Insert(objDocTemplate);
				}
				else
				{
					Update(objDocTemplate);
				}
			}
		}
        
        static private void Insert(DocTemplate objDocTemplate)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DocTemplateInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objDocTemplate.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDocTemplate.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDocTemplate.LetterID > 0)
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, objDocTemplate.LetterID);
            }
            else
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "DisplayName", DbType.String, objDocTemplate.DisplayName);
            if (objDocTemplate.UploadDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, objDocTemplate.UploadDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "WordDoc", DbType.Binary, objDocTemplate.WordDoc);
            db.AddInParameter(dbCommand, "WordContentType", DbType.String, objDocTemplate.WordContentType);
            db.AddInParameter(dbCommand, "WordFileSize", DbType.Int32, objDocTemplate.WordFileSize);
            db.AddInParameter(dbCommand, "LargePic", DbType.Binary, objDocTemplate.LargePic);
            db.AddInParameter(dbCommand, "LargePicContentType", DbType.String, objDocTemplate.LargePicContentType);
            db.AddInParameter(dbCommand, "LargePicSize", DbType.Int32, objDocTemplate.LargePicSize);
            db.AddInParameter(dbCommand, "ThumbnailPic", DbType.Binary, objDocTemplate.ThumbnailPic);
            db.AddInParameter(dbCommand, "ThumbnailPicContentType", DbType.String, objDocTemplate.ThumbnailPicContentType);
            db.AddInParameter(dbCommand, "ThumbnailPicSize", DbType.Int32, objDocTemplate.ThumbnailPicSize);
            db.AddInParameter(dbCommand, "Color", DbType.Boolean, objDocTemplate.Color);
            db.AddInParameter(dbCommand, "Grayscale", DbType.Boolean, objDocTemplate.Grayscale);
            db.AddInParameter(dbCommand, "TextOnly", DbType.Boolean, objDocTemplate.TextOnly);
            db.AddInParameter(dbCommand, "UseDealerLogo", DbType.Boolean, objDocTemplate.UseDealerLogo);
            db.AddInParameter(dbCommand, "SecondNotice", DbType.Boolean, objDocTemplate.SecondNotice);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDocTemplate.Comment);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDocTemplate.Active);
            db.ExecuteNonQuery(dbCommand);
            objDocTemplate.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DocTemplate objDocTemplate)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DocTemplateUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDocTemplate.ID);
            if (objDocTemplate.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDocTemplate.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDocTemplate.LetterID > 0)
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, objDocTemplate.LetterID);
            }
            else
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "DisplayName", DbType.String, objDocTemplate.DisplayName);
            if (objDocTemplate.UploadDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, objDocTemplate.UploadDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "WordDoc", DbType.Binary, objDocTemplate.WordDoc);
            db.AddInParameter(dbCommand, "WordContentType", DbType.String, objDocTemplate.WordContentType);
            db.AddInParameter(dbCommand, "WordFileSize", DbType.Int32, objDocTemplate.WordFileSize);
            db.AddInParameter(dbCommand, "LargePic", DbType.Binary, objDocTemplate.LargePic);
            db.AddInParameter(dbCommand, "LargePicContentType", DbType.String, objDocTemplate.LargePicContentType);
            db.AddInParameter(dbCommand, "LargePicSize", DbType.Int32, objDocTemplate.LargePicSize);
            db.AddInParameter(dbCommand, "ThumbnailPic", DbType.Binary, objDocTemplate.ThumbnailPic);
            db.AddInParameter(dbCommand, "ThumbnailPicContentType", DbType.String, objDocTemplate.ThumbnailPicContentType);
            db.AddInParameter(dbCommand, "ThumbnailPicSize", DbType.Int32, objDocTemplate.ThumbnailPicSize);
            db.AddInParameter(dbCommand, "Color", DbType.Boolean, objDocTemplate.Color);
            db.AddInParameter(dbCommand, "Grayscale", DbType.Boolean, objDocTemplate.Grayscale);
            db.AddInParameter(dbCommand, "TextOnly", DbType.Boolean, objDocTemplate.TextOnly);
            db.AddInParameter(dbCommand, "UseDealerLogo", DbType.Boolean, objDocTemplate.UseDealerLogo);
            db.AddInParameter(dbCommand, "SecondNotice", DbType.Boolean, objDocTemplate.SecondNotice);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDocTemplate.Comment);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDocTemplate.Active);
            db.ExecuteNonQuery(dbCommand);
        }
        #region save, insert, update using transactions
        static public void Save(DocTemplate objDocTemplate, Database db, DbTransaction trans, bool CommitTrans)
        {
            try
            {
                if (objDocTemplate.IsModified == true)
                {
                    if (objDocTemplate.ID == 0)
                    {
                        Insert(objDocTemplate, db, trans);
                    }
                    else
                    {
                        Update(objDocTemplate, db, trans);
                    }
                }
                if (CommitTrans == true)
                {
                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                if (CommitTrans == true)
                {
                    //we were hoping to commit the transaction which means we're at the end so
                    //Roll back the transaction. Otherwise just rethrow the error and let the next
                    //higher level catch it and rollback
                    trans.Rollback();
                }
                //and then rethrow the error
                throw (e);
            }
        }
        static private void Insert(DocTemplate objDocTemplate, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DocTemplateInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objDocTemplate.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDocTemplate.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDocTemplate.LetterID > 0)
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, objDocTemplate.LetterID);
            }
            else
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "DisplayName", DbType.String, objDocTemplate.DisplayName);
            if (objDocTemplate.UploadDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, objDocTemplate.UploadDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "WordDoc", DbType.Binary, objDocTemplate.WordDoc);
            db.AddInParameter(dbCommand, "WordContentType", DbType.String, objDocTemplate.WordContentType);
            db.AddInParameter(dbCommand, "WordFileSize", DbType.Int32, objDocTemplate.WordFileSize);
            db.AddInParameter(dbCommand, "LargePic", DbType.Binary, objDocTemplate.LargePic);
            db.AddInParameter(dbCommand, "LargePicContentType", DbType.String, objDocTemplate.LargePicContentType);
            db.AddInParameter(dbCommand, "LargePicSize", DbType.Int32, objDocTemplate.LargePicSize);
            db.AddInParameter(dbCommand, "ThumbnailPic", DbType.Binary, objDocTemplate.ThumbnailPic);
            db.AddInParameter(dbCommand, "ThumbnailPicContentType", DbType.String, objDocTemplate.ThumbnailPicContentType);
            db.AddInParameter(dbCommand, "ThumbnailPicSize", DbType.Int32, objDocTemplate.ThumbnailPicSize);
            db.AddInParameter(dbCommand, "Color", DbType.Boolean, objDocTemplate.Color);
            db.AddInParameter(dbCommand, "Grayscale", DbType.Boolean, objDocTemplate.Grayscale);
            db.AddInParameter(dbCommand, "TextOnly", DbType.Boolean, objDocTemplate.TextOnly);
            db.AddInParameter(dbCommand, "UseDealerLogo", DbType.Boolean, objDocTemplate.UseDealerLogo);
            db.AddInParameter(dbCommand, "SecondNotice", DbType.Boolean, objDocTemplate.SecondNotice);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDocTemplate.Comment);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDocTemplate.Active);
            db.ExecuteNonQuery(dbCommand, trans);
            objDocTemplate.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(DocTemplate objDocTemplate, Database db, DbTransaction trans)
        {

            //Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DocTemplateUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objDocTemplate.ID);
            if (objDocTemplate.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objDocTemplate.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            if (objDocTemplate.LetterID > 0)
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, objDocTemplate.LetterID);
            }
            else
            {
                db.AddInParameter(dbCommand, "LetterID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "DisplayName", DbType.String, objDocTemplate.DisplayName);
            if (objDocTemplate.UploadDate > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, objDocTemplate.UploadDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "WordDoc", DbType.Binary, objDocTemplate.WordDoc);
            db.AddInParameter(dbCommand, "WordContentType", DbType.String, objDocTemplate.WordContentType);
            db.AddInParameter(dbCommand, "WordFileSize", DbType.Int32, objDocTemplate.WordFileSize);
            db.AddInParameter(dbCommand, "LargePic", DbType.Binary, objDocTemplate.LargePic);
            db.AddInParameter(dbCommand, "LargePicContentType", DbType.String, objDocTemplate.LargePicContentType);
            db.AddInParameter(dbCommand, "LargePicSize", DbType.Int32, objDocTemplate.LargePicSize);
            db.AddInParameter(dbCommand, "ThumbnailPic", DbType.Binary, objDocTemplate.ThumbnailPic);
            db.AddInParameter(dbCommand, "ThumbnailPicContentType", DbType.String, objDocTemplate.ThumbnailPicContentType);
            db.AddInParameter(dbCommand, "ThumbnailPicSize", DbType.Int32, objDocTemplate.ThumbnailPicSize);
            db.AddInParameter(dbCommand, "Color", DbType.Boolean, objDocTemplate.Color);
            db.AddInParameter(dbCommand, "Grayscale", DbType.Boolean, objDocTemplate.Grayscale);
            db.AddInParameter(dbCommand, "TextOnly", DbType.Boolean, objDocTemplate.TextOnly);
            db.AddInParameter(dbCommand, "UseDealerLogo", DbType.Boolean, objDocTemplate.UseDealerLogo);
            db.AddInParameter(dbCommand, "SecondNotice", DbType.Boolean, objDocTemplate.SecondNotice);
            db.AddInParameter(dbCommand, "Comment", DbType.String, objDocTemplate.Comment);
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objDocTemplate.Active);
            db.ExecuteNonQuery(dbCommand, trans);
        }
        #endregion
        static public void Delete(DocTemplate objDocTemplate)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_DocTemplateDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objDocTemplate.ID);
			db.ExecuteNonQuery(dbCommand);
		}
	}
}
