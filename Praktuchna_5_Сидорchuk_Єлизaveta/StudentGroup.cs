using System.Text.Json;
using System.Text.Json.Serialization;

namespace Praktuchna_5;

public class StudentGroup
{
    private readonly List<Student> _students = new();

    public string GroupName { get; set; } = "К-320";

    public string Specialty { get; set; } = "Комп'ютерні науки";

    public int Course { get; set; } = 3;

    public Teacher? Curator { get; set; }

    public int GroupSize => _students.Count;

    public double AverageGroupGrade =>
        _students.Count == 0 ? 0 : _students.Average(student => student.AverageGrade);

    public IReadOnlyList<Student> Students => _students.AsReadOnly();

    public Student this[int index]
    {
        get
        {
            if (index < 0 || index >= _students.Count)
            {
                throw new StudentNotFoundException("Студента з таким індексом не знайдено.");
            }

            return _students[index];
        }
    }

    public Student this[string recordBookNumber]
    {
        get => FindStudent(recordBookNumber);
    }

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Any(existing => existing.RecordBookNumber == student.RecordBookNumber))
        {
            throw new InvalidOperationException("Студента з таким номером залікової книжки вже додано.");
        }

        _students.Add(student);
    }

    public void RemoveStudent(string recordBookNumber)
    {
        Student student = FindStudent(recordBookNumber);
        _students.Remove(student);
    }

    public Student FindStudent(string recordBookNumber)
    {
        Student? student = _students.FirstOrDefault(item => item.RecordBookNumber == recordBookNumber);

        if (student == null)
        {
            throw new StudentNotFoundException($"Студента з номером залікової книжки {recordBookNumber} не знайдено.");
        }

        return student;
    }

    public List<Student> GetExcellentStudents()
    {
        return _students.Where(student => student.IsExcellent()).ToList();
    }

    public List<Student> GetStudentsByStatus(Student.StudentStatus status)
    {
        return _students.Where(student => student.Status == status).ToList();
    }

    public void SortStudentsByGrade()
    {
        _students.Sort((first, second) => second.CompareTo(first));
    }

    public void CloneStudent(string recordBookNumber)
    {
        Student original = FindStudent(recordBookNumber);
        Student clone = (Student)original.Clone();
        clone.FullName = original.FullName + " (Copy)";
        clone.RecordBookNumber = GenerateUniqueRecordBookNumber(original.RecordBookNumber);
        _students.Add(clone);
    }

    public void SaveToFile(string filePath)
    {
        StudentGroupData data = new()
        {
            GroupName = GroupName,
            Specialty = Specialty,
            Course = Course,
            Curator = Curator,
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
        Curator = data.Curator;
        _students.Clear();
        _students.AddRange(data.Students);
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
        while (_students.Any(student => student.RecordBookNumber == number.ToString("D8")));

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
