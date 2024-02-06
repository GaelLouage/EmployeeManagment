using Employee_Data_Management_System.CustomExceptions;
using Employee_Data_Management_System.Factory.Interface;
using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Factory.Classes
{
    public class CsVFile : IFileReaderFactory
    {
        public async Task<List<EmployeeEntity>> FileAsync(Microsoft.Win32.OpenFileDialog openFileDlg)
        {
            try
            {
                return (await System.IO.File.ReadAllLinesAsync(openFileDlg.FileName))
                   .Skip(1)
                   .ToList()
                   .Select(x => x
                   .Split(",")).Select(e => new EmployeeEntity { Name = e[0], Age = int.Parse(e[1]), Department = e[2], Salary = int.Parse(e[3]) }).ToList();
            }
            catch
            {
                throw new InValidFileException();
            }
        }
    }
}
