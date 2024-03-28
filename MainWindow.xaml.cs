
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

//sort filter team adding here
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Policy;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;


//ability to create update delete 


//a filter button possibly
//records is a column

//a category filter table

//WANTS TO ASSIGN ITS OWN RECORD NUMBER
//CREATE ONE DIGIT CATEGORY VIEW WITH NAME AND DESCRIPTION, DISPLAY/PRINT BY CATEGORY
//CALCULATE TOTAL COST FUNCTION IN REPORTS
//HE WANTS INQUIRIES BY CATEGORY OR LOCATION
//TOTAL VALUE BY CATEGORY/MANUFACTURER


namespace MSBeverageRecordApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        //GLOBAL VARIABLE
        string file = "";
        string[] rows = new string[1];
        int colCount = 0;
        RootObject deserializeObject = new RootObject();

        public class urlResult
        {
            public string[] results { get; set; }
        }//end class

        public MainWindow() {

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
                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;
                //CURRENTLY TRYING TO CHANGE OUR STRING TO OBJECT DATA VVV
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);

                //How to set the query data to the DATAGRID element.
                MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
                CreateLocationFilterItems(deserializeObject);

            }

            //    #region CSV
            //    //vars to hold rows, cost, headers, col count
            //    StringBuilder sb = new StringBuilder();
            //    decimal totalCost = 0.0m;
            //    string headerline = "id, category, company, model, serial, purchase date, cost, location, sub-location, ";
            //    string[] cols = headerline.Split(',');
            //    colCount = cols.Length;
            //    rows = new string[deserializeObject.Items.Count + 3];
            //    //set headers as first row
            //    rows[0] = headerline + "\n";
            //    //loop over list and set values of each row into an array
            //    for (int i = 1; i <= deserializeObject.Items.Count; i++) {
            //        //clear string on each iteration
            //        sb.Clear();
            //        //add values
            //        sb.Append(deserializeObject.Items[i - 1].record_id.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].categoryName.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].companyName.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].model.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].serial.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].purchase_date.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].cost.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].locationName.ToString() + ",");
            //        sb.Append(deserializeObject.Items[i - 1].sub_location.ToString() + "\n");
            //        //get total cost
            //        totalCost += (decimal)deserializeObject.Items[i - 1].cost;
            //        //remove trailing comma on last row
            //        if (i == deserializeObject.Items.Count) {
            //            sb.Remove(sb.Length - 2, 1);
            //        }
            //        //assign current string value to be one row
            //        rows[i] = sb.ToString();
            //    }

            //    consoleOutput.Text = $"{totalCost:C}";
            //    sb.Clear();
            //    sb.Append("\ntotal equipment cost: " + totalCost.ToString());
            //    rows[deserializeObject.Items.Count + 1] = sb.ToString();
        //end if statusOK
    }//end main

        public void Saving(string filePath, string[] array, int num) {

            //VARIABLE
            int count = 0;
            //loop over rows and append lines
            for (int i = 0; i < array.Length; i++) {
                count++;
                System.IO.File.AppendAllText(file, array[i]);
                if (count > num) {
                    //start new line after printing each cell in a row
                    System.IO.File.AppendAllText(file, "\n");
                }
                
            }//end for
        }//end function
        private void muiSave_Click(object sender, RoutedEventArgs e) {
            //create a save file dialog object
            SaveFileDialog sfdSave = new SaveFileDialog();
            //open the dialog and wait for the user to make a selection

            //FIX
            sfdSave.DefaultExt = "csv";
            sfdSave.Filter =
                "Text files (*.csv)|*.txt|All files (*.*)|*.*";
            bool fileSelected = (bool)sfdSave.ShowDialog();
            file = sfdSave.FileName;
            if (fileSelected == true) {
                Saving(file, rows, colCount);
                
            }//end if
        }








        //THIS IS WHAT IS GOING TO BE THE ITEM SOURCE for the DATAGRID
        //THIS IS SETTING UP A PLACE TO STORE EACH OBJECT ATTRIBUTE INSIDE A LIST 
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
            
            public List <Records> Items { get; set; }
        }//end class

        public static List<Records> FilterHotspotRecords(List<Records> records, ComboBox filter) {

            if (filter.SelectedItem.ToString() != "All Categories") {

                return records.FindAll(record => record.categoryName == filter.SelectedItem);

            } else {

                return records;

            }//end if

        }//end function

        public void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var record = deserializeObject.Items;
            MSBeverageRecordApp.ItemsSource = FilterHotspotRecords(record, Filter);

        }//end function

        private void CreateLocationFilterItems(RootObject list) {

            bool contains = false;

            Filter.Items.Add("All Categories");

            for (int index = 0; index < list.Items.Count; index++) {

                for (int itemIndex = 0; itemIndex < Filter.Items.Count; itemIndex++)

                    if (Filter.Items[itemIndex].ToString() == list.Items[index].categoryName) {

                        contains = true;

                    }//end if

                if (contains == false) {

                    Filter.Items.Add(list.Items[index].categoryName);

                }//end if

            }//end for

        }//end for

        private void addRecord(object sender, RoutedEventArgs e)
        {
            //MSBeverageRecordApp.Visibility = Visibility.Hidden;
            CreateRecord window = new CreateRecord();

            window.Show();
        }//end function

        static async Task TestAPI(HttpClient client)
        {

        }//end function

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }//end function

        //private void WriteTextToFile(string fileName, string text) {

        //    if (File.Exists(fileName)) {
        //        File.Delete(fileName);
        //    }//end if

        //    FileStream outfile = new FileStream(fileName, FileMode.OpenOrCreate);


        //    char[] buffer = text.ToCharArray();
        //    char currentChar = '\0';
        //    byte writeData = 0;

        //    for (int index = 0; index < buffer.Length; index += 1) {
        //        currentChar = buffer[index];
        //        writeData = (byte)currentChar;
        //        outfile.WriteByte(writeData);
        //    }//end for
        //    outfile.Close();
        //}//end function
    }//end class
}//end namespace