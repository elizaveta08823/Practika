using System.Net.Mail;
using System.Text;

namespace Praktuchna_1;

public class Student
{
    public enum StudentStatus
    {
        Active,
        AcademicLeave,
        Expelled,
        Graduated
    }

    private string _fullName = string.Empty;
    private string _recordBookNumber = string.Empty;
    private double _averageGrade;
    private string _personalEmail = string.Empty;
    private readonly StringBuilder _notesBuilder = new();

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

    public DateTime DateOfBirth { get; set; }

    public int Age => CalculateAge();

    public string RecordBookNumber
    {
        get => _recordBookNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 8 || !value.All(char.IsDigit))
            {
                throw new ArgumentException("Номер залікової книжки має містити рівно 8 цифр.");
            }

            _recordBookNumber = value;
        }
    }

    public double AverageGrade
    {
        get => _averageGrade;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Середній бал має бути від 0 до 100.");
            }

            _averageGrade = value;
        }
    }

    public StudentStatus Status { get; set; }

    public DateTime EnrollmentDate { get; init; }

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

    public string Notes
    {
        get => _notesBuilder.ToString();
        private set
        {
            _notesBuilder.Clear();

            if (!string.IsNullOrEmpty(value))
            {
                _notesBuilder.Append(value);
            }
        }
    }

    public GradeJournal Journal { get; set; } = new();

    public void ShowDetailedInfo()
    {
        Console.WriteLine($"ПІБ: {FullName}");
        Console.WriteLine($"Дата народження: {DateOfBirth:dd.MM.yyyy}");
        Console.WriteLine($"Вік: {Age}");
        Console.WriteLine($"Номер залікової книжки: {RecordBookNumber}");
        Console.WriteLine($"Середній бал: {AverageGrade:F2}");
        Console.WriteLine($"Статус: {Status}");
        Console.WriteLine($"Дата зарахування: {EnrollmentDate:dd.MM.yyyy}");
        Console.WriteLine($"Електронна пошта: {PersonalEmail}");
        Console.WriteLine($"Примітки: {(string.IsNullOrWhiteSpace(Notes) ? "немає" : Notes)}");
        Console.WriteLine($"Відмінник: {(IsExcellent() ? "так" : "ні")}");
        Console.WriteLine($"На відрахування: {(IsFailing() ? "так" : "ні")}");
        Console.WriteLine($"Років до закінчення: {GetYearsToGraduation()}");

        if (Journal.Count > 0)
        {
            Console.WriteLine("Оцінки з журналу:");

            foreach (KeyValuePair<string, double> grade in Journal)
            {
                Console.WriteLine($"  {grade.Key}: {grade.Value:F2}");
            }
        }
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(newGrade), "Середній бал має бути від 0 до 100.");
        }

        AverageGrade = newGrade;
    }

    public bool IsExcellent()
    {
        return AverageGrade >= 90;
    }

    public bool IsFailing()
    {
        return AverageGrade < 60;
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

    public int GetYearsToGraduation()
    {
        DateTime graduationDate = EnrollmentDate.AddYears(4);
        int yearsLeft = graduationDate.Year - DateTime.Today.Year;

        if (graduationDate.Date < DateTime.Today.AddYears(yearsLeft))
        {
            yearsLeft--;
        }

        return Math.Max(0, yearsLeft);
    }

    public void SetNotes(string notes)
    {
        Notes = notes;
    }

    public void SyncAverageFromJournal()
    {
        AverageGrade = Journal.RecalculateAverageGrade();
    }
}
