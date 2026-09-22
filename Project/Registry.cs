public class Registry
{
    private readonly List<StardewValley> _tasks = new List<StardewValley>();

    public static string Topic => "A list of the main action you complete each day in Stardew Valley";

    public StardewValley NewItem(string name) => new StardewValley(name);

    public void Add(StardewValley task)
    {
        _tasks.Add(task);
    }

    public int Count => _tasks.Count;

    public List<StardewValley> All()
    {
        return new List<StardewValley>(_tasks);
    }

    public StardewValley? Find(string name)
    {
        foreach (StardewValley task in _tasks)
        {
            if (task.Name == name)
            {
                return task;
            }
        }
    
    return null;
    }

    // inside the same Registry class, under Find
    public bool Remove(string name)
    {
        StardewValley? found = Find(name);

        if (found == null)
        {
            return false;
        }

        _tasks.Remove(found);
        return true;
    }
}