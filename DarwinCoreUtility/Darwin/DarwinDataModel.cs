using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DarwinCoreUtility.CSV;
using DarwinCoreUtility.KML;

namespace DarwinCoreUtility.Darwin
{
    public class DarwinDataModel: INotifyPropertyChanged
    {

        #region Static Singleton
        private static DarwinDataModel currentData = new DarwinDataModel();
        public static DarwinDataModel CurrentData
        {
            get => currentData;
            private set { currentData = value; }
        }

        public static void LoadFile(string filepath)
        {
            CurrentData.Load(CSVFile.Load(filepath));
        }

        public static void LoadCSV(CSVFile file)
        {
            CurrentData.Load(file);
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private ObservableCollection<DarwinData> data = new ObservableCollection<DarwinData>();
        public ObservableCollection<DarwinData> Data
        {
            get => data;
            set
            {
                data = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Folder> FolderStructure { get; set; } = new ObservableCollection<Folder>();

        public IEnumerable<string> Headers
        {
            get => DarwinData.PublicProperties;
        }

        private DarwinData selectedData;

        public DarwinData SelectedData
        {
            get => selectedData;
            set
            {
                selectedData = value;
                NotifyPropertyChanged();
            }
        }

        public DarwinDataModel(){}
        public DarwinDataModel(CSVFile file)
        {
            foreach (var row in file)
            {
                data.Add(new DarwinData(row));
            }
        }
        public void Load(CSVFile file)
        {
            data.Clear();
            foreach (var row in file)
            {
                data.Add(new DarwinData(row));
            }
            if (data.Count > 0)
            {
                SelectedData = data[0];
            }
            NotifyPropertyChanged("Data");

        }

        public void ExportKML(string filename)
        {
            Utils.ColorIterator.Reset();
            KMLFile outputFile = new KMLFile();
            var document = new Document();

            document.Styles = new List<Style>();
            GenerateStyles(Data, KMLFileSettings.CurrentSettings.ColorGrouping.ToArray(), 0, "", document.Styles);

            document.Folders = new List<Folder>();

            GenerateFolderStructure(KMLFileSettings.CurrentSettings.FolderGrouping.ToArray());         

            foreach (Folder f in FolderStructure)
            {
                document.Folders.Add(f);
            }



            outputFile.Document = document;
            KMLFile.Save(outputFile, filename);
            
        }
        public string ResolveFields(string formatstring)
        {
            return ResolveFields(formatstring, SelectedData);
        }
        public string ResolveFields(string formatstring, int dataIdx)
        {
            var selectedData = Data[dataIdx];
            return ResolveFields(formatstring, selectedData);
        }
        public static string ResolveFields(string formatstring, DarwinData selectedData)
        {
            if (selectedData == null) return "";

            int openBracket = -1;
            int closeBracket = 0;
            StringBuilder formatBuilder = new StringBuilder(formatstring);
            while ((openBracket = formatstring.IndexOf("[[", closeBracket)) != -1)
            {
                closeBracket = formatstring.IndexOf("]]", openBracket);
                string field = formatstring.Substring(openBracket + 2, closeBracket - openBracket - 2);
                string resolved = selectedData[field];
                if (resolved != null)
                {
                    formatBuilder.Replace($"[[{field}]]", resolved);
                }
            }
            return formatBuilder.ToString();
        }

        public static string GetStyleURL(DarwinData d)
        {
            string url = "#";
            foreach(var field in KMLFileSettings.CurrentSettings.ColorGrouping)
            {
                url += $"{d[field]}-";
            }
            url = url.Trim('-');
            if (url == "#") url = "#[Not_Specified]";
            return url;
        }

        private static Placemark GeneratePlacemark(DarwinData d)
        { 
            return new Placemark() {
                Name = ResolveFields(KMLFileSettings.CurrentSettings.PlacemarkNameFormat, d),
                Description = String.Format(KMLFileSettings.PlacemarkDescriptionWrapperFormat, 
                                                ResolveFields(KMLFileSettings.CurrentSettings.PlacemarkDescriptionFormat, d)),
                StyleURL = GetStyleURL(d),
                Point = new PlacemarkPoint(d.DecimalLatitude, d.DecimalLongitude)
            };
        }

        public void GenerateFolderStructure(string[] propertyNames)
        {
            FolderStructure.Clear();
            var f = new Folder() { Name = "root" };
            f.Folders = new List<Folder>();
            DarwinDataModel.GenerateFolderStructure(Data, propertyNames, 0, f);
            f.Folders.ForEach(add => FolderStructure.Add(add));
        }


        private static void GenerateFolderStructure(IEnumerable<DarwinData> enumerations, string[] propertyNames, int propertyNameIndex, Folder parentFolder)
        {
            if (propertyNames.Length <= 0 || enumerations.Count() == 0) return;

            var grouping = enumerations.GroupBy(datum => datum[propertyNames[propertyNameIndex]]);
            foreach (var g in grouping)
            {
                //Console.WriteLine(new string('\t',propertyNameIndex) + g.Key);
                var f = new Folder() { Name = string.IsNullOrEmpty(g.Key) ? "[Not Specified]" : g.Key };
                if (propertyNameIndex + 1 < propertyNames.Length)
                {
                    f.Folders = new List<Folder>();
                    GenerateFolderStructure(g, propertyNames, propertyNameIndex + 1, f);
                }
                else
                {
                    f.Placemarks = new List<Placemark>();
                    foreach (var d in g)
                    {
                        f.Placemarks.Add(GeneratePlacemark(d));
                    }
                }
                parentFolder.Folders.Add(f);
            }
            parentFolder.Folders.Sort((a, b) => { return String.Compare(a.Name, b.Name); });
        }

        

        private static void GenerateStyles(IEnumerable<DarwinData> enumerations, string[] propertyNames, int propertyNameIndex, string name, List<Style> styleList)
        {
            if (propertyNames.Length <= 0 || enumerations.Count() == 0) return;

            var grouping = enumerations.GroupBy(datum => datum[propertyNames[propertyNameIndex]]);
            foreach (var g in grouping)
            {
                var styleName = g.Key==null ? "" : g.Key;
                if (propertyNameIndex + 1 < propertyNames.Length)
                {
                    GenerateStyles(g, propertyNames, propertyNameIndex + 1, String.IsNullOrEmpty(name)?styleName:$"{name}-{styleName}", styleList);
                }
                else
                {
                    var s = new Style() {
                        ID = (name != "" ? $"{name}-{styleName}" : styleName).Trim('-'),
                        IconStyle = new IconStyle()
                        {
                            ColorMode = "normal",
                            Icon = new Icon() { Href = "http://maps.google.com/mapfiles/kml/paddle/wht-blank.png" },
                            Color = Utils.ColorIterator.NextColor(129).HexABGR
                        }
                    };
                    if (string.IsNullOrEmpty(s.ID)) s.ID = "[Not_Specified]";
                    styleList.Add(s);
                }
            }
        }


    }
}
