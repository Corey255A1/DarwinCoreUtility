using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DarwinCoreUtility.CSV
{

    public class CSVFile : IEnumerable<CSVRow>
    {
        public readonly Dictionary<string, int> HeaderFields = new Dictionary<string, int>();
        public readonly Dictionary<DataRow, CSVRow> TableToCSVMap = new Dictionary<DataRow, CSVRow>();
        public readonly List<CSVRow> Data = new List<CSVRow>();
        public DataTable Table { get; set; } = new DataTable();
        public CSVRow NewRow() { return new CSVRow(this); }

        public CSVFile(string[] header)
        {
            for (int i = 0; i < header.Length; ++i)
            {
                HeaderFields.Add(header[i], i);
                Table.Columns.Add(header[i]);
            }
        }

        public DataView DefaultView
        {
            get => Table.DefaultView;
        }

        public static CSVFile Load(string filepath, char delim = '\t', bool firstlineheader = true)
        {
            try
            {
                CSVFile csvfile;
                string[] lines = File.ReadAllLines(filepath);
                if (lines.Length == 0)
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
                    for (int i = 0; i < row.Length; i++)
                    {
                        csvfile.HeaderFields[i.ToString()] = i;
                        datarow.Set(i, row[i]);
                    }
                    csvfile.TableToCSVMap.Add(csvfile.Table.Rows.Add(datarow.Data), datarow);
                    csvfile.Data.Add(datarow);
                }

                for (int lineIdx = 1; lineIdx < lines.Length; ++lineIdx)
                {
                    row = lines[lineIdx].Split(new char[] { delim });
                    var datarow = csvfile.NewRow();
                    for (int i = 0; i < row.Length; i++)
                    {
                        datarow.Set(i, row[i]);
                    }
                    csvfile.TableToCSVMap.Add(csvfile.Table.Rows.Add(datarow.Data), datarow);
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

        public int Count
        {
            get => Table.Rows.Count;
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
