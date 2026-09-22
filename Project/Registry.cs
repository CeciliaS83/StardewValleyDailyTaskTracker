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

    public StardewVally? Find(string name)
    {
        foreach (StardewVally task in _tasks)
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
        StardewVally? found = Find(name);

        if (found == null)
        {
            return false;
        }

        _tasks.Remove(found);
        return true;
    }
}