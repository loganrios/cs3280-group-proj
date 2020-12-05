using System.ComponentModel;

namespace group_proj
{
    public class clsItem
    {
        /// <summary>
        /// Creates the header for the column name
        /// </summary>
        [DisplayName("Item Code")]
        public string ItemCode { get; set; }
        /// <summary>
        /// Creates the header for the column name
        /// </summary>
        [DisplayName("Item Desc")]
        public string ItemDesc { get; set; }
        /// <summary>
        /// Creates the header for the column name
        /// </summary>
        [DisplayName("Cost")]
        public double Cost { get; set; }

        /// <summary>
        /// Instantiates the variables when constructed.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        public clsItem(string ItemCode, string ItemDesc, double Cost)
        {
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Cost = Cost;
        }
    }
}
