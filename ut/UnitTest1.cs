using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ut
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string agent = "abc";
            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType res= LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            Debug.WriteLine("abc");
            Debug.WriteLine(res);
        }
    }
}