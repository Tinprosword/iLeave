using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class AnnouncementDetail : BLL.CustomLoginTemplate
    {
        private int mAnnounctID = 0;
        private WebServiceLayer.WebReference_Ileave_Other.t_Announcement mT_Announcement = null;
        private List<WebServiceLayer.WebReference_Ileave_Other.t_Attachment> mT_Attachments = null;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            
        }

        protected override void InitPage_OnFirstLoad2()
        {

        }



        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string strid = Request.QueryString["id"];
                int.TryParse(strid, out mAnnounctID);
                if (mAnnounctID <= 0)
                {
                    ResponseOnError();
                }
                else
                {
                    mT_Announcement = BLL.Announcement.GetAnouncementByID(mAnnounctID);
                    if (mT_Announcement != null)
                    {
                        mT_Attachments = BLL.Attachment.GetAttachementByAnnounceID(mAnnounctID);
                        mT_Attachments = mT_Attachments == null ? new List<WebServiceLayer.WebReference_Ileave_Other.t_Attachment>() : mT_Attachments;
                    }
                    else
                    {
                        ResponseOnError();
                    }
                }
            }
            else
            {
                ResponseOnError();
            }
        }

        private void ResponseOnError()
        {
            Response.Redirect("main.aspx");
        }
        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            
        }

        protected override void InitPage_OnNotFirstLoad2()
        {

        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            string backUrl = "~/pages/main.aspx";
            if (mT_Announcement != null)
            {
                backUrl = "~/pages/Announcement.aspx?" + Announcement.qs_activeTab + "=" + mT_Announcement.TypeID.ToString();
            }
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().announcement_Title, backUrl, true);
            this.lb_title.Text = mT_Announcement.Subject.Trim();
            this.lb_content.Text = mT_Announcement.Content.Trim();

            this.rp_attachment.DataSource = mT_Attachments;
            this.rp_attachment.DataBind();

            BLL.Announcement.Announce_ReadAnncount(mAnnounctID, loginer.userInfo.firsteid??0);
        }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        {

        }




        protected string RP_Name(WebServiceLayer.WebReference_Ileave_Other.t_Attachment item,bool Isshort,int maxLenth)
        {
            string result = "";
            if (item != null )
            {
                string attachLinkInfo = "{0}[{1}] {2}";
                string filename = BLL.Attachment.Attachment_GetFileName(item.Path);
                List<LSLibrary.keyValueCommon> types = MODEL.Announcement.GetAttachmentType();
                var thetype = types.Where(x=>x.mKey==item.TypeID).FirstOrDefault();
                string fileType = "Others";
                if (thetype != null)
                {
                    fileType = thetype.mValue;
                }
                maxLenth = maxLenth - filename.Length - fileType.Length;
                string fileRemark = item.Remarks.Trim();
                if (Isshort)
                {
                    fileRemark = LSLibrary.StringUtil.SubString(fileRemark, maxLenth, "...");
                }
                attachLinkInfo = string.Format(attachLinkInfo, filename, fileType, fileRemark);
                result = attachLinkInfo;
            }
            return result;
        }

        protected void lb_attachment1_Click(object sender, EventArgs e)
        {
            LinkButton LB = (LinkButton)sender;
            int attachmentID = 0;
            int.TryParse(LB.CommandArgument, out attachmentID);
            if (attachmentID > 0)
            {
                var attachInfo = mT_Attachments.Where(x => x.ID == attachmentID).FirstOrDefault();
                if (attachInfo != null)
                {
                    string filePath = attachInfo.Path;
                    string fileName = BLL.Attachment.Attachment_GetFileName(filePath);
                    byte[] fileData = BLL.Announcement.GetByteByAttachmentid(attachmentID);

                    if (fileData != null && fileData.Length > 0)
                    {
                        //区分不同的平台。来download.

                        string agent = HttpContext.Current.Request.UserAgent;
                        LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);
                        var cookies = BLL.Page.MyCookieManage.GetCookie();

                        if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android && cookies.isAppLogin == "1")//android
                        {
                            //0.删除超过8小时的文件。1.获得文件的byte[],name  2.在临时文件夹中生成一个临时文件。3.生成 提供下载的js。
                            BLL.Other.DeleteOlderFiles(Server);
                            string TempFileUrl = GenerateFile_TempFloderHardCode(fileName, fileData);
                            string JSDownload = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid("DOWNLOAD3", TempFileUrl, HttpContext.Current.Server);
                            LT_JSDOWNLOAD.Text = JSDownload;
                        }
                        else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1")
                        {
                            BLL.Other.DeleteOlderFiles(Server);
                            string TempFileUrl = GenerateFile_TempFloderHardCode(fileName, fileData);
                            string JSDownload = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("DOWNLOAD3", TempFileUrl, HttpContext.Current.Server);
                            LT_JSDOWNLOAD.Text = JSDownload;
                        }
                        else
                        {
                            LSLibrary.WebAPP.httpHelper.SimpleDownloadLocalFile_ClearAndEndResponse(fileData, this.Response, fileName);
                        }
                    }
                }
            }
        }

        private string GenerateFile_TempFloderHardCode(string tempFileName, byte[] filedate,string tempFloderPath= "~/tempdownload")
        {
            string result = "";
            try
            {
                string tempDatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string tempFileFolderPath = Server.MapPath(tempFloderPath) + LSLibrary.FileUtil.filepathflag + tempDatetime;
                string tempFilePath = LSLibrary.FileUtil.GenerateFileName(tempFileFolderPath, tempFileName);
                string tempFileURL = LSLibrary.WebAPP.httpHelper.GenerateURL("tempdownload/" + tempDatetime + "/" + tempFileName);
                LSLibrary.FileUtil.CreateFile(tempFilePath, filedate);
                result = tempFileURL;
            }
            catch
            {

            }
            return result;
        }


    }
}