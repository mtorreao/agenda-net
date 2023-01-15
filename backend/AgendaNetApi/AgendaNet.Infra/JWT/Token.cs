namespace AgendaNet.Infra.JWT;

public class Token
{
    public string? Created { get; set; }
    public string? Expiration { get; set; }
    public string? AccessToken { get; set; }
}