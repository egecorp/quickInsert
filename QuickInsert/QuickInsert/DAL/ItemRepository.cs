using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace QuickInsert.DAL
{
    public class ItemRepository
    {
        private static ItemRepository _singleton = null;
        
        private static string GetJsonFileFullName()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), jsonFileName);

        }
        public static void Save(string jsonFileBody)
        {
            ItemRepository newItemRepository = JsonConvert.DeserializeObject<ItemRepository>(jsonFileBody);
            File.WriteAllText(GetJsonFileFullName(), jsonFileBody);
            _singleton = newItemRepository;
        }
        public static ItemRepository GetSingleton()
        {
            if (_singleton != null) return _singleton;

            var jsonFullFileName = GetJsonFileFullName();

            string jsonFileBody;
            if (File.Exists(jsonFullFileName))
            {
                jsonFileBody = File.ReadAllText(jsonFullFileName);
                return _singleton = JsonConvert.DeserializeObject<ItemRepository>(jsonFileBody);
            }

            _singleton = new ItemRepository();

            _singleton.Items = new List<OneItem>()
                    {
                        new OneItem()
                        {
                            Name = "Название",
                            Value = "Значение",
                            KeyShortCuts = null,
                            IsValueHidden = false
                        }
                    };

            Save(_singleton.GetJson());
            return _singleton;
        }


        private const string jsonFileName = "quickInsert.json";

        [JsonProperty]
        public List<OneItem> Items { set; get; }
        
        public ItemRepository()
        {

        }

        public List<OneItem> GetItems()
        {
            var resultItems = (Items ??= new List<OneItem>()).ToList();

            if (resultItems.Count < 1) resultItems.Add(new OneItem() { Name = "Empty", Value = "Empty" });
            if (resultItems.Count < 2) resultItems.Add(new OneItem() { Name = "Empty", Value = "Empty" });
            if (resultItems.Count < 3) resultItems.Add(new OneItem() { Name = "Empty", Value = "Empty" });
            if (resultItems.Count < 4) resultItems.Add(new OneItem() { Name = "Empty", Value = "Empty" });
            if (resultItems.Count < 5) resultItems.Add(new OneItem() { Name = "Empty", Value = "Empty" });

            return Items;
        }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
