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


        private CollectionViewSource groupFilterView;
        private void GroupFilter(object filterObj, FilterEventArgs e)
        {
            e.Accepted = !currentSettings.FolderGrouping.Contains(e.Item as String);
        }

        private KMLFileSettings currentSettings { get => KMLFileSettings.CurrentSettings; }
        public DarwinDataModel Data { get=>DarwinDataModel.CurrentData; }

        public KMLSettings()
        {
            this.DataContext = currentSettings;
            
            InitializeComponent();
            groupFilterView = ((CollectionViewSource)this.Resources["GroupFilter"]);
            groupFilterView.Filter += GroupFilter;
            webster.NavigateToString("<html><body><strong>THIS IS A TEST</strong></body></html>");
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
                groupFilterView.View.Refresh();
            }
        }

        private void RemoveGrouping_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            currentSettings.RemoveGrouping(btn.DataContext as String);
            groupFilterView.View.Refresh();
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


        private void InsertName_Click(object sender, RoutedEventArgs e)
        {
            if(placemarkNameCombo.SelectedItem != null)
            {
                currentSettings.PlacemarkNameFormat += $"[[{placemarkNameCombo.SelectedItem as String}]]";
            }
        }
    }

    public class WebBrowserDynamicUpdate
    {

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(WebBrowserDynamicUpdate), new PropertyMetadata(HtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string body)
        {
            dependencyObject.SetValue(HtmlProperty, body);
        }
        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wb = d as WebBrowser;
            if(wb != null && !String.IsNullOrEmpty(e.NewValue as string))
            {
                wb.NavigateToString(e.NewValue as string);
            }
        }
    }
}
