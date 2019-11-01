using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DarwinCoreUtility.CSV
{
    public class CSVRow
    {
        private readonly CSVFile parentFile;
        private string[] data;
        public string[] Data { get => data; }

        public CSVRow(CSVFile parent)
        {
            parentFile = parent;
            data = new string[parentFile.HeaderFields.Count];
        }

        public string this[string key]
        {
            get
            {
                string ret;
                Get(key, out ret);
                return ret;
            }
            set
            {
                Set(key, value);
            }
        }


        public IEnumerable<KeyValuePair<string, string>> GetAllColumns()
        {
            foreach(var header in parentFile.HeaderFields)
            {
                yield return new KeyValuePair<string, string>(header.Key, data[header.Value]);
            }
        }

        public bool Get(string key, out string value)
        {
            if (parentFile.HeaderFields.ContainsKey(key))
            {
                value = data[parentFile.HeaderFields[key]];
                return true;
            }
            value = "";
            return false;
        }
        public bool Set(string key, string value)
        {
            if (parentFile.HeaderFields.ContainsKey(key))
            {
                data[parentFile.HeaderFields[key]] = value;
                return true;
            }
            return false;
        }
        public void Set(int idx, string value)
        {
            if (idx >= 0 && idx < data.Length)
            {
                data[idx] = value;
            }
        }

    }
}
