using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj
{
    class clsItem
    {
        [DisplayName("Item Code")]
        public string ItemCode { get; set; }

        [DisplayName("Item Desc")]
        public string ItemDesc { get; set; }

        [DisplayName("Cost")]
        public double Cost { get; set; }


        public clsItem(string ItemCode, string ItemDesc, double Cost)
        {
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Cost = Cost;
        }
    }
}
