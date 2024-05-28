using DemoFunction.Logic;
using DemoFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoFunction;

public class Demo
{
    private readonly ILogger<Demo> _logger;
    private readonly ISampleLogic _sampleLogic;
    private readonly IConfiguration _config;

    public Demo(ILogger<Demo> logger, ISampleLogic sampleLogic, IConfiguration config)
    {
        _logger = logger;
        _sampleLogic = sampleLogic;
        _config = config;
    }

    [Function("Demo")]
    public async Task<IActionResult> Run
    (
        [HttpTrigger(AuthorizationLevel.Function, "post")] 
        HttpRequest req
    )
    {
        _logger.LogInformation(message: "C# HTTP trigger function processed a request.");

       PersonModel? data = await req.ReadFromJsonAsync<PersonModel>();

        if 
        (
            data is null ||
            data.FirstName is null ||
            data.LastName is null
        )
        {
            _logger.LogError(message: "Invalid JSON object in the request body.");

            return new BadRequestObjectResult(error: "Please provide a valid JSON object in the request body.");
        }

        #region notes
        // In Azure Portal you need two underscores. It can't handle a colon in your env. variable name.
        // string cnn = Environment.GetEnvironmentVariable("ConnectionStrings__Default");
        // This is not how we do things anywhere else in C#.
        //string cnn = Environment.GetEnvironmentVariable("ConnectionStrings:Default");
        #endregion notes

        string? connectionString = _config.GetConnectionString(name: "Default");

        return new OkObjectResult(value: connectionString);
    }
}
