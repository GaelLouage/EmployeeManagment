using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.CustomExceptions
{
    public class InValidFileException : Exception
    {
        public InValidFileException() : base("Invalid file format or content.")
        {
        }
    }
}
