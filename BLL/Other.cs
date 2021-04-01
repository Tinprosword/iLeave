using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Other
    {
        public static WebServiceLayer.WebReference_leave.AttendanceRawData[] GetAttendanceList(string[] refInfo)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttendanceByExternRef(refInfo);
        }


        public static bool InsertAttendanceRawData(WebServiceLayer.WebReference_leave.AttendanceRawData[] data)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.InsertAttendance(data);
        }

        public static WebServiceLayer.WebReference_leave.AttendanceRawData GenerateModel(DateTime logdate,int uid,string _Type,string _ExternalRef,int _AttendanceInterfaceCenterID,
            int _InterfaceID,int? _RemoteIdent,string _StaffName,string _DeviceID,string _Zone,string _GpsLocation,string _GpsLocationName)
        {
            WebServiceLayer.WebReference_leave.AttendanceRawData result = new WebServiceLayer.WebReference_leave.AttendanceRawData();
            result.LogDateTime = logdate;
            result.Type = _Type;
            result.ExternalRef = _ExternalRef;
            result.InterfaceID = _InterfaceID;
            result.AttendanceInterfaceCenterID = _AttendanceInterfaceCenterID;
            result.RemoteIdent = _RemoteIdent;
            result.StaffName = _StaffName;
            result.DeviceID = _DeviceID;
            result.Zone = _Zone;
            result.CreateDate = DateTime.Now;
            result.CreateUser = uid;
            result.GpsLocation = _GpsLocation;
            result.GpsLocationName = _GpsLocationName;


            return result;
        }
    }
}