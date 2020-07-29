using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class common
    {
        public static bool IsImagge(string filename)
        {
            string type = filename.Remove(0, filename.IndexOf('.') + 1);
            if (type == "jpg" || type == "png" | type == "gif" | type == "bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}