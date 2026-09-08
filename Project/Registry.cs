public class Registry
{
    private readonly List<StardewVally> _items = new List<StardewVally>();

    // TODO — Task 1. Say what your project is about, in words.
    public static string Topic => "A list of the main action you complete each day in Stardew Valley";     // ← yours

    public StardewVally NewItem(string name) => new StardewVally(name);

    public void Add(StardewVally item)
    {
        _items.Add(item);
    }

    public int Count => _items.Count;

    public List<StardewVally> All()
    {
        // TODO — Task 5. Hand back a COPY, never the list itself.
        return _items;                                   // ← yours
    }
}