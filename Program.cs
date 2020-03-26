using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace KrishnapriyaAssignment3.Models.Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914";
            Student student = new Student();
            // 1. Retrieve a list of directories from the FTP
            FTP ftpApp = new FTP();
            Console.WriteLine("-----------List of Directories--------------");
            List<Student> students = student.FromCSV(url);
            JSON json = new JSON();
            XML xml = new XML();
            json.serialize(students);
            student.ToCSV(students);
          

            string CSVPath = @"C:\Users\KP\Desktop\info.csv";
            string fileName = "students";

 

            string XMLPath = $"{Constants.Locations.DataFolder}\\{fileName}.xml";
            string JsonPath = $"{Constants.Locations.DataFolder}\\{fileName}.json";
            string csv = File.ReadAllText(CSVPath);
            XDocument doc = xml.ConvertCsvToXML(csv, new[] { "," });
            doc.Save(XMLPath);
            ftpApp.UploadFile(@"C:\Users\KP\Desktop\info.csv", url, "/students.csv");
            ftpApp.UploadFile(JsonPath, url, "/students.json");
            ftpApp.UploadFile(XMLPath, url, "/students.xml");

            int countOfCorrectAges = 0;
            int sumOfAges = 0;

            int countOfWrongAge = 0;
            List<int> ages = new List<int>();
            int highestAge = 0;
            int lowestAge = 100;

            Console.WriteLine("No of students:");
            Console.WriteLine(students.Count);
            int count = 0, containsCount = 0;
            foreach (var stud in students)
            {

                if (stud.FirstName.StartsWith('K'))
                {
                    count++;
                }

                if (stud.FirstName.Contains('r'))
                {
                    containsCount++;
                }
            }
            Console.WriteLine("Number of students starting with  K");
            Console.WriteLine(count);
            Console.WriteLine("No of students whose name contains r ");
            Console.WriteLine(containsCount);

            foreach (var stud in students)
            {
                ages.Add(stud.Age);
                if (stud.Age > 0 && stud.Age < 100)
                {
                    countOfCorrectAges++;
                    sumOfAges += stud.Age;
                    if (stud.Age > highestAge)
                    {
                        highestAge = stud.Age;
                    }
                    if (stud.Age < lowestAge)
                    {
                        lowestAge = stud.Age;
                    }
                }
                else
                {
                    countOfWrongAge++;
                }

                double avgOfAges = sumOfAges / countOfCorrectAges;
                Console.WriteLine($"Average of ages of {countOfCorrectAges} students : {avgOfAges}");
                Console.WriteLine($"Highest age : {highestAge}");
                Console.WriteLine($"Lowest age : {lowestAge}");
                Console.WriteLine("-----------------------------------------------");
            }

        }
    }
}
