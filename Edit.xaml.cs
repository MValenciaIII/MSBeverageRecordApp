﻿using Microsoft.Win32;
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
        //public Records Reports { get; set; }
        //public MainWindow main = new();
        public RootObject c = new MainWindow.RootObject();
        public Records Reports = new MainWindow.Records();
        





        public Edit() {
            InitializeComponent();
        }


        //MainWindow main = new MainWindow();

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
        public void BtnSave_Click(object sender, RoutedEventArgs e) {

            //WHY WONT YOU RUN
            
            Reports.categoryName = txbCatName.Text;
            Reports.companyName = txbCompName.Text;
            Reports.model = txbModel.Text;
            Reports.serial = txbSerial.Text;
            Reports.purchase_date = txbPurchaseDate.Text;
            Reports.cost = double.Parse(txbCost.Text);
            Reports.locationName = txbLocation.Text;
            Reports.sub_location = txbSubLocation.Text;
            saveGrid();
            Close();
        }
        //save row
        public RootObject saveGrid() {
            //should take in root obj, update records
            for (int i = 0; i < c.Items.Count; i++) {
                if (c.Items[i].record_id == Reports.record_id) {
                    c.Items[i].categoryName = Reports.categoryName;
                    c.Items[i].companyName = Reports.companyName;
                    c.Items[i].model = Reports.model;
                    c.Items[i].serial = Reports.serial;
                    c.Items[i].purchase_date = Reports.purchase_date;
                    c.Items[i].cost = Reports.cost;
                    c.Items[i].locationName = Reports.locationName;
                    c.Items[i].sub_location = Reports.sub_location;
                }
            }
            return c;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

    }
}
