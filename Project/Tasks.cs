public class Tasks
{
    public int Id { get; set; }

    public int StardewValleyId { get; set; }

    public string Name { get; private set; }

    public Tasks(string name)
    {
        Name = name;
    }
}