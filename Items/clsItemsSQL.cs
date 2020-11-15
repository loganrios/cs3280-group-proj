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

        public string deleteItem(string code)
        {
            return "Delete from ItemDesc Where ItemCode = '"+ code +"'";
        }

        public string getItems()
        {
            return "select * from itemdesc";
        }

        public string getSingleItem(string code)
        {
            return "Select Distinct InvoiceNum From LineItems Where ItemCode = '" + code + "'";
        }
    }
}
