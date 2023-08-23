using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Controls
{
    public partial class leave : System.Web.UI.MasterPage,BLL.ILoginPageMaster
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsfixmsg.Text = "";
            if (!IsPostBack)
            {
                this.appcss.Href += "?lastmodify=" + BLL.GlobalVariate.appcssLastmodify;
            }

            initUi();
        }

        private void initUi()
        {
            this.LB_MSG.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_Message;
            this.LB_Setup.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_current;
        }

        public void SetupNaviagtion(bool isVisitable,string backLink,string title,string url,bool showback, ImageClickEventHandler ClickEvent=null,bool showgoback=false,bool ismainpage=false)
        {
            this.Navigation.Visible = isVisitable;
            this.Navigation2.Visible = showgoback;

            this.label_title.Text = title;
            if (string.IsNullOrEmpty(url))
            {this.ib_back.Click += ClickEvent;}
            else
            {this.ib_back.PostBackUrl = url;}
            this.ib_back.Visible = showback;


            this.tb_img.Visible = ismainpage;
            this.label_title.Visible = !ismainpage;


            this.Navigation.Style.Remove("background-image");
            this.Navigation.Style.Remove("background-color");
            if(ismainpage)
            {
                this.Navigation.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(../res/images/banner.jpg)");
            }
            else
            {
                this.Navigation.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#06468c");
            }

            this.ib_exit.Visible = ismainpage;
        }

        public string GetMyPostBackArgumentByTargetname(string targetName)
        {
            string result = null;
            if (!string.IsNullOrEmpty(Request.Form["mypostback_target"]) && Request.Form["mypostback_target"] == targetName)
            {
                result = Request.Form["mypostback_argument"];
            }
            return result;
        }

        public string GetMyPostTargetname()
        {
            string result = null;
            if (!string.IsNullOrEmpty(Request.Form["mypostback_target"]))
            {
                result = Request.Form["mypostback_target"];
            }
            return result;
        }

        public enum msgtype
        {
            success,
            error,
            info,
        }

        public void SetupMsg(string msg, int mintime, msgtype type)
        {
            msg = LSLibrary.StringUtil.GetSaftControlValue(msg);
            string cssname = "Flostalert-success";
            if (type == msgtype.error)
            {
                cssname = "Flostalert-danger";
            }
            else if (type == msgtype.info)
            {
                cssname = "Flostalert-info";
            }

            this.lt_jsfixmsg.Text = "<script>$('#fixmsg').html('" + msg + "').addClass('"+ cssname + "').show().delay(" + mintime + ").fadeOut();</script>";
        }


        public void SetPageState(string value)
        {
            this.PageState.Value = value;
        }

       

        public static WEBUI.Controls.leave FindMasterPage(System.Web.UI.Page page)
        {
            WEBUI.Controls.leave result = null;

            if(page.Master is WEBUI.Controls.leave)
            {
                result = (WEBUI.Controls.leave)page.Master;
            }

            return result;
        }

        public void ReSetMessageCountLable(int UnReadmsgCount)
        {
            if (UnReadmsgCount > 0)
            {
                this.lb_unreadCount.Text = "(" + UnReadmsgCount.ToString() + ")";
            }
            else
            {
                this.lb_unreadCount.Text = "";
            }
        }

    }
}