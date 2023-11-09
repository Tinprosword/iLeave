function startClock()
{
    setInterval("document.getElementById('ContentPlaceHolder1_lb_time').innerHTML=formatDate(new Date());", 10000);
}
startClock();


function ShowMsgAndLogout(msg)
{
    //alert("abc");
    confirm(msg);
    document.location.href = "../login.aspx?action=userloginout";
}


function formatDate(date)
{
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

//为什么mobile不直接调用MyPostBack,因为外部非串行触发的脚本，最好不要直接调用。有可能页面已经切换。那么会直接调用切换后页面的MyPostBack所以加个壳，检测来源。
//js 處理的數據，有2套，一個是前台顯示，一個是後台顯示。 為了避免前台的需求變化影響後台。所以分為2個。
//如 ContentPlaceHolder1_hf_nowifi 保存前台的WIFIName,   ContentPlaceHolder1_hf_back_haswifi 保存後台的wifiName.
//這裡的JS 會比較混亂，主要是前台數據，後台數據的區分。
//還有就是 lable 的名字，無法在IDE中智能感應，所以最好把ID名字 先列出來，不要隨用隨寫。 1.是為了知道影響了多少LABLE. 2.修改名字，值在一個地方。 3.內部變量更好可以修改，體現意思。
function Mobile_UpdateLocation(towhichpage,actionname, data)
{
    if (Mobile_CheckIsMyJsFunction(towhichpage)) {
        //get value from parameter and check the value.
        var dataisvalid = false;

        var resultCode = actionname.split("|");
        var dataArray = data.split("|");
        if (resultCode.length == 2 && dataArray.length == 3) {
            if (resultCode[0] == "00" && (dataArray[0] == "" || dataArray[1] == "")) {
            }
            else {
                if (resultCode[1] == "00" && dataArray[2] == "") {
                }
                else {
                    dataisvalid = true;
                }
            }
        }

        if (dataisvalid)
        {

            var hasLocation = true;
            var hasWifi = true;
            var wifiName = dataArray[2];
            var lata = dataArray[0];
            var lona = dataArray[1];

            if (resultCode[1] == "00") {
                hasWifi = true;
            }
            else {
                hasWifi = false;
            }
            if (resultCode[0] == "00") {
                hasLocation = true;
            }
            else {
                hasLocation = false;
            }

            //前端顯示
            var label_id_front_wifiname = "#ContentPlaceHolder1_lb_wifi";
            var label_id_front_gpslocationName = "#ContentPlaceHolder1_lb_locationname";

            //前端顯示,多語言錯誤的提示。
            var label_id_msg_nowifi = "#ContentPlaceHolder1_hf_nowifi";
            var label_id_msg_nogps = "#ContentPlaceHolder1_hf_nogps";

            //給後端的數據。
            var label_id_back_invaliddata = "#ContentPlaceHolder1_hf_back_invaliddata";
            var label_id_back_wifiname = "#ContentPlaceHolder1_hf_back_wifiname";
            var label_id_back_gpslatlon = "#ContentPlaceHolder1_hf_back_gpslatlon";
            var label_id_back_decodeGps = "#ContentPlaceHolder1_hf_back_gpsDecode";


            var label_nowifi = $(label_id_msg_nowifi).val();
            var label_nogps = $(label_id_msg_nogps).val() + ". Code:" + resultCode[0];


            if (hasWifi) {
                $(label_id_front_wifiname).html(wifiName);
            }
            else {
                $(label_id_front_wifiname).html(label_nowifi);
            }

            if (hasLocation) {
                //如果gps lat ,lon 沒有變化，那麼不解析。
                var oldgps = $(label_id_back_gpslatlon).val();
                var oldlat = 0;//默認值設置為0.而且不能隨意更改。因為NeedToGetGPSAddress，會使用這個值，來判斷是否是第一次，第一次必須解析地址。
                var oldlon = 0;//默認值設置為0.而且不能隨意更改。因為NeedToGetGPSAddress，會使用這個值，來判斷是否是第一次，第一次必須解析地址。
                if (oldgps != "") {
                    var oldgpsarray = oldgps.split("|");
                    if (oldgpsarray.length == 2) {
                        oldlat = oldgpsarray[0];
                        oldlon = oldgpsarray[1];
                    }
                }

                var needdecodegps = NeedToGetGPSAddress(lata, lona, oldlat, oldlon);
                if (needdecodegps) {
                    SingleResult('../webservices/leave.asmx/GetMapLocationName', { lat: lata, lon: lona }, 'string', Mobile_UpdateLocation_onGetLocationName);
                }
            }
            else {
                $(label_id_front_gpslocationName).html(label_nogps);
            }

            //set backvalue for cs file
            $(label_id_back_invaliddata).val("ok");
            if (hasWifi)
            {
                $(label_id_back_wifiname).val(wifiName);
            }
            else
            {
                $(label_id_back_wifiname).val("");
            }
            if (hasLocation)
            {
                $(label_id_back_gpslatlon).val(lata + "|" + lona);
            }
            else
            {
                $(label_id_back_gpslatlon).val("");
            }
        }
        else
        {
            $(label_id_front_gpslocationName).html("Error:invalid data.");
            $(label_id_front_wifiname).html("Error:invalid data.");

            //set backvalue for cs file
            $(label_id_back_wifiname).val("");
            $(label_id_back_invaliddata).val("");
            $(label_id_back_gpslatlon).val("");
            $(label_id_back_decodeGps).val("");
        }
    }
}

//0.00001 距離相差大概1米。 3米之內就不重新計算了。
function NeedToGetGPSAddress(nowlat, nowlon, prelat, prelon) {
    //alert(nowlat + ".  " + nowlon + ".  " + prelat + ".  " +  prelon);
    var result = true;
    var latoffset = Math.abs(nowlat - prelat);
    var lonoffset = Math.abs(nowlon - prelon);

    if (prelat == 0 && prelon == 0) {
        result = true;
    }
    else {
        if (latoffset <= 0.00003 && lonoffset <= 0.00003) {
            result = false;
        }
    }
    
    //alert(result + "....." + nowlat + ".  " + nowlon + ".  " + prelat + ".  " + prelon);
    return result;
}

function Mobile_UpdateLocation_onGetLocationName(obj)
{
    //前端顯示
    var label_id_front_wifiname = "#ContentPlaceHolder1_lb_wifi";
    var label_id_front_gpslocationName = "#ContentPlaceHolder1_lb_locationname";

    //前端顯示,多語言錯誤的提示。
    var label_id_msg_nowifi = "#ContentPlaceHolder1_hf_nowifi";
    var label_id_msg_nogps = "#ContentPlaceHolder1_hf_nogps";
    var label_id_msg_decodeError = "#ContentPlaceHolder1_hf_cantconvertGps";

    //給後端的數據。
    var label_id_back_invaliddata = "#ContentPlaceHolder1_hf_back_invaliddata";
    var label_id_back_wifiname = "#ContentPlaceHolder1_hf_back_wifiname";
    var label_id_back_gpslatlon = "#ContentPlaceHolder1_hf_back_gpslatlon";
    var label_id_back_decodeGps = "#ContentPlaceHolder1_hf_back_gpsDecode";


    var label_hf_cantconvertGps = $(label_id_msg_decodeError).val();
    if (obj == "") {
        $(label_id_front_gpslocationName).html(label_hf_cantconvertGps);
    }
    else {
        $(label_id_front_gpslocationName).html(obj);
    }

    //set back value
    if (obj=="") {
        $(label_id_back_decodeGps).val("");
    }
    else {
        $(label_id_back_decodeGps).val(obj);
    }
}