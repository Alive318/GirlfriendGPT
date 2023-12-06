namespace GirlfriendGPT_API.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}