namespace Praktuchna_7;

public class GradeJournal : Dictionary<string, double>
{
    public double RecalculateAverageGrade()
    {
        if (Count == 0)
        {
            return 0;
        }

        return Values.Average();
    }
}
