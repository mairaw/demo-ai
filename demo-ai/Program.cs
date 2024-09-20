using OpenAI;
using System.ClientModel;

string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    throw new InvalidOperationException("The OpenAI API key is not set in the environment variables.");
}

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(apiKey));
var chatService = client.GetChatClient("gpt-4o-mini");

var result = await chatService.CompleteChatAsync("Qual é a cor do céu?");
Console.WriteLine(result.Value);