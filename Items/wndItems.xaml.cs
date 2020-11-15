using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        clsItemsLogic logic = new clsItemsLogic();
        clsItemsSQL sqlQueries;
        DataGrid data;
        public int index;

        public wndItems()
        {
            InitializeComponent();
            sqlQueries = new clsItemsSQL();
            data = new DataGrid();
            //getItemsList();
        }

        //public void getItemsList()
        //{
        //    try
        //    {
        //        foreach (var item in sqlQueries.getItemsList())
        //        {
        //            itemDescDataGrid.Items.Add(item.ToString());
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void ItemDescDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                data = sender as DataGrid;
                DataRowView dataInfo = (DataRowView)data.SelectedItems[0];
                ItemSelectedCode.Text = dataInfo.Row.ItemArray[0].ToString();
                ItemDescUpdate.Text = dataInfo.Row.ItemArray[1].ToString();
                CostUpdate.Text = dataInfo.Row.ItemArray[2].ToString();
                index = data.SelectedIndex;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Will be used to add items to the database
                //sqlQueries.addItem(ItemSelectedCode.Text, addItem.Text, addCost.Text);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void updateItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                sqlQueries.updateCurrentRow(ItemSelectedCode.Text, ItemDescUpdate.Text, CostUpdate.Text);
                //When applicable this method call updates the rows in the data grid.
                //logic.updateItemRow(index, ItemDescUpdate.Text, CostUpdate.Text);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Deleting this row will permanently delete it from the database. Would you like to continue?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
    }
}
