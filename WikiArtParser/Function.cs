using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WikiArtParser.Core.Infra;
using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WikiArtParser;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        var services = new ServiceCollection();
        var configuration = GetConfiguration();
        services.AddSingleton(configuration);
        services.AddWikiArtParserCore(configuration);

        var serviceProvider = services.BuildServiceProvider();
        var wikiArtParserMessageHandler = serviceProvider.GetRequiredService<IWikiArtParserMessageHandler>();

        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(wikiArtParserMessageHandler, message, context);
        }
    }

    private async Task ProcessMessageAsync(IWikiArtParserMessageHandler wikiArtParserMessageHandler, SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processing message {message.Body}");

        var messageBody = JsonSerializer.Deserialize<WikiArtParserMessage>(message.Body);

        await wikiArtParserMessageHandler.Handle(messageBody!);
    }

    internal static IConfiguration GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        builder.AddSystemsManager(config =>
        {
            config.Path = "/bartsart/wikiartparser";
        });

        return builder.Build();
    }
}