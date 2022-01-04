using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using LSLibrary;

namespace ut
{

    #region test workflow.

    public class User
    {
        public int personid { get; set; }
        public int userid { get; set; }
        public int empolyid { get; set; }
        public int staffid { get; set; }
        public int firsteid { get; set; }
        public int alid { get; set; }
        public string alCode { get; set; }

        public User(int uid, int eid, int sid, int feid, int _alid, string _alcode,int _pid)
        {
            userid = uid;
            empolyid = eid;
            staffid = sid;
            firsteid = feid;
            alid = _alid;
            alCode = _alcode;
            personid = _pid;
        }

        public int addLeave(List<MODEL.Apply.apply_LeaveData> originDetail)
        {
            int result = 0;
            string errorMsg = "";
            result = BLL.Leave.InsertLeave(originDetail, userid, empolyid, staffid, "", ref errorMsg, firsteid, true);
            return result;
        }

    }


    [TestClass]
    public class UT_Workflow
    {
        public User applyer_103 = null;
        public User approver_102 = null;
        public User approver_101 = null;

        public string message = "";

        //25628
        //25628   25627     25626
        //AL12 AL12ALS  AL12CLY
        //14  2110  2109

        public UT_Workflow()
        {
            var allLeaveinfo = BLL.CodeSetting.GetAllLeaveInfo();

            var u103= BLL.User_wsref.GetPersonBaseInfoByPid(25628);
            var u102 = BLL.User_wsref.GetPersonBaseInfoByPid(25627);
            var u101 = BLL.User_wsref.GetPersonBaseInfoByPid(25626);

            string lcode1 = "AL12";
            string lcode2 = "AL12ALS";
            string lcode3 = "AL12CLY";

            int code1 = allLeaveinfo.Where(x => x.Code == lcode1).FirstOrDefault().ID;
            int code2 = allLeaveinfo.Where(x => x.Code == lcode2).FirstOrDefault().ID;
            int code3 = allLeaveinfo.Where(x => x.Code == lcode3).FirstOrDefault().ID;

            applyer_103 = new User(u103[0].u_id??0, u103[0].e_id??0,u103[0].s_id??0, u103[0].e_id ?? 0, code1, lcode1,u103[0].p_id);
            approver_102 = new User(u102[0].u_id ?? 0, u102[0].e_id ?? 0, u102[0].s_id ?? 0, u102[0].e_id ?? 0, code2, lcode2, u102[0].p_id);
            approver_101 = new User(u101[0].u_id ?? 0, u101[0].e_id ?? 0, u101[0].s_id ?? 0, u101[0].e_id ?? 0, code3, lcode3, u101[0].p_id);
        }

        
        //測試方法用戶是固定的，可以構造函數的時候變更。來達到測試不同用戶。裡面hardcode .來簡化代碼。
        [TestMethod]
        public void StartTest()
        {
            
            Console.WriteLine("Start Test");
            List<int> waitlist = new List<int>();
            List<int> histroyList = new List<int>();

            int waitingRequestid= UT_Waiting(waitlist, histroyList);
        }


        public int UT_Waiting(List<int> preWait, List<int> preHistory)
        {
            int result = 0;
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();

            //insert
            MODEL.Apply.apply_LeaveData theLeaveInfo = new MODEL.Apply.apply_LeaveData(applyer_103.alid, applyer_103.alCode, applyer_103.alCode, 0, new DateTime(2022, 1, 1), 8);
            leaveinfos.Add(theLeaveInfo);

            result = applyer_103.addLeave(leaveinfos);

            if (result <= 0)
            {
                throw new Exception("error on insert");
            }
            Console.WriteLine("add:"+result.ToString());

            //applyer check myleave_wait
            preWait.Add(result);
            checkMyLeaveMater_wait(applyer_103, preWait);

            //applyer check myleave_history
            checkMyLeaveMater_history(applyer_103, preHistory);

            return result;
        }

        public string checkMyLeaveMater_wait(User user,List<int> requestids)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyLeaveMaster(applyer_103.personid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022);
            var rids=mywait.Select(x => x.RequestID).ToList();
            if (rids.Count() == requestids.Count())
            {
                foreach (int item in requestids)
                {
                    if (!rids.Contains(item))
                    {
                        throw new Exception("not contain :"+item);
                    }
                }
            }
            else
            {
                throw new Exception("total is not equel");
            }
            Console.WriteLine("total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyLeaveMater_history(User user, List<int> requestids)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyLeaveMaster(applyer_103.personid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022);
            var rids = mywait.Select(x => x.RequestID).ToList();
            if (rids.Count() == requestids.Count())
            {
                foreach (int item in requestids)
                {
                    if (!rids.Contains(item))
                    {
                        throw new Exception("not contain :" + item);
                    }
                }
            }
            else
            {
                throw new Exception("total is not equel");
            }
            Console.WriteLine("total is " + rids.Count().ToString());
            return result;
        }



    }


