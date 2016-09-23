using System;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.Net.Mail;
using System.Net;
namespace BKLeadsOnline.Domain
{
	/// <summary>
	/// A Class interacting with the UserProfile table.
	/// Author: Jonathan Shaw
	/// Date Created: 5/23/2013 1:44:04 PM
	/// </summary>
	public class UserProfileService 
	{
        static private string _selectViewSQL = "Select ID, DealerID, IsAdmin, IsPrimaryContact, PositionID, ApplicationLogin, Password, FirstName, LastName, Phone, Cell, Email, SendToEmail, TextAddress, SendToText, UserCookie, LimitToOneComputer, LastLoginDateTime, Active, SecurityQuestion1ID, SecurityQuestion2ID, SecurityAnswer1, SecurityAnswer2, PasswordResetGUID, PasswordResetTimestamp, LastPasswordChange, DealerName, PositionName, SecurityQuestion1Name, SecurityQuestion2Name from v_UserProfile";
        static public List<UserProfile> GetAll()
        {
            List<UserProfile> objUserProfiles = new List<UserProfile>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _selectViewSQL))
            {
                while (reader.Read())
                {
                    UserProfile objUserProfile = new UserProfile();
                    LoadUserProfile(objUserProfile, reader);
                    objUserProfile.IsModified = false;
                    objUserProfiles.Add(objUserProfile);
                }
                // always call Close when done reading.
                reader.Close();
                return objUserProfiles;
            }
        }
        static public UserProfile GetByID(int ID)
        {
            UserProfile objUserProfile = new UserProfile();
            string query = _selectViewSQL + " where ID = " + ID;
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadUserProfile(objUserProfile, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objUserProfile.IsModified = false;
                    return objUserProfile;
                }
                else
                {
                    return null;
                }
            }
        }
        static private void LoadUserProfile(UserProfile objUserProfile, IDataReader _reader)
        {
            if (_reader.IsDBNull(0) != true)
            {
                objUserProfile.ID = _reader.GetInt32(0);
            }
            if (_reader.IsDBNull(1) != true)
            {
                objUserProfile.DealerID = _reader.GetInt32(1);
            }
            if (_reader.IsDBNull(2) != true)
            {
                objUserProfile.IsAdmin = _reader.GetBoolean(2);
            }
            if (_reader.IsDBNull(3) != true)
            {
                objUserProfile.IsPrimaryContact = _reader.GetBoolean(3);
            }
            if (_reader.IsDBNull(4) != true)
            {
                objUserProfile.PositionID = _reader.GetInt32(4);
            }
            if (_reader.IsDBNull(5) != true)
            {
                objUserProfile.ApplicationLogin = _reader.GetString(5);
            }
            if (_reader.IsDBNull(6) != true)
            {
                objUserProfile.Password = Crypto.DecryptString(_reader.GetString(6));
            }
            if (_reader.IsDBNull(7) != true)
            {
                objUserProfile.FirstName = _reader.GetString(7);
            }
            if (_reader.IsDBNull(8) != true)
            {
                objUserProfile.LastName = _reader.GetString(8);
            }
            if (_reader.IsDBNull(9) != true)
            {
                objUserProfile.Phone = _reader.GetString(9);
            }
            if (_reader.IsDBNull(10) != true)
            {
                objUserProfile.Cell = _reader.GetString(10);
            }
            if (_reader.IsDBNull(11) != true)
            {
                objUserProfile.Email = _reader.GetString(11);
            }
            if (_reader.IsDBNull(12) != true)
            {
                objUserProfile.SendToEmail = _reader.GetBoolean(12);
            }
            if (_reader.IsDBNull(13) != true)
            {
                objUserProfile.TextAddress = _reader.GetString(13);
            }
            if (_reader.IsDBNull(14) != true)
            {
                objUserProfile.SendToText = _reader.GetBoolean(14);
            }
            if (_reader.IsDBNull(15) != true)
            {
                objUserProfile.UserCookie = _reader.GetString(15);
            }
            if (_reader.IsDBNull(16) != true)
            {
                objUserProfile.LimitToOneComputer = _reader.GetBoolean(16);
            }
            if (_reader.IsDBNull(17) != true)
            {
                objUserProfile.LastLoginDateTime = _reader.GetDateTime(17);
            }
            if (_reader.IsDBNull(18) != true)
            {
                objUserProfile.Active = _reader.GetBoolean(18);
            }
            if (_reader.IsDBNull(19) != true)
            {
                objUserProfile.SecurityQuestion1ID = _reader.GetInt32(19);
            }
            if (_reader.IsDBNull(20) != true)
            {
                objUserProfile.SecurityQuestion2ID = _reader.GetInt32(20);
            }
            if (_reader.IsDBNull(21) != true)
            {
                objUserProfile.SecurityAnswer1 = _reader.GetString(21);
            }
            if (_reader.IsDBNull(22) != true)
            {
                objUserProfile.SecurityAnswer2 = _reader.GetString(22);
            }
            if (_reader.IsDBNull(23) != true)
            {
                objUserProfile.PasswordResetGUID = _reader.GetString(23);
            }
            if (_reader.IsDBNull(24) != true)
            {
                objUserProfile.PasswordResetTimestamp = _reader.GetDateTime(24);
            }
            if (_reader.IsDBNull(25) != true)
            {
                objUserProfile.LastPasswordChange = _reader.GetDateTime(25);
            }
            if (_reader.IsDBNull(26) != true)
            {
                objUserProfile.DealerName = _reader.GetString(26);
            }
            if (_reader.IsDBNull(27) != true)
            {
                objUserProfile.PositionName = _reader.GetString(27);
            }
            if (_reader.IsDBNull(28) != true)
            {
                objUserProfile.SecurityQuestion1Name = _reader.GetString(28);
            }
            if (_reader.IsDBNull(29) != true)
            {
                objUserProfile.SecurityQuestion2Name = _reader.GetString(29);
            }
        }
        static public List<UserProfile> GetForDealer(int DealerID)
        {
            List<UserProfile> objUserProfiles = new List<UserProfile>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL + " where (IsAdmin = 0 or IsAdmin is null) and (DealerID is not null and DealerID = " + DealerID + ")";
            _query += " order by LastName asc";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    UserProfile objUserProfile = new UserProfile();
                    LoadUserProfile(objUserProfile, reader);
                    objUserProfile.IsModified = false;
                    objUserProfiles.Add(objUserProfile);
                }
                // always call Close when done reading.
                reader.Close();
                return objUserProfiles;
            }
        }

        static public List<UserProfile> GetFiltered(string Name, string DealerName)
        {
            List<UserProfile> objUserProfiles = new List<UserProfile>();
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            string _query = _selectViewSQL + " where ";
            if (Name.Length > 0)
            {
                _query += " ( firstname like '%" + Name + "%' or lastname like '%" + Name + "%' ) AND ";
            }
            if (DealerName.Length > 0)
            {
                _query += " DealerName like '%" + DealerName + "%' and ";
            }
            _query += " 1 = 1  order by DealerName, LastName asc";

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, _query))
            {
                while (reader.Read())
                {
                    UserProfile objUserProfile = new UserProfile();
                    LoadUserProfile(objUserProfile, reader);
                    objUserProfile.IsModified = false;
                    objUserProfiles.Add(objUserProfile);
                }
                // always call Close when done reading.
                reader.Close();
                return objUserProfiles;
            }
        }
        static public UserProfile GetByPasswordResetGUID(string guid)
        {
            UserProfile objUserProfile = new UserProfile();
            string query = _selectViewSQL + " where PasswordResetGuid = '" + guid + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (reader.Read())
                {
                    LoadUserProfile(objUserProfile, reader);
                    // always call Close when done reading.
                    reader.Close();
                    objUserProfile.IsModified = false;
                    return objUserProfile;
                }
                else
                {
                    return null;
                }
            }
        }
        static public UserProfile GetByApplicationLogin(string UserName)
        {
            UserProfile objUserProfile = new UserProfile();
            string query = _selectViewSQL + " where ApplicationLogin = '" + UserName + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader _reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (_reader.Read())
                {
                    LoadUserProfile(objUserProfile, _reader);
                    // always call Close when done reading.
                    _reader.Close();
                    objUserProfile.IsModified = false;
                    return objUserProfile;
                }
                else
                {
                    return null;
                }
            }
        }
        static public UserProfile GetByName(string UserName)
        {
            UserProfile objUserProfile = new UserProfile();
            string query = _selectViewSQL + " where Name = '" + UserName + "'";
            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");

            using (IDataReader _reader = db.ExecuteReader(CommandType.Text, query))
            {
                if (_reader.Read())
                {
                    LoadUserProfile(objUserProfile, _reader);
                    // always call Close when done reading.
                    _reader.Close();
                    objUserProfile.IsModified = false;
                    return objUserProfile;
                }
                else
                {
                    return null;
                }
            }
        }
        static public UserProfile AuthenticateUser(string UserName, string Password)
        {
            //As the current NMotionCRM implementation uses SQl Server authentication,
            //we have to parse the connection string and replace the UserID and passowrd rather
            //than using the DatabaseFactory.CreateDatabase() function for this method only.
            //once we have established that the current user has connetivity we connect for everything else
            //using the standard connection and DatabaseFactory.CreateDatabase() this eliminates the
            //need to pass the connectin string aronud.  Might come back to this later.

            //string _connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");	
            //string CurrentUser = _connectionString.Substring(_connectionString.IndexOf("User Id=") + 8, _connectionString.IndexOf("Password=") -1  - (_connectionString.IndexOf("User Id=") + 8));
            //Debug.Write(CurrentUser);
            //string CurrentPassword = _connectionString.Substring(_connectionString.IndexOf("Password=") + 9, _connectionString.Length - 1 - (_connectionString.IndexOf("Password=") + 9) );
            //Debug.Write(CurrentPassword);

            //_connectionString = _connectionString.Replace(CurrentUser, UserName);
            //_connectionString = _connectionString.Replace(CurrentPassword, Password);

            //Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(_connectionString);
            //try
            //{
            //   System.Data.Common.DbConnection con = db.CreateConnection();
            //   con.Open();
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}

            UserProfile up = GetByApplicationLogin(UserName);

            Debug.WriteLine("The entered password is: " + Password);
            Debug.WriteLine("The retrieved password is: " + up.Password);
            if (up.Password == Password && up.Active == true)
            {
                //we have a match
                return up;
            }
            else
                return null;

            //if (!(up == null))
            //{
            //    up.ConnectionString = _connectionString;
            //    return up;
            //}
            //else
            //{
            //    return null;            
            //}

        }
      
        static public void Save(UserProfile objUserProfile)
		{
			if (objUserProfile.IsModified == true)
			{
				if (objUserProfile.ID == 0 )
				{
					Insert(objUserProfile);
				}
				else
				{
					Update(objUserProfile);
				}
			}
		}
        static private void Insert(UserProfile objUserProfile)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UserProfileInsert");
            db.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
            if (objUserProfile.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objUserProfile.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "IsAdmin", DbType.Boolean, objUserProfile.IsAdmin);
            db.AddInParameter(dbCommand, "IsPrimaryContact", DbType.Boolean, objUserProfile.IsPrimaryContact);
            if (objUserProfile.PositionID > 0)
            {
                db.AddInParameter(dbCommand, "PositionID", DbType.Int32, objUserProfile.PositionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PositionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ApplicationLogin", DbType.String, objUserProfile.ApplicationLogin);
            db.AddInParameter(dbCommand, "Password", DbType.String, Crypto.EncryptString( objUserProfile.Password));
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objUserProfile.FirstName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objUserProfile.LastName);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objUserProfile.Phone);
            db.AddInParameter(dbCommand, "Cell", DbType.String, objUserProfile.Cell);
            db.AddInParameter(dbCommand, "Email", DbType.String, objUserProfile.Email);
            db.AddInParameter(dbCommand, "SendToEmail", DbType.Boolean, objUserProfile.SendToEmail);
            db.AddInParameter(dbCommand, "TextAddress", DbType.String, objUserProfile.TextAddress);
            db.AddInParameter(dbCommand, "SendToText", DbType.Boolean, objUserProfile.SendToText);
            db.AddInParameter(dbCommand, "UserCookie", DbType.String, objUserProfile.UserCookie);
            db.AddInParameter(dbCommand, "LimitToOneComputer", DbType.Boolean, objUserProfile.LimitToOneComputer);
            if (objUserProfile.LastLoginDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastLoginDateTime", DbType.DateTime, objUserProfile.LastLoginDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastLoginDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objUserProfile.Active);
            if (objUserProfile.SecurityQuestion1ID > 0)
            {
                db.AddInParameter(dbCommand, "SecurityQuestion1ID", DbType.Int32, objUserProfile.SecurityQuestion1ID);
            }
            else
            {
                db.AddInParameter(dbCommand, "SecurityQuestion1ID", DbType.Int32, Convert.DBNull);
            }
            if (objUserProfile.SecurityQuestion2ID > 0)
            {
                db.AddInParameter(dbCommand, "SecurityQuestion2ID", DbType.Int32, objUserProfile.SecurityQuestion2ID);
            }
            else
            {
                db.AddInParameter(dbCommand, "SecurityQuestion2ID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "SecurityAnswer1", DbType.String, objUserProfile.SecurityAnswer1);
            db.AddInParameter(dbCommand, "SecurityAnswer2", DbType.String, objUserProfile.SecurityAnswer2);
            db.AddInParameter(dbCommand, "PasswordResetGUID", DbType.String, objUserProfile.PasswordResetGUID);
            if (objUserProfile.PasswordResetTimestamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PasswordResetTimestamp", DbType.DateTime, objUserProfile.PasswordResetTimestamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "PasswordResetTimestamp", DbType.DateTime, Convert.DBNull);
            }
            if (objUserProfile.LastPasswordChange > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPasswordChange", DbType.DateTime, objUserProfile.LastPasswordChange);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPasswordChange", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
            objUserProfile.ID = Int32.Parse(db.GetParameterValue(dbCommand, "ID").ToString());
        }
        static private void Update(UserProfile objUserProfile)
        {

            Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UserProfileUpdate");
            db.AddInParameter(dbCommand, "ID", DbType.Int32, objUserProfile.ID);
            if (objUserProfile.DealerID > 0)
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, objUserProfile.DealerID);
            }
            else
            {
                db.AddInParameter(dbCommand, "DealerID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "IsAdmin", DbType.Boolean, objUserProfile.IsAdmin);
            db.AddInParameter(dbCommand, "IsPrimaryContact", DbType.Boolean, objUserProfile.IsPrimaryContact);
            if (objUserProfile.PositionID > 0)
            {
                db.AddInParameter(dbCommand, "PositionID", DbType.Int32, objUserProfile.PositionID);
            }
            else
            {
                db.AddInParameter(dbCommand, "PositionID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "ApplicationLogin", DbType.String, objUserProfile.ApplicationLogin);
            db.AddInParameter(dbCommand, "Password", DbType.String, Crypto.EncryptString( objUserProfile.Password));
            db.AddInParameter(dbCommand, "FirstName", DbType.String, objUserProfile.FirstName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, objUserProfile.LastName);
            db.AddInParameter(dbCommand, "Phone", DbType.String, objUserProfile.Phone);
            db.AddInParameter(dbCommand, "Cell", DbType.String, objUserProfile.Cell);
            db.AddInParameter(dbCommand, "Email", DbType.String, objUserProfile.Email);
            db.AddInParameter(dbCommand, "SendToEmail", DbType.Boolean, objUserProfile.SendToEmail);
            db.AddInParameter(dbCommand, "TextAddress", DbType.String, objUserProfile.TextAddress);
            db.AddInParameter(dbCommand, "SendToText", DbType.Boolean, objUserProfile.SendToText);
            db.AddInParameter(dbCommand, "UserCookie", DbType.String, objUserProfile.UserCookie);
            db.AddInParameter(dbCommand, "LimitToOneComputer", DbType.Boolean, objUserProfile.LimitToOneComputer);
            if (objUserProfile.LastLoginDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastLoginDateTime", DbType.DateTime, objUserProfile.LastLoginDateTime);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastLoginDateTime", DbType.DateTime, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "Active", DbType.Boolean, objUserProfile.Active);
            if (objUserProfile.SecurityQuestion1ID > 0)
            {
                db.AddInParameter(dbCommand, "SecurityQuestion1ID", DbType.Int32, objUserProfile.SecurityQuestion1ID);
            }
            else
            {
                db.AddInParameter(dbCommand, "SecurityQuestion1ID", DbType.Int32, Convert.DBNull);
            }
            if (objUserProfile.SecurityQuestion2ID > 0)
            {
                db.AddInParameter(dbCommand, "SecurityQuestion2ID", DbType.Int32, objUserProfile.SecurityQuestion2ID);
            }
            else
            {
                db.AddInParameter(dbCommand, "SecurityQuestion2ID", DbType.Int32, Convert.DBNull);
            }
            db.AddInParameter(dbCommand, "SecurityAnswer1", DbType.String, objUserProfile.SecurityAnswer1);
            db.AddInParameter(dbCommand, "SecurityAnswer2", DbType.String, objUserProfile.SecurityAnswer2);
            db.AddInParameter(dbCommand, "PasswordResetGUID", DbType.String, objUserProfile.PasswordResetGUID);
            if (objUserProfile.PasswordResetTimestamp > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "PasswordResetTimestamp", DbType.DateTime, objUserProfile.PasswordResetTimestamp);
            }
            else
            {
                db.AddInParameter(dbCommand, "PasswordResetTimestamp", DbType.DateTime, Convert.DBNull);
            }
            if (objUserProfile.LastPasswordChange > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "LastPasswordChange", DbType.DateTime, objUserProfile.LastPasswordChange);
            }
            else
            {
                db.AddInParameter(dbCommand, "LastPasswordChange", DbType.DateTime, Convert.DBNull);
            }
            db.ExecuteNonQuery(dbCommand);
        }
        static public void Delete(UserProfile objUserProfile)
		{
			
			Database db = DatabaseFactory.CreateDatabase("BKLeadsOnline");
			System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("p_UserProfileDelete");
			db.AddInParameter(dbCommand, "ID", DbType.Int32, objUserProfile.ID);
			db.ExecuteNonQuery(dbCommand);
		}

        static public void SendForgotPasswordLink(UserProfile objUserProfile)
        {
            //Create a new GUID
            Guid _guid = Guid.NewGuid();
            objUserProfile.PasswordResetGUID = _guid.ToString();
            objUserProfile.PasswordResetTimestamp = DateTime.Now;
            Update(objUserProfile);

            string EmailSender = ConfigurationManager.AppSettings["SMTPFromAddress"];

            string emailSubject = "Your BK Leads Online Password Reset Link";

            string emailRecipient = objUserProfile.Email;

            string _content = "Please click the link to reset your password.  This link will expire in 30 minutes: " + ConfigurationManager.AppSettings["ServerAppPath"] + "NewPassword.aspx?guid=" + objUserProfile.PasswordResetGUID;

            var m = new MailMessage(EmailSender, emailRecipient, emailSubject, _content);

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"], Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserID"], ConfigurationManager.AppSettings["SMTPPassword"]);

            //smtp.EnableSsl = true;

            smtp.Send(m);
        }

	}
}
