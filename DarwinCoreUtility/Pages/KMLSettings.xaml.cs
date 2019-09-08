using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using DarwinCoreUtility.KML;
namespace DarwinCoreUtility.Pages
{
    /// <summary>
    /// Interaction logic for KMLSettings.xaml
    /// </summary>
    public partial class KMLSettings : UserControl, INavigateable
    {
        public string ButtonName => "KML Settings";
        public string ButtonTag => "kmlsettings";


        private ICollectionView filterHeaderView;
        private bool HeaderFilter(object filterObj)
        {
            return !currentSettings.FolderGrouping.Contains(filterObj as String);
        }

        private KMLFileSettings currentSettings { get => KMLFileSettings.CurrentSettings; }
        public DarwinDataModel Data { get=>DarwinDataModel.CurrentData; }

        public KMLSettings()
        {
            this.DataContext = currentSettings;
            
            InitializeComponent();
            filterHeaderView = CollectionViewSource.GetDefaultView(headerCombo.ItemsSource);
            filterHeaderView.Filter = HeaderFilter;
        }
        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            currentSettings.Save();
        }
        private void AddHeaderBtn_Click(object sender, RoutedEventArgs e)
        {
            if(headerCombo.SelectedItem != null)
            {
                currentSettings.AddGrouping(headerCombo.SelectedItem as String);
                filterHeaderView.Refresh();
            }
        }

        private void RemoveGrouping_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            currentSettings.RemoveGrouping(btn.DataContext as String);
            filterHeaderView.Refresh();
        }

        private void MoveGroupingUp_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn.DataContext as String;
            currentSettings.MoveGrouping(item, -1);
        }

        private void MoveGroupingDown_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn.DataContext as String;
            currentSettings.MoveGrouping(item, 1);
        }
    }
}
