using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using LSLibrary;

namespace ut
{
    public class mytestClass
    {
        public int getint()
        {
            return 1;
        }
    }

    public static class MytestClassExtend
    {
        public static int getint2(this mytestClass mytest)
        {
            return 3;
        }
    }


    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ICSHelper iCSHelper = new ICSHelper("mycalendar", "my first metting");
            ICSHelper.DataItem item = new ICSHelper.DataItem(DateTime.Now, DateTime.Now, "my first calendar","test location", "test my desc",true);
            ICSHelper.DataItem item2 = new ICSHelper.DataItem(DateTime.Now.AddHours(4), DateTime.Now.AddHours(5), "my secode calendar", "test location", "test my desc", false);
            iCSHelper.InsertItem(item);
            iCSHelper.InsertItem(item2);
            iCSHelper.Save("c:\\abc\\");
        }


        [TestMethod]
        public void testjiami()
        {
            string abc = LSLibrary.DesStand.encrypt("http://192.168.19.210:8099", "abd58237", "abd58237");
            string docode = LSLibrary.DesStand.decrypt(abc, "abd58237", "abd58237");
            int a = 4;
        }


        [TestMethod]
        public void TestMethod222()
        {
            List<int> allints = new List<int>();
            allints.Add(1);
            allints.Add(2);
            allints.Add(3);

            int[] validate = { 2 };

         //   var a= allints.Where(x => validate.Contains(x));
            Func<int, bool> func = new Func<int, bool>(myContain);

           var bb=  allints.Where(func).ToArray();


            mytestClass a = new mytestClass();
            a.getint2();


            var c = "aa";
        }

       
        public bool myContain(int a)
        {
            if (a > 2)
            {
                return true;
            }
            else
            {

                return false;
            }
        }


        [TestMethod]
        public void TestMethod222333()
        {
            double a = 3.334324;
            string b = a.ToString("0.##");
            int c = 4;
        }






        [TestMethod]
        public void testbll4()
        {
            WebServiceLayer.WebReference_user.UserManagementV2 userManagement = new WebServiceLayer.WebReference_user.UserManagementV2();
        }



        [TestMethod]
        public void linqTrainning()
        {
            WebServiceLayer.WebReference_user.UserManagementV2 userManagement = new WebServiceLayer.WebReference_user.UserManagementV2();


        }


        [TestMethod]
        public void testtryParse()
        {
            string a = "abc";
            DateTime tt = System.DateTime.Now;
            DateTime.TryParse(a, out tt);
            int abc = 44;
        }


        [TestMethod]
        public void Txml_0102()
        {
            string xml1 = @"<?xml version='1.0' encoding='utf-8'?>
<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
  <soap:Body>
    <InsertStaffLeaveDetailsResponse xmlns='http://tempuri.org/'>
      <InsertStaffLeaveDetailsResult>
        <ProcessID>int</ProcessID>
        <ProcessIDList>
          <int>int</int>
          <int>int</int>
        </ProcessIDList>
        <AffectedRecordCount>int</AffectedRecordCount>
        <ErrorMessage>string</ErrorMessage>
        <CompletedRatio>string</CompletedRatio>
        <IsMessageList>boolean</IsMessageList>
        <ErrorMessageList>
          <ErrorMessageItem>
            <IsMessageList>boolean</IsMessageList>
            <Message>string</Message>
            <MessageParameter1>string</MessageParameter1>
            <MessageParameter2>string</MessageParameter2>
            <MessageParameter3>string</MessageParameter3>
            <Severity>ERROR or WARNING or INFORMATION or VALIDATION</Severity>
          </ErrorMessageItem>
          <ErrorMessageItem>
            <IsMessageList>boolean</IsMessageList>
            <Message>string</Message>
            <MessageParameter1>string</MessageParameter1>
            <MessageParameter2>string</MessageParameter2>
            <MessageParameter3>string</MessageParameter3>
            <Severity>ERROR or WARNING or INFORMATION or VALIDATION</Severity>
          </ErrorMessageItem>
        </ErrorMessageList>
      </InsertStaffLeaveDetailsResult>
    </InsertStaffLeaveDetailsResponse>
  </soap:Body>
</soap:Envelope>";

            LSLibrary.XmlHelper xmlHelper = new LSLibrary.XmlHelper(xml1, true);
            DataSet ds = xmlHelper.GetDataTableByXmlString();

            DataSet ds2 = LSLibrary.XmlHelper.ConvertXMLToDataSet(xml1);

            string abc = LSLibrary.XmlHelper.ConvertDataSetToXML(ds);

            int a = 4;
        }
    }
}