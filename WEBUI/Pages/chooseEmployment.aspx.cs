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
        private int pid;
        private WebServiceLayer.WebReference_user.PersonBaseinfo[] canlogins;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            
        }

        protected override void InitPage_OnNotFirstLoad2()
        { }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        { }

        protected override void InitPage_OnFirstLoad2()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                pid = int.Parse(Request.QueryString["pid"]);

                WebServiceLayer.WebReference_user.PersonBaseinfo[] allinfos = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
                canlogins = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.FilterCanLoginUser(allinfos);

                if (canlogins != null && canlogins.Count() == 1)
                {
                    gotoNextPage();
                }
                else
                {
                    //如果是同一个first id. 那么只显示最新的。
                    var firsteid = canlogins.Select(x => x.e_FirstEmploymentID).ToList().Distinct().ToList();
                    if(firsteid!=null && firsteid.Count()==1)
                    {
                        var latesteid = canlogins.OrderByDescending(x => x.e_CommenceDate).ToList();
                        if(latesteid!=null && latesteid.Count()>0)
                        {
                            int eid = latesteid[0].e_id ?? 0;
                            string eno = latesteid[0].e_EmploymentNumber;
                            int sid = latesteid[0].s_id ?? 0;
                            string sno = latesteid[0].s_StaffNumber;

                            BLL.User_wsref.ChangeInfoToSession(eid, eno, sid, sno, true);

                            gotoNextPage();
                        }
                    }
                }
            }
            else
            {
                BLL.User_wsref.MPG_GoBackToLogin();
            }
        }

        private void gotoNextPage()
        {
            string tempUrl = BLL.common.isShortcutQSAndGetDecodeURL(Request);
            if (!string.IsNullOrEmpty(tempUrl))
            {
                tempUrl = HttpUtility.UrlEncode(tempUrl);
                Response.Redirect("getheight.aspx?action=shortcut&url=" + tempUrl);
            }
            else
            {
                Response.Redirect("getheight.aspx");
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            this.rp_items.ItemCreated += Rp_items_ItemCreated;
        }

       
        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().login_employlist, "", false);
            this.rp_items.DataSource = canlogins;
            this.rp_items.DataBind();
        }

        private void Rp_items_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            var item = (RepeaterItem)e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.HtmlControls.HtmlButton lbl = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btn_choose");

                int strindex = e.Item.ItemIndex >= 3 ? 3 : e.Item.ItemIndex;
                lbl.Style.Add("background-image", "url(../res/images/itmea"+ (strindex+1).ToString()+ ".png)");
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}


        protected void chooseMe(object target, EventArgs aa)
        {
            System.Web.UI.HtmlControls.HtmlButton btn = (System.Web.UI.HtmlControls.HtmlButton)target;
            int eid = int.Parse(btn.Attributes["eid"]);
            string eno = btn.Attributes["eno"];
            int sid = int.Parse(btn.Attributes["sid"]);
            string sno = btn.Attributes["sno"];

            BLL.User_wsref.ChangeInfoToSession(eid, eno, sid, sno, true);

            gotoNextPage();
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


        public string GetItemBack(int index)
        {
            return "item"+index.ToString()+".png";
        }

    }
}