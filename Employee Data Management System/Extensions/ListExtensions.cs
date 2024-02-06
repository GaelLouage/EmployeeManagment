using Employee_Data_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Extensions
{
    public static class ListExtensions
    {
        public static List<EmployeeEntity> Sort(this List<EmployeeEntity> employees, string search, string orderedChoice)
        {
            var searchTextLower = search?.ToLower() ?? ""; // Convert search text to lowercase

            var filteredList = employees.Where(x => x.Name.ToLower().Contains(searchTextLower)).ToList();

            switch (orderedChoice)
            {
                case "Age":
                    return filteredList.OrderBy(x => x.Age).ToList();
                case "Department":
                    return filteredList.OrderBy(x => x.Department).ToList();
                case "Salary":
                    return filteredList.OrderBy(x => x.Salary).ToList();
                default:
                    return filteredList.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
