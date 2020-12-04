using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj
{
    public class clsLineItem
    {
        [DisplayName("Line Item")]
        public int LineItemNumber { get; set; }

        [DisplayName("Item Code")]
        public string ItemCode { get; set; }

        [DisplayName("Item Desc")]
        public string ItemDesc { get; set; }

        [DisplayName("Cost")]
        public double Cost { get; set; }

        public clsLineItem(int lineItemNumber, clsItem item)
        {
            this.LineItemNumber = lineItemNumber;
            this.ItemCode = item.ItemCode;
            this.ItemDesc = item.ItemDesc;
            this.Cost = item.Cost;
        }
    }
}
