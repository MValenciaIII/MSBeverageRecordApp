﻿using System.Data;
using System.Net.Http;
//IMPORTING
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


//TODO
//

namespace MSBeverageRecordApp {

    public partial class CrudWindow : Page {

        //#region OBJ CONTAINERS

        ////THIS IS SETTING UP A PLACE TO STORE EACH OBJECT ATTRIBUTE INSIDE A LIST 
        //public class Records {
        //    public int record_id { get; set; }
        //    public string categoryName { get; set; }
        //    public string companyName { get; set; }
        //    public string model { get; set; }
        //    public string serial { get; set; }
        //    public DateTime purchase_date { get; set; }
        //    public double cost { get; set; }
        //    public string locationName { get; set; }
        //    public string sub_location { get; set; }
        //}//end class

        ////RECORDS CLASS FOR FK'S
        //public class PostRecords {
        //    public int record_id { get; set; }
        //    public int category { get; set; }
        //    public int manufacturer { get; set; }
        //    public string model { get; set; }
        //    public string serial { get; set; }
        //    public DateTime purchase_date { get; set; }
        //    public double cost { get; set; }
        //    public int location { get; set; }
        //    public string sub_location { get; set; }
        //}//end class

        ////main
        //public class RootObject {
        //    public int id { get; set; }

        //    public List<Records> Items { get; set; }
        //}//end class

        ////post obj
        //public class RootObj {
        //    public int id { get; set; }
        //    public PostRecords Items { get; set; }
        //}//end class


        //#region FK'S CLASSES
        //public class Category {
        //    public int id { get; set; }
        //    public string categoryName { get; set; }
        //}//end class
        //public class Manufacture {
        //    public int id { get; set; }
        //    public string companyName { get; set; }
        //}//end class
        //public class Location {
        //    public int ID { get; set; }
        //    public string locationName { get; set; }
        //}//end class

        ////category
        //public class Root {
        //    public int id { get; set; }

        //    public List<Category> Items { get; set; }
        //}//end class

        ////location
        //public class RootLoc {
        //    public int id { get; set; }

        //    public List<Location> Items { get; set; }
        //}//end class

        ////manufacturer
        //public class RootComp {
        //    public int id { get; set; }

        //    public List<Manufacture> Items { get; set; }
        //}//end class
        //#endregion

        //#endregion

        //#region GLOBAL VARIABLES
        ////GLOBAL VARIABLES
        //public RootObject deserializeObject = new RootObject();
        //Root root = new Root();
        //RootLoc rootLoc = new RootLoc();
        //RootComp rootComp = new RootComp();
        ////RootObj postObj = new RootObj();
        //#endregion


        //GLOBAL VARIABLE


        //RootObject deserializeObject = new RootObject();

        ////GLOBAL CLASS FOR DATA TO SEND TO API
        //class PostRecordsData {
        //    public int record_id { get; set; }
        //    public int category { get; set; }
        //    public int manufacturer { get; set; }
        //    public string model { get; set; }
        //    public string serial { get; set; }
        //    public DateTime purchase_date { get; set; }
        //    public decimal cost { get; set; }
        //    public int location { get; set; }
        //    public string sub_location { get; set; }
        //}//end class

        ////GLOBAL CLASS FOR RECORDS TABLE
        //public class Records {
        //    public int record_id { get; set; }
        //    public string categoryName { get; set; }
        //    public string companyName { get; set; }
        //    public string model { get; set; }
        //    public string serial { get; set; }
        //    public DateTime purchase_date { get; set; }
        //    public decimal cost { get; set; }
        //    public string locationName { get; set; }
        //    public string sub_location { get; set; }
        //}//end class

        ////GLOBAL CLASS FOR CATEGORY TABLE
        //public class Category {
        //    public int id { get; set; }
        //    public string categoryName { get; set; }
        //}//end class

        ////GLOBAL CLASS FOR LOCATION TABLE
        //public class Location {
        //    public int ID { get; set; }//*NOTE- LOCATION ID IS UPPERCASE!
        //    public string locationName { get; set; }
        //}//end class

        ////GLOBAL CLASS FOR MANUFACTURER TABLE
        //public class Manufacturer {
        //    public int id { get; set; }
        //    public string companyName { get; set; }
        //}//end class

