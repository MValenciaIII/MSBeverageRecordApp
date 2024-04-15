using System.Windows;


namespace MSBeverageRecordApp {
    /// <summary>
    /// INTERACTION LOGIC FOR MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            MainFrame.Content = new MenuPage();
        }//end main window

    }//end class
}//end namespace