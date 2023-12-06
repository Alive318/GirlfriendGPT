using GirlfriendGPT_API.Common.DeserializeModels;
using GirlfriendGPT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GirlfriendGPT_API.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class GirlfriendController : ControllerBase
{
    [HttpGet("/Girlfriends")]
    public async Task<JsonResult> Index()
    {
        var girlfriends = await GetGirlfriends();
        return new JsonResult(girlfriends);
    }

    private async Task<Girlfriends> GetGirlfriends()
    {
        var pathEnglish = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "girlfriends-en.json");
        var jsonStringEnglish = await System.IO.File.ReadAllTextAsync(pathEnglish);
        
        var pathGerman = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "girlfriends-de.json");
        var jsonStringGerman = await System.IO.File.ReadAllTextAsync(pathGerman);
        
        
        var englishGirlfriends = JsonConvert.DeserializeObject<List<GirlfriendModel>>(jsonStringEnglish) 
                                 ?? new List<GirlfriendModel>();
        
        var germanGirlfriends = JsonConvert.DeserializeObject<List<GirlfriendModel>>(jsonStringGerman) 
                                 ?? new List<GirlfriendModel>();

        return new Girlfriends { GirlfriendsGerman = germanGirlfriends, GirlfriendsEnglish = englishGirlfriends };
    }
}