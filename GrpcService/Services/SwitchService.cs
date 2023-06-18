using Grpc.Core;
using GrpcService;
using SwitchService;

namespace GrpcService.Services
{
    public class SwitchService : SwitchApi.SwitchApiBase
    {
        private readonly ILogger<SwitchService> _logger;
        public SwitchService(ILogger<SwitchService> logger)
        {
            _logger = logger;
        }

        // Override Reply Task
        public override Task<Reply> ExecRpcCommandSync(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Reply
            {
                StrRply = request.StrRequest + " ExecRpcCommandSync Reply."
            });
        }

        public override Task<Reply> ExecRpcCommand(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Reply
            {
                StrRply = request.StrRequest + " ExecRpcCommand Reply."
            });
        }
    }
}