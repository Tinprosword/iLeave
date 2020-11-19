using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace WEBUI.temp
{
    /// <summary>
    /// testws 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class testws : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld(string abc)
        {
            System.Threading.Thread.Sleep(2000);

            MODEL.Apply.ajax_data_apply pagedate = LSLibrary.MyJson.UnSObj<MODEL.Apply.ajax_data_apply>(abc);
            return pagedate.pagedata.LeaveTypeSelectValue + "." + pagedate.loginid;


            ////1,获得数据   2,调用ws,进行插入.  3.并把图片放置到制定目录，并插入到数据库
            //List<MODEL.Apply.apply_LeaveData> LeaveList = pagedate.LeaveList;
            //List<MODEL.Apply.App_AttachmentInfo> pics = pagedate.uploadpic;
            //string errorMsg = "";
            //int reslut = BLL.Leave.InsertLeave(LeaveList, 3, 4, null, "444", ref errorMsg);
            //if (reslut >= 0)
            //{
            //    for (int i = 0; i < pics.Count; i++)
            //    {
            //        copyFileTo(pics[i].originAttendance_RelatePath, pics[i].originAttendance_HRDBPath);
            //    }
            //    BLL.Leave.InsertAttachment(pics, loginer.userInfo.id, loginer.userInfo.personid, reslut);
            //    //((WEBUI.Controls.leave)this.Master).SetupMsg(BLL.MultiLanguageHelper.GetLanguagePacket().apply_apply, 2000, WEBUI.Controls.leave.msgtype.success);
            //    Response.Redirect("myapplications.aspx?applicationType=0");
            //    //LSLibrary.WebAPP.httpHelper.ResponseRedirectDalay(2.3f, "", Response);
            //}
            //else
            //{
            //    this.literal_errormsga.Visible = true;
            //    this.literal_errormsga.Text = errorMsg;
            //}

        }

        [WebMethod]
        public int add(int a, int b)
        {
            return a + b;
        }


        [WebMethod]
        public book getbook()
        {
            book temp = new book();
            temp.id = 3;
            temp.name = "abc";
            return temp;
        }

        [WebMethod]
        public List<book> getbooks(int count)
        {
            List<book> result = new List<book>();

            for (int i = 0; i < count; i++)
            {
                book temp = new book();
                temp.id = i;
                temp.name = "abc" + i.ToString();

                result.Add(temp);
            }
            return result;
        }


        [WebMethod]
        public string teststring(string a, string b)
        {
            return a + b;
        }
    }

    public class book
    {
        public int id;
        public string name;
    }
}
