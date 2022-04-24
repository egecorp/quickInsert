using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickInsert.DAL
{
    public class OneItem
    {

        public long Id { set; get; }

        public string Name { set; get; }

        public string Value { set; get; }

        public bool IsValueHidden { set; get; }

        public int Order { set; get; }

        public string KeyShortCuts { set; get; }

        public bool IsReadyToSave()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value);
        }
    }
}
