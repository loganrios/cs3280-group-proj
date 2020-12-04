using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_proj.Main
{
    public static class clsMainLogic
    {
        static clsDataAccess db;

        static clsMainLogic()
        {
            db = new clsDataAccess();
        }

        public static clsInvoice GenerateNewInvoice()
        {
            return new clsInvoice();
        }

        public static List<clsItem> GetAllAvailableItems()
        {
            // todo
            List<clsItem> items = new List<clsItem>();
            items.Add(new clsItem("X", "MacBook Air (Intel)", 1200.0));
            items.Add(new clsItem("Y", "Airpods Pro", 250.0));
            items.Add(new clsItem("Z", "iPad Pro 12.5", 1100.0));
            return items;
        }

        public static clsItem GetItemFromItemCode(List<clsItem> availableItems, string code)
        {
            return availableItems.Find(x => x.ItemCode.Equals(code));
        }

        public static clsInvoice GetInvoiceFromId(int id)
        {
            // todo
            return new clsInvoice();
        }
    }
}
