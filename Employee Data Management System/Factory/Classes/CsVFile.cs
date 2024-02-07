using Employee_Data_Management_System.CustomExceptions;
using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Factory.Classes
{
    public class CsVFile : IFileReaderFactory
    {
        private List<EmployeeEntity> _employees = new List<EmployeeEntity>();
     
        public async Task<List<EmployeeEntity>> ReadAllAsync(Microsoft.Win32.OpenFileDialog openFileDlg = null)
        {
            try
            {
                var fileName = openFileDlg.FileName;
                return await GetEmployeesAsync(fileName);
            }
            catch
            {
                throw new InValidFileException();
            }
        }

        public async Task<List<EmployeeEntity>> DeleteByNameAsync(string name, string fileName)
        {
            _employees = await GetEmployeesAsync(fileName);
            if (_employees.Count() > 0)
            {
               _employees.Remove(_employees.FirstOrDefault(x => x.Name == name));
                await UpdateFileAsync(fileName);
                return _employees;
            }
            return _employees;
        }

        public async Task<List<EmployeeEntity>> UpdateByNameAsync(string name, EmployeeEntity employee, string fileName)
        {
            _employees = await GetEmployeesAsync(fileName);
            if (_employees.Count() > 0)
            {
                var employeeToUpdate  = _employees.FirstOrDefault(x => x.Name == name);
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.Age = employee.Age;
                employeeToUpdate.Department = employee.Department;
                employeeToUpdate.Salary = employee.Salary;
                await UpdateFileAsync(fileName);
                return _employees;
            }
            return _employees;
        }

      
        private async Task<List<EmployeeEntity>> GetEmployeesAsync(string fileName)
        {
            return (await System.IO.File.ReadAllLinesAsync(fileName))
               .ToList()
               .Select(x => x
               .Split(",")).Select(e => new EmployeeEntity { Name = e[0], Age = int.Parse(e[1]), Department = e[2], Salary = int.Parse(e[3]) }).ToList();
        }

        private async Task UpdateFileAsync(string fileName)
        {
            // xml file should also be updated 
            using (var wr = new StreamWriter(fileName))
            {
                foreach (var employee in _employees)
                {
                    wr.WriteLine($"{employee.Name},{employee.Age},{employee.Department},{employee.Salary}");
                }
            }
            _employees = await GetEmployeesAsync(fileName);
        }


    }
}
