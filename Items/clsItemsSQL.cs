using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace group_proj.Items
{
    class clsItemsSQL
    {
        /// <summary>
        /// Method returns a string to be used in the items logic class
        /// string returned is used to update the item itemDesc table.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="item"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string updateCurrentRow(string code, string item, string cost)
        {
            try
            {
                return "Update ItemDesc " +
                        "Set ItemDesc = '" + item + "', Cost = '" + cost + "' " +
                        "Where ItemCode = '" + code + "'";
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Method returns a string to be used in the items logic class
        /// string returned is used to insert an item to the itemDesc table.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="item"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string insertItem(string code, string item, string cost)
        {
            try
            {
                return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('" + code + "','" + item + "','" + cost + "')";
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Method returns a string to be used in the items logic class
        /// string returned is used to delete the item selected.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string deleteItem(string code)
        {
            return "Delete from ItemDesc Where ItemCode = '"+ code +"'";
        }

        /// <summary>
        /// Method returns a string to be used in the items logic class
        /// string returned is used to get all the items from itemDesc table.
        /// </summary>
        /// <returns></returns>
        public string getItems()
        {
            return "select * from itemdesc";
        }
        /// <summary>
        /// Method returns a string to be used in the items logic class
        /// string returned is used to get all the items from itemDesc table.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string getSingleItem(string code)
        {
            return "Select Distinct InvoiceNum From LineItems Where ItemCode = '" + code + "'";
        }
    }
}
