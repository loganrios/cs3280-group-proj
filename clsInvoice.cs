using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace group_proj
{
    public class clsInvoice
    {
        [DisplayName("Invoice Number")]
        public int? iInvoiceNum { get; set; }

        [DisplayName("Invoice Date")]
        public string sInvoiceDate { get; set; }

        [DisplayName("Total Cost")]
        public int dTotalCost { get; set; }

        public List<clsLineItem> LineItems;

        private DateTime InvoiceDate;

        public clsInvoice(int iInvoiceNum, DateTime dtInvoiceDate, int dTotalCost)
        {
            this.iInvoiceNum = iInvoiceNum;
            this.dTotalCost = dTotalCost;
            this.InvoiceDate = dtInvoiceDate;
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

        public DateTime GetInvoiceDate()
        {
            return this.InvoiceDate;
        }

        public void SetInvoiceDate(DateTime d)
        {
            this.InvoiceDate = d;
        }
    }
}
