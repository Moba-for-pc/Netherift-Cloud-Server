using Netherift_Cloud_Server.CloudSave.Repositories;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server.Endpoints
{
    public class GetUserByIdEndpoint
    {
        private IPlayerRepository _playerRepository;
        public GetUserByIdEndpoint(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [CloudCodeFunction("GetPlayerById")]
        public async Task<Dictionary<string, object>> GetPlayerByIdAsync(string playerId)
        {
            return await _playerRepository.GetPlayerByIdAsync(playerId);
        }
    }
}
