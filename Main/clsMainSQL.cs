using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj.Main
{
    class clsMainSQL
    {
        public string DeleteLineItemsFromInvoiceID(int InvoiceID)
        {
            return
                "DELETE FROM LineItems" +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public string DeleteInvoice(int InvoiceID)
        {
            return
                "DELETE FROM Invoices" +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public string InsertLineItemForInvoice(int InvoiceID, int Quantity, string ItemCode)
        {
            return
                "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                "Values(" + InvoiceID.ToString() + ", " +
                Quantity.ToString() + ", " +
                "'" + ItemCode + "');";
        }

        public string InsertNewInvoice(DateTime InvoiceDate, int TotalCost)
        {
            return
                "INSERT INTO Invoices(InvoiceDate, TotalCost) " +
                "Values('#" + StringifyDate(InvoiceDate) + "#', " + TotalCost.ToString() + ");";
        }

        public string SelectInvoiceData(int InvoiceID)
        {
            return
                "SELECT InvoiceNum, InvoiceDate, TotalCost" +
                "FROM Invoices" +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }

        public string GetAllItems()
        {
            return
                "select ItemCode, ItemDesc, Cost from ItemDesc;";
        }

        public string GetLineItemsInInvoice(int InvoiceID)
        {
            return
                "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                "FROM LineItems, ItemDesc " +
                "WHERE LineItems.ItemCode = ItemDesc.ItemCode " +
                "AND LineItems.InvoiceNum = " + InvoiceID.ToString() + ";";
        }

        public string StringifyDate(DateTime dt)
        {
            return dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString();
        }
    }
}
