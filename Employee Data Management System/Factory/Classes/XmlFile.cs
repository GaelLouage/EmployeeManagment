using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Employee_Data_Management_System.Factory.Classes
{
    public class XmlFile : IFileReaderFactory
    {
        private List<EmployeeEntity> _employees = new List<EmployeeEntity>();

       
        public async Task<List<EmployeeEntity>> ReadAllAsync(Microsoft.Win32.OpenFileDialog openFileDlg)
        {
            // Create an XML reader for this file.
            using (XmlReader reader = XmlReader.Create(openFileDlg.FileName))
            {
                var employee = new EmployeeEntity();
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        if(reader.Name is "Name")
                        {
                            employee.Name = reader.ReadString();
                        }
                        else if(reader.Name is "Age")
                        {
                            employee.Age = int.Parse(reader.ReadString());
                        } else if(reader.Name is "Department")
                        {
                            employee.Department = reader.ReadString();
                        } else if( reader.Name is "Salary")
                        {
                            employee.Salary = int.Parse(reader.ReadString());
                        }
                    }
                    if(!string.IsNullOrEmpty(employee.Name) && employee.Salary > 0) 
                    {
                        _employees.Add(employee);
                        employee = new EmployeeEntity();
                    }
                }
            }
            return _employees;
        }
    

        public Task<List<EmployeeEntity>> UpdateByNameAsync(string name, EmployeeEntity employee, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEntity>> DeleteByNameAsync(string name, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
