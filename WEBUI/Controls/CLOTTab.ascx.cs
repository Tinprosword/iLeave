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

        private bool tab1show = false;
        private bool tab2show = false;
        private bool tab3show = false;

        private string tab1url = "";//~/pages/ApplyCLOT.aspx
        private string tab2url = "";
        private string tab3url = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetupTAB(bool t1,bool t2,bool t3,string t1_url,string t2_url,string t3_url, WEBUI.Controls.leave masterpage)
        {
            tab1show = t1;
            tab2show = t2;
            tab3show = t3;

            tab1url = t1_url;
            tab2url = t2_url;
            tab3url = t3_url;

            this.a_new.Visible = t1;
            this.a_pending.Visible = t2;
            this.a_history.Visible = t3;

            this.a_new.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName1 + "','')");
            this.a_pending.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName2 + "','')");
            this.a_history.Attributes.Add("onclick", "MyPostBack('" + ConstString_EventName3 + "','')");

            SetEvent(masterpage);
        }


        private void SetEvent(WEBUI.Controls.leave masterpage)
        {
            string value1 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName1);
            string value2 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName2);
            string value3 = ((WEBUI.Controls.leave)masterpage).GetMyPostBackArgumentByTargetname(ConstString_EventName3);


            if (value1 != null)
            {
                string url = tab1url;
                Response.Redirect(url);
            }
            else if (value2 != null)
            {
                string url = tab2url;
                Response.Redirect(url);
            }

            else if (value3 != null)
            {
                string url = tab3url;
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