using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.temp
{
    public partial class testRQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var all= Request.Form;
            var allkeys=all.Keys;

            for (int i = 0; i < allkeys.Count; i++)
            {
                this.Label1.Text += all.Keys[i].ToString() + "  :  " + all[i].ToString();
            }
        }
    }
}