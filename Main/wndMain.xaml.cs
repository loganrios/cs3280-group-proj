using System.Windows;
using group_proj.Search;
using group_proj.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace group_proj.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        private clsInvoice ActiveInvoice;
        private List<clsItem> AvailableItems;

        public wndMain()
        {
            InitializeComponent();
            this.ActiveInvoice = new clsInvoice();
            this.AvailableItems = new List<clsItem>();
            ToggleMenuActive();
        }

        /// <summary>
        /// Open the Search for Invoices window, then retrieve the last active
        /// Invoice object after it closes. The last invoice object may or 
        /// may not be the one it was sent (the currently edited object),
        /// but either way we'll re-ping the database and get updated.
        /// </summary>
        private void btnSearchForInvoice_Click(object sender, RoutedEventArgs e)
        {
            clsInvoice inv = this.ActiveInvoice;
            wndSearch s = new wndSearch(inv);
            _ = s.ShowDialog();
            BindInvoice(s.invoiceToEdit);
        }

        /// <summary>
        /// Open Edit Items window. Forces a refresh upon close to update line items.
        /// </summary>
        private void btnEditAvailableItems_Click(object sender, RoutedEventArgs e)
        {
            wndItems i = new wndItems();
            _ = i.ShowDialog();
        }

        private void btnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            BindInvoice(clsMainLogic.GenerateNewInvoice());
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ToggleMenuActive();
        }

        private void btnDiscardChanges_Click(object sender, RoutedEventArgs e)
        {
            ToggleMenuActive();
        }

        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            ToggleMenuActive();
        }

        private void cbChooseAddItem_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.AvailableItems is null)
            {
                return;
            }
            string choice = (string)cbChooseAddItem.SelectedItem ?? "";
            clsItem selectedAddItem = clsMainLogic.GetItemFromItemCode(this.AvailableItems, choice);
            if (!(selectedAddItem is null))
            {
                this.txtAddItemDesc.Text = selectedAddItem.ItemDesc + " $" + selectedAddItem.Cost.ToString("0.##");
            }
        }

        private void dpInvoiceDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            clsItem itemToAdd = clsMainLogic.GetItemFromItemCode(this.AvailableItems, (string)cbChooseAddItem.SelectedItem ?? "");
            if (itemToAdd is null)
            {
                return;
            }

            int lineItemNo = clsMainLogic.GetNextLineItemNumber(this.ActiveInvoice.LineItems);
            this.ActiveInvoice.LineItems.Add(new clsLineItem(lineItemNo, itemToAdd));
            DrawItemsDataGrid(this.ActiveInvoice);
        }

        private void btnDeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtSelectedItemID.Text, out int ln))
            {
                return;
            }

            this.ActiveInvoice.LineItems = clsMainLogic.RemoveLineItem(this.ActiveInvoice.LineItems, ln);
            DrawItemsDataGrid(this.ActiveInvoice);
        }

        private void ToggleMenuActive()
        {
            gbMenu.IsEnabled = true;
            gbActiveInvoice.IsEnabled = false;
        }

        private void ToggleInvoiceActive()
        {
            gbMenu.IsEnabled = false;
            this.AvailableItems = clsMainLogic.GetAllAvailableItems();

            cbChooseAddItem.Items.Clear();

            foreach (clsItem item in this.AvailableItems)
            {
                cbChooseAddItem.Items.Add(item.ItemCode);
            }
            gbActiveInvoice.IsEnabled = true;
        }

        private void BindInvoice(clsInvoice i)
        {
            this.ActiveInvoice = i;

            if (i.iInvoiceNum is null)
            {
                txtInvoiceNumber.Text = "TBD";
            } 
            else
            {
                txtInvoiceNumber.Text = i.iInvoiceNum.ToString();
            }

            dpInvoiceDate.SelectedDate = i.InvoiceDate;

            ToggleInvoiceActive();
            return;
        }

        private async void DrawItemsDataGrid(clsInvoice inv)
        {
            dgInvoiceItems.ItemsSource = null;
            dgInvoiceItems.ItemsSource = inv.LineItems;
            await Task.Delay(10);
        }

        private async void BindSelectedItem(clsLineItem item)
        {
            txtSelectedItemID.Text = item.LineItemNumber.ToString();
            txtSelectedItemName.Text = item.ItemDesc;
            txtSelectedItemCost.Text = item.Cost.ToString("0.##");
            await Task.Delay(10);
        }

        private void dgInvoiceItems_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgInvoiceItems.SelectedItems.Count > 0)
            {
                BindSelectedItem((clsLineItem)dgInvoiceItems.SelectedItems[0]);
            }
        }
    }
} 
