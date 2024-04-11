using System.Text;
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
using System.Collections.ObjectModel;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using System.Windows.Navigation;

//sort filter team adding here
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Data;
using static MSBeverageRecordApp.MainWindow;

namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page {
        //GLOBAL VARIABLE
        RootObject deserializeObject = new RootObject();

        public List<Records> allReports;
        public class urlResult {
            public string[] results { get; set; }
        }//end class

        public Reports() {

            InitializeComponent();

            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/records/recordsreal");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "response" variable DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);

                //How to set the query data to the DATAGRID element.
                MSBeverageRecordApp.ItemsSource = deserializeObject.Items;

                MSBeverageRecordApp2.ItemsSource = MSBeverageRecordApp.ItemsSource;

                MSBeverageRecordApp3.ItemsSource = MSBeverageRecordApp.ItemsSource;

                MSBeverageRecordApp4.ItemsSource = MSBeverageRecordApp.ItemsSource;

                MSBeverageRecordApp5.ItemsSource = MSBeverageRecordApp.ItemsSource;

                CreateAllDataFilterItemsCat(deserializeObject);

                CreateAllDataFilterItemsManu(deserializeObject);

                CreateAllDataFilterItemsLoco(deserializeObject);

                CreateCategoryFilterItems(deserializeObject);

                CreateManufacturerFilterItems(deserializeObject);

                CreateLocationFilterItems(deserializeObject);

                comboboxSearch(deserializeObject);
            }//end if
        }//end main
        public class Records {
            public int record_id { get; set; }
            public string categoryName { get; set; }
            public string companyName { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public DateTime purchase_date { get; set; }
            public decimal cost { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }

        }//end class

        //CALLING THIS AS PARENT OBJECT HOLDER 
        public class RootObject {
            public int id { get; set; }
            public List<Records> Items { get; set; }
        }//end class
         //How to set the query data to the DATAGRID element.

               
        #region Tab alldata

        #region sub category

        public static List<Records> FilterHotspotRecordsAll(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Categories") {

                return records.FindAll(record => record.categoryName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChangedAllCat(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsAll(record, FilterCategoryAll);

        }//end function

        private void CreateAllDataFilterItemsCat(RootObject list) {

            bool contains = false;

            FilterCategoryAll.Items.Add("All Categories");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterCategoryAll.Items.Count; itemIndex++)

                    if (FilterCategoryAll.Items[itemIndex].ToString() == list.Items[index].categoryName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterCategoryAll.Items.Add(list.Items[index].categoryName);

                }//end if

            }//end for

        }//end for

        #endregion sub category

        #region sub manufacturer

        public static List<Records> FilterHotspotRecordsManufacturerAll(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Manufacturers") {

                return records.FindAll(record => record.companyName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChangedManufacturerAll(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsManufacturerAll(record, FilterManufacturerAll);

        }//end function

        private void CreateAllDataFilterItemsManu(RootObject list) {

            bool contains = false;

            FilterManufacturerAll.Items.Add("All Manufacturers");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterManufacturerAll.Items.Count; itemIndex++)

                    if (FilterManufacturerAll.Items[itemIndex].ToString() == list.Items[index].companyName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterManufacturerAll.Items.Add(list.Items[index].companyName);

                }//end if

            }//end for

        }//end for

        #endregion sub manufacturer

        #region sub location

        public static List<Records> FilterHotspotRecordsLocationAll(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Locations") {

                return records.FindAll(record => record.locationName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChangedLocationAll(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsLocationAll(record, FilterLocationAll);

        }//end function

        private void CreateAllDataFilterItemsLoco(RootObject list) {

            bool contains = false;

            FilterLocationAll.Items.Add("All Locations");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterLocationAll.Items.Count; itemIndex++)

                    if (FilterLocationAll.Items[itemIndex].ToString() == list.Items[index].locationName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterLocationAll.Items.Add(list.Items[index].locationName);

                }//end if

            }//end for

        }//end for

        #endregion sub location

        #region sub combobox search

        private void comboboxSearch(RootObject deserializedObjectList) {

            Filterby.ItemsSource = deserializedObjectList.Items;

            Filterby.ItemsSource = new string[] { "RecordID", "Category", "Manufacturer", "Model", "SerialNumber", "Location" };

            //Filterby.ItemsSource = typeof(Records).GetProperties().Select((o) => o.Name);

            //MSBeverageRecordApp.Items.Filter = RecordIDFilter;

        }//end comboBoxSearch

        public Predicate<object> GetFilter() {

            switch (Filterby.SelectedItem as string) {

                case "RecordID":

                    return RecordIDFilter;

                case "Category":

                    return CategoryFilter;

                case "Manufacturer":

                    return ManufacturerFilter;

                case "Model":

                    return ModelFilter;

                case "SerialNumber":

                    return SerialNumberFilter;

                case "Location":

                    return LocationFilter;

            }

            return RecordIDFilter;

        }//end GetFilter

        private bool RecordIDFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.record_id.ToString().Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool CategoryFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.categoryName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool ManufacturerFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.companyName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool ModelFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.model.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool SerialNumberFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.serial.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool LocationFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.locationName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (FilterTextBox.Text == null) {

                MSBeverageRecordApp.Items.Filter = null;

            } else {

                MSBeverageRecordApp.Items.Filter = GetFilter();

            }

        }

        private void Filterby_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            MSBeverageRecordApp.Items.Filter = GetFilter();

        }


        #endregion sub combobox search

        #endregion Tab alldata

        #region Tab category

        public static List<Records> FilterHotspotRecordsCategory(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Categories") {

                return records.FindAll(record => record.categoryName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp2.ItemsSource = FilterHotspotRecordsCategory(record, FilterCategory);

        }//end function

        private void CreateCategoryFilterItems(RootObject list) {

            bool contains = false;

            FilterCategory.Items.Add("All Categories");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterCategory.Items.Count; itemIndex++)

                    if (FilterCategory.Items[itemIndex].ToString() == list.Items[index].categoryName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterCategory.Items.Add(list.Items[index].categoryName);

                }//end if

            }//end for

        }//end for

        #endregion Tab category
        #region Tab manufacturer

        public static List<Records> FilterHotspotRecordsManufacturer(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Manufacturers") {

                return records.FindAll(record => record.companyName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChangedManufacturer(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp3.ItemsSource = FilterHotspotRecordsManufacturer(record, FilterManufacturer);

        }//end function

        private void CreateManufacturerFilterItems(RootObject list) {

            bool contains = false;

            FilterManufacturer.Items.Add("All Manufacturers");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterManufacturer.Items.Count; itemIndex++)

                    if (FilterManufacturer.Items[itemIndex].ToString() == list.Items[index].companyName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterManufacturer.Items.Add(list.Items[index].companyName);

                }//end if

            }//end for

        }//end for

        #endregion Tab manufacturer

        #region Tab location

        public static List<Records> FilterHotspotRecordsLocation(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Locations") {

                return records.FindAll(record => record.locationName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChangedLocation(object sender, SelectionChangedEventArgs e) {

            var record = deserializeObject.Items;

            MSBeverageRecordApp4.ItemsSource = FilterHotspotRecordsLocation(record, FilterLocation);

        }//end function

        private void CreateLocationFilterItems(RootObject list) {

            bool contains = false;

            FilterLocation.Items.Add("All Locations");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < FilterLocation.Items.Count; itemIndex++)

                    if (FilterLocation.Items[itemIndex].ToString() == list.Items[index].locationName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    FilterLocation.Items.Add(list.Items[index].locationName);

                }//end if

            }//end for

        }//end function

        #endregion Tab location

    }//end class

}//end namespace
