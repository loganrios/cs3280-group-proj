using System.ComponentModel;

namespace group_proj
{
    public class clsItem
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
