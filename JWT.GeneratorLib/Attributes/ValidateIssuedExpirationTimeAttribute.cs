namespace JWT.GeneratorLib.Attributes;

/// <summary>
/// Compara os valores de IssuedAt e Expiration
/// </summary>
public class ValidateIssuedExpirationTimeAttribute : ValidationAttribute
{
    private string _issuedDateOnly { get; }
    private string _issuedTimeOnly { get; }
    private string _expirationTimeOrDate { get; }

    public ValidateIssuedExpirationTimeAttribute(
        string issuedDateOnly,
        string issuedTimeOnly,
        string expirationTimeOrDate)
    {
        _issuedDateOnly = issuedDateOnly;
        _issuedTimeOnly = issuedTimeOnly;
        _expirationTimeOrDate = expirationTimeOrDate;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var issuedDateOnlyInfo = validationContext.ObjectType.GetRuntimeProperty(_issuedDateOnly);
        if (issuedDateOnlyInfo == null)
            return new ValidationResult($"A propridade '{_issuedDateOnly}' não existe");

        var issuedTimeOnlyInfo = validationContext.ObjectType.GetRuntimeProperty(_issuedTimeOnly);
        if (issuedTimeOnlyInfo == null)
            return new ValidationResult($"A propridade '{_issuedTimeOnly}' não existe");

        var expirationTimeOrDateInfo = validationContext.ObjectType.GetRuntimeProperty(_expirationTimeOrDate);
        if (expirationTimeOrDateInfo == null)
            return new ValidationResult($"A propridade '{_expirationTimeOrDate}' não existe");

        var isExpirationTime = TimeOnly.TryParse(expirationTimeOrDateInfo.GetValue(validationContext.ObjectInstance, null)!.ToString()!, out TimeOnly valorTimeOnly);
        var isExpirationDate = DateOnly.TryParse(expirationTimeOrDateInfo.GetValue(validationContext.ObjectInstance, null)!.ToString()!, out DateOnly valorDateOnly);

        var issuedDate = DateOnly.Parse(issuedDateOnlyInfo.GetValue(validationContext.ObjectInstance, null)!.ToString()!);
        var issuedTime = TimeOnly.Parse(issuedTimeOnlyInfo.GetValue(validationContext.ObjectInstance, null)!.ToString()!);

        DateTime dateTimeIssued = new DateTime(issuedDate, issuedTime);
        DateTime dateTimeExpiration = new();
        if (isExpirationTime)
        {
            var valorPropriedadeDateOnly = DateOnly.Parse(value!.ToString()!);
            dateTimeExpiration = new DateTime(valorPropriedadeDateOnly, valorTimeOnly);
        }
        else if (isExpirationDate)
        {
            var valorPropriedadeTimeOnly = TimeOnly.Parse(value!.ToString()!);
            dateTimeExpiration = new DateTime(valorDateOnly, valorPropriedadeTimeOnly);
        }

        var valorIssuedAtEhMaiorQueExpiration = dateTimeIssued > dateTimeExpiration;
        if (valorIssuedAtEhMaiorQueExpiration)
            return new ValidationResult($"O valor do '{nameof(dateTimeExpiration)}' é menor ou igual ao '{nameof(dateTimeIssued)}'!");
        else
            return null;
    }
}
