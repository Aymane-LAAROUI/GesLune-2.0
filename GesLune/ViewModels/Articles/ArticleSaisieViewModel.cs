using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Articles
{
    public class ArticleSaisieViewModel : ViewModelBase
    {
        public Model_Article Article { get; private set; }
        public List<Model_Categorie> Categories { get; set; } = [];
        private Model_Categorie _Categorie { get; set; } = new();
        public Model_Categorie Selected_Categorie
        {
            get => _Categorie;
            set
            {
                _Categorie = value;
                Article.Article_Categorie_Id = value.Categorie_Id;
                OnPropertyChanged(nameof(Selected_Categorie));
            }
        }
        public List<string> Codes { get; set; } = [];
        public string? Selected_Code { get; set; }
        public string New_Code { get; set; } = string.Empty;
        
        public ArticleSaisieViewModel(Model_Article? model)
        {
            Article = model ?? new Model_Article();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                Categories = await CategorieRepository.GetAll();
                if (Article == null || Article.Article_Categorie_Id == 0)
                {
                    _Categorie = Categories.FirstOrDefault() ?? new();
                    Codes = [];
                    OnPropertyChanged(nameof(Codes));
                }
                else
                {
                    _Categorie = Categories.Find(e => e.Categorie_Id == Article.Article_Categorie_Id) ?? new();
                    await LoadCodes(Article.Article_Id);
                }
                OnPropertyChanged(nameof(Categories));
                OnPropertyChanged(nameof(Selected_Categorie));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des catégories: {ex.Message}");
            }
        }

        private async Task LoadCodes(int articleId)
        {
            try
            {
                Codes = await ArticleRepository.GetCodes(articleId);
                OnPropertyChanged(nameof(Codes));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des codes: {ex.Message}");
            }
        }

        public async void Enregistrer()
        {
            try
            {
                Article.Article_Categorie_Id = Selected_Categorie.Categorie_Id;
                Article = await ArticleRepository.Enregistrer(Article);
                MessageBox.Show("Opération réussie", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }

        public async void AjouterCode()
        {
            if (string.IsNullOrEmpty(New_Code)) return;
            try
            {
                await ArticleRepository.AddCode(Article.Article_Id, New_Code);
                await LoadCodes(Article.Article_Id);
                New_Code = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du code: {ex.Message}");
            }
        }

        public async void DeleteCode()
        {
            if (Selected_Code == null) return;
            try
            {
                await ArticleRepository.DeleteCode(Selected_Code);
                await LoadCodes(Article.Article_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du code: {ex.Message}");
            }
        }
    }
}
