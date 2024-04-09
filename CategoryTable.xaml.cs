using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for CategoryTable.xaml
    /// </summary>
    public partial class CategoryTable : Page {
        //GLOBAL CLASS FOR DATA TO SEND TO API
        class PostData {
            public string categoryName { get; set; }
        }//end class

        //CLASS TO GET RESPONSE FROM API
        class PostResponse {
            public int Id { get; set; }
        }//end class
        public CategoryTable() {
            InitializeComponent();
        }//end main CategoryTable
        private void btnSubmit_Click(object sender, RoutedEventArgs e) {
            string input = txtInput.Text;

            //CREATE AN INSTANCE OF THE POSTDATA CLASS
            var postData = new PostData {
                categoryName = input
            };//end var postData

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/category/categorycreate/");

            //SERIALIZE POSTDATA OBJECT TO JSON STRING
            var json = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //MAKE POST REQUEST
            var response = client.PostAsync("", content).Result;

            //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                };//end var options

                Debug.WriteLine("Post request sent");
            }//end if
        }//end function

    }//end class

}//end namespace
