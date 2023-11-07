using System.Web;

namespace BLL
{
    public enum enum_ClientSource
    {
        pc=0,

        mobile_browser_android=1,
        mobile_browser_ios = 2,

        mobile_app_android=4,
        mobile_app_ios=5,
        mobile_app_other=6,


    }

    public class SystemEnviroment
    {
        public static enum_ClientSource GetClientSource(HttpRequest theRequest,BLL.Page.MyCookie theCookie)
        {
            //1.coockis 2.response   1.is app, is pc
            enum_ClientSource result = enum_ClientSource.pc;

            string agent = theRequest.UserAgent;
            LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

            if (theCookie!=null)
            {
                if (theCookie.isAppLogin == "1")
                {
                    if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android)//android
                    {
                        result = enum_ClientSource.mobile_app_android;
                    }
                    else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone)
                    {
                        result = enum_ClientSource.mobile_app_ios;
                    }
                    else
                    {
                        result = enum_ClientSource.mobile_app_other;
                    }
                }
                else
                {
                    if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android)//android
                    {
                        result = enum_ClientSource.mobile_browser_android;
                    }
                    else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone)
                    {
                        result = enum_ClientSource.mobile_browser_ios;
                    }
                    else
                    {
                        result = enum_ClientSource.pc;
                    }
                }
            }
            else
            {
                result = enum_ClientSource.pc;
            }

            return result;
        }

        public static bool isFromMobilApp(enum_ClientSource _s)
        {
            bool result = false;

            if (_s == enum_ClientSource.mobile_app_android || _s == enum_ClientSource.mobile_app_ios || _s == enum_ClientSource.mobile_app_other)
            {
                result = true;
            }

            return result;
        }

        public static bool isFromMobilApp(HttpRequest theRequest, BLL.Page.MyCookie theCookie)
        {
            var clientSource = GetClientSource(theRequest, theCookie);
            return isFromMobilApp(clientSource);
        }

    }
}
