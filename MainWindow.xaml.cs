﻿
﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Http;
//IMPORTING
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.CodeDom.Compiler;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
//TODO
//modify/update data grid 
//
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

//JAMIE WANTS/NOTES:
//ability to create update delete 


//a filter button possibly
//records is a column

//a category filter table

//WANTS TO ASSIGN ITS OWN RECORD NUMBER
//CREATE ONE DIGIT CATEGORY VIEW WITH NAME AND DESCRIPTION, DISPLAY/PRINT BY CATEGORY
//CALCULATE TOTAL COST FUNCTION IN REPORTS
//HE WANTS INQUIRIES BY CATEGORY OR LOCATION
//TOTAL VALUE BY CATEGORY/MANUFACTURER


namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for MenuPage.xaml
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

        }//end event
        #endregion

        private void addRecord(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CreateRecord.xaml", UriKind.Relative));
        }
    }//end class
}//end namespace