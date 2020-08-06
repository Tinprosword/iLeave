using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class test : System.Web.UI.Page
    {
        private string viewname = "tt";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<int> source = new List<int>();

            for(int i=0;i<10;i++)
            {
                source.Add(i);
            }

            var des = source.Where(x => x > 20);

            int[] deslist = des.ToArray();
            int aa = deslist.First();
            int a = 4;
        }
    }
}