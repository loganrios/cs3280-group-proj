using System;

namespace group_proj.Main
{
    public static class clsMainSQL
    {
        /// <summary>
        /// Delete line items for a given invoice id.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static string DeleteLineItemsFromInvoiceID(int InvoiceID)
        {
            return
                "DELETE FROM LineItems " +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        /// <summary>
        /// Delete an invoice from an associated id.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static string DeleteInvoice(int InvoiceID)
        {
            return
                "DELETE FROM Invoices " +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        /// <summary>
        /// Inserts a new line item for a given invoice (by id).
        /// Correlates to the LineItems table.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <param name="LineItemNumber"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public static string InsertLineItemForInvoice(int InvoiceID, int LineItemNumber, string ItemCode)
        {
            return
                "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                "Values(" + InvoiceID.ToString() + ", " +
                LineItemNumber.ToString() + ", " +
                "'" + ItemCode + "');";
        }

        /// <summary>
        /// Insert a new invoice into the Invoices table.
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        /// <returns></returns>
        public static string InsertNewInvoice(DateTime InvoiceDate, int TotalCost)
        {
            return
                "INSERT INTO Invoices(InvoiceDate, TotalCost) " +
                "Values(#" + StringifyDate(InvoiceDate) + "#, " + TotalCost.ToString() + ");";
        }

        /// <summary>
        /// Returns all relevant fields from the ItemDesc table.
        /// </summary>
        /// <returns></returns>
        public static string GetAllItems()
        {
            return
                "select ItemCode, ItemDesc, Cost from ItemDesc;";
        }

        /// <summary>
        /// Returns a list of LineItems along with their ItemDesc information
        /// for a given Invoice ID.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static string GetLineItemsInInvoice(int InvoiceID)
        {
            return
                "SELECT LineItems.LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                "FROM LineItems INNER JOIN ItemDesc " +
                "ON LineItems.ItemCode = ItemDesc.ItemCode " +
                "WHERE LineItems.InvoiceNum = " + InvoiceID.ToString() + ";";
        }

        /// <summary>
        /// Gets the maximum ID in the Invoices table--since our application is not
        /// concurrent, this will always be the newest invoice we just inserted.
        /// </summary>
        /// <returns></returns>
        public static string GetNewestInsertedInvoiceID()
        {
            return "SELECT TOP 1 InvoiceNum FROM Invoices ORDER BY InvoiceNum DESC;";
        }

        /// <summary>
        /// Updates an invoice in the invoice table based on an id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string UpdateInvoice(int id, DateTime date, int cost)
        {
            return "UPDATE Invoices SET InvoiceDate = #"+ StringifyDate(date) + "#," +
                " TotalCost = " + cost.ToString() + 
                " WHERE InvoiceNum = " + id.ToString() + ";";
        }

        /// <summary>
        /// Utility function to stringify a date into the right format.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string StringifyDate(DateTime dt)
        {
            return dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString();
        }
    }
}
