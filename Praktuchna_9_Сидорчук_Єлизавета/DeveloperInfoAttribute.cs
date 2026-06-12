namespace Praktuchna_9;

[AttributeUsage(AttributeTargets.Class)]
public class DeveloperInfoAttribute : Attribute
{
    public string DeveloperName { get; }

    public string DateCreated { get; }

    public DeveloperInfoAttribute(string developerName, string dateCreated)
    {
        DeveloperName = developerName;
        DateCreated = dateCreated;
    }
}
