using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppLibraly
{
    //注意ResetUIOnEachLoad,只做一些清除工作, 会有很多工作其实是放入到 事件结尾处,由某个事件所带来的ui更新,并非每次都要
    public class PageTemplate: CheckLogin
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            InitPageDataOnEachLoad();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ResetUIOnEachLoad();
            if (!IsPostBack)
            {
                InitUIOnFirstLoad();
            }
        }

        protected virtual void InitPageDataOnEachLoad()
        {
        }

        protected virtual void InitUIOnFirstLoad()
        {
        }

        protected virtual void ResetUIOnEachLoad()
        {
        }

    }
}