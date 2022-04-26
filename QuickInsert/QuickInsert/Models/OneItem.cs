using Newtonsoft.Json;

namespace QuickInsert.DAL
{
    public class OneItem
    {
        public string Name { set; get; }

        public string Value { set; get; }

        public bool IsValueHidden { set; get; }

        public string KeyShortCuts { set; get; }

        [JsonIgnore]
        public bool Selected { set; get; }
    }
}
