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
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }



        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            
        }

        protected override void InitPage_OnNotFirstLoad2()
        {

        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().announcement_Title, "~/pages/main.aspx", true);

        }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        {

        }




        protected string RP_DisplayAttach(MODEL.Announcement.Attachement item)
        {
            string result = "";
            if (item != null )
            {
                result = item.fileName;
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
                var attachInfo = BLL.Other.GetAttachementByattID(attachmentID);
                string filePath = attachInfo.Path;
                string fileName = BLL.Other.Attachment_GetFileName(filePath);

                byte[] fileData = BLL.Other.GetByteByAttachmentid(attachmentID);
                if (fileData != null && fileData.Length > 0)
                {
                    LSLibrary.WebAPP.httpHelper.SimpleDownloadLocalFile_ClearAndEndResponse(fileData, this.Response, fileName);
                }
            }
        }
    }
}