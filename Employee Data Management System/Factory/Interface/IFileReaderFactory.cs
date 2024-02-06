﻿using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Factory.Interface
{
    public interface IFileReaderFactory
    {
        Task<List<EmployeeEntity>> ReadAllAsync(Microsoft.Win32.OpenFileDialog openFileDlg);

        EmployeeEntity UpdateByName(string name);
        Task<List<EmployeeEntity>> DeleteByNameAsync(string name, string fileName);
    }
}
