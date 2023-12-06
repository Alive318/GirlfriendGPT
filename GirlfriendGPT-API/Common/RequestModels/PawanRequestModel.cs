using GirlfriendGPT_API.ViewModel;
using Newtonsoft.Json;

namespace GirlfriendGPT_API.Common.RequestModels;

public class ChatGptRequestModel
{
    [JsonProperty(PropertyName = "messages")]
    public List<ChatViewModel> Messages { get; set; }
    
    [JsonProperty(PropertyName = "model")]
    public string Model { get; set; } = "gpt-3.5-turbo";
}