using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public IEnumerable<string> Headers
        {
            get => DarwinData.PublicProperties;
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
            NotifyPropertyChanged("Data");
        }

        public void ExportKML(string filename)
        {
            KMLFile outputFile = new KMLFile();
            var document = new Document();
            document.Placemarks = new List<Placemark>();
            foreach (var datarow in Data)
            {
                document.Placemarks.Add(new Placemark()
                {
                    Name = datarow.Genus,
                    Description = datarow.Locality,
                    Point = new PlacemarkPoint(datarow.DecimalLatitude, datarow.DecimalLongitude)
                });
            }
            outputFile.Document = document;
            KMLFile.Save(outputFile, filename);
            
        }


    }
}
