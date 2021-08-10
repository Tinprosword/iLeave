using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Controls
{
    public partial class CLOTTab : System.Web.UI.UserControl
    {
        private const string ConstString_EventName1 = "tabnew";
        private const string ConstString_EventName2 = "tabpending";
        private const string ConstString_EventName3 = "tabhistory";

        private bool isManage = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public  void SetupControls(bool PisManage)
        {
            isManage = PisManage;
            this.a_new.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName1 + "','')");
            this.a_pending.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName2 + "','')");
            this.a_history.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName3 + "','')");
            if (isManage)
            {
                this.a_new.Visible = false;
            }
        }

        public void SetEvent(WEBUI.Controls.leave masterpage)
        {
            string value1 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName1);
            string value2 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName2);
            string value3 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName3);
            string datatype = isManage == true ? "0" : "1";
            if (value1 != null)
            {
                string url = "~/pages/ApplyCLOT.aspx";
                Response.Redirect(url);
            }
            else if (value2 != null)
            {
                string url = "~/pages/CLOTHistory.aspx?datatype="+ datatype + "&dataRange=0";
                Response.Redirect(url);
            }

            else if (value3 != null)
            {
                string url = "~/pages/CLOTHistory.aspx?datatype=" + datatype + "&dataRange=1";
                Response.Redirect(url);
            }
        }

        public void showTabActive(int index)
        {
            this.myTabapply_new.Attributes.Remove("class");
            this.myTabapply_pending.Attributes.Remove("class");
            this.myTabapply_history.Attributes.Remove("class");

            if (index == 0)
            {
                this.myTabapply_new.Attributes.Add("class", "active");
            }
            else if (index == 1)
            {
                this.myTabapply_pending.Attributes.Add("class", "active");
            }
            else if(index==2)
            {
                this.myTabapply_history.Attributes.Add("class", "active");
            }
        }

        public void MultipLanguage()
        {
            lt_new.Text= BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_new;
            lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_pending;
            lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_processed;
        }
    }
}