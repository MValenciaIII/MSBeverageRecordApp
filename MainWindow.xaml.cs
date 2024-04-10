
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Net.Http;
//IMPORTING
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Net.Http.Headers;
using System.Text.Json;

using System.Runtime.CompilerServices;
using static MSBeverageRecordApp.MainWindow;
using System.Data;

using System.Printing;
using System.Windows.Controls.Primitives;
using Syncfusion.Data;

using System.Windows.Markup;

using System.Windows.Xps;
using System;
using System.IO;
using Syncfusion.UI.Xaml.Grid;
using System.Windows.Xps.Packaging;
using System.Reflection.Metadata;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using PrintDialogX;
using PrintDialogX.PrintDialog;
using ChoETL;
using System.Reflection.PortableExecutable;
using System.Collections.Generic;

//TODO
//add print whole data grid function so raw csv data and report are both options
namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //todo


    //find library to give option print out datagrid records instead of raw csv
    public partial class MainWindow : Window {



        public class RootObject {
            public int id { get; set; }

            public List<Records> Items { get; set; }
        }//end class

        public class RootObj {
            public int id { get; set; }

            public List<PostRecords> Items { get; set; }
        }//end class

        public class Root {
            public int id { get; set; }

            public List<Category> Items { get; set; }
        }//end class


        //GLOBAL VARIABLE
        string file = "";
        string[] rows = new string[1];
        int colCount = 0;
        public RootObject deserializeObject = new RootObject();
        string c = "";
        Root root = new Root();
        RootObj post = new RootObj();


        public class urlResult {
            public string[] results { get; set; }
        }
        public MainWindow() {
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
                Tables();
                //CreateLocationFilterItems(deserializeObject);

                //create csv array why did we do this here?
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
             //MessageBox.Show();


            //this.DataContext = deserializeObject;
        }//end main

        //save to file
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
        }//end function

        //test serialize datagrid back to json
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
        }


        //adding function to format the grid and print
        //saves pdf kind of

        private void savepdf_Click(object sender, RoutedEventArgs e) {
            //how to set the width?
            FrameworkElement head = new FrameworkElement();
            FrameworkElement foot = new FrameworkElement();

            //saves but need to add print preview
            //test with more than one page of data
            //maybe add a header

            //MSBeverageRecordGrid.
            System.Windows.Controls.PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
            //printDlg.PrintVisual(MSBeverageRecordGrid, "title");
            PrintDG print = new PrintDG();
            print.PrintDataGrid(head, MSBeverageRecordGrid, foot, printDlg);

            //WORKS BUT ONLY USES HALF THE PAGE AND NO PREVIEW
            //add a show print dialog in class to give print options
            //or add a print button 
        }


        //file dialog and csv format
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


        //UPDATE

        //SET post request here then call in the edit window save button click event
        private void UpdateDataBase() {
           Records reports = rep;



            var postRec = new Records {
                record_id = rep.record_id,
                categoryName = rep.categoryName,
                companyName = rep.companyName,
                model = rep.model,
                serial = rep.serial,
                purchase_date = rep.purchase_date,
                cost = rep.cost,
                locationName = rep.locationName,
                sub_location = rep.sub_location
            };
            //http client instance
            var client = new HttpClient();
            //connection url
            client.BaseAddress = new Uri("http://localhost:4002/api/records/recordsreal");
            var json = JsonSerializer.Serialize(postRec);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //no status code?
            var response = client.PostAsync("modifyid", content).Result;

            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<Records>(responseContent);
                MessageBox.Show("CatName:" + postResponse.categoryName);

            } else {
                MessageBox.Show("Error " + response.StatusCode);
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
            public string purchase_date { get; set; }
            public double cost { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }
        }//end class

        public class PostRecords {
            public int record_id { get; set; }
            public int categoryName { get; set; }
            public int companyName { get; set; }
            public string model { get; set; }
            public string serial { get; set; }
            public string purchase_date { get; set; }
            public double cost { get; set; }
            public int locationName { get; set; }
            public string sub_location { get; set; }
        }//end class

        private void addRecord(object sender, RoutedEventArgs e) {
            //MSBeverageRecordApp.Visibility = Visibility.Hidden;
            CreateRecord window = new CreateRecord();
            window.Show();
        }//end function

        Records rep = new Records();
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

            Records Reports = new Records();
            var row = sender as DataGridRow;
            rep = row.DataContext as Records;

            Reports = rep;
            txbCatName.Text = $"{Reports.categoryName}";
            txbCompName.Text = $"{Reports.companyName}";
            txbModel.Text = $"{Reports.model}";
            txbSerial.Text = $"{Reports.serial}";
            txbPurchaseDate.Text = $"{Reports.purchase_date}";
            txbCost.Text = $"{Reports.cost}";
            txbLocation.Text = $"{Reports.locationName}";
            txbSubLocation.Text = $"{Reports.sub_location}";

            //switch views
            spLabels.Visibility = Visibility.Visible;
            spText.Visibility = Visibility.Visible;
            spLbl.Visibility = Visibility.Visible;
            spTxt.Visibility = Visibility.Visible;


            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;

            MSBeverageRecordGrid.Visibility = Visibility.Collapsed;
            consoleOutput.Visibility = Visibility.Collapsed;
            fileMenu.Visibility = Visibility.Collapsed;
            btnaddRecord.Visibility = Visibility.Collapsed;
            Filter.Visibility = Visibility.Collapsed;

        }//ef

        //fix datetime format to just date or a string
        private void btnSaveChange_Click(object sender, RoutedEventArgs e) {

            Records Reports = rep;
            

            for (int i = 0; i < deserializeObject.Items.Count; i++) {
                if (deserializeObject.Items[i].record_id == Reports.record_id) {
                    deserializeObject.Items[i].categoryName = txbCatName.Text;
                    deserializeObject.Items[i].companyName = txbCompName.Text;
                    deserializeObject.Items[i].model = txbModel.Text;
                    deserializeObject.Items[i].serial = txbSerial.Text;
                    deserializeObject.Items[i].purchase_date = txbPurchaseDate.Text;
                    deserializeObject.Items[i].cost = double.Parse(txbCost.Text);
                    deserializeObject.Items[i].locationName = txbLocation.Text;
                    deserializeObject.Items[i].sub_location = txbSubLocation.Text;

                    if (txbCatName.Text == root.Items[i].categoryName) {
                        post.Items[i].categoryName = root.Items[i].id;
                    }
                    //post.Items[i].companyName = txbCompName.Text;
                    //post.Items[i].model = txbModel.Text;
                    //post.Items[i].serial = txbSerial.Text;
                    //post.Items[i].purchase_date = txbPurchaseDate.Text;
                    //post.Items[i].cost = double.Parse(txbCost.Text);
                    //post.Items[i].locationName = txbLocation.Text;
                    //post.Items[i].sub_location = txbSubLocation.Text;

                }


                MSBeverageRecordGrid.ItemsSource = null;
                MSBeverageRecordGrid.ItemsSource = deserializeObject.Items;

            }


            //switch views
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            btnaddRecord.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
            spLbl.Visibility = Visibility.Hidden;
            spTxt.Visibility = Visibility.Hidden;


            //call update database here
            UpdateDataBase();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            MSBeverageRecordGrid.Visibility = Visibility.Visible;
            consoleOutput.Visibility = Visibility.Visible;
            fileMenu.Visibility = Visibility.Visible;
            btnaddRecord.Visibility = Visibility.Visible;
            Filter.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;

            spLabels.Visibility = Visibility.Hidden;
            spText.Visibility = Visibility.Hidden;
            spLbl.Visibility = Visibility.Hidden;
            spTxt.Visibility = Visibility.Hidden;
        }//end method

        public class Category {
            public int id { get; set; }
            public string categoryName { get; set; }
        }//end class

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
                CreateLocationFilterItems(deserializeObject);
                CreateCompanyFilterItems(deserializeObject);
                CreateCategoryFilterItems(root);
            }//end if statusOK
        }

        private void CreateLocationFilterItems(RootObject list) {

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

        }//end method

        private void CreateCompanyFilterItems(RootObject list) {

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

        }//end for


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

        }//end for

    }//end class
}//end namespace