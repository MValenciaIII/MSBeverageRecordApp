using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public CategoryTable() {
            InitializeComponent();
            //TO DO & NOTES:
            //USE REQ.PARAMS TO MAKE A POST REQUEST TO ADD A CATEGORY TO MYSQL DATABASE
            //CATEGORY TABLE ALREADY AUTO INCREMENTS, NO NEED TO ADD ID

            //IDEAS:
            //USE A HTTP CLIENT/REQ.PARAMS

        }//end main CategoryTable

    }//end class

}//end namespace
