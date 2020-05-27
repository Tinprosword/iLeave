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
        public string loginid;
        public string nickname;
        public string position_leaveType;

        public UserInfo(int id, string loginid, string nickname, string position_leaveType)
        {
            this.id = id;
            this.loginid = loginid;
            this.nickname = nickname;
            this.position_leaveType = position_leaveType;
        }
    }
}