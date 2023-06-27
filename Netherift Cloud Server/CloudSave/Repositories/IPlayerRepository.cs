namespace Netherift_Cloud_Server.CloudSave.Repositories
{
    public interface IPlayerRepository
    {
        public Task<Dictionary<string, object>> GetPlayerByIdAsync(string id);
    }
}
