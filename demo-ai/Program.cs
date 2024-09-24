/* 

// demo 1 - OpenAI demo

using OpenAI;
using System.ClientModel;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(apiKey));
var chatService = client.GetChatClient("gpt-4o-mini");

var result = await chatService.CompleteChatAsync("Qual é a cor do céu?");
Console.WriteLine(result.Value);
*/

/*
// demo 2 - OpenAI demo with streaming

using OpenAI;
using System.ClientModel;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(apiKey));
var chatService = client.GetChatClient("gpt-4o-mini");

await foreach (var update in chatService.CompleteChatStreamingAsync("Qual é a cor do céu?"))
{
    foreach (var item in update.ContentUpdate)
        Console.Write(item);
}
*/

/*
// demo 3 - SemanticKernel interface

using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
IChatCompletionService chatService = new OpenAIChatCompletionService("gpt-4o-mini", apiKey);

var result = await chatService.GetChatMessageContentAsync("Qual é a cor do céu?");
Console.WriteLine(result);

*/

/*
// demo 4 - Google Gemini

using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;

string? apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
IChatCompletionService chatService = 
    // new OpenAIChatCompletionService("gpt-4o-mini", apiKey);
    new GoogleAIGeminiChatCompletionService("gemini-1.5-flash-latest", apiKey);

var result = await chatService.GetChatMessageContentAsync("Qual é a cor do céu?");
Console.WriteLine(result);
*/

/*
// demo 5 - interactive chat
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

IChatCompletionService chatService =
    new OpenAIChatCompletionService("gpt-4o-mini", apiKey);

while (true)
{
    Console.Write("P: ");
    Console.WriteLine(await chatService.GetChatMessageContentAsync(Console.ReadLine()));
}
*/

/*

// demo 6 - interactive chat with history

using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

IChatCompletionService chatService =
    new OpenAIChatCompletionService("gpt-4o-mini", apiKey);

ChatHistory history = new();
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
*/

/*
// demo 7 - interactive chat with history and context

using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

IChatCompletionService chatService =
    new OpenAIChatCompletionService("gpt-4o-mini", apiKey);

ChatHistory history = new();
history.AddUserMessage("Maíra mora em Los Angeles");
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
*/

/*
// demo 8 - DI, kernel and prompt settings

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
ServiceCollection c = new();
c.AddOpenAIChatCompletion("gpt-4o-mini", apiKey);
c.AddKernel();
IServiceProvider services = c.BuildServiceProvider();

Kernel kernel = services.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Location>();

PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

IChatCompletionService chatService = services.GetRequiredService<IChatCompletionService>();

ChatHistory history = new();
history.AddUserMessage("Maíra mora em Los Angeles");
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history, settings, kernel);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
class Location
{
    [KernelFunction]
    public string GetPersonLocation(string name)
    {
        return name switch
        {
            "Maíra" => "Los Angeles",
            "Gláucia" => "Rio de Janeiro",
            "Cynthia" => "São Paulo",
            _ => "Brasil"
        };
    }
}
*/

/*
// demo 9 - logging

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
ServiceCollection c = new();
c.AddOpenAIChatCompletion("gpt-4o-mini", apiKey);
c.AddKernel();
c.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
IServiceProvider services = c.BuildServiceProvider();

Kernel kernel = services.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Location>();

PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

IChatCompletionService chatService = services.GetRequiredService<IChatCompletionService>();

ChatHistory history = new();
history.AddUserMessage("Maíra mora em Los Angeles");
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history, settings, kernel);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
class Location
{
    [KernelFunction]
    public string GetPersonLocation(string name)
    {
        return name switch
        {
            "Maíra" => "Los Angeles",
            "Gláucia" => "Rio de Janeiro",
            "Cynthia" => "São Paulo",
            _ => "Brasil"
        };
    }
}
*/

/*
// demo 10 - plug-ins

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
ServiceCollection c = new();
c.AddOpenAIChatCompletion("gpt-4o-mini", apiKey);
c.AddKernel();
c.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
IServiceProvider services = c.BuildServiceProvider();

Kernel kernel = services.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Location>();
kernel.ImportPluginFromObject(new WebSearchEnginePlugin(new BingConnector(Environment.GetEnvironmentVariable("BING_API_KEY"))));

PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

IChatCompletionService chatService = services.GetRequiredService<IChatCompletionService>();

ChatHistory history = new();
history.AddUserMessage("Maíra mora em Los Angeles");
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history, settings, kernel);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
class Location
{
    [KernelFunction]
    public string GetPersonLocation(string name)
    {
        return name switch
        {
            "Maíra" => "Los Angeles",
            "Gláucia" => "Rio de Janeiro",
            "Cynthia" => "São Paulo",
            _ => "Brasil"
        };
    }
}
*/

// demo 10 - permission filter

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
ServiceCollection c = new();
c.AddOpenAIChatCompletion("gpt-4o-mini", apiKey);
c.AddKernel();
c.AddSingleton<IFunctionInvocationFilter, PermissionFilter>();
IServiceProvider services = c.BuildServiceProvider();

Kernel kernel = services.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Location>();
kernel.ImportPluginFromObject(new WebSearchEnginePlugin(new BingConnector(Environment.GetEnvironmentVariable("BING_API_KEY"))));

PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

IChatCompletionService chatService = services.GetRequiredService<IChatCompletionService>();

ChatHistory history = new();
history.AddUserMessage("Maíra mora em Los Angeles");
while (true)
{
    Console.Write("P: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentAsync(history, settings, kernel);
    history.Add(assistant);
    Console.WriteLine(assistant);
}
class Location
{
    [KernelFunction]
    public string GetPersonLocation(string name)
    {
        return name switch
        {
            "Maíra" => "Los Angeles",
            "Gláucia" => "Rio de Janeiro",
            "Cynthia" => "São Paulo",
            _ => "Brasil"
        };
    }
}

class PermissionFilter : IFunctionInvocationFilter
{
    public Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        Console.WriteLine($"Ok de chamar a função {context.Function.Name} com {string.Join(", ", context.Arguments)} (s/n)");
        if (Console.ReadLine() == "s")
            return next(context);

        throw new Exception("Usuário não autorizou o chamado da função");
    }
}