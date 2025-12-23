using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Acteurs;
using GesLune.Windows.Articles;
using GesLune.Windows.Documents;
using System.Collections.ObjectModel;
using System.Windows;

namespace GesLune.ViewModels.Documents
{
    public class DocumentSaisieViewModel : ViewModelBase
    {
        private Model_Document _document;
        public Model_Document Document
        {
            get => _document;
            set
            {
                if (_document != value && value != null)
                {
                    _document = value;
                    OnPropertyChanged(nameof(Document));
                }
                else
                {
                    MessageBox.Show("hwa hwa");
                }
            }
        }
        public string? Document_Adresse_Client
        {
            get => Document.Document_Acteur_Adresse;
            set
            {
                Document.Document_Acteur_Adresse = value;
                OnPropertyChanged(nameof(Document_Adresse_Client));
            }
        }
        private ObservableCollection<Model_Document_Ligne> _lignes;
        public ObservableCollection<Model_Document_Ligne> Lignes
        {
            get => _lignes;
            set
            {
                if (_lignes != value)
                {
                    _lignes = value;
                    OnPropertyChanged(nameof(Lignes));
                }
            }
        }
        private List<Model_Document_Type> _document_types = [];
        public List<Model_Document_Type> Document_Types
        {
            get => _document_types;
            set
            {
                if (value != _document_types)
                {
                    _document_types = value;
                    OnPropertyChanged(nameof(Document_Types));
                }
            }
        }
        private Model_Document_Type _Selected_Type;
        public Model_Document_Type Selected_Type
        {
            get => _Selected_Type;
            set
            {
                if (value != null)
                {
                    Document.Document_Type_Id = value.Document_Type_Id;
                    _Selected_Type = value;
                }
            }
        }
        private Model_Acteur? _Selected_Acteur;
        public Model_Acteur? Selected_Acteur
        {
            get => _Selected_Acteur;
            set
            {
                if (value != null)
                {
                    _Selected_Acteur = value;
                    Document.Document_Acteur_Id = value.Acteur_Id;
                    Document.Document_Acteur_Nom = value.Acteur_Nom;
                    Document_Adresse_Client = value.Acteur_Adresse;
                    OnPropertyChanged(nameof(Selected_Acteur));
                }

            }
        }
        private decimal _Total_Document;
        public decimal Total_Document
        {
            get => _Total_Document;
            set
            {
                _Total_Document = value;
                //MessageBox.Show($"{_Total_Document} : {value}");
                OnPropertyChanged(nameof(Total_Document));
            }
        }

        public DocumentSaisieViewModel(Model_Document? document = null)
        {
            _document = document ?? new Model_Document();
            Selected_Acteur = new()
            {
                Acteur_Id = Document.Document_Acteur_Id,
                Acteur_Nom = Document.Document_Acteur_Nom,
                Acteur_Adresse = Document.Document_Acteur_Adresse,
            };
            _lignes = [];
            _Selected_Type = new();
            LoadDocumentTypes();
            LoadLignes();
        }

        private async void LoadDocumentTypes()
        {
            try
            {
                Document_Types = await DocumentRepository.GetTypes();
                _Selected_Type = Document_Types.Find(e => _document.Document_Type_Id == e.Document_Type_Id) ?? Document_Types.FirstOrDefault() ?? new();
                OnPropertyChanged(nameof(Document_Types));
                OnPropertyChanged(nameof(Selected_Type));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des types: {ex.Message}");
            }
        }

        public async void LoadLignes()
        {
            try
            {
                var lignesList = await DocumentRepository.GetLignes(_document.Document_Id);
                Lignes = new(lignesList);
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des lignes: {ex.Message}");
            }
        }

        public async void Enregistrer()
        {
            try
            {
                var document_ = await DocumentRepository.Enregistrer(_document);
                if (document_ != null)
                {
                    Document = document_;
                }
                else MessageBox.Show("ra ja 5awi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }

        public async void EnregistrerLigne(Model_Document_Ligne ligne)
        {
            try
            {
                Enregistrer();
                ligne.Document_Id = Document.Document_Id;
                await DocumentRepository.EnregistrerLigne(ligne);
                LoadLignes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement de la ligne: {ex.Message}");
            }
        }

        public async void Delete(int id)
        {
            try
            {
                int res = await DocumentRepository.DeleteLigne(id);
                LoadLignes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0.00m;
            foreach (var item in Lignes)
            {
                total += item.Document_Ligne_Total;
            }
            Total_Document = total;
        }

        public void SelectActeur()
        {
            ActeurSelectionWindow selectionWindow = new(Selected_Type.Document_Type_Acteur_Type_Id);
            if (selectionWindow.ShowDialog() == true)
            {
                //MessageBox.Show($"{selectionWindow.SelectedActeur?.Acteur_Id}");
                Selected_Acteur = selectionWindow.SelectedActeur;
            }
        }

        public void RechercherArticles()
        {
            ArticleSelectionWindow selectionWindow = new();
            if (selectionWindow.ShowDialog() == true)
            {
                foreach (Model_Article article in selectionWindow.SelectedArticles)
                {
                    EnregistrerLigne(
                        new()
                        {
                            Document_Article_Id = article.Article_Id,
                            Document_Ligne_Article_Nom = article.Article_Nom,
                            Document_Ligne_Quantity = 1,
                            Document_Ligne_Prix_Unitaire = (decimal)article.Article_Prix,
                            Document_Id = Document.Document_Id,
                            Document_Ligne_Total = (decimal)article.Article_Prix
                        }
                    );
                }
            }
        }

        public async void Encaisser()
        {
            try
            {
                Model_Paiement paiement = new Model_Paiement()
                {
                    Paiement_Acteur_Id = Document.Document_Acteur_Id,
                    Paiement_Acteur_Nom = Selected_Acteur?.Acteur_Nom,
                    Paiement_Date = DateTime.Now,
                    Paiement_Document_Id = Document.Document_Id,
                    Paiement_Montant = Total_Document,
                };
                await PaiementRepository.Enregistrer(paiement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'encaissement: {ex.Message}");
            }
        }

        public void Transferer()
        {
            new DocumentTransfertWindow(Document).ShowDialog();
        }
    }
}
