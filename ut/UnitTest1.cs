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
using System.Net;
using System.Security;
using System.Net.Mail;

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
        public List<int> mywaitLeave { get; set; }
        public List<int> myHistoryLeave { get; set; }
        public List<int> myManagewaitLeave { get; set; }
        public List<int> myManageHistoryLeave { get; set; }
        public List<Staff_Leave> myLeaves { get; set; }

        public List<int> myWaitClot { get; set; }
        public List<int> myHistroyClot { get; set; }
        public List<int> myManageWaitClot { get; set; }
        public List<int> myManageHistroyClot { get; set; }

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
            mywaitLeave = new List<int>();
            myHistoryLeave = new List<int>();
            myManagewaitLeave = new List<int>();
            myManageHistoryLeave = new List<int>();
            myLeaves = new List<Staff_Leave>();

            myWaitClot = new List<int>();
            myHistroyClot = new List<int>();
            myManageWaitClot = new List<int>();
            myManageHistroyClot = new List<int>();
        }

        #region leave function
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
            string remark = System.DateTime.Now.ToString("yyyyMMdd") + empolyid.ToString();
            result = BLL.Leave.InsertLeave(leaveinfos, userid, empolyid, staffid,remark, ref errorMsg, firsteid, true);

            if (result <= 0)
            {
                throw new Exception("error on insert:" + errorMsg);
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
            return BLL.workflow.CancelRequest_leave(rid, userid, "withdraw").mResult;
        }

        public void UpdateALLeave()
        {
            BLL.Leave.UpdateTodayLeaveBalanceToTable(empolyid);
        }

        public string ApproveCancelLeave(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.ApprovalCancelRequest_leave(requestid, userid, remark, out result);
            return result;
        }

        public string RejectCancelLeave(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.RejectCancelRequest_leave(requestid, userid, remark, out result);
            return result;
        }


        #endregion


        #region clot function
        public void AttachCLOTS(MODEL.CLOT.enum_clotType type,DateTime dt, int fh,int fm,int th,int tm, string remk, string numh,int section, List<MODEL.CLOT.CLOTItem> Predata)
        {
            MODEL.CLOT.CLOTItem newLeave = new MODEL.CLOT.CLOTItem(type, dt, fh, fm, th, tm,remk, numh,section);
            if (Predata == null)
            {
                Predata = new List<MODEL.CLOT.CLOTItem>();
            }
            Predata.Add(newLeave);
        }

        public List<int> AddCLOT(List<MODEL.CLOT.CLOTItem> clots)
        {
            List<int> result = new List<int>();

            string errorMsg = "";


            result = BLL.CLOT.InsertCLOTRequests(clots, userid, empolyid);

            if (result==null || result.Count()==0)
            {
                throw new Exception("error on insert");
            }
            Console.WriteLine(name + ":add:" + result.ToString());
            Console.WriteLine("");
            return result;
        }

        public string ApproveCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.ApproveRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void ApproveCLOT(List<int> requestid, string remark)
        {
            foreach (var item in requestid)
            {
                ApproveCLOT(item, remark);
            }
        }

        public string RejectCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.RejectRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void RejectCLOT(List<int> requestids, string remark)
        {
            foreach (var item in requestids)
            {
                RejectCLOT(item, remark);
            }
        }

        public string WithDrawCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.WithDrawRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void WithDrawCLOT(List<int> requestids, string remark)
        {
            foreach (var item in requestids)
            {
                WithDrawCLOT(item, remark);
            }
        }

        public string CancelCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.CancelRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void CancelCLOT(List<int> requestids, string remark)
        {
            foreach (var item in requestids)
            {
                CancelCLOT(item, remark);
            }
        }


        public string ApproveCancelCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.ApprovalCancelRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void ApproveCancelCLOT(List<int> requestids, string remark)
        {
            foreach (var item in requestids)
            {
                ApproveCancelCLOT(item, remark);
            }
        }

        public string RejectCancelCLOT(int requestid, string remark)
        {
            string result = "";
            BLL.workflow.RejectCancelRequest_leave_clot(requestid, userid, remark, out result);
            return result;
        }

        public void RejectCancelCLOT(List<int> requestids, string remark)
        {
            foreach (var item in requestids)
            {
                RejectCancelCLOT(item, remark);
            }
        }


        #endregion


        #region check function leave
        public string checkMyLeaveMater_wait()
        {
            string result = "";
            var mywaitaa = BLL.Leave.GetMyLeaveMaster(personid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022);
            var rids = mywaitaa.Select(x => x.RequestID).ToList();
            CheckResult(mywaitLeave, rids);
            Console.WriteLine(name + ":MyLeave_wait:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyLeaveMater_history()
        {
            string result = "";
            var mywait = BLL.Leave.GetMyLeaveMaster(personid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022);
            var rids = mywait.Select(x => x.RequestID).ToList();
            CheckResult(myHistoryLeave, rids);
            Console.WriteLine(name + ":MyLeave_history:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageLeaveMater_waiting()
        {
            string result = "";
            var mywait = BLL.Leave.GetMyManageLeaveMaster(userid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022, null);
            var rids = mywait.Select(x => x.RequestID).ToList();
            CheckResult(myManagewaitLeave, rids);
            Console.WriteLine(name + "MyManageLeaveMater_waiting:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageLeaveMater_history()
        {
            string result = "";
            var mywaitaa = BLL.Leave.GetMyManageLeaveMaster(userid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022, null);
            var rids = mywaitaa.Select(x => x.RequestID).ToList();
            CheckResult(myManageHistoryLeave, rids);
            Console.WriteLine(name + ":MyManageLeaveMater_history:total is " + rids.Count().ToString());
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

        public static bool chang_slf_debug =true;

        public string checkMyLeave()
        {
            string result = "";
            if (!chang_slf_debug)
            {
                foreach (var item in myLeaves)
                {
                    int[] tempCheck = BLL.Leave.CheckLeaveExist(staffid, item.date, item.leaveid, item.sectionid);
                    if (tempCheck[0] == -1)
                    {
                        throw new Exception("no " + item.date.ToShortDateString());
                    }
                    if (tempCheck[1] != myLeaves.Count())
                    {
                        throw new Exception("myleave count is not match");
                    }
                }
            }
            return result;
        }
        #endregion

        #region check function clot
        public string checkMyCLOT_wait()
        {
            string result = "";
            var mywaitaa = BLL.CLOT.GetMyCLOTUID(userid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022);
            var rids = mywaitaa.Select(x => x.ID).ToList();
            CheckResult(myWaitClot, rids);
            Console.WriteLine(name + ":Myclot_wait:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyCLOT_history()
        {
            string result = "";
            var mywait = BLL.CLOT.GetMyCLOTUID(userid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022);
            var rids = mywait.Select(x => x.ID).ToList();
            CheckResult(myHistroyClot, rids);
            Console.WriteLine(name + ":MyClot_history:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageCLOT_waiting()
        {
            string result = "";
            var mywait = BLL.CLOT.GetMyManageClOT(userid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, 2022, null);
            var rids = mywait.Select(x => x.ID).ToList();
            CheckResult(myManageWaitClot, rids);
            Console.WriteLine(name + "MyManageClot_waiting:total is " + rids.Count().ToString());
            return result;
        }

        public string checkMyManageCLOT_history()
        {
            string result = "";
            var mywaitaa = BLL.CLOT.GetMyManageClOT(userid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, 2022, null);
            var rids = mywaitaa.Select(x => x.ID).ToList();
            CheckResult(myManageHistroyClot, rids);
            Console.WriteLine(name + ":MyManageCLOT_history:total is " + rids.Count().ToString());
            return result;
        }
        #endregion
    }


    public class Staff_Leave
    {
        public static int autoid = 1;

        public int id { get; set; }
        public int staffid { get; set; }
        public System.DateTime date { get; set; }
        public int leaveid { get; set; }
        public int sectionid { get; set; }

        public Staff_Leave(int sid, DateTime _date, int leave_id, int section_id)
        {
            id = autoid;
            staffid = sid;
            date = _date;
            leaveid = leave_id;
            sectionid = section_id;
            autoid++;
        }

        public static void DeleteItem(List<Staff_Leave> data, int _id)
        {
            var deleteitem = data.Where(x => x.id == _id).FirstOrDefault();
            if (deleteitem != null)
            {
                data.Remove(deleteitem);
            }
        }
    }


    [TestClass]
    public class UT_Workflow
    {
        //25628
        //25628   25627     25626
        //AL12 AL12ALS  AL12CLY
        //14  2110  2109

        public User user_103 = null;//user 
        public User user_102 = null;//approrver1
        public User user_101 = null;//apporver2
        public User user_104 = null;//user
        public User user_105 = null;

        public int u101pid = 25628;
        public int u102pid = 25629;
        public int u103pid = 25630;
        public int u104pid = 25631;
        public int u105pid = 25632;

        public string u101alcode = "AL14";
        public string u102alcode = "SL";
        public string u103alcode = "AL14";
        public string u104alcode = "AL07";
        public string u105alcode = "AL14";


        public UT_Workflow()
        {
            var allLeaveinfo = BLL.CodeSetting.GetAllLeaveInfo();

            var u103 = BLL.User_wsref.GetPersonBaseInfoByPid(u103pid);
            var u102 = BLL.User_wsref.GetPersonBaseInfoByPid(u102pid);
            var u101 = BLL.User_wsref.GetPersonBaseInfoByPid(u101pid);
            var u104 = BLL.User_wsref.GetPersonBaseInfoByPid(u104pid);
            var u105 = BLL.User_wsref.GetPersonBaseInfoByPid(u105pid);

            string lcode1 = u101alcode;
            string lcode2 = u102alcode;
            string lcode3 = u103alcode;
            string lcode4 = u104alcode;
            string lcode5 = u105alcode;

            int code1 = allLeaveinfo.Where(x => x.Code == lcode1).FirstOrDefault().ID;
            int code2 = allLeaveinfo.Where(x => x.Code == lcode2).FirstOrDefault().ID;
            int code3 = allLeaveinfo.Where(x => x.Code == lcode3).FirstOrDefault().ID;
            int code4 = allLeaveinfo.Where(x => x.Code == lcode4).FirstOrDefault().ID;
            int code5= allLeaveinfo.Where(x => x.Code == lcode5).FirstOrDefault().ID;

            user_103 = new User(u103[0].u_id ?? 0, u103[0].e_id ?? 0, u103[0].s_id ?? 0, u103[0].e_id ?? 0, code1, lcode1, u103[0].p_id, 8, "103");
            user_102 = new User(u102[0].u_id ?? 0, u102[0].e_id ?? 0, u102[0].s_id ?? 0, u102[0].e_id ?? 0, code2, lcode2, u102[0].p_id, 8, "102");
            user_101 = new User(u101[0].u_id ?? 0, u101[0].e_id ?? 0, u101[0].s_id ?? 0, u101[0].e_id ?? 0, code3, lcode3, u101[0].p_id, 8, "101");
            user_104= new User(u104[0].u_id ?? 0, u104[0].e_id ?? 0, u104[0].s_id ?? 0, u104[0].e_id ?? 0, code4, lcode4, u104[0].p_id, 8, "104");
            user_105 = new User(u105[0].u_id ?? 0, u105[0].e_id ?? 0, u105[0].s_id ?? 0, u105[0].e_id ?? 0, code5, lcode5, u105[0].p_id, 8, "105");
        }

        #region common function clot


        private void checkAllUer_leaveMyAndMyManageRecord()
        {
            checkleaveMyAndMyManageRecordByUser(user_103);
            checkleaveMyAndMyManageRecordByUser(user_102);
            checkleaveMyAndMyManageRecordByUser(user_101);
        }

        private void checkleaveMyAndMyManageRecordByUser(User user)
        {
            user.checkMyLeaveMater_wait();
            user.checkMyLeaveMater_history();
            user.checkMyManageLeaveMater_waiting();
            user.checkMyManageLeaveMater_history();
            user.checkMyLeave();
            Console.WriteLine("");
        }

        private void checkAllUer_CLOTMyAndMyManageRecord()
        {
            checkCLOTMyAndMyManageRecordByUser(user_103);
            checkCLOTMyAndMyManageRecordByUser(user_102);
            checkCLOTMyAndMyManageRecordByUser(user_101);
        }


        private void checkCLOTMyAndMyManageRecordByUser(User user)
        {
            user.checkMyCLOT_wait();
            user.checkMyCLOT_history();
            user.checkMyManageCLOT_waiting();
            user.checkMyManageCLOT_history();
            Console.WriteLine("");
        }

        private void CheckLeaveRequestStatus(int Requestid, BLL.GlobalVariate.ApprovalRequestStatus status)
        {
            bool isok = false;
            var request = BLL.Leave.GetRequestMasterByRequestID(Requestid);
            if (request != null)
            {
                isok = request.Status == (byte)status;
            }
            if (isok == false)
            {
                throw new Exception("status is not match or not exists");
            }
        }

        private void CheckClotRequestStatus(List<int> Requestid, BLL.GlobalVariate.ApprovalRequestStatus status)
        {
            var data = BLL.CLOT.GetCLOTDetails(Requestid.ToArray());

            foreach (var item in data)
            {
                if (item.Status != (int)status)
                {
                    throw new Exception(item.ID.ToString()+ "'s status is not match");
                }
            }

        }


        #endregion'

        #region leave
        [TestMethod]
        public void StartTest_leavev_testNotice()
        {
            User applyer = user_104;
            applyer.UpdateALLeave();
            Scene_Approvedv2(applyer);
        }


        [TestMethod]
        public void StartTest_leavev_all()
        {
            Console.WriteLine("Start Test");
            User applyer = user_102;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);//1.1  ; 10.1
            Scene_Approvedv2(applyer);//1.2
            Scene_Reject1v2(applyer);//1.3
            Scene_WithDrawv2(applyer);//1.4
            Scene_wcv2(applyer);//1.5
            Scene_wc_approvedv2(applyer);//1.6
            Scene_wc_rejectv2(applyer);//1.7 ; 1.8


            applyer = user_101;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);

            applyer = user_103;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);

            applyer = user_104;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);


            applyer = user_105;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);
        }


        [TestMethod]
        public void StartTest_leavev_u102()
        {
            Console.WriteLine("Start Test");
            User applyer = user_102;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);
        }

        [TestMethod]
        public void StartTest_leavev_u103()
        {
            Console.WriteLine("Start Test");
            User applyer = user_103;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);
        }

        [TestMethod]
        public void StartTest_leavev_u101()
        {
            Console.WriteLine("Start Test");
            User applyer = user_101;

            applyer.UpdateALLeave();
            Scene_waitingv2(applyer);
            Scene_Approvedv2(applyer);
            Scene_Reject1v2(applyer);
            Scene_WithDrawv2(applyer);
            Scene_wcv2(applyer);
            Scene_wc_approvedv2(applyer);
            Scene_wc_rejectv2(applyer);
        }

        public void Scene_waitingv2(User applyer)
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(new DateTime(2022, 1, 1), 0, leaveinfos);
            applyer.AttachLeaves(new DateTime(2022, 10, 1), 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);
            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);
        }


        public void Scene_Approvedv2(User applyer)
        {
            DateTime d1 = new DateTime(2022, 1, 2);

            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(d1, 1, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);

            Staff_Leave leaveItme = new Staff_Leave(applyer.staffid, d1, applyer.alid, 0);
            applyer.myLeaves.Add(leaveItme);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }


        public void Scene_Reject1v2(User applyer)
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(new DateTime(2022, 1, 3), 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "approve remark");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.RejectLeave(Requestid, "reject remark");

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.REJECT);
        }

        public void Scene_WithDrawv2(User applyer)
        {
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(new DateTime(2022, 1, 4), 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            applyer.WithDrawLeave(Requestid);

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);



            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
        }


        public void Scene_wcv2(User applyer)
        {
            DateTime d1 = new DateTime(2022, 1, 5);
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(d1, 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);

            applyer.myLeaves.Add(new Staff_Leave(applyer.staffid, d1, applyer.alid, 0));
            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid = applyer.cancelLeave(Requestid);

            applyer.mywaitLeave.Add(cancelRid);
            //applyer.myHistory.Remove(Requestid);

            user_102.myManagewaitLeave.Add(cancelRid);
            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);
        }

        public void Scene_wc_approvedv2(User applyer)
        {
            DateTime d1 = new DateTime(2022, 1, 6);
            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(d1, 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);

            Staff_Leave leaveItme = new Staff_Leave(applyer.staffid, d1, applyer.alid, 0);
            applyer.myLeaves.Add(leaveItme);
            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid = applyer.cancelLeave(Requestid);

            applyer.mywaitLeave.Add(cancelRid);
            //applyer.myHistory.Remove(Requestid);

            user_102.myManagewaitLeave.Add(cancelRid);
            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_102.ApproveCancelLeave(cancelRid, "wc1");

            user_102.myManagewaitLeave.Remove(cancelRid);
            user_102.myManageHistoryLeave.Add(cancelRid);

            user_101.myManagewaitLeave.Add(cancelRid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_101.ApproveCancelLeave(cancelRid, "wc2");
            user_101.myManagewaitLeave.Remove(cancelRid);
            user_101.myManageHistoryLeave.Add(cancelRid);

            applyer.mywaitLeave.Remove(cancelRid);
            //applyer.myHistory.Add(Requestid);

            Staff_Leave.DeleteItem(applyer.myLeaves, leaveItme.id);
            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL);
        }

        public void Scene_wc_rejectv2(User applyer)
        {
            DateTime d1 = new DateTime(2022, 1, 7);
            DateTime d2 = new DateTime(2022, 1, 8);

            List<MODEL.Apply.apply_LeaveData> leaveinfos = new List<MODEL.Apply.apply_LeaveData>();
            applyer.AttachLeaves(d1, 0, leaveinfos);
            applyer.AttachLeaves(d2, 0, leaveinfos);
            int Requestid = applyer.AddLeave(leaveinfos);

            applyer.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);


            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");


            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            applyer.mywaitLeave.Remove(Requestid);
            applyer.myHistoryLeave.Add(Requestid);



            Staff_Leave leaveItme = new Staff_Leave(applyer.staffid, d1, applyer.alid, 0);
            applyer.myLeaves.Add(leaveItme);
            Staff_Leave leaveItme2 = new Staff_Leave(applyer.staffid, d2, applyer.alid, 0);
            applyer.myLeaves.Add(leaveItme2);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);

            int cancelRid = applyer.cancelLeave(Requestid);

            applyer.mywaitLeave.Add(cancelRid);
            //applyer.myHistory.Remove(Requestid);

            user_102.myManagewaitLeave.Add(cancelRid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_102.ApproveCancelLeave(cancelRid, "wc1");

            user_102.myManagewaitLeave.Remove(cancelRid);
            user_102.myManageHistoryLeave.Add(cancelRid);

            user_101.myManagewaitLeave.Add(cancelRid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            user_101.RejectCancelLeave(cancelRid, "rej wc2");
            user_101.myManagewaitLeave.Remove(cancelRid);
            user_101.myManageHistoryLeave.Add(cancelRid);

            applyer.mywaitLeave.Remove(cancelRid);
            //applyer.myHistory.Add(Requestid);

            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
            CheckLeaveRequestStatus(cancelRid, BLL.GlobalVariate.ApprovalRequestStatus.REJECT);
        }

        #endregion

        #region clot
        //測試方法用戶是固定的，可以構造函數的時候變更。來達到測試不同用戶。裡面hardcode .來簡化代碼。
        [TestMethod]
        public void StartTest_clot_all()
        {

            var client = new SmtpClient();
            
                // Note: don't set a timeout unless you REALLY know what you are doing.
                //client.Timeout = 1000 * 20;



            User applyer = user_101;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);

            applyer = user_102;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);

            applyer = user_103;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);

        }

        [TestMethod]
        public void StartTest_clot_u101()
        {
            Console.WriteLine("Start Test");
            User applyer = user_101;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);
        }

        [TestMethod]
        public void StartTest_clot_u102()
        {
            Console.WriteLine("Start Test");
            User applyer = user_102;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);
        }

        [TestMethod]
        public void StartTest_clot_u103()
        {
            Console.WriteLine("Start Test");
            User applyer = user_103;
            Scene_waiting_clot(applyer);
            Scene_Approved_clot(applyer);
            Scene_Reject1_clot(applyer);
            Scene_WithDraw_clot(applyer);
            Scene_wc_clot(applyer);
            Scene_wc_approved_clot(applyer);
            Scene_wc_reject_clot(applyer);
        }

        private void Scene_waiting_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 1), 10, 0, 12, 0, "ot wainting", "7",-1, cLOTItems);
            List<int> clotids= applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();

            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);
        }

        private void Scene_Approved_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 2), 10, 0, 12, 0, "ot approved", "7", 0, cLOTItems);
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.CL, new DateTime(2022, 1, 24), 10, 0, 12, 0, "CL approved", "7", 0, cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x=>clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 approve 2
            user_101.ApproveCLOT(clotids, "2a");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }



        private void Scene_Reject1_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 3), 10, 0, 12, 0, "ot2 rejected", "7", -1, cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 reject 2
            user_101.RejectCLOT(clotids, "2j");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.REJECT);
        }

        private void Scene_WithDraw_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 4), 10, 0, 12, 0, "ot2 withdraw", "7", -1, cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 withdraw
            user_101.WithDrawCLOT(clotids, "2w");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
        }

        private void Scene_wc_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 5), 10, 0, 12, 0, "wc->null", "7", -1, cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve 1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 approve 2
            user_101.ApproveCLOT(clotids, "2a");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);


            //4 cancel
            applyer.CancelCLOT(clotids, "cancel");

            applyer.myWaitClot.AddRange(clotids);
            applyer.myHistroyClot.RemoveAll(x => clotids.Contains(x));

            user_102.myManageWaitClot.AddRange(clotids);
            user_102.myManageHistroyClot.RemoveAll(x => clotids.Contains(x));

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);
        }


        private void Scene_wc_approved_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 6), 10, 0, 12, 0, "ot wc->approved", "7", -1, cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve 1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 approve 2
            user_101.ApproveCLOT(clotids, "2a");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);


            //4 cancel
            applyer.CancelCLOT(clotids, "cancel");

            applyer.myWaitClot.AddRange(clotids);
            applyer.myHistroyClot.RemoveAll(x => clotids.Contains(x));

            user_102.myManageWaitClot.AddRange(clotids);
            user_102.myManageHistroyClot.RemoveAll(x => clotids.Contains(x));

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            //5 approve cancel 1
            user_102.ApproveCancelCLOT(clotids, "approve cancel1");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);
            user_101.myManageHistroyClot.RemoveAll(x => clotids.Contains(x));

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            //approve cancel 2

            user_101.ApproveCancelCLOT(clotids, "approve cancel2");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.CANCEL);
        }

        private void Scene_wc_reject_clot(User applyer)
        {
            //1,add 2.mywaitclot.add 3.check alluser.4.check status.
            List<MODEL.CLOT.CLOTItem> cLOTItems = new List<MODEL.CLOT.CLOTItem>();
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 7), 10, 0, 12, 0, "ot wc->rej", "7",1 ,cLOTItems);
            applyer.AttachCLOTS(MODEL.CLOT.enum_clotType.OT, new DateTime(2022, 1, 8), 10, 0, 12, 0, "ot wc->rej", "7", 2,cLOTItems);
            List<int> clotids = applyer.AddCLOT(cLOTItems);
            applyer.myWaitClot.AddRange(clotids);
            user_102.myManageWaitClot.AddRange(clotids);
            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //2 approve 1
            user_102.ApproveCLOT(clotids, "1a");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            //3 approve 2
            user_101.ApproveCLOT(clotids, "2a");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);


            //4 cancel
            applyer.CancelCLOT(clotids, "cancel");

            applyer.myWaitClot.AddRange(clotids);
            applyer.myHistroyClot.RemoveAll(x => clotids.Contains(x));

            user_102.myManageWaitClot.AddRange(clotids);
            user_102.myManageHistroyClot.RemoveAll(x => clotids.Contains(x));

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            //5 approve cancel 1
            user_102.ApproveCancelCLOT(clotids, "approve cancel1");

            user_102.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_102.myManageHistroyClot.AddRange(clotids);

            user_101.myManageWaitClot.AddRange(clotids);
            user_101.myManageHistroyClot.RemoveAll(x => clotids.Contains(x));

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL);

            //reject cancel 2

            user_101.RejectCancelCLOT(clotids, "reject cancel2");

            user_101.myManageWaitClot.RemoveAll(x => clotids.Contains(x));
            user_101.myManageHistroyClot.AddRange(clotids);

            applyer.myWaitClot.RemoveAll(x => clotids.Contains(x));
            applyer.myHistroyClot.AddRange(clotids);

            checkAllUer_CLOTMyAndMyManageRecord();
            CheckClotRequestStatus(clotids, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }
        #endregion

        #region other test case
        [TestMethod]
        public void add20batch()
        {
            DateTime dt = new DateTime(2022, 1, 1);
            for (int i = 0; i < 20; i++)
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

            user_103.mywaitLeave.Add(Requestid);
            user_102.myManagewaitLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_102.ApproveLeave(Requestid, "a1");

            user_102.myManagewaitLeave.Remove(Requestid);
            user_102.myManageHistoryLeave.Add(Requestid);

            user_101.myManagewaitLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE);

            user_101.ApproveLeave(Requestid, "a2");

            user_101.myManagewaitLeave.Remove(Requestid);
            user_101.myManageHistoryLeave.Add(Requestid);

            user_103.mywaitLeave.Remove(Requestid);
            user_103.myHistoryLeave.Add(Requestid);


            checkAllUer_leaveMyAndMyManageRecord();
            CheckLeaveRequestStatus(Requestid, BLL.GlobalVariate.ApprovalRequestStatus.APPROVE);
        }

        #endregion
    }


    #endregion


    #region pre test.


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
            //ICSHelper iCSHelper = new ICSHelper("mycalendar", "my first metting");
            //ICSHelper.DataItem item = new ICSHelper.DataItem(DateTime.Now, DateTime.Now, "my first calendar","test location", "test my desc",true);
            //ICSHelper.DataItem item2 = new ICSHelper.DataItem(DateTime.Now.AddHours(4), DateTime.Now.AddHours(5), "my secode calendar", "test location", "test my desc", false);
            //iCSHelper.InsertItem(item);
            //iCSHelper.InsertItem(item2);
            //iCSHelper.Save("c:\\abc\\");

            DateTime aa = System.DateTime.Now;


            aa = getSpecialSplitbyday(new DateTime(2023, 4, 3), new DateTime(1900, 5, 31));
        }


        [TestMethod]
        public  DateTime getSpecialSplitbyday(DateTime startdate, DateTime splitdate)
        {
            int d1 = startdate.Day;
            int d2 = splitdate.Day;
            if (d2 > d1)
            {
                int maxDaysTheMonth = System.DateTime.DaysInMonth(startdate.Year, startdate.Month);
                if (d2 > maxDaysTheMonth)
                {
                    return new DateTime(startdate.Year, startdate.Month, maxDaysTheMonth);
                }
                else
                {
                    return new DateTime(startdate.Year, startdate.Month, d2);
                }
            }
            else
            {
                int year = startdate.Year;
                int month = startdate.Month;
                int nextmonth = month + 1;
                if (nextmonth >= 13)
                {
                    nextmonth = 1;
                    year = year + 1;
                }


                int maxDaysTheMonth = System.DateTime.DaysInMonth(year, nextmonth);
                if (d2 > maxDaysTheMonth)
                {
                    return new DateTime(year, nextmonth, maxDaysTheMonth);
                }
                else
                {
                    return new DateTime(year, nextmonth, d2);
                }
            }
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
        public void TempTestFunction()
        {
            TestAndroidPush();
            //TestMerge();
            //testSplit();
            //Test_roundup();
            //DataRange();
        }

      



        private void TestAndroidPush()
        {
            string androidID = "enW3dAo54ME:APA91bFenC2tedUEzEkpauE5TRI-ukreTQurwbnLwzu1Xbm8ZhhG6yFmdBQGSFeWHYxF-nufcYOY9k-eR1u9zn7wqvmmfW1M0C-IYhoKzPqPbSsvUaD5DoKvxIbHQ32WJ0XPu07l4cA_";
            //string aa= LSLibrary.HttpWebRequestHelper.HttpPost_Josn("https://fcm.googleapis.com/fcm/send", "AAAAvJufqGw:APA91bHN5s-7tbRL4VrrSmo_HGycigWZwvqf7z5xo_Ee8k-GFcJ4JOD-QtZ-efPpu-PUevGON1KzvzmockvyitjK_1zuR3G6fl1XdPct7U3cAUrYf78xV0Sb9gkzPb7LJL3N6qRE-Va-", "810064783468", androidID, "test1",3000);

            int a = BLL.Announcement.pushAndroidNotice("test222", androidID);
            int b = 333;
        }

        private static void TestMerge()
        {
            List<StaffSL> sl = new List<StaffSL>();

            sl.Add(new StaffSL(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1), 3));
            sl.Add(new StaffSL(new DateTime(2020, 1, 2), new DateTime(2020, 1, 2), 3));

            sl.Add(new StaffSL(new DateTime(2020, 1, 5), new DateTime(2020, 1, 6), 3));
            sl.Add(new StaffSL(new DateTime(2020, 1, 7), new DateTime(2020, 1, 8), 3));


            sl.Add(new StaffSL(new DateTime(2020, 1, 10), new DateTime(2020, 1, 10), 3));
            sl.Add(new StaffSL(new DateTime(2020, 1, 11), new DateTime(2020, 1, 11), 3));
            sl.Add(new StaffSL(new DateTime(2020, 1, 12), new DateTime(2020, 1, 13), 3));


            sl.Add(new StaffSL(new DateTime(2020, 1, 15), new DateTime(2020, 1, 15), 3));

            var result= StaffSL.GetRanges(sl);

        }

        private static void testSplit()
        {
            DateTime splitd = new DateTime(2022, 3, 30);
            DateTime startdate = new DateTime(2022, 2, 5);
            var dd = getEndDate_month(splitd, startdate);

            int a = 4;
        }

        public class StaffSL
        {
            public DateTime start;
            public DateTime end;
            public double unit;
            public StaffSL() { }
            public StaffSL(DateTime s, DateTime e, double u)
            {
                start = s;
                end = e;
                unit = u;
            }

            public static List<DateRange> GetRanges(List<StaffSL> data)
            {
                List<DateRange> result = new List<DateRange>();

                data = data.OrderBy(x => x.start).ToList();

                //
                DateTime? TempStart = null;
                DateTime? TempEnd = null;

                foreach (var theItem in data)
                {
                    if (TempStart == null || TempEnd == null)
                    {
                        TempStart = theItem.start;
                        TempEnd = theItem.end;
                    }
                    else
                    {
                        if (theItem.start.Date == TempEnd.Value.Date.AddDays(1).Date)
                        {
                            TempEnd = theItem.end;
                        }
                        else
                        {
                            result.Add(new DateRange(TempStart ?? System.DateTime.MinValue, TempEnd ?? System.DateTime.MinValue));

                            TempStart = theItem.start.Date;
                            TempEnd = theItem.end.Date;
                        }
                    }
                }

                if (TempStart != null && TempEnd != null)
                {
                    result.Add(new DateRange(TempStart ?? System.DateTime.MinValue, TempEnd ?? System.DateTime.MinValue));
                }

                return result;
            }
        }

     


        public static DateTime getEndDate_month(DateTime splitDate, DateTime startdate)
        {
            DateTime result = startdate;
            try
            {
                if (splitDate.Day <= 28)
                {
                    if (startdate.Day >= splitDate.Day)
                    {
                        result = startdate.AddMonths(1);
                    }
                    int year = result.Year;
                    int month = result.Month;
                    int day = splitDate.Day;
                    result = new DateTime(year, month, day);
                }
                else
                {
                    int realSplitday = splitDate.Day;
                    int daysMonth_startday = System.DateTime.DaysInMonth(startdate.Year, startdate.Month);
                    realSplitday = System.Math.Min(realSplitday, daysMonth_startday);
                    if (startdate.Day >= realSplitday)
                    {
                        result = startdate.AddMonths(1);
                    }
                    //1.整月天数，是否大于等于 split day.
                    //1.1是，用split day
                    //1.2否，用最大天数
                    int daysMonth = System.DateTime.DaysInMonth(result.Year, result.Month);
                    if (daysMonth >= splitDate.Day)
                    {
                        result = new DateTime(result.Year, result.Month, splitDate.Day);
                    }
                    else
                    {
                        result = new DateTime(result.Year, result.Month, daysMonth);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error:getEndDate_month");
            }
            return result;

        }

        public void Test_roundup()
        {
            List<double> result = new List<double>();

            result.Add(roundup_halforint(2.234, 0));//2.234
            result.Add(11111111111111111);

            result.Add(roundup_halforint(2, 1));//2
            result.Add(roundup_halforint(2.001, 1));//3
            result.Add(11111111111111111);

            result.Add(roundup_halforint(2, 0.5f));//2
            result.Add(roundup_halforint(2.0004, 0.5f));//2
            result.Add(roundup_halforint(2.0005, 0.5f));//2.5
            result.Add(roundup_halforint(2.5004, 0.5f));//2.5
            result.Add(roundup_halforint(2.5005, 0.5f));//3
            result.Add(11111111111111111);

            result.Add(roundup_halforint(2, 2f));//2
            result.Add(roundup_halforint(2.009, 2f));//2
            result.Add(roundup_halforint(2.010, 2f));//2.5
            result.Add(roundup_halforint(2.509, 2f));//2.5
            result.Add(roundup_halforint(2.510, 2f));//3

            int a = 4;
        }

        public void DataRange()
        {
            List<bool> result = new List<bool>();
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2020, 4, 3)));//false
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2020, 4, 4)));//t
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2020, 4, 5)));//f


            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2021, 4, 3)));//f
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2021, 4, 4)));//t
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 5), 1, 3, new DateTime(2021, 4, 5)));//f


            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 31), 1, 3, new DateTime(2020, 4, 29)));//f
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 31), 1, 3, new DateTime(2020, 4, 30)));//t
            result.Add(IsLastDay_Probation(new DateTime(2020, 1, 31), 1, 3, new DateTime(2020, 5, 1)));//f

            int a = 4;

        }


        public static double roundup_halforint(double value, float roundupValue)
        {
            double result = value;

            int intTermination = (int)value;
            double floatTermination = value - intTermination;
            floatTermination = Math.Round(floatTermination, 3);

            if (roundupValue == 0.5)
            {
                if (floatTermination > 0.5)
                {
                    result = intTermination + 1;
                }
                else if (floatTermination > 0 && floatTermination <= 0.5)
                {
                    result = intTermination + 0.5;
                }
                else
                {
                    result = intTermination;
                }
            }
            else if (roundupValue == 1)
            {
                if (floatTermination > 0)
                {
                    result = intTermination + 1;
                }
                else
                {
                    result = intTermination;
                }
            }
            else if(roundupValue==2)
            {
                if (floatTermination >= 0.51)
                {
                    result = intTermination + 1;
                }
                else if (floatTermination >= 0.01 && floatTermination < 0.51)
                {
                    result = intTermination + 0.5;
                }
                else
                {
                    result = intTermination;
                }
            }

            return result;
        }


        [TestMethod]
        public void testtemp()
        {
            double result= GetDecimal(1);
            result = GetDecimal(1.34);
            result = GetDecimal(1.54);
            result = GetDecimal(-0.23);
            result = GetDecimal(-5.67);

            int a = 4;
        }

        public static double GetDecimal(double data)
        {
            double result = 0;
            string strData = data.ToString();
            if (strData.Contains('.'))
            {
                string decimalStr = strData.Substring(strData.IndexOf('.'));
                double.TryParse(decimalStr, out result);
                if(data<0)
                {
                    result = -result;
                }
            }
            return result;
        }

        public static void FixInvalidMinDatetime(object OBJ)
        {
            try
            {
                var allFiles = OBJ.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                foreach (var item in allFiles)
                {
                    if (item.FieldType == typeof(System.DateTime))
                    {
                        if (((DateTime)item.GetValue(OBJ)).Year == 1)
                        {
                            item.SetValue(OBJ, new DateTime(1900, 1, 1));
                        }
                    }
                }
            }
            catch { }
        }

        public class abc
        {
            public string a;
            string b;
            DateTime c;
            public DateTime d;
        }

        public static string ComputeMD5HashHEX(string Seed)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider p_MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return ConvertByteArrayToHex(p_MD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Seed)));
        }
        public static string ConvertByteArrayToHex(byte[] ByteArray)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < ByteArray.Length; i++)
            {
                sb.Append(ByteArray[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private static void urlcode()
        {
            var dd = System.Web.HttpUtility.UrlEncode("approval_wait.aspx?action=0&applicationtype=0&from=0&requestid=35218");
            var a = 3;
        }

        private static void testRange()
        {
            DateRange dr1 = new DateRange(new DateTime(2022, 2, 5), new DateTime(2022, 3, 5));

            DateRange t1 = new DateRange(new DateTime(2022, 1, 1), new DateTime(2022, 2, 4));

            DateRange t2 = new DateRange(new DateTime(2022, 1, 1), new DateTime(2022, 2, 5));

            DateRange t3 = new DateRange(new DateTime(2022, 1, 1), new DateTime(2022, 2, 6));

            DateRange t4 = new DateRange(new DateTime(2022, 1, 1), new DateTime(2022, 3, 5));

            DateRange t5 = new DateRange(new DateTime(2022, 1, 1), new DateTime(2022, 3, 6));

            DateRange t6 = new DateRange(new DateTime(2022, 2, 5), new DateTime(2022, 2, 5));

            DateRange t7 = new DateRange(new DateTime(2022, 2, 6), new DateTime(2022, 2, 8));

            DateRange t8 = new DateRange(new DateTime(2022, 2, 6), new DateTime(2022, 3, 5));

            DateRange t9 = new DateRange(new DateTime(2022, 2, 6), new DateTime(2022, 3, 9));

            DateRange t10 = new DateRange(new DateTime(2022, 3, 5), new DateTime(2022, 3, 9));

            DateRange t11 = new DateRange(new DateTime(2022, 3, 6), new DateTime(2022, 3, 9));

            List<bool> result = new List<bool>();
            result.Add(HasUniteData(dr1, t1));
            result.Add(HasUniteData(dr1, t2));
            result.Add(HasUniteData(dr1, t3));
            result.Add(HasUniteData(dr1, t4));
            result.Add(HasUniteData(dr1, t5));
            result.Add(HasUniteData(dr1, t6));
            result.Add(HasUniteData(dr1, t7));
            result.Add(HasUniteData(dr1, t8));
            result.Add(HasUniteData(dr1, t9));
            result.Add(HasUniteData(dr1, t10));
            result.Add(HasUniteData(dr1, t11));

            int a = 4;
        }


        //是否有重叠日期
        public static bool HasUniteData(DateRange dr1, DateRange dr2)
        {
            bool result = false;
            if (dr2.fromday < dr1.fromday && dr2.today >= dr1.fromday)
            {
                result = true;
            }
            if (dr2.fromday == dr1.fromday)
            {
                result = true;
            }
            if (dr2.fromday > dr1.fromday && dr2.fromday <= dr1.today)
            {
                result = true;
            }
            return result;
        }

        public class DateRange
        {
            public DateTime fromday { get; set; }
            public DateTime today { get; set; }
            public DateRange(DateTime from, DateTime to)
            {
                fromday = from;
                today = to;
            }
        }


            private static bool IsLastDay_Probation(DateTime joinday, int delayyear, int delaymonth, DateTime checkday)
        {
            bool result = false;
            //check parameter.
            if (checkday >= joinday && delayyear >= 0 && delaymonth >= 1)
            {
                int yearOffset = checkday.Year - joinday.Year;

                DateTime start1 = joinday.AddYears(yearOffset);
                DateTime end1 = start1.AddMonths(delaymonth).AddDays(-1);

                DateTime start2 = joinday.AddYears(yearOffset - 1);
                DateTime end2 = start2.AddMonths(delaymonth).AddDays(-1);


                if (checkday == end1 || checkday == end2)
                {
                    result = true;
                }
            }
            return result;
        }


        [TestMethod]
        public void testtryParse()
        {
        }

        [TestMethod]
        public void testtest()
        {
            DateTime? result = GetSpecialEndYearDate(new DateTime(2022, 2, 1));

            DateTime? result2 = GetSpecialEndYearDate(new DateTime(2023, 12, 31));

            DateTime? result3 = GetSpecialEndYearDate(new DateTime(2024, 1, 1));

            DateTime? result4 = GetSpecialEndYearDate(new DateTime(2025, 3, 1));

            int a = 4;
        }

        public DateTime? GetSpecialEndYearDate(DateTime applydate)
        {
            DateTime? carryforwardDate_CurrentYearField = null;// new DateTime(2024, 1, 1);
            DateTime? result = null;
            DateTime? carryforwardDate_applydate = carryforwardDate_CurrentYearField;

            if (carryforwardDate_CurrentYearField != null)
            {
                //1.get temp carryforwardDate_applydate ,same year,month,day 2. if applydate>< temp ok. other add 1year.
                int year_carryforwardDate = carryforwardDate_CurrentYearField.Value.Year;
                int month_carryforwardDate = carryforwardDate_CurrentYearField.Value.Month;
                int day_carryforwardDate = carryforwardDate_CurrentYearField.Value.Day;

                DateTime temp = new DateTime(applydate.Year, month_carryforwardDate, day_carryforwardDate);
                if (applydate < temp)
                {
                    carryforwardDate_applydate = temp;
                }
                else
                {
                    carryforwardDate_applydate = temp.AddYears(1);
                }
            }

            if (carryforwardDate_applydate != null)
            {
                result = carryforwardDate_applydate.Value.AddDays(-1);
            }
            return result;
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