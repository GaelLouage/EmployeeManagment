using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Factory.Classes;
using Employee_Data_Management_System.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Data
{
    public static class FileExtensionsDictionary
    {
        public static Dictionary<string, Func<OpenFileDialog, Task<List<EmployeeEntity>>>> Dictionary = new Dictionary<string, Func<OpenFileDialog, Task<List<EmployeeEntity>>>>()
         {
             {"txt", async (opf) => await FileReaderFactory.Read(FileType.CSV).ReadAllAsync(opf) },
             {"xml", async (opf) => await FileReaderFactory.Read(FileType.XML).ReadAllAsync(opf) }
         };

    }
}
