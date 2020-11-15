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
        private clsInvoice inv;

        public wndMain()
        {
            InitializeComponent();
            db = new clsDataAccess();
        }

        public void ReturnFromSearch(clsInvoice invoice)
        {
            Refresh();
            return;
        }

        private void Refresh()
        {
            return;
        }

        private void btnSearchForInvoice_Click(object sender, RoutedEventArgs e)
        {
            clsInvoice inv = this.inv;
            wndSearch s = new wndSearch(ref inv);
            _ = s.ShowDialog();
            ReturnFromSearch(inv);
        }

        private void btnEditAvailableItems_Click(object sender, RoutedEventArgs e)
        {
            wndItems i = new wndItems();
            _ = i.ShowDialog();
            Refresh();
        }
    }
} 
