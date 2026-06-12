namespace Praktuchna_7;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    public void Remove(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Remove(item);
    }

    public List<T> GetAll()
    {
        return _items;
    }
}
