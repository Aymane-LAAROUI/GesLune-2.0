namespace GesLune.Repositories
{
    /// <summary>
    /// Classe de base pour les repositories.
    /// Note: Cette classe est conservée pour compatibilité rétroactive.
    /// Les repositories utilisent maintenant une API HTTP via ApiClientProvider.
    /// </summary>
    public abstract class MainRepository
    {
        // Conservé pour compatibilité, mais ne doit plus être utilisé
        // car tous les repositories utilisent maintenant l'API HTTP
        [Obsolete("Les repositories utilisent maintenant une API HTTP. Cette propriété n'est plus utilisée.")]
        public static readonly string ConnectionString =
            "Data Source=localhost;Initial Catalog=GesLune;Integrated Security=True;TrustServerCertificate=True;";
    }
}
