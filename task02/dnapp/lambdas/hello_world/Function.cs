using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public Dictionary<string, object> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (request.Path == "/hello")
        {
            return new Dictionary<string, object>()
            {
                { "statusCode", 200 },
                { "message", "Hello from Lambda" },
            };
        }
        else
        {
            return new Dictionary<string, object>()
            {
                { "statusCode", 400 },
                { "message", $"Bad request syntax or unsupported method. Request path: {request.Path}. HTTP method: {request.HttpMethod}" },
            };
        }
    }
}
