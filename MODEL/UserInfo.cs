﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class UserInfo
    {
        public int u_id;
        public string loginName;
        public string nickname;
        public string sessionid;
        public int? employID;
        public string employNnumber;
        public int? staffid;
        public string staffNumber;
        public int personid;
        public string surname;
        public string firstname;
        public string namech;
        public int ScreenHeight;
        public int ScreenWidth;
        public bool moreEmployment;
        public List<int> eidRefFirstEid;
        public List<string> eNoRefFirstEid;
        public int? firsteid;
        public int? companyid;
        public string companyDeployCode;

        public UserInfo(int id, string loginName, string nickname, string sessionid, int? employID, string employNnumber, int? staffid, string staffNumber, int personid,string surname,string firstname,string ch,int sh,int sw,bool more,List<int> eidsRefFirsteid, List<string> eNoRefFirsteid,int? _firstid,int? _cid,string _companyDeployCode)
        {
            this.u_id = id;
            this.loginName = loginName;
            this.nickname = nickname;
            this.sessionid = sessionid;
            this.employID = employID;
            this.employNnumber = employNnumber;
            this.staffid = staffid;
            this.staffNumber = staffNumber;
            this.personid = personid;
            this.surname = surname;
            this.firstname = firstname;
            this.namech = ch;
            this.ScreenHeight = sh;
            ScreenWidth = sw;
            moreEmployment = more;
            eidRefFirstEid = eidsRefFirsteid;
            eNoRefFirstEid = eNoRefFirsteid;
            firsteid = _firstid;
            companyid = _cid;
            companyDeployCode = _companyDeployCode;
        }

        

        public bool hasValidEmploynumber()
        {
            bool result = true;
            if (employID == null || employID == 0)
            {
                result = false;
            }
            return result;
        }
    }

    public class UserName
    {
        public string surname;
        public string firstname;
        public string nickname;
        public string namech;

        public UserName(string surname, string firstname, string nickname, string namech)
        {
            this.surname = surname;
            this.firstname = firstname;
            this.nickname = nickname;
            this.namech = namech;
        }


        public static bool IsNameLike(string engname, string cnname, string searchName)
        {
            bool result = false;

            if (engname.ToUpper().Contains(searchName.ToUpper()) || cnname.ToUpper().Contains(searchName.ToUpper()))
            {
                result = true;
            }
            return result;
        }

        //1. Surname + Othername                Leung Shun
        //2. Surname + Othername or Nick name    Leung Linson
        //3. Nick Mame + Surname Linson Leung
        //4. Chinese Name or(1,2,3,)
        //5. Chinese Name + Nick name
        //6. Chinese Name
        public string GetDisplayName(int type)
        {
            if (type == 1)
            {
                return surname + " " + firstname;
            }
            else if (type == 2)
            {
                if (!string.IsNullOrEmpty(firstname))
                {
                    return surname + " " + firstname;
                }
                else
                {
                    return surname + " " + nickname;
                }
            }
            else if (type == 3)
            {
                return nickname + " " + surname;
            }
            else if (type == 4)
            {
                if (!string.IsNullOrEmpty(namech))
                {
                    return namech;
                }
                else
                {
                    return surname + " " + firstname;
                }
            }
            else if (type == 5)
            {
                return namech + " " + nickname;
            }
            else if (type == 6)
            {
                if (!string.IsNullOrEmpty(namech))
                {
                    return namech;
                }
                else
                {
                    return surname + " " + firstname;
                }
            }
            else
            {
                return surname + " " + firstname + " " + namech;
            }
        }
    }
}