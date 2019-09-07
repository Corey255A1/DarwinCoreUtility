using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DarwinCoreUtility.Darwin;
using DarwinCoreUtility.Utils;
namespace DarwinCoreUtility.KML
{
    [XmlRoot("KMLSettings")]
    public class KMLFileSettings
    {
        private static KMLFileSettings currentSettings = null;
        public static KMLFileSettings CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                {
                    currentSettings = XmlUtils.Load<KMLFileSettings>("Settings.kmlsettings");//= new KMLFileSettings();
                }
                return currentSettings;
            }
        }

        [XmlIgnore]
        public ObservableCollection<Folder> FolderStructure { get; set; } = new ObservableCollection<Folder>();

        [XmlArray("FolderGroupings"), XmlArrayItem("FolderGrouping")]
        public ObservableCollection<string> FolderGrouping { get; set; } = new ObservableCollection<string>();

        public KMLFileSettings()
        {
            DarwinDataModel.CurrentData.PropertyChanged += CurrentData_PropertyChanged;
        }

        public void Load()
        {
            var loadedSettings = XmlUtils.Load<KMLFileSettings>("Settings.kmlsettings");
            FolderGrouping.Clear();
            foreach (string grouping in loadedSettings.FolderGrouping) { FolderGrouping.Add(grouping); }
            GenerateFolderStructure();
        }

        public void Save()
        {
            XmlUtils.Save(this, "Settings.kmlsettings");
        }


        public void AddGrouping(string name)
        {
            currentSettings.FolderGrouping.Add(name);            
            currentSettings.GenerateFolderStructure();
        }
        public void RemoveGrouping(string name)
        {
            currentSettings.FolderGrouping.Remove(name);
            currentSettings.GenerateFolderStructure();
        }

        public bool MoveGrouping(string name, int direction)
        {
            int idx = currentSettings.FolderGrouping.IndexOf(name);
            if (idx + direction >= 0 && idx + direction < currentSettings.FolderGrouping.Count)
            {
                currentSettings.FolderGrouping.Move(idx, idx + direction);
            }
            return false;
        }

        private void CurrentData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Data")
            {
                GenerateFolderStructure();
            }            
        }

        private void GenerateFolderStructure(IEnumerable<DarwinData> enumerations, string[] propertyNames, int propertyNameIndex, Folder parentFolder)
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
                parentFolder.Folders.Add(f);
            }
            parentFolder.Folders.Sort((a, b) => { return String.Compare(a.Name, b.Name); });
        }

        private void GenerateFolderStructure()
        {
            FolderStructure.Clear();
            var f = new Folder() { Name = "root" };
            f.Folders = new List<Folder>();
            GenerateFolderStructure(DarwinDataModel.CurrentData.Data, FolderGrouping.ToArray(), 0, f);
            f.Folders.ForEach(add => FolderStructure.Add(add));
        }




    }
}
