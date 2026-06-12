using System.Reflection;

namespace Praktuchna_9;

public static class ReflectionAnalyzer
{
    public static void AnalyzeClassMetadata(Type type)
    {
        Console.WriteLine($"Аналіз класу: {type.FullName}");
        Console.WriteLine();
        Console.WriteLine("Публічні властивості:");

        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo property in properties)
        {
            Console.WriteLine($"- {property.PropertyType.Name} {property.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Публічні методи:");

        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        foreach (MethodInfo method in methods)
        {
            if (method.DeclaringType == typeof(object))
            {
                continue;
            }

            if (method.IsSpecialName)
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();
            string parameterList = string.Join(", ", parameters.Select(parameter => $"{parameter.ParameterType.Name} {parameter.Name}"));
            Console.WriteLine($"- {method.ReturnType.Name} {method.Name}({parameterList})");
        }
    }

    public static void GetDeveloperInfo(Type type)
    {
        DeveloperInfoAttribute? attribute = type.GetCustomAttribute<DeveloperInfoAttribute>();

        if (attribute == null)
        {
            Console.WriteLine("Атрибут DeveloperInfo не знайдено.");
            return;
        }

        Console.WriteLine($"Розробник: {attribute.DeveloperName}");
        Console.WriteLine($"Дата створення: {attribute.DateCreated}");
    }
}
