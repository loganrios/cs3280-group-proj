using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace group_proj.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// General string to return items
        /// </summary>
        private string sSQLTOReturn;

        /// <summary>
        /// Returns all Invoices
        /// </summary>
        /// <returns>String to return All Invoices</returns>
        public string returnInvoices()
        {
            try
            {
                return "SELECT * FROM Invoices";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Return invoice for a particular invoice number
        /// </summary>
        /// <param name="iInvoiceNumber">Invoice Number</param>
        /// <returns>String for a particular invoice number</returns>
        public string returnInvoices(int iInvoiceNumber)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + iInvoiceNumber.ToString();
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Return invoice for a particular invoice number and invoice date
        /// </summary>
        /// <param name="iInvoiceNumber">Invoice Number</param>
        /// <param name="dtInvoiceDate">Invoice Date</param>
        /// <returns>String for invoice number and invoice date</returns>
        public string returnInvoices(int iInvoiceNumber, DateTime dtInvoiceDate)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + iInvoiceNumber.ToString() +
                " AND InvoiceDate = #" + dtInvoiceDate.ToShortDateString() + "#";
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }           
        }

        /// <summary>
        /// Return invoice for a particular invoice number,  invoice date, and total cost
        /// </summary>
        /// <param name="iInvoiceNumber">Invoice Number</param>
        /// <param name="dtInvoiceDate">Invoice Date</param>
        /// <param name="dTotalCost">Total Cost</param>
        /// <returns>String for invoice based on parm</returns>
        public string returnInvoices(int iInvoiceNumber, DateTime dtInvoiceDate, double dTotalCost)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + iInvoiceNumber.ToString() +
                                " AND InvoiceDate = #" + dtInvoiceDate.ToShortDateString() + "# AND TotalCost = " + dTotalCost.ToString();
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL for Invoice based on total cost
        /// </summary>
        /// <param name="dTotalCost">Total Cost</param>
        /// <returns>String for invoices based on cost</returns>
        public string returnInvoices(double dTotalCost)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE TotalCost = " + dTotalCost.ToString();
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }           
        }

        /// <summary>
        /// SQL for invoice based on Invoice Date
        /// </summary>
        /// <param name="dtInvoiceDate">Invoice Date</param>
        /// <returns>String for invoice based on invoice date</returns>
        public string returnInvoices(DateTime dtInvoiceDate)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dtInvoiceDate.ToShortDateString() + "#";
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }    
        }

        /// <summary>
        /// SQL for invoice based invoice date and total cost
        /// </summary>
        /// <param name="dtInvoiceDate">Invoice Date</param>
        /// <param name="dTotalCost">Total Cost</param>
        /// <returns>String based on invoice date and total cost</returns>
        public string returnInvoices(DateTime dtInvoiceDate, double dTotalCost)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceDate = #" +
                                dtInvoiceDate.ToShortDateString() + "# AND TotalCost = " + dTotalCost.ToString();
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }            
        }

        /// <summary>
        /// SQL for invoice based on invoice number and total cost
        /// </summary>
        /// <param name="iInvoiceNumber">Invoice Number</param>
        /// <param name="dTotalCost">Total Cost</param>
        /// <returns>String based on invoice number and total cost</returns>
        public string returnInvoices(int iInvoiceNumber, double dTotalCost)
        {
            try
            {
                sSQLTOReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + iInvoiceNumber.ToString() +
                                " AND TotalCost = " + dTotalCost.ToString();
                return sSQLTOReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }    
        }
    }
}
