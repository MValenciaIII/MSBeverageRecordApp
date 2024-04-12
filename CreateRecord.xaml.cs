using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Controls;
using static MSBeverageRecordApp.Reports;
using System.Text.Json;

namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for CreateRecord.xaml
    /// </summary>
    public partial class CreateRecord : Page {
        //GLOBAL VARIABLE
        RootObject deserializeObject = new RootObject();
        public CreateRecord() {
            InitializeComponent();

            CategoryAPI();
            LocationAPI();
            ManufacturerAPI();

            CreateCategoryComboBox(deserializeObject);
            CreateLocationComboBox(deserializeObject);
            CreateManufacturerComboBox(deserializeObject);
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
        public class Category {
            public int category_id { get; set; }
            public string categoryName { get; set; }
        }//end class

        public class Location {
            public int location_id { get; set; }
            public string locationName { get; set; }
        }//end class

        public class Manufacturer {
            public int manufacturer_id { get; set; }
            public string companyName { get; set; }
        }//end class

        public class RootObject {
            public int id { get; set; }
            public List<Category> CategoryItems { get; set; }
            public List<Location> LocationItems { get; set; }
            public List<Manufacturer> ManufacturerItems { get; set; }
        }//end class
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }//end function

        #region Category
        private void CategoryAPI() {

            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/category");

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
                deserializeObject.CategoryItems = JsonSerializer.Deserialize<List<Category>>(dataobjects);

            }//end if
        }//end function
        private void CreateCategoryComboBox (RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.CategoryItems.Count; index++) {
                //LOOP THROUGH COMBOBOX
                for (int itemIndex = 0; itemIndex < cboCategory.Items.Count; itemIndex++)

                    //IF COMBOBOX CONTAINS THE LIST ITEM
                    if (cboCategory.Items[itemIndex].ToString() == list.CategoryItems[index].categoryName) {
                        //SET CONTAINS TO TRUE, THE COMBOBOX ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE COMBOBOX DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE COMBOBOX
                    cboCategory.Items.Add(list.CategoryItems[index].categoryName);
                }//end if

            }//end for
        }//end function

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            
        }//end function
        #endregion Category

        #region Location
        private void LocationAPI() {

            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/location");

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
                deserializeObject.LocationItems = JsonSerializer.Deserialize<List<Location>>(dataobjects);

                //SET THE QUERY DATA TO DATAGRID ELEMENT
                //MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
            }//end if
        }//end function
        private void CreateLocationComboBox(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.LocationItems.Count; index++) {
                //LOOP THROUGH COMBOBOX
                for (int itemIndex = 0; itemIndex < cboLocation.Items.Count; itemIndex++)

                    //IF COMBOBOX CONTAINS THE LIST ITEM
                    if (cboLocation.Items[itemIndex].ToString() == list.LocationItems[index].locationName) {
                        //SET CONTAINS TO TRUE, THE COMBOBOX ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE COMBOBOX DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE COMBOBOX
                    cboLocation.Items.Add(list.LocationItems[index].locationName);
                }//end if

            }//end for
        }//end function
        private void cboLocation_SelectionChanged(object sender, SelectionChangedEventArgs e) {
           
        }//end function
        #endregion Location

        #region Manufacturer
        private void ManufacturerAPI() {

            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/manufacturer");

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
                deserializeObject.ManufacturerItems = JsonSerializer.Deserialize<List<Manufacturer>>(dataobjects);

                //SET THE QUERY DATA TO DATAGRID ELEMENT
                //MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
            }//end if
        }
        private void CreateManufacturerComboBox(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.ManufacturerItems.Count; index++) {
                //LOOP THROUGH COMBOBOX
                for (int itemIndex = 0; itemIndex < cboManufacturer.Items.Count; itemIndex++)

                    //IF COMBOBOX CONTAINS THE LIST ITEM
                    if (cboManufacturer.Items[itemIndex].ToString() == list.ManufacturerItems[index].companyName) {
                        //SET CONTAINS TO TRUE, THE COMBOBOX ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE COMBOBOX DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE COMBOBOX
                    cboManufacturer.Items.Add(list.ManufacturerItems[index].companyName);
                }//end if

            }//end for
        }//end function
        private void cboManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            
        }//end function
        #endregion Manufacturer
    }//end class

}//end namespace
