namespace JWT.GeneratorLib.Interfaces;

public interface ITokenService
{
    Task<string> CriarTokenAsync(JsonWebToken jsonWebToken);
}
