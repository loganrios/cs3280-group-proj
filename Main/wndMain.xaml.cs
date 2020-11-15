using System.Windows;
using group_proj.Search;
using group_proj.Items;

namespace group_proj.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        private clsDataAccess db;
        private clsMainLogic ml;

        public wndMain()
        {
            InitializeComponent();
            db = new clsDataAccess();
        }

        private void btnSearchForInvoice_Click(object sender, RoutedEventArgs e)
        {
            wndSearch s = new wndSearch();
            this.Close();
            _ = s.ShowDialog();
        }

        private void btnEditAvailableItems_Click(object sender, RoutedEventArgs e)
        {
            wndItems i = new wndItems();
            this.Close();
            _ = i.ShowDialog();
        }
    }
} 
