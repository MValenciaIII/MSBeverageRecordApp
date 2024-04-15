using System.Windows;
using System.Windows.Controls;

namespace MSBeverageRecordApp {
    /// <summary>
    /// INTERACTION LOGIC FOR MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page {
        public MenuPage() {
            InitializeComponent();
        }
        #region Button Event Functions
        private void btnAddCategory_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CategoryTable.xaml", UriKind.Relative));
        }//end event

        private void btnViewReports_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("Reports.xaml", UriKind.Relative));
        }//end event
        #endregion

        private void addRecord(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CreateRecord.xaml", UriKind.Relative));
        }

        private void btnModifyDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("CrudWindow.xaml", UriKind.Relative));
        }
    }
}
