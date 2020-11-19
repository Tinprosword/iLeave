using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Controls
{
    public partial class leave : System.Web.UI.MasterPage
    {
        



        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsfixmsg.Text = "";
            if (!IsPostBack)
            {
                this.appcss.Href += "?lastmodify=" + BLL.GlobalVariate.appcssLastmodify;
            }
        }

        public void SetupNaviagtion(bool isVisitable,string backLink,string title,string url,bool showback, ImageClickEventHandler ClickEvent=null,bool showgoback=false)
        {
            this.Navigation.Visible = isVisitable;
            this.Navigation2.Visible = showgoback;


            this.label_title.Text = title;
            if (string.IsNullOrEmpty(url))
            {this.ib_back.Click += ClickEvent;}
            else
            {this.ib_back.PostBackUrl = url;}
            this.ib_back.Visible = showback;

            
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

    }
}