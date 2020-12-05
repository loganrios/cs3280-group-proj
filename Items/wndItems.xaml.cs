using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using group_proj.Main;

namespace group_proj.Items
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// Creates a new object for clsItemsLogic class.
        /// </summary>
        clsItemsLogic logic = new clsItemsLogic();
        /// <summary>
        /// Creates a new data grid object.
        /// </summary>
        DataGrid data;
        /// <summary>
        /// Creates a new list of items.
        /// </summary>
        List<clsItem> dataInfo;

        /// <summary>
        /// Runs when the items window is called. 
        /// Instantiates objects and variables when needed. 
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            data = new DataGrid();
            DataContext = this;
            getItemsList();
        }

        /// <summary>
        /// Gets the list of the items from the database. 
        /// </summary>
        public void getItemsList()
        {
            try
            {
                itemdescdatagrid.ItemsSource = logic.getItemsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Event is called when the item in the data grid is selected.
        /// Also populates item selected to the update and delete fields. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDescDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                data = sender as DataGrid;
                if(data.Items.Count != 0)
                {
                    dataInfo = data.SelectedItems.Cast<clsItem>().ToList();
                    ItemSelectedCode.Text = dataInfo[0].ItemCode;
                    ItemDescUpdate.Text = dataInfo[0].ItemDesc;
                    CostUpdate.Text = dataInfo[0].Cost.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); 
            }
        }

        /// <summary>
        /// Calls the method to add an item to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(addItem.Text == "" && addCost.Text == "")
                {
                    MessageBox.Show("Please insert an item and its cost");
                }
                else
                {
                    //Will be used to add items to the database
                    logic.addItem(addItem.Text, addCost.Text);
                    resetDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Calls the updateItemRow method from the items logic class to update the item selected
        /// to the users changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (ItemDescUpdate.Text == "" && CostUpdate.Text == "" || ItemSelectedCode.Text == "")
                {
                    MessageBox.Show("No item selected to update");
                }
                else
                {
                    //When applicable this method call updates the rows in the data grid.
                    logic.updateItemRow(ItemSelectedCode.Text, ItemDescUpdate.Text, CostUpdate.Text);
                    resetDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Deletes item from the database using the item code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logic.deleteSelectedItem(ItemSelectedCode.Text);
                resetDG();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Upon returning from available items from database list. The main window will reflect the changes made in the window items interface. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Creates and displays the headers for the columns in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Itemdescdatagrid_AutoGeneratedColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

        /// <summary>
        /// Resetsw and clears the fields in the window.
        /// </summary>
        private void resetDG()
        {
            itemdescdatagrid.ItemsSource = null;
            itemdescdatagrid.ItemsSource = logic.getItemsList();
            ItemSelectedCode.Text = " ";
            ItemDescUpdate.Text = " ";
            CostUpdate.Text = " ";
            addItem.Text = " ";
            addCost.Text = " ";
        }

        /// <summary>
        /// Validating that user only enters numbers for cost in adding item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validating that user only enters numbers for cost in updating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostUpdate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
