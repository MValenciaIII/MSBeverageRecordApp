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

namespace MSBeverageRecordApp {
    /// <summary>
    /// Interaction logic for AddManufacturer.xaml
    /// </summary>
    public partial class AddManufacturer : Page {
        //CLASS TO GET RESPONSE FROM API
        class PostResponse {
            public int Id { get; set; }
        }//end class
        class PostManufacturer {
            public string companyName { get; set; }
        }//end class
        public AddManufacturer() {
            InitializeComponent();
        }//end main

        private void Manufacturer_Button_Click(object sender, RoutedEventArgs e) {
            //INPUT VALIDATION
            if (string.IsNullOrEmpty(txtManufacturer.Text)) {
                //THE TEXTBOX IS EMPTY; DISPLAY AN ERROR MESSAGE OR TAKE APPROPRIATE ACTION.
                MessageBox.Show("Please enter a value in the category.");
            } else {
                string input = txtManufacturer.Text;
                txtManufacturer.Text = "";

                var postData = new PostManufacturer {
                    companyName = input.ToUpper()
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
                    //PROMPT USER CATEGORY IS UPDATED
                    MessageBox.Show("New manufacturer Added");
                }//end if

                //RETURN TO MAIN MENU
                this.NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));
            }//end if

        }//end event
    }//end class
}//end namespace
