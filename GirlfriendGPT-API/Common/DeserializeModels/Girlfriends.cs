using GirlfriendGPT_API.Models;
using Newtonsoft.Json;

namespace GirlfriendGPT_API.Common.DeserializeModels;

public class Girlfriends
{
    public List<GirlfriendModel> GirlfriendsEnglish { get; set; }
    public List<GirlfriendModel> GirlfriendsGerman { get; set; }
}