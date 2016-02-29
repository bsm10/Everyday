using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Everyday
{
    public struct ErrorStatus
    {
        /*Результаты обработки запросов
        Каждый ответ сервера начинается с параметра success, который равняется  «1», если запрос обработан успешно или «0», если возникли какие-либо ошибки. 
        Остальные параметры ответов и их структура  различаются в зависимости от запроса.
        В случае возникновения ошибки (success=0) скрипты возвращают ее код (параметр error_code), а также 2 варианта текстового представления. Один вариант расшифровывает ошибку для разработчика (параметр error_description). 2-й вариант – error_for_user (общая фраза + код ошибки) служит для вывода (при необходимости) пользователю устройства. По своей сути error_for_user – это фраза обобщающая группу однородных ошибок.*/
        public int success;
        public string error_code;
        public string error_description;
        public string error_for_user;
        public string working_time;
    }
    public struct LoginData
    {
        public int success;
        public string token;
        public string client_id;
        public int new_notifications_count;
        public int not_confirmed_events_count;
        public float working_time;
    }
    public struct Result
    {
        public int success { get; set; }
    }
    public struct Items
    {
        public string id;
        public string name;
    }
    public struct Events
    {
        public string event_id;
        public string img;
        public string time;
        public string expert;
        public string caption;
        public int confirmed;
        public int items_count;
        public Items[] items;
        public int a_day_events_count;
        public int not_confirmed_events_count;
        public float working_time;
    }
    public struct GetEvents
    {
        public int success;
        public string a_day_string;
        public string a_day_date;
        public Events[] events;
    }
    public struct AppSettings
    {
        public bool confirm_events; // true,
        public bool enable_report_eating; // true,
        public bool enable_report_preparats; // true,
        public int cache_period; // 7
    }
    public struct GetUserInfo
    {
        public int success;
        public string UserId;
        public string UserLogin; // "elchukov",
        public string UserImg; //"avatars/1.png",
        public string UserF; //"Ельчуков",
        public string UserI; // "Сергей",
        public string UserO; // "Викторович",
        public string UserDateReg; //"2014-06-23 14:37:46",
        public AppSettings Settings;
        public int not_confirmed_events_count; // 12,          
        public int new_notifications_count; // 5,
        public float working_time; // 0.002
    }

    public class Everyday 
    {
        public LoginData loginData;
        public ErrorStatus errStatus;
        public GetEvents getEvents;
        public GetUserInfo getUserInfo;
                
        public string OSVersion = Environment.OSVersion.ToString();
        public string SERVER = "http://api.go.pl.ua/";
        public string SERVER_IMG = "http://api.go.pl.ua/img/640/";
        public string ComputerID = "Test for Win8 bsm10";
        public string response;
        public Bitmap UserImg;

        private const string quote = "\"";
        public int SUCCESS;

        public Everyday(string sLog, string sPass)
        {
            if (Login(sLog, sPass)==0)
            {
            }
        }
        private int Login(string sLog, string sPass)
        {
            string qry;
            qry = (SERVER
                        + (("Login.php?&Devid="
                        + (ComputerID + ("&Platform="
                        + (OSVersion + "&Query={\"login\":\""))))
                        + (sLog + ("\",\"pass\":\""
                        + (sPass + "\"}")))));
            if (MakeQueryToServer(qry) == 1)
            {
                loginData = JsonConvert.DeserializeObject<LoginData>(response);
            }
            else return 0;
            //qry = SERVER + "GetUserInfo.php?Token=" + loginData.token 
            //             + "&Devid=" + ComputerID 
            //             + "&Platform=" 
            //             + OSVersion.ToString() + "&Query={}";
            //""
            qry = String.Format("{0}GetUserInfo.php?Token={1}&Devid={2}&Platform={3}&Query=", SERVER,loginData.token,ComputerID,OSVersion)+"{}";
            if (MakeQueryToServer(qry) == 1)
            {
                getUserInfo = JsonConvert.DeserializeObject<GetUserInfo>(response);
            }
            else return 0;

            //UserImg =(Bitmap) GetResponse(SERVER + getUserInfo.UserImg, true);

            SUCCESS=1;
            return 1;
        }
        public GetEvents GetEventsByData(string date) //date format "2014-08-20"
    {
        string qry = SERVER + "GetEvents.php?Token=" + loginData.token
                     + "&Devid=" + ComputerID
                     + "&Platform=" + OSVersion.ToString()
                     + "&Query={" + quote + "aday" + quote + ":" + quote + date + quote + "}";
        if (MakeQueryToServer(qry) == 1)
        {
            getEvents = JsonConvert.DeserializeObject<GetEvents>(response);
        }
        return getEvents;
    }

    private int MakeQueryToServer(string qry)
        {
        Result res;
            response = (string)GetResponse(qry);
            res = JsonConvert.DeserializeObject<Result>(response);
            if (res.success == 0)
            {
                errStatus = JsonConvert.DeserializeObject<ErrorStatus>(response);
                MessageBox.Show(errStatus.error_for_user);
            }
            return res.success;
        }

    public object GetResponse(string QueryPHP, bool bBitmap=false) {
        HttpWebRequest request;
        HttpWebResponse response;
        request = (HttpWebRequest)WebRequest.Create(QueryPHP);
        if ((bBitmap == false)) {
            request.Method = "POST";
        }
        else {
            request.Method = "GET";
        }
        request.Proxy = new WebProxy("10.0.0.112", 8080);
        request.Proxy.Credentials = new NetworkCredential("bmaliy", "123");
        try {
            response = (HttpWebResponse)request.GetResponse();
            if ((bBitmap == false)) {
                Encoding enc = Encoding.GetEncoding("windows-1251");
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                string responseContent = sr.ReadToEnd();
                sr.Close();
                response.Close();
                return responseContent.Trim();
            }
            else {
                Stream responseStream = response.GetResponseStream();
                Bitmap bitmap2 = new Bitmap(responseStream);
                return bitmap2;
            }
        }
        catch (Exception ex) {
            request = null;
            //MessageBox.Show(ex.Message.ToString());
            return "";
        }
        
    }
    private string GetDataFromString(string sParametr, string StringResponse)
    {
        int i;
        int j;
        int k;
        i = (StringResponse.IndexOf(sParametr) + 1);
        if ((i > 0)) {
            j = (StringResponse.IndexOf(":", (i - 1), System.StringComparison.Ordinal) + 1);
            k = (StringResponse.IndexOf(",", (j - 1), System.StringComparison.Ordinal) + 1);
            return StringResponse.Substring(j, (k 
                            - (j - 1))).Trim('\"');
        }
        return "NoData";
    }

//{
  //"success": 1,
  //"a_day_string": "26 \u042f\u043d\u0432\u0430\u0440\u044f 2016",
  //"a_day_date": 1453759200,
  //"events": [
  //  {
  //    "event_id": 55098,
  //    "img": "events\/1.png",
  //    "time": "07:00",
  //    "time_to_sort": 1456722000,
  //    "expert": "\u0414\u043c\u0438\u0442\u0440\u0438\u0439 \u041a\u0443\u043b\u0438\u043d\u0438\u0447\u0435\u0432",
  //    "caption": "\u041d\u0430\u0442\u043e\u0449\u0430\u043a",
  //    "event_name_id": 8,
  //    "unscheduled": false,
  //    "class": 1,
  //    "items_count": "1",
  //    "items": [
  //      {
  //        "0": "GetEvents",
  //        "1": "php",
  //        "id": "1291",
  //        "name": "\u0412\u043e\u0434\u0430 \u043e\u0447\u0438\u0449\u0435\u043d\u043d\u0430\u044f (300)"
  //      }
  //    ],
  //    "confirmed": 1
  //  },

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parametr0 { get; set; }
        public string parametr1 { get; set; }
    }

    public class Event
    {
        public int event_id { get; set; }
        public string img { get; set; }
        public string time { get; set; }
        public int time_to_sort { get; set; }
        public string expert { get; set; }
        public string caption { get; set; }
        public int event_name_id { get; set; }
        public bool unscheduled { get; set; }
        public int @class { get; set; }
        public string items_count { get; set; }
        public List<Item> items { get; set; }
        public int confirmed { get; set; }
    }

    public class Warning
    {
        public int code { get; set; }
        public string caption { get; set; }
        public string details { get; set; }
    }

    public class Result
    {
        public List<object> errors { get; set; }
        public List<Warning> warnings { get; set; }
        public List<object> notifies { get; set; }
    }

    public class Messages
    {
        public List<object> errors { get; set; }
        public List<string> warnings { get; set; }
        public List<object> notifies { get; set; }
    }

    public class Debug
    {
        public string client_id { get; set; }
        public double runtime { get; set; }
        public string script { get; set; }
        public int queries { get; set; }
        public Messages messages { get; set; }
        public int responsesize { get; set; }
    }

    public class RootObject
    {
        public int success { get; set; }
        public string a_day_string { get; set; }
        public int a_day_date { get; set; }
        public List<Event> events { get; set; }
        public int a_day_events_count { get; set; }
        public bool DevMode { get; set; }
        public Result result { get; set; }
        public Debug debug { get; set; }
    }
    }
}