        ////GLOBAL CLASS TO HOLD DATA
        //public class RootObject {
        //    public int id { get; set; }
        //    public List<Records> Items { get; set; }
        //    public List<Category> CategoryItems { get; set; }
        //    public List<Location> LocationItems { get; set; }
        //    public List<Manufacturer> ManufacturerItems { get; set; }
        //}//end class

        ////CLASS TO GET RESPONSE FROM API
        //class PostResponse {
        //    public int Id { get; set; }
        //}//end class


        //GLOBAL VARIABLE from crud window


        string file = "";
        string[] rows = new string[1];
        int colCount = 0;
        string c = "";
        double equipmentCost = 0.0;
        PostRecordsData post = new PostRecordsData();
        Records rep = new Records();
        RootObject deserializeObject = new RootObject();

        //GLOBAL CLASS FOR DATA TO SEND TO API
        class PostRecordsData {
            public int record_id { get; set; }
            public int category { get; set; }
            public int manufacturer { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public DateTime purchase_date { get; set; }
            public decimal cost { get; set; }
            public int location { get; set; }
            public string sub_location { get; set; }
        }//end class

        //GLOBAL CLASS FOR RECORDS TABLE
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

        //GLOBAL CLASS FOR CATEGORY TABLE
        public class Category {
            public int id { get; set; }
            public string categoryName { get; set; }
        }//end class

        //GLOBAL CLASS FOR LOCATION TABLE
        public class Location {
            public int ID { get; set; }//*NOTE- LOCATION ID IS UPPERCASE!
            public string locationName { get; set; }
        }//end class

        //GLOBAL CLASS FOR MANUFACTURER TABLE
        public class Manufacturer {
            public int id { get; set; }
            public string companyName { get; set; }
        }//end class

        //GLOBAL CLASS TO HOLD DATA
        public class RootObject {
            public int id { get; set; }
            public List<Records> Items { get; set; }
            public List<Category> CategoryItems { get; set; }
            public List<Location> LocationItems { get; set; }
            public List<Manufacturer> ManufacturerItems { get; set; }
        }//end class

        //CLASS TO GET RESPONSE FROM API
        class PostResponse {
            public int Id { get; set; }
        }//end class


        public CrudWindow() {
            InitializeComponent();
            //CALL API's FOR EACH TABLE IN DATABASE
            RecordsAPI();
            CategoryAPI();
            LocationAPI();
            ManufacturerAPI();

            //CALL CREATE COMBO BOX FUNCTIONS
            //RecordIDTextBox(deserializeObject);
            //CreateCategoryComboBox(deserializeObject);
            //CreateLocationComboBox(deserializeObject);
            //CreateManufacturerComboBox(deserializeObject);

            MSBeverageRecordGrid.ItemsSource = deserializeObject.Items;
        
        }//end main
    
        #region SAVE FUNCTIONS
        public void Saving(string filePath, string[] array, int num) {
            //VARIABLE
            int count = 0;
            //loop over rows and append lines
            for (int i = 0; i < array.Length; i++) {
                count++;
                System.IO.File.AppendAllText(file, array[i]);
                //START NEW LINE AFTER PRINTING EACH CELL IN A ROW
                count = 0;
            }//end for loop
        }//end function


        private void btnSave_Click(object sender, RoutedEventArgs e) {
            #region fromfile
            //rows array --> csv
            //var csv = new List<string[]>();
            //var lines = System.IO.File.ReadAllLines(@"C:\Users\MCA\source\repos\MSBeverageRecordApp\test.csv"); // csv file location

            //// loop through all lines and add it in list as string
            //foreach (string line in lines)
            //    csv.Add(line.Split(','));

            ////split string to get first line, header line as JSON properties
            //var properties = lines[0].Split(',');

            //var listObjResult = new List<Dictionary<string, string>>();

            ////loop all remaining lines, except header so starting it from 1
            //// instead of 0
            //var objResult = new Dictionary<string, string>();
            //for (int i = 1; i < lines.Length; i++) {
            //    if (i > 1) {
            //        objResult.Clear();
            //    }
            //    for (int j = 0; j < properties.Length; j++) {

            //        objResult.Add(properties[j], csv[i][j]);
            //        //clear duplicates maybe?
            //         listObjResult.Add(objResult);
            //    }
            //}
            //consoleOutput.Text = listObjResult[0]["id"].ToString(); 
            #endregion

            //FROM OBJECT
            #region from obj
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(deserializeObject.Items);
            System.IO.File.AppendAllText(@"C:\Users\MCA\source\repos\MSBeverageRecordApp\test2.txt", json);
            #endregion
        }//end event


        private void savepdf_Click(object sender, RoutedEventArgs e) {
            System.Windows.Controls.PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
        }//ef
        private void print_Click(object sender, RoutedEventArgs e) {

            //PrintDG print = new PrintDG();

            //print.printDG(deserializeObject ,MSBeverageRecordGrid, "Title");
        }
        public void savecsv_Click(object sender, RoutedEventArgs e) {

            //SAVE TO CSV
            //CREATE A SAVE FILE DIALOG OBJECT
            Microsoft.Win32.SaveFileDialog sfdSave = new Microsoft.Win32.SaveFileDialog();
            //OPEN THE DIALOG AND WAIT FOR THE USER TO MAKE A SELECTION
            //FIX
            sfdSave.InitialDirectory = @"C:\";
            sfdSave.Filter = "CSV file (*.csv)|*.csv|All Files (*.*)|*.*";
            bool fileSelected = (bool)sfdSave.ShowDialog();
            file = sfdSave.FileName;
            if (fileSelected == true) {
                #region CSV
                StringBuilder sb = new StringBuilder();
                decimal totalCost = 0.0m;
                string headerline = "id, category, company, model, serial, purchase date, cost, location, sub-location, ";
                string[] cols = headerline.Split(',');
                colCount = cols.Length;
                rows = new string[deserializeObject.Items.Count + 3];
                //SET HEADERS AS FIRST ROW
                rows[0] = headerline + "\n";
                //LOOP OVER LIST AND SET VALUES OF EACH ROW INTO AN ARRAY
                for (int i = 1; i <= deserializeObject.Items.Count; i++) {
                    //CLEAR STRING ON EACH ITERATION
                    sb.Clear();
                    //ADD VALUES
                    sb.Append(deserializeObject.Items[i - 1].record_id.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].categoryName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].companyName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].model.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].serial.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].purchase_date.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].cost.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].locationName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].sub_location.ToString() + "\n");
                    //GET TOTAL COST
                    totalCost += (decimal)deserializeObject.Items[i - 1].cost;
                    //remove trailing comma on last row
                    if (i == deserializeObject.Items.Count) {
                        sb.Remove(sb.Length - 2, 1);
                    }
                    //ASSIGN CURRENT STRING VALUE TO BE ONE ROW
                    rows[i] = sb.ToString();
                }

                consoleOutput.Text = $"{totalCost:C}";
                sb.Clear();
                sb.Append("\ntotal equipment cost: " + totalCost.ToString());
                rows[deserializeObject.Items.Count + 1] = sb.ToString();
                #endregion
                Saving(file, rows, colCount);
            }//end if
        }//ef
        #endregion

        //UPDATE TO THE DATABASE
        private void UpdateDataBase(RootObject list) {
            //invalid column names 
            post = new PostRecordsData {
                record_id = post.record_id,
                category = post.category,
                manufacturer = post.manufacturer,
                model = post.model,
                serial = post.serial,
                purchase_date = post.purchase_date,
                cost = post.cost,
                location = post.location,
                sub_location = post.sub_location
            };


            for (int itemIndex = 0; itemIndex < list.CategoryItems.Count; itemIndex++) {
                //IF COMBOBOX SELECTED VALUE IS EQUAL TO CATEGORY NAME AT INDEX
                if (cboCatName.SelectedValue == list.CategoryItems[itemIndex].categoryName) {
                    //SAVE ID TO POST DATA FOR CATEGORY
                    post.category = list.CategoryItems[itemIndex].id;
                }//end if
            }//end for 

            //LOOP THROUGH LOCATION TABLE ITEMS 
            for (int itemIndex = 0; itemIndex < list.LocationItems.Count; itemIndex++) {
                //IF COMBOBOX SELECTED VALUE IS EQUAL TO LOCATION NAME AT INDEX
                if (cboLocation.SelectedValue == list.LocationItems[itemIndex].locationName) {
                    //SAVE ID TO POST DATA FOR LOCATION
                    post.location = list.LocationItems[itemIndex].ID;
                }//end if
            }//end for

            //LOOP THROUGH MANUFACTURER TABLE ITEMS
            for (int itemIndex = 0; itemIndex < list.ManufacturerItems.Count; itemIndex++) {
                //IF COMBOBOX SELECTED VALUE IS EQUAL TO MANUFACTURER NAME AT INDEX
                if (cboManufacturer.SelectedValue == list.ManufacturerItems[itemIndex].companyName) {
                    //SAVE ID TO POST DATA FOR MANUFACTURER
                    post.manufacturer = list.ManufacturerItems[itemIndex].id;
                }//end if
            }

                //HTTP CLIENT INSTANCE
                var client = new HttpClient();
            //CONNECTION URL
            client.BaseAddress = new Uri("http://localhost:4001/api/records/modifyid");
            var json = JsonSerializer.Serialize(post);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //NO STATUS CODE?
            var response = client.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<Records>(responseContent);
            } else {
                System.Windows.MessageBox.Show("Error " + response.StatusCode);
            }
        }//end function

        #region MODIFY RECORDS EVENT
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

            CreateCategoryComboBox(deserializeObject);
            CreateLocationComboBox(deserializeObject);
            CreateManufacturerComboBox(deserializeObject);

            //selects the row that user double clicks

            Records Reports = new Records();
            var row = sender as DataGridRow;
            rep = row.DataContext as Records;

            //FILLS EDIT WINDOW VALUES
            Reports = rep;
            cboCatName.Text = $"{rep.categoryName}";
            cboManufacturer.Text = $"{rep.companyName}";
            txbModel.Text = $"{rep.model}";
            txbSerial.Text = $"{rep.serial}";
            txbPurchaseDate.Text = $"{rep.purchase_date.ToString("d")}";
            txbCost.Text = $"{rep.cost}";
            cboLocation.Text = $"{rep.locationName}";
            txbSubLocation.Text = $"{rep.sub_location}";

            //SWITCH VIEWS TO EDIT WINDOW
            spLabels.Visibility = Visibility.Visible;
            spText.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;

            MSBeverageRecordGrid.Visibility = Visibility.Collapsed;
            consoleOutput.Visibility = Visibility.Collapsed;
            fileMenu.Visibility = Visibility.Collapsed;
            Filter.Visibility = Visibility.Collapsed;




        }//end event


        private void btnSaveChange_Click(object sender, RoutedEventArgs e) {


            Records Reports = rep;
            //CALL UPDATE FUNCTION WHEN USER SAVES
          
            for (int i = 0; i < deserializeObject.Items.Count; i++) {

                if (deserializeObject.Items[i].record_id == Reports.record_id) {

                    post.record_id = Reports.record_id;
                    //post.manufacturer = deserializeObject.ManufacturerItems[i].id;
                    //post.category = deserializeObject.CategoryItems[i].id;
                    post.model = txbModel.Text;
                    post.serial = txbSerial.Text;
                    post.purchase_date = DateTime.Parse(txbPurchaseDate.Text);
                    post.cost = decimal.Parse(txbCost.Text);
                    //post.location = deserializeObject.LocationItems[i].ID;
                    post.sub_location = txbSubLocation.Text;

                    deserializeObject.Items[i].categoryName = cboCatName.Text;
                    deserializeObject.Items[i].companyName = cboManufacturer.Text;
                    deserializeObject.Items[i].model = txbModel.Text;
                    deserializeObject.Items[i].serial = txbSerial.Text;
                    deserializeObject.Items[i].purchase_date = DateTime.Parse(txbPurchaseDate.Text);
                    deserializeObject.Items[i].cost = decimal.Parse(txbCost.Text);
                    deserializeObject.Items[i].locationName = cboLocation.Text;
                    deserializeObject.Items[i].sub_location = txbSubLocation.Text;
                }

                //doesn't work
                //if (cboCatName.Text == deserializeObject.Items[i].categoryName) {
                //    for (int j = 0; j < deserializeObject.CategoryItems.Count; j++) {
                //        if (i > j) {
                //            break;
                //        }
                //        post.category = deserializeObject.CategoryItems[i].id;
                //        if(post.category == deserializeObject.CategoryItems[i].id) {
                //            break;
                //        }
                //    }
                //}

                //if (cboManufacturer.Text == deserializeObject.Items[i].companyName) {

                //    for (int j = 0; j < deserializeObject.ManufacturerItems.Count; j++) {
                //        if (i > j) {
                //            break;
                //        }
                //        post.manufacturer = deserializeObject.ManufacturerItems[i].id;
                //        if (post.manufacturer == deserializeObject.ManufacturerItems[i].id) {
                //            break;
                //        }
                //    }
                //}

                //if (cboLocation.Text == deserializeObject.Items[i].locationName) {
                //    for (int j = 0; j < deserializeObject.LocationItems.Count; j++) {
                //        if (i > j) {
                //            break;
                //        }
                //        post.location = deserializeObject.LocationItems[i].ID;
                //        if (post.location == deserializeObject.LocationItems[i].ID) {
                //            break;
                //        }
                //    }
                //}



                MSBeverageRecordGrid.ItemsSource = null;
                MSBeverageRecordGrid.ItemsSource = deserializeObject.Items;
                
            }//end for loop
             //CALL UPDATE FUNCTION WHEN USER SAVES
            UpdateDataBase(deserializeObject);



            //SWITCH VIEWS
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
        }//end event function

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
        }//end function
        #endregion

        //#region API PULL FROM DATABASE
        //public void Tables() {

        //    //SETTING UP NEW INSTANCE OF A TYPE OF DATA
        //    using HttpClient client = new();
        //    //GETTING QUERY API LINK FOR OBJECT DATA 
        //    client.BaseAddress = new Uri("http://localhost:4001/api/category");

        //    //ADD AN "Accept" HEADER FOR JSON FORMAT.
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));
        //    //THIS IS VARIABLE TO GET OBJECT DATA FROM API

        //    var response = client.GetAsync(client.BaseAddress).Result;

        //    //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
        //    if (response.IsSuccessStatusCode) {
        //        ////CONVERTING OBJECT "response" variable DATA TO STRING 
        //        string dataobjects = response.Content.ReadAsStringAsync().Result;

        //        //NEED ACCESS GLOBALLY TO 
        //        root.Items = JsonSerializer.Deserialize<List<Category>>(dataobjects);
        //        CreateCategoryFilterItems(root);
        //    }//end if statusOK
        //}//end Tables function


        //public void Locations() {
        //    //SETTING UP NEW instance of a type of data
        //    using HttpClient client = new();
        //    //GETTING QUERY API LINK FOR OBJECT DATA 
        //    client.BaseAddress = new Uri("http://localhost:4001/api/location");

        //    //ADD AN "Accept" HEADER FOR JSON FORMAT.
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));
        //    //THIS IS VARIABLE TO GET OBJECT DATA FROM API

        //    var response = client.GetAsync(client.BaseAddress).Result;

        //    //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
        //    if (response.IsSuccessStatusCode) {
        //        ////CONVERTING OBJECT "response" variable DATA TO STRING 
        //        string dataobjects = response.Content.ReadAsStringAsync().Result;

        //        //NEED ACCESS GLOBALLY TO 
        //        rootLoc.Items = JsonSerializer.Deserialize<List<Location>>(dataobjects);
        //        CreateLocationFilterItems(rootLoc);
        //    }//end if statusOK
        //}//end Locations function


        //public void Companies() {
        //    //SETTING UP NEW instance of a type of data
        //    using HttpClient client = new();
        //    //GETTING QUERY API LINK FOR OBJECT DATA 
        //    client.BaseAddress = new Uri("http://localhost:4001/api/manufacturer");

        //    //ADD AN "Accept" HEADER FOR JSON FORMAT.
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));
        //    //THIS IS VARIABLE TO GET OBJECT DATA FROM API

        //    var response = client.GetAsync(client.BaseAddress).Result;

        //    //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
        //    if (response.IsSuccessStatusCode) {
        //        ////CONVERTING OBJECT "response" variable DATA TO STRING 
        //        string dataobjects = response.Content.ReadAsStringAsync().Result;

        //        //NEED ACCESS GLOBALLY TO 
        //        rootComp.Items = JsonSerializer.Deserialize<List<Manufacture>>(dataobjects);
        //        CreateCompanyFilterItems(rootComp);
        //    }
        //}
        //#endregion

        //#region THE COMBOBOX FILL UP
        //private void CreateLocationFilterItems(RootLoc list) {

        //    bool contains = false;

        //    txbLocation.Items.Add("none");

        //    for (int index = 0; index < list.Items.Count; index++) {

        //        for (int itemIndex = 0; itemIndex < txbLocation.Items.Count; itemIndex++)

        //            if (txbLocation.Items[itemIndex].ToString() == list.Items[index].locationName) {

        //                contains = true;

        //            }//end if

        //        if (contains == false) {

        //            txbLocation.Items.Add(list.Items[index].locationName);

        //        }//end if

        //    }//end for loop
        //}//end function


        //private void CreateCompanyFilterItems(RootComp list) {

        //    bool contains = false;

        //    txbCompName.Items.Add("none");

        //    for (int index = 0; index < list.Items.Count; index++) {

        //        for (int itemIndex = 0; itemIndex < txbCompName.Items.Count; itemIndex++)

        //            if (txbCompName.Items[itemIndex].ToString() == list.Items[index].companyName) {

        //                contains = true;

        //            }//end if

        //        if (contains == false) {

        //            txbCompName.Items.Add(list.Items[index].companyName);

        //        }//end if

        //    }//end for loop
        //}//end function


        //private void CreateCategoryFilterItems(Root list) {

        //    bool contains = false;


        //    txbCatName.Items.Add("none");

        //    for (int index = 0; index < list.Items.Count; index++) {

        //        for (int itemIndex = 0; itemIndex < txbCatName.Items.Count; itemIndex++)

        //            if (txbCatName.Items[itemIndex].ToString() == list.Items[index].categoryName) {

        //                contains = true;

        //            }//end if

        //        if (contains == false) {

        //            txbCatName.Items.Add(list.Items[index].categoryName);

        //        }//end if

        //    }//end for loop
        //}//end function


        //#endregion
        private void RecordsAPI() {

            //SETTING UP NEW INSTANCE OF A TYPE OF DATA
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/records/recordsreal");

            //ADD AN "Accept" HEADER FOR JSON FORMAT.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE "response" VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "response" VARIABLE DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);

            }//end if
        }//end function

        private void CategoryAPI() {

            //SETTING UP NEW INSTANCE OF A TYPE OF DATA
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/category");

            //ADD AN "Accept" HEADER FOR JSON FORMAT
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE "response" VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "response" VARIABLE DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.CategoryItems = JsonSerializer.Deserialize<List<Category>>(dataobjects);

            }//end if
        }//end function

        private void LocationAPI() {

            //SETTING UP NEW INSTANCE OF A TYPE OF DATA
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/location");

            //ADD AN "Accept" HEADER FOR JSON FORMAT
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE "response" VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "response" VARIABLE DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.LocationItems = JsonSerializer.Deserialize<List<Location>>(dataobjects);

            }//end if
        }//end function

        private void ManufacturerAPI() {

            //SETTING UP NEW INSTANCE OF A TYPE OF DATA
            using HttpClient client = new();

            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4001/api/manufacturer");

            //ADD AN "Accept" HEADER FOR JSON FORMAT
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE "response" VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {

                //CONVERTING OBJECT "response" VARIABLE DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;

                //CHANGE OUR STRING TO OBJECT DATA
                deserializeObject.ManufacturerItems = JsonSerializer.Deserialize<List<Manufacturer>>(dataobjects);

            }//end if
        }//end function

        private void CreateCategoryComboBox(RootObject list) {
            //INITIALIZE BOOL TO FALSE
            bool contains = false;

            //LOOP THROUGH LIST OF ITEMS
            for (int index = 0; index < list.CategoryItems.Count; index++) {
                //LOOP THROUGH COMBOBOX
                for (int itemIndex = 0; itemIndex < cboCatName.Items.Count; itemIndex++)

                    //IF COMBOBOX CONTAINS THE LIST ITEM
                    if (cboCatName.Items[itemIndex].ToString() == list.CategoryItems[index].categoryName) {
                        //SET CONTAINS TO TRUE, THE COMBOBOX ALREADY HAS THE LIST ITEM
                        contains = true;
                    }//end if

                //IF THE COMBOBOX DOES NOT CONTAIN THE LIST ITEM
                if (contains == false) {
                    //ADD THE LIST ITEM TO THE COMBOBOX
                    cboCatName.Items.Add(list.CategoryItems[index].categoryName);
                }//end if

            }//end for
        }//end fu

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

        private void CreateManufacturerComboBox(RootObject list) {
            PostRecordsData manufacturer = new PostRecordsData();

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



        //private void PostNewRecords(RootObject list) {
        //    //RETRIEVE ALL INPUTS FROM CREATE RECORD PAGE
        //    var postData = new PostRecordsData {
        //        record_id = Convert.ToInt32(recordNumber.Text),
        //        model = txtModel.Text.ToUpper(),
        //        serial = txtSerialNumber.Text.ToUpper(),
        //        purchase_date = PurchaseDate.SelectedDate.Value,
        //        cost = decimal.Parse(txtCost.Text),
        //        sub_location = txtSubLocation.Text.ToUpper()
        //    };//end var postData

        //    //LOOP THROUGH CATEGORY TABLE ITEMS
        //    for (int itemIndex = 0; itemIndex < list.CategoryItems.Count; itemIndex++) {
        //        //IF COMBOBOX SELECTED VALUE IS EQUAL TO CATEGORY NAME AT INDEX
        //        if (cboCategory.SelectedValue == list.CategoryItems[itemIndex].categoryName) {
        //            //SAVE ID TO POST DATA FOR CATEGORY
        //            postData.category = list.CategoryItems[itemIndex].id;
        //        }//end if
        //    }//end for 

        //    //LOOP THROUGH LOCATION TABLE ITEMS 
        //    for (int itemIndex = 0; itemIndex < list.LocationItems.Count; itemIndex++) {
        //        //IF COMBOBOX SELECTED VALUE IS EQUAL TO LOCATION NAME AT INDEX
        //        if (cboLocation.SelectedValue == list.LocationItems[itemIndex].locationName) {
        //            //SAVE ID TO POST DATA FOR LOCATION
        //            postData.location = list.LocationItems[itemIndex].ID;
        //        }//end if
        //    }//end for

        //    //LOOP THROUGH MANUFACTURER TABLE ITEMS
        //    for (int itemIndex = 0; itemIndex < list.ManufacturerItems.Count; itemIndex++) {
        //        //IF COMBOBOX SELECTED VALUE IS EQUAL TO MANUFACTURER NAME AT INDEX
        //        if (cboManufacturer.SelectedValue == list.ManufacturerItems[itemIndex].companyName) {
        //            //SAVE ID TO POST DATA FOR MANUFACTURER
        //            postData.manufacturer = list.ManufacturerItems[itemIndex].id;
        //        }//end if
        //    }//end for

        //    //CREATING A NEW HTTPCLIENT OBJECT
        //    var client = new HttpClient();

        //    //SET BASE ADDRESS OF API
        //    client.BaseAddress = new Uri("http://localhost:4001/api/records/recordscreate");

        //    //SERIALIZE POSTDATA OBJECT TO JSON STRING
        //    var json = System.Text.Json.JsonSerializer.Serialize(postData);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    //MAKE POST REQUEST
        //    var response = client.PostAsync(" ", content).Result;

        //    //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
        //    if (response.IsSuccessStatusCode) {
        //        var responseContent = response.Content.ReadAsStringAsync().Result;
        //        var options = new JsonSerializerOptions {
        //            PropertyNameCaseInsensitive = true
        //        };//end var options

        //        //PROMPT USER THAT A NEW RECORD WAS CREATED
        //        MessageBox.Show("New Record Created. Please add another record, or return to main menu.");

        //    }//end if

        //}//end function


        //refresh/save

    }//end class
}//end namespace