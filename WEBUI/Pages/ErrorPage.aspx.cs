﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["message"]))
                {
                    this.Label1.Text = "Error Msg:" + Server.UrlDecode(Request.QueryString["message"]);
                }
            }
            catch
            {
                this.Label1.Text = "Error Msg:";
            }
        }
    }
}