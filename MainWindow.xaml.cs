
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Net.Http;
//IMPORTING
using System.Net.Http.Headers;
using System.Text.Json;
using static MSBeverageRecordApp.Edit;

//TODO
//add print whole data grid function so raw csv data and report are both options
namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
        //todo
    //cannot access deserialized object inside of edit window to update it on save
    //OPTION 1
    //re-factor xaml into one window? setting visibility on/off in the datagrid double click event
    //then could use datacontext/databinding to update both on save click
    //OPTION 2
    //await the button click event in edit window,
    //how to run a function in mainWindow based on event in editWindow?
    //then update deserialized obj inside of main using editWindow.c.Items

    //fix bug where program crashes on closing/re-opening edit window
    //find library to give option print out datagrid records instead of raw csv
    public partial class MainWindow : Window {
        Edit editWindow = new Edit();
        public class RootObject {
            public int id { get; set; }

            public List<Records> Items { get; set; }
        }//end class

        //GLOBAL VARIABLE
        string file = "";
        string[] rows = new string[1];
        int colCount = 0;
        public RootObject deserializeObject = new RootObject();
        string c = "";

        public class urlResult {
            public string[] results { get; set; }
        }
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
                string dataobjects = response.Content.ReadAsStringAsync().Result;
                c = dataobjects;

                //Need access globally to 

                deserializeObject.Items = JsonSerializer.Deserialize<List<Records>>(dataobjects);
                //How to set the query data to the DATAGRID element.
                MSBeverageRecordApp.ItemsSource = deserializeObject.Items;
                editWindow.c.Items = deserializeObject.Items;

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
        }//end main

        //save to file
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
        

        //add function
        private void printRec_Click(object sender, RoutedEventArgs e) {
            PrintDialog s = new PrintDialog();
            
        }
        
        //file dialog and csv format
        public void muiSave_Click(object sender, RoutedEventArgs e) {

            //save to csv
            //create a save file dialog object
            SaveFileDialog sfdSave = new SaveFileDialog();
            //open the dialog and wait for the user to make a selection
            //fix
            sfdSave.DefaultExt = "csv";
            sfdSave.Filter =
                "Text files (*.csv)|*.txt|All files (*.*)|*.*";
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
        private void UpdateDataBase(object sender, DependencyPropertyChangedEventArgs e) {

           
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
         ////CALLING THIS AS PARENT OBJECT HOLDER THINGY 
         /// <summary>
        
         /// </summary>



        private void addRecord(object sender, RoutedEventArgs e) {
            //MSBeverageRecordApp.Visibility = Visibility.Hidden;
            CreateRecord window = new CreateRecord();
            window.Show();
            
        }//end function

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            //make global to be able to call save function
            var row = sender as DataGridRow;
            var rep = row.DataContext as Records;
            //Edit editRep = new Edit();
            editWindow.Owner = this;
            //editWindow.ShowRecord(rep);
            editWindow.Show();
            


            

            //RootObject root = editWindow.BtnSave_Click(editWindow.x, editWindow.y);

            //var tasks = new List<Task> {editWindow.saveGrid()};
            //Task<RootObject> tasks =  editWindow.BtnSave_Click(editWindow.x, editWindow.y);

           


            //// await Task.WhenAll(tasks);
            //do {
            //    await Task.WhenAll(tasks);

            //} while (editWindow.editing == false );

            //MSBeverageRecordApp.ItemsSource = tasks.Result.Items

            
            //somehow call save function and send new value to datagrid object
        }//ef



        //public async Task<Task<RootObject>> edit() {
        //    //deserializeObject = editWindow.saveGrid(root);

        //    // MSBeverageRecordApp.ItemsSource = editWindow.saveGrid(deserializeObject).Items;

        //    // RootObject tasks = editWindow.saveGrid();
        //    Task<RootObject> tasks = editWindow.saveGrid();
        //    await Task.WhenAll(tasks);

        //    return tasks;

        //}





    }//end class
}//end namespace