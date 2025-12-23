using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Acteurs;
using System.Windows;

namespace GesLune.ViewModels.Utilisateurs
{
    public class UtilisateurSaisieViewModel : ViewModelBase
    {
        public Model_Utilisateur Utilisateur { get; private set; }
        public UtilisateurSaisieViewModel(Model_Utilisateur model)
        {
            Utilisateur = model ?? new Model_Utilisateur();
        }

        public async void Enregistrer()
        {
            try
            {
                Utilisateur = await UtilisateurRepository.Enregistrer(Utilisateur);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }
    }
}
