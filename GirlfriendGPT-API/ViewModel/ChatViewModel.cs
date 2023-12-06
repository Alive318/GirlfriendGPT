using Newtonsoft.Json;

namespace GirlfriendGPT_API.ViewModel;

public class ChatViewModel
{
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }
    
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; }
}