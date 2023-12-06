using System.Diagnostics;
using GirlfriendGPT_API.Common.RequestModels;
using GirlfriendGPT_API.Common.SerializeModels;
using GirlfriendGPT_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GirlfriendGPT_API.Controllers;

[Controller]
public class ChatController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ChatController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/");
        
        // openai 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
        // pawan pk-iuhPPzbJenBwbdiaLdGiTULkaobBdrFWAmGXEFxuFtwtFVsc
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
    }

    [HttpPost("/Chat")]
    public async Task<IActionResult> Chat([FromBody] List<ChatViewModel> chatViewModels)
    {
        try
        {
            // Konvertiere die Liste in ein JSON-Format, das von der API erwartet wird
            var requestModel = new ChatGptRequestModel { Messages = chatViewModels };

            var jsonContent = JsonConvert.SerializeObject(requestModel);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(12));
            var response = await _httpClient.PostAsync("/v1/chat/completions", httpContent, cancellationToken.Token);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ChatCompletions>(responseContent);

                return Ok(responseData.Choices.Select(c => c.Message));
            }

            if ((int)response.StatusCode == 429)
            {
                Console.WriteLine("Es kam 429 zurück, rekursiv erneut probieren in 5 sek");
                Thread.Sleep(5000);
                return await Chat(chatViewModels);

            }


            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("timeout");
            return await Chat(chatViewModels);
        }
        catch (Exception ex)
        {
            // Behandle Ausnahmen hier nach Bedarf
            return StatusCode(500, ex.Message);
        }
    }
}