using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DarwinCoreUtility.CSV;

namespace DarwinCoreUtility.Darwin
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class DarwinField : System.Attribute
    {
        public string ColumnHeader;
        public DarwinField(string columnHeader) { ColumnHeader = columnHeader; }
    }

    public class DarwinData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DarwinData(CSVRow rowofdata)
        {
            foreach (PropertyInfo propertyInfo in PropertyMap.Values)
            {
                propertyInfo.SetValue(this, rowofdata[PropertyToColumnHeader[propertyInfo.Name]]);
            }
        }

        private static Dictionary<string, string> propertyToColumnHeader = null;
        public static Dictionary<string, string> PropertyToColumnHeader
        {
            get
            {
                if (propertyToColumnHeader == null)
                {
                    propertyToColumnHeader = new Dictionary<string, string>();
                    foreach (PropertyInfo propertyInfo in PropertyMap.Values)
                    {
                        var name = (propertyInfo.GetCustomAttribute(typeof(DarwinField)) as DarwinField).ColumnHeader;
                        propertyToColumnHeader.Add(propertyInfo.Name, name);
                    }
                }
                return propertyToColumnHeader;
            }
        }


        private static Dictionary<string, PropertyInfo> propertyMap = null;
        public static Dictionary<string, PropertyInfo> PropertyMap
        {
            get
            {
                if (propertyMap == null)
                {
                    propertyMap = new Dictionary<string, PropertyInfo>();
                    foreach (var prop in typeof(DarwinData).GetProperties().Where(p=>Attribute.IsDefined(p,typeof(DarwinField))))
                    {
                        propertyMap.Add(prop.Name, prop);
                    }
                }
                return propertyMap;
            }
        }
        

        public string this[string key]{
            get => PropertyMap.ContainsKey(key) ? (string)PropertyMap[key].GetValue(this) : null;
        }

        public static IEnumerable<string> PublicProperties
        {
            get => PropertyMap.Keys;
        }


        private string catalogNumber;
        [DarwinField("catalogNumber")]
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
        [DarwinField("individualCount")]
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
        [DarwinField("sex")]
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
        [DarwinField("lifeStage")]
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
        [DarwinField("recordNumber")]
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
        [DarwinField("recordedBy")]
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
        [DarwinField("eventDate")]
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
        [DarwinField("habitat")]
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
        [DarwinField("locality")]
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
        [DarwinField("county")]
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
        [DarwinField("stateProvince")]
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
        [DarwinField("country")]
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
        [DarwinField("minimumElevationInMeters")]
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
        [DarwinField("decimalLatitude")]
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
        [DarwinField("decimalLongitude")]
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
        [DarwinField("coordinateUncertaintyInMeters")]
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
        [DarwinField("genus")]
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
        [DarwinField("specificEpithet")]
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
        [DarwinField("subspecies")]
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
        [DarwinField("family")]
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
        [DarwinField("order")]
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
        [DarwinField("preparations")]
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
        [DarwinField("institutionCode")]
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
        [DarwinField("collectionCode")]
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
        [DarwinField("id")]
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
        [DarwinField("identifiedBy")]
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
        [DarwinField("dateIdentified")]
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
        [DarwinField("occurrenceRemarks")]
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
        [DarwinField("ownerInstitutionCode")]
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
        [DarwinField("recordEnteredBy")]
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
        [DarwinField("references")]
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
