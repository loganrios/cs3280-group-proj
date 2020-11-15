using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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

        wndMain main;

        /// <summary>
        /// Constructor constructing things
        /// </summary>
        public wndSearch(ref clsInvoice inv)
        {
            try
            {
                InitializeComponent();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                classSearchLogic = new clsSearchLogic();
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// TODO Cant figure out why this is not filling the data grid for me
        /// </summary>
        public void fillDataGrid()
        {
            try
            {
                List<clsInvoice> testList = classSearchLogic.pullAllInvoice();

                dgResults.ItemsSource = testList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Controls delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //delete the item selected
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
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
                    main.ReturnFromSearch((clsInvoice)dgResults.SelectedItem);
                   
                    this.Close();
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
    }
}
