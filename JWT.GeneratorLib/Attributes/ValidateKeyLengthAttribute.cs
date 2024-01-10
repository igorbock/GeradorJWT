namespace JWT.GeneratorLib.Attributes;

public class ValidateKeyLengthAttribute : ValidationAttribute
{
    private string _algorithmProperty { get; }

    public ValidateKeyLengthAttribute(string algorithmProperty)
    {
        _algorithmProperty = algorithmProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return null;

        var algorithmPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(_algorithmProperty);
        if (algorithmPropertyInfo == null)
            return new ValidationResult($"A propridade '{_algorithmProperty}' não existe!");

        var algorithmObject = algorithmPropertyInfo.GetValue(validationContext.ObjectInstance, null);
        if(algorithmObject == null)
            return new ValidationResult($"A valor da propridade '{_algorithmProperty}' é nulo!");

        var algorithmValue = algorithmObject.ToString();
        var keyValue = value!.ToString();
        var lengthKeyValue = keyValue!.Length;

        switch(algorithmValue)
        {
            case "HS256":
                if (lengthKeyValue >= 32)
                    return null;
                return new ValidationResult("O campo 'Key' deve ter no mínimo 32 caracteres!");
            case "HS384":
                if (lengthKeyValue >= 48)
                    return null;
                return new ValidationResult("O campo 'Key' deve ter no mínimo 48 caracteres!");
            case "HS512":
                if (lengthKeyValue >= 65)
                    return null;
                return new ValidationResult("O campo 'Key' deve ter no mínimo 65 caracteres!");

            default: return null;
        }
    }
}
