using Microsoft.Win32;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public RootObject c { get; set; }
       // public System.Windows.Window Owner { }

        //MainWindow main = new MainWindow();

        public void ShowRecord(Records rep, RootObject cereal) {
            c = cereal;
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
        public void BtnSave_Click(object sender, RoutedEventArgs e) {
            Reports.categoryName = txbCatName.Text;
            Reports.companyName = txbCompName.Text;
            Reports.model = txbModel.Text;
            Reports.serial = txbSerial.Text;
            Reports.purchase_date = txbPurchaseDate.Text;
            Reports.cost = double.Parse(txbCost.Text);
            Reports.locationName = txbLocation.Text;
            Reports.sub_location = txbSubLocation.Text;
            // saveGrid(c);

            //why no work with static
            //MainWindow.edit(c);
            //((MainWindow)System.Windows.Application.Current.MainWindow).edit(c);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        //save row
        public RootObject saveGrid(RootObject rep) {

            //should take in root obj, update records
            for (int i = 0; i < rep.Items.Count; i++) {
                if (rep.Items[i].record_id == Reports.record_id) {
                    rep.Items[i].categoryName = Reports.categoryName;
                    rep.Items[i].companyName = Reports.companyName;
                    rep.Items[i].model = Reports.model;
                    rep.Items[i].serial = Reports.serial;
                    rep.Items[i].purchase_date = Reports.purchase_date;
                    rep.Items[i].cost = Reports.cost;
                    rep.Items[i].locationName = Reports.locationName;
                    rep.Items[i].sub_location = Reports.sub_location;
                }
            }

            return rep;

        }
    }
}
