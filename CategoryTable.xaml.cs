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

            //CREATE AN INSTANCE OF THE POSTDATA CLASS
            var postData = new PostData {
                categoryName = "test"//will link to txtInput
            };

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/category");

            //SERIALIZE POSTDATA OBJECT TO JSON STRING
            var json = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //MAKE POST REQUEST
            var response = client.PostAsync("posts", content).Result;

            //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                };

                //DESERIALIZE BACK INTO C# OBJECT
                var postResponse = System.Text.Json.JsonSerializer.Deserialize<PostResponse>(responseContent, options);
                Debug.WriteLine("Post successful. ID: " + postResponse.Id);

            } else {
                //GIVE ERROR MESSAGE IF UNSUCCESSFUL
                Debug.WriteLine("Error: " + response.StatusCode);
            }//end if

        }//end main CategoryTable


        //FIGURING OUT HOW TO CREATE A WAY TO MAKE A NEW CATEGORY TO POST INTO THE API?
        //The Hypertext Transfer Protocol(HTTP) Post method is mainly used at the client (Browser)
        //side to send data to a Specified server in order to create or rewrite a particular resource/data.
        //This data sent to the server is stored in the request body of the HTTP request.
        //Post method eventually leads to the creation of a new resource or updating an existing one. -FW

        // Instantiate one HttpClient for your application's lifetime (recommended)
        //private static HttpClient client = new HttpClient();

        //From POST example, it was written just like this, trying to figure it out it didnt have private
       // async Task SendPostRequestAsync() {
        //    var categoryValue = new Category { 
        //            "Icecream";
        //}
        //        var categoryContent = new FormUrlEncodedContent(categoryValue);
        //        //i want Icecream to be added as a new Category value
        //        var categoryResponse = await client.PostAsync(http://localhost:4001/api/category);
        //        var responseString = await categoryResponse.Content.ReadAsStringAsync;
        //} 

    private void txtInput_TextChanged(object sender, TextChangedEventArgs e) {
           string input = txtInput.Text;
        }//end function

    }//end class

}//end namespace
