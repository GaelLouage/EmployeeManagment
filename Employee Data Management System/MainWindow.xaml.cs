using Employee_Data_Management_System.Data;
using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Extensions;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Helpers;
using Employee_Data_Management_System.Models;
using System.Collections.ObjectModel;
using System.Linq;
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
        private List<EmployeeEntity> _employees = new List<EmployeeEntity>();
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
                _employees = new List<EmployeeEntity>();

                // Create OpenFileDialog
                var openFileDlg = FileDialog.Open();
                if (openFileDlg is null) return;
                var fileName = openFileDlg.FileName;
                var fileExtention = fileName.GetFileExtension();
                var fileExtensionsDictionary = FileExtensionsDictionary.Dictionary;
                if (fileExtensionsDictionary.ContainsKey(fileExtention))
                {
                    _employees = await fileExtensionsDictionary[fileExtention](openFileDlg);
                    lvEmployees.ItemsSource = _employees;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void txtSearchEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvEmployees.ItemsSource = _employees;
            if (_employees is not null &&  _employees.Count > 0 )
            {
                lvEmployees.ItemsSource = _employees.Where(x => x.Name.ToLower().Contains(txtSearchEmployee.Text.ToLower())).ToList();
            }
        }
        private void cmbOrderEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var search = txtSearchEmployee.Text;
            string orderedChoice = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            _employees = _employees.Sort(search, orderedChoice);
            lvEmployees.ItemsSource = _employees;
        }
      
    }
}