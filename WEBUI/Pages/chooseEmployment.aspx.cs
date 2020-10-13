using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WEBUI.Pages
{
    public partial class chooseEmployment : BLL.CustomLoginTemplate
    {
        private int sourceType = 1;//1.login 2:setting
        private int pid;
        private WebServiceLayer.WebReference_user.PersonBaseinfo[] canlogins;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            
        }

        protected override void InitPage_OnFirstLoad2()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["sourceType"]))
            {
                sourceType = int.Parse(Request.QueryString["sourceType"]);
                pid = int.Parse(Request.QueryString["pid"]);

                WebServiceLayer.WebReference_user.PersonBaseinfo[] allinfos = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
                canlogins = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.FilterCanLoginUser(allinfos);
                if (canlogins != null && canlogins.Count() == 1 && sourceType == 1)
                {
                    Response.Redirect("main.aspx");
                }
            }
            else
            {
                BLL.User_wsref.GoBackToLogin();
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            
        }

       
        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            if (sourceType == 2)
            {
                //todo show navigation
            }
            this.rp_items.DataSource = canlogins;
            this.rp_items.DataBind();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
        }


        protected void chooseMe(object target, EventArgs aa)
        {
            System.Web.UI.HtmlControls.HtmlButton btn = (System.Web.UI.HtmlControls.HtmlButton)target;
            int eid = int.Parse(btn.Attributes["eid"]);
            string eno = btn.Attributes["eno"];

            if (sourceType == 1)
            {
                BLL.User_wsref.ChangeInfoToSession(eid, eno);
                Response.Redirect("main.aspx");
            }
            else
            {
                BLL.User_wsref.ChangeInfoToSession(eid, eno);
                //todo back
            }
        }


        protected string ShowEmploymentDesc(int eid)
        {
            WebServiceLayer.WebReference_user.EmployDetail employDetail = BLL.User_wsref.GetEmployDetailByeid(eid);
            string desc = employDetail.s_staffnumber + "<br/>";
            desc += employDetail.Position + "<br/>";
            desc += employDetail.Contract + "<br/>";
            desc += employDetail.Zone + "<br/>";
            desc += employDetail.Shift;
            return desc;
        }

    }
}