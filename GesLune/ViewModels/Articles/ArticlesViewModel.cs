using GesLune.Commands;
using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Articles;
using System.Windows;

namespace GesLune.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        public List<Model_Article> Articles { get; set; } = [];
        public Model_Article? Selected_Article { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public ArticlesViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Article model = Selected_Article ?? new();
            ArticleSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                Articles = await ArticleRepository.GetAll();
                OnPropertyChanged(nameof(Articles));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Delete(int id)
        {
            try
            {
                int res = await ArticleRepository.Delete(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }

        public void AfficherFicheStock()
        { 
            if (Selected_Article == null) return;
            if (!Selected_Article.Article_Stockable) return;
            new ArticleFicheStockWindow(Selected_Article.Article_Id).ShowDialog();
        }

    }
}
