using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Deployment.Application;

namespace Everyday
{
    public class ErrorStatus
    {
        /*Результаты обработки запросов
        Каждый ответ сервера начинается с параметра success, который равняется  «1», если запрос обработан успешно или «0», если возникли какие-либо ошибки. 
        Остальные параметры ответов и их структура  различаются в зависимости от запроса.
        В случае возникновения ошибки (success=0) скрипты возвращают ее код (параметр error_code), а также 2 варианта текстового представления. Один вариант расшифровывает ошибку для разработчика (параметр error_description). 2-й вариант – error_for_user (общая фраза + код ошибки) служит для вывода (при необходимости) пользователю устройства. По своей сути error_for_user – это фраза обобщающая группу однородных ошибок.*/
        public int success {get; set;}
        public string error_code { get; set; }
        public string error_description { get; set; }
        public string error_for_user { get; set; }
        public string working_time { get; set; }
    }
    public class LoginData
    {
        public int success { get; set; }
        public string token { get; set; }
        public string client_id { get; set; }
        public int new_notifications_count { get; set; }
        public int not_confirmed_events_count { get; set; }
        public float working_time { get; set; }
    }
    public static class Everyday 
    {
        public static LoginData loginData {get; set;}
        public static ErrorStatus errStatus { get; set; }
        public static GetEvents getEvents { get; set; }
        //public GetUserInfo getUserInfo;

        public static string OSVersion = Environment.OSVersion.ToString();
        public static string SERVER = "http://api.go.pl.ua/";
        public static string SERVER_IMG = "http://api.go.pl.ua/ios/img/640/";
        public static string SERVER_IOS = "http://api.go.pl.ua/ios/";
        public static string ComputerID = "Test for Win8 bsm10";
        public static string response;
        public static Bitmap UserImg;
        private const string quote = "\"";
        //public static int SUCCESS;

        //public Everyday(string sLog, string sPass)
        //{
        //    //Properties.
        //    //protected int ID { get; set; }
        //    //protected string Title { get; set; }
        //    //protected string Description { get; set; }
        //    //protected TimeSpan jobLength { get; set; }
        //    Login(sLog, sPass);

        //    //if (Login(sLog, sPass)==0)
        //    //{
        //    //}
        //}


        //GetEvents IEveryday.getEvents()
        //{
        //    return getEvents;
        //}
        //LoginData IEveryday.getLoginData()
        //{
        //    return loginData;
        //}
        //ErrorStatus IEveryday.getErrorStatus()
        //{
        //    return errStatus;
        //}
        public static int Login(string sLog, string sPass)
        {
            Uri uri = new Uri(SERVER + "Login.php?");
            string postData =  String.Format("&Devid={0}&Platform={1}&Query={{\"login\":\"{2}\",\"pass\":\"{3}\"}}", ComputerID, OSVersion, sLog, sPass);
            if (MakeQueryToServer(uri, postData) == 0) return 0;
            
            loginData = JsonConvert.DeserializeObject<LoginData>(response);

            uri = new Uri(SERVER_IOS + "rGetEvents.php?");
            
            postData = String.Format("Token={0}&Devid={1}&Platform={2}&Query={{\"date_start\":\"{3}\",\"date_end\":\"{4}\"}}",
                                        loginData.token, "bsm11", "WinXP", DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"));
            if (MakeQueryToServer(uri, postData,true) == 0) return 0;
            
            getEvents = JsonConvert.DeserializeObject<GetEvents>(response);

            return 1;
        }
        //=================================================================================================
        public static GetEvents GetEventsByData(string date_start, string date_end) //date format "2014-08-20"
        {
            Uri uri = new Uri(SERVER + "ios/rGetEvents.php?");
            string postData = String.Format("Token={0}&Devid={1}&Platform={2}&Query={{\"date_start\":\"{3}\",\"date_end\":\"{4}\"}}",
            loginData.token, ComputerID, OSVersion, date_start, date_end);
            
            if (MakeQueryToServer(uri,postData) == 0) return null;
            GetEvents getEvents = JsonConvert.DeserializeObject<GetEvents>(response);
            return getEvents;
        }
        //=================================================================================================
        private static int MakeQueryToServer(Uri uri, string postData, bool saveDataToFile = false)
        {
            ErrorStatus res;
            response = SendPostRequest(uri, postData);
            res = JsonConvert.DeserializeObject<ErrorStatus>(response) as ErrorStatus;
            if (res.success == 0)
            {
                MessageBox.Show(res.error_for_user);
                return 0;
            }
            //save response to file - data.json (bsm10)
            if (saveDataToFile)SaveDataRequest(response);
            return 1;
        }
        private static string SendPostRequest(Uri uri, string postData)
    {
        //Пример параметров
        //Uri uri = new Uri("http://api.go.pl.ua/ios/GetEventDetails.php?");
        //postData = "Token=ab34a3ca168ea757b7d8b618a5f15be4&Devid=&Platform=Mozilla/5.0%20%28Windows%20NT%206.3;%20WOW64;%20rv:41.0%29%20Gecko/20100101%20Firefox/41.0&Query={%22event_id%22:55104}";
        HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
        //********************************************************************
        request.Proxy = new WebProxy("10.0.0.112", 8080);
        request.Proxy.Credentials = new NetworkCredential("bmaliy", "123");
        //********************************************************************
        request.Method = "POST";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentLength = byteArray.Length;
        request.ContentType = "application/x-www-form-urlencoded";
        try
        {
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            Encoding enc = Encoding.GetEncoding("windows-1251");
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
            string responseContent = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return responseContent;
        }
        catch (Exception ex)
        {
            request = null;
            MessageBox.Show(ex.Message.ToString());
            return "";
        }

    
}
//эта функция используется для получения картинок (пока)
        public static object GetResponse(string QueryPHP, bool bBitmap = false)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            request = (HttpWebRequest)WebRequest.Create(QueryPHP);
            if ((bBitmap == false))
            {
                request.Method = "POST";
            }
            else
            {
                request.Method = "GET";
            }
            request.Proxy = new WebProxy("10.0.0.112", 8080);
            request.Proxy.Credentials = new NetworkCredential("bmaliy", "123");
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if ((bBitmap == false))
                {
                    Encoding enc = Encoding.GetEncoding("windows-1251");
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                    string responseContent = sr.ReadToEnd();
                    sr.Close();
                    response.Close();
                    return responseContent.Trim();
                }
                else
                {
                    Stream responseStream = response.GetResponseStream();
                    Bitmap bitmap2 = new Bitmap(responseStream);
                    return bitmap2;
                }
            }
            catch (Exception ex)
            {
                request = null;
                if(bBitmap==false)MessageBox.Show(ex.Message.ToString());
                return "";
            }

        }
        private static string GetDataFromString(string sParametr, string StringResponse)
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
        public static void SaveDataRequest(string content)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "events.json");
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(startupPath, false))
                    {
                        file.Write(content);
                    }
            }
             catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
