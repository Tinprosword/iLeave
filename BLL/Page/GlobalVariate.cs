using System.Collections.Generic;
using System.Web;

namespace BLL
{
    public class GlobalVariate
    {
        #region global string
        public static bool iosDebug =false;

        public static string login_error = "invalid user and password.";
        //public static string submit_success = "Submitted,Please wait.";

        public static string path_uploadPic = "uploadPic";
        public static HttpServerUtility pageServer;
        public static string Session_ApplyToCanlendar = "Session_ApplyToCanlendar";
        public static string Session_CanlendarToApply = "Session_CanlendarToApply";

        public static string Session_ApplyToUpload = "Session_ApplyToUpload";
        public static string Session_UploadToApply = "Session_UploadToApply";

        public static string Session_clottoupload = "sessionclottoupload";
        public static string Session_uploadtoclot = "sessionuploadtoclot";

        public static string splangcodeus = "us";
        public static string splangcodecn = "cn";
        public static string splangcodehk = "hk";

        public static string GetSPLanguageCode(LSLibrary.WebAPP.LanguageType type)
        {
            if (type == LSLibrary.WebAPP.LanguageType.tc)
            {
                return "hk";
            }
            else if (type == LSLibrary.WebAPP.LanguageType.sc)
            {
                return "cn";
            }
            else
            {
                return "us";
            }
        }

        //us, cn, hk

        public static bool testvalue = true;
        public static bool testdownload = true;

        #endregion

        #region common enum 使用端的生成的enum居然默然从0开始.全然不管webservices的设定....只能采用会有隐患的复制的方法


        public enum spFunctionid
        {
            leave_ADD_Portal=7,
            clot_add_portal=13
        }

        

        public enum AttachmentUploadType
        {
            [System.ComponentModel.Description("AttachmentUndefined")]
            UNDEFINED = -1,
            [System.ComponentModel.Description("AttachmentHKID")]
            HKID = 0,
            [System.ComponentModel.Description("Common_WorkingExperience")]
            WORK_EXPERIENCE = 2,
            [System.ComponentModel.Description("Common_DrivingLicense")]
            DRIVE_LICENSE = 3,
            [System.ComponentModel.Description("AttachmentTrainingRecord")]
            TRAINING = 4,
            [System.ComponentModel.Description("AttachmentOthers")]
            OTHERS = 5,
            [System.ComponentModel.Description("Common_MPFWithdrawal")]
            MPF_WITHDRAWAL = 6,
            [System.ComponentModel.Description("AttachmentALPurchase")]
            AL_PURCHASE = 7,
            [System.ComponentModel.Description("AttachmentSkill")]
            SKILL = 8,
            [System.ComponentModel.Description("AttachmentAcademicAttainment")]
            ACADEMIC_ATTAINMENT = 9,
            [System.ComponentModel.Description("AddressProof")]
            ADDRESS_PROOF = 10,
            [System.ComponentModel.Description("BankAccountDocument")]
            BANK_ACCOUNT_DOCUMENT = 11,
            [System.ComponentModel.Description("Common_Announcement")]
            ANNOUNCEMENT = 12,
            [System.ComponentModel.Description("Common_DisciplinaryRecord")]
            DISCIPLINARY_RECORD = 13,
            [System.ComponentModel.Description("TaxationIR56G")]
            TAXATION_IR56G = 14,
            [System.ComponentModel.Description("Common_Insurance")]
            INSURANCE = 15,
            [System.ComponentModel.Description("Common_Roster")]
            ROSTER = 16,
            [System.ComponentModel.Description("EmploymentContract")]
            EMPLOYMENT_CONTRACT = 17,
            [System.ComponentModel.Description("ConfidentialityAgreement")]
            CONFIDENTIALITY_AGREEMENT = 18,
            [System.ComponentModel.Description("AttachmentPersonalQualification")]
            QUALIFICATION = 51,//6, Original is 6 , after merge to 51
            [System.ComponentModel.Description("AttachmentDoctorReceipt")]
            DOCTOR_RECEIPT = 52,//clot
            [System.ComponentModel.Description("AttachmentLeaveCertificate")]
            LEAVE_CERTIFICATE = 53,//8, Original is 8 , after merge to 53
            [System.ComponentModel.Description("AttachmentAppraisal")]
            APPRAISAL = 54,//9, Original is 9 , after merge to 54
            [System.ComponentModel.Description("Safety")]
            SAFETY = 55,
        }


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

        public enum AttachType
        {
            leave=0,
            clot=1,
        }


        public enum LeaveBigRangeStatus
        {
            waitapproval,
            approvaled,
            withdraw,
            beyongdWait
        }

        public enum OTSection
        {
            none=0,half=3,full=2,hour=1
        }

        public enum CLSection
        {
            none = 0, half = 2, full = 1, hour = 3
        }

        //public string GetStateDesc(ApprovalRequestStatus status)
        //{
        //    return RequestDesc[status];
        //}

        #endregion

        #region common dictionary full day 2 ampm 3 3secton 4 houre

        public static string GetUnit(WebServiceLayer.WebReference_leave.LeaveRequestDetail detail)
        {
            string result = "";

            result= detail.Unit.ToString();

            return result;
        }

        public static string GetSectionMultLanguage(int sectionid, int lange = -1)
        {

            if (sectionid == 0)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_fullday;
            }
            else if (sectionid == 1)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_am;
            }
            else if (sectionid == 2)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_pm;
            }
            else if (sectionid == 3)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_3section;
            }
            else
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_ByHour;
            }

        }

        public static string GetSectionMultLanguage(WebServiceLayer.WebReference_leave.LeaveRequestDetail detail, int lange = -1)
        {
            string result = "";

            if (detail.LeaveHours != 0)
            {
                return MODEL.CLOT.CLOTItem.GetTimeRangeDesc(detail.LeaveFrom.Value.Hour, detail.LeaveTo.Value.Hour, detail.LeaveFrom.Value.Minute, detail.LeaveTo.Value.Minute);
            }
            else
            {
                if (detail.Section == 0)
                {
                    return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_fullday;
                }
                else if (detail.Section == 1)
                {
                    return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_am;
                }
                else if (detail.Section == 2)
                {
                    return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_pm;
                }
                else if (detail.Section == 3)
                {
                    return BLL.MultiLanguageHelper.GetLanguagePacket().apply_ddlsetion_3section;
                }
            }

            return result;
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


        //public static Dictionary<ApprovalRequestStatus, string> RequestDesc
        //{
        //    get
        //    {
        //        Dictionary<ApprovalRequestStatus, string> temp = new Dictionary<ApprovalRequestStatus, string>();
        //        temp.Add(ApprovalRequestStatus.WAIT_FOR_APPROVE, "Wait for approval");
        //        temp.Add(ApprovalRequestStatus.APPROVE, "Approved");
        //        temp.Add(ApprovalRequestStatus.REJECT, "Rejected");
        //        temp.Add(ApprovalRequestStatus.WAIT_FOR_CANCEL, "Wait for cancel");
        //        temp.Add(ApprovalRequestStatus.CONFIRM_CANCEL, "Canceled");
        //        temp.Add(ApprovalRequestStatus.CANCEL, "Withdraw");
        //        temp.Add(ApprovalRequestStatus.NEW, "New");
        //        temp.Add(ApprovalRequestStatus.SENDEMAIL, "SendEmail");

        //        return temp;
        //    }
        //}


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
            LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType type = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);
            if (type == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android || type == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone)
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

        public static string checkjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/check.js");
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