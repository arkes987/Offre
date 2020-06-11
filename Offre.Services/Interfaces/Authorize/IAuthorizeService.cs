namespace Offre.Services.Interfaces.Authorize
{
    public interface IAuthorizeService
    {
        object TryAuthorizeUser(string login, string password);
    }
}
