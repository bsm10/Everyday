using System.Collections.Generic;
namespace Everyday
{
    public class Img
    {
        public object path { get; set; }
        public object md5 { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string event_id { get; set; }
        public string item_type_id { get; set; }
        public string reference_item_id { get; set; }
        public string dish_count { get; set; }
        public object measure { get; set; }
        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbs { get; set; }
        public string cellulose { get; set; }
        public string kkal { get; set; }
        public object repeats { get; set; }
        public object sets { get; set; }
        public object rest { get; set; }
        public object exercises_count { get; set; }
        public string caption { get; set; }
        public override string ToString()
        {
            return caption;
        }
    }

    public class Performer
    {
        public string id { get; set; }
        public string expert_name { get; set; }
        public string expert_login { get; set; }
        public string avatar { get; set; }
        public string expert_sex { get; set; }
        public string task_id { get; set; }
        public string expert_id { get; set; }
        public string mail { get; set; }
    }

    public class Details
    {
//белки
//жиры
//углеводы
//клетчатка
//килокаллории

        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbs { get; set; }
        public string cellulose { get; set; }
        public string kkal { get; set; }
        public List<Item> items { get; set; }
        public string descr { get; set; }
        public object assoc_client { get; set; }
        public List<Performer> performers { get; set; }
    }

    public class Event
    {
        public int event_class { get; set; }
        public int id { get; set; }
        public Img img { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string event_name { get; set; }
        public string expert_name { get; set; }
        public int confirmed { get; set; }
        public int formalized { get; set; }
        public string comment { get; set; }
        public Details details { get; set; }
        public string data_md5 { get; set; }
    }

    public class Result
    {
        public List<object> errors { get; set; }
        public List<object> warnings { get; set; }
        public List<object> notifies { get; set; }
    }

    public class Messages
    {
        public List<object> errors { get; set; }
        public List<object> warnings { get; set; }
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

    public class GetEvents
    {
        public int success { get; set; }
        public string last_events_update { get; set; }
        public List<Event> events { get; set; }
        public bool DevMode { get; set; }
        public Result result { get; set; }
        public Debug debug { get; set; }
    }
}
