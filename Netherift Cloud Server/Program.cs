using Microsoft.Extensions.DependencyInjection;
using Netherift_Cloud_Server.Authentication;
using Netherift_Cloud_Server.CloudSave.Repositories;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server
{
    public class Program : ICloudCodeSetup
    {
        public void Setup(ICloudCodeConfig config)
        {
            config.Dependencies.AddSingleton<IJwtAuth, JwtAuth>();
        }
    }
}