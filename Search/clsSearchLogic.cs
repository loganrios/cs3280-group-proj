using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace group_proj.Search
{
    class clsSearchLogic
    {

        /// <summary>
        /// string for the sSQL to be sent to the database
        /// </summary>
        private string sSQL;

        /// <summary>
        /// Creates the dataset object
        /// </summary>
        DataSet ds;

        /// <summary>
        /// set the num of return values
        /// </summary>
        int iNumRetValues = 0;

        /// <summary>
        /// The class Data object access the database
        /// </summary>
        clsDataAccess classDataAccess;

        /// <summary>
        /// Class Search SQL handles all the SQL search items
        /// </summary>
        clsSearchSQL classSearchSQL;

        /// <summary>
        /// Constructor constructing
        /// </summary>
        public clsSearchLogic()
        {
            try
            {
                classDataAccess = new clsDataAccess();
                classSearchSQL = new clsSearchSQL();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This will eventually be used to pull the invoices using the sql class. Right now this 
        /// is just a test bed. 
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> pullAllInvoice()
        {
            try
            {
                List<clsInvoice> invoiceToReturn = new List<clsInvoice>();

                sSQL = classSearchSQL.returnInvoices();

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);


                for (int i = 0; i < iNumRetValues; i++)
                {
                    clsInvoice currentInvoice = new clsInvoice((int)ds.Tables[0].Rows[i][0], (DateTime)ds.Tables[0].Rows[i][1], (int)ds.Tables[0].Rows[i][2]);
                    invoiceToReturn.Add(currentInvoice);
                }

                return invoiceToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<int> pullAllInvoiceNumbers()
        {
            try
            {
                List<int> invoiceToReturn = new List<int>();

                sSQL = classSearchSQL.returnInvoices();

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);


                for (int i = 0; i < iNumRetValues; i++)
                {
                    invoiceToReturn.Add((int)ds.Tables[0].Rows[i][0]);
                }

                return invoiceToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<int> pullAllInvoiceCharges()
        {
            try
            {
                List<int> invoiceToReturn = new List<int>();

                sSQL = classSearchSQL.returnInvoices();

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);


                for (int i = 0; i < iNumRetValues; i++)
                {
                    invoiceToReturn.Add((int)ds.Tables[0].Rows[i][2]);
                }

                invoiceToReturn.Sort();

                return invoiceToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        ///// <summary>
        ///// Returns a string to send to main window
        ///// </summary>
        ///// <param name="selectedItem">Class invoice</param>
        ///// <returns>String of Invoice num</returns>
        //public int invoiceToSendToEdit(clsInvoice selectedItem)
        //{
        //    try
        //    {
        //        clsInvoice invoiceToEdit = selectedItem;
        //        return invoiceToEdit.iInvoiceNum;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
        //                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }
        //}
    }
}
