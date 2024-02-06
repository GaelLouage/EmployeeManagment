using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Extensions
{
    public static class StringExtensions
    {
        public static string GetFileExtension(this string file)
        {
            return file.Split(".")[1];
        }
        
    }
}