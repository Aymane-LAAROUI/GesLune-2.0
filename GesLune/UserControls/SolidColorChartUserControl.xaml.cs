using GesLune.ViewModels.Statistiques;
using System.Windows;
using System.Windows.Controls;

namespace GesLune.UserControls
{
    /// <summary>
    /// Logique d'interaction pour SolidColorChartUserControl.xaml
    /// </summary>
    public partial class SolidColorChartUserControl : UserControl
    {
        public SolidColorChartUserControl()
        {
            InitializeComponent();
        }
        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as ChiffreAffaireViewModel).DataInit();
        }
    }
}
