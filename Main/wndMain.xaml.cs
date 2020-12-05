using group_proj.Items;
using group_proj.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Reflection;

namespace group_proj.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Holds the invoice that is currently being edited.
        /// </summary>
        private clsInvoice ActiveInvoice;

        /// <summary>
        /// Holds a list of all available items from the database.
        /// </summary>
        private List<clsItem> AvailableItems;

        /// <summary>
        /// Constructs the window and sets default state.
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                this.ActiveInvoice = new clsInvoice();
                this.AvailableItems = new List<clsItem>();
                ToggleMenuActive();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Open the Search for Invoices window, then retrieve the last active
        /// Invoice object after it closes. The last invoice object may or 
        /// may not be the one it was sent (the currently edited object),
        /// but either way we'll re-ping the database and get updated.
        /// </summary>
        private void btnSearchForInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsInvoice inv = this.ActiveInvoice;
                wndSearch s = new wndSearch(inv);
                _ = s.ShowDialog();
                // We'll need to also get the items first, since the Search window doesn't do this.
                if (s.invoiceToEdit is null)
                {
                    return;
                }
                s.invoiceToEdit.LineItems = clsMainLogic.GetLineItemsForInvoice(s.invoiceToEdit.iInvoiceNum);
                BindInvoice(s.invoiceToEdit);
                DrawItemsDataGrid(this.ActiveInvoice);
                ToggleEditInvoiceItemsOff();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Open Edit Items window. Forces a refresh upon close to update line items.
        /// </summary>
        private void btnEditAvailableItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndItems i = new wndItems();
                _ = i.ShowDialog();
                this.ActiveInvoice.LineItems = clsMainLogic.GetLineItemsForInvoice(this.ActiveInvoice.iInvoiceNum);
                BindInvoice(this.ActiveInvoice);
                DrawItemsDataGrid(this.ActiveInvoice);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Generate a new invoice and bind it to the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BindInvoice(clsMainLogic.GenerateNewInvoice());
                DrawItemsDataGrid(this.ActiveInvoice);
                ToggleEditInvoiceItemsOff();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Save the changes made to an individual invoice (write to database)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainLogic.SaveInvoiceChanges(this.ActiveInvoice);
                gbInvoiceItems.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Select a new invoice without saving the changes to the current one.
        /// Disables the Invoice Items group box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiscardChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gbInvoiceItems.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the invoice from the database and unsets it from the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainLogic.DeleteInvoice(ActiveInvoice);
                this.ActiveInvoice = clsMainLogic.GenerateNewInvoice();
                BindInvoice(ActiveInvoice);
                DrawItemsDataGrid(ActiveInvoice);
                ToggleEditInvoiceItemsOn();
                ToggleMenuActive();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Changes the text in the item description window upon the ItemCode combo box change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseAddItem_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Sets the active invoice's date after a DatePicker change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpInvoiceDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (!(dpInvoiceDate.SelectedDate is null))
                {
                    this.ActiveInvoice.SetInvoiceDate((System.DateTime)dpInvoiceDate.SelectedDate);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Adds the currently selected item to the ActiveInvoice's line items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected item from the Active Invoice's line items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtSelectedItemLN.Text, out int ln))
                {
                    return;
                }

                this.ActiveInvoice.LineItems = clsMainLogic.RemoveLineItem(this.ActiveInvoice.LineItems, ln);
                DrawItemsDataGrid(this.ActiveInvoice);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If a row is selected on the data grid, make it the Selected Item and bind to UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgInvoiceItems_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (dgInvoiceItems.SelectedItems.Count > 0)
                {
                    BindSelectedItem((clsLineItem)dgInvoiceItems.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Enables the editing of an Invoice's items. If the invoice is new, 
        /// write it out to the database and get an ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.ActiveInvoice.iInvoiceNum is null)
                {
                    int id = clsMainLogic.SaveNewInvoice(this.ActiveInvoice);
                    this.ActiveInvoice.iInvoiceNum = id;
                    BindInvoice(this.ActiveInvoice);
                    DrawItemsDataGrid(this.ActiveInvoice);
                }

                ToggleEditInvoiceItemsOn();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        // Internal Methods Begin

        /// <summary>
        /// Toggles the Invoice editor off.
        /// </summary>
        private void ToggleMenuActive()
        {
            try
            {
                gbActiveInvoice.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Toggle the invoice editor on (also populates the combo box to prepare for editing)
        /// </summary>
        private void ToggleInvoiceActive()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Binds a given invoice to the UI (may or may not be ActiveInvoice)
        /// </summary>
        /// <param name="i"></param>
        private void BindInvoice(clsInvoice i)
        {
            try
            {
                if (i is null)
                {
                    return;
                }

                this.ActiveInvoice = i;
                if (i.iInvoiceNum is null)
                {
                    txtInvoiceNumber.Text = "TBD";
                }
                else
                {
                    txtInvoiceNumber.Text = i.iInvoiceNum.ToString();
                }

                dpInvoiceDate.SelectedDate = i.GetInvoiceDate();

                ToggleInvoiceActive();
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Draw the line items into the data grid. May or may not be ActiveInvoice's LineItems.
        /// </summary>
        /// <param name="inv"></param>
        private async void DrawItemsDataGrid(clsInvoice inv)
        {
            try
            {
                dgInvoiceItems.ItemsSource = null;
                dgInvoiceItems.ItemsSource = inv.LineItems;
                double totalcost = 0;
                foreach (clsLineItem item in inv.LineItems)
                {
                    totalcost += item.Cost;
                }

                txtTotalCost.Text = "$" + totalcost.ToString("0.##");
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Bind an item as "selected" into the UI to enable deletion.
        /// </summary>
        /// <param name="item"></param>
        private async void BindSelectedItem(clsLineItem item)
        {
            try
            {
                txtSelectedItemLN.Text = item.LineItemNumber.ToString();
                txtSelectedItemName.Text = item.ItemDesc;
                txtSelectedItemCost.Text = item.Cost.ToString("0.##");
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Enables the editing of an invoice's items
        /// </summary>
        private async void ToggleEditInvoiceItemsOn()
        {
            try
            {
                gbInvoiceItems.IsEnabled = true;
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private async void ToggleEditInvoiceItemsOff()
        {
            try
            {
                gbInvoiceItems.IsEnabled = false;
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
