using GesLune.ViewModels;
using System.Windows;

namespace GesLune.Windows
{
    public partial class MainWindow : Window
    {

        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadMenuItems();
        }
    }
}
