namespace RapidPayAuthSystem.Services
{
    public interface IAuthorizationService
    {
        bool Authorize(string username, string password);
    }
}
