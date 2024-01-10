namespace JWT.Generator.Extensions;

public static class ClaimExtensions
{
    public static IEnumerable<System.Security.Claims.Claim> TransformarClaims(this IEnumerable<Claim> claims)
    {
        foreach (var claim in claims)
            yield return new System.Security.Claims.Claim(claim.Chave!, claim.Valor!);
    }
}
