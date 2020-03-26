using System;
using System.Collections.Generic;
using System.Text;

namespace KrishnapriyaAssignment3.Models
{
    class Constants
    {
       // public readonly Student Student = new Student { StudentId = "200450333", FirstName = "Krishnapriya", LastName = "Sarojam" };
        public class Locations
        {
            public readonly static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            public readonly static string ExePath = Environment.CurrentDirectory;

            public readonly static string ContentFolder = $"{ExePath}\\..\\..\\..\\Content";
            public readonly static string DataFolder = $"{ExePath}\\..\\..\\..\\Content\\Data";
            public readonly static string ImageFolder = $"{ExePath}\\..\\..\\..\\Content\\Images";

            public const string InfoFile = "info.csv";
            public const string ImageFile = "myimage.jpg";

        }
        public class ftp
        {
            public const string UserName = @"bdat100119f\bdat1001";
            public const string Password = "bdat1001";

            public const string BaseUrl = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914";



            public const int OperationPauseTime = 10000;
        }
    }
}
