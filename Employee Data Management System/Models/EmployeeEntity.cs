using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee_Data_Management_System.Models
{
    public class EmployeeEntity
    {
        private int _age;
        public string Name { get; set; }
    
        public string Department { get; set; }
        public int Age 
        {
            get => _age; 
            set 
            {
                if(value < 18)
                {
                    throw new ArgumentException($"{value} is not a valid age");
                }
                _age = value;
            }
        }
        public int Salary { get;set; }
    }
}
