using System.Net.Mail;

namespace Praktuchna_7;

public abstract class Person
{
    private string _fullName = string.Empty;
    private string _personalEmail = string.Empty;

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Повне ім'я не може бути порожнім.");
            }

            if (value.Trim().Length < 5)
            {
                throw new ArgumentException("Повне ім'я має містити щонайменше 5 символів.");
            }

            _fullName = value.Trim();
        }
    }

    protected void AssignFullName(string value)
    {
        _fullName = value.Trim();
    }

    public DateTime DateOfBirth { get; set; }

    public int Age => CalculateAge();

    public string PersonalEmail
    {
        get => _personalEmail;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Електронна пошта не може бути порожньою.");
            }

            try
            {
                _ = new MailAddress(value.Trim());
            }
            catch (FormatException)
            {
                throw new ArgumentException("Некоректний формат електронної пошти.");
            }

            _personalEmail = value.Trim();
        }
    }

    public int CalculateAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - DateOfBirth.Year;

        if (DateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public virtual void ShowDetailedInfo()
    {
        Console.WriteLine($"ПІБ: {FullName}");
        Console.WriteLine($"Дата народження: {DateOfBirth:dd.MM.yyyy}");
        Console.WriteLine($"Вік: {Age}");
        Console.WriteLine($"Електронна пошта: {PersonalEmail}");
    }
}
