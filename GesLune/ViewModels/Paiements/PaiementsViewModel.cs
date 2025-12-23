using GesLune.Models;
using System.Windows;
using GesLune.Repositories;
using GesLune.Commands;
using GesLune.Windows.Paiements;

namespace GesLune.ViewModels.Paiements
{
    public class PaiementsViewModel : ViewModelBase
    {
        private IEnumerable<Model_Paiement> _Paiements = [];
        public IEnumerable<Model_Paiement> Paiements
        {
            get => _Paiements;
            set
            {
                if (_Paiements != value)
                {
                    _Paiements = value;
                    OnPropertyChanged(nameof(Paiements));
                }
            }
        }
        public Model_Paiement? Selected_Paiement { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public PaiementsViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Paiement model = Selected_Paiement
                ?? new()
                {

                };
            PaiementSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        public async void LoadData()
        {
            try
            {
                var data = await PaiementRepository.GetAll();
                Paiements = data;
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
                int res = await PaiementRepository.Delete(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }
    }
}