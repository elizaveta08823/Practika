using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Praktuchna_8;

public class StudentGroup
{
    public delegate void GroupNotificationHandler(string message);

    private readonly Repository<Student> _studentRepository = new();

    public string GroupName { get; set; } = "К-320";

    public string Specialty { get; set; } = "Комп'ютерні науки";

    public int Course { get; set; } = 3;

    public Teacher? Curator { get; set; }

    public event GroupNotificationHandler? OnStudentAdded;

    private List<Student> StudentsInternal => _studentRepository.GetAll();

    public int GroupSize => StudentsInternal.Count;

    public double AverageGroupGrade =>
        StudentsInternal.Count == 0 ? 0 : StudentsInternal.Average(student => student.AverageGrade);

    public IReadOnlyList<Student> Students => StudentsInternal.AsReadOnly();

    public Student this[int index]
    {
        get
        {
            List<Student> students = StudentsInternal;

            if (index < 0 || index >= students.Count)
            {
                throw new StudentNotFoundException("Студента з таким індексом не знайдено.");
            }

            return students[index];
        }
    }

    public Student this[string recordBookNumber]
    {
        get => FindStudent(recordBookNumber);
    }

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (StudentsInternal.Any(existing => existing.RecordBookNumber == student.RecordBookNumber))
        {
            throw new InvalidOperationException("Студента з таким номером залікової книжки вже додано.");
        }

        _studentRepository.Add(student);
        OnStudentAdded?.Invoke($"Студента {student.FullName} успішно додано до групи {GroupName}.");
    }

    public void RemoveStudent(string recordBookNumber)
    {
        Student student = FindStudent(recordBookNumber);
        _studentRepository.Remove(student);
    }

    public Student FindStudent(string recordBookNumber)
    {
        Student? student = StudentsInternal.FirstOrDefault(item => item.RecordBookNumber == recordBookNumber);

        if (student == null)
        {
            throw new StudentNotFoundException($"Студента з номером залікової книжки {recordBookNumber} не знайдено.");
        }

        return student;
    }

    public List<Student> GetExcellentStudents()
    {
        return StudentsInternal.Where(student => student.IsExcellent()).ToList();
    }

    public List<Student> GetStudentsByStatus(Student.StudentStatus status)
    {
        return StudentsInternal.Where(student => student.Status == status).ToList();
    }

    public List<Student> GetTopStudents(int count)
    {
        return StudentsInternal
            .OrderByDescending(student => student.AverageGrade)
            .Take(count)
            .ToList();
    }

    public double GetAverageGradeOfActiveStudents()
    {
        List<Student> activeStudents = StudentsInternal
            .Where(student => student.Status == Student.StudentStatus.Active)
            .ToList();

        return activeStudents.Count == 0 ? 0 : activeStudents.Average(student => student.AverageGrade);
    }

    public void SortStudentsByGrade()
    {
        StudentsInternal.Sort((first, second) => second.CompareTo(first));
    }

    public void CloneStudent(string recordBookNumber)
    {
        Student original = FindStudent(recordBookNumber);
        Student clone = (Student)original.Clone();
        clone.FullName = original.FullName + " (Copy)";
        clone.RecordBookNumber = GenerateUniqueRecordBookNumber(original.RecordBookNumber);
        _studentRepository.Add(clone);
    }

    public async Task SaveToFileAsync(string filePath)
    {
        StudentGroupData data = new()
        {
            GroupName = GroupName,
            Specialty = Specialty,
            Course = Course,
            Curator = Curator,
            Students = StudentsInternal
        };

        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        await using FileStream stream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, data, options);
    }

    public async Task LoadFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не знайдено.", filePath);
        }

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        await using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        StudentGroupData? data = await JsonSerializer.DeserializeAsync<StudentGroupData>(stream, options);

        if (data == null)
        {
            throw new InvalidDataException("Не вдалося прочитати дані з файлу.");
        }

        GroupName = data.GroupName;
        Specialty = data.Specialty;
        Course = data.Course;
        Curator = data.Curator;
        StudentsInternal.Clear();
        StudentsInternal.AddRange(data.Students);
    }

    public async Task<string> GenerateHeavyReportAsync()
    {
        await Task.Delay(3000);

        return $"Аналітичний звіт групи {GroupName}: кількість студентів — {GroupSize}, середній бал — {AverageGroupGrade:F2}.";
    }

    private string GenerateUniqueRecordBookNumber(string sourceNumber)
    {
        long number = long.Parse(sourceNumber);

        do
        {
            number++;

            if (number > 99999999)
            {
                number = 10000000;
            }
        }
        while (StudentsInternal.Any(student => student.RecordBookNumber == number.ToString("D8")));

        return number.ToString("D8");
    }

    private class StudentGroupData
    {
        public string GroupName { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public int Course { get; set; }

        public Teacher? Curator { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
