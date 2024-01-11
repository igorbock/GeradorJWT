namespace JWT.Generator.Extensions;

public static class ClaimExtensions
{
    public static ObservableCollection<System.Security.Claims.Claim> TransformarClaims(this ObservableCollection<Claim> claims)
    {
        var retorno = new ObservableCollection<System.Security.Claims.Claim>();
        foreach (var claim in claims)
            retorno.Add(new System.Security.Claims.Claim(claim.Chave!, claim.Valor!));

        return retorno;
    }
}
