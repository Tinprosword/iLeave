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
    //測試用例包括1.所有狀態 7種，2.包括單，多條。3包括跨年。
    //不包括，1.approver 自己申請。
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
        public double workh { get; set; }
        public string name { get; set; }
        public List<int> mywait { get; set; }
        public List<int> myHistory { get; set; }
        public List<int> myManagewait { get; set; }
        public List<int> myManageHistory { get; set; }


        public User(int uid, int eid, int sid, int feid, int _alid, string _alcode,int _pid,double wh,string _name)
        {
            userid = uid;
            empolyid = eid;
            staffid = sid;
            firsteid = feid;
            alid = _alid;
            alCode = _alcode;
            personid = _pid;
            workh = wh;
            name = _name;
            mywait = new List<int>();
            myHistory = new List<int>();
            myManagewait = new List<int>();
            myManageHistory = new List<int>();
        }

        public void AttachLeaves(DateTime dt, int section,List<MODEL.Apply.apply_LeaveData> Predata)
        {
            MODEL.Apply.apply_LeaveData newLeave = new MODEL.Apply.apply_LeaveData(alid, alCode, alCode, section, dt, workh);
            if (Predata == null)
            {
                Predata = new List<MODEL.Apply.apply_LeaveData>();
            }
            Predata.Add(newLeave);
        }

        public int AddLeave(List<MODEL.Apply.apply_LeaveData> leaveinfos)
        {
            int result = 0;

            string errorMsg = "";
            result = BLL.Leave.InsertLeave(leaveinfos, userid, empolyid, staffid, "", ref errorMsg, firsteid, true);

            if (result <= 0)
            {
                throw new Exception("error on insert");
            }
            Console.WriteLine(name+ ":add:" + result.ToString());
            Console.WriteLine("");
            return result;
        }

        public string ApproveLeave(int requestid,string remark)
        {
            string result = "";
            BLL.workflow.ApproveRequest_leave(requestid, userid, remark, out result);
            return result;
        }

        public string RejectLeave(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.RejectRequest_leave(requestid, userid, remark, out result);
            return result;
        }

        public string WithDrawLeave(int rid)
        {
            string result = "";
            BLL.workflow.WithDrawRequest_leave(rid, userid, "withdraw", out result);
            return result;
        }


        public int cancelLeave(int rid)
        {
            string msg = "";
            return BLL.workflow.CancelRequest_leave(rid, userid, "withdraw", out msg);
        }


        public void AttachCLOTS(MODEL.CLOT.enum_clotType type,DateTime dt, int fh,int fm,int th,int tm, string remk, string numh, List<MODEL.CLOT.CLOTItem> Predata)
        {
            MODEL.CLOT.CLOTItem newLeave = new MODEL.CLOT.CLOTItem(type, dt, fh, fm, th, tm,remk, numh);
            if (Predata == null)
            {
                Predata = new List<MODEL.CLOT.CLOTItem>();
            }
            Predata.Add(newLeave);
        }

        public int AddCLOT()
        {
            int result = 0;

            string errorMsg = "";


            //result = BLL.CLOT.InsertCLOTRequests();

            if (result <= 0)
            {
                throw new Exception("error on insert");
            }
            Console.WriteLine(name + ":add:" + result.ToString());
            Console.WriteLine("");
            return result;
        }
    }




    [TestClass]
    public class UT_Workflow
    {
        //25628
        //25628   25627     25626
        //AL12 AL12ALS  AL12CLY
        //14  2110  2109


        public User user_103 = null;
        public User user_102 = null;//approrver1
        public User user_101 = null;//apporver2


        [TestMethod]
        public void add20batch()
        {
            DateTime dt = new DateTime(2022, 1, 1);
            for (int i = 0; i < 25; i++)
            {
                Scene_Approved1(dt);
                dt = dt.AddDays(1);
            }
        }

        public void Scene_Approved1(DateTime dt)
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(dt, 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);


            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);


            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);


            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }

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

            user_103 = new User(u103[0].u_id??0, u103[0].e_id??0,u103[0].s_id??0, u103[0].e_id ?? 0, code1, lcode1,u103[0].p_id,8,"103");
            user_102 = new User(u102[0].u_id ?? 0, u102[0].e_id ?? 0, u102[0].s_id ?? 0, u102[0].e_id ?? 0, code2, lcode2, u102[0].p_id,8,"102");
            user_101 = new User(u101[0].u_id ?? 0, u101[0].e_id ?? 0, u101[0].s_id ?? 0, u101[0].e_id ?? 0, code3, lcode3, u101[0].p_id,8,"101");

        }

        
        //測試方法用戶是固定的，可以構造函數的時候變更。來達到測試不同用戶。裡面hardcode .來簡化代碼。
        [TestMethod]
        public void StartTest_clot()
        {
            Console.WriteLine("Start Test");
            Scene_waiting_clot();
            //Scene_Approved_clot();
            //Scene_Reject1_clot();
            //Scene_WithDraw_clot();
            //Scene_wc_clot();
            //Scene_wc_approved_clot();
            //Scene_wc_reject_clot();
        }

        private void Scene_waiting_clot()
        {

            //List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            //user_103.AttachLeaves(new DateTime(2022, 1, 1), 0, leaveinfos);
            //user_103.AttachLeaves(new DateTime(2023, 1, 1), 0, leaveinfos);
            //int Requestid = user_103.AddLeave(leaveinfos);
            //user_103.mywait.Add(Requestid);
            //user_102.myManagewait.Add(Requestid);

            //checkAllUer();
            //CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);
        }




        #region clot


        #endregion

        #region leave
        [TestMethod]
        public void StartTest_leave()
        {
            Console.WriteLine("Start Test");
            Scene_waiting();
            Scene_Approved();
            Scene_Reject1();
            Scene_WithDraw();
            Scene_wc();
            Scene_wc_approved();
            Scene_wc_reject();
        }

        public void Scene_waiting()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 1), 0, leaveinfos);
            user_103.AttachLeaves(new DateTime(2023, 1, 1), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);
            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);
        }

        private void CheckLeaveRequestStatus(int Requestid, BLL.GlobalVariate.ApprovalRequestStatus status)
        {
            var request= BLL.Leave.GetRequestMasterByRequestID(Requestid);
            if (request.Status != (byte)status)
            {
                throw new Exception("status is match");
            }
        }

        public void Scene_Approved()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 2), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            
            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            
            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            
            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }


        public void Scene_Reject1()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 3), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.RejectLeave(Requestid, "a2");

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.REJECT);
        }

        public void Scene_WithDraw()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 4), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_103.WithDrawLeave(Requestid);

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
        }


        public void Scene_wc()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 5), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid=user_103.cancelLeave(Requestid);

            user_103.mywait.Add(cancelRid);
            //todo 0 ，嚴格來說，不應該有。
            //user_103.myHistory.Remove(Requestid);

            user_102.myManagewait.Add(cancelRid);
            user_102.myManageHistory.Remove(Requestid);
            checkAllUer();
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);
        }

        public void Scene_wc_approved()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 6), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid = user_103.cancelLeave(Requestid);

            user_103.mywait.Add(cancelRid);
            //todo 0 ，嚴格來說，不應該有。
            //user_103.myHistory.Remove(Requestid);

            user_102.myManagewait.Add(cancelRid);
            user_102.myManageHistory.Remove(Requestid);
            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_102.ApproveLeave(cancelRid, "wc1");

            user_102.myManagewait.Remove(cancelRid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(cancelRid);
            user_101.myManageHistory.Remove(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_101.ApproveLeave(cancelRid, "wc2");
            user_101.myManagewait.Remove(cancelRid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(cancelRid);
            //todo 0 上面沒有減去首請求，這裡所有還有首請求。不需要再添加。
            //user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL);
        }

        public void Scene_wc_reject()
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            user_103.AttachLeaves(new DateTime(2022, 1, 7), 0, leaveinfos);
            user_103.AttachLeaves(new DateTime(2022, 1, 8), 0, leaveinfos);
            int Requestid = user_103.AddLeave(leaveinfos);

            user_103.mywait.Add(Requestid);
            user_102.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);


            user_102.ApproveLeave(Requestid, "a1");
            
            user_102.myManagewait.Remove(Requestid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");
            

            user_101.myManagewait.Remove(Requestid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(Requestid);
            user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid = user_103.cancelLeave(Requestid);

            user_103.mywait.Add(cancelRid);
            //todo 0 ，嚴格來說，不應該有。
            //user_103.myHistory.Remove(Requestid);

            user_102.myManagewait.Add(cancelRid);
            user_102.myManageHistory.Remove(Requestid);
            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_102.ApproveLeave(cancelRid, "wc1");

            user_102.myManagewait.Remove(cancelRid);
            user_102.myManageHistory.Add(Requestid);

            user_101.myManagewait.Add(cancelRid);
            user_101.myManageHistory.Remove(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_101.RejectLeave(cancelRid, "rej wc2");
            user_101.myManagewait.Remove(cancelRid);
            user_101.myManageHistory.Add(Requestid);

            user_103.mywait.Remove(cancelRid);
            //todo 0 上面沒有減去首請求，這裡所有還有首請求。不需要再添加。
            //user_103.myHistory.Add(Requestid);

            checkAllUer();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.REJECT);
        }


        private void checkAllUer()
        {
            checkAll(user_103);
            checkAll(user_102);
            checkAll(user_101);
        }

        private void checkAll(User user)
        {
            checkMyLeaveMater_wait(user);
            checkMyLeaveMater_history(user);
            checkMyManageLeaveMater_waiting(user);
            checkMyManageLeaveMater_history(user);
            Console.WriteLine("");
        }

        

        public string checkMyLeaveMater_wait(User user)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyLeaveMaster(user.personid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022);
            var rids=mywait.Select(x => x.RequestID).ToList();
            CheckResult(user.mywait, rids);
            Console.WriteLine(user.name + ":MyLeave_wait:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyLeaveMater_history(User user)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyLeaveMaster(user.personid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022);
            var rids = mywait.Select(x => x.RequestID).ToList();
            CheckResult(user.myHistory, rids);
            Console.WriteLine(user.name+ ":MyLeave_history:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageLeaveMater_waiting(User user)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyManageLeaveMaster(user.userid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022,null);
            var rids = mywait.Select(x => x.RequestID).ToList();
            CheckResult(user.myManagewait, rids);
            Console.WriteLine(user.name + "MyManageLeaveMater_waiting:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageLeaveMater_history(User user)
        {
            string result = "";
            var mywait = BLL.Leave.GetMyManageLeaveMaster(user.userid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022, null);
            var rids = mywait.Select(x => x.RequestID).ToList();
            CheckResult(user.myManageHistory, rids);
            Console.WriteLine(user.name + ":MyManageLeaveMater_history:total is " + rids.Count().ToString());
            return result;
        }

        private void CheckResult(List<int> wantBe, List<int> data)
        {
            if (wantBe.Count() == data.Count())
            {
                foreach (int item in data)
                {
                    if (!wantBe.Contains(item))
                    {
                        throw new Exception("not contain :" + item);
                    }
                }
            }
            else
            {
                throw new Exception("total is not equel");
            }
        }
        #endregion

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