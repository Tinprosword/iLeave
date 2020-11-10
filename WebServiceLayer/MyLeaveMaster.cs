﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceLayer.MyModel
{
    public class LeaveMaster
    {
        private WebServiceLayer.WebReference_leave.LeaveRequestMaster data;

        public LeaveMaster(WebServiceLayer.WebReference_leave.LeaveRequestMaster _data)
        {
            data = _data;
        }

        public string Info_GetFromto()
        {
            string result = "{0} To {1}({2} Day)";
            result= string.Format(result, data.leavefrom.ToString("yyyy-MM-dd"), data.leaveto.ToString("yyyy-MM-dd"), data.totaldays);
            return result;
        }

        public string Info_GetBalance()
        {
            string result = "";
            result = data.maxLeaveCode + ":x Day";
            return result;
        }

        public string Info_GetApplydate()
        {
            string result = "Apply Date:" + data.createDate.ToString("yyyy-MM-dd");
            return result;
        }

        //生成<a> .连接后来处理图片的copy和现实.
        public string Info_GetAttachment()
        {
            string result = data.paths == null ? "" : data.paths;
            List<string> pics = result.Split(new char[] { '|' },StringSplitOptions.RemoveEmptyEntries).ToList();
            //"<a href="xx.jpg">aa</a>"
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pics.Count; i++)
            {
                string urlpath = pics[i];
                sb.Append("<a href='showpic2.aspx?dbpath="+urlpath+"'>aa.jpg</a>&nbsp;");
            }
            return sb.ToString();
        }

        public bool Info_BShowPanel(int usertype,int bigrange,int requestBigrange)
        {
            return true;
        }
    }
}