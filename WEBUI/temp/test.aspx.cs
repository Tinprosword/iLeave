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
        }

        private void addgroup()
        {
        }

   

        protected void test1_Click(object sender, EventArgs e)
        {
            addgroup();
        }

        private void iosPush()
        {
            BLL.Announcement.pushIOSNotice("hi test", "05eb62ba07a11d74f322066ed37c3a21926a47e8b3fd2d6b9deb2090cde723e0", Server);
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