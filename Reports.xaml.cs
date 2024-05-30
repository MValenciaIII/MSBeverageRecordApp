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

        //GLOBALS FOR PRINT
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
            LoadData();
        }

        private async void LoadData() {
            try {
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:4001/api/records/recordsreal");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode) {
                    var dataobjects = await response.Content.ReadAsStringAsync();
                    deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);
                    if (deserializeObject.Items != null) {
                        //ALL DATA
                        MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
                        allObj = deserializeObject;
                        allGrid.ItemsSource = allObj.Items;
                        //CATEGORY
                        MSBeverageRecordApp2.ItemsSource = deserializeObject.Items;
                        //MANUFACTURER
                        MSBeverageRecordApp3.ItemsSource = deserializeObject.Items;
                        //LOCATION
                        MSBeverageRecordApp4.ItemsSource = deserializeObject.Items;
                        //COST
                        if (deserializeObject.Items.Count > 0) {
                            var costReport = CreateCostReport(deserializeObject);
                            MSBeverageRecordApp5.ItemsSource = costReport;
                            totalGrid.ItemsSource = costReport;
                            totalObj = deserializeObject;
                            totalObj.CostItems = (List<TotalCostData>)totalGrid.ItemsSource;
                        }
                        // Initialize filters
                        CreateCategoryFilterItems(deserializeObject);
                        CreateManufacturerFilterItems(deserializeObject);
                        CreateLocationFilterItems(deserializeObject);
                    } else {
                        // Handle case where deserializedObject.Items is null
                        MessageBox.Show("Failed to deserialize items.");
                    }
                } else {
                    MessageBox.Show("Failed to retrieve data from API.");
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        //public Reports() {

        //    InitializeComponent();

        //    //SETTING UP NEW INSTANCE OF A TYPE OF DATA
        //    using HttpClient client = new();

        //    //GETTING QUERY API LINK FOR OBJECT DATA 
        //    client.BaseAddress = new Uri("http://localhost:4001/api/records/recordsreal");

        //    //ADD AN "ACCEPT" HEADER FOR JSON FORMAT
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));

        //    //THIS IS VARIABLE TO GET OBJECT DATA FROM API
        //    var response = client.GetAsync(client.BaseAddress).Result;

        //    //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
        //    if (response.IsSuccessStatusCode) {

        //        //CONVERTING OBJECT "RESPONSE" VARIABLE DATA TO STRING 
        //        var dataobjects = response.Content.ReadAsStringAsync().Result;

        //        //CHANGE OUR STRING TO OBJECT DATA
        //        deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);
        //        //ALL DATA
        //        MSBeverageRecordApp.ItemsSource = deserializeObject.Items;

        //        allObj = deserializeObject;
        //        allGrid.ItemsSource = allObj.Items;

        //        //CATEGORY
        //        MSBeverageRecordApp2.ItemsSource = deserializeObject.Items;

        //        //catObj = deserializeObject;
        //        //catGrid.ItemsSource = catObj.Items;

        //        //MANUFACTURER
        //        MSBeverageRecordApp3.ItemsSource = deserializeObject.Items;

        //        //manuObj = deserializeObject;
        //        //manuGrid.ItemsSource = manuObj.Items;

        //        //LOCATION
        //        MSBeverageRecordApp4.ItemsSource = deserializeObject.Items;

        //        //locObj = deserializeObject;
        //        //locGrid.ItemsSource = locObj.Items;

        //        //TOTAL VALUE
        //        //CALL COMBO BOX FUNCTIONS

        //        CreateCategoryFilterItems(deserializeObject);
        //        CreateManufacturerFilterItems(deserializeObject);
        //        CreateLocationFilterItems(deserializeObject);


        //        //COST
        //        if (deserializeObject.Items.Count > 0) {
        //            MSBeverageRecordApp5.ItemsSource = CreateCostReport(deserializeObject);
        //            totalGrid.ItemsSource = CreateCostReport(totalObj);
        //            totalObj = deserializeObject;
        //            totalObj.CostItems = (List<TotalCostData>)totalGrid.ItemsSource;
        //        }
        //        #region GridRowCustom
        //        MSBeverageRecordApp.CanUserAddRows = false;
        //        MSBeverageRecordApp2.CanUserAddRows = false;
        //        MSBeverageRecordApp3.CanUserAddRows = false;
        //        MSBeverageRecordApp4.CanUserAddRows = false;
        //        MSBeverageRecordApp5.CanUserAddRows = false;
        //        #endregion GridRowCustom
        //    }//end if
        //}//end main

        #region print calls

        private void muiPrint_Click(object sender, RoutedEventArgs e) {

            //"PrintDG" CLASS IS RESPONSIBLE FOR THE ACTUAL PRINTING PROCESS

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
            }//end switch
        } //end function
        #endregion


        #region save calls

        private void muiSavePDF_Click(object sender, RoutedEventArgs e) {


            SaveReport s = new SaveReport();

            //call current report filter
            switch (filterName) {
                case "allData":

                    title = $"report    {DateTime.UtcNow.ToString("d")}";
                    s.ExportToPdf(allObj, MSBeverageRecordApp, title, filterName);
                    //p.printDG(allObj, MSBeverageRecordApp, title, filterName);
                    break;
                case "category":
                    title = $"category report    {DateTime.UtcNow.ToString("d")}";
                    s.ExportToPdf(catObj, MSBeverageRecordApp2, title, filterName);
                    //p.printDG(catObj, MSBeverageRecordApp2, title, filterName);
                    //s.ExportToPdf(catGrid);
                    break;
                case "manufacturer":
                    s.ExportToPdf(manuObj, MSBeverageRecordApp3, title, filterName);
                    title = $"manufacturers    {DateTime.UtcNow.ToString("d")}";
                    //p.printDG(manuObj, MSBeverageRecordApp3, title, filterName);
                    //s.ExportToPdf(manuGrid);
                    break;
                case "location":
                    s.ExportToPdf(locObj, MSBeverageRecordApp4, title, filterName);
                    title = $"location report    {DateTime.UtcNow.ToString("d")}";
                    //p.printDG(locObj, MSBeverageRecordApp4, title, filterName);
                    //s.ExportToPdf(locGrid);
                    break;
                case "totalValue":
                    s.ExportToPdf(totalObj, MSBeverageRecordApp5, title, filterName);
                    title = $"cost report    {DateTime.UtcNow.ToString("d")}";
                    // p.printDG(totalObj, MSBeverageRecordApp5, title, filterName);
                    //s.ExportToPdf(totalGrid);
                    break;
            }//end switch

        }

        private void muiSaveCSV_Click(object sender, RoutedEventArgs e) {


            SaveReport s = new SaveReport();

            //call current report filter
            switch (filterName) {
                case "allData":

                    title = $"report    {DateTime.UtcNow.ToString("d")}";
                    s.ExportToCsv(allObj, MSBeverageRecordApp, title, filterName);
                    //p.printDG(allObj, MSBeverageRecordApp, title, filterName);
                    break;
                case "category":
                    title = $"category report    {DateTime.UtcNow.ToString("d")}";
                    //p.printDG(catObj, MSBeverageRecordApp2, title, filterName);
                    s.ExportToCsv(catObj, MSBeverageRecordApp2, title, filterName);
                    break;
                case "manufacturer":
                    title = $"manufacturers    {DateTime.UtcNow.ToString("d")}";
                    //p.printDG(manuObj, MSBeverageRecordApp3, title, filterName);
                    s.ExportToCsv(manuObj, MSBeverageRecordApp3, title, filterName);
                    break;
                case "location":
                    title = $"location report    {DateTime.UtcNow.ToString("d")}";
                    //p.printDG(locObj, MSBeverageRecordApp4, title, filterName);
                    s.ExportToCsv(locObj, MSBeverageRecordApp4, title, filterName);

                    break;
                case "totalValue":
                    title = $"cost report    {DateTime.UtcNow.ToString("d")}";
                    // p.printDG(totalObj, MSBeverageRecordApp5, title, filterName);
                    s.ExportToCsv(totalObj, MSBeverageRecordApp5, title, filterName);
                    break;
            }//end switch

        }
        #endregion

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
            public int is_deleted { get; set; }
            public bool isHidden { get; set; }

        }//end class

        //CALLING THIS AS PARENT OBJECT HOLDER 
        public class RootObject {
            public int id { get; set; }
            public List<Records> Items { get; set; }
            public List<TotalCostData> CostItems { get; set; }
        }//end class
        public class TotalCostData {
            public string categoryName { get; set; }
            public decimal cost { get; set; }
        }//end class

        #region Tab alldata

        //moved to crud window
        #region sub combobox search

        //private void comboboxSearch(RootObject deserializedObjectList) {
        //    //SET FILTERBY ITEM SOURCE TO DESERIALIZED ITEMS LIST
        //    Filterby.ItemsSource = deserializedObjectList.Items;

        //    //INITIALIZE FILTERBY WITH ARRAY OF ITEMS
        //    Filterby.ItemsSource = new string[] { "RecordID", "Category", "Manufacturer", "Model", "SerialNumber", "Location" };
        //}//end comboBoxSearch


        //public Predicate<object> GetFilter() {

        //    //CHECKS WHAT IS SELECTED ON FILTERBY 
        //    switch (Filterby.SelectedItem as string) {

        //        //IF RECORD ID IS SELECTED
        //        case "RecordID":
        //            //RETURN RECORD ID FILTER
        //            return RecordIDFilter;

        //        //IF CATEGORY IS SELECTED
        //        case "Category":
        //            //RETURN CATEGORY FILTER
        //            return CategoryFilter;

        //        //IF MANUFACTURER IS SELECTED
        //        case "Manufacturer":
        //            //RETURN MANUFACTURER FILTER
        //            return ManufacturerFilter;

        //        //IF MODEL IS SELECTED
        //        case "Model":
        //            //RETURN MODEL FILTER
        //            return ModelFilter;

        //        //IF SERIAL NUMBER IS SELECTED
        //        case "SerialNumber":
        //            //RETURN SERIAL NUMBER FILTER
        //            return SerialNumberFilter;

        //        //IF LOCATION IS SELECTED
        //        case "Location":
        //            //RETURN LOCATION FILTER
        //            return LocationFilter;

        //    }//end switch

        //    //IF NONE OF THE ABOVE ARE SELECTED, RETURN RECORDIDFILTER
        //    return RecordIDFilter;

        //}//end GetFilter


        //private bool RecordIDFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF RECORD ID IS IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT RECORD ID.
        //    return Filterobj.record_id.ToString().Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private bool CategoryFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF CATEGORY NAME IS IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT CATEGORY NAME.
        //    return Filterobj.categoryName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private bool ManufacturerFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF MANUFACTURER NAME IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT MANUFACTURER.
        //    return Filterobj.companyName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private bool ModelFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF MODEL IS IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT MODEL.
        //    return Filterobj.model.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private bool SerialNumberFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF SERIAL NUMBER IS IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT SERIAL NUMBER.
        //    return Filterobj.serial.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private bool LocationFilter(object obj) {
        //    //SET RECORDS CLASS TO FILTER OBJ
        //    var Filterobj = obj as Records;

        //    //CHECK IF LOCATION NAME IS IN RECORDS, IF TRUE, RETURN RECORDS WITH THAT LOCATION.
        //    return Filterobj.locationName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}//end function


        //private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e) {

        //    //CHECK IF TEXT BOX IS EMPTY
        //    if (FilterTextBox.Text == "") {
        //        //IF NO FILTER IS SELECTED
        //        MSBeverageRecordApp.Items.Filter = null;
        //        //SHOW PLACEHOLDER TEXT
        //        txtSearchPlaceholder.Visibility = System.Windows.Visibility.Visible;
        //    } else {
        //        //IF A FILTER IS SELECTED
        //        MSBeverageRecordApp.Items.Filter = GetFilter();
        //        //HIDE PLACEHOLDER TEXT
        //        txtSearchPlaceholder.Visibility = System.Windows.Visibility.Hidden;

        //    }//end if
        //}//end function


        //private void Filterby_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        //    //DYNAMICALLY FILTERS DATAGRID USING SELECTION IN GET FILTER FUNCTION
        //    MSBeverageRecordApp.Items.Filter = GetFilter();

        //}//end function


        #endregion sub combobox search

        #endregion Tab alldata

        #region Tab category


        public static List<Records> FilterHotspotRecordsCategory(List<Records> records, ComboBox filter) {
            List<Records> filteredRecords = new List<Records>();

            //null check
            if (filter == null || filter.SelectedItem == null) {
                return records;
            }


            // If the selected item is "All Categories", return all records
            if (filter.SelectedItem.ToString() == "All Categories") {
                return records;
            }

            string selectedCategory = filter.SelectedItem.ToString();
            Console.WriteLine($"Filtering records by category: {selectedCategory}");

            // Iterate through each record
            foreach (var record in records) {
                // Check if the categoryName matches the selected item
                if (record.categoryName == selectedCategory && record.is_deleted == 0) {
                    // If yes, add it to the filteredRecords list
                    filteredRecords.Add(record);
                    Console.WriteLine($"Adding record: {record}");
                }
            }

            // Return the filtered records
            return filteredRecords;
        }


        public void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Get the original list of records
            var originalRecords = deserializeObject.Items;

            // Create a copy of the original records for filtering
            var recordsCopy = new List<Records>(originalRecords);

            // Get the filtered records based on the selected category
            var filteredRecords = FilterHotspotRecordsCategory(recordsCopy, FilterCategory);

            // Update the ItemsSource of the grid with the filtered records
            MSBeverageRecordApp2.ItemsSource = filteredRecords;

            // Update the ItemsSource of the catGrid
            catGrid.ItemsSource = new List<Records>(filteredRecords);

            // Update the Items property of the catObj
            catObj.Items = new List<Records>(filteredRecords);

        }//end function


        private void CreateCategoryFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL CATEGORIES" TO FILTER
            FilterCategory.Items.Add("All Categories");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                contains = false;
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
            List<Records> filteredRecords = new List<Records>();

            //null check
            if (filter == null || filter.SelectedItem == null) {
                return records;
            }

            if (filter.SelectedItem == "All Manufacturers") {
                return records;
            }

            // Iterate through each record
            foreach (Records record in records) {
                // Check if the categoryName matches the selected item
                if (record.companyName == filter.SelectedItem.ToString()) {
                    // If yes, add it to the filteredRecords list
                    filteredRecords.Add(record);
                }
            }

            // Return the filtered records
            return filteredRecords;

        }//end function


        public void Filter_SelectionChangedManufacturer(object sender, SelectionChangedEventArgs e) {
            // Get the original list of records
            var originalRecords = deserializeObject.Items;

            // Create a copy of the original records for filtering
            var recordsCopy = new List<Records>(originalRecords);

            // Get the filtered records based on the selected category
            var filteredRecords = FilterHotspotRecordsManufacturer(recordsCopy, FilterManufacturer);

            // Update the ItemsSource of the grid with the filtered records
            MSBeverageRecordApp3.ItemsSource = filteredRecords;

            // Update the ItemsSource of the catGrid
            manuGrid.ItemsSource = new List<Records>(filteredRecords);

            // Update the Items property of the catObj
            manuObj.Items = new List<Records>(filteredRecords);
        }//end function


        private void CreateManufacturerFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            FilterManufacturer.Items.Add("All Manufacturers");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                contains = false;
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
            List<Records> filteredRecords = new List<Records>();

            //null check
            if (filter == null || filter.SelectedItem == null) {
                return records;
            }

            if (filter.SelectedItem == "All Locations") {
                return records;
            }

            // Iterate through each record
            foreach (Records record in records) {
                // Check if the categoryName matches the selected item
                if (record.locationName == filter.SelectedItem.ToString()) {
                    // If yes, add it to the filteredRecords list
                    filteredRecords.Add(record);
                }
            }

            // Return the filtered records
            return filteredRecords;

        }//end function


        public void Filter_SelectionChangedLocation(object sender, SelectionChangedEventArgs e) {
            // Get the original list of records
            var originalRecords = deserializeObject.Items;

            // Create a copy of the original records for filtering
            var recordsCopy = new List<Records>(originalRecords);

            // Get the filtered records based on the selected category
            var filteredRecords = FilterHotspotRecordsLocation(recordsCopy, FilterLocation);

            // Update the ItemsSource of the grid with the filtered records
            MSBeverageRecordApp4.ItemsSource = filteredRecords;

            // Update the ItemsSource of the catGrid
            locGrid.ItemsSource = new List<Records>(filteredRecords);

            // Update the Items property of the catObj
            locObj.Items = new List<Records>(filteredRecords);

        }//end function


        private void CreateLocationFilterItems(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //ADD STRING "ALL LOCATIONS" TO FILTER
            FilterLocation.Items.Add("All Locations");

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.Items.Count; index++) {
                contains = false;
                //LOOP THROUGH FILTER
                for (int itemIndex = 0; itemIndex < FilterLocation.Items.Count; itemIndex++)
                    //IF FILTER CONTAINS THE LIST ITEM
                    if (FilterLocation.Items[itemIndex].ToString() == list.Items[index].locationName) {
                        //SET CONTAINS TO TRUE, THE FILTER ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE FILTER DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD LOCATION NAME TO THE FILTER
                    FilterLocation.Items.Add(list.Items[index].locationName);
                }//end if
            }//end for

        }//end function


        #endregion Tab location


        #region Total Cost Report
        //create item for each category

        public static List<TotalCostData> CreateCostReport(RootObject list) {
              List<TotalCostData> data = new List<TotalCostData>();
            if (list.Items == null || list.Items.Count == 0) {
                return data;
            }

            // Add first category
            data.Add(new TotalCostData {
                categoryName = list.Items[0].categoryName,
                cost = 0
            });

            for (int index = 0; index < list.Items.Count; index++) {
                bool contains = false;
                for (int itemIndex = 0; itemIndex < data.Count; itemIndex++) {
                    if (data[itemIndex].categoryName == list.Items[index].categoryName) {
                        contains = true;
                    }
                }
                if (!contains) {
                    data.Add(new TotalCostData {
                        categoryName = list.Items[index].categoryName,
                        cost = 0
                    });
                }
            }

            decimal totalCosts = 0;
            for (int index = 0; index < list.Items.Count; index++) {
                for (int itemIndex = 0; itemIndex < data.Count; itemIndex++) {
                    if (data[itemIndex].categoryName == list.Items[index].categoryName && list.Items[index].is_deleted == 0) {
                        data[itemIndex].cost += list.Items[index].cost;
                    }
                }
                if (list.Items[index].is_deleted == 0) {
                    totalCosts += list.Items[index].cost;
                }
            }

            data.Add(new TotalCostData {
                categoryName = "Total Cost",
                cost = totalCosts
            });

            return data;

        }//end function



        #endregion

        #region tabcontrol testing
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (xalldata.IsSelected) {
                filterName = "allData";
                MSBeverageRecordApp.ItemsSource = allObj.Items; // Update grid for all data
            }
            if (xcategory.IsSelected) {
                filterName = "category";
                var filteredCategoryRecords = FilterHotspotRecordsCategory(deserializeObject.Items, FilterCategory);
                MSBeverageRecordApp2.ItemsSource = filteredCategoryRecords; // Update grid for category
                catGrid.ItemsSource = filteredCategoryRecords;
                catObj.Items = filteredCategoryRecords;
            }
            if (xmanufacturer.IsSelected) {
                filterName = "manufacturer";
                var filteredManufacturerRecords = FilterHotspotRecordsManufacturer(deserializeObject.Items, FilterManufacturer);
                MSBeverageRecordApp3.ItemsSource = filteredManufacturerRecords; // Update grid for manufacturer
                manuGrid.ItemsSource = filteredManufacturerRecords;
                manuObj.Items = filteredManufacturerRecords;
            }
            if (xlocation.IsSelected) {
                filterName = "location";
                var filteredLocationRecords = FilterHotspotRecordsLocation(deserializeObject.Items, FilterLocation);
                MSBeverageRecordApp4.ItemsSource = filteredLocationRecords; // Update grid for location
                locGrid.ItemsSource = filteredLocationRecords;
                locObj.Items = filteredLocationRecords;
            }
            if (xtotalvalue.IsSelected && deserializeObject.Items.Count > 0) {
                filterName = "totalValue";
                MSBeverageRecordApp5.ItemsSource = CreateCostReport(deserializeObject); // Update grid for total value
                totalGrid.ItemsSource = CreateCostReport(deserializeObject);
                totalObj.CostItems = (List<TotalCostData>)totalGrid.ItemsSource;
            }
        }

        #endregion tabcontrol testing


    }//end class
}//end namespace

