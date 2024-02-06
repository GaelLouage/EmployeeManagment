using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Employee_Data_Management_System.Factory.Classes
{
    public class XmlFile : IFileReaderFactory
    {
        public async Task<List<EmployeeEntity>> FileAsync(Microsoft.Win32.OpenFileDialog openFileDlg)
        {
            var employees = new List<EmployeeEntity>();
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
                        employees.Add(employee);
                        employee = new EmployeeEntity();
                    }
                }
            }
            return employees;
        }
    }
}
