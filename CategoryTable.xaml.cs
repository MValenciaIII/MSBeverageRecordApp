using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace MSBeverageRecordApp {
    /// <summary>
    /// INTERACTION LOGIC FOR CategoryTable.xaml
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
         //GLOBAL CLASS FOR DATA TO SEND TO API
        class PostCategory {
            public string categoryName { get; set; }
        }//end class
        class PostLocation {
            public string locationName { get; set; }
        }//end class
        class PostManufacturer {
            public string companyName { get; set; }
        }//end class
        public CategoryTable() {
            InitializeComponent();
        }//end main CategoryTable
         //Post Category
        private void Category_Button_Click(object sender, RoutedEventArgs e) {
            var postData = new PostCategory {
                categoryName = txtCategory.Text.ToUpper() //will link to txtInput
            };

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/category/categorycreate/");

            //SERIALIZE POSTDATA OBJECT TO JSON STRING
            var json = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //MAKE POST REQUEST
            var response = client.PostAsync(" ", content).Result;

            //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true

                };
                // Prompt user category is updated
                MessageBox.Show("New Category Created");
            }



            //return to main menu
            this.NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));


        }// END EVENT
         //POST LOCATION
        private void Location_Button_Click(object sender, RoutedEventArgs e) {
            var postData = new PostLocation {
                locationName = txtLocation.Text.ToUpper() //will link to txtInput
            };

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/location/locationcreate/");

            //SERIALIZE POSTDATA OBJECT TO JSON STRING
            var json = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //MAKE POST REQUEST
            var response = client.PostAsync(" ", content).Result;

            //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true

                };
                // Prompt user category is updated
                MessageBox.Show("New Location Added");
            }



            //return to main menu
            this.NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));


        }// END EVENT

        //POST LOCATION
        private void Manufacturer_Button_Click(object sender, RoutedEventArgs e) {
            var postData = new PostManufacturer {
                companyName = txtManufacturer.Text.ToUpper() //will link to txtInput
            };

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/manufacturer/manufacturercreate/");

            //SERIALIZE POSTDATA OBJECT TO JSON STRING
            var json = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //MAKE POST REQUEST
            var response = client.PostAsync(" ", content).Result;

            //CHECK STATUS CODE TO SEE IF REQUEST WAS SUCCESSFUL
            if (response.IsSuccessStatusCode) {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true

                };
                // Prompt user category is updated
                MessageBox.Show("New manufacturer Added");
            }



            //return to main menu
            this.NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));


        }// END EVENT
    }//end class

}//end namespace
