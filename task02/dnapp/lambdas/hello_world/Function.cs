using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleLambdaFunction;

public class Function
{
    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (request.Path.EndsWith("/hello"))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "{ \"statusCode\": 200, \"message\": \"Hello from Lambda\" }",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
        else
        {
            var message = $"Bad request syntax or unsupported method. Request path: {request.Path}. HTTP method: {request.HttpMethod}";
            return new APIGatewayProxyResponse
            {
                StatusCode = 400,
                Body = "{ \"statusCode\": 400, \"message\": \"" + message + "\" }",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
    }
}
