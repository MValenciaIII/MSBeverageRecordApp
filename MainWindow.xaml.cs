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
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Reflection;


//TODO
// export to csv on button click





namespace MSBeverageRecordApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        //GLOBAL VARIABLE
        string file = "";
        public class urlResult
        {
            public string[] results { get; set; }
        }

        public MainWindow()
        {
            
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
            if (response.IsSuccessStatusCode)
            {


                ////CONVERTING OBJECT "response" variable DATA TO STRING 
                var dataobjects = response.Content.ReadAsStringAsync().Result;
                //CURRENTLY TRYING TO CHANGE OUR STRING TO OBJECT DATA VVV
                RootObject deserializeObject = new RootObject();
                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);

                //How to set the query data to the DATAGRID element.
                MSBeverageRecordApp.ItemsSource = deserializeObject.Items;

                StringBuilder sb = new StringBuilder();


                decimal totalCost = 0.0m;
                string headerline = "id, category, company, model, serial, purchase date, cost, location, sub-location";
                string[] rows = new string[deserializeObject.Items.Count + 1];
                rows[0] = headerline + "\n";
                //loop over list and set values of each row into an array
                for (int i = 1; i < deserializeObject.Items.Count +1; i++) {
                    //clear string on each iteration
                    sb.Clear();
                    //set headers as first row
                    
                    //add values
                    sb.Append(deserializeObject.Items[i-1].record_id.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].categoryName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].companyName.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].model.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].serial.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].purchase_date.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].cost.ToString() + ",");
                    sb.Append(deserializeObject.Items[i-1].locationName.ToString() + ","); 
                    sb.Append(deserializeObject.Items[i-1].sub_location.ToString() + "\n");
                    //get total cost
                    totalCost += (decimal)deserializeObject.Items[i - 1].cost;
                    
                    //assign current string value to be one row
                    rows[i] = sb.ToString();    
                    //test output
                }
                    consoleOutput.Text = $"{totalCost:C}";

                //path to test csv file
                string file = @"C:\Users\MCA\source\repos\MSBeverageRecordApp\test.csv";
                int colCount = 0;
                ////loop over rows and append lines
                //for(int i = 0; i < rows.Length; i++) {
                //    colCount++; 
                //    System.IO.File.AppendAllText(file, rows[i]);
                //    if(colCount > 7) {
                //        System.IO.File.AppendAllText(file, "\n");
                //    }
                //}

                Saving(file, rows, colCount);

            }//end if statusOK


        }//end main

        // use on button click, open save dialog and use path name as file to write to

////////            MSBeverageRecordApp//

            //PrintDG print = new Print();
            //print.printDG(MSBeverageRecordApp, "Title"); 

        public void Saving(string filePath, string[] array, int count) {

            //loop over rows and append lines
            for (int i = 0; i < array.Length; i++) {
                count++;
                System.IO.File.AppendAllText(file, array[i]);
                if (count > 7) {
                    System.IO.File.AppendAllText(file, "\n");
                }
            }
        }//end function
            
        private void muiSave_Click_1(object sender, RoutedEventArgs e, string filePath, string[] array, int count) {
            //create a save file dialog object
            SaveFileDialog sfdSave = new SaveFileDialog();
            file = sfdSave.FileName;
            //open th edialog and wait for the user to make a selection
            bool fileSelected = (bool)sfdSave.ShowDialog();
            if (fileSelected == true) {



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
            public double cost { get; set; }
            public string locationName { get; set; }
            public string sub_location { get; set; }
        }//end class
         ////CALLING THIS AS PARENT OBJECT HOLDER THINGY 
        public class RootObject {
            public int id { get; set; }
            
            public List <Records> Items { get; set; }
        }//end class


        private void addRecord(object sender, RoutedEventArgs e) {
            //MSBeverageRecordApp.Visibility = Visibility.Hidden;
            CreateRecord window = new CreateRecord();

            window.Show();
        }//end function
        //waldo














            static async Task TestAPI(HttpClient client)
            {

            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

    }//end class
}//end namespace