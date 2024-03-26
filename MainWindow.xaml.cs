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

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;


//ability to create update delete 


//a filter button possibly
//records is a coloumn

//a category filter table


//category
//manufactuer
//model
//serial number
//purchase date
//cost
//location
//sublocation



//TODO
//a way to print/export a C# datagrid to a .pdf or .csv sheet?
//new list to keep track of filtered list to replace data grid when it's filtered





namespace MSBeverageRecordApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        //GLOBAL VARIABLE
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


            }
            

        }//end mainwindow

        //private void muiSave_Click(object sender, RoutedEventArgs e) {
        //    //create a save file dialog object
        //    SaveFileDialog sfdSave = new SaveFileDialog();
        //    //open th edialog and wait for the user to make a selection
        //    bool fileSelected = (bool)sfdSave.ShowDialog();
        //    if (fileSelected == true) {
        //        WriteTextToFile(sfdSave.FileName, txtMain.Text);
        //    }//end if
        //}//end event

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


        private void addRecord(object sender, RoutedEventArgs e)
            {
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