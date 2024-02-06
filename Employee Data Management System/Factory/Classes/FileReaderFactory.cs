using Employee_Data_Management_System.Enum;
using Employee_Data_Management_System.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Data_Management_System.Factory.Classes
{
    public class FileReaderFactory
    {
        public static IFileReaderFactory Read(FileType fileType)
        {
            switch(fileType)
            {
                case FileType.CSV:
                    return new CsVFile();
                case FileType.XML:
                    return new XmlFile();
                default:
                    throw new NotSupportedException($"{fileType} is not currently supported!");
            }
        }
    }
}
