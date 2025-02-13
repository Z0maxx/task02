using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using System;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public Dictionary<string, object> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request)
    {
        Console.WriteLine(JsonSerializer.Serialize(request));
        Dictionary<string, object> response;
        string path = request.RequestContext.Http.Path;
        string method = request.RequestContext.Http.Method;
        Console.WriteLine($"Path: {path}, Method: {method}");
        if (path != null && path.EndsWith("/hello"))
        {
            response = new Dictionary<string, object>()
            {
                { "statusCode", 200 },
                { "message", "Hello from Lambda" },
            };
        }
        else
        {
            var message = $"Bad request syntax or unsupported method. Request path: {path}. HTTP method: {method}";
            response = new Dictionary<string, object>()
            {
                { "statusCode", 400 },
                { "message", message },
            };
        }

        System.Console.WriteLine("Returning: " + JsonSerializer.Serialize(response));
        return response;
    }
}
