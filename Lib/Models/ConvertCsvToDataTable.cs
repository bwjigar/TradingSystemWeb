using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Lib.Models
{
    public class ConvertCsvToDataTable
    {
        static Regex exSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

        public static string[] SplitString(string input)
        {

            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in exSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }
        public DataTable CsvToDataTable(string csvText)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(new StringReader(csvText)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read the first row as column headers
                if (!parser.EndOfData)
                {
                    string[] headers = parser.ReadFields();
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                }

                // Read the remaining rows as data
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;
        }
    }
}
