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
    public class KMLFileSettings : INotifyPropertyChanged
    {
        public static readonly string DefaultSettings = "Settings.dcsettings";
        private static string LastSettingsFile = DefaultSettings;
        public delegate void SettingsLoaded(KMLFileSettings newsettings);
        public static SettingsLoaded KMLSettingsLoaded;

        private static KMLFileSettings currentSettings = null;
        public static KMLFileSettings CurrentSettings
        {
            set
            {
                currentSettings = value;
                KMLSettingsLoaded?.Invoke(currentSettings);
            }
            get
            {
                if (currentSettings == null)
                {
                    //currentSettings = XmlUtils.Load<KMLFileSettings>(DefaultSettings);
                    //if (currentSettings == null)
                    {
                        currentSettings = new KMLFileSettings();
                    }
                }
                return currentSettings;
            }
        }

        [XmlArray("FolderGroupings"), XmlArrayItem("FolderGrouping")]
        public ObservableCollection<string> FolderGrouping { get; set; } = new ObservableCollection<string>();

        [XmlArray("ColorGroupings"), XmlArrayItem("ColorGrouping")]
        public ObservableCollection<string> ColorGrouping { get; set; } = new ObservableCollection<string>();

        [XmlIgnore]
        public ObservableCollection<Folder> FolderStructure { get => DarwinDataModel.CurrentData.FolderStructure; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string placemarkNameFormat = "";
        public string PlacemarkNameFormat
        {
            get { return placemarkNameFormat; }
            set { placemarkNameFormat = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(PlacemarkNameFormatPreview)); }
        }

        [XmlIgnore]
        public string PlacemarkNameFormatPreview
        {
            get {

                if (DarwinDataModel.CurrentData.Data.Count > 0)
                {
                    return DarwinDataModel.CurrentData.ResolveFields(placemarkNameFormat);
                }
                else
                {
                    return placemarkNameFormat;
                }
            }
        }

        public static readonly string PlacemarkDescriptionWrapperFormat = "<div class=\"googft-info-window\">{0}</div>";// May or may not need this <![CDATA[ ]]>

        private string placemarkDescriptionFormat = "";
        public string PlacemarkDescriptionFormat
        {
            get => placemarkDescriptionFormat;
            set {
                placemarkDescriptionFormat = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PlacemarkDescriptionFormatPreview));
            }
        }

        public string LatitudeField { get; set; }
        public string LongitudeField { get; set; }

        [XmlIgnore]
        public string PlacemarkDescriptionFormatPreview
        {
            get {

                if (DarwinDataModel.CurrentData.Data.Count > 0)
                {
                 return DarwinDataModel.CurrentData.ResolveFields(placemarkDescriptionFormat);
                }
                else
                {
                    return PlacemarkDescriptionFormat;
                }
            }
        }

        public KMLFileSettings()
        {
            DarwinDataModel.CurrentData.PropertyChanged += CurrentData_PropertyChanged;
        }

        public static void Load(string path = "")
        {
            if (!String.IsNullOrEmpty(path))
            {
                LastSettingsFile = path;
            }
            CurrentSettings = XmlUtils.Load<KMLFileSettings>(LastSettingsFile);
        }

        public static void Save(string path="")
        {
            if (!String.IsNullOrEmpty(path))
            {
                LastSettingsFile = path;                
            }
 
            XmlUtils.Save(CurrentSettings, LastSettingsFile);
        }

        public void AddColorGrouping(string name)
        {
            this.ColorGrouping.Add(name);
        }
        public void RemoveColorGrouping(string name)
        {
            this.ColorGrouping.Remove(name);
        }

        public bool MoveColorGrouping(string name, int direction)
        {
            int idx = this.ColorGrouping.IndexOf(name);
            if (idx + direction >= 0 && idx + direction < this.ColorGrouping.Count)
            {
                this.ColorGrouping.Move(idx, idx + direction);
                return true;
            }
            return false;
        }


        public void AddFolderGrouping(string name)
        {
            this.FolderGrouping.Add(name);
            DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
        }
        public void RemoveFolderGrouping(string name)
        {
            this.FolderGrouping.Remove(name);
            DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
        }

        public bool MoveFolderGrouping(string name, int direction)
        {
            int idx = this.FolderGrouping.IndexOf(name);
            if (idx + direction >= 0 && idx + direction < this.FolderGrouping.Count)
            {
                this.FolderGrouping.Move(idx, idx + direction);
                DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
                return true;
            }
            return false;
        }

        private void CurrentData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Data":
                    {
                        DarwinDataModel.CurrentData.GenerateFolderStructure(FolderGrouping.ToArray());
                        NotifyPropertyChanged(nameof(PlacemarkNameFormatPreview));
                        NotifyPropertyChanged(nameof(PlacemarkDescriptionFormatPreview));
                    }
                    break;

                case "SelectedData":
                    {
                        NotifyPropertyChanged(nameof(PlacemarkNameFormatPreview));
                        NotifyPropertyChanged(nameof(PlacemarkDescriptionFormatPreview));
                    }
                    break;
            }
        }    

    }
}
