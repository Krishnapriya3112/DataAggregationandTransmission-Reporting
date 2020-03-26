using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace KrishnapriyaAssignment3.Models.Utilities
{
    class XML
    {
        public XDocument ConvertCsvToXML(string csvString, string[] separatorField)

        {
            //split the rows
            var sep = new[] { "\r\n" };
            string[] rows = csvString.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            //Create the declaration
            var xsurvey = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));
            var xroot = new XElement("root"); //Create the root
            for (int i = 0; i < rows.Length; i++)
            {
                //Create each row
                if (i > 0)
                {
                    xroot.Add(rowCreator(rows[i], rows[0], separatorField));
                }
            }
            xsurvey.Add(xroot);
            return xsurvey;
        }

        private static XElement rowCreator(string row,
                       string firstRow, string[] separatorField)
        {

            string[] temp = row.Split(separatorField, StringSplitOptions.None);
            string[] names = firstRow.Split(separatorField, StringSplitOptions.None);
            var xrow = new XElement("row");
            for (int i = 0; i < temp.Length; i++)
            {

                var xvar = new XElement("var",
                                        new XAttribute("name", names[i]),
                                        new XAttribute("value", temp[i]));
                xrow.Add(xvar);
            }
            return xrow;
        }
    }
}