    #endregion


    #region pre test


    public class mytestClass
    {
        public int getint()
        {
            return 1;
        }
    }

    public static class MytestClassExtend
    {
        public static int getint2(this mytestClass mytest)
        {
            return 3;
        }
    }

   



    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ICSHelper iCSHelper = new ICSHelper("mycalendar", "my first metting");
            ICSHelper.DataItem item = new ICSHelper.DataItem(DateTime.Now, DateTime.Now, "my first calendar","test location", "test my desc",true);
            ICSHelper.DataItem item2 = new ICSHelper.DataItem(DateTime.Now.AddHours(4), DateTime.Now.AddHours(5), "my secode calendar", "test location", "test my desc", false);
            iCSHelper.InsertItem(item);
            iCSHelper.InsertItem(item2);
            iCSHelper.Save("c:\\abc\\");
        }


        [TestMethod]
        public void testjiami()
        {
            string abc = LSLibrary.DesStand.encrypt("http://192.168.19.210:8099", "abd58237", "abd58237");
            string docode = LSLibrary.DesStand.decrypt(abc, "abd58237", "abd58237");
            int a = 4;
        }


        [TestMethod]
        public void TestMethod222()
        {
            List<int> allints = new List<int>();
            allints.Add(1);
            allints.Add(2);
            allints.Add(3);

            int[] validate = { 2 };

         //   var a= allints.Where(x => validate.Contains(x));
            Func<int, bool> func = new Func<int, bool>(myContain);

           var bb=  allints.Where(func).ToArray();


            mytestClass a = new mytestClass();
            a.getint2();


            var c = "aa";
        }

       
        public bool myContain(int a)
        {
            if (a > 2)
            {
                return true;
            }
            else
            {

                return false;
            }
        }


        [TestMethod]
        public void TestMethod222333()
        {
            double a = 3.334324;
            string b = a.ToString("0.##");
            int c = 4;
        }






        [TestMethod]
        public void testbll4()
        {
            WebServiceLayer.WebReference_user.UserManagementV2 userManagement = new WebServiceLayer.WebReference_user.UserManagementV2();
        }



        [TestMethod]
        public void linqTrainning()
        {
            WebServiceLayer.WebReference_user.UserManagementV2 userManagement = new WebServiceLayer.WebReference_user.UserManagementV2();


        }


        [TestMethod]
        public void testtryParse()
        {
            string a = "abc";
            DateTime tt = System.DateTime.Now;
            DateTime.TryParse(a, out tt);
            int abc = 44;
        }


        [TestMethod]
        public void Txml_0102()
        {
            string xml1 = @"<?xml version='1.0' encoding='utf-8'?>
<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
  <soap:Body>
    <InsertStaffLeaveDetailsResponse xmlns='http://tempuri.org/'>
      <InsertStaffLeaveDetailsResult>
        <ProcessID>int</ProcessID>
        <ProcessIDList>
          <int>int</int>
          <int>int</int>
        </ProcessIDList>
        <AffectedRecordCount>int</AffectedRecordCount>
        <ErrorMessage>string</ErrorMessage>
        <CompletedRatio>string</CompletedRatio>
        <IsMessageList>boolean</IsMessageList>
        <ErrorMessageList>
          <ErrorMessageItem>
            <IsMessageList>boolean</IsMessageList>
            <Message>string</Message>
            <MessageParameter1>string</MessageParameter1>
            <MessageParameter2>string</MessageParameter2>
            <MessageParameter3>string</MessageParameter3>
            <Severity>ERROR or WARNING or INFORMATION or VALIDATION</Severity>
          </ErrorMessageItem>
          <ErrorMessageItem>
            <IsMessageList>boolean</IsMessageList>
            <Message>string</Message>
            <MessageParameter1>string</MessageParameter1>
            <MessageParameter2>string</MessageParameter2>
            <MessageParameter3>string</MessageParameter3>
            <Severity>ERROR or WARNING or INFORMATION or VALIDATION</Severity>
          </ErrorMessageItem>
        </ErrorMessageList>
      </InsertStaffLeaveDetailsResult>
    </InsertStaffLeaveDetailsResponse>
  </soap:Body>
</soap:Envelope>";

            LSLibrary.XmlHelper xmlHelper = new LSLibrary.XmlHelper(xml1, true);
            DataSet ds = xmlHelper.GetDataTableByXmlString();

            DataSet ds2 = LSLibrary.XmlHelper.ConvertXMLToDataSet(xml1);

            string abc = LSLibrary.XmlHelper.ConvertDataSetToXML(ds);

            int a = 4;
        }
    }

    #endregion
}