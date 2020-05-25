using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DataDemo
    {
        public class UserDemo
        {
            public string uername;
            public string password;
            public int level;

            public UserDemo(string uername, string password, int level)
            {
                this.uername = uername;
                this.password = password;
                this.level = level;
            }
        }

        public static List<UserDemo> getUser()
        {
            List<UserDemo> users = new List<UserDemo>();

            users.Add(new UserDemo("admin", "admin", 1));
            users.Add(new UserDemo("lili", "lili", 2));
            return users;
        }


        public static bool checklogin(string uid, string password)
        {
            bool res = false;
            List<UserDemo> users = getUser();
            foreach (UserDemo user in users)
            {
                if (user.uername == uid && user.password == password)
                {
                    res= true;
                }
            }

            return res;
        }
    }
}
