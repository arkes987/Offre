namespace Offre.Logic.Interfaces.Authorize
{
    public interface IAuthorizeLogic
    {
        string GenerateToken(long userId);
    }
}
