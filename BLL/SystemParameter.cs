using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class SystemParameters
    {
        #region pname
        public const string MIN_COMMITTED_WAGES_GOV = "MIN_COMMITTED_WAGES_GOV";
        public const string MIN_COMMITTED_WAGES_LAW = "MIN_COMMITTED_WAGES_LAW";
        public const string ENFORCE_MIN_COMMITTED_WAGES_LAW = "ENFORCE_MIN_COMMITTED_WAGES_LAW";

        public const string ADDITION_DECIMAL = "ADDITION_DECIMAL";
        public const string ADDITION_FIXDAY = "ADDITION_FIXDAY";
        public const string DEDUCTION_DECIMAL = "DEDUCTION_DECIMAL";
        public const string DEDUCTION_FIXDAY = "DEDUCTION_FIXDAY";
        public const string MAX_TOTAL_PER_GRID = "MAX_TOTAL_PER_GRID";
        public const string SPECIAL_SHIFT_RATIO = "SPECIAL_SHIFT_RATIO";
        public const string PART_TIME_FIXDAY = "PART_TIME_FIXDAY";
        public const string UI_DISPLAY_DECIMAL = "UI_DISPLAY_DECIMAL";
        public const string PRECONTRACT_DAY = "PRECONTRACT_DAY";
        public const string MONTHLY_WORKING_FIXDAY = "MONTHLY_WORKING_FIXDAY";
        public const string ADDITION_TYPE = "ADDITION_TYPE";
        public const string DEDUCTION_TYPE = "DEDUCTION_TYPE";

        //v1.4.0 2013-04-15 Kitty add minimum wages for 28
        public const string MIN_COMMITTED_WAGES_LAW_28 = "MIN_COMMITTED_WAGES_LAW_28";

        //v1.2.0 - Wing - Get the right to enforce to pay the min wage for rest day
        public const string ENFORCE_MIN_COMMITTED_WAGES_LAW_FOR_REST_DAY = "ENFORCE_MIN_COMMITTED_WAGES_LAW_FOR_REST_DAY";

        //v1.2.1 - Derek - Fix day for position difference
        public const string POSITION_DIFFERENCE_FIXDAY = "POSITION_DIFFERENCE_FIXDAY";

        //v1.2.2 - Derek - handle harris request on special source fix day for position code: 72 (滅蟲員)
        public const string POSITION_DIFFERENCE_SOURCE = "POSITION_DIFFERENCE_SOURCE";

        //v1.2.2 - Wing - handle cindy request on 替人地位, 對方個BASE唔係除26
        public const string SPECIAL_POSITION_DIFFERENCE_SOURCE = "SPECIAL_POSITION_DIFFERENCE_SOURCE";

        //v1.2.3 Wing - Handle the Staff Number Generation
        public const string STAFF_NUMBER_PREFIX = "STAFF_NUMBER_PREFIX";

        //v1.2.3 - Kitty - handle cindy request on 替人地位, base of destination is C or C-R
        public const string SPECIAL_POSITION_DIFFERENCE_DESTINATION = "SPECIAL_POSITION_DIFFERENCE_DESTINATION";

        //v1.3.0 Kitty handle harris request, default position difference calendar day if C for holiday worker
        public const string HOLIDAY_WORKER_POSITION_DIFFERENCE_CALENDARDAY = "HOLIDAY_WORKER_POSITION_DIFFERENCE_CALENDARDAY";

        //v1.2.3 - Kitty - handle PR$ for Company (Pest Company)
        public const string PR_COMPANY_CODE = "PR_COMPANY_CODE";

        //2013.05.15 terry  handle PR$ for Contract (Pest Company)
        public const string PR_CONTRACT_CODE = "PR_CONTRACT_CODE";

        //v1.2.2 - Kitty - Name of Company Group
        public const string COMPANY_GROUP_NAME = "COMPANY_GROUP_NAME";

        //v1.3.0 - Kitty - Query message for payslip
        public const string QUERY_MESSAGE_ENG = "QUERY_MESSAGE_ENG";
        public const string QUERY_MESSAGE_CHI = "QUERY_MESSAGE_CHI";

        //v1.1.19C wing MPF Company File name part
        public const string g_strSystemMPFCompanyFileNamePart = "_COMPANY_MPFFILE_";

        public const string MAX_RECORD_STAFF_LIST = "MAX_RECORD_STAFF_LIST";

        //v1.4.0 2013.01.07 Terry add system financial period
        public const string SYSTEM_FINANCIAL_PERIOD = "SYSTEM_FINANCIAL_PERIOD";

        //v1.8.8 Fai 2018.12.13 - Split Taxation Period instead of share "SYSTEM_FINANCIAL_PERIOD"
        public const string SYSTEM_TAXATION_PERIOD = "SYSTEM_TAXATION_PERIOD";

        //v1.5.0 Fai 2014.08.01 - Default System Language
        public const string DEFAULT_SYSTEM_LANGUAGE = "DEFAULT_SYSTEM_LANGUAGE";

        //v1.3.0 Fai 2012.08.24 - Skip input Contract Code Prefix of Shift Code for Planned Roster (1 = Yes, 0 = No)
        public const string SKIP_INPUT_CONTRACTCODE_ON_PLANNEDROSTER = "SKIP_INPUT_CONTRACTCODE_ON_PLANNEDROSTER";

        //v1.4.0 Fai 2013.01.28 - Update wages on position changed
        public const string UPDATE_WAGES_ON_POSITION_CHANGED = "UPDATE_WAGES_ON_POSITION_CHANGED";

        //v1.4.0 Fai 2013.02.08 - Round Up AL Balance to Multiple of 0.5 during Employment Termination
        public const string ROUNDUP_ALBALANCE_ON_EMPLOYMENT_TERMINATION = "ROUNDUP_ALBALANCE_ON_EMPLOYMENT_TERMINATION";

        //v1.4.0 2013.01.18 terry add system para advance debug on
        public const string ADVANCE_DEBUG_MODE = "ADVANCE_DEBUG_MODE";

        //v1.4.0 2013.01.18 terry add system para advance debug on
        public const string CALCULATION_REFERENCE_CARRY_FORWARD = "CALCULATION_REFERENCE_CARRY_FORWARD";

        //v1.4.0 2013-03-07 Kitty add flag to enforce Residence Identity checking
        public const string ENFORCE_RESIDENCE_IDENTITY_CHECKING = "ENFORCE_RESIDENCE_IDENTITY_CHECKING";

        //v1.4.3 2013.10.18 mark add flag to enforce District Identity checking 
        public const string ENFORCE_DISTRICT_IDENTITY_CHECKING = "ENFORCE_DISTRICT_IDENTITY_CHECKING";

        //2013.07.10 terry add system para to hide CLOT application
        public const string HIDDEN_CLOT_APPLICATION = "HIDDEN_CLOT_APPLICATION";

        //2013.07.10 terry add system para to hide without benefit
        public const string WITHOUT_EMPLOYMENT_BENEFIT = "WITHOUT_EMPLOYMENT_BENEFIT";

        //2013.08.01 terry add system para for default payment code in PPP file
        public const string DEFAULT_PAYMENT_CODE_FOR_PPP_FILE = "DEFAULT_PAYMENT_CODE_FOR_PPP_FILE";

        //v1.4.0 Fai 2013.08.05 - Send Alert To Direct Supervisor
        public const string SEND_ALERT_TO_DIRECT_SUPERVISOR = "SEND_ALERT_TO_DIRECT_SUPERVISOR";

        //v1.6.6 Fai 2015.09.16 - Hide Direct supervisor
        public const string HIDE_DIRECT_SUPERVISOR = "HIDE_DIRECT_SUPERVISOR";

        //2013.09.10 terry system para show employemnt fixed payitem
        public const string FIX_PAYITEM_IN_EMPLOYMENT_INFO = "FIX_PAYITEM_IN_EMPLOYMENT_INFO";

        //2013.10.15 terry system para show employemnt payitem fixed amount
        public const string FIX_PAYITEM_AMOUNT_IN_EMPLOYMENT_INFO = "FIX_PAYITEM_AMOUNT_IN_EMPLOYMENT_INFO";

        //v1.4.3 2013.11.12 terry system parameter for basic salary include fixed payitem
        //v1.8.8 Fai 2018.11.15 - Used t_FixedPayitem.IsIncludedInBasicSalary instead of all fixed payitem - Begin
        //public const string BASIC_SALARY_INCLUDE_FIXED_PAYITEM_AMOUNT = "BASIC_SALARY_INCLUDE_FIXED_PAYITEM_AMOUNT";
        //v1.8.8 Fai 2018.11.15 - Used t_FixedPayitem.IsIncludedInBasicSalary instead of all fixed payitem - End

        //2013.12.19 terry add para to skip date not continues in related employment
        public const string LEAVE_CALCULATION_SKIP_NOT_CONTINUE_DATE = "LEAVE_CALCULATION_SKIP_NOT_CONTINUE_DATE";

        //v1.5.0 20131104 Billy - Training Sponsorship Policy Link
        public const string TRAINING_SPONSORSHIP_POLICY_URL = "TRAINING_SPONSORSHIP_POLICY_URL";

        //v1.5.0 Fai 2014.11.14 - Hide Expiry Date and Notice Period on Attachment (Portal Only)
        public const string HIDE_EXPIRY_ATTACHMENTUPLOAD_PORTAL = "HIDE_EXPIRY_ATTACHMENTUPLOAD_PORTAL";

        //v1.6.0 Fai 2014.12.01 - Hide OT Balance
        public const string HIDE_OT_BALANCE = "HIDE_OT_BALANCE";

        //v1.6.1 Fai 2015.01.06 - Hide Appraisal Attachment Expiry Date
        public const string HIDE_APPRAISAL_EXPIRYDATE = "HIDE_APPRAISAL_EXPIRYDATE";

        //v1.6.1 Fai 2015.01.09 - Query Report Config move from UIModule to System Parameter
        public const string QUERYREPORT_DBID = "QUERYREPORT_DBID";
        public const string QUERYREPORT_DBNAME = "QUERYREPORT_DBNAME";
        public const string QUERYREPORT_URL = "QUERYREPORT_URL";
        //v1.6.9 - Cheong - 2016/05/18 - Make the salt used to create QueryReport login hash configurable
        public const string QUERYREPORT_KEYSALT = "QUERYREPORT_KEYSALT";

        //v1.6.1 Billy 20150113 - Send Notice Messge on approved workflow only
        public const string SEND_NOTICE_MESSAGE_ON_APPROVED_WORKFLOW_ONLY = "SEND_NOTICE_MESSAGE_ON_APPROVED_WORKFLOW_ONLY";

        //v1.6.2 Fai 2015.01.20 - System Parameter to config the email subject prefix and sender name
        public const string EMAIL_ALERT_SUBJECT_PREFIX = "EMAIL_ALERT_SUBJECT_PREFIX";
        public const string EMAIL_ALERT_SENDER_NAME = "EMAIL_ALERT_SENDER_NAME";

        //v1.6.2 Fai 2015.02.11 - Control ST Deduction or not on Holiday Type = ST during Probation Period
        public const string ST_DEDUCTION_ON_PROBATIONPERIOD = "ST_DEDUCTION_ON_PROBATIONPERIOD";

        //v1.7.0 Fai 2016.06.13 - Separate ST / BH Deduction or not during Probation Period
        public const string BH_DEDUCTION_ON_PROBATIONPERIOD = "BH_DEDUCTION_ON_PROBATIONPERIOD";

        //v1.6.3 Fai 2015.03.03 - License Expiry Notice Email
        public const string LICENSE_EXPIRY_NOTICE_EMAIL = "LICENSE_EXPIRY_NOTICE_EMAIL";

        //v1.6.4 Fai 2015.05.06
        public const string USER_MAX_FAIL_ATTEMPT = "USER_MAX_FAIL_ATTEMPT";
        public const string USER_CHANGE_PASSWORD_CYCLE = "USER_CHANGE_PASSWORD_CYCLE";

        //v1.6.4 Fai 2015.05.12 - User Minimum Password Length
        public const string USER_MIN_PASSWORD_LENGTH = "USER_MIN_PASSWORD_LENGTH";
        public const string USER_MIN_PASSWORD_CONTAIN_NUMBER = "USER_MIN_PASSWORD_CONTAIN_NUMBER";
        public const string USER_MIN_PASSWORD_CONTAIN_LOWERCASE_ALPHABET = "USER_MIN_PASSWORD_CONTAIN_LOWERCASE_ALPHABET";
        public const string USER_MIN_PASSWORD_CONTAIN_UPPERCASE_ALPHABET = "USER_MIN_PASSWORD_CONTAIN_UPPERCASE_ALPHABET";
        public const string USER_MIN_PASSWORD_CONTAIN_SYMBOL = "USER_MIN_PASSWORD_CONTAIN_SYMBOL";

        //v1.6.5 Billy 20150617 - My portal show Postion-Shift Dropdown
        public const string PORTAL_HIDE_EMPLOYMENTNUMBER_ON_LEAVEAPPLICATION = "PORTAL_HIDE_EMPLOYMENTNUMBER_ON_LEAVEAPPLICATION";

        public const string EMPLOYMENT_NUMBER_FROM_STAFF_NUMBER = "EMPLOYMENT_NUMBER_FROM_STAFF_NUMBER";

        //v1.6.5 Fai 2015.06.24 - Payroll Deduction Amount Rounding Method (0 = RoundDown, 1 = RoundOff, 2 = RoundUp)
        public const string PAYROLL_DEDUCTION_AMOUNT_ROUNDING_METHOD = "PAYROLL_DEDUCTION_AMOUNT_ROUNDING_METHOD";

        //v1.6.5 Fai 2016.01.12 - Payroll Addition Amount Rounding Method (0 = RoundDown, 1 = RoundOff, 2 = RoundUp)
        public const string PAYROLL_ADDITION_AMOUNT_ROUNDING_METHOD = "PAYROLL_ADDITION_AMOUNT_ROUNDING_METHOD";

        //v1.6.5 BIlly 2015.06.25 - Staff detail show MPF Policy Number
        public const string STAFF_DETAIL_SHOW_MPF_POLICY_NUMBER = "STAFF_DETAIL_SHOW_MPF_POLICY_NUMBER";

        //v1.6.5 Billy 2015.06.26 - Hide salary point in employment
        public const string EMPLOYMENT_HIDE_SALARY_POINT = "EMPLOYMENT_HIDE_SALARY_POINT";

        //v1.6.5 Billy 2015.06.26 - Staff retirement age, for calculating terminate date
        public const string STAFF_RETIREMENT_AGE = "STAFF_RETIREMENT_AGE";

        //v1.6.5 Fai 2015.07.02 - Staff and Employment Number
        public const string STAFF_NUMBER_SEQUENCE = "STAFF_NUMBER_SEQUENCE";
        public const string EMPLOYMENT_NUMBER_SEQUENCE = "EMPLOYMENT_NUMBER_SEQUENCE";

        //v1.6.5 Billy 2015.07.03 - Person Surname/Last name capital letter
        public const string PERSON_SURNAME_FIRSTNAME_CAPITAL_LETTER = "PERSON_SURNAME_FIRSTNAME_CAPITAL_LETTER";

        //v1.6.5 Billy 2015.07.06 - Staff hide Resign status
        public const string STAFF_DETAIL_HIDE_RESIGN_STATUS = "STAFF_DETAIL_HIDE_RESIGN_STATUS";

        //v1.6.5 Billy 2015.07.16 - Payment in lieu compare daily rate to 713
        public const string COMPARE_713_AND_DAILYRATE_ON_PAYMENT_IN_LIEU = "COMPARE_713_AND_DAILYRATE_ON_PAYMENT_IN_LIEU";

        //v1.6.5 Billy 2015.07.21 - Display option in Approval screen (0 = Both 1 = Staff 2 = Employment; Default : 2 HKNP : 1)
        public const string PORTAL_DISPLAY_NUMBER_ON_APPROVAL = "PORTAL_DISPLAY_NUMBER_ON_APPROVAL";

        //v1.6.5 Billy 2015.07.31 - Reminder for referral bonus release to staff after a specific period of time(month) (default: 2)
        public const string NOTICE_PERIOD_OF_REFERRAL_AFTER_PROBATION = "NOTICE_PERIOD_OF_REFERRAL_AFTER_PROBATION";

        //v1.6.6 Fai 2015.08.28 - Enable Leave Calculation Formula (0 = No, 1 = Yes)
        //v1.8.4 Ben 2018.04.20 - Replace ENABLE_LEAVE_CALCULATION_FORMULA with ENABLE_PAYCODEFORMULA
        //public const string ENABLE_LEAVE_CALCULATION_FORMULA = "ENABLE_LEAVE_CALCULATION_FORMULA";

        //v1.6.6 Fai 2015.10.05 - Update 713 before Payroll Calculation
        public const string PAYROLL_UPDATE_713_BEFORE_PAYROLL_CALCULATION = "PAYROLL_UPDATE_713_BEFORE_PAYROLL_CALCULATION";

        //v1.6.6 Billy 2015.10.07 - Update payroll type, payment method when terminate
        public const string UPDATE_PAYROLL_TYPE_ON_STAFF_QUIT = "UPDATE_PAYROLL_TYPE_ON_STAFF_QUIT";
        public const string UPDATE_PAYMENT_METHOD_ON_STAFF_QUIT = "UPDATE_PAYMENT_METHOD_ON_STAFF_QUIT";

        //v1.6.6 Paul 2015.10.08 - DEFAULT STAFF HOLIDAY TYPE
        public const string DEFAULT_STAFF_HOLIDAY_TYPE = "DEFAULT_STAFF_HOLIDAY_TYPE";

        //giActualAttendanceThreholdInMinute
        public const string ATTENDANCE_ACTUAL_THRESHOLD_IN_MINUTE_LOWER_BOUND = "ATTENDANCE_ACTUAL_THRESHOLD_IN_MINUTE_LOWER_BOUND";
        public const string ATTENDANCE_ACTUAL_THRESHOLD_IN_MINUTE_UPPER_BOUND = "ATTENDANCE_ACTUAL_THRESHOLD_IN_MINUTE_UPPER_BOUND";

        //v1.6.6 Paul 2015.10.16 - Staff Employment Position System Parameter
        public const string EMPLOYMENT_POSITION_FILTER_BY_MASTERZONE = "EMPLOYMENT_POSITION_FILTER_BY_MASTERZONE";

        //v1.6.6 Fai 2015.10.29 - Enable Continue OT Calculation in 713 (0 = No, 1 = Yes)
        public const string ENABLE_CONTINUE_OT_CALCULATION_IN_713 = "ENABLE_CONTINUE_OT_CALCULATION_IN_713";

        //v1.6.6 Paul 2015.11.04
        //- Set default create self-service account
        //- Hide Appraisal attachment upload
        //- Hide Payitem Period
        //- Hide Part Time Employment Type (hourly and daily)
        //- Hide Doctor Visit
        public const string CREATE_SELF_SERVICE_ACCOUNT = "CREATE_SELF_SERVICE_ACCOUNT";
        //public const string HIDE_APPRAISAL_ATTACHMENTUPLOAD = "HIDE_APPRAISAL_ATTACHMENTUPLOAD";
        public const string HIDE_PAYITEM_PERIOD = "HIDE_PAYITEM_PERIOD";
        public const string HIDE_PART_TIME_EMPLOYMENT_TYPE = "HIDE_PART_TIME_EMPLOYMENT_TYPE";
        public const string HIDDEN_DOCTOR_VISIT_APPLICATION = "HIDDEN_DOCTOR_VISIT_APPLICATION";

        //v1.6.6 Billy 2015.11.19
        public const string SHOW_APPROVER_INFO_IN_MYPORTAL_APPROVAL_SCREEN = "SHOW_APPROVER_INFO_IN_MYPORTAL_APPROVAL_SCREEN";

        // v1.6.6 Billy 2015.12.04 - SHOW_MAX_AL_CARRIED_FORWARD_IN_MYPORTAL_LEAVE
        public const string SHOW_MAX_CARRIED_FORWARD_IN_MYPORTAL_LEAVE = "SHOW_MAX_CARRIED_FORWARD_IN_MYPORTAL_LEAVE";

        //v1.6.6 - Cheong - 2015/12/04 - Add filter to set default for Attendance Detail page.
        public const string ATTENDANCEDETAIL_FILTER_OPTION = "ATTENDANCEDETAIL_FILTER_OPTION";
        //v1.6.6 - Cheong - 2015/12/07 - Add SystemParameter to control whether need to enable consent interface in Attendance Detail page
        public const string ATTENDANCEDETAIL_NEEDCONSENT = "ATTENDANCEDETAIL_NEEDCONSENT";
        //v1.6.6 - Cheong - 2015/12/17 - Add SystemParameter to control whether need to show early leave column in Attendance Detail page
        public const string ATTENDANCEDETAIL_SHOWEARLYLEAVE = "ATTENDANCEDETAIL_SHOWEARLYLEAVE";

        //v1.6.8 Paul 2016.01.27 - Show Doctor Visit Application Period Message (Please apply sick leave if the application period more than 3.5 hrs) (0 = No, 1 = YES)
        public const string SHOW_DOCTOR_VISIT_APPLICATION_PERIOD_MESSAGE = "SHOW_DOCTOR_VISIT_APPLICATION_PERIOD_MESSAGE";

        //v1.6.8 - Cheong - 2016/02/01 - Add SystemParameter for showing nickname in inbox/outbox if exist
        public const string SHOW_NICKNAME_IN_MYPORTAL_MAINPAGE = "SHOW_NICKNAME_IN_MYPORTAL_MAINPAGE";

        //v1.6.8 Fai 2016.02.01 - Reset Entitlement Year of Service on Leave Class change (AL Only)
        public const string RESET_AL_YEAR_OF_SERVICE_ON_CHANGE_LEAVECLASS = "RESET_AL_YEAR_OF_SERVICE_ON_CHANGE_LEAVECLASS";

        //v1.6.8 Fai 2016.02.01 - Common Leave Year (AL Only)
        public const string COMMON_LEAVE_YEAR = "COMMON_LEAVE_YEAR";
        public const string COMMON_LEAVE_YEAR_INCOMPLETE_YEAR = "COMMON_LEAVE_YEAR_INCOMPLETE_YEAR";

        //v1.6.8 Paul - 2016.02.05 - Add new setting for contribute on Complete Month only and using Basic Salary
        public const string CONTRIBUTE_ON_COMPLETE_MONTH_ONLY = "CONTRIBUTE_ON_COMPLETE_MONTH_ONLY";
        public const string CALCULATE_BASIC_SALARY_ONLY = "CALCULATE_BASIC_SALARY_ONLY";

        //v1.6.8 - Cheong - 2016/02/15 - Add SystemParameter for showing SAT/SUN/Holiday
        public const string SHOW_HOLIDAY_FILTER_ON_ONLINE_LEAVE_APPL = "SHOW_HOLIDAY_FILTER_ON_ONLINE_LEAVE_APPL";

        //v1.6.8 Billy 2016.02.15 - Hide Export Roster Default Shift Code 
        public const string HIDE_EXPORT_ROSTER_DEFAULT_SHIFT_CODE = "HIDE_EXPORT_ROSTER_DEFAULT_SHIFT_CODE";

        //v1.6.8 Billy 2016.02.17 - MPF File Generation excel show age column
        public const string MPF_FILE_GENERATION_EXCEL_SHOW_AGE_COLUMN = "MPF_FILE_GENERATION_EXCEL_SHOW_AGE_COLUMN";

        //v1.6.8 Billy 2016.02.18 - PPP File Generation output path
        public const string PPPFILE_OUTPUT_PATH = "PPPFILE_OUTPUT_PATH";

        //v1.6.8 Fai 2016.02.17 - Workaround to hide Doctor Receipt on Online Staff Leave Application (Remove later)
        public const string PORTAL_LEAVE_HIDE_DOCTOR_RECEIPT = "PORTAL_LEAVE_HIDE_DOCTOR_RECEIPT";

        //v1.6.8 Fai 2016.02.22 - Show Position in Portal Inbox
        public const string SHOW_POSITION_PORTAL_INBOX = "SHOW_POSITION_PORTAL_INBOX";

        //v1.6.8 Fai 2016.02.24 - Hide Overwrite Upload Mode on Import Roster
        public const string IMPORT_ROSTER_HIDE_OVERWRITE = "IMPORT_ROSTER_HIDE_OVERWRITE";

        public const string IMPORT_ROSTER_HIDE_UPLOADMODE = "IMPORT_ROSTER_HIDE_UPLOADMODE";

        //v1.6.8 Fai 2016.02.24 - Grid Size on My Portal
        public const string PORTAL_GRID_SIZE = "PORTAL_GRID_SIZE";

        //v1.6.8 Fai 2016.02.24 - Default Status on My Task
        public const string DEFAULT_STATUS_ON_MY_TASK = "DEFAULT_STATUS_ON_MY_TASK";

        //v1.6.8 Fai 2016.02.26 - Default Inbox Unread only on My Task (0 = No, 1 = Yes)
        public const string DEFAULT_INBOX_UNREADONLY_ON_MY_TASK = "DEFAULT_INBOX_UNREADONLY_ON_MY_TASK";

        //v1.6.8 Paul 2016.03.04 - Add Leave Application checking when upload Roster
        public const string IMPORT_ROSTER_LEAVE_SETTING_METHOD = "IMPORT_ROSTER_LEAVE_SETTING_METHOD";

        //v1.6.8 Fai 2016.03.08 - Update Previous Month Payroll Snapshot before Payroll Calculation (0 = No, 1 = Yes)
        public const string PAYROLL_UPDATE_SNAPSHOT_BEFORE_PAYROLL_CALCULATION = "PAYROLL_UPDATE_SNAPSHOT_BEFORE_PAYROLL_CALCULATION";

        //v1.6.8 Paul 2016.03.17 - Add system parameter HIDE_STAFF_COMPANYPAYROLL_AND_COMPANYIRD to hide COMPANY PAYROLL and COMPANY IRD in Staff Detail (0 = Show, 1 = Hide)
        public const string HIDE_STAFF_COMPANYPAYROLL_AND_COMPANYIRD = "HIDE_STAFF_COMPANYPAYROLL_AND_COMPANYIRD";

        //v1.6.9 Fai 2016.03.21 - Sick Leave Consecutive Day Definition
        public const string SICK_LEAVE_CONSECUTIVE_DAY_DEFINITION = "SICK_LEAVE_CONSECUTIVE_DAY_DEFINITION";

        //v1.6.9 Fai 2016.03.30 - Exclude Remain Half Day in 713
        public const string EXCLUDE_REMAIN_HALF_DAY_IN_713 = "EXCLUDE_REMAIN_HALF_DAY_IN_713";

        //v1.6.9 Fai 2016.03.30 - Auto Allowance
        public const string ENABLE_AUTO_ALLOWANCE = "ENABLE_AUTO_ALLOWANCE";

        //v1.6.8 Paul 2016.04.14 - Add Benefit Package Probation Month and After Probation Notice Month
        public const string BENEFIT_PACKAGE_PROBATION_MONTH = "BENEFIT_PACKAGE_PROBATION_MONTH";
        public const string BENEFIT_PACKAGE_AFTER_PROBATION_NOTICE_MONTH = "BENEFIT_PACKAGE_AFTER_PROBATION_NOTICE_MONTH";

        //v1.6.8 Paul 2016.04.27 - Add Hide Item in Staff Detail and Hide Item in Employment Detail
        public const string HIDE_ITEMS_IN_STAFF_DETAIL = "HIDE_ITEMS_IN_STAFF_DETAIL";
        public const string HIDE_ITEMS_IN_EMPLOYMENT_DETAIL = "HIDE_ITEMS_IN_EMPLOYMENT_DETAIL";

        /*- v1.6.9 Sun 2016-04-29: Add system parameters of LDAP setup -*/
        public const string LDAP_IS_AVAILABLE = "LDAP_IS_AVAILABLE";
        public const string LDAP_DOMAIN = "LDAP_DOMAIN";

        //v1.6.9 - Cheong - 2016/05/05 - Add SystemParameter HOURLY_ROSTER_USE_FIRSTEMPLOYMENTNUMBER
        public const string HOURLY_ROSTER_USE_FIRSTEMPLOYMENTNUMBER = "HOURLY_ROSTER_USE_FIRSTEMPLOYMENTNUMBER";
        public const string SHIFT_ROSTER_USE_FIRSTEMPLOYMENTNUMBER = "SHIFT_ROSTER_USE_FIRSTEMPLOYMENTNUMBER";

        //v1.6.9 Fai 2016.06.01 - Control Mark Task IsRead = true if on Same Step
        public const string MARK_WORKFLOWTASK_AS_READ_ON_SAME_LEVEL = "MARK_WORKFLOWTASK_AS_READ_ON_SAME_LEVEL";

        //v1.7.0 Fai 2016.06.16 - Old Missing System Parameter just add in here
        public const string PHOTO_HEIGHT_IN_STAFF_DETAIL = "PHOTO_HEIGHT_IN_STAFF_DETAIL";

        //v1.7.0 Fai 2016.06.23 - Hide Portal Application Details on Email Alert
        public const string HIDE_PORTAL_APPLICATION_DETAILS_ON_EMAILALERT = "HIDE_PORTAL_APPLICATION_DETAILS_ON_EMAILALERT";

        //v1.7.0 Fai 2016.06.27 - Workflow Email Link Bypass Login (0 = No, 1 = Yes)
        public const string WORKFLOW_EMAIL_LINK_BYPASS_LOGIN = "WORKFLOW_EMAIL_LINK_BYPASS_LOGIN";

        //v1.7.0 Fai 2016.07.13 - Show Approval History on Approval Screen (0 = No, 1 = Yes)
        public const string SHOW_APPROVALHISTORY_ON_APPROVAL_SCREEN = "SHOW_APPROVALHISTORY_ON_APPROVAL_SCREEN";

        //v1.7.0 Fai 2016.07.18 - Recruitment System URL
        public const string RECRUITMENT_URL = "RECRUITMENT_URL";

        //v1.7.0 - Cheong - 2016/08/08 - Add setting to control two factor authentication
        //v1.7.0 - Cheong - 2016/08/11 - Change to use web.config setting to control two factor authentication
        //public const string USER_REQUIRE_TWO_FACTOR_AUTHENTICATION = "USER_REQUIRE_TWO_FACTOR_AUTHENTICATION";

        //v1.7.0 - Cheong - 2016/08/11 - Add root OUID parameter for drawing OrgChart
        public const string ORGCHART_ROOTOUID = "ORGCHART_ROOTOUID";

        //v1.7.0 Fai 2016.08.18 - Mark Holiday Used when Leave on Statutory Holiday (Value = Leave Code)
        //public const string MARK_HOLIDAY_USED_WHEN_NPL_ON_SH = "MARK_HOLIDAY_USED_WHEN_NPL_ON_SH";
        public const string MARK_HOLIDAY_USED_WHEN_LEAVE_ON_SH = "MARK_HOLIDAY_USED_WHEN_LEAVE_ON_SH";

        //v1.7.0 - Cheong - 2016/09/07 - Add SystemParameter to control whether allow user view other employment in Online Attendance in Portal Mode
        public const string ATTENDANCEDETAIL_PORTAL_ALLOW_VIEW_OTHER_STAFF = "ATTENDANCEDETAIL_PORTAL_ALLOW_VIEW_OTHER_STAFF";

        //v1.7.0 Kim 2016.09.15 Control hide/show of export roster estimate hour
        public const string HIDE_EXPORT_ROSTER_ESTIMATE_HOUR = "HIDE_EXPORT_ROSTER_ESTIMATE_HOUR";

        //v1.7.0 Fai 2016.09.26 - Config 713 Calculation
        //0 = Leave (All belong to occur month, included Amount and Days), OT belong to Payroll Month
        //1 = Leave (Exclude 713 will belong to occur month, Otherwise belong to Payroll Month), SH Adj will belong to Payroll Month, OT belong to Payroll Month
        public const string AVERAGE_DAILY_RATE_CALCULATION_METHOD = "AVERAGE_DAILY_RATE_CALCULATION_METHOD";

        //v1.7.0 Kim 2016.10.12 add system parameter: Control hide/show of login page change password checkbox
        public const string HIDE_LOGIN_PAGE_CHANGE_PASSWORD = "HIDE_LOGIN_PAGE_CHANGE_PASSWORD";

        //v1.7.0 Fai 2016.10.28 - Remove Obsolete System Parameter - Begin
        //v1.7.0 - Cheong - 2016/10/18 - Add System Parameter ENABLE_HALF_SESSION_LEAVE
        //public const string ENABLE_HALF_SESSION_LEAVE = "ENABLE_HALF_SESSION_LEAVE";
        //v1.7.0 Fai 2016.10.28 - Remove Obsolete System Parameter - End

        //v1.7.0 Kim 2016.10.18 add system parameter: Control hide/show of bank acc in my portal - personal info
        public const string SHOW_PORTAL_PERSONAL_INFO_BANK_ACC = "SHOW_PORTAL_PERSONAL_INFO_BANK_ACC";

        //v1.7.0 Kim 2016.10.18 add system parameter: Control hide/show of employment list in my portal - personal info
        public const string SHOW_PORTAL_PERSONAL_INFO_EMPLOYMENT_LIST = "SHOW_PORTAL_PERSONAL_INFO_EMPLOYMENT_LIST";

        //v1.7.0 - Cheong - 2016/11/25 - Enable auto assign PL based on missing attendance record
        public const string ASSIGN_PL_FOR_MISSING_ATTENDNACE_DAY_THESHOLD = "ASSIGN_PL_FOR_MISSING_ATTENDNACE_DAY_THESHOLD";

        //v1.7.0 - Cheong - 2016/12/14 - Add parameter to control whether to show Position / Zone columns on AttendanceDetailByDate (and AttendaceDetail in future)
        public const string ATTENDANCEDETAIL_SHOW_POSITION = "ATTENDANCEDETAIL_SHOW_POSITION";
        public const string ATTENDANCEDETAIL_SHOW_ZONE = "ATTENDANCEDETAIL_SHOW_ZONE";

        /*-v1.7.0 Sun 2017.01.04 - add parameter to control running auto-add FSL in scheduler or not. General feature but temporary for HKPA -*/
        public const string ENABLE_AUTOADD_FULLPAYSICKLEAVE = "ENABLE_AUTOADD_FULLPAYSICKLEAVE";

        /*-v1.7.0 Sun 2017.01.10: Add parameter to control AL balance calculation to skip probation period or not -*/
        public const string LEAVE_AL_CALCULATION_SKIP_PROBATION = "LEAVE_AL_CALCULATION_SKIP_PROBATION";

        //v1.7.0 Billy 2017.01.24 - System parameter to Show/Hide [Symbol remark] in export planned roster
        public const string SHOW_EXPORT_PLANNED_ROSTER_SYMBOL_REMARK = "SHOW_EXPORT_PLANNED_ROSTER_SYMBOL_REMARK";

        /*-v1.7.0 Sun 2017.01.25: Add parameter to define 366 days calculation depends on start date period or not, i.e. 01 Feb 2016 count 366 where 01 Mar 2016 count 365  -*/
        public const string ENABLE_366Days_BY_STARTDATE_ON_OR_BEFORE_29FEB = "ENABLE_366Days_BY_STARTDATE_ON_OR_BEFORE_29FEB";
        /*-v1.7.0 Sun 2017.01.25: Add parameter to enable data massage on In/Out type in Attendance Raw Data -*/
        public const string ENABLE_MASSAGE_ON_ATTENDANCERAWDATA_INOUT_TYPE = "ENABLE_MASSAGE_ON_ATTENDANCERAWDATA_INOUT_TYPE";

        //v1.7.0 Fai 2017.02.21 - Workaround to fill the rounding different within configurable value (Idea by Noordin) - Begin
        public const string PAYROLL_FILL_ROUNDING_DIFF_WITHIN_RANGE = "PAYROLL_FILL_ROUNDING_DIFF_WITHIN_RANGE";
        //v1.7.0 Fai 2017.02.21 - Workaround to fill the rounding different within configurable value (Idea by Noordin) - End

        //v1.7.0 Fai 2017.03.07 - Enable Statutory Holidays within +/- 60 Days Limit
        public const string ENABLE_60_DAYS_LIMIT_FOR_ST = "ENABLE_60_DAYS_LIMIT_FOR_ST";

        //v1.7.0 Fai 2017.03.14 - System Parameter control to hide the Staff Department
        public const string HIDE_STAFF_DEPARTMENT = "HIDE_STAFF_DEPARTMENT";

        //v1.7.0 Billy 2017.05.12 - Export Planned Roster - prefill imported data
        public const string SHOW_EXPORT_ROSTER_IMPORTED_DATA = "SHOW_EXPORT_ROSTER_IMPORTED_DATA";

        //v1.7.0 Billy 2017.05.12 - Export Planned Roster - show import changes
        public const string SHOW_EXPORT_ROSTER_IMPORTED_CHANGES = "SHOW_EXPORT_ROSTER_IMPORTED_CHANGES";

        //v1.7.0 Billy 2017.05.12 - Export Planned Roster - prefill imported data
        public const string SHOW_EXPORT_ROSTER_ROSTER_TIME = "SHOW_EXPORT_ROSTER_ROSTER_TIME";

        //v1.7.0 Janet 2017.05.17 - Staff Number Renew Pattern
        public const string STAFF_NUMBER_RENEW_PATTERN = "STAFF_NUMBER_RENEW_PATTERN";

        //v1.7.0 Janet 2017.05.17 - Employmen Number Renew Pattern
        public const string EMPLOYMENT_NUMBER_RENEW_PATTERN = "EMPLOYMENT_NUMBER_RENEW_PATTERN";

        //v1.7.0 Billy 2017.05.23 - Export Planned Roster - Export Roster Shift Info on the left side [working hour] column
        public const string SHOW_EXPORT_ROSTER_SHIFT_INFO_ON_LEFT = "SHOW_EXPORT_ROSTER_SHIFT_INFO_ON_LEFT";

        //v1.7.0 Janet 2017.05.24 - Enable Vadlidation 2FSL Per Month
        public const string ENABLE_VALIDATION_2FSL_PER_MONTH = "ENABLE_VALIDATION_2FSL_PER_MONTH";

        /*-v1.7.0 Sun 2017.06.08: Hide Forfieted Leave count -*/
        public const string LEAVE_HIDE_FORFEITED_VALUE = "LEAVE_HIDE_FORFEITED_VALUE";

        /*-v1.7.0 Sun 2017.06.09: Control the no. of decimal places to be displayed for days -*/
        public const String LEAVE_BALANCE_DISPLAY_DECIMAL_PLACES = "LEAVE_BALANCE_DISPLAY_DECIMAL_PLACES";

        /*-v1.7.0 Sun 2017.06.28: Add parameter to control the show/hide of Leave code in Leave calendar -*/
        public const String HIDE_LEAVECODE_IN_LEAVE_CALENDAR = "HIDE_LEAVECODE_IN_LEAVE_CALENDAR";

        //v1.7.0 Fai 2017.06.30 - Required Person Attachment / Qualification for Create Staff - Begin
        public const String REQUIRED_PERSON_ATTACHMENT_FOR_CREATE_STAFF = "REQUIRED_PERSON_ATTACHMENT_FOR_CREATE_STAFF";

        public const String REQUIRED_PERSON_QUALIFICATION_FOR_CREATE_STAFF = "REQUIRED_PERSON_QUALIFICATION_FOR_CREATE_STAFF";
        //v1.7.0 Fai 2017.06.30 - Required Person Attachment / Qualification for Create Staff - End

        //v1.7.0 Fai 2017.06.30 - Depend on Part-time / Full-time to Control Payment In Lieu by 713 or Daily Rate
        public const String PAYMENT_IN_LIEU_DEPEND_ON_EMPLOYMENT_PARTTIME = "PAYMENT_IN_LIEU_DEPEND_ON_EMPLOYMENT_PARTTIME";

        //v1.7.0 Janet 2017.07.05 - Add hidden CLOT for portal - Begin
        public const String HIDDEN_CLOT_APPLICATION_FOR_PORTAL = "HIDDEN_CLOT_APPLICATION_FOR_PORTAL";
        //v1.7.0 Janet 2017.07.05 - Add hidden CLOT for portal - End

        //v1.7.0 Janet 2017.07.12 - Add CLOT change to al balance - Begin
        public const String CLOT_CHANGE_TO_AL_BALANCE = "CLOT_CHANGE_TO_AL_BALANCE";
        //v1.7.0 Janet 2017.07.12 - Add CLOT change to al balance - End

        //v1.7.0 Janet 2017.07.13 - Add Hourly Leave minutes unit - Begin
        public const String HOURLY_LEAVE_MINUTES_UNIT = "HOURLY_LEAVE_MINUTES_UNIT";
        public const String ENFORCE_HOURLY_LEAVE_PERIOD = "ENFORCE_HOURLY_LEAVE_PERIOD";
        //v1.7.0 Janet 2017.07.13 - Add Hourly Leave minutes unit - End

        //v1.7.0 Janet 2017.07.18 - Add Hidden Part time checkbox in employment and hide staff detail item - Begin
        // v1.7.0 Billy 2017.09.04 - remove the system parameter, combine into HIDE_ITEMS_IN_EMPLOYMENT_DETAIL
        //public const String HIDDEN_EMPLOYMENT_PART_TIME = "HIDDEN_EMPLOYMENT_PART_TIME";

        public const String HIDE_ITEM_IN_STAFF_DETAIL = "HIDE_ITEM_IN_STAFF_DETAIL";
        //v1.7.0 Janet 2017.07.18 - Add Hidden Part time checkbox in employment and hide staff detail item - End
        //v1.7.0 Ben 2017.07.24 -Add Hidden addition/deduction column in salary allowance report
        //v1.8.1 Ben 2018.01.10 - Change name so that it can be continous to add columns
        //public const String HIDDEN_SALARYALLOWANCE_ADDITION_DEDUCTION = "HIDDEN_SALARYALLOWANCE_ADDITION_DEDUCTION";
        public const String HIDDEN_SALARYALLOWANCE_COLUMNS = "HIDDEN_SALARYALLOWANCE_COLUMNS";
        //kyrie 2020.08.14 add HIDDEN SALARYALLOWANCE HEADER COLUMNS
        public const String HIDDEN_SALARYALLOWANCE_HEADER_COLUMNS = "HIDDEN_SALARYALLOWANCE_HEADER_COLUMNS";
        //kyrie 2020.08.14 add HIDDEN SALARYALLOWANCE CONTENT COLUMNS
        public const String HIDDEN_SALARYALLOWANCE_CONTENT_COLUMNS = "HIDDEN_SALARYALLOWANCE_CONTENT_COLUMNS";
        //v1.7.0 Fai 2017.07.27 - Depend on Part-time / Full-time to Control Compensate AL by 713 or Daily Rate
        public const String COMPENSATE_AL_DEPEND_ON_EMPLOYMENT_PARTTIME = "COMPENSATE_AL_DEPEND_ON_EMPLOYMENT_PARTTIME";

        //v1.7.0 Fai 2017.07.27 - Add parameter to allow editing the bank account
        public const String LAST_DAY_ALLOW_EDIT_BANKACCOUNT = "LAST_DAY_ALLOW_EDIT_BANKACCOUNT";

        //v1.7.0 Ben 2017.07.28 - Add parameters to set default value of exclude Sat,Sun,Holiday
        public const String CHECK_ANNUALLEAVE_EXCLUDE = "CHECK_ANNUALLEAVE_EXCLUDE";

        //v1.7.0 Ben 2017.07.28 - Add parameters to set the date of last date
        public const String LAST_DATE_OF_CURRENTMONTH = "LAST_DATE_OF_CURRENTMONTH";
        //v1.7.0 Ben 2017.08.15 - Add parameters to set if checking Salary Range
        public const String CHECK_SALARY_RANGE = "CHECK_SALARY_RANGE";

        //v1.7.0 Janet 2017.08.17 - Add parameter to enforce under approval role input acting staff - Begin
        public const String ENFORCE_INPUT_ACTING_STAFF = "ENFORCE_INPUT_ACTING_STAFF";
        //v1.7.0 Janet 2017.08.17 - Add parameter to enforce under approval role input acting staff - End

        //v1.7.0 Janet 2017.08.17 - Add parameter to control employment history grid show column - Begin
        public const String EMPLOYMENT_HISTORY_GRID_SHOW_COLUMN = "EMPLOYMENT_HISTORY_GRID_SHOW_COLUMN";
        //v1.7.0 Janet 2017.08.17 - Add parameter to control employment history grid show column - End

        public const String HIDE_RANK_SALARYRANGE = "HIDE_RANK_SALARYRANGE";

        //v1.7.0 Janet 2017.08.21 - Generate Username pattern - Begin
        public const String GENERATE_USERNAME_PATTERN = "GENERATE_USERNAME_PATTERN";
        //v1.7.0 Janet 2017.08.21 - Generate Username pattern - End

        //v1.7.0 Ben 2017.08.21 - Show FractionPt
        public const String SHOW_FRACTIONAL_POINT = "SHOW_FRACTIONAL_POINT";
        //1.7.0 Ben 2017.09.06 - Enable Renew Staff Contract
        public const String RENEW_STAFF_CONTRACT = "RENEW_STAFF_CONTRACT";
        //1.7.0 Ben 2017.09.06 - NP Calucaltion on budget
        public const String NP_SPECIAL_CALCULATE_BUDGET = "NP_SPECIAL_CALCULATE_BUDGET";

        //v1.7.0 Fai 2017.09.06 - Leave Balance Snapshot Day
        public const String LEAVE_BALANCE_SNAPSHOT_DAY = "LEAVE_BALANCE_SNAPSHOT_DAY";

        //v1.7.0 Ben 2017.09.28 - Merge 'ENABLE_ROSTER_CYCLE_MANAGER', 'ENABLE_ROSTER_CYCLE_PATTERN' to ENABLE_ROSTER_CYCLE
        //v1.7.0 Billy 2017.09.11 - Roster Cycle Manager
        //public const String ENABLE_ROSTER_CYCLE_MANAGER = "ENABLE_ROSTER_CYCLE_MANAGER";

        //v1.7.0 Ben 2017.09.14 - Ignore the black pop up for validation
        //v1.7.0 Ben 2017.09.19 - Change Name
        //public const String IGNORE_BLACK_POP_UP = "IGNORE_BLACK_POP_UP";
        public const String DISABLE_RIGHT_AUTHORIZATION = "DISABLE_RIGHT_AUTHORIZATION";

        //v1.7.0 Fai 2017.09.18 - Substitute Separate Rounding
        public const String ENABLE_SUBSTITUTE_SEPARATE_ROUNDING = "ENABLE_SUBSTITUTE_SEPARATE_ROUNDING";

        //v1.7.0 Ben 2017.09.28 - Merge 'ENABLE_ROSTER_CYCLE_MANAGER', 'ENABLE_ROSTER_CYCLE_PATTERN' to ENABLE_ROSTER_CYCLE
        //v1.7.0 Fai 2017.09.26 - Enable Change Working Pattern
        //public const String ENABLE_ROSTER_CYCLE_PATTERN = "ENABLE_ROSTER_CYCLE_PATTERN";
        public const String ENABLE_ROSTER_CYCLE = "ENABLE_ROSTER_CYCLE";

        //v1.7.0 Fai 2017.10.04 - Enable Pay Code Formula on Daily Basis
        public const String ENABLE_PAYCODEFORMULA_ON_DAILYBASIS = "ENABLE_PAYCODEFORMULA_ON_DAILYBASIS";

        //v1.7.0 Ben 2017.10.06 - Hide Valid from,MAXIMUM ENTITLEMENT 
        public const String HIDE_MAXIMUM_ENTITLEMENT = "HIDE_MAXIMUM_ENTITLEMENT";
        public const String HIDE_VALIDFROM_IN_LEAVE_ENTITLEMENT = "HIDE_VALIDFROM_IN_LEAVE_ENTITLEMENT";

        //v1.7.0 Ben 2017.10.09 - Hide MyPortal Leave Calendar 
        public const String HIDE_PORTAL_LEAVE_CALENDAR_INFO = "HIDE_PORTAL_LEAVE_CALENDAR_INFO";

        //v1.7.0 Janet 2017.10.09 - Add System Parameter to control return advanced AL button - Begin
        public const String HIDE_RETURN_ADVANCED_AL = "HIDE_RETURN_ADVANCED_AL";
        //v1.7.0 Janet 2017.10.09 - Add System Parameter to control return advanced AL button - End

        //v1.7.0 Ben 2017.10.09 - Add System Parameter to control Email to same user combine or not
        public const String COMBINE_SAME_EMAIL = "COMBINE_SAME_EMAIL";

        //v1.7.0 Ben 2017.10.09 - Add System Parameter to control combining 713 of renewed staff and old staff
        //Assume 1. same person's old employment terminate date = new employment commencedate, it's renewed. Then 713 calculation will use old 12 months.
        //       2. If multi old employment found Uncount Days,Related Days are averaged while RI, OT are summed
        //       3. 
        public const String COMBINE_RENEWEDSTAFF_713 = "COMBINE_RENEWEDSTAFF_713";


        /*-v1.7.0 Sun 2017.10.18: add system parameter to enable the restriction on not allowing cancellation of AL when NPL is found in the future-*/
        public const String ENABLE_NOT_ALLOW_CANCEL_AL_WITH_NPL = "ENABLE_NOT_ALLOW_CANCEL_AL_WITH_NPL";

        /*-v1.7.0 Sun 2017.10.18: add system parameter to enable the restriction on not allowing application of NPL when AL balance >= 0.5 -*/
        public const String ENABLE_NOT_ALLOW_APPLY_NPL_WITH_HalfDayAL = "ENABLE_NOT_ALLOW_APPLY_NPL_WITH_HalfDayAL";

        /*-v1.7.0 Sun 2017.10.23: add system parameter to enable the restriction on forfeiting AL balance (Max 1/3 of entitlement) when staff terminate or contract renewal -*/
        public const string ENABLE_FORFEIT_ALBALANCE_ON_TERMINATEORRENEW = "ENABLE_FORFEIT_ALBALANCE_ON_TERMINATEORRENEW";

        /*-v1.7.0 Sun 2017.10.23: add system parameter to enable all annual entitlement AL leave to be released in New financial year -*/
        public const string ENABLE_RELEASE_ALL_ALENTITLEMENT = "ENABLE_RELEASE_ALL_ALENTITLEMENT";

        public const string ENABLE_GRATUITY = "ENABLE_GRATUITY";

        /*-v1.7.0 Sun 2017.10.26: add system parameter to apply HKCAAVQ logic on CLOT application -*/
        public const string ENABLE_HKCAAVQ_CLOT_APPLICATION_LOGIC = "ENABLE_HKCAAVQ_CLOT_APPLICATION_LOGIC";

        public const string ENABLE_CLOT_DEFAULT_SHIFT_HOUR = "ENABLE_CLOT_DEFAULT_SHIFT_HOUR";

        //v1.7.0 Ben 2017.11.07: Don't use now
        //v1.7.0 Ben 2017.11.06: add system parameter to show New Employment History By Conversion Button in StaffDetail
        //public const string ENABLE_CONVERSION_OF_EMPLOYMENTHISTORY_STAFF = "ENABLE_CONVERSION_OF_EMPLOYMENTHISTORY_STAFF";

        //v1.7.0 Ben 2017.11.06: add system parameter to set if contract type is mandatory
        public const string MANDATORY_CONTRACTTYPE = "MANDATORY_CONTRACTTYPE";
        //v1.7.0 Ben 2017.11.07: add system parameter to set the months within will return to staff term
        public const string DEFAULT_MONTH_TERMINATE = "DEFAULT_MONTH_TERMINATE";
        //v1.7.0 Ben 2017.11.10: add system parameter to set the Visible of different Salary Basis
        public const string SHOW_SALARY_BASIS = "SHOW_SALARY_BASIS";

        //v1.7.0 Janet 2017.11.14 - Block Back Date Leave AL - Begin
        public const string LEAVE_BLOCK_BACKDATE_APPLICATION = "LEAVE_BLOCK_BACKDATE_APPLICATION";
        public const string LEAVE_BLOCK_BACKDATE_CANCELLATION = "LEAVE_BLOCK_BACKDATE_CANCELLATION";
        public const string LEAVE_BLOCK_BACKDATE_WITHDRAWAL = "LEAVE_BLOCK_BACKDATE_WITHDRAWAL";
        //v1.7.0 Janet 2017.11.14 - Block Back Date Leave AL - Begin

        //v1.7.0 Ben 2017.12.07 - add system parameter to set view all or view ASSOCIATED ZONE
        public const string ENABLE_EMPLOYMENT_ASSOZONE_ON_PORTAL_LEAVECALENDER = "ENABLE_EMPLOYMENT_ASSOZONE_ON_PORTAL_LEAVECALENDER";

        /*-v1.7.0 Sun 2017.12.06 - add system parameter to hide the AL entitlement type in UI -*/
        public const string LEAVE_HIDE_AL_ENTITLEMENT_TYPE = "LEAVE_HIDE_AL_ENTITLEMENT_TYPE";

        /*-v1.7.0 Sun 2017.12.28 - add system parameter to enable excluding future record in accumulable balance calculation -*/
        public const string LEAVE_ACCUMULABLE_BALANCE_EXCLUDE_FUTURERECORD = "LEAVE_ACCUMULABLE_BALANCE_EXCLUDE_FUTURERECORD";

        //v1.8.1 Ben 2018.01.10 - add system parameter to limit the availiable year starting from
        public const string PORTAL_TAXATION_START_YEAR = "PORTAL_TAXATION_START_YEAR";

        /*-v1.8.1 Sun 2018.01.11 - add system parameter to control SL consecutive days calculation scope: approved or approved + pending approval applications -*/
        public const string LEAVE_SL_MERGE_APPROVE_PENDING_CONSECUTIVE_CALC = "LEAVE_SL_MERGE_APPROVE_PENDING_CONSECUTIVE_CALC";

        //v1.8.1 Ben 2018.01.22 - add system parameter to show/hide of Reminding Notice Drop Down List next to field Expiry Date of any attachment
        public const string SHOW_REMINDING_NOTICE = "SHOW_REMINDING_NOTICE";

        //v1.8.1 Fai 2018.01.25 - Payroll Trial post all MPF in Latest Employment
        public const string POST_MPF_TO_LATEST_STAFF = "POST_MPF_TO_LATEST_STAFF";

        //v1.8.1 Fai 2018.02.02 - AL Compensate support PayCode Formula
        public const string COMPENSATE_AL_USING_PAYCODEFORMULA = "COMPENSATE_AL_USING_PAYCODEFORMULA";

        //v1.8.1 Janet 2008.02.02 - where the commencing day is the last day of a month, at the end of the last day - Begin
        public const string ENABLE_ENDOFNOTICE_CALCULATION_END_TO_END = "ENABLE_ENDOFNOTICE_CALCULATION_END_TO_END";
        //v1.8.1 Janet 2008.02.02 - where the commencing day is the last day of a month, at the end of the last day - End

        //v1.8.1 Ben 2018.02.05 - Hide the Spouse Name in Person
        public const string HIDE_PERSON_SPOUSE_NAME = "HIDE_PERSON_SPOUSE_NAME";

        //v1.8.1 Ben 2018.02.28 - Roundup the AL Balance in Common Leave Year for the 1st year of renewed staff using its 1st Commence Date but not Join Date
        public const string LEAVE_ROUNDUP_1STYEAR_RENEWEDSTAFF_BY_COMMENCEDATE = "LEAVE_ROUNDUP_1STYEAR_RENEWEDSTAFF_BY_COMMENCEDATE";

        /*-v1.8.1 Sun 2018.01.23: add Condition to launch AL round up option >> show the panel -*/
        public const string LEAVE_HIDE_AL_ENTITLEMENT_ROUNDUP = "LEAVE_HIDE_AL_ENTITLEMENT_ROUNDUP";

        /*-v1.8.1 Sun 2018.02.25: enable AL balance returned by granting last year's entitlement -*/
        public const string LEAVE_HIDE_GRANT_LAST_YEAR_ENTITLEMENT = "LEAVE_HIDE_GRANT_LAST_YEAR_ENTITLEMENT";

        public const string GRIDVIEW_FOR_LEAVE_ENTITLEMENT_CONDITION = "GRIDVIEW_FOR_LEAVE_ENTITLEMENT_CONDITION";

        //v1.8.2 Ben 2018.03.28 - Enable OT has expiry setting
        public const string ENABLE_OT_DAYS_EXPIRY = "ENABLE_OT_DAYS_EXPIRY";
        //v1.8.4 Ben 2018.04.17 - Enable Save Confirmation
        public const string ENABLE_SAVE_CONFIRMMESSAGE = "ENABLE_SAVE_CONFIRMMESSAGE";

        //v1.8.4 Ben 2018.04.20 - Replace ENABLE_LEAVE_CALCULATION_FORMULA with ENABLE_PAYCODEFORMULA
        public const string ENABLE_PAYCODEFORMULA = "ENABLE_PAYCODEFORMULA";

        //v1.8.4 Ben 2018.04.30 - Disable just as it says
        public const string TEMP_DISABLE_FORCELOGOUT_INLOGINPAGE = "TEMP_DISABLE_FORCELOGOUT_INLOGINPAGE";

        //v1.8.4 Ben 2018.05.07 - Control what add button will do
        public const string LEAVE_ADD_BUTTON_MODE = "LEAVE_ADD_BUTTON_MODE";

        //v1.8.4 Billy 2018.05.07 - Leave Batch Mode
        public const string ENABLE_LEAVE_BATCH_MODE = "ENABLE_LEAVE_BATCH_MODE";

        //public const string ESTIMATE_USING_CONTRACT_END_DATE = "ESTIMATE_USING_CONTRACT_END_DATE";
        public const string LEAVE_APPLICATION_DEFAULT_ESTIMATE_DATE = "LEAVE_APPLICATION_DEFAULT_ESTIMATE_DATE";

        //v1.8.4 Fai 2018.05.18 - This is a list of leave code (Default = NML, Nishi = ""), so if NML in Holiday, Holiday Code will be overwrite Leave Code on that day
        public const string HOLIDAY_OVERRIDE_LEAVE_WHEN_LEAVE_ON_HOLIDAY = "HOLIDAY_OVERRIDE_LEAVE_WHEN_LEAVE_ON_HOLIDAY";

        //v1.8.6 Ben 2018.07.04 - The number of rows in shift session
        public const string DEFAULT_SESSION_ROWS = "DEFAULT_SESSION_ROWS";

        //v1.8.6 Ben 2018.07.04 - The columns show/hide
        public const string HIDE_OPTIONAL_COLUMNS_IN_SESSIONS = "HIDE_OPTIONAL_COLUMNS_IN_SESSIONS";

        //v1.8.6 Fai 2018.07.06 - Payroll (Basic Salary Prorata / Service Day) Support using Pay Code Formula
        public const string PAYROLL_BASICSALARY_PRORATA_USING_PAYCODEFORMULA = "PAYROLL_BASICSALARY_PRORATA_USING_PAYCODEFORMULA";
        public const string PAYROLL_SERVICEDAY_USING_PAYCODEFORMULA = "PAYROLL_SERVICEDAY_USING_PAYCODEFORMULA";

        //v1.8.8 Ben 2018.08.14 - BAGUIO ONLY. It will add D after R,ST,AL in exporting attendancefor non monthly employment
        public const string ROSTER_ADD_D_ON_NOTMONTHLYEMP = "ROSTER_ADD_D_ON_NOTMONTHLYEMP";

        //v1.8.8 Billy 2018.08.28 - My Portal hourly leave lock total hour
        public const string MYPORTAL_HOURLY_LEAVE_LOCK_TOTALHOUR = "MYPORTAL_HOURLY_LEAVE_LOCK_TOTALHOUR";

        //v1.8.8 Ben 2018.09.04 - The sessions allow to be modified in UI
        public const string MAX_ATTENDANCE_SESSION = "MAX_ATTENDANCE_SESSION";

        //v1.8.8 Elvira 2018.11.30 - WAIVE SLBALANCE DEDUCTION ON RESTDAY
        public const string LEAVE_WAIVE_SLBALANCE_DEDUCTION_ON_RESTDAY = "LEAVE_WAIVE_SLBALANCE_DEDUCTION_ON_RESTDAY";
        public const string LEAVE_WAIVE_SLBALANCE_DEDUCTION_ON_BANKHOLIDAY = "LEAVE_WAIVE_SLBALANCE_DEDUCTION_ON_BANKHOLIDAY";

        //v1.8.8 Elvira 2018.12.12 - LEAVE ENTITLEMENT AL START TO ENTITLE
        //Annual Leave calculation for labor law
        //if staff work for less then 3 months for the leave year, will staff terminate, will not entitle to AL compensate for remaining AL balance
        public const string LEAVE_ENTITLEMENT_AL_START_TO_ENTITLE = "LEAVE_ENTITLEMENT_AL_START_TO_ENTITLE";

        //v1.8.8 Elvira 2018.12.17 - According Join Date / Common Leave Year to separate the leave cycle 
        //null / string.empty : by join date
        //otherwise, using the date as common leave year
        public const string LEAVE_HIDE_AL_COMMON_LEAVE_YEAR = "LEAVE_HIDE_AL_COMMON_LEAVE_YEAR";

        //v1.8.8 Billy 2018.12.24 - Hide CL/OT Time From/To period
        public const string HIDE_CLOT_TIME_FROMTO = "HIDE_CLOT_TIME_FROMTO";

        //v1.8.8 Billy 2019.03.06 - Hide Night Shift Balance
        public const string HIDE_NIGHTSHIFT_BALANCE = "HIDE_NIGHTSHIFT_BALANCE";

        //v1.8.8 Ben 2019.03.11 - For Chinese Women, they do not want to create Planned Rotser by whole month
        public const string CREATE_PLANNEDROSTER_BY_RAWDATA_ONLY = "CREATE_PLANNEDROSTER_BY_RAWDATA_ONLY";

        //v1.8.8 Alex 2019.04.23 - By request, controls whether the total amount is the sum of truncated tax items or untruncated tax items
        public const string TAXATION_IR56_ADD_REMAINDER_TO_SALARY_WAGES = "TAXATION_IR56_ADD_REMAINDER_TO_SALARY_WAGES";

        //v1.8.8 Billy 2019.05.02 - system parameter to hide total minutes summary in Attendance monthly view
        public const string HIDE_TOTALTIME_SUMMARY_IN_ATTENDANCE_MONTHLY_VIEW = "HIDE_TOTALTIME_SUMMARY_IN_ATTENDANCE_MONTHLY_VIEW";

        //v1.8.8 Fai 2019.05.22 - Payment Claim
        public const string CLOT_ENABLE_PAYMENT_CLAIM = "CLOT_ENABLE_PAYMENT_CLAIM";

        //v1.8.8 Alex 2019.05.30 - Enable Display of Shift/Leave details of Zone on Clicking Cell of the Leave Calendar (0 = No, 1 = Yes)
        public const string LEAVE_CALENDAR_DETAIL_GRID = "LEAVE_CALENDAR_DETAIL_GRID";

        //v1.8.8 Alex 2019.05.30 - Available components: {surname}, {othernames}, {nickname}, {chinesename}, {nicknamebrackets} & {chinesenamebrackets}. Default: {nickname} {surname}{chinesenamebrackets}
        public const string LEAVE_CALENDAR_STAFF_NAME_FORMAT = "LEAVE_CALENDAR_STAFF_NAME_FORMAT";

        //v1.8.8 Billy - system parameter to control show/hide demo items
        public const string ROSTER_SKILL_DEMO = "ROSTER_SKILL_DEMO";

        //v1.8.8 Alex 2019.06.04 - Status dialog timeout in seconds
        public const string DIALOG_TIMEOUT_SECS = "DIALOG_TIMEOUT_SECS";

        //v1.8.8 Alex 2019.06.06 - Whether to Show Edit Confirm Message
        public const string ENABLE_EDIT_CONFIRM = "ENABLE_EDIT_CONFIRM";

        //v1.8.8 Alex 2019.06.18 - Hide telephone number in IR56F/G form
        public const string TAXATION_IR56F_G_HIDE_TELEPHONE_NUMBER = "TAXATION_IR56F_G_HIDE_TELEPHONE_NUMBER";

        //v1.8.8 Ben 2019.06.27 - Chinese Women wants carry forward on original carry forward's next date
        public const string MAX_CARRY_FORWARD_DAY_DELAY = "MAX_CARRY_FORWARD_DAY_DELAY";

        //v1.8.8 Fai 2019.07.11 - Add System parameter to control IsIncludedInIL show or not
        public const string HIDE_PAYITEMTYPE_INCLUDEDINIL = "HIDE_PAYITEMTYPE_INCLUDEDINIL";

        //v1.8.8 Billy 2019.07.11 - Adjustment value of PayrollTrial BasicSalaryProrata rounding problem
        public const string BASICSALARYPRORATA_ADJUSTMENT_VALUE = "BASICSALARYPRORATA_ADJUSTMENT_VALUE";

        //v1.8.8 Alex 2019.07.19 - Recalculate 713 and minimum wages before calculating payroll if payroll period/leave period/OT period mismatch
        public const string PAYROLL_RECALC_713_MW_PAYROLLP_LEAVEP_OTP = "PAYROLL_RECALC_713_MW_PAYROLLP_LEAVEP_OTP";

        //v1.8.8 Alex 2019.07.22 - Wages greater than this value will skip the above operation
        public const string PAYROLL_RECALC_713_MW_MAX_SALARY = "PAYROLL_RECALC_713_MW_MAX_SALARY";

        //v1.8.8 Alex 2019.08.05 - In payroll calculation, group cross-month leave dates by formula of corresponding month. If the formulae are the same, group those dates regardless of month.
        public const string PAYROLL_ENABLE_713_CROSS_MONTH_GROUP_BY_FORMULA = "PAYROLL_ENABLE_713_CROSS_MONTH_GROUP_BY_FORMULA";

        //v1.8.8 Billy 2019.08.09 - Hide Severe weather Payitem type
        public const string HIDE_SEVEREWEATHER_PAYITEMTYPE = "HIDE_SEVEREWEATHER_PAYITEMTYPE";

        //v1.8.8 Alex 2019.08.14 - Payroll ORSO Rounding Significant Figures
        public const string ORSO_ROUNDING = "ORSO_ROUNDING";

        //v1.8.8 Ben 2019.10.29 - Add to hide 'Import Exception Report' button
        public const string HIDE_BUTTON_IMPORT_EXCEPTION_REPORT = "HIDE_BUTTON_IMPORT_EXCEPTION_REPORT";

        //Ben 2020.01.20 - If using limit to find in/out fails, force using the nearest
        public const string ATTENDANCE_INOUT_USE_NEAREST_RAWDATA_IFNOTFIND = "ATTENDANCE_INOUT_USE_NEAREST_RAWDATA_IFNOTFIND";

        //Billy 2020.01.29 - Default value for Export Roster - Empty Template
        public const string HIDDEN_DEFAULT_SHIFT = "HIDDEN_DEFAULT_SHIFT";

        //Ben 2020.02.04 - Default checking of Continuous Employment in EmploymentDetail.aspx
        public const string EMPLOYMENY_CONTINUOUS_DEFAULT_CHECK = "EMPLOYMENY_CONTINUOUS_DEFAULT_CHECK";

        // Billly 2020.02.04 - Export roster ignore rostergroup setting
        public const string EXPORT_ROSTER_IGNORE_ROSTERGROUP_FILTERING = "EXPORT_ROSTER_IGNORE_ROSTERGROUP_FILTERING";

        //Ben 2020.02.10 - To replace ENABLE_VALIDATION_2FSL_PER_MONTH and enhance to control the maximum number
        //  Will remove ENABLE_VALIDATION_2FSL_PER_MONTH later
        public const string LEAVE_ENABLE_MAX_FSL_PER_MONTH = "LEAVE_ENABLE_MAX_FSL_PER_MONTH";

        //Ben 2020.02.17 - Add columns for Nishi
        public const string REPORT_SHOW_YEARLYJOIN_RELATED = "REPORT_SHOW_YEARLYJOIN_RELATED";

        //Linus 2020-04-09 - control diplay employment / staff number in ppp file
        public const string PAYROLL_PPP_FILE_DISPLAY_SECOND_PARTY_IDENTIFIER = "PAYROLL_PPP_FILE_DISPLAY_SECOND_PARTY_IDENTIFIER";
        public const string ATTENDANCE_HIDE_TEAM = "ATTENDANCE_HIDE_TEAM";

        // Billy 2020.06.18 - Show/Hide Person - Recrutiment tab
        public const string SHOW_PERSON_RECRUITMENT = "SHOW_PERSON_RECRUITMENT";

        // Billy 2020.06.24 - Show all shift code in Export Roster Summary
        public const string SHOW_ALL_SHIFTCODE_IN_EXPORT_ROSTER_SUMMARY = "SHOW_ALL_SHIFTCODE_IN_EXPORT_ROSTER_SUMMARY";

        /*Linus 2020-09-24*/
        public const string EXPORT_BLANK_ROSTER = "EXPORT_BLANK_ROSTER";
        public const string HIDDEN_EXPORT_ROSTER_COLUMN = "HIDDEN_EXPORT_ROSTER_COLUMN";
        public const string EXPORT_ROSTER_CHECKBOX_DEFAULT = "EXPORT_ROSTER_CHECKBOX_DEFAULT";

        // Billy 2020.10.06 - Exclude ORSO Trustee in staff mpf setting
        public const string EXCLUDE_ORSO_TRUSTEE_IN_STAFF_MPF_SETTING = "EXCLUDE_ORSO_TRUSTEE_IN_STAFF_MPF_SETTING";

        // Kyrie 2020-10-28 - Employment Paid Holiday Start Date By Default Month
        public const string EMPLOYMENT_PAID_HOLIDAY_START_DATE_DEFAULT_MONTH = "EMPLOYMENT_PAID_HOLIDAY_START_DATE_DEFAULT_MONTH";

        //Kyrie 2020-10-29 - PersonInfo Related Staff List Join Date
        public const string RELATED_STAFF_LIST_JOIN_DATE = "RELATED_STAFF_LIST_JOIN_DATE";

        //Kyrie 2020-11-03 - Hidden buttons in Attendance Details
        public const string HIDDEN_BUTTON_IN_ATTENDANCE_DETAILS = "HIDDEN_BUTTON_IN_ATTENDANCE_DETAILS";

        //Kyrie 2020-11-04 - Hidden buttons in Attendance Details
        public const string HIDDEN_COLUMN_IN_ATTENDANCE_DETAILS = "HIDDEN_COLUMN_IN_ATTENDANCE_DETAILS";

        //Linus 2020-12-02 - Payroll TrialCal by Store Procedure
        public const string PAYROLL_CAL_BY_STORE_PROCEDURE = "PAYROLL_CAL_BY_STORE_PROCEDURE";

        //Kyrie 2020-12-07 - Hidden content
        public const string HIDDEN_CONTENT = "HIDDEN_CONTENT";

        //Kyrie 2020-12-24 - PersonInfo Document Type
        public const string PERSONINFO_DOCUMENT_TYPE = "PERSONINFO_DOCUMENT_TYPE";

        //Kyrie 2021-03-08 - Hidden column in payitem batch import template
        public const string PAYITEM_BATCH_IMPORT_HEADER = "PAYITEM_BATCH_IMPORT_HEADER";

        //Linus 2021-03-11 - Control License Checking when login
        public const string SYSTEM_LOGIN_LICENSE_CHECKING = "SYSTEM_LOGIN_LICENSE_CHECKING";

        //Tony 2021-03-26 - Attendance contract code use store procedure to import time card
        public const string ATTENDANCE_NEW_IMPORT_TIME_CARD = "ATTENDANCE_NEW_IMPORT_TIME_CARD";

        //Linus 2021-04-12 - Attendance Report sorting
        public const string ATTENDANCE_SORTING = "ATTENDANCE_SORTING";

        //Kyrie 2021-05-24 - ADDITION_SCORE_IN_APPRAISAL
        public const string ADDITION_SCORE_IN_APPRAISAL = "ADDITION_SCORE_IN_APPRAISAL";

        //Kyrie 2021-07-06 - Person advanced option recruitment tabs 
        public const string SHOW_PERSON_RECRUITMENT_ADVANCED_OPTION = "SHOW_PERSON_RECRUITMENT_ADVANCED_OPTION";

        // BIlly 2021.07.19 - MyPortal 
        public const string USE_ZIPFORMAT_FOR_MYPORTAL_PAYSLIP_TAXATION = "USE_ZIPFORMAT_FOR_MYPORTAL_PAYSLIP_TAXATION";

        //Kyrie 2021-09-09 - Import roster hourly Baguio mode
        public const string IMPORT_ROSTER_HOURLY_BAGUIO = "IMPORT_ROSTER_HOURLY_BAGUIO";

        //Tony 2021-10-09 - Allow Modifiy Future AttendanceDetail
        public const string ALLOW_MODIFIY_FUTURE_ATTENDANCEDETAIL = "ALLOW_MODIFIY_FUTURE_ATTENDANCEDETAIL";

        public const string Portal_InBox_OutBox_Mode = "Portal_InBox_OutBox_Mode";

        public const string STAFF_SCHEME_MANDATORY = "STAFF_SCHEME_MANDATORY";

        // Billy 2021.11.17 Medical Claim check Probation
        public const string MEDICAL_EXPENSE_CLAIM_CHECK_PROBATION = "MEDICAL_EXPENSE_CLAIM_CHECK_PROBATION";

        public const string LEAVE_CLOTHALFHOUR = "LEAVE_CLOTHALFHOUR";

        //BIlly 2021.12.02 - defalut selection of taxation version
        public const string TAXATION_DEFAULT_VERSION = "TAXATION_DEFAULT_VERSION";

        #endregion


        //懶加載
        public static bool mCLOTOnlyHalfHours()
        {
            bool result = false;
            string str = WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetSystemParameter(SystemParameters.LEAVE_CLOTHALFHOUR);
            if (!string.IsNullOrEmpty(str))
            {
                result = str == "1";
            }
            return result;
        }

        public static int mLeaveHourUnit()
        {
            int result = -1;
            string str = WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetSystemParameter(SystemParameters.HOURLY_LEAVE_MINUTES_UNIT);
            if (!string.IsNullOrEmpty(str))
            {
                int.TryParse(str, out result);
            }
            return result;
        }

        

        //從hr copy 過來。需要那個就挑代碼出來，寫個懶加載方法吧。
        ////设置参数默认值
        //public bool mIsBatch = false;
        //public bool misClearAndAdd = false;
        //public bool mHiddenActingStaff = true;
        //public bool p_leaveCalendarDetail_Grid = false;
        //public bool p_boolAskConfirmation = true;
        //public string IsEnableHKCAAVQCLOTLogic = "";
        //public string IsHIDE_CLOT_TIME_FROMTO = "";
        //public string UI_DisplayDecimal = "";
        //public bool mHiddenCLOTAdmin = false;
        //public bool mHiddenCLOTPortal = false;
        //public bool mHiddenDoctorPortal = false;
        //public bool mHiddenDoctorAdmin = false;
        //public bool mBLOCK_BACKDATE_CANCELLATION = false;
        //public bool mINBOX_UNREADONLY_ON_MY_TASK = true;
        //public int mGridSize = 16;
        //public bool mShowPositionCode = false;
        //public bool mLEAVE_SL_MERGE_APPROVE_PENDING_CONSECUTIVE_CALC = false;
        //public double mDayDefination = 0;
        //public bool mUseSnapshot = false;
        //public int mAlv2 = 1;
        //public bool mNewDocType = false;
        //public bool mAttachmentHiddenDoctor = true;
        //public int mPortal_InBox_OutBox_Mode = 2;//0:old 1.batch mode 2. addfilter for batch mode.
        //public bool mCalculateYearlyJoinRelated = true;
        //public string IsUseDefaultShiftHour = "";




        ///// <summary>
        ///// should just call it once when page_load.
        ///// </summary>
        ///// <param name="session"></param>
        ///// <returns></returns>
        //public static SystemParameters GetSysParameters()
        //{

        //    SystemParameters data = new SystemParameters();



        //    //从外部加载重新赋值
        //    string[] hiddenContentArray = GetSysPara(HIDDEN_CONTENT).Split(new string[] { "," }, StringSplitOptions.None);
        //    data.mIsBatch = GetSysPara( ENABLE_LEAVE_BATCH_MODE) == "1";
        //    data.misClearAndAdd = GetSysPara(LEAVE_ADD_BUTTON_MODE) == "2";
        //    data.mHiddenActingStaff = true; //mHiddenActingStaff todo 0 add it in db later.
        //    data.p_leaveCalendarDetail_Grid = GetSysPara(LEAVE_CALENDAR_DETAIL_GRID) == "1";
        //    data.p_boolAskConfirmation = GetSysPara(ENABLE_SAVE_CONFIRMMESSAGE) != "0";
        //    data.IsEnableHKCAAVQCLOTLogic = GetSysPara(ENABLE_HKCAAVQ_CLOT_APPLICATION_LOGIC);
        //    data.IsHIDE_CLOT_TIME_FROMTO = GetSysPara(HIDE_CLOT_TIME_FROMTO);
        //    data.UI_DisplayDecimal = GetSysPara(UI_DISPLAY_DECIMAL);

        //    data.IsUseDefaultShiftHour = GetSysPara(ENABLE_CLOT_DEFAULT_SHIFT_HOUR);

        //    data.mHiddenCLOTPortal = GetSysPara(HIDDEN_CLOT_APPLICATION_FOR_PORTAL) == "1";
        //    data.mHiddenCLOTAdmin = GetSysPara(HIDDEN_CLOT_APPLICATION) == "1";
        //    data.mHiddenDoctorPortal = GetSysPara(HIDDEN_DOCTOR_VISIT_APPLICATION) == "1";
        //    data.mHiddenDoctorAdmin = GetSysPara(HIDDEN_DOCTOR_VISIT_APPLICATION) == "1";

        //    data.mAttachmentHiddenDoctor = GetSysPara(PORTAL_LEAVE_HIDE_DOCTOR_RECEIPT) == "0" ? false : true;

        //    data.mBLOCK_BACKDATE_CANCELLATION = GetSysPara(LEAVE_BLOCK_BACKDATE_CANCELLATION) == "1";
        //    data.mINBOX_UNREADONLY_ON_MY_TASK = GetSysPara(DEFAULT_INBOX_UNREADONLY_ON_MY_TASK) == "0" ? false : true;
        //    data.mGridSize = int.Parse(GetSysPara(PORTAL_GRID_SIZE));
        //    data.mShowPositionCode = Convert.ToBoolean(int.Parse(GetSysPara(SHOW_POSITION_PORTAL_INBOX)));

        //    data.mLEAVE_SL_MERGE_APPROVE_PENDING_CONSECUTIVE_CALC = GetSysPara(LEAVE_SL_MERGE_APPROVE_PENDING_CONSECUTIVE_CALC) == "1" ? true : false;

        //    data.mDayDefination = Convert.ToDouble(GetSysPara(SICK_LEAVE_CONSECUTIVE_DAY_DEFINITION));
        //    data.mUseSnapshot = GetSysPara(LEAVE_BALANCE_SNAPSHOT_DAY) != "";

        //    data.mAlv2 = GetSysPara(ALBALANCEVERSION) == "0" ? 0 : 1;
        //    data.mNewDocType = GetSysPara(PERSONINFO_DOCUMENT_TYPE) == "1" ? true : false;
        //    string piomString = GetSysPara(Portal_InBox_OutBox_Mode);
        //    if (!string.IsNullOrEmpty(piomString))
        //    {
        //        int.TryParse(piomString, out data.mPortal_InBox_OutBox_Mode);
        //    }
        //    data.mCalculateYearlyJoinRelated = GetSysPara(REPORT_SHOW_YEARLYJOIN_RELATED) == "1";
        //    data.mCLOTOnlyHalfHours = GetSysPara(LEAVE_CLOTHALFHOUR) == "1";


        //    return data;
        //}

    }
}