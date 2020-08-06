﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class UserInfo
    {
        public int id;
        public string loginName;
        public string nickname;
        public string sessionid;
        public int? employID;
        public string employNnumber;
        public int? staffid;
        public string staffNumber;
        public int personid;

        public UserInfo(int id, string loginName, string nickname, string sessionid, int? employID, string employNnumber, int? staffid, string staffNumber, int personid)
        {
            this.id = id;
            this.loginName = loginName;
            this.nickname = nickname;
            this.sessionid = sessionid;
            this.employID = employID;
            this.employNnumber = employNnumber;
            this.staffid = staffid;
            this.staffNumber = staffNumber;
            this.personid = personid;
        }

        public bool hasValidEmploynumber()
        {
            bool result = true;
            if (employID == null || employID == 0)
            {
                result = false;
            }
            return result;
        }

    }
}