using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.temp
{
    public partial class demo2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal num1 = (decimal)0.5;
            string strNum = num1.ToString("{0.##}");//0.5
            string strNum2 = num1.ToString("{0.00}");//0.5
            this.Label1.Text = strNum + strNum2;
        }
    }
}