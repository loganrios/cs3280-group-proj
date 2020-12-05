using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace group_proj.Main
{
    public static class clsMainLogic
    {
        /// <summary>
        /// Holds the connection to the database.
        /// </summary>
        static clsDataAccess db;

        /// <summary>
        /// Static constructor that's called upon program run.
        /// Generates a data access class for static use.
        /// </summary>
        static clsMainLogic()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a new, blank invoice.
        /// </summary>
        /// <returns></returns>
        public static clsInvoice GenerateNewInvoice()
        {
            try
            {
                return new clsInvoice();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Fetches all available items in the database and
        /// returns as a list.
        /// </summary>
        /// <returns></returns>
        public static List<clsItem> GetAllAvailableItems()
        {
            try
            {
                List<clsItem> items = new List<clsItem>();
                int i = 0;
                DataSet ds = db.ExecuteSQLStatement(clsMainSQL.GetAllItems(), ref i);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string code = row.ItemArray[0].ToString();
                    string desc = row.ItemArray[1].ToString();
                    if (!double.TryParse(row.ItemArray[2].ToString(), out double cost))
                    {
                        cost = 0.0;
                    }
                    items.Add(new clsItem(code, desc, cost));
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the specific item from a list of items and an
        /// item code to search for.
        /// </summary>
        /// <param name="availableItems"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static clsItem GetItemFromItemCode(List<clsItem> availableItems, string code)
        {
            try
            {
                return availableItems.Find(x => x.ItemCode.Equals(code));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list from the database of all line items associated
        /// to a given invoice. Returns an empty list if the invoice id is null.
        /// </summary>
        /// <param name="iInvoiceNum"></param>
        /// <returns></returns>
        public static List<clsLineItem> GetLineItemsForInvoice(int? iInvoiceNum)
        {
            try
            {
                List<clsLineItem> li = new List<clsLineItem>();

                if (iInvoiceNum is null)
                {
                    return li;
                }

                // We shouldn't ever hit this statement, but we can't pass in a nullable int to
                // our SQL (nor should we be able to)
                int InvoiceID = iInvoiceNum ?? default;
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a line item from the internal representation of an invoice.
        /// Does not ping the database--that's done in the SaveChanges function.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="lineItemNo"></param>
        /// <returns></returns>
        public static List<clsLineItem> RemoveLineItem(List<clsLineItem> items, int lineItemNo)
        {
            try
            {
                items.RemoveAll(x => x.LineItemNumber == lineItemNo);
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an invoice from the database.
        /// </summary>
        /// <param name="invoiceToDelete"></param>
        internal static void DeleteInvoice(clsInvoice invoiceToDelete)
        {
            try
            {
                if (invoiceToDelete.iInvoiceNum is null)
                {
                    // this is a new invoice, do nothing
                    return;
                }
                int id = (int)invoiceToDelete.iInvoiceNum;
                db.ExecuteNonQuery(clsMainSQL.DeleteLineItemsFromInvoiceID(id));
                db.ExecuteNonQuery(clsMainSQL.DeleteInvoice(id));
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the next Line Item Number to use to add a new
        /// item to the invoice.
        /// </summary>
        /// <param name="lineitems"></param>
        /// <returns></returns>
        public static int GetNextLineItemNumber(List<clsLineItem> lineitems)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates date, cost, and lineitems for a pre-existing
        /// invoice. Commits internal invoice changes to the database.
        /// </summary>
        /// <param name="inv"></param>
        public static void SaveInvoiceChanges(clsInvoice inv)
        {
            try
            {
                // todo throw exception if id is null
                if (inv.iInvoiceNum is null)
                {
                    throw new Exception();
                }

                int id = (int)inv.iInvoiceNum;
                int totalcost = 0;
                foreach (clsLineItem li in inv.LineItems)
                {
                    totalcost += (int)li.Cost;
                }

                int rows_aff = db.ExecuteNonQuery(clsMainSQL.UpdateInvoice(id, inv.GetInvoiceDate(), totalcost));

                if (rows_aff != 1)
                {
                    throw new Exception();
                }

                db.ExecuteNonQuery(clsMainSQL.DeleteLineItemsFromInvoiceID(id));

                foreach (clsLineItem li in inv.LineItems)
                {
                    db.ExecuteNonQuery(clsMainSQL.InsertLineItemForInvoice(id, li.LineItemNumber, li.ItemCode));
                }

                return;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Writes out a new invoice to the database and returns
        /// the InvoiceID for further use.
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public static int SaveNewInvoice(clsInvoice inv)
        {
            try
            {
                // INSERT INTO -or- UPDATE
                int totalcost = 0;
                foreach (clsLineItem li in inv.LineItems)
                {
                    totalcost += (int)li.Cost;
                }

                int rowcount = 0;
                int insertCount = db.ExecuteNonQuery(
                    clsMainSQL.InsertNewInvoice(inv.GetInvoiceDate(), totalcost));

                // Get the number we just inserted
                DataSet ds = db.ExecuteSQLStatement(clsMainSQL.GetNewestInsertedInvoiceID(), ref rowcount);
                int newid = (int)ds.Tables[0].Rows[0].ItemArray[0];
                return newid;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
