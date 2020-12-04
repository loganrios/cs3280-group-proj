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
        public int? iInvoiceNum { get; set; }

        [DisplayName("Invoice Date")]
        public string sInvoiceDate { get; set; }

        public DateTime InvoiceDate { get; set; }

        [DisplayName("Total Cost")]
        public int dTotalCost { get; set; }

        public List<clsLineItem> LineItems;

        public clsInvoice (int iInvoiceNum, DateTime dtInvoiceDate, int dTotalCost)
        {
            this.iInvoiceNum = iInvoiceNum;
            this.dTotalCost = dTotalCost;
            sInvoiceDate = dtInvoiceDate.ToShortDateString();
            LineItems = new List<clsLineItem>();
        }

        public clsInvoice()
        {
            this.iInvoiceNum = null;
            this.InvoiceDate = DateTime.Now;
            this.sInvoiceDate = DateTime.Now.ToShortDateString();
            this.dTotalCost = 0;
            this.LineItems = new List<clsLineItem>();
        }
    }
}
