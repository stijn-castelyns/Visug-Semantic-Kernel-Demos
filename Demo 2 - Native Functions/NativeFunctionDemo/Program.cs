using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using NativeFunctionDemo.Plugins;
using NativeFunctionDemo.Filters;


IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

AzureKeyCredential azureKeyCredential = new(config["AzureOpenAI:AzureKeyCredential"]);
string deploymentName = config["AzureOpenAI:DeploymentName"];
Uri endpoint = new Uri(config["AzureOpenAI:Endpoint"]);
OpenAIClient openAIClient = new(endpoint, azureKeyCredential);

IKernelBuilder builder = Kernel.CreateBuilder();

builder.Services.AddLogging((options) =>
{
    options.SetMinimumLevel(LogLevel.Trace);
    options.AddConsole();
});

builder.Services.AddAzureOpenAIChatCompletion(deploymentName, openAIClient);

var kernel = builder.Build();

kernel.Plugins.AddFromType<MathPlugin>();

// Add filter without DI
// kernel.PromptFilters.Add(new MyPromptFilter());

// Create chat history
ChatHistory history = [];

// Retrieve the chat completion service from the kernel
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

//Console.WriteLine(await kernel.InvokePromptAsync("What is the square root of 2?"));

// Enable auto function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

//// Start the conversation
while (true)
{
    // Get user input
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User > ");
    history.AddUserMessage(Console.ReadLine()!);

    // Get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentsAsync(history, kernel: kernel, executionSettings: openAIPromptExecutionSettings);

    // Stream the results
    string fullMessage = "";
    var first = true;
    Console.ForegroundColor = ConsoleColor.Cyan;
    foreach (var content in result)
    {
        if (first)
        {
            Console.Write("Assistant > ");
            first = false;
        }
        Console.Write(content.Content);
        fullMessage += content.Content;
    }
    Console.WriteLine();

    // Add the message from the agent to the chat history
    history.AddAssistantMessage(fullMessage);
}