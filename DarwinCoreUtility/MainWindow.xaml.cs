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
using System.Collections.ObjectModel;
using DarwinCoreUtility.Pages;
using System.Reflection;
using Microsoft.Win32;

namespace DarwinCoreUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<INavigateable> NavigationOptions { get; set; } = new List<INavigateable>()
        {
           new DataGridPage(),
           new KMLSettings()
        };

        public DarwinDataModel Data { get => DarwinDataModel.CurrentData; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            controlWindow.Content = NavigationOptions[0];
            navigationList.SelectedItem = NavigationOptions[0];
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var button = e.AddedItems[0] as INavigateable;
            controlWindow.Content = button;
        }

        private void OpenLoadDialog(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                DarwinDataModel.LoadFile(ofd.FileName);
                //controlWindow.Content = NavigationOptions[0];
                //navigationList.SelectedItem = NavigationOptions[0];
            }
        }

        private void ExportKML(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "KML Files (*.kml)|*.kml";
            if (DarwinDataModel.CurrentData != null && sfd.ShowDialog() == true)
            {
                DarwinDataModel.CurrentData.ExportKML(sfd.FileName);
            }
        }
    }
}
