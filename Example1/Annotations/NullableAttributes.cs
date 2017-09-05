using System.ComponentModel.DataAnnotations;

public class NullableAttribute : ValidationAttribute
{
    public NullableAttribute() : base("The {0} field is invalid") { }
}
public class NullableEmailAddressAttribute : NullableAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        string inputValue = value as string;
        var emailAddressAttribute = new EmailAddressAttribute();

        return (inputValue != null) && (string.IsNullOrEmpty(inputValue) || emailAddressAttribute.IsValid(inputValue));
    }
}

public class NullablePhoneAttribute : NullableAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        string inputValue = value as string;
        var phoneAttribute = new PhoneAttribute();

        return (inputValue != null) && (string.IsNullOrEmpty(inputValue) || phoneAttribute.IsValid(inputValue));
    }
}

public class NullableUrlAttribute : NullableAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        string inputValue = value as string;
        var urlAttribute = new UrlAttribute();

        return (inputValue != null) && (string.IsNullOrEmpty(inputValue) || urlAttribute.IsValid(inputValue));
    }
}

public class NullableFieExtensionsAttribute : NullableAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        string inputValue = value as string;
        var fileExtensionsAttribute = new FileExtensionsAttribute();

        return (inputValue != null) && (string.IsNullOrEmpty(inputValue) || fileExtensionsAttribute.IsValid(inputValue));
    }
}
