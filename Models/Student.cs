using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KrishnapriyaAssignment3.Models.Utilities
{
    class Student
    {
        public string HeaderRow = $"{nameof(Student.StudentId)},{nameof(Student.FirstName)},{nameof(Student.LastName)},{nameof(Student.DateOfBirth)},{nameof(Student.Age)},{nameof(Student.ImageData)},{nameof(Student.MyRecord)}";
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string _DateOfBirth;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set
            {
                _DateOfBirth = value;

                //Convert DateOfBirth to DateTime
                // DateTime dtOut;
                var formatInfo = new System.Globalization.DateTimeFormatInfo();
                formatInfo.ShortDatePattern = "MM/dd/yyyy";
                DateTime dtOut = DateTime.Parse(_DateOfBirth, formatInfo);
                DateOfBirthDT = dtOut;
            }
        }

        //public DateTime DateOfBirthDT { get; internal set; }



        public int headerNum = 0;
        List<Student> students = new List<Student>();

        public virtual int Age
        {
            get
            {
                DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(DateOfBirthDT).Ticks).Year - 1;
                DateTime PastYearDate = DateOfBirthDT.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return Years;
            }
        }

        private int _Age;
        // read-only Age
        public DateTime DateOfBirthDT { get; internal set; }
        public string ImageData { get; set; }
        public bool MyRecord { get; set; }

        public string Directory { get; set; }

        public void FromDirectory(string directory)
        {
            Directory = directory;  // Is this line to set value for Directory?

            if (String.IsNullOrEmpty(directory.Trim()))
            {
                return;
            }

            string[] data = directory.Trim().Split(" ", StringSplitOptions.None);

            StudentId = data[0];
            FirstName = data[1];
            LastName = data[2];
        }


        public List<Student> FromCSV(string url)
        {
            FTP ftpApp = new FTP();
            List<string> directories = ftpApp.GetDirectory(url);
            foreach (var directory in directories)
            {
                Student student = new Student();
                student.FromDirectory(directory);
                Console.WriteLine(directory);
                List<string> files = ftpApp.GetDirectory(url + "/" + directory);
                foreach (var file in files)
                {
                    Console.WriteLine("\t" + file);
                }
                string filePath = url + "/" + directory + "/info.csv";
                byte[] bytes = ftpApp.DownloadFileBytes(filePath);
                string csv = Encoding.Default.GetString(bytes);
                string[] csvlines = csv.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                if (csvlines.Length != 2)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} has Error in CSV format");
                }
                else
                {
                    string[] data = csvlines[1].Split(",", StringSplitOptions.None);
                    student.StudentId = data[0];
                    student.FirstName = data[1];
                    student.LastName = data[2];
                    student.DateOfBirth = data[3];
                    student.ImageData = data[4];
                    if (student.StudentId == "200450333")
                    {
                        student.MyRecord = true;
                    }
                    else
                    {
                        student.MyRecord = false;
                    }
                    students.Add(student);
                }
            }
            return students;
        }
        public void ToCSV(List<Student> students)
        {
            string studentCSVPath = @"C:\Users\KP\Desktop\info.csv";

            //Establish a file stream to collect data from the response. this point is local disk.
            using (StreamWriter fs = new StreamWriter(studentCSVPath))
            {
                foreach (var student in students)
                {
                    if (headerNum == 0)
                    {
                        fs.Write(student.HeaderRow + "\r\n");
                        headerNum++;
                    }
                    else
                    {
                        fs.Write(student.ToString() + "\r\n");
                    }

                }
            }
        }
        public override string ToString()
        {
            string result = $"{StudentId},{FirstName},{LastName},{DateOfBirth},{Age},{ImageData},{MyRecord}";
            return result;
        }
        public string ToCSV1()
        {
            string result = $"{StudentId},{FirstName},{LastName},{DateOfBirthDT.ToShortDateString()},{Age},{ImageData},{MyRecord}";
            return result;
        }

    }
}
