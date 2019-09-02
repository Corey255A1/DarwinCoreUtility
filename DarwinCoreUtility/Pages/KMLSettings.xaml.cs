using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DarwinCoreUtility.Darwin;

namespace DarwinCoreUtility.Pages
{
    /// <summary>
    /// Interaction logic for KMLSettings.xaml
    /// </summary>
    public partial class KMLSettings : UserControl, INavigateable
    {
        public string ButtonName => "KML Settings";
        public string ButtonTag => "kmlsettings";
        public KMLSettings()
        {
            this.DataContext = DarwinDataModel.CurrentData;
            InitializeComponent();
        }
    }
}
