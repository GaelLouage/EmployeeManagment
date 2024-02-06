using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Factory.Interface
{
    public interface IFileReaderFactory
    {
        Task<List<EmployeeEntity>> FileAsync(Microsoft.Win32.OpenFileDialog openFileDlg);
    }
}
