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
                //Temp list to return
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

        /// <summary>
        /// Returns all invoices for comboBox
        /// </summary>
        /// <returns>Int of Invoice Numbers</returns>
        public List<int> pullAllInvoiceNumbers()
        {
            try
            {
                //Temp list to return
                List<int> invoiceToReturn = new List<int>();

                sSQL = classSearchSQL.returnInvoices();

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);


                for (int i = 0; i < iNumRetValues; i++)
                {
                    invoiceToReturn.Add((int)ds.Tables[0].Rows[i][0]);
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

        /// <summary>
        /// Returns List of Invoice Charges for ComboBox
        /// </summary>
        /// <returns>Int of Charges</returns>
        public List<int> pullAllInvoiceCharges()
        {
            try
            {
                //Temp list to filter out below
                List<int> listToFilter = new List<int>();

                //Temp list to return
                List<int> listToReturn = new List<int>();


                sSQL = classSearchSQL.returnInvoices();

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);


                for (int i = 0; i < iNumRetValues; i++)
                {
                    listToFilter.Add((int)ds.Tables[0].Rows[i][2]);
                }


                listToReturn = listToFilter.Distinct().ToList();

                listToReturn.Sort();
                return listToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// using filters selected, updates invoice list
        /// </summary>
        /// <param name="cbInvoiceNumber"></param>
        /// <param name="cbTotalCharge"></param>
        /// <param name="selectedDate"></param>
        /// <returns>List of invoices</returns>
        internal List<clsInvoice> determineFilter(object cbInvoiceNumber, object cbTotalCharge, DateTime? selectedDate)
        {
            try
            {
                //temp filter to return
                List<clsInvoice> listToReturn = new List<clsInvoice>();

                if (cbInvoiceNumber != null && cbTotalCharge != null && selectedDate != null)
                {
                    sSQL = classSearchSQL.returnInvoices((int)cbInvoiceNumber, (DateTime)selectedDate, (int)cbTotalCharge);
                }
                else if (cbInvoiceNumber != null && cbTotalCharge != null)
                {
                    sSQL = classSearchSQL.returnInvoices((int)cbInvoiceNumber, (int)cbTotalCharge);
                }
                else if (cbTotalCharge != null && selectedDate != null)
                {
                    sSQL = classSearchSQL.returnInvoices((DateTime)selectedDate, (int)cbTotalCharge);
                }
                else if (cbInvoiceNumber != null && selectedDate != null)
                {
                    sSQL = classSearchSQL.returnInvoices((int)cbInvoiceNumber, (DateTime)selectedDate);
                }
                else if (cbInvoiceNumber != null)
                {
                    sSQL = classSearchSQL.returnInvoices((int)cbInvoiceNumber);
                }
                else if (cbTotalCharge != null)
                {
                    sSQL = classSearchSQL.returnInvoicesTotalCostOnly((int)cbTotalCharge);
                }
                else if (selectedDate != null)
                {
                    sSQL = classSearchSQL.returnInvoices((DateTime)selectedDate);
                }
                else
                {
                    sSQL = sSQL = classSearchSQL.returnInvoices();
                }

                ds = classDataAccess.ExecuteSQLStatement(sSQL, ref iNumRetValues);

                for (int i = 0; i < iNumRetValues; i++)
                {
                    clsInvoice currentInvoice = new clsInvoice((int)ds.Tables[0].Rows[i][0], (DateTime)ds.Tables[0].Rows[i][1], (int)ds.Tables[0].Rows[i][2]);
                    listToReturn.Add(currentInvoice);
                }

                return listToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
