﻿using System;
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
        clsDataAccess db = new clsDataAccess();
        clsItemsSQL sql = new clsItemsSQL();
        public DataSet ds;
        char char1 = 'A';
        char char2 = 'A';
        string sSQL;
        /// <summary>
        /// Updates the row selected in the DataGrid when it is used.
        /// <param name = "index" ></ param >
        /// < param name="item"></param>
        /// <param name = "cost" ></ param >
        public void updateItemRow(string index, string item, string cost)
        {
            try
            {
                sSQL = sql.updateCurrentRow(index, item, cost);
                db.ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add item to database randomly creates an item id. Also Creates a new item codes.
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void addItem(string desc, string cost)
        {
            string code = char1.ToString() + char2.ToString();
            char1++;
            char2++;
            sSQL = sql.insertItem(code ,desc, cost);
            db.ExecuteNonQuery(sSQL);
        }

        ///  <summary>
        ///  Retreives a list of every item in the ItemsDesc table.
        ///  </summary>
        ///  <returns></returns>
        public List<clsItem> getItemsList()
        {
            List<clsItem> itemsList = new List<clsItem>();

            try
            {
                //number of returned values.
                int iret = 0;

                //sql query to get all the values from the ItemDesc table.
                ds = db.ExecuteSQLStatement(sql.getItems(), ref iret);

                //loops through all the values returned
                for (int i = 0; i < iret; i++)
                {
                    //creates new item object.
                    clsItem item = new clsItem(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[2]));
                    itemsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return itemsList;
        }
        /// <summary>
        /// Obtains sql query and executes query to delete item from database.
        /// </summary>
        /// <param name="itemCode"></param>
        public void deleteItem(string itemCode)
        {
            sSQL = sql.deleteItem(itemCode);
            db.ExecuteNonQuery(sSQL);
        }
        /// <summary>
        /// Runs a query for lineItems to check for invoice number.
        /// </summary>
        public int checkInvoice(string code)
        {
            sSQL = sql.getSingleItem(code);
            int iret = 0;

            ds =  db.ExecuteSQLStatement(sSQL,ref iret);
            if(ds.Tables[0].Rows.Count == 1)
            {
               return Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray[0]);
            }
            else
            {
                return ds.Tables[0].Rows.Count;
            }
        }

    }
}
