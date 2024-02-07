using Employee_Data_Management_System.Data;
using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Extensions;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Factory.Interface;
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
        private string _fileExtension = string.Empty;
        private string _fileName = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
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
                _fileName = openFileDlg.FileName;
                var fileName = openFileDlg.FileName;
                _fileExtension = fileName.GetFileExtension();
                var fileExtensionsDictionary = FileExtensionsDictionary.Dictionary;
                if (fileExtensionsDictionary.ContainsKey(_fileExtension))
                {
                    _employees = await fileExtensionsDictionary[_fileExtension](openFileDlg);
                    lvEmployees.ItemsSource = _employees;

                    txtAverageSalary.Text = $"Average Salary: {_employees.Average(x => x.Salary)}";
                    txtHighestSalary.Text = $"Highest Salary: {_employees.MaxBy(x => x.Salary).Salary}";
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
            if (_employees is not null && _employees.Count > 0)
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

        private void lvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = lvEmployees.SelectedItem as EmployeeEntity;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is EmployeeEntity)
            {
                var employeeName = (lvEmployees.SelectedItem as EmployeeEntity)?.Name;
                var updateEmployee = new EmployeeEntity();
                _employees = await PopulateEmployeesFromFileAsync(async (dir) => await dir.UpdateByNameAsync(employeeName, updateEmployee, _fileName));
                //switch (_fileExtension)
                //{
                //    case "xml":
                //        _employees = await FileReaderFactory.Read(FileType.XML).UpdateByNameAsync(employeeName, updateEmployee, _fileName);
                //        break;
                //    default:
                //        _employees = await FileReaderFactory.Read(FileType.CSV).UpdateByNameAsync(employeeName, updateEmployee, _fileName);
                //        break;
                //};
                lvEmployees.ItemsSource = _employees;
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is EmployeeEntity)
            {
                var employeeName = (lvEmployees.SelectedItem as EmployeeEntity)?.Name;

                _employees = await PopulateEmployeesFromFileAsync(async (dir) => await dir.DeleteByNameAsync(employeeName, _fileName));

                lvEmployees.ItemsSource = _employees;
            }
        }

        private async Task<List<EmployeeEntity>> PopulateEmployeesFromFileAsync(Func<IFileReaderFactory, Task<List<EmployeeEntity>>> func)
        {
            var employeeName = (lvEmployees.SelectedItem as EmployeeEntity)?.Name;
            switch (_fileExtension)
            {
                case "xml":
                    var dirXML = FileReaderFactory.Read(FileType.XML);

                    return await func?.Invoke(dirXML);
                default:
                    var dirCSV = FileReaderFactory.Read(FileType.CSV);

                    return await func?.Invoke(dirCSV);
            }
        }
    }
}