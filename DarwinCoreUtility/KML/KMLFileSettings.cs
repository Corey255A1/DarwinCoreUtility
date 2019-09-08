using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DarwinCoreUtility.Darwin;
using DarwinCoreUtility.Utils;
namespace DarwinCoreUtility.KML
{
    [XmlRoot("KMLSettings")]
    public class KMLFileSettings: INotifyPropertyChanged
    {
        private static KMLFileSettings currentSettings = null;
        public static KMLFileSettings CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                {
                    currentSettings = XmlUtils.Load<KMLFileSettings>("Settings.kmlsettings");
                    if (currentSettings == null)
                    {
                        currentSettings = new KMLFileSettings();
                    }
                }
                return currentSettings;
            }
        }


        [XmlArray("FolderGroupings"), XmlArrayItem("FolderGrouping")]
        public ObservableCollection<string> FolderGrouping { get; set; } = new ObservableCollection<string>();

        [XmlIgnore]
        public ObservableCollection<Folder> FolderStructure { get => DarwinDataModel.CurrentData.FolderStructure; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string placemarkNameFormat = "";
        public string PlacemarkNameFormat
        {
            get { return placemarkNameFormat; }
            set { placemarkNameFormat = value; NotifyPropertyChanged(); }
        }


        public KMLFileSettings()
        {
            DarwinDataModel.CurrentData.PropertyChanged += CurrentData_PropertyChanged;
        }

        public void Load()
        {
            var loadedSettings = XmlUtils.Load<KMLFileSettings>("Settings.kmlsettings");
            FolderGrouping.Clear();
            foreach (string grouping in loadedSettings.FolderGrouping) { FolderGrouping.Add(grouping); }
            DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
        }

        public void Save()
        {
            XmlUtils.Save(this, "Settings.kmlsettings");
        }


        public void AddGrouping(string name)
        {
            currentSettings.FolderGrouping.Add(name);
            DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
        }
        public void RemoveGrouping(string name)
        {
            currentSettings.FolderGrouping.Remove(name);
            DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
        }

        public bool MoveGrouping(string name, int direction)
        {
            int idx = currentSettings.FolderGrouping.IndexOf(name);
            if (idx + direction >= 0 && idx + direction < currentSettings.FolderGrouping.Count)
            {
                currentSettings.FolderGrouping.Move(idx, idx + direction);
                DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
                return true;
            }
            return false;
        }

        private void CurrentData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Data")
            {
                DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
            }            
        }

        






    }
}
