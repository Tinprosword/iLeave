﻿using DAL.WebReference_codesetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CodeSetting
    {
        public static LeaveInfo[] GetLeaveInfo()
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.CodeSetting.GetLeaveInfo();
        }
    }
}