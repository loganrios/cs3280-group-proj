using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace group_proj.Main
{
    public static class clsMainLogic
    {
        static clsDataAccess db;

        static clsMainLogic()
        {
            db = new clsDataAccess();
        }

        public static clsInvoice GenerateNewInvoice()
        {
            return new clsInvoice();
        }

        public static List<clsItem> GetAllAvailableItems()
        {
            // todo
            List<clsItem> items = new List<clsItem>();
            int i = 0;
            DataSet ds = db.ExecuteSQLStatement(clsMainSQL.GetAllItems(), ref i);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string code = row.ItemArray[0].ToString();
                string desc = row.ItemArray[1].ToString();
                if (!double.TryParse(row.ItemArray[2].ToString(), out double cost))
                {
                    // todo maybe throw exception here?
                    cost = 0.0;
                }
                items.Add(new clsItem(code, desc, cost));
            }
            return items;
        }

        public static clsItem GetItemFromItemCode(List<clsItem> availableItems, string code)
        {
            return availableItems.Find(x => x.ItemCode.Equals(code));
        }

        public static List<clsLineItem> GetLineItemsForInvoice(int? iInvoiceNum)
        {
            List<clsLineItem> li = new List<clsLineItem>();

            if (iInvoiceNum is null) 
            {
                return li;
            }

            // We shouldn't ever hit this statement, but we can't pass in a nullable int to
            // our SQL (nor should we be able to)
            int InvoiceID = iInvoiceNum ?? default(int);
            int rowcount = 0;
            DataSet ds = db
                .ExecuteSQLStatement(clsMainSQL.GetLineItemsInInvoice(InvoiceID), ref rowcount);

            if (rowcount == 0)
            {
                return li;
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int LineItemNumber = (int)row.ItemArray[0];
                string ItemCode = row.ItemArray[1].ToString();
                string ItemDesc = row.ItemArray[2].ToString();
                if (!double.TryParse(row.ItemArray[3].ToString(), out double cost))
                    cost = 0.0;

                li.Add(new clsLineItem(LineItemNumber, new clsItem(ItemCode, ItemDesc, cost)));
            }

            return li;
        }

        public static List<clsLineItem> RemoveLineItem(List<clsLineItem> items, int lineItemNo)
        {
            items.RemoveAll(x => x.LineItemNumber == lineItemNo);
            return items;
        }

        public static clsInvoice GetInvoiceFromId(int id)
        {
            // todo
            return new clsInvoice();
        }

        public static int GetNextLineItemNumber(List<clsLineItem> lineitems)
        {
            int m = 0;
            foreach (clsLineItem li in lineitems)
            {
                if (li.LineItemNumber > m)
                {
                    m = li.LineItemNumber;
                }
            }
            return m + 1;
        }
    }
}
