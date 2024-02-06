using Employee_Data_Management_System.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Employee_Data_Management_System.Helpers
{
    public static class FileDialog
    {
        public static OpenFileDialog? Open()
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            bool? result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result is not true || result is null)
            {
                //MessageBox.Show("File not supported!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return openFileDlg;
        }
    }
}
