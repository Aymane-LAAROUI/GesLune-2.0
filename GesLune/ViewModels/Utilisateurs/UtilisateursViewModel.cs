using GesLune.Models;
using System.Windows;
using GesLune.Repositories;
using GesLune.Commands;
using GesLune.Windows.Utilisateurs;

namespace GesLune.ViewModels.Utilisateurs
{
    public class UtilisateursViewModel : ViewModelBase
    {
        private IEnumerable<Model_Utilisateur> _Utilisateurs = [];
        public IEnumerable<Model_Utilisateur> Utilisateurs
        {
            get => _Utilisateurs;
            set
            {
                if (_Utilisateurs != value)
                {
                    _Utilisateurs = value;
                    OnPropertyChanged(nameof(Utilisateurs));
                }
            }
        }
        public Model_Utilisateur? Selected_Utilisateur { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public UtilisateursViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Utilisateur model = Selected_Utilisateur
                ?? new()
                {

                };
            UtilisateurSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        public async void LoadData()
        {
            try
            {
                var data = await UtilisateurRepository.GetAll();
                Utilisateurs = data;
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
                int res = await UtilisateurRepository.Delete(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }
    }
}