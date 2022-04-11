using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Threading;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // The port number(5000) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            {
                // var client = new Greeter.GreeterClient(channel);
                // var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
                // Console.WriteLine("Greeting: " + reply.Message);

                // var client1 = new Example.ExampleClient(channel);
                // var reply1 = await client1.UnaryCallAsync(new ExampleRequest { });
                // Console.WriteLine("agent: " + reply1.Message);

                // // Create the token source.
                // CancellationTokenSource cts = new CancellationTokenSource();

                // using var call = client1.StreamingFromServer(new ExampleRequest { });
                // while (await call.ResponseStream.MoveNext(cts.Token))
                // {
                //     Console.WriteLine("StreamingFromServer: " + call.ResponseStream.Current.Message);
                // }



                var client2 = new Person.PersonClient(channel);
                var reply2 = await client2.GetPersonAsync(new PersonRequest { FirstName = "justin" });
                Console.WriteLine("Person: " + reply2.IsSuccess);


            }




        }
    }
}
