using Employee_Data_Management_System.Data;
using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Extensions;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Helpers;
using Employee_Data_Management_System.Models;
using System.Collections.ObjectModel;
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

namespace Employee_Data_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
        }
     
        public void LastNameCM_Click(object sender, RoutedEventArgs e)
        {

        }

        public void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnOpenCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lvEmployees.ItemsSource = null;
                lvEmployees.Items.Clear();


                // Create OpenFileDialog
                var openFileDlg = FileDialog.Open();
                var fileName = openFileDlg.FileName;
                var fileExtention = fileName.GetFileExtension();
                var fileExtensionsDictionary = FileExtensionsDictionary.Dictionary;
                if (fileExtensionsDictionary.ContainsKey(fileExtention))
                {
                    lvEmployees.ItemsSource = await fileExtensionsDictionary[fileExtention](openFileDlg);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}