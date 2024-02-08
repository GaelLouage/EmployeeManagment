using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Singletons
{
    public  class FileSingleton
    {
        private static FileSingleton? instance;
        // thread safety
        private static readonly object padlock = new object();

        FileSingleton()
        {
        }

        public static FileSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FileSingleton();
                    }
                    return instance;
                }
            }
        }
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public OpenFileDialog OpenFileDialog { get; set; } 
    }
}
