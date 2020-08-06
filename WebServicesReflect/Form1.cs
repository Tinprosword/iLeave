using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebServicesReflect
{
    public partial class Form1 : Form
    {
        public static string allCode = @"
        public {0}.t_User[] Base_GetListt_User(string sqlWhere)
        {
            DalHelper.WebServicesHelper webServices = DalHelper.WebServicesHelper.GetInstance();
            return webServices.ws_user.Base_GetListt_User(sqlWhere);
        }";


        public Form1()
        {
            InitializeComponent();
        }


        //public WebReference_User.t_User[] Base_GetListt_User(string sqlWhere)
        //{
        //    DalHelper.WebServicesHelper webServices = DalHelper.WebServicesHelper.GetInstance();
        //    return webServices.ws_user.Base_GetListt_User(sqlWhere);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            System.Reflection.Assembly tempAss= LSLibrary.MyReflect.CreateAssembly("BLL");

            string classname = "DAL.WebReference_User.UserManagementV2";
            string classname2 = "BLL.ServicesWithSession.UserManagementV2";

            Type temptype;
            object obj = LSLibrary.MyReflect.CreateInstance1(tempAss, classname2, out temptype);//一般2种方法得到一个类的实例和类型信息.  1通过指定命名空间加类名 xxx.xxx

            System.Reflection.MethodInfo[] allm= temptype.GetMethods();

            foreach (System.Reflection.MethodInfo method in allm)
            {
                string code= GenerateCode(method);
                this.textBox1.Text += code;
                this.textBox1.Text += System.Environment.NewLine;
            }
        }


        public string GenerateCode(System.Reflection.MethodInfo method)
        {
            string result = "";

            string wsname = "WebReference_User";

           

            allCode.Replace("{0}", wsname);


            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
