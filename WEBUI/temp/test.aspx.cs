using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class test : System.Web.UI.Page
    {

        //获得重叠时间段
        public static void GetOverlapRange(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2, out DateTime? overlapStart, out DateTime? overlapEnd)
        {
            overlapStart = null; overlapEnd = null;
            if (startDate1 < startDate2 && (endDate1 >= startDate2 && endDate1 <= endDate2))
            {
                overlapStart = startDate2;
                overlapEnd = endDate1;
            }
            else if (startDate1 < startDate2 && (endDate1 > endDate2))
            {
                overlapStart = startDate2;
                overlapEnd = endDate2;
            }
            else if ((startDate1 >= startDate2 && startDate1 <= endDate2) && endDate1 <= endDate2)
            {
                overlapStart = startDate1;
                overlapEnd = endDate1;
            }
            else if ((startDate1 >= startDate2 && startDate1 <= endDate2) && endDate1 > endDate2)
            {
                overlapStart = startDate1;
                overlapEnd = endDate2;
            }
        }

        public static double GetRealTotal(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
        {
            double result = 0;

            DateTime? f1;
            DateTime? t1;

            DateTime? f2;
            DateTime? t2;

            DeleteOverlapRangeFromDate1(startDate1, endDate1, startDate2, endDate2, out f1, out t1,out f2,out t2);

            List<DateRange> tempResult = new List<DateRange>();
            if (f1 != null && t1 != null)
            {
                tempResult.Add(new DateRange(f1.Value, t1.Value));
            }

            if (f2 != null && t2 != null)
            {
                tempResult.Add(new DateRange(f2.Value, t2.Value));
            }

            result = DateRange.SumHoursFromRange(tempResult);



            return result;
        }


        //从date1中 ，去除和date2重叠时间段
        public static void DeleteOverlapRangeFromDate1(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2, out DateTime? newStart1, out DateTime? newEnd1, out DateTime? newStart2, out DateTime? newEnd2)
        {
            newStart1 = startDate1;
            newEnd1 = endDate1;

            newStart2 = startDate1;
            newEnd2 = endDate1;

            DateTime? overlapStart;
            DateTime? overlapEnd;
            GetOverlapRange(startDate1, endDate1, startDate2, endDate2, out overlapStart, out overlapEnd);

            if (overlapStart != null && overlapEnd != null)
            {
                newStart1 = startDate1;
                newEnd1 = overlapStart;

                newStart2 = overlapEnd;
                newEnd2 = endDate1;

                if (newStart1 == newEnd1)
                {
                    newStart1 = newEnd1 = null;
                }

                if (newStart2 == newEnd2)
                {
                    newStart2 = newEnd2 = null;
                }
            }
            else
            {
                newStart1 = startDate1;
                newEnd1 = endDate1;

                newStart2 = null;
                newEnd2 = null;
            }

        }

        public class DateRange
        {
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public DateRange(DateTime f, DateTime t)
            {
                from = f;
                to = t;
            }

            public static double SumHoursFromRange(List<DateRange> ranges)
            {
                double result = 0;
                foreach (DateRange item in ranges)
                {
                    double tempItem = 0;
                    TimeSpan ts = item.to - item.from;
                    int mins = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
                    tempItem = (double)mins / 60.0;
                    result += tempItem;
                }
                return result;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //leavebase leavebase = new leavebase();

            //this.Label1.Text = leavebase.add(1, 3).ToString();
            //getbaseinfo();
            //showsite();

            DateTime f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            DateTime t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            DateTime f2 = new DateTime(2021, 1, 1, 3, 0, 0);
            DateTime t2 = new DateTime(2021, 1, 1, 4, 0, 0);
            testDateRange(f1,t1,f2,t2);
            this.Label1.Text+= GetRealTotal(f1, t1, f2, t2)+"       ---";


            f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            f2 = new DateTime(2021, 1, 1, 0, 0, 0);
            t2 = new DateTime(2021, 1, 1, 1, 35, 0);
            testDateRange(f1, t1, f2, t2);
            this.Label1.Text += GetRealTotal(f1, t1, f2, t2) + "       ---";

            f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            f2 = new DateTime(2021, 1, 1, 1, 30, 0);
            t2 = new DateTime(2021, 1, 1, 2, 30, 0);
            testDateRange(f1, t1, f2, t2);
            this.Label1.Text += GetRealTotal(f1, t1, f2, t2) + "       ---";


            f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            f2 = new DateTime(2021, 1, 1, 1, 18, 0);
            t2 = new DateTime(2021, 1, 1, 1, 42, 0);
            testDateRange(f1, t1, f2, t2);
            this.Label1.Text += GetRealTotal(f1, t1, f2, t2) + "       ---";

            f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            f2 = new DateTime(2021, 1, 1, 0, 15, 0);
            t2 = new DateTime(2021, 1, 1, 2, 45, 0);
            testDateRange(f1, t1, f2, t2);
            this.Label1.Text += GetRealTotal(f1, t1, f2, t2) + "       ---";

            f1 = new DateTime(2021, 1, 1, 1, 0, 0);
            t1 = new DateTime(2021, 1, 1, 2, 0, 0);

            f2 = new DateTime(2021, 1, 1, 1, 0, 0);
            t2 = new DateTime(2021, 1, 1, 2, 0, 0);
            testDateRange(f1, t1, f2, t2);
            this.Label1.Text += GetRealTotal(f1, t1, f2, t2) + "       ---";
        }

        private  void testDateRange(DateTime f1,DateTime t1,DateTime f2,DateTime t2)
        {//1no overlap .1left 2.right 3.center 4full over 5,just full
           

            DateTime? rf1; DateTime? rt1; DateTime? rf2; DateTime? rt2;

            DeleteOverlapRangeFromDate1(f1, t1, f2, t2, out rf1, out rt1, out rf2, out rt2);

            this.Label1.Text += rf1.GetValueOrDefault().ToShortTimeString() + "--" + rt1.GetValueOrDefault().ToShortTimeString();
            this.Label1.Text+= rf2.GetValueOrDefault().ToShortTimeString() + "--" + rt2.GetValueOrDefault().ToShortTimeString()+"\r\n"+"|";
        }





        private void showsite()
        {
            this.Label1.Text = BLL.Other.GetHRWebSiteRootUrl();
        }

        private void getbaseinfo()
        {
            string url = "http://localhost:80/WEBUI/webservices/leave.asmx/GetCLOTDetail_html";

            int index= url.IndexOf("webservices");
            if (index > 0)
            {
                url = url.Substring(0, index);
            }

            if (url == "http://localhost:80/WEBUI/")
            {
                this.Label1.Text = "ok" + url;
            }
            else
            {
                this.Label1.Text = "error" + url;
            }
        }


    }



    public class leavebase
    {
        public int add(int a, int b)
        {
            leaveAdv leaveAdv = new leaveAdv();

            return leaveAdv.addAdv(a, b);
        }


    }

    public class leaveAdv
    {
        public leavebase lb = new leavebase();

        public int addAdv(int a, int b)
        {
            int res = lb.add(a, b);
            return res * 2;
        }

        public int addSingle(int a, int b)
        {
            return (a + b) *3;
        }
    }
}