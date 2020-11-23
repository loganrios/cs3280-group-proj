using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj
{
    public class clsInvoice
    {
        [DisplayName("Invoice Number")]
        public int iInvoiceNum { get; set; }

        [DisplayName("Invoice Date")]
        public string sInvoiceDate { get; set; }

        [DisplayName("Total Cost")]
        public int dTotalCost { get; set; }


        public clsInvoice (int iInvoiceNum, DateTime dtInvoiceDate, int dTotalCost)
        {
            this.iInvoiceNum = iInvoiceNum;
            this.dTotalCost = dTotalCost;

            DateTime date = dtInvoiceDate;
            sInvoiceDate = date.ToShortDateString();
        }


    }
}
