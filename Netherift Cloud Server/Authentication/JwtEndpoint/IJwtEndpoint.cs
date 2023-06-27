namespace Netherift_Cloud_Server.Authentication.JwtEndpoint
{
    public interface IJwtEndpoint
    {
        public Task<string> MakeRequestAsync();
    }
}
