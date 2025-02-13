using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public Dictionary<string, object> FunctionHandler(string path, ILambdaContext context)
    {
        if (path != null && path.EndsWith("/hello"))
        {
            return new Dictionary<string, object>()
            {
                { "statusCode", 200 },
                { "message", "Hello from Lambda" },
            };
        }
        else
        {
            var message = $"Bad request syntax or unsupported method. Request path: {path}. HTTP method: GET";
            return new Dictionary<string, object>()
            {
                { "statusCode", 400 },
                { "message", message },
            };
        }
    }
}
