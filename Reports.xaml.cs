using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MSBeverageRecordApp.MainWindow;
using Microsoft.Win32;
using System.Text.Json;


namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page {
        //GLOBAL VARIABLE
        RootObject deserializeObject = new RootObject();
        public List<Records> allReports;
        public class urlResult {
            public string[] results { get; set; }
        }//end class

        public Page1() {

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
                CreateLocationFilterItems(deserializeObject);

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

        public class CategoryReport {
            public string categoryName { get; set; }
            public string companyName { get; set; }
            public string serial { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }
        }
        //essentially that ^


        //CALLING THIS AS PARENT OBJECT HOLDER 
        public class RootObject {
            public int id { get; set; }
            public List<Records> Items { get; set; }
        }//end class

        public List<Records> CreateReports() {
            //INITIALIZE NEW INSTANCE OF LIST
            List<Records> allReports = new List<Records>();
            var recordItem = deserializeObject.Items;

            //LOOP THROUGH EACH ITEM IN RECORDS
            foreach (var record in recordItem) {

                //CREATE CATEGORY REPORT 
                var categoryReport = new Records {
                    categoryName = record.categoryName,
                    companyName = record.companyName,
                    serial = record.serial,
                    locationName = record.locationName,
                    sub_location = record.sub_location
                };//end var categoryReport

                //CREATE MANUFACTURER REPORT
                var manufacturerReport = new Records {
                    companyName = record.companyName,
                    serial = record.serial,
                    locationName = record.locationName,
                    sub_location = record.sub_location
                };//end var manufacturerReport

                //CREATE LOCATION REPORT
                var locationReport = new Records {
                    locationName = record.locationName,
                    sub_location = record.sub_location,
                    companyName = record.companyName,
                    serial = record.serial
                };//end var locationReport

                //CREATE EQUIPMENT REPORT
                var equipmentReport = new Records {
                    categoryName = record.categoryName,
                    companyName = record.companyName,
                    cost = record.cost
                };//end var equipmentReport

                //ADD EACH REPORT TO ALL REPORTS
                allReports.Add(categoryReport);
                allReports.Add(manufacturerReport);
                allReports.Add(locationReport);
                allReports.Add(equipmentReport);

                consoleOutput.Text = allReports.ToString();

            }//end foreach
            return allReports;
        }//end function

        private void Reports_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //SAVE SELECTED COMBOBOX CATEGORY TO A STRING
            string selectedCategory = Filter.SelectedItem.ToString();

            // Create a new list to store filtered records
            List<Records> filteredReports = new List<Records>();

            for (int index = 0; index < filteredReports.Count; index++) {
                //CHECK IF SELECTED CATEGORY IS ALL CATEGORIES(DEFAULT)
                if (selectedCategory == "All Categories") {
                    //SAVE ALL REPORTS TO FILTER REPORTS
                    //should show all

                    //CHECK IF SELECTED CATEGORY IS CATEGORY REPORT
                } else if (selectedCategory == "CategoryReport") {//    not working v need to use report name
                    filteredReports = allReports.FindAll(report => report.categoryName == selectedCategory);

                    //CHECK IF SELECTED CATEGORY IS MANUFACTURER REPORT
                } else if (selectedCategory == "ManufacturerReport") {//not working v
                    filteredReports = allReports.FindAll(report => report.companyName == selectedCategory);

                    //CHECK IF SELECTED CATEGORY IS LOCATION REPORT
                } else if (selectedCategory == "LocationReport") {//    not working v
                    filteredReports = allReports.FindAll(report => report.locationName == selectedCategory);

                    //    //CHECK IF SELECTED CATEGORY IS CATEGORY REPORT
                    //} else if (selectedCategory == "EquipmentReport") {
                    //    recordsArray[index] = allReports.FindAll(report => report.equipmentReport == selectedCategory);
                }//end if

            }//end for

            //SAVE DATAGRID ITEM SOURCE TO NEW FILTERED REPORTS
            MSBeverageRecordApp.ItemsSource = filteredReports;
        }//end function

        #region FILTER FUNCTIONS
        public static List<Records> FilterHotspotRecords(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IN THE COMBO BOX IS EQUAL TO ALL CATEGORIES
            if (filter.SelectedItem.ToString() != "All Categories") {
                //IF NOT ALL CATEGORIES, FILTER RECORDS BY CATEGORY NAME IN COMBO BOX
                return records.FindAll(record => record.categoryName == filter.SelectedItem);
            } else {
                //IF ALL CATEGORIES, RETURN ALL RECORDS WITHOUT FILTERING
                return records;
            }//end if
        }//end function

        public void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //GET DESERIALIZED OBJECT ITEMS AND SAVE TO VAR RECORD
            var record = deserializeObject.Items;
            //CALL FILTERHOTSPOT RECORDS AND SAVE TO ITEMSOURCE
            MSBeverageRecordApp.ItemsSource = FilterHotspotRecords(record, Filter);
        }//end function
        private void CreateLocationFilterItems(RootObject list) {
            bool contains = false;

            //ADD COMBO BOX ITEM 'ALL CATEGORIES' TO FILTER COMBO BOX
            Filter.Items.Add("All Categories");

            //LOOP THROUGH RECORD LIST ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER COMBO BOX ITEMS
                for (int itemIndex = 0; itemIndex < Filter.Items.Count; itemIndex++)
                    //CHECK IF THE FILTER ITEM MATCHES THE LIST ITEM
                    if (Filter.Items[itemIndex].ToString() == list.Items[index].categoryName) {
                        //IF IT DOES, MOVE TO NEXT ITEM
                        contains = true;
                    }//end if
                     //IF LIST ITEM DOES NOT EQUAL FILTER ITEM, ADD CATEGORY TO THE FILTER BOX
                if (contains == false) {
                    Filter.Items.Add(list.Items[index].categoryName);
                }//end if
            }//end for
        }//end function

        #endregion FILTER FUNCTIONS
        private void addRecord(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("CreateRecord.xaml", UriKind.Relative)); 
        }//end function

        private void muiSave_Click(object sender, RoutedEventArgs e) {
            //create a save file dialog object
            SaveFileDialog sfdSave = new SaveFileDialog();

            //open the dialog and wait for the user to make a selection
            bool fileSelected = (bool)sfdSave.ShowDialog();

            //if (fileSelected == true) {
            //    WriteTextToFile(sfdSave.FileName, txtMain.Text);
            //}//end if
        }//end event

        private void btnAddCategory_Click(object sender, RoutedEventArgs e) {

        }

        private void btnViewReports_Click(object sender, RoutedEventArgs e) {

        }

    }//end class

}//end namespace
