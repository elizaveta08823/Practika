using System.Text;

namespace Praktuchna_3;

public class Student : Person, IEntity, IComparable<Student>, ICloneable
{
    public enum StudentStatus
    {
        Active,
        AcademicLeave,
        Expelled,
        Graduated
    }

    private string _recordBookNumber = string.Empty;
    private double _averageGrade;
    private readonly StringBuilder _notesBuilder = new();

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

    public override void ShowDetailedInfo()
    {
        base.ShowDetailedInfo();
        Console.WriteLine($"Номер залікової книжки: {RecordBookNumber}");
        Console.WriteLine($"Середній бал: {AverageGrade:F2}");
        Console.WriteLine($"Статус: {Status}");
        Console.WriteLine($"Дата зарахування: {EnrollmentDate:dd.MM.yyyy}");
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

    public int CompareTo(Student? other)
    {
        if (other is null)
        {
            return 1;
        }

        return AverageGrade.CompareTo(other.AverageGrade);
    }

    public object Clone()
    {
        GradeJournal clonedJournal = new();

        foreach (KeyValuePair<string, double> grade in Journal)
        {
            clonedJournal[grade.Key] = grade.Value;
        }

        Student clone = new()
        {
            FullName = FullName,
            DateOfBirth = DateOfBirth,
            RecordBookNumber = RecordBookNumber,
            PersonalEmail = PersonalEmail,
            EnrollmentDate = EnrollmentDate,
            Status = Status,
            AverageGrade = AverageGrade,
            Journal = clonedJournal
        };

        clone.SetNotes(Notes);

        return clone;
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
