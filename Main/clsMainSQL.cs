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
                "Values('#4/13/2018#', 100)";
        }

        public string SelectInvoiceData(int InvoiceID)
        {
            return
                "SELECT InvoiceNum, InvoiceDate, TotalCost" +
                "FROM Invoices" +
                "WHERE InvoiceNum = " + InvoiceID.ToString() + "; ";
        }
    }
}
