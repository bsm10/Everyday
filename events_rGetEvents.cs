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
    }

    public class Details
    {
        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbs { get; set; }
        public string cellulose { get; set; }
        public string kkal { get; set; }
        public IList<Item> items { get; set; }
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
        public string comment { get; set; }
        public Details details { get; set; }
        public string data_md5 { get; set; }
    }

    public class Events
    {
        public IList<Event> events_range { get; set; }
    }


