using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //goodsMaster
    public class t_attendanceClockGoGo
    {
        private int _autoid;
        public int autoid
        {
            get { return _autoid; }
            set { _autoid = value; }
        }

        private DateTime _createtime;
        public DateTime createTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }

        private string _date;
        public string date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _workspotname;
        public string workspotName
        {
            get { return _workspotname; }
            set { _workspotname = value; }
        }

        private string _employmentcode;
        public string employmentCode
        {
            get { return _employmentcode; }
            set { _employmentcode = value; }
        }

        private decimal? _gpslat;
        public decimal? gpsLat
        {
            get { return _gpslat; }
            set { _gpslat = value; }
        }

        private string _cardtype;
        public string cardType
        {
            get { return _cardtype; }
            set { _cardtype = value; }
        }

        private string _workspotcode;
        public string workspotCode
        {
            get { return _workspotcode; }
            set { _workspotcode = value; }
        }

        private decimal? _gpslng;
        public decimal? gpsLng
        {
            get { return _gpslng; }
            set { _gpslng = value; }
        }

        private bool? _isbiometric;
        public bool? isBiometric
        {
            get { return _isbiometric; }
            set { _isbiometric = value; }
        }

        private long? _id;
        public long? id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _time;
        public string time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}