using System.Text.Json.Serialization;

public class StardewValley : IListed
{
    public int Id { get; set; }
    
    private string _name = "unknown";
    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _name = value.Trim();
            }
        }
    }
    private string _note = "unknown";
    public string Note
    {
        get { return _note; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _note = value.Trim();
            }
        }
    }
    private int _number = 0;
    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }

    [JsonInclude]
    public int TimesCompleted { get; private set; }
    public void Completed()
    {
        TimesCompleted++;
    }

    public StardewValley(string name)
    {
        Name = name;
        Note = "";
        Added = DateTime.Now;
    }

    public string Kind => "Tasks";

    public string Line() => $"{Name} - Completed {TimesCompleted} times  - added {AddedOn()}, {Stars} stars";

    public List<Tasks> Tasks { get; private set; } = new List<Tasks>();

    public DateTime? Added { get; private set; }

    public string AddedOn()
    {
        if (Added == null)
        {
            return "-";
        }

        return Added.Value.ToString("yyyy-MM-dd");
    }

        public int Stars { get; set; } = 3;
}