using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace ut
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string agent = "abc";
            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType res = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            Debug.WriteLine("abc");
            Debug.WriteLine(res);
        }



        [TestMethod]
        public void Txml_0101()
        {
            string strxml1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            string strxml2 = "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">";
            string strxml3 = "<soap:Body>";
            string strxml4 = "<InsertStaffLeaveDetails xmlns=\"http://tempuri.org/\">";
            string strXml = @"<poStaffLeaveDetailsArray>
                                <StaffLeaveDetails>
                                <ID>int</ID>
                                <EmploymentID>int</EmploymentID>
                                <EmploymentNumber>string</EmploymentNumber>
                                <EngName>string</EngName>
                                <ChiName>string</ChiName>
                                <Type>string</Type>
                                <TypeID>int</TypeID>
                                <LeaveFrom>dateTime</LeaveFrom>
                                <LeaveTo>dateTime</LeaveTo>
                                <Unit>double</Unit>
                                <Remarks>string</Remarks>
                                <DeleteKey>string</DeleteKey>
                                <Section>int</Section>
                                <Code>string</Code>
                                <Date>dateTime</Date>
                                <WorkingHourPattern>string</WorkingHourPattern>
                                <WorkingHourHalfDay>double</WorkingHourHalfDay>
                                <WorkingHourAM>double</WorkingHourAM>
                                <WorkingHourPM>double</WorkingHourPM>
                                <CreateDate>dateTime</CreateDate>
                                <RequestID>int</RequestID>
                                <HolidayCode>string</HolidayCode>
                                <LeaveCalculationTypeID>int</LeaveCalculationTypeID>
                                <LeaveCalculationTypeDesc>string</LeaveCalculationTypeDesc>
                                <LeaveHours>double</LeaveHours>
                                <TotalWorkHours>double</TotalWorkHours>
                                <Sections>double</Sections>
                                <IsHalfDay>boolean</IsHalfDay>
                                <DisplaySection>int</DisplaySection>
                                <DisplayUnit>string</DisplayUnit>
                                <DisplaySectionCombined>string</DisplaySectionCombined>
                                <SecurityGroupCode>string</SecurityGroupCode>
                                </StaffLeaveDetails>
                                </poStaffLeaveDetailsArray>
                                <piUserID>int</piUserID>
                                <piWorkflowDelegationToStaffID>int</piWorkflowDelegationToStaffID>
                                </InsertStaffLeaveDetails>
                                </soap:Body>
                                </soap:Envelope>";
            string xmlFull = strxml1 + strxml2 + strxml3 + strxml4 + strXml;

            LSLibrary.XmlHelper xmlHelper = new LSLibrary.XmlHelper(xmlFull, true);
            DataSet ds= xmlHelper.GetDataTableByXmlString();


            //1.fill class object  2.convert to dataset 3.dataset to xml 4 post to webservices.
            MODEL.Apply.StaffLeaveDetails tempItem = new MODEL.Apply.StaffLeaveDetails();
            tempItem.chiNameField = null;
            tempItem.chiNameField = null;
            tempItem.createDateField = System.DateTime.Now;
            tempItem.dateField = System.DateTime.Now;
            tempItem.deleteKeyField = "???";
            tempItem.displaySectionCombinedField = null;
            tempItem.displaySectionField = 0;
            tempItem.displayUnitField = "1 D";//???
            tempItem.employmentIDField = 22855;
            tempItem.employmentNumberField = null;
            tempItem.engNameField = null;
            tempItem.holidayCodeField = "";
            tempItem.idField = 0;
            tempItem.isHalfDayField = false;
            tempItem.leaveCalculationTypeDescField = "N/A";
            tempItem.leaveCalculationTypeIDField = -1;
            tempItem.leaveFromField = DateTime.Parse("2020-7-2");
            tempItem.leaveHoursField = 0;
            tempItem.leaveToField = DateTime.Parse("2020-7-2");
            tempItem.remarksField = "aa";
            tempItem.requestIDField = 0;
            tempItem.sectionField = 0;
            tempItem.sectionsField = 0;
            tempItem.securityGroupCodeField = null;
            tempItem.totalWorkHoursField = 8;
            tempItem.typeField = "Annual Leave 年假";
            tempItem.typeIDField = 11;
            tempItem.unitField = 1;
            tempItem.workingHourAMField = 0;
            tempItem.workingHourHalfDayField = 0;
            tempItem.workingHourPatternField = null;
            tempItem.workingHourPMField = 0;






            string postData_xml = LSLibrary.XmlHelper.ConvertDataSetToXML(ds);

            int a = 4;
        }

        public DataSet FillDataset(DataSet ds, List<MODEL.Apply.StaffLeaveDetails> details, int piUserID, int? piWorkflowDelegationToStaffID)
        {
            ds.Tables["Body"].Rows[0]["Body_Id"] = 0;
            ds.Tables["InsertStaffLeaveDetails"].Rows[0][""] = 0;
            return ds;
        }

        [TestMethod]
        public void testbll()
        {
            var aa= BLL.User_wsref.GetPersonBaseinfoByEmploymentID("admin", 21845);
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