using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Item(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
