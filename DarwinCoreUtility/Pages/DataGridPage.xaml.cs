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
    /// Interaction logic for DataGridPage.xaml
    /// </summary>
    public partial class DataGridPage : UserControl, INavigateable
    {
        public string ButtonName => "Data Grid View";
        public string ButtonTag => "gridview";
        public DarwinDataModel Data { get => DarwinDataModel.CurrentData; }
        public DataGridPage()
        {
            this.DataContext = Data;
            InitializeComponent();
        }
    }
}
