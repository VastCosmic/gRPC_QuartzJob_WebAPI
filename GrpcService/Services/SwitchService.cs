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
                StrRply = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ExecRpcCommandSync Reply." + request.StrRequest
            });
        }

        public override Task<Reply> ExecRpcCommand(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Reply
            {
                StrRply = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ExecRpcCommand Reply." + request.StrRequest
            });
        }
    }
}