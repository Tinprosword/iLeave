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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.Label1.Text= TestMultiply.names[TestMultiply.GetLanguageType()];
            book c = new book();
            c.id = 1;
            c.name = "c";

            LSLibrary.MyJson myJson = new LSLibrary.MyJson();
            string cc= myJson.JsonObject(c);
            this.Label1.Text = cc;
        }

        public class book
        {
            public int id = 4;
            public string name = "c";
        }

        private class TestMultiply
        {
            public static string[] names = new string[] { "v1", "v2" };

            public static int GetLanguageType()
            {
                return 1; // assume get value from cooike.
            }
        }


        public void addNumber(int number)
        {
            var watermarkedStream = new MemoryStream();
            using (var img = System.Drawing.Image.FromStream(File.OpenRead(@"D:\_\WatermarkDemo.png")))
            {
                using (var graphic = System.Drawing.Graphics.FromImage(img))
                {
                    var font = new Font("微软雅黑", 30, FontStyle.Bold, GraphicsUnit.Pixel);
                    var color = Color.FromArgb(128, 255, 255, 255);
                    var brush = new SolidBrush(color);
                    var point = new Point(img.Width - 130, img.Height - 50);

                    graphic.DrawString("1", font, brush, point);
                    img.Save(watermarkedStream, ImageFormat.Png);
                }
            }
        }

    }
}