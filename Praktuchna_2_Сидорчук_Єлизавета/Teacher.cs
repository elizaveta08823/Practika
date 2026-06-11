namespace Praktuchna_2;

public class Teacher : Person
{
    private string _department = string.Empty;
    private int _experienceYears;

    public string Department
    {
        get => _department;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Назва кафедри не може бути порожньою.");
            }

            _department = value.Trim();
        }
    }

    public int ExperienceYears
    {
        get => _experienceYears;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Стаж не може бути від'ємним.");
            }

            _experienceYears = value;
        }
    }

    public override void ShowDetailedInfo()
    {
        base.ShowDetailedInfo();
        Console.WriteLine($"Кафедра: {Department}");
        Console.WriteLine($"Стаж роботи: {ExperienceYears} років");
    }
}
