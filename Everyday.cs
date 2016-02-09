﻿using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Everyday
{
    public class Everyday 
    {
        public LoginData loginData;
        public ErrorStatus errStatus;
        public GetEvents getEvents;
        public GetUserInfo getUserInfo;
                
        public string OSVersion = Environment.OSVersion.ToString();
        public string SERVER = "http://api.go.pl.ua/";
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
            qry = SERVER + "GetEvents.php?Token=" + loginData.token
                         + "&Devid=" + ComputerID
                         + "&Platform=" + OSVersion.ToString()
                         + "&Query={" + quote + "aday" + quote + ":" + quote + "2014-08-20" + quote + "}";
            if (MakeQueryToServer(qry)==1)
            {
                getEvents = JsonConvert.DeserializeObject<GetEvents>(response);
            }
            else return 0;
            qry = SERVER + "GetUserInfo.php?Token=" + loginData.token 
                         + "&Devid=" + ComputerID 
                         + "&Platform=" 
                         + OSVersion.ToString() + "&Query={}";
            if (MakeQueryToServer(qry) == 1)
            {
                getUserInfo = JsonConvert.DeserializeObject<GetUserInfo>(response);
            }
            else return 0;

            UserImg =(Bitmap) GetResponse(getUserInfo.UserImg, true);
            //LoadTree(response, TreeView1);
            //treeView1.Focus();
            SUCCESS=1;
            return 1;
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

    private object GetResponse(string QueryPHP, bool bBitmap=false) {
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
            MessageBox.Show(ex.Message.ToString());
            return "";
        }
        
    }
    
    //public void LoadTree(string sResponse, TreeView TrV) {
    //    TrV.BeginUpdate();
    //    TrV.Nodes.Clear();
    //    string[] arr = sResponse.Split(new Char[] { ","c });
    //    int level = 0;
    //    level = 0;

    //    for (int i = 0; (i <= UBound(arr)); i++)
    //    {
    //        TreeNode Node = new TreeNode();
    //        string s;
    //        if ((((arr[i].IndexOf("{") + 1) 
    //                    > 0) 
    //                    && ((arr[i].IndexOf("[") + 1) 
    //                    == 0))) {
    //            if ((TrV.SelectedNode == null)) {
    //                Node.Text = arr[i].Replace("{"c, ""c).Replace('\"', "");
    //                TrV.Nodes.Add(Node);
    //                TrV.SelectedNode = Node;
    //            }
    //            else {
    //                s = arr[i].Substring((arr[i].IndexOf("{") + 1)).Replace('\"', "");
    //                Node.Text = s;
    //                TrV.SelectedNode.Nodes.Add(Node);
    //                TrV.SelectedNode = Node;
    //            }
                
    //            level = (level + 1);
    //        }
    //        else if ((((arr[i].IndexOf("{") + 1) 
    //                    > 0) 
    //                    && ((arr[i].IndexOf("[") + 1) 
    //                    > 0))) {
    //            TreeNode NodeChild = new TreeNode();
    //            Node.Text = arr[i].Substring(0, ((arr[i].IndexOf("[") + 1) 
    //                            - 2)).Replace('\"', "");
    //            TrV.SelectedNode.Nodes.Add(Node);
    //            TrV.SelectedNode = Node;
    //            s = arr[i].Substring((arr[i].IndexOf("{") + 1)).Replace('\"', "");
    //            NodeChild.Text = s;
    //            TrV.SelectedNode.Nodes.Add(NodeChild);
    //            TrV.SelectedNode = NodeChild;
    //        }
    //        else if ((((arr[i].IndexOf("}") + 1) 
    //                    > 0) 
    //                    && ((arr[i].IndexOf("]") + 1) 
    //                    > 0))) {
    //            s = arr[i].Substring(0, ((arr[i].IndexOf("}") + 1) 
    //                            - 1)).Trim("\"");
    //            Node.Text = s;
    //            TrV.SelectedNode.Nodes.Add(Node);
    //            if (((TrV.SelectedNode.Parent == null) 
    //                        == false)) {
    //                TrV.SelectedNode = TrV.SelectedNode.Parent;
    //                TrV.SelectedNode = TrV.SelectedNode.Parent;
    //            }
                
    //        }
    //        else if (((arr[i].IndexOf("}") + 1) 
    //                    > 0)) {
    //            s = arr[i].Substring(0, ((arr[i].IndexOf("}") + 1) 
    //                            - 1)).Trim("\"");
    //            Node.Text = s;
    //            TrV.SelectedNode.Nodes.Add(Node);
    //            if (((TrV.SelectedNode.Parent == null) 
    //                        == false)) {
    //                TrV.SelectedNode = TrV.SelectedNode.Parent;
    //            }
                
    //            level = (level - 1);
    //        }
    //        else if ((level != 0)) {
    //            if ((TrV.SelectedNode == null)) {
    //                Node.Text = arr[i].Replace('\"', "");
    //                TrV.Nodes.Add(arr[i].Replace('\"', ""));
    //                TrV.SelectedNode = Node;
    //            }
    //            else {
    //                TrV.SelectedNode.Nodes.Add(arr[i].Replace('\"', ""));
    //            }
                
    //        }
    //        else {
    //            TrV.Nodes.Add(arr[i].Replace('\"', ""));
    //        }
            
    //    }
        
    //    TrV.CollapseAll();
    //    TrV.Nodes[0].Expand();
    //    TrV.EndUpdate();
    //}
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
    class Person
    {
        internal string name;
        internal int age;
    }

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
    }
}
