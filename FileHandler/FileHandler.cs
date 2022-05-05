using KBCsv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class FileHandler
    {
        private string _path;
        private const char SEPARATOR = ',';
        public FileHandler(string path)
        {
            _path = path;
        }

        public List<ContentRecord> ReadFile()
        {
            var csvOutputStrings = new List<ContentRecord>();
            var streamReader = new StreamReader(_path);
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.ValueSeparator = SEPARATOR;
                while (csvReader.HasMoreRecords)
                {
                    var record = csvReader.ReadDataRecord();

                    if(DateTime.TryParse(record[1], out var dateOfBirth) && bool.TryParse(record[2], out var married) && decimal.TryParse(record[4], out var salary))
                    {
                        var contextRecord = new ContentRecord();

                        contextRecord.Name = record[0];
                        contextRecord.DateOfBirth = dateOfBirth.Date;
                        contextRecord.Married = married;
                        contextRecord.Phone = record[3];
                        contextRecord.Salary = salary; 

                         csvOutputStrings.Add(contextRecord);
                    }
                    else
                    {
                        continue;
                    }
                    

                }
            }
            return csvOutputStrings;
        }

        
    }
    public class ContentRecord
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
