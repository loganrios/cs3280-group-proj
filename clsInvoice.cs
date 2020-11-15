using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj
{
    public class clsInvoice
    {
        public int iInvoiceNum { get; set; }
        public DateTime dtInvoiceDate { get; set; }
        public int dTotalCost { get; set; }


        public clsInvoice (int iInvoiceNum, DateTime dtInvoiceDate, int dTotalCost)
        {
            this.iInvoiceNum = iInvoiceNum;
            this.dtInvoiceDate = dtInvoiceDate;
            this.dTotalCost = dTotalCost;
        }


    }
}
