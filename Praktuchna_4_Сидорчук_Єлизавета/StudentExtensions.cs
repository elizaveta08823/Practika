namespace Praktuchna_4;

public static class StudentExtensions
{
    public static bool IsEligibleForScholarship(this Student student)
    {
        return student.AverageGrade >= 75 && student.Status == Student.StudentStatus.Active;
    }
}
