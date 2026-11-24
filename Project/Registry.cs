using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class Registry : IListed
{    
    private readonly List<StardewValley> _tasks = new List<StardewValley>();

    public static string Topic => "A list of the main action you complete each day in Stardew Valley";

    public StardewValley NewItem(string name) => new StardewValley(name);
    
    public Tasks NewDetail(string name) => new Tasks(name);

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
        return _tasks.FirstOrDefault(item => item.Name == name);
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

    public void Save(ProjectContext db)
    {
        foreach (StardewValley tasks in _tasks)
        {
            if (tasks.Id == 0)
            {
                db.Task.Add(tasks);
            }
            else
            {
                db.Task.Update(tasks);
            }
        }
        
        foreach (StardewValley row in db.Task.ToList())
        {
            if (!_tasks.Any(item => item.Id == row.Id))
            {
                db.Task.Remove(row);
            }
        }

        db.SaveChanges();
    }

    public void Load(ProjectContext db)
    {
        List<StardewValley> loaded = db.Task.Include(item => item.Tasks).OrderBy(task => task.Id).ToList();

        if (loaded.Count == 0)
        {
            return;
        }

        _tasks.Clear();

        _tasks.AddRange(loaded);
    }
    
    public int Import(string path)
    {
        string json = File.ReadAllText(path);
        List<string> names = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

        int before = Count;

        foreach (string name in names)
        {
            Add(NewItem(name));
        }

        return Count - before;
    }

    public List<string> WithDetails(ProjectContext db)
    {
        return db.Task.Where(item => item.Tasks.Any()).OrderBy(item => item.Name).Select(item => item.Name).ToList();
    }

    public List<StardewValley> Matching(string term)
    {
        return _tasks.Where(item => item.Name.Contains(term)).ToList();
    }

    public List<string> Names()
    {
        return _tasks.Select(item => item.Name).ToList();
    }

    public List<StardewValley> Sorted()
    {
        return _tasks.OrderBy(item => item.Name).ToList();
    }
}