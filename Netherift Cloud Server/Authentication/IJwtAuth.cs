namespace Netherift_Cloud_Server.Authentication
{
    public interface IJwtAuth
    {
        public Task<string> GetTokenAsync();
    }
}
