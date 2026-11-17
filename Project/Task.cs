public class Task
{
    public int Id { get; set; }

    public int StardewValleyId { get; set; }

    public string Name { get; private set; }

    public Task(string name)
    {
        Name = name;
    }
}