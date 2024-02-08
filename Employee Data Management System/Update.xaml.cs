using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Models;
using Employee_Data_Management_System.Singletons;
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
using System.Windows.Shapes;

namespace Employee_Data_Management_System
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private EmployeeEntity _employee;

        public Update(EmployeeEntity employee)
        {
            InitializeComponent();
            _employee = employee;
         
            txtName.Text = employee.Name;
            txtAge.Text = $"{employee.Age}";
            txtSalary.Text = $"{employee.Salary}";
            cmbEmployeeDepartment.Text = employee.Department;

        }


        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var employee = new EmployeeEntity();
           
         
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtAge.Text) ||
                string.IsNullOrEmpty(txtSalary.Text) ||
                 string.IsNullOrEmpty(cmbEmployeeDepartment.Text))
            {
                MessageBox.Show("Al fields are required!");
                return;
            }
            if(!int.TryParse(txtSalary.Text, out var salary))
            {
                MessageBox.Show("Salary must be numeric without decimals!");
                return;
            }
            if(!int.TryParse(txtAge.Text, out var age))
            {
                MessageBox.Show("Age must be numeric!");
                return;
            }
            employee.Name = txtName.Text;
            employee.Department = cmbEmployeeDepartment.Text;
            employee.Salary = salary;
            employee.Age = age;
            switch (FileSingleton.Instance.FileExtension)
            {
                case "xml":
                    //FileReaderFactory.Read(FileType.XML).UpdateByNameAsync(_employee.Name,);
                default:
                    await FileReaderFactory.Read(FileType.CSV).UpdateByNameAsync(_employee.Name,employee, FileSingleton.Instance.FileName);
                    break;
            }
            OpenMainWindow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
