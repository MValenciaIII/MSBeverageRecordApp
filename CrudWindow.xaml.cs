using System.Data;
using System.Net.Http;
//IMPORTING
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


//TODO
//re-format the printDG class to fit the cells text/fill page
//make new button and click event function to call print class so save PDF and print are separate
//update data grid on all pages on save
//filter/search inside of update window

namespace MSBeverageRecordApp {


    public partial class CrudWindow : Page {
        //obj containers

        //main
        public class RootObject {
            public int id { get; set; }

            public List<Records> Items { get; set; }
        }//end class
        //post obj
        public class RootObj {
            public int id { get; set; }
            public PostRecords Items { get; set; }
        }//end class
        //category
        public class Root {
            public int id { get; set; }

            public List<Category> Items { get; set; }
        }//end class
        //location
        public class RootLoc {
            public int id { get; set; }

            public List<Location> Items { get; set; }
        }//end class
        //manufacturer
        public class RootComp {
            public int id { get; set; }

            public List<Manufacture> Items { get; set; }

        }//end class

        //GLOBAL VARIABLE
        string file = "";
        string[] rows = new string[1];
        int colCount = 0;
        public RootObject deserializeObject = new RootObject();
        string c = "";
        Root root = new Root();
        RootLoc rootLoc = new RootLoc();
        RootComp rootComp = new RootComp();
        //RootObj postObj = new RootObj();
        PostRecords post = new PostRecords();
        double equipmentCost = 0.0;

        public CrudWindow() {
            InitializeComponent();
            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();
            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4002/api/records/recordsreal");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            //THIS IS VARIABLE TO GET OBJECT DATA FROM API
            var response = client.GetAsync(client.BaseAddress).Result;
            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {
                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                string dataobjects = response.Content.ReadAsStringAsync().Result;
                c = dataobjects;
                //Need access globally to 
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);
                //How to set the query data to the DATAGRID element.

                MSBeverageRecordGrid.ItemsSource = deserializeObject.Items;

                for (int i = 0; i > deserializeObject.Items.Count; i++) {

                    deserializeObject.Items[i].purchase_date.ToString("MM/dd/yy");
                    equipmentCost += deserializeObject.Items[i].cost;
                }
                
                //tables to pull id's/foreign keys
                Tables();
                Locations();
                Companies();

                #region csv
                //StringBuilder sb = new StringBuilder();
                //decimal totalCost = 0.0m;
                //string headerline = "id, category, company, model, serial, purchase date, cost, location, sub-location";
                //string[] cols = headerline.Split(',');
                //colCount = cols.Length;
                //rows = new string[deserializeObject.Items.Count + 2];
                ////set headers as first row
                //rows[0] = headerline + "\n";
                ////loop over list and set values of each row into an array
                //for (int i = 1; i <= deserializeObject.Items.Count; i++) {
                //    //clear string on each iteration
                //    sb.Clear();
                //    //add values
                //    sb.Append(deserializeObject.Items[i - 1].record_id.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].categoryName.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].companyName.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].model.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].serial.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].purchase_date.ToString());
                //    sb.Append(deserializeObject.Items[i - 1].cost.ToString() + ",");
                //    sb.Append(deserializeObject.Items[i - 1].locationName.ToString() + ",");
                //    sb.Append(deserializeObject.Items[i - 1].sub_location.ToString());
                //    //get total cost
                //    totalCost += (decimal)deserializeObject.Items[i - 1].cost;
                //    //remove trailing comma on last row
                //    if (i == deserializeObject.Items.Count) {
                //        sb.Remove(sb.Length - 2, 1);
                //    }
                //    //assign current string value to be one row
                //    rows[i] = sb.ToString();

                //    // StringBuilder s2 = new StringBuilder();
                //} 
                #endregion
            }//end if statusOK

        }//end main
        //saves 

        //TODO add print button/function, fix print format


        public void Saving(string filePath, string[] array, int num) {
            //VARIABLE
            int count = 0;
            //loop over rows and append lines
            for (int i = 0; i < array.Length; i++) {
                count++;
                System.IO.File.AppendAllText(file, array[i]);
                //start new line after printing each cell in a row
                count = 0;
            }//end for
        }//ef

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
            //from object
            #region from obj
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(deserializeObject.Items);
            System.IO.File.AppendAllText(@"C:\Users\MCA\source\repos\MSBeverageRecordApp\test2.txt", json);
            #endregion
        }//ef

        private void savepdf_Click(object sender, RoutedEventArgs e) {
            System.Windows.Controls.PrintDialog printDlg = new System.Windows.Controls.PrintDialog();


        }//ef
        private void print_Click(object sender, RoutedEventArgs e) {





            PrintDG print = new PrintDG();

            print.printDG(MSBeverageRecordGrid, "Title");

        }

        public void savecsv_Click(object sender, RoutedEventArgs e) {

            //save to csv
            //create a save file dialog object
            Microsoft.Win32.SaveFileDialog sfdSave = new Microsoft.Win32.SaveFileDialog();
            //open the dialog and wait for the user to make a selection
            //fix
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
                //set headers as first row
                rows[0] = headerline + "\n";
                //loop over list and set values of each row into an array
                for (int i = 1; i <= deserializeObject.Items.Count; i++) {
                    //clear string on each iteration
                    sb.Clear();
                    //add values
                    sb.Append(deserializeObject.Items[i - 1].record_id.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].categoryName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].companyName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].model.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].serial.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].purchase_date.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].cost.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].locationName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i - 1].sub_location.ToString() + "\n");
                    //get total cost
                    totalCost += (decimal)deserializeObject.Items[i - 1].cost;
                    //remove trailing comma on last row
                    if (i == deserializeObject.Items.Count) {
                        sb.Remove(sb.Length - 2, 1);
                    }
                    //assign current string value to be one row
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

        //TODO
        //UPDATE
        private void UpdateDataBase() {

            //invalid column names 
            var postRec = new PostRecords {
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

            //http client instance
            var client = new HttpClient();
            //connection url
            client.BaseAddress = new Uri("http://localhost:4002/api/records/modifyid");
            var json = JsonSerializer.Serialize(postRec);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //no status code?
            var response = client.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<Records>(responseContent);

            }
            else
            {
                System.Windows.MessageBox.Show("Error " + response.StatusCode);
            }
        }//ef

        //THIS IS WHAT IS GOING TO BE THE ITEM SOURCE for the DATAGRID
        //THIS IS SETTING UP A PLACE TO STORE EACH OBJECT ATTRIBUTE INSIDE A LIST 
        public class Records {
            public int record_id { get; set; }
            public string categoryName { get; set; }
            public string companyName { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public DateTime purchase_date { get; set; }
            public double cost { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }
        }//end class

        //records class to hold int values of FK id's
        public class PostRecords {
            public int record_id { get; set; }
            public int category { get; set; }
            public int manufacturer { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public DateTime purchase_date { get; set; }
            public double cost { get; set; }
            public int location { get; set; }
            public string sub_location { get; set; }
        }//end class
        private void addRecord(object sender, RoutedEventArgs e) {
            //MSBeverageRecordApp.Visibility = Visibility.Hidden;
            //CreateRecord window = new CreateRecord();
            //window.Show();
        }//ef

        Records rep = new Records();
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

            
            //selects the row that user double clicks
            Records Reports = new Records();
            var row = sender as DataGridRow;
            rep = row.DataContext as Records;

            //fills edit window values
            Reports = rep;
            txbCatName.Text = $"{Reports.categoryName}";
            txbCompName.Text = $"{Reports.companyName}";
            txbModel.Text = $"{Reports.model}";
            txbSerial.Text = $"{Reports.serial}";
            txbPurchaseDate.Text = $"{Reports.purchase_date.ToString("d")}";
            txbCost.Text = $"{Reports.cost}";
            txbLocation.Text = $"{Reports.locationName}";
            txbSubLocation.Text = $"{Reports.sub_location}";

            //switch views to edit window
            spLabels.Visibility = Visibility.Visible;
            spText.Visibility = Visibility.Visible;
            spLbl.Visibility = Visibility.Visible;
            spTxt.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;

            MSBeverageRecordGrid.Visibility = Visibility.Collapsed;
            consoleOutput.Visibility = Visibility.Collapsed;
            fileMenu.Visibility = Visibility.Collapsed;
            Filter.Visibility = Visibility.Collapsed;

        }//ef

        private void btnSaveChange_Click(object sender, RoutedEventArgs e) {

            Records Reports = rep;
            for (int i = 0; i < deserializeObject.Items.Count; i++) {

                if (deserializeObject.Items[i].record_id == Reports.record_id) {

                    post.record_id = Reports.record_id;
                    post.manufacturer = rootComp.Items[i].id;
                    post.category = root.Items[i].id;
                    post.model = txbModel.Text;
                    post.serial = txbSerial.Text;
                    post.purchase_date = DateTime.Parse(txbPurchaseDate.Text);
                    post.cost = double.Parse(txbCost.Text);
                    post.location = rootLoc.Items[i].ID;
                    post.sub_location = txbSubLocation.Text;

                    deserializeObject.Items[i].categoryName = txbCatName.Text;
                    deserializeObject.Items[i].companyName = txbCompName.Text;
                    deserializeObject.Items[i].model = txbModel.Text;
                    deserializeObject.Items[i].serial = txbSerial.Text;
                    deserializeObject.Items[i].purchase_date = DateTime.Parse(txbPurchaseDate.Text);
                    deserializeObject.Items[i].cost = double.Parse(txbCost.Text);
                    deserializeObject.Items[i].locationName = txbLocation.Text;
                    deserializeObject.Items[i].sub_location = txbSubLocation.Text;
                }

                if (txbCatName.Text == root.Items[i].categoryName) {
                    post.category = root.Items[i].id;
                }

                if (txbCompName.Text == rootComp.Items[i].companyName) {
                    post.manufacturer = rootComp.Items[i].id;
                }

                if (txbLocation.Text == rootLoc.Items[i].locationName) {
                    post.location = rootLoc.Items[i].ID;
                    //why
                    //all cap work - lowercase no work
                }

                MSBeverageRecordGrid.ItemsSource = null;
                MSBeverageRecordGrid.ItemsSource = deserializeObject.Items;

            }//end for


            //switch views
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
            spLbl.Visibility = Visibility.Hidden;
            spTxt.Visibility = Visibility.Hidden;

            //call update function when user saves

            UpdateDataBase();
        }//ef

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
            spLbl.Visibility = Visibility.Hidden;
            spTxt.Visibility = Visibility.Hidden;
        }//ef

        public class Category {
            public int id { get; set; }
            public string categoryName { get; set; }
        }//end class
        public class Manufacture {
            public int id { get; set; }
            public string companyName { get; set; }
        }//end class
        public class Location {
            public int ID { get; set; }
            public string locationName { get; set; }
        }//end class

        //pull all tables from db
        public void Tables() {

            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();
            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4002/api/category");
            // Add an Accept header for JSON format.

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            //THIS IS VARIABLE TO GET OBJECT DATA FROM API

            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {
                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                string dataobjects = response.Content.ReadAsStringAsync().Result;

                //Need access globally to 
                root.Items = JsonSerializer.Deserialize<List<Category>>(dataobjects);
                CreateCategoryFilterItems(root);
            }//end if statusOK
        }

        public void Locations() {
            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();
            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4002/api/location");
            // Add an Accept header for JSON format.

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            //THIS IS VARIABLE TO GET OBJECT DATA FROM API

            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {
                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                string dataobjects = response.Content.ReadAsStringAsync().Result;

                //Need access globally to 
                rootLoc.Items = JsonSerializer.Deserialize<List<Location>>(dataobjects);
                CreateLocationFilterItems(rootLoc);
            }//end if statusOK
        }

        public void Companies() {
            //SETTING UP NEW instance of a type of data
            using HttpClient client = new();
            //GETTING QUERY API LINK FOR OBJECT DATA 
            client.BaseAddress = new Uri("http://localhost:4002/api/manufacturer");
            // Add an Accept header for JSON format.

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            //THIS IS VARIABLE TO GET OBJECT DATA FROM API

            var response = client.GetAsync(client.BaseAddress).Result;

            //IF THE RESPONSE VARIABLE IS TRUE RUN THIS CODE.
            if (response.IsSuccessStatusCode) {
                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                string dataobjects = response.Content.ReadAsStringAsync().Result;

                //Need access globally to 
                rootComp.Items = JsonSerializer.Deserialize<List<Manufacture>>(dataobjects);
                CreateCompanyFilterItems(rootComp);
            }
        }

        //fill comboBox values 
        private void CreateLocationFilterItems(RootLoc list) {

            bool contains = false;

            txbLocation.Items.Add("none");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < txbLocation.Items.Count; itemIndex++)

                    if (txbLocation.Items[itemIndex].ToString() == list.Items[index].locationName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    txbLocation.Items.Add(list.Items[index].locationName);

                }//end if
            }
        }//ef

        private void CreateCompanyFilterItems(RootComp list) {

            bool contains = false;

            txbCompName.Items.Add("none");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < txbCompName.Items.Count; itemIndex++)

                    if (txbCompName.Items[itemIndex].ToString() == list.Items[index].companyName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    txbCompName.Items.Add(list.Items[index].companyName);

                }//end if

            }//end for

        }//ef

        private void CreateCategoryFilterItems(Root list) {

            bool contains = false;


            txbCatName.Items.Add("none");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < txbCatName.Items.Count; itemIndex++)

                    if (txbCatName.Items[itemIndex].ToString() == list.Items[index].categoryName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    txbCatName.Items.Add(list.Items[index].categoryName);

                }//end if

            }//end for

        }//ef

    }//end class
}//end namespace