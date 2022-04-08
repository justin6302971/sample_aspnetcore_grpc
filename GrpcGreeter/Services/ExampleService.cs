using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class ExampleService : Example.ExampleBase
    {
        private readonly ILogger<ExampleService> _logger;
        public ExampleService(ILogger<ExampleService> logger)
        {
            _logger = logger;
        }

        public override Task<ExampleResponse> UnaryCall(ExampleRequest request, ServerCallContext context)
        {
            var userAgent = context.RequestHeaders.FirstOrDefault(d=>d.Key=="user-agent")?.Value;
            var response = new ExampleResponse() { Message = userAgent };
            return Task.FromResult(response);
        }
    }
}
