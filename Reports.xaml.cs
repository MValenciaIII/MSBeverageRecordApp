﻿using System.Net.Http;
//IMPORTING
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using ComboBox = System.Windows.Controls.ComboBox;

namespace MSBeverageRecordApp {
    /// <summary>
    /// INTERACTION LOGIC FOR Reports.xaml
    /// </summary>


    public partial class Reports : Page {
        //GLOBAL VARIABLE
        RootObject deserializeObject = new RootObject();
        string filterName = "allData";
        public List<Records> allReports;
        public class urlResult {
            public string[] results { get; set; }
        }//end class

        //globals for print
        RootObject allObj = new RootObject();
        DataGrid allGrid = new DataGrid();

        RootObject catObj = new RootObject();
        DataGrid catGrid = new DataGrid();

        RootObject manuObj = new RootObject();
        DataGrid manuGrid = new DataGrid();

        RootObject locObj = new RootObject();
        DataGrid locGrid = new DataGrid();

        RootObject totalObj = new RootObject();
        DataGrid totalGrid = new DataGrid();
        string title = "";
        public Reports() {

            InitializeComponent();

            //SETTING UP NEW INSTANCE OF A TYPE OF DATA
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4002/api/records/recordsreal");

            //ADD AN "ACCEPT" HEADER FOR JSON FORMAT
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "RESPONSE" VARIABLE DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);

                //SET THE QUERY DATA TO DATAGRID ELEMENT

                //all data
                MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
                allObj = deserializeObject;
                allGrid.ItemsSource = allObj.Items;

                //category
                MSBeverageRecordApp2.ItemsSource = MSBeverageRecordApp.ItemsSource;
                catObj = deserializeObject;
                catGrid.ItemsSource = catObj.Items; 

                //manufacturer
                MSBeverageRecordApp3.ItemsSource = MSBeverageRecordApp.ItemsSource;
                manuObj = deserializeObject;
                manuGrid.ItemsSource = manuObj.Items;

                //location
                MSBeverageRecordApp4.ItemsSource = MSBeverageRecordApp.ItemsSource;
                locObj = deserializeObject;
                locGrid.ItemsSource = locObj.Items;

                //total value
                MSBeverageRecordApp5.ItemsSource = MSBeverageRecordApp.ItemsSource;
                totalObj = deserializeObject;
                totalGrid.ItemsSource = manuObj.Items;

                CreateAllDataFilterItemsCat(deserializeObject);
                CreateAllDataFilterItemsManu(deserializeObject);
                CreateAllDataFilterItemsLoco(deserializeObject);
                CreateCategoryFilterItems(deserializeObject);
                CreateManufacturerFilterItems(deserializeObject);
                CreateLocationFilterItems(deserializeObject);
                comboboxSearch(deserializeObject);

            }//end if
        }//end main
        

        
        #region print calls
        //set filter name
        private void xtabitems_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (xalldata.IsSelected) {
                filterName = "allData";

            }
            if (xcategory.IsSelected) {
                filterName = "category";


            }
            if (xmanufacturer.IsSelected) {
                filterName = "manufacturer";

            }
            if (xlocation.IsSelected) {
                filterName = "location";

            }
            if (xtotalvalue.IsSelected) {
              
                filterName = "totalValue";
            }

           
        }//ef
        
        //TODO
        //print total cost on grid, do in printDG class
        //add updates to grid on comboBox changes
        private void muiPrint_Click(object sender, RoutedEventArgs e) {

            PrintDG p = new PrintDG();
            
            switch (filterName) {
                case "allData":
                    title = $"report    {DateTime.UtcNow.ToString("d")}";
                    p.printDG(allObj, MSBeverageRecordApp, title, filterName);
                    break;
                case "category":
                    title = $"category report    {DateTime.UtcNow.ToString("d")}";
                    p.printDG(catObj, MSBeverageRecordApp2, title, filterName);

                    break;
                case "manufacturer":
                    title = $"manufacturers    {DateTime.UtcNow.ToString("d")}";
                    p.printDG(manuObj, MSBeverageRecordApp3, title, filterName);
                    break;
                case "location":
                    title = $"location report    {DateTime.UtcNow.ToString("d")}";
                    p.printDG(locObj, MSBeverageRecordApp4, title, filterName);
                    break;
                case "totalValue":
                    title = $"cost report    {DateTime.UtcNow.ToString("d")}";
                    p.printDG(totalObj, MSBeverageRecordApp5, title, filterName);
                    break;
            }

        } //ef
        #endregion

        public class Records {
            public int record_id { get; set; }
            public string categoryName { get; set; }
            public string companyName { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public string purchase_date { get; set; }
            public decimal cost { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }

        }//end class


        //CALLING THIS AS PARENT OBJECT HOLDER 
        public class RootObject {
            public int id { get; set; }
            public List<Records> Items { get; set; }
        }//end class


        #region Tab alldata


        #region sub category


        public static List<Records> FilterHotspotRecordsAll(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL CATEGORIES
            if (filter.SelectedItem.ToString() != "All Categories") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES CATEGORY NAME
                return records.FindAll(record => record.categoryName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if

        }//end function


        public void Filter_SelectionChangedAllCat(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSALL FUNCTION
            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsAll(record, FilterCategoryAll);
            allGrid.ItemsSource = FilterHotspotRecordsAll(allObj.Items, FilterCategoryAll);
            allObj.Items = FilterHotspotRecordsAll(allObj.Items, FilterCategoryAll);

        }//end function


        private void CreateAllDataFilterItemsCat(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL CATEGORIES" TO FILTER
            FilterCategoryAll.Items.Add("All Categories");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterCategoryAll.Items.Count; itemIndex++)

                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterCategoryAll.Items[itemIndex].ToString() == list.Items[index].categoryName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE FILTER
                    FilterCategoryAll.Items.Add(list.Items[index].categoryName);
                }//end if

            }//end for
        }//end function

        #endregion sub category

        #region sub manufacturer

        public static List<Records> FilterHotspotRecordsManufacturerAll(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL MANUFACTURERS
            if (filter.SelectedItem.ToString() != "All Manufacturers") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES COMPANY NAME
                return records.FindAll(record => record.companyName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if

        }//end function


        public void Filter_SelectionChangedManufacturerAll(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSMANUFACTURERALL FUNCTION
            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsManufacturerAll(record, FilterManufacturerAll);
            manuGrid.ItemsSource = FilterHotspotRecordsAll(manuObj.Items, FilterCategoryAll);
            manuObj.Items = FilterHotspotRecordsAll(manuObj.Items, FilterCategoryAll);
        }//end function

        private void CreateAllDataFilterItemsManu(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL MANUFACTURERS" TO FILTER
            FilterManufacturerAll.Items.Add("All Manufacturers");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterManufacturerAll.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterManufacturerAll.Items[itemIndex].ToString() == list.Items[index].companyName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE FILTER
                    FilterManufacturerAll.Items.Add(list.Items[index].companyName);
                }//end if
            }//end for

        }//end function


        #endregion sub manufacturer


        #region sub location


        public static List<Records> FilterHotspotRecordsLocationAll(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL LOCATIONS
            if (filter.SelectedItem.ToString() != "All Locations") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES LOCATION NAME
                return records.FindAll(record => record.locationName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if

        }//end function


        public void Filter_SelectionChangedLocationAll(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSLOCATIONALL FUNCTION
            MSBeverageRecordApp.ItemsSource = FilterHotspotRecordsLocationAll(record, FilterLocationAll);
            locGrid.ItemsSource = FilterHotspotRecordsAll(locObj.Items, FilterCategoryAll);
            locObj.Items = FilterHotspotRecordsAll(locObj.Items, FilterCategoryAll);
        }//end function


        private void CreateAllDataFilterItemsLoco(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL LOCATIONS" TO FILTER
            FilterLocationAll.Items.Add("All Locations");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterLocationAll.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterLocationAll.Items[itemIndex].ToString() == list.Items[index].locationName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE FILTER
                    FilterLocationAll.Items.Add(list.Items[index].locationName);
                }//end if
            }//end for

        }//end function


        #endregion sub location


        #region sub combobox search


        private void comboboxSearch(RootObject deserializedObjectList) {
            Filterby.ItemsSource = deserializedObjectList.Items;

            Filterby.ItemsSource = new string[] { "RecordID", "Category", "Manufacturer", "Model", "SerialNumber", "Location" };
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

            }//end switch

            return RecordIDFilter;

        }//end GetFilter


        private bool RecordIDFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.record_id.ToString().Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private bool CategoryFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.categoryName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private bool ManufacturerFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.companyName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private bool ModelFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.model.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private bool SerialNumberFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.serial.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private bool LocationFilter(object obj) {

            var Filterobj = obj as Records;

            return Filterobj.locationName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }//end function


        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (FilterTextBox.Text == null) {

                MSBeverageRecordApp.Items.Filter = null;

            } else {

                MSBeverageRecordApp.Items.Filter = GetFilter();

            }//end if

        }//end function


        private void Filterby_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            MSBeverageRecordApp.Items.Filter = GetFilter();
            

        }//end function


        #endregion sub combobox search


        #endregion Tab alldata


        #region Tab category


        public static List<Records> FilterHotspotRecordsCategory(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL CATEGORIES
            if (filter.SelectedItem.ToString() != "All Categories") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES CATEGORY NAME
                return records.FindAll(record => record.categoryName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if



        }//end function


        public void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSCATEGORY FUNCTION
            MSBeverageRecordApp2.ItemsSource = FilterHotspotRecordsCategory(record, FilterCategory);
            catGrid.ItemsSource = MSBeverageRecordApp2.ItemsSource;

        }//end function


        private void CreateCategoryFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL CATEGORIES" TO FILTER
            FilterCategory.Items.Add("All Categories");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterCategory.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterCategory.Items[itemIndex].ToString() == list.Items[index].categoryName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE FILTER
                    FilterCategory.Items.Add(list.Items[index].categoryName);
                }//end if
            }//end for

        }//end function


        #endregion Tab category


        #region Tab manufacturer


        public static List<Records> FilterHotspotRecordsManufacturer(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL MANUFACTURERS
            if (filter.SelectedItem.ToString() != "All Manufacturers") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES COMPANY NAME
                return records.FindAll(record => record.companyName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if

        }//end function


        public void Filter_SelectionChangedManufacturer(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSMANUFACTURER FUNCTION
            MSBeverageRecordApp3.ItemsSource = FilterHotspotRecordsManufacturer(record, FilterManufacturer);
            manuGrid.ItemsSource = MSBeverageRecordApp3.ItemsSource;
        }//end function


        private void CreateManufacturerFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL MANUFACTURERS" TO FILTER
            FilterManufacturer.Items.Add("All Manufacturers");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterManufacturer.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterManufacturer.Items[itemIndex].ToString() == list.Items[index].companyName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE FILTER
                    FilterManufacturer.Items.Add(list.Items[index].companyName);
                }//end if
            }//end for

        }//end function


        #endregion Tab manufacturer


        #region Tab location


        public static List<Records> FilterHotspotRecordsLocation(List<Records> records, ComboBox filter) {
            //CHECK IF SELECTED ITEM IS NOT EQUAL TO ALL LOCATIONS
            if (filter.SelectedItem.ToString() != "All Locations") {
                //IF TRUE, RETURN FILTERED LIST OF RECORDS THAT MATCHES LOCATION NAME
                return records.FindAll(record => record.locationName == filter.SelectedItem);
            } else {
                //IF FALSE, RETURN ALL RECORDS WITHOUT FILTER
                return records;
            }//end if

        }//end function


        public void Filter_SelectionChangedLocation(object sender, SelectionChangedEventArgs e) {
            //SAVE DESERIALIZED OBJECT ITEMS TO RECORD
            var record = deserializeObject.Items;

            //SAVE ITEM SOURCE TO RETURN OF FILTERHOTSPOTRECORDSLOCATION FUNCTION
            MSBeverageRecordApp4.ItemsSource = FilterHotspotRecordsLocation(record, FilterLocation);
            locGrid.ItemsSource = MSBeverageRecordApp4.ItemsSource;
        }//end function


        private void CreateLocationFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL LOCATIONS" TO FILTER
            FilterLocation.Items.Add("All Locations");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterLocation.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterLocation.Items[itemIndex].ToString() == list.Items[index].locationName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    FilterLocation.Items.Add(list.Items[index].locationName);
                }//end if
            }//end for

        }//end function


        #endregion Tab location



        //set string value for filter name to pass to print function

    }//end class
}//end namespace
