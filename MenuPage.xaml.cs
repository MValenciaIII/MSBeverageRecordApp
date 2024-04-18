using System.Windows;
using System.Windows.Controls;

namespace MSBeverageRecordApp {

    /// <summary>
    /// INTERACTION LOGIC FOR MenuPage.xaml
    /// </summary>

    public partial class MenuPage : Page {
        public MenuPage() {
            InitializeComponent();
        }//end MenuPage

        #region Button Event Functions
        private void btnAddCategory_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CategoryTable.xaml", UriKind.Relative));
        }//end event
        private void btnViewReports_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("Reports.xaml", UriKind.Relative));
        }//end event
        private void addRecord(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CreateRecord.xaml", UriKind.Relative));
        }//end event
        private void btnModifyDeleteRecord_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CrudWindow.xaml", UriKind.Relative));
        }//end event
        #endregion

    }//end class
}//end namespace
