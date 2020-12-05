using System;

namespace group_proj.Main
{
    public static class clsMainSQL
    {
        public static string DeleteLineItemsFromInvoiceID(int InvoiceID)
        {
            return
                "DELETE FROM LineItems " +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public static string DeleteInvoice(int InvoiceID)
        {
            return
                "DELETE FROM Invoices " +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public static string InsertLineItemForInvoice(int InvoiceID, int LineItemNumber, string ItemCode)
        {
            return
                "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                "Values(" + InvoiceID.ToString() + ", " +
                LineItemNumber.ToString() + ", " +
                "'" + ItemCode + "');";
        }

        public static string InsertNewInvoice(DateTime InvoiceDate, int TotalCost)
        {
            return
                "INSERT INTO Invoices(InvoiceDate, TotalCost) " +
                "Values(#" + StringifyDate(InvoiceDate) + "#, " + TotalCost.ToString() + ");";
        }

        public static string SelectInvoiceData(int InvoiceID)
        {
            return
                "SELECT InvoiceNum, InvoiceDate, TotalCost" +
                "FROM Invoices" +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public static string GetAllItems()
        {
            return
                "select ItemCode, ItemDesc, Cost from ItemDesc;";
        }

        public static string GetLineItemsInInvoice(int InvoiceID)
        {
            return
                "SELECT LineItems.LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                "FROM LineItems INNER JOIN ItemDesc " +
                "ON LineItems.ItemCode = ItemDesc.ItemCode " +
                "WHERE LineItems.InvoiceNum = " + InvoiceID.ToString() + ";";
        }

        public static string GetNewestInsertedInvoiceID()
        {
            return "SELECT TOP 1 InvoiceNum FROM Invoices ORDER BY InvoiceNum DESC;";
        }

        public static string UpdateInvoice(int id, DateTime date, int cost)
        {
            return "UPDATE Invoices SET InvoiceDate = #"+ StringifyDate(date) + "#," +
                " TotalCost = " + cost.ToString() + 
                " WHERE InvoiceNum = " + id.ToString() + ";";
        }

        public static string StringifyDate(DateTime dt)
        {
            return dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString();
        }
    }
}
