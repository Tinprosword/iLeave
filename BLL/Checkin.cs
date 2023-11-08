using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class Checkin
    {
        public static string GetLocationUrl_bingo(LSLibrary.WebAPP.LanguageType _cul, double lat, double lon)
        {
            string result = "";
            result = "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lon + "?o=json&key=AqviYV7wGGW6_Bx2Y1RIb_-w4eqXlS_GsgYTVuA_KYVMmUpnhfq3CvtpOjM9R6JQ&output=json";
            string culcode = "en-US";
            if (_cul == LSLibrary.WebAPP.LanguageType.sc)
            {
                culcode = "zh-Hans";
            }
            else if (_cul == LSLibrary.WebAPP.LanguageType.tc)
            {
                culcode = "zh-Hant";
            }
            result += "&c=" + culcode;
            return result;
        }


        public static string GetLocationUrl_Google(double lat, double lon, string key, LSLibrary.WebAPP.LanguageType _cul)
        {
            string result = "https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}&language={3}";

            string culcode = "en";
            if (_cul == LSLibrary.WebAPP.LanguageType.sc)
            {
                culcode = "zh-CN";
            }
            else if (_cul == LSLibrary.WebAPP.LanguageType.tc)
            {
                culcode = "zh-HK";
            }


            result = string.Format(result, lat, lon, key,culcode);

            return result;
        }

        public static string GetAddFromUrl_Bingo(string url)
        {
            string result = "";

            try
            {
                System.Net.WebClient webClient = new LSLibrary.WebClientPro(1000);
                webClient.Encoding = System.Text.Encoding.UTF8;
                string jsonstr = webClient.DownloadString(url);

               

                System.Xml.XmlDocument xmlDoc = LSLibrary.MyJson.UnSxml(jsonstr);

                var addlist = xmlDoc.GetElementsByTagName("addressLine");
                var locallist = xmlDoc.GetElementsByTagName("locality");
                if (locallist != null && locallist.Count > 0)
                {
                    result = locallist[0].InnerText;
                }

                if (addlist != null && addlist.Count > 0)
                {
                    result = addlist[0].InnerText + " " + result;
                }
            }
            catch
            {
                result = "";
            }

            return result;
        }


        public static string GetAddFromUrl_Google(string url)
        {
            string result = "";

            try
            {
                System.Net.WebClient webClient = new LSLibrary.WebClientPro(2000);
                webClient.Encoding = System.Text.Encoding.UTF8;
                string jsonstr = webClient.DownloadString(url);

                result = LSLibrary.MyJson.GetAddress_GoogleMap(jsonstr);
            }
            catch
            {
                result = "";
            }

            return result;
        }

        public static string CheckCheckin(int uid, int eid, string ssid, decimal lat, decimal lng, DateTime checkedtime, string lang)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.ExecFn_checkmsg(uid, eid, ssid, lat, lng, checkedtime, lang.ToString());
        }

    }
}
