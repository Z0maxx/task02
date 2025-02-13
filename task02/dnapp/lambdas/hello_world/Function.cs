using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using System;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public APIGatewayHttpApiV2ProxyResponse FunctionHandler(APIGatewayHttpApiV2ProxyRequest request)
    {
        Console.WriteLine(JsonSerializer.Serialize(request));
        APIGatewayHttpApiV2ProxyResponse response;
        Dictionary<string, object> dict;
        string path = request.RequestContext.Http.Path;
        string method = request.RequestContext.Http.Method;
        Console.WriteLine($"Path: {path}, Method: {method}");
        if (path != null && path.EndsWith("/hello"))
        {
            dict = new Dictionary<string, object>()
            {
                { "statusCode", 200 },
                { "message", "Hello from Lambda" },
            };
            response = new APIGatewayHttpApiV2ProxyResponse()
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(dict),
                Headers = new Dictionary<string, string>() { { "Content-Type", "text/plain" } }
            };
        }
        else
        {
            var message = $"Bad request syntax or unsupported method. Request path: {path}. HTTP method: {method}";
            dict = new Dictionary<string, object>()
            {
                { "statusCode", 400 },
                { "message", message },
            };
            response = new APIGatewayHttpApiV2ProxyResponse()
            {
                StatusCode = 400,
                Body = JsonSerializer.Serialize(dict),
                Headers = new Dictionary<string, string>() { { "Content-Type", "text/plain" } }
            };
        }

        System.Console.WriteLine("Returning: " + JsonSerializer.Serialize(response));
        return response;
    }
}
