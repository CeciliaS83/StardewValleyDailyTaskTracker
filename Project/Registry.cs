public class Registry
{
    private readonly List<StardewVally> _tasks = new List<StardewVally>();

    public static string Topic => "A list of the main action you complete each day in Stardew Valley";

    public StardewVally NewItem(string name) => new StardewVally(name);

    public void Add(StardewVally task)
    {
        _tasks.Add(task);
    }

    public int Count => _tasks.Count;

    public List<StardewVally> All()
    {
        return new List<StardewVally>(_tasks);
    }
}