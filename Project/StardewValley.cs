public class StardewVally
{
    // TODO — Task 2. These three are holes: anything, anywhere, can write
    // anything into them. Close every one into a property.
    // Task 3 gives one of them a rule; Task 4 seals one shut.
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

    public bool IsAwake { get; private set; }
    public void Awake()
    {
        IsAwake = true;
    }

    public StardewVally(string name)
    {
        Name = name;
        Note = "";
    }
}