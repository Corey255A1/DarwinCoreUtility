using DarwinCoreUtility.Darwin;
using System.Windows.Controls;

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
