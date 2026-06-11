using System.Text.Json;
using System.Text.Json.Serialization;

namespace Praktuchna_1;

public class StudentGroup
{
    private readonly List<Student> _students = new();

    public string GroupName { get; set; } = "К-320";

    public string Specialty { get; set; } = "Комп'ютерні науки";

    public int Course { get; set; } = 3;

    public int GroupSize => _students.Count;

    public double AverageGroupGrade =>
        _students.Count == 0 ? 0 : _students.Average(student => student.AverageGrade);

    public IReadOnlyList<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Any(existing => existing.RecordBookNumber == student.RecordBookNumber))
        {
            throw new InvalidOperationException("Студента з таким номером залікової книжки вже додано.");
        }

        _students.Add(student);
    }

    public bool RemoveStudent(string recordBookNumber)
    {
        Student? student = FindStudent(recordBookNumber);

        if (student == null)
        {
            return false;
        }

        return _students.Remove(student);
    }

    public Student? FindStudent(string recordBookNumber)
    {
        return _students.FirstOrDefault(student => student.RecordBookNumber == recordBookNumber);
    }

    public List<Student> GetExcellentStudents()
    {
        return _students.Where(student => student.IsExcellent()).ToList();
    }

    public List<Student> GetStudentsByStatus(Student.StudentStatus status)
    {
        return _students.Where(student => student.Status == status).ToList();
    }

    public void SaveToFile(string filePath)
    {
        StudentGroupData data = new()
        {
            GroupName = GroupName,
            Specialty = Specialty,
            Course = Course,
            Students = _students
        };

        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, json);
    }

    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не знайдено.", filePath);
        }

        string json = File.ReadAllText(filePath);

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        StudentGroupData? data = JsonSerializer.Deserialize<StudentGroupData>(json, options);

        if (data == null)
        {
            throw new InvalidDataException("Не вдалося прочитати дані з файлу.");
        }

        GroupName = data.GroupName;
        Specialty = data.Specialty;
        Course = data.Course;
        _students.Clear();
        _students.AddRange(data.Students);
    }

    private class StudentGroupData
    {
        public string GroupName { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public int Course { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
