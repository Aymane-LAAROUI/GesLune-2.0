using GesLune.Repositories;
using System.Data;
using System.Windows;

namespace GesLune.ViewModels.Stocks
{
    public class ArticleFicheStockViewModel : ViewModelBase
    {
        public DataTable FicheStock { get; set; } = new();
        
        public ArticleFicheStockViewModel(int Article_Id)
        {
            LoadFicheStock(Article_Id);
        }

        private async void LoadFicheStock(int Article_Id)
        {
            try
            {
                FicheStock = await ArticleRepository.GetFicheStock(Article_Id);
                OnPropertyChanged(nameof(FicheStock));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de la fiche stock: {ex.Message}");
            }
        }
    }
}
