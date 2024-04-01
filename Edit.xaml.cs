using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static MSBeverageRecordApp.MainWindow;

namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window {
        
        public Edit() {
            InitializeComponent();
        }

        public Records Reports { get; set; }

        public void ShowRecord(Records rep) {

            Reports = rep;
            txbCatName.Text = $"{Reports.categoryName}";
            txbCompName.Text = $"{Reports.companyName}";
            txbModel.Text = $"{Reports.model}";
            txbSerial.Text = $"{Reports.serial}";
            txbPurchaseDate.Text = $"{Reports.purchase_date}";
            txbCost.Text = $"{Reports.cost}";
            txbLocation.Text = $"{Reports.locationName}";
            txbSubLocation.Text = $"{Reports.sub_location}";
            Show();

        }

        //EDIT WINDOW SAVE
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
