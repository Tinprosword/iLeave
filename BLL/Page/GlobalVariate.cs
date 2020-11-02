using System.Collections.Generic;
using System.Web;

namespace BLL
{
    public class GlobalVariate
    {
        #region global string
        public static string login_error = "invalid user and password.";
        public static string path_uploadPic = "uploadPic";
        public static HttpServerUtility pageServer;
        public static string Session_ApplyToCanlendar = "Session_ApplyToCanlendar";
        public static string Session_CanlendarToApply = "Session_CanlendarToApply";
        public static string Session_ApplyToUpload = "Session_ApplyToUpload";
        public static string Session_UploadToApply = "Session_UploadToApply";
        
        #endregion

        #region common enum 使用端的生成的enum居然默然从0开始.全然不管webservices的设定....只能采用会有隐患的复制的方法
        public enum sectionType
        {
            pleaseselect=-1,
            full=0,
            am=1,
            pm=2,
            three=3,
            hours=4
        }

        public enum ApprovalRequestStatus
        {
            [System.ComponentModel.Description("SendEmail")]
            SENDEMAIL = -1,
            [System.ComponentModel.Description("MyPortal_WF_Created")]
            NEW = 0,
            [System.ComponentModel.Description("MyPortal_WF_WaitForApproval")]
            WAIT_FOR_APPROVE = 1,
            [System.ComponentModel.Description("MyPortal_WF_Approved")]
            APPROVE = 2,
            [System.ComponentModel.Description("MyPortal_WF_Rejected")]
            REJECT = 3,
            [System.ComponentModel.Description("MyPortal_WF_Cancelled")]
            CANCEL = 4,
            [System.ComponentModel.Description("MyPortal_WF_WaitForCancel")]
            WAIT_FOR_CANCEL = 5,
            [System.ComponentModel.Description("MyPortal_WF_ConfirmCancelled")]
            CONFIRM_CANCEL = 6,
        }

        public enum WorkflowTypeID
        {
            [System.ComponentModel.Description("MyPortal_LeaveApplication")]
            LEAVE_APPLICATION = 0,
            [System.ComponentModel.Description("MyPortal_WF_UpdatePersonInformation")]
            UPDATE_PERSON_INFO = 1,
            [System.ComponentModel.Description("MyPortal_WF_UpdateTrainingRecord")]
            UPDATE_TRAINING_RECORD = 2,
            [System.ComponentModel.Description("MyPortal_WF_UpdateQualificationRecord")]
            UPDATE_QUALIFICATION_RECORD = 3,
            [System.ComponentModel.Description("MyPortal_WF_PayrollLock")]
            PAYROLL_LOCK = 4,
            [System.ComponentModel.Description("MyPortal_WF_CLOTApplication")]
            CLOT_APPLICATION = 5,
            [System.ComponentModel.Description("MyPortal_WF_OTApproval")]
            OT_APPROVAL = 6,
            [System.ComponentModel.Description("MyPortal_WF_MedicalClaim")]
            MEDICAL_CLAIM = 7,
            [System.ComponentModel.Description("MyPortal_WF_Attendance")]
            ATTENDANCE = 8,
            //v1.6.6 - Paul - 2015.11.06 - Add Doctor Visit
            [System.ComponentModel.Description("MyPortal_WF_DoctorVisit")]
            DOCTOR_VISIT = 9,
            [System.ComponentModel.Description("MyPortal_WF_CancelLeaveApplication")]
            CANCEL_LEAVE_APPLICATION = 10,
            //v1.7.0 Paul 2016.06.08 - Add Expense Claim
            [System.ComponentModel.Description("MyPortal_WF_ExpenseClaim")]
            EXPENSE_CLAIM = 11,
        }

        public enum WorkflowInOutTypeID
        {
            [System.ComponentModel.Description("IN")]
            IN = 0,
            [System.ComponentModel.Description("OUT")]
            OUT = 1,
        }


        public enum LeaveBigRangeStatus
        {
            waitapproval,
            approvaled,
            withdraw
        }

        public string GetStateDesc(ApprovalRequestStatus status)
        {
            return RequestDesc[status];
        }

        #endregion

        #region common dictionary full day 2 ampm 3 3secton 4 houre

        public static Dictionary<int, string> sections
        {
            get
            {
                Dictionary<int, string> temp = new Dictionary<int, string>();
                temp.Add(0, BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_fullday);
                temp.Add(1, BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_am);
                temp.Add(2, BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_pm);
                temp.Add(3, "3Sections");
                //temp.Add( 4,"Hours");
                return temp;
            }
        }


        public static Dictionary<ApprovalRequestStatus, string> RequestDesc
        {
            get
            {
                Dictionary<ApprovalRequestStatus, string> temp = new Dictionary<ApprovalRequestStatus, string>();
                temp.Add(ApprovalRequestStatus.WAIT_FOR_APPROVE, "Wait for approval");
                temp.Add(ApprovalRequestStatus.APPROVE, "Approved");
                temp.Add(ApprovalRequestStatus.REJECT, "Reject");
                temp.Add(ApprovalRequestStatus.WAIT_FOR_CANCEL, "Wait for cancel");
                temp.Add(ApprovalRequestStatus.CONFIRM_CANCEL, "Cancel");
                temp.Add(ApprovalRequestStatus.CANCEL, "Withdraw");
                temp.Add(ApprovalRequestStatus.NEW, "New");
                temp.Add(ApprovalRequestStatus.SENDEMAIL, "SendEmail");

                return temp;
            }
        }

        #endregion

        #region global string 用于js css文件的修改后自动重新下载.
        public static string appcssLastmodify
        {
            get
            {
                string filePath= pageServer.MapPath("~/Res/App/appcss.css");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");
                
            }
            set { }
        }

        public static string applyjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/apply.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string autoscalejsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/autoScale.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string commonjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/CommonJS.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string myapplicationjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/myapplication.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string myapprovaljsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/approval.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");
            }
            set { }
        }
        #endregion

    }
}