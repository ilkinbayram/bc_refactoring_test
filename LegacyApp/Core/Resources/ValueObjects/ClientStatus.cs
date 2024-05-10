using LegacyApp.Core.Resources.Base.Abstract;

namespace LegacyApp.Core.Resources.ValueObjects;

/// <summary>
/// 
/// </summary>
public class ClientStatus : ValueObject
{
    public string Value { get; } = default!;
    public static ClientStatus None => new(nameof(None));

    public readonly static ClientStatus[] All = { None };

    protected ClientStatus()
    {
    }

    private ClientStatus(string value)
    {
        Value = value;
    }

    public static ClientStatus FromValue(string value)
    {
        value = value?.Trim() ?? throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

        var status = All.FirstOrDefault(x => x.Value == value);
        if (status == null)
        {
            throw new ArgumentException($"No ClientStatus found with the value '{value}'.", nameof(value));
        }

        return status;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}