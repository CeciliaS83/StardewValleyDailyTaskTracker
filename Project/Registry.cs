using System.Text.Json;

public class Registry : IListed
{
    private readonly List<StardewValley> _tasks = new List<StardewValley>();

    public static string Topic => "A list of the main action you complete each day in Stardew Valley";

    public StardewValley NewItem(string name) => new StardewValley(name);

    public void Add(StardewValley task)
    {
        if (Find(task.Name) != null)
        {
            return;
        }

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

    public string Kind => "Logs";

    public string Line() => $"{Topic} - {Count} tasks able to complete";

    public List<IListed> Everything()
    {
        List<IListed> listing = new List<IListed>();

        listing.Add(this);

        foreach (StardewValley item in _tasks)
        {
            listing.Add(item);
        }

        return listing;
    }

    public void Save(string path)
    {
        string json = JsonSerializer.Serialize(_tasks,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(path, json);
    }

    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        List<StardewValley>? loaded = JsonSerializer.Deserialize<List<StardewValley>>(File.ReadAllText(path));

        if (loaded == null)
        {
            return;
        }

        _tasks.Clear();

        foreach (StardewValley task in loaded)
        {
            _tasks.Add(task);
        }
    }
}