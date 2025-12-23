using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Documents
{
    public class DocumentTransfertViewModel : ViewModelBase
    {
        Model_Document Document { get; set; }
        private List<Model_Document_Type> _Document_Types = [];
        public List<Model_Document_Type> Document_Types
        {
            get => _Document_Types;
            set
            {
                _Document_Types = value;
                OnPropertyChanged(nameof(Document_Types));
            }
        }
        public Model_Document_Type SelectedDocumentType { get; set; }

        public DocumentTransfertViewModel(Model_Document document)
        {
            Document = document;
            LoadDocumentTypes();
        }

        private async void LoadDocumentTypes()
        {
            try
            {
                Document_Types = await DocumentRepository.GetTypes();
                SelectedDocumentType = Document_Types.FirstOrDefault() ?? new();
                OnPropertyChanged(nameof(Document_Types));
                OnPropertyChanged(nameof(SelectedDocumentType));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des types: {ex.Message}");
            }
        }

        public async void Transferer()
        {
            try
            {
                var document = Document;
                document.Document_Type_Id = SelectedDocumentType.Document_Type_Id;
                var documentLignes = await DocumentRepository.GetLignes(document.Document_Id);

                document.Document_Reference_Id = Document.Document_Id;

                document.Document_Id = 0;
                document.Document_Num = string.Empty;

                document = await DocumentRepository.Enregistrer(document);

                foreach (var item in documentLignes)
                {
                    item.Document_Ligne_Id = 0;
                    item.Document_Id = document.Document_Id;
                    await DocumentRepository.EnregistrerLigne(item);
                }
                MessageBox.Show("Done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du transfert: {ex.Message}");
            }
        }
    }
}
