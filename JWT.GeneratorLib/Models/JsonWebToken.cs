namespace JWT.GeneratorLib.Models;

public class JsonWebToken
{
    /// <summary>
    /// Identificador ou nome do emissor do token. Geralmente uma URL.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(Issuer)} é obrigatório!")]
    public string? Issuer { get; set; }

    /// <summary>
    /// Data e hora da emissão do token.
    /// </summary>
    public DateTime? IssuedAt
    {
        get
        {
            if(IssuedDay.HasValue && IssuedTime.HasValue)
                return new DateTime(IssuedDay.Value, IssuedTime.Value);

            return null;
        }
    }

    /// <summary>
    /// Dia da emissão do token.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(IssuedDay)} é obrigatório!")]
    public DateOnly? IssuedDay { get; set; }

    /// <summary>
    /// Hora da emissão do token.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(IssuedTime)} é obrigatório!")]
    public TimeOnly? IssuedTime { get; set; }

    /// <summary>
    /// Data e hora da expiração do token.
    /// </summary>
    public DateTime? Expiration
    {
        get
        {
            if(ExpirationDay.HasValue && ExpirationTime.HasValue)
                return new DateTime(ExpirationDay.Value, ExpirationTime.Value);

            return null;
        }
    }

    /// <summary>
    /// Dia da expiração do token.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(ExpirationDay)} é obrigatório!")]
    [ValidateIssuedExpirationTime(nameof(IssuedDay), nameof(IssuedTime), nameof(ExpirationTime))]
    public DateOnly? ExpirationDay { get; set; }

    /// <summary>
    /// Hora da expiração do token.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(ExpirationTime)} é obrigatório!")]
    [ValidateIssuedExpirationTime(nameof(IssuedDay), nameof(IssuedTime), nameof(ExpirationDay))]
    public TimeOnly? ExpirationTime { get; set; }

    /// <summary>
    /// Identificador ou nome do destinatário do token. O destinatário deve corresponder para validação.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(Audience)} é obrigatório!")]
    public string? Audience { get; set; }

    /// <summary>
    /// Id ou nome do representante do token. Geralmente um usuário.
    /// </summary>
    [Required(ErrorMessage = $"O campo {nameof(Subject)} é obrigatório!")]
    public string? Subject { get; set; }

    /// <summary>
    /// Claims do token. Pares de chave e valor para indicar permissões ao usuário do token.
    /// </summary>
    public ObservableCollection<Claim>? Claims { get; set; }

    /// <summary>
    /// Chave de segurança única que é o valor que o algoritmo vai usar para criptografar o token. Mínimo de 32 caracteres.
    /// </summary>
    //[MinLength(32, ErrorMessage = $"O campo {nameof(Key)} deve ter no mínimo 32 caracteres")]
    [ValidateKeyLength(nameof(Algorithm))]
    [Required(ErrorMessage = $"O campo {nameof(Key)} é obrigatório!")]
    public string? Key { get; set; }

    /// <summary>
    /// Tipo de algoritmo usado na criptografia. Utilizar a biblioteca Microsoft.IdentityModel.Tokens.SecurityAlgorithms
    /// </summary>
    [Required(ErrorMessage = "Escolha um valor para o campo!")]
    [MinLength(4, ErrorMessage = "Escolha um valor para o campo!")]
    public string? Algorithm { get; set; }
}
