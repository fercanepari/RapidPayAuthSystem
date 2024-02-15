namespace RapidPayAuthSystem.Services
{
    public class BasicAuthorizationService : IAuthorizationService
    {
        public bool Authorize(string username, string password)
        {
            // Basic authentication logic
            return username == "admin" && password == "password";
        }
    }
}
