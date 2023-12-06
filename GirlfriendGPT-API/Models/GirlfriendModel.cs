using System.Text.Json.Serialization;

namespace GirlfriendGPT_API.Models;

public class GirlfriendModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public List<string> Behavior { get; set; }

    public List<string> Identity { get; set; }
    
    public string Picture { get; set; }
}