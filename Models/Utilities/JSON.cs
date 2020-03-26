using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KrishnapriyaAssignment3.Models.Utilities
{
    class JSON
    {
        Student s = new Student();
        public string JSONresult = "";

        public void serialize(List<Student> students)
        {
            Console.WriteLine("\n ========== Serialization \n");

            var json = System.Text.Json.JsonSerializer.Serialize(students);

            Console.WriteLine(json);
            String fileName = "students";

            string path2 = $"{Constants.Locations.DataFolder}\\{fileName}.json";

            //string path2 = @"D:\Student.json ";
            File.WriteAllText(path2, json);
        }
    }
}
