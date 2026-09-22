public class StardewValley
{
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

    public int TimesCompleted { get; private set; }
    public void Completed()
    {
        TimesCompleted++;
    }

    public StardewValley(string name)
    {
        Name = name;
        Note = "";
    }
}