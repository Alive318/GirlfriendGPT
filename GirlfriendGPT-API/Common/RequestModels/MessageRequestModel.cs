using Newtonsoft.Json;

namespace GirlfriendGPT_API.Common.RequestModels;

public class MessageRequestModel
{
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }
    
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; }
}