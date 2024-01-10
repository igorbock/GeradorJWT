namespace JWT.GeneratorBlazor.Services;

public class TokenService : ITokenService
{
    private readonly HttpClient _httpClient;

    public TokenService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    ~TokenService()
    {
        _httpClient.Dispose();
    }

    public async Task<string> CriarTokenAsync(JsonWebToken jsonWebToken)
    {
        var json = JsonSerializer.Serialize(jsonWebToken);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        var resultado = await _httpClient.PostAsync("api/token", stringContent);
        resultado.EnsureSuccessStatusCode();

        return await resultado.Content.ReadAsStringAsync();
    }
}
