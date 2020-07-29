using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace BLL
{
    public class GlobalVariate
    {
        //ui 不要直接去 访问dal,所以在bll中,采用这种方式来映射webservice的枚举类型,也不能直接复制类型过来,违背 唯一依赖原则.这样,如果WS修改后,这里最起码会编译错误.
        //上面是好的做法,但是微软居然有bug... 使用端的生成的enum居然默然从0开始.全然不管webservices的设定....只能采用会有隐患的复制的方法
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




        //global string
        public static string login_error = "invalid user and password.";
        public static string path_uploadPic = "uploadPic";


        //cooki name
        public static readonly string COOKIE_SERVERADDRESS="cookie_serveraddress";
        public static readonly string COOKIE_HTTPS = "cookie_https";


        

        //用于js css文件的修改后自动重新下载.
        public static HttpServerUtility pageServer;
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

    }
}