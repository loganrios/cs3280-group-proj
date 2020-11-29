using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using group_proj.Main;

namespace group_proj.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// clsSearchLogic controls all the logic for the search function
        /// </summary>
        clsSearchLogic classSearchLogic;

        /// <summary>
        /// Global variable that is able to be pulled by other classes. 
        /// This is how the user selected invoice is sent to main to be edited. 
        /// </summary>
        public clsInvoice invoiceToEdit { get; set; }

     
        /// <summary>
        /// Constructor constructing things
        /// </summary>
        public wndSearch(clsInvoice invoice)
        {
            try
            {
                InitializeComponent();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                classSearchLogic = new clsSearchLogic();

                updateDataGrid();
                updateCbInvoiceNum();
                updateCbTotalCharge();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void updateCbTotalCharge()
        {
            cbTotalCharge.ItemsSource = classSearchLogic.pullAllInvoiceCharges();
        }

        private void updateCbInvoiceNum()
        {
            cbInvoiceNumber.ItemsSource = classSearchLogic.pullAllInvoiceNumbers();
        }

        /// <summary>
        /// Fills the data grid
        /// </summary>
        public void updateDataGrid()
        {
            try
            {
                dgResults.ItemsSource = classSearchLogic.pullAllInvoice();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Controls the edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if ((clsInvoice)dgResults.SelectedItem != null)
                {
                    invoiceToEdit = (clsInvoice)dgResults.SelectedItem;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Click an Invoice To Edit");
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
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

        private void dgResults_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateListBasedOnFilter();
        }

        private void updateListBasedOnFilter()
        {
            dgResults.ItemsSource =
                classSearchLogic.determineFilter(cbInvoiceNumber.SelectedItem, cbTotalCharge.SelectedItem, dateInvoiceDate.SelectedDate);
        }

        private void cbTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateListBasedOnFilter();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            updateListBasedOnFilter();
        }

        private void btnEClearFilter_Click(object sender, RoutedEventArgs e)
        {
            dateInvoiceDate.SelectedDate = null;
            cbInvoiceNumber.SelectedItem = null;
            cbTotalCharge.SelectedItem = null;
         }
    }
}
