using Employee_Data_Management_System.Data;
using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Extensions;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Helpers;
using Employee_Data_Management_System.Models;
using Employee_Data_Management_System.Singletons;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
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
        public async void mainMenuGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(FileSingleton.Instance.FileName) && !string.IsNullOrEmpty(FileSingleton.Instance.FileExtension)) 
            {

                _employees = await PopulateEmployeesFromFileAsync(async (dir) => await dir.ReadAllAsync(FileSingleton.Instance.OpenFileDialog));

                lvEmployees.ItemsSource = _employees;
            }
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
                FileSingleton.Instance.OpenFileDialog = FileDialog.Open();
                
                if (FileSingleton.Instance.OpenFileDialog is null) return;
                FileSingleton.Instance.FileName = FileSingleton.Instance.OpenFileDialog.FileName;
                var fileName = FileSingleton.Instance.OpenFileDialog.FileName;
                FileSingleton.Instance.FileExtension = fileName.GetFileExtension();
                var fileExtensionsDictionary = FileExtensionsDictionary.Dictionary;
                if (fileExtensionsDictionary.ContainsKey(FileSingleton.Instance.FileExtension))
                {
                    _employees = await fileExtensionsDictionary[FileSingleton.Instance.FileExtension](FileSingleton.Instance.OpenFileDialog);
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

                var employee = (lvEmployees.SelectedItem as EmployeeEntity);
                var updateForm = new Update(employee);
                updateForm.Show();
                this.Close();
                
                //var employeeName = (lvEmployees.SelectedItem as EmployeeEntity)?.Name;
                //// testing
                //var updateEmployee = new EmployeeEntity();
                //updateEmployee.Name = "Gaël";
                //updateEmployee.Department = "IT";
                //_employees = await PopulateEmployeesFromFileAsync(async (fileReaderFactory) => await fileReaderFactory.UpdateByNameAsync(employeeName, updateEmployee, _fileName));
                //lvEmployees.ItemsSource = _employees;
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is EmployeeEntity)
            {
                var employeeName = (lvEmployees.SelectedItem as EmployeeEntity)?.Name;

                _employees = await PopulateEmployeesFromFileAsync(async (dir) => await dir.DeleteByNameAsync(employeeName, FileSingleton.Instance.FileName));

                lvEmployees.ItemsSource = _employees;
            }
        }

        private async Task<List<EmployeeEntity>> PopulateEmployeesFromFileAsync(Func<IFileReaderFactory, Task<List<EmployeeEntity>>> func)
        {
        
            switch (FileSingleton.Instance.FileExtension)
            {
                case "xml":
                    return await func?.Invoke(FileReaderFactory.Read(FileType.XML));
                default:
                    return await func?.Invoke(FileReaderFactory.Read(FileType.CSV));
            }
        }
    }
}