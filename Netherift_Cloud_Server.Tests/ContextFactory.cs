using Moq;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server.Tests
{
    public class ContextBuilder
    {
        private Mock<IExecutionContext> _contextMock = new();

        public ContextBuilder UseDefaultSettings()
        {
            SetProjectId(ConstantsForTests.PROJECT_ID);
            SetEnvironmentId(ConstantsForTests.ENVIRONMENT_ID);
            SetPlayerId(ConstantsForTests.PLAYER_ID);
            return this;
        }

        public ContextBuilder SetProjectId(string projectId)
        {
            _contextMock.Setup(r => r.ProjectId).Returns(projectId);
            return this;
        }
        public ContextBuilder SetEnvironmentId(string environmentId)
        {
            _contextMock.Setup(r => r.EnvironmentId).Returns(environmentId);
            return this;
        }

        public ContextBuilder SetPlayerId(string playerId)
        {
            _contextMock.Setup(r => r.PlayerId).Returns(playerId);
            return this;
        }


        public IExecutionContext Build()
        {
            return _contextMock.Object;
        }
    }
}
