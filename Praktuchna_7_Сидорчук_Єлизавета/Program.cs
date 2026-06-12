using System.Globalization;

namespace Praktuchna_7;

internal class Program
{
    private static readonly StudentGroup Group = new();
    private static readonly string DataFilePath = Path.Combine(AppContext.BaseDirectory, "students_k320.json");

    static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
        CultureInfo.CurrentUICulture = new CultureInfo("uk-UA");

        Group.OnStudentAdded += HandleStudentAdded;

        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();
            Console.Write("Оберіть пункт меню: ");
            string? choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    try { AddStudent(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "2":
                    try { RemoveStudent(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "3":
                    try { ShowAllStudents(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "4":
                    try { SearchStudent(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "5":
                    try { EditStudent(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "6":
                    try { ShowExcellentStudents(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "7":
                    try { ShowStatistics(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "8":
                    try { SaveOrLoadData(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "9":
                    try { AssignCurator(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "10":
                    try { ShowCuratorInfo(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "11":
                    try { SortStudentsByGrade(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "12":
                    try { CloneStudentByRecordBook(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "13":
                    try { CompareStudentsByGrade(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "14":
                    try { GetStudentByIndexer(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "15":
                    try { CheckScholarshipEligibility(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "16":
                    try { GenerateArtificialError(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "17":
                    try { DemonstrateGenerics(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "18":
                    try { ShowTopStudents(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "19":
                    try { ShowAverageGradeOfActiveStudents(); }
                    catch (InvalidStudentDataException ex) { WriteError(ex.Message); }
                    catch (StudentNotFoundException ex) { WriteError(ex.Message); }
                    catch (Exception ex) { WriteError(ex.Message); }
                    break;
                case "0":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void WriteError(string message)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    private static void WriteSuccess(string message)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    private static void HandleStudentAdded(string message)
    {
        WriteSuccess(message);
    }

    private static void ShowMenu()
    {
        Console.WriteLine("=== Група К-320 | Сидорчук Єлизавета | Практична робота №7 ===");
        Console.WriteLine($"Група: {Group.GroupName} | Спеціальність: {Group.Specialty} | Курс: {Group.Course}");
        Console.WriteLine("1. Додати студента");
        Console.WriteLine("2. Видалити студента");
        Console.WriteLine("3. Вивести всіх студентів");
        Console.WriteLine("4. Пошук студента");
        Console.WriteLine("5. Редагування студента");
        Console.WriteLine("6. Відмінники");
        Console.WriteLine("7. Статистика групи");
        Console.WriteLine("8. Зберегти / завантажити дані");
        Console.WriteLine("9. Призначити куратора групи");
        Console.WriteLine("10. Вивести інформацію про куратора");
        Console.WriteLine("11. Відсортувати студентів за середнім балом");
        Console.WriteLine("12. Клонувати студента");
        Console.WriteLine("13. Порівняти двох студентів за балами");
        Console.WriteLine("14. Отримати студента за індексом або заліковкою");
        Console.WriteLine("15. Перевірити право на стипендію");
        Console.WriteLine("16. Штучна генерація помилки");
        Console.WriteLine("17. Демонстрація роботи Generics");
        Console.WriteLine("18. Вивести Топ-3 студентів");
        Console.WriteLine("19. Середній бал активних студентів");
        Console.WriteLine("0. Вихід");
    }

    private static void AddStudent()
    {
        Student student = ReadStudentFromConsole();
        Group.AddStudent(student);
        Console.WriteLine("Студента успішно додано.");
    }

    private static void RemoveStudent()
    {
        Console.Write("Введіть номер залікової книжки: ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Group.RemoveStudent(recordBookNumber);
        Console.WriteLine("Студента видалено.");
    }

    private static void ShowAllStudents()
    {
        if (Group.GroupSize == 0)
        {
            Console.WriteLine("Список студентів порожній.");
            return;
        }

        int index = 1;

        foreach (Student student in Group.Students)
        {
            Console.WriteLine($"--- Студент {index} ---");
            student.ShowDetailedInfo();
            Console.WriteLine();
            index++;
        }
    }

    private static void SearchStudent()
    {
        Console.Write("Введіть номер залікової книжки: ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Student student = Group.FindStudent(recordBookNumber);
        student.ShowDetailedInfo();
    }

    private static void EditStudent()
    {
        Console.Write("Введіть номер залікової книжки студента для редагування: ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Student student = Group.FindStudent(recordBookNumber);

        Console.WriteLine("Залиште поле порожнім, щоб не змінювати значення.");
        Console.Write($"ПІБ [{student.FullName}]: ");
        string? fullName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(fullName))
        {
            student.FullName = fullName;
        }

        Console.Write($"Електронна пошта [{student.PersonalEmail}]: ");
        string? email = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(email))
        {
            student.PersonalEmail = email;
        }

        Console.Write($"Статус (Active/AcademicLeave/Expelled/Graduated) [{student.Status}]: ");
        string? statusInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(statusInput) && Enum.TryParse(statusInput, true, out Student.StudentStatus status))
        {
            student.Status = status;
        }

        Console.Write($"Середній бал [{student.AverageGrade:F2}]: ");
        string? gradeInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(gradeInput) && double.TryParse(gradeInput, out double grade))
        {
            student.UpdateAverageGrade(grade);
        }

        Console.Write($"Примітки [{student.Notes}]: ");
        string? notes = Console.ReadLine();

        if (notes != null)
        {
            student.SetNotes(notes);
        }

        Console.WriteLine("Дані студента оновлено.");
    }

    private static void ShowExcellentStudents()
    {
        List<Student> excellentStudents = Group.GetExcellentStudents();

        if (excellentStudents.Count == 0)
        {
            Console.WriteLine("Відмінників у групі немає.");
            return;
        }

        foreach (Student student in excellentStudents)
        {
            student.ShowDetailedInfo();
            Console.WriteLine();
        }
    }

    private static void ShowStatistics()
    {
        Console.WriteLine($"Назва групи: {Group.GroupName}");
        Console.WriteLine($"Спеціальність: {Group.Specialty}");
        Console.WriteLine($"Курс: {Group.Course}");
        Console.WriteLine($"Кількість студентів: {Group.GroupSize}");
        Console.WriteLine($"Середній бал групи: {Group.AverageGroupGrade:F2}");
        Console.WriteLine($"Активних: {Group.GetStudentsByStatus(Student.StudentStatus.Active).Count}");
        Console.WriteLine($"На академічній відпустці: {Group.GetStudentsByStatus(Student.StudentStatus.AcademicLeave).Count}");
        Console.WriteLine($"Відрахованих: {Group.GetStudentsByStatus(Student.StudentStatus.Expelled).Count}");
        Console.WriteLine($"Випускників: {Group.GetStudentsByStatus(Student.StudentStatus.Graduated).Count}");
        Console.WriteLine($"Відмінників: {Group.GetExcellentStudents().Count}");
        Console.WriteLine($"Куратор: {(Group.Curator == null ? "не призначено" : Group.Curator.FullName)}");
    }

    private static void SaveOrLoadData()
    {
        Console.WriteLine("1. Зберегти у файл");
        Console.WriteLine("2. Завантажити з файлу");
        Console.Write("Оберіть дію: ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Group.SaveToFile(DataFilePath);
            Console.WriteLine($"Дані збережено у файл: {DataFilePath}");
        }
        else if (choice == "2")
        {
            Group.LoadFromFile(DataFilePath);
            Console.WriteLine($"Дані завантажено з файлу: {DataFilePath}");
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }

    private static void AssignCurator()
    {
        Teacher curator = ReadTeacherFromConsole();
        Group.Curator = curator;
        Console.WriteLine("Куратора групи успішно призначено.");
    }

    private static void ShowCuratorInfo()
    {
        if (Group.Curator == null)
        {
            Console.WriteLine("Куратор групи ще не призначений.");
            return;
        }

        Group.Curator.ShowDetailedInfo();
    }

    private static void SortStudentsByGrade()
    {
        if (Group.GroupSize == 0)
        {
            Console.WriteLine("Список студентів порожній.");
            return;
        }

        Group.SortStudentsByGrade();
        Console.WriteLine("Студентів відсортовано за середнім балом (за спаданням).");
    }

    private static void CloneStudentByRecordBook()
    {
        Console.Write("Введіть номер залікової книжки: ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Group.CloneStudent(recordBookNumber);
        Console.WriteLine("Студента успішно клоновано.");
    }

    private static void CompareStudentsByGrade()
    {
        Console.Write("Номер залікової книжки першого студента: ");
        string firstNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Номер залікової книжки другого студента: ");
        string secondNumber = Console.ReadLine()?.Trim() ?? string.Empty;

        Student firstStudent = Group.FindStudent(firstNumber);
        Student secondStudent = Group.FindStudent(secondNumber);

        if (firstStudent > secondStudent)
        {
            Console.WriteLine($"{firstStudent.FullName} має вищий середній бал ({firstStudent.AverageGrade:F2}), ніж {secondStudent.FullName} ({secondStudent.AverageGrade:F2}).");
        }
        else if (firstStudent < secondStudent)
        {
            Console.WriteLine($"{secondStudent.FullName} має вищий середній бал ({secondStudent.AverageGrade:F2}), ніж {firstStudent.FullName} ({firstStudent.AverageGrade:F2}).");
        }
        else
        {
            Console.WriteLine("Середні бали студентів однакові.");
        }
    }

    private static void GetStudentByIndexer()
    {
        if (Group.GroupSize == 0)
        {
            Console.WriteLine("Список студентів порожній.");
            return;
        }

        Console.WriteLine("1. За порядковим номером у списку");
        Console.WriteLine("2. За номером залікової книжки");
        Console.Write("Оберіть спосіб: ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write($"Введіть індекс (від 0 до {Group.GroupSize - 1}): ");
            string? indexInput = Console.ReadLine();

            if (!int.TryParse(indexInput, out int index))
            {
                throw new InvalidStudentDataException("Некоректний індекс.");
            }

            Student student = Group[index];
            student.ShowDetailedInfo();
        }
        else if (choice == "2")
        {
            Console.Write("Введіть номер залікової книжки: ");
            string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
            Student student = Group[recordBookNumber];
            student.ShowDetailedInfo();
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }

    private static void CheckScholarshipEligibility()
    {
        Console.Write("Введіть номер залікової книжки: ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Student student = Group.FindStudent(recordBookNumber);

        if (student.IsEligibleForScholarship())
        {
            Console.WriteLine($"{student.FullName} має право на стипендію.");
        }
        else
        {
            Console.WriteLine($"{student.FullName} не має права на стипендію.");
        }
    }

    private static void GenerateArtificialError()
    {
        Console.WriteLine("1. Створити студента з порожнім іменем");
        Console.WriteLine("2. Знайти неіснуючого студента");
        Console.Write("Оберіть варіант: ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Student student = new();
            student.FullName = string.Empty;
        }
        else if (choice == "2")
        {
            Group.FindStudent("00000000");
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }

    private static void ShowTopStudents()
    {
        if (Group.GroupSize == 0)
        {
            Console.WriteLine("Список студентів порожній.");
            return;
        }

        List<Student> topStudents = Group.GetTopStudents(3);

        Console.WriteLine("Топ-3 студентів за середнім балом:");
        Console.WriteLine();

        int place = 1;

        foreach (Student student in topStudents)
        {
            Console.WriteLine($"{place}. {student.FullName} — {student.AverageGrade:F2}");
            place++;
        }
    }

    private static void ShowAverageGradeOfActiveStudents()
    {
        if (Group.GroupSize == 0)
        {
            Console.WriteLine("Список студентів порожній.");
            return;
        }

        double averageGrade = Group.GetAverageGradeOfActiveStudents();
        int activeCount = Group.GetStudentsByStatus(Student.StudentStatus.Active).Count;

        Console.WriteLine($"Кількість активних студентів: {activeCount}");
        Console.WriteLine($"Середній бал активних студентів: {averageGrade:F2}");
    }

    private static void DemonstrateGenerics()
    {
        Repository<Teacher> teacherRepository = new();

        Teacher teacher = new()
        {
            FullName = "Петренко Олена Іванівна",
            DateOfBirth = new DateTime(1985, 3, 15),
            PersonalEmail = "petrenko@university.edu.ua",
            Department = "Кафедра програмної інженерії",
            ExperienceYears = 12
        };

        teacherRepository.Add(teacher);

        Console.WriteLine("Універсальний репозиторій Repository<Teacher>:");
        Console.WriteLine($"Кількість викладачів: {teacherRepository.GetAll().Count}");
        Console.WriteLine();

        foreach (Teacher item in teacherRepository.GetAll())
        {
            item.ShowDetailedInfo();
            Console.WriteLine();
        }
    }

    private static Student ReadStudentFromConsole()
    {
        Console.Write("ПІБ: ");
        string fullName = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Дата народження (dd.MM.yyyy): ");
        string birthInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!DateTime.TryParseExact(birthInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
        {
            throw new FormatException("Некоректний формат дати народження.");
        }

        Console.Write("Номер залікової книжки (8 цифр): ");
        string recordBookNumber = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Електронна пошта: ");
        string email = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Дата зарахування (dd.MM.yyyy): ");
        string enrollmentInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!DateTime.TryParseExact(enrollmentInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime enrollmentDate))
        {
            throw new FormatException("Некоректний формат дати зарахування.");
        }

        Console.Write("Статус (Active/AcademicLeave/Expelled/Graduated): ");
        string statusInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!Enum.TryParse(statusInput, true, out Student.StudentStatus status))
        {
            status = Student.StudentStatus.Active;
        }

        Console.Write("Примітки: ");
        string notes = Console.ReadLine()?.Trim() ?? string.Empty;

        GradeJournal journal = ReadJournalFromConsole();

        Student student = new()
        {
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            RecordBookNumber = recordBookNumber,
            PersonalEmail = email,
            EnrollmentDate = enrollmentDate,
            Status = status,
            Journal = journal
        };

        student.SetNotes(notes);
        student.SyncAverageFromJournal();

        return student;
    }

    private static Teacher ReadTeacherFromConsole()
    {
        Console.Write("ПІБ куратора: ");
        string fullName = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Дата народження (dd.MM.yyyy): ");
        string birthInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!DateTime.TryParseExact(birthInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
        {
            throw new FormatException("Некоректний формат дати народження.");
        }

        Console.Write("Електронна пошта: ");
        string email = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Кафедра: ");
        string department = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Стаж роботи (років): ");
        string experienceInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!int.TryParse(experienceInput, out int experienceYears))
        {
            throw new FormatException("Некоректне значення стажу.");
        }

        Teacher teacher = new()
        {
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            PersonalEmail = email,
            Department = department,
            ExperienceYears = experienceYears
        };

        return teacher;
    }

    private static GradeJournal ReadJournalFromConsole()
    {
        GradeJournal journal = new();

        Console.Write("Кількість предметів у журналі: ");
        string? countInput = Console.ReadLine();

        if (!int.TryParse(countInput, out int count) || count < 0)
        {
            return journal;
        }

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Назва предмету {i + 1}: ");
            string subject = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write($"Оцінка за предмет {i + 1} (0-100): ");
            string gradeInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(subject) || !double.TryParse(gradeInput, out double grade))
            {
                continue;
            }

            if (grade < 0 || grade > 100)
            {
                continue;
            }

            journal[subject] = grade;
        }

        return journal;
    }
}
