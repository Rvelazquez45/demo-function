using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DemoFunction;

public class Demo
{
    private readonly ILogger<Demo> _logger;

    public Demo(ILogger<Demo> logger)
    {
        _logger = logger;
    }

    [Function("Demo")]
    public IActionResult Run
    (
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] 
        HttpRequest req
    )
    {
        _logger.LogInformation(message: "C# HTTP trigger function processed a request.");

        return new OkObjectResult(value: "Hello World!");
    }
}
