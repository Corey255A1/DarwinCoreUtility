using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace DarwinCoreUtility.CSV
{
    public class CSVFile: IEnumerable<CSVRow>
    {
        public readonly Dictionary<string, int> HeaderFields = new Dictionary<string, int>();
        public readonly List<CSVRow> Data = new List<CSVRow>();
        public CSVRow NewRow() { return new CSVRow(this); }

        public CSVFile(string[] header)
        {
            for(int i=0; i < header.Length; ++i)
            {
                HeaderFields.Add(header[i], i);
            }
        }

        public static CSVFile Load(string filepath, char delim = '\t', bool firstlineheader = true)
        {
            try
            {
                CSVFile csvfile;
                string[] lines = File.ReadAllLines(filepath);
                if(lines.Length == 0)
                {
                    return null;
                }

                string[] row = lines[0].Split(new char[] { delim });
                if (firstlineheader)
                {
                    csvfile = new CSVFile(row);
                }
                else
                {
                    csvfile = new CSVFile(row);
                    var datarow = csvfile.NewRow();
                    for(int i = 0; i < row.Length; i++)
                    {
                        csvfile.HeaderFields[i.ToString()] = i;
                        datarow.Set(i, row[i]);
                    }
                    csvfile.Data.Add(datarow);
                }

                for(int lineIdx=1;lineIdx < lines.Length; ++lineIdx)
                {
                    row = lines[lineIdx].Split(new char[] { delim });
                    var datarow = csvfile.NewRow();
                    for (int i = 0; i < row.Length; i++)
                    {
                        datarow.Set(i, row[i]);
                    }
                    csvfile.Data.Add(datarow);
                }
                return csvfile;
            }
            catch (Exception e)
            {
                //To Do, error system
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public dynamic this[int index]
        {
            get
            {
                return Data[index];
            }
        }

        public IEnumerator<CSVRow> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
