using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DarwinCoreUtility.CSV;
using System.Linq;
namespace DarwinCoreUtility.Darwin
{
    public class DarwinData: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DarwinData(CSVRow rowofdata)
        {
            foreach(FieldInfo fieldInfo in typeof(DarwinData).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldInfo.Name == nameof(PropertyChanged)) continue;
                fieldInfo.SetValue(this, rowofdata[fieldInfo.Name]);
            }
        }

        public static IEnumerable<string> PublicProperties
        {
            get {
              foreach(var prop in typeof(DarwinData).GetProperties(BindingFlags.Public | BindingFlags.Instance)) { yield return prop.Name; }
            }
        }


        private string catalogNumber;
        public string CatalogNumber
        {
            get => catalogNumber;
            set
            {
                catalogNumber = value;
                NotifyPropertyChanged();
            }
        }
        private string individualCount;
        public string IndividualCount
        {
            get => individualCount;
            set
            {
                individualCount = value;
                NotifyPropertyChanged();
            }
        }
        private string sex;
        public string Sex
        {
            get => sex;
            set
            {
                sex = value;
                NotifyPropertyChanged();
            }
        }
        private string lifeStage;
        public string LifeStage
        {
            get => lifeStage;
            set
            {
                lifeStage = value;
                NotifyPropertyChanged();
            }
        }
        private string recordNumber;
        public string RecordNumber
        {
            get => recordNumber;
            set
            {
                recordNumber = value;
                NotifyPropertyChanged();
            }
        }
        private string recordedBy;
        public string RecordedBy
        {
            get => recordedBy;
            set
            {
                recordedBy = value;
                NotifyPropertyChanged();
            }
        }
        private string eventDate;
        public string EventDate
        {
            get => eventDate;
            set
            {
                eventDate = value;
                NotifyPropertyChanged();
            }
        }
        private string habitat;
        public string Habitat
        {
            get => habitat;
            set
            {
                habitat = value;
                NotifyPropertyChanged();
            }
        }
        private string locality;
        public string Locality
        {
            get => locality;
            set
            {
                locality = value;
                NotifyPropertyChanged();
            }
        }
        private string county;
        public string County
        {
            get => county;
            set
            {
                county = value;
                NotifyPropertyChanged();
            }
        }
        private string stateProvince;
        public string StateProvince
        {
            get => stateProvince;
            set
            {
                stateProvince = value;
                NotifyPropertyChanged();
            }
        }
        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                NotifyPropertyChanged();
            }
        }
        private string minimumElevationInMeters;
        public string MinimumElevationInMeters
        {
            get => minimumElevationInMeters;
            set
            {
                minimumElevationInMeters = value;
                NotifyPropertyChanged();
            }
        }
        private string decimalLatitude;
        public string DecimalLatitude
        {
            get => decimalLatitude;
            set
            {
                decimalLatitude = value;
                NotifyPropertyChanged();
            }
        }
        private string decimalLongitude;
        public string DecimalLongitude
        {
            get => decimalLongitude;
            set
            {
                decimalLongitude = value;
                NotifyPropertyChanged();
            }
        }
        private string coordinateUncertaintyInMeters;
        public string CoordinateUncertaintyInMeters
        {
            get => coordinateUncertaintyInMeters;
            set
            {
                coordinateUncertaintyInMeters = value;
                NotifyPropertyChanged();
            }
        }
        private string genus;
        public string Genus
        {
            get => genus;
            set
            {
                genus = value;
                NotifyPropertyChanged();
            }
        }
        private string specificEpithet;
        public string SpecificEpithet
        {
            get => specificEpithet;
            set
            {
                specificEpithet = value;
                NotifyPropertyChanged();
            }
        }
        private string subspecies;
        public string Subspecies
        {
            get => subspecies;
            set
            {
                subspecies = value;
                NotifyPropertyChanged();
            }
        }
        private string family;
        public string Family
        {
            get => family;
            set
            {
                family = value;
                NotifyPropertyChanged();
            }
        }
        private string order;
        public string Order
        {
            get => order;
            set
            {
                order = value;
                NotifyPropertyChanged();
            }
        }
        private string preparations;
        public string Preparations
        {
            get => preparations;
            set
            {
                preparations = value;
                NotifyPropertyChanged();
            }
        }
        private string institutionCode;
        public string InstitutionCode
        {
            get => institutionCode;
            set
            {
                institutionCode = value;
                NotifyPropertyChanged();
            }
        }
        private string collectionCode;
        public string CollectionCode
        {
            get => collectionCode;
            set
            {
                collectionCode = value;
                NotifyPropertyChanged();
            }
        }
        private string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
        private string identifiedBy;
        public string IdentifiedBy
        {
            get => identifiedBy;
            set
            {
                identifiedBy = value;
                NotifyPropertyChanged();
            }
        }
        private string dateIdentified;
        public string DateIdentified
        {
            get => dateIdentified;
            set
            {
                dateIdentified = value;
                NotifyPropertyChanged();
            }
        }
        private string occurrenceRemarks;
        public string OccurrenceRemarks
        {
            get => occurrenceRemarks;
            set
            {
                occurrenceRemarks = value;
                NotifyPropertyChanged();
            }
        }
        private string ownerInstitutionCode;
        public string OwnerInstitutionCode
        {
            get => ownerInstitutionCode;
            set
            {
                ownerInstitutionCode = value;
                NotifyPropertyChanged();
            }
        }
        private string recordEnteredBy;
        public string RecordEnteredBy
        {
            get => recordEnteredBy;
            set
            {
                recordEnteredBy = value;
                NotifyPropertyChanged();
            }
        }
        private string references;
        public string References
        {
            get => references;
            set
            {
                references = value;
                NotifyPropertyChanged();
            }
        }


    }
}
