using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace group_proj.Items
{
    class clsItemsLogic
    {
        //clsDataAccess db = new clsDataAccess();
        //public DataSet ds;

        /// <summary>
        /// Updates the row selected in the DataGrid when it is used.
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <param name="cost"></param>
        //public void updateItemRow(int index, string item, string cost)
        //{
        //    try
        //    {
        //        ds.Tables[1].Rows[index][1] = item;
        //        ds.Tables[1].Rows[index][2] = cost;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }
        //}

        /// <summary>
        /// Retreives a list of every item in the ItemsDesc table.
        /// </summary>
        /// <returns></returns>
        //public List<clsItem> getitemslist()
        //{
        //    List<clsItem> itemsList = new List<clsItem>();

        //    try
        //    {
        //        //create database to hold the data
        //        DataSet ds;

        //        //number of returned values.
        //        int iret = 0;

        //        //sql query to get all the values from the ItemDesc table.
        //        ds = db.ExecuteSQLStatement("select * from itemdesc", ref iret);

        //        //loops through all the values returned
        //        for (int i = 0; i < iret; i++)
        //        {
        //            //creates new item object.
        //            clsItem item = new clsItem();
        //            item.ItemCode = ds.Tables[0].Rows[i].ItemArray[0].ToString();
        //            item.ItemDesc = ds.Tables[0].Rows[i].ItemArray[1].ToString();
        //            item.Cost = (double)ds.Tables[0].Rows[i].ItemArray[2];
        //            itemsList.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }
        //    return itemsList;
        //}

    }
}
