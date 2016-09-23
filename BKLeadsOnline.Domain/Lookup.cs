using System;
using System.Collections.Generic;
using System.Text;

namespace BKLeadsOnline.Domain
{
    public class Lookup : IComparable, ICloneable
    {
        /// <summary>
        /// Lookup is the base class for a variety of classes that represent tables
        /// with the same structure. It is abstract and cannot be constructed.
        /// </summary>
        private bool _isModified;
        private int _iD;
        private string _name;
        private string _description;
        private bool _active;

        public Lookup()
        {
            _iD = 0;
            _name = string.Empty;
            _description = string.Empty;
            _active = false;
        }

        public Lookup(int iD, string name, string description, bool active)
        {
            _iD = iD;
            _name = name;
            _description = description;
            _active = active;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

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
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    this._isModified = true;
                    _name = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    this._isModified = true;
                    _description = value;
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
                }
            }
        }
                                                               
        public override string ToString()
        {
            if (_description.Length > 0)
                return _name + ": " + _description;
            else
                return _name;
        }

        public string NameAndDescription()
        {
            if (_description.Length > 0)
                return _name + ": " + _description;
            else
                return _name;
        }

        //we want to sort by default on the CustomerID
        //to binary search on CustomerID to return an Customer
        //by ID from the Customer collection
        public int CompareTo(Object objLookup)
        {
            //			if (objLookup.GetType().FullName != "NMotionServer.Lookup")
            //			{
            //				throw new ArgumentException("Object is not of type Lookup");
            //			}

            //sort by ID ascending - this is used by the default sort mechanism
            return this.ID.CompareTo(((Lookup)objLookup).ID);

        }
    }

    //public class OFCYLookup : Lookup
    //{
    //    private string _oFCYCode;
    //    public string OFCYCode
    //    {
    //        get
    //        {
    //            return _oFCYCode;
    //        }
    //        set
    //        {
    //            _oFCYCode = value;
    //        }
    //    }
    //    public string OFCYName
    //    {
    //        get
    //        {
    //            return _oFCYCode + " - " + this.Name;
    //        }
    //    }
    //    public OFCYLookup ()
    //    {
    //    }
    //    public OFCYLookup(int iD, string oFCYCode, string name, bool active)
    //        : base(iD, name, active)
    //    {
    //        _oFCYCode = OFCYCode;
    //    }
    //}
    
    //// OFCY Lookup types  
    //public class TargetPopulation : OFCYLookup
    //{
    //    public TargetPopulation()
    //    {
    //    }
    //    public TargetPopulation(int iD, string oFCYCode, string name, bool active)
    //        : base(iD, oFCYCode, name, active)
    //    {
    //    }
    //}
    //public class Gender : OFCYLookup 
    //{
    //    public Gender()
    //    {
    //    }
    //    public Gender(int iD, string oFCYCode, string name, bool active): base(iD, oFCYCode, name, active)
    //    {
    //    }
    //}
    //public class Ethnicity : OFCYLookup 
    //{
    //    public Ethnicity()
    //    {
    //    }
    //    public Ethnicity(int iD, string oFCYCode, string name, bool active)
    //        : base(iD, oFCYCode, name, active)
    //    { }
    //}

    //standard lookup types

    //this should match the Event Type table

    public enum enumGender
    {
        MALE = 1,
        FEMALE = 2,
        TRANSGENDER = 3
    }

    public class Letter : Lookup
    {
        public Letter()
        {
        }
        public Letter(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class Envelope : Lookup
    {
        public Envelope()
        {
        }
        public Envelope(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class ApplicationStatus : Lookup
    {
        public ApplicationStatus()
        {
        }
        public ApplicationStatus(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class EventType : Lookup
    {
        public EventType()
        {
        }
        public EventType(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class ApplicationStatusReason : Lookup
    {
        public ApplicationStatusReason()
        {
        }
        public ApplicationStatusReason(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class DisplayMonth : Lookup
    {
        public DisplayMonth()
        {
        }
        public DisplayMonth(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class ApplicationSource : Lookup
    {
        public ApplicationSource()
        {
        }
        public ApplicationSource(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class LeadStatus : Lookup
    {
        public LeadStatus()
        {
        }
        public LeadStatus(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class SecurityQuestion : Lookup
    {
        public SecurityQuestion()
        {
        }
        public SecurityQuestion(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class Position : Lookup
    {
        public Position()
        {
        }
        public Position(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }


    public class LeadStatusReason : Lookup
    {
        public LeadStatusReason()
        {
        }
        public LeadStatusReason(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class AutoYear : Lookup
    {
        public AutoYear()
        {
        }
        public AutoYear(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class ReportColLookup : Lookup
    {
        public ReportColLookup()
        {
        }
        public ReportColLookup(int iD, string name, string description, bool active)
            : base(iD, name, description, active)
        {
        }
    }

    public class LookupFactory
    {
        public static Lookup CreateLookup(string tableName)
        {
            switch (tableName)
            {
                case "DisplayMonth":
                    return new DisplayMonth();
                case "ReportColLookup":
                    return new ReportColLookup();
                case "ApplicationStatus":
                    return new ApplicationStatus();
                case "EventType":
                    return new EventType();
                case "ApplicationStatusReason":
                    return new ApplicationStatusReason();
                case "LeadStatus":
                    return new LeadStatus();
                case "ApplicationSource":
                    return new ApplicationSource();
                case "LeadStatusReason":
                    return new LeadStatus();
                case "SecurityQuestion":
                    return new SecurityQuestion();
                case "AutoYear":
                    return new AutoYear();
                case "Letter":
                    return new Letter();
                case "Envelope":
                    return new Envelope();
                case "Position":
                    return new Position();
                default:
                    throw new ArgumentException("The supplied tablename does not correspond to an implemented Lookup");
            }
        }
    }
}
