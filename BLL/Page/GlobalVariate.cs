using System.Collections.Generic;
using System.Web;

namespace BLL
{
    public class GlobalVariate
    {
        #region global string
        public static string login_error = "invalid user and password.";
        //public static string submit_success = "Submitted,Please wait.";

        public static string path_uploadPic = "uploadPic";
        public static HttpServerUtility pageServer;
        public static string Session_ApplyToCanlendar = "Session_ApplyToCanlendar";
        public static string Session_CanlendarToApply = "Session_CanlendarToApply";
        public static string Session_ApplyToUpload = "Session_ApplyToUpload";
        public static string Session_UploadToApply = "Session_UploadToApply";
        public static bool testvalue = true;
        
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
            withdraw,
            beyongdWait
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
                temp.Add(3, BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_3section);
                //temp.Add( 4,"Hours");
                return temp;
            }
        }

        public static Dictionary<int, double> sectionsUnit
        {
            get
            {
                Dictionary<int, double> temp = new Dictionary<int, double>();
                temp.Add(0, 1);
                temp.Add(1, 0.5);
                temp.Add(2, 0.5);
                temp.Add(3, 1.5);
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
                temp.Add(ApprovalRequestStatus.REJECT, "Rejected");
                temp.Add(ApprovalRequestStatus.WAIT_FOR_CANCEL, "Wait for cancel");
                temp.Add(ApprovalRequestStatus.CONFIRM_CANCEL, "Canceled");
                temp.Add(ApprovalRequestStatus.CANCEL, "Withdraw");
                temp.Add(ApprovalRequestStatus.NEW, "New");
                temp.Add(ApprovalRequestStatus.SENDEMAIL, "SendEmail");

                return temp;
            }
        }


        public static Dictionary<ApprovalRequestStatus, string> RequestActionDesc//作为动作的描述词，用于detail'log
        {
            get
            {
                Dictionary<ApprovalRequestStatus, string> temp = new Dictionary<ApprovalRequestStatus, string>();
                temp.Add(ApprovalRequestStatus.WAIT_FOR_APPROVE, BLL.MultiLanguageHelper.GetLanguagePacket().approval_approvedon);
                temp.Add(ApprovalRequestStatus.APPROVE, BLL.MultiLanguageHelper.GetLanguagePacket().approval_approvedon);
                temp.Add(ApprovalRequestStatus.REJECT, BLL.MultiLanguageHelper.GetLanguagePacket().approval_rejectedon);
                temp.Add(ApprovalRequestStatus.WAIT_FOR_CANCEL, BLL.MultiLanguageHelper.GetLanguagePacket().approval_appcancelon);
                temp.Add(ApprovalRequestStatus.CONFIRM_CANCEL, BLL.MultiLanguageHelper.GetLanguagePacket().approval_appcancelon);
                temp.Add(ApprovalRequestStatus.CANCEL, BLL.MultiLanguageHelper.GetLanguagePacket().approval_appcancelon);
                temp.Add(ApprovalRequestStatus.NEW, "New on");
                temp.Add(ApprovalRequestStatus.SENDEMAIL, "SendEmail on");

                return temp;
            }
        }

        #endregion

        #region global string 用于js css文件的修改后自动重新下载.

        //输入一个360*640高度下某控件的高度，会计算出一个自适应手机高度，此控件的高。
        public static string setHeight(int GoodHeight)
        {
            int SCNormalHeight = 640;
            string agent = HttpContext.Current.Request.UserAgent;
            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType type = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if (type == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android || type == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)
            {
                LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
                if (loginer != null && loginer.userInfo != null && loginer.userInfo.ScreenHeight > SCNormalHeight)
                {
                    int value = (loginer.userInfo.ScreenHeight - SCNormalHeight + GoodHeight);//0计算多出的高度
                    value =(int) (value * (360.0 / loginer.userInfo.ScreenWidth)); // 1.考虑缩放比例调整。
                    int offset = (int)(0.133 * loginer.userInfo.ScreenHeight - 80);//2。自己写的一个遮挡栏多于普通栏的偏移值
                    value=value - offset;
                    return value.ToString();
                }
                else
                {
                    return GoodHeight.ToString();
                }
            }
            else
            {
                return GoodHeight.ToString();
            }
        }


     


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