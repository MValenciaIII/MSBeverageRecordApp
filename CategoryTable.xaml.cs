using System;
using System.Collections.Generic;
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

namespace MSBeverageRecordApp
{
    /// <summary>
    /// Interaction logic for CategoryTable.xaml
    /// </summary>
    public partial class CategoryTable : Page {
        //GLOBAL CLASS FOR DATA TO SEND TO API
        class PostCategory {
        public string categoryName { get; set; }
    }//end class
     class PostLocation {
        public string locationName { get; set; }
    }//end class
        public CategoryTable()
        {
            InitializeComponent();
        }//end main CategoryTable
        //Post Category
        private void Category_Button_Click(object sender, RoutedEventArgs e) {
            var postData = new PostCategory {
                categoryName = txtCategory.Text //will link to txtInput
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


        }
        //POST LOCATION
        private void Location_Button_Click(object sender, RoutedEventArgs e) {
            var postData = new PostLocation {
                locationName = txtLocation.Text //will link to txtInput
            };

            //CREATING A NEW HTTPCLIENT OBJECT
            var client = new HttpClient();

            //SET BASE ADDRESS OF API
            client.BaseAddress = new Uri("http://localhost:4001/api/category/locationcreate/");

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


        }
    }//end class
}//end namespace
