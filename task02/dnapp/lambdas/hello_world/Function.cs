using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public Dictionary<string, object> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request)
    {
        var path = request?.RequestContext?.Http?.Path;
        var method = request?.RequestContext?.Http?.Method;
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
            var message = $"Bad request syntax or unsupported method. Request path: {path}. HTTP method: {method}";
            return new Dictionary<string, object>()
            {
                { "statusCode", 400 },
                { "message", message },
            };
        }
    }
}
