namespace JWT.Generator.Services;

public class TokenService : ITokenService
{
    public Task<string> CriarTokenAsync(JsonWebToken jsonWebToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebToken.Key!));
        var credentials = new SigningCredentials(securityKey, jsonWebToken.Algorithm);

        var subject = new Claim { Chave = System.Security.Claims.ClaimTypes.NameIdentifier, Valor = jsonWebToken.Subject! };
        jsonWebToken.Claims?.Add(subject);

        var jwt = new JwtSecurityToken(
            issuer: jsonWebToken.Issuer,
            audience: jsonWebToken.Audience,
            expires: jsonWebToken.Expiration,
            claims: jsonWebToken.Claims?.TransformarClaims(),
            signingCredentials: credentials);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);
            return Task.FromResult(token);
        }
        catch (SecurityTokenEncryptionFailedException ex)
        {
            return Task.FromResult($"A criptografia do token falhou! Confira se a chave tem o mínimo de caracteres e se está correta. Erro: {ex.Message}");
        }
    }
}
