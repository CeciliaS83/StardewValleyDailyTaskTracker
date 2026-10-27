namespace Project.Tests;

public class RegistryTests
{
    [Fact]
    public void Check2_AddingGrowsTheCount()
    {
        var registry = new Registry();
        registry.Add(registry.NewItem("Fish"));
        registry.Add(registry.NewItem("Fight"));
        Assert.Equal(2, registry.Count);
    }

    [Fact]
    public void Check3_FindHandsBackTheRecordItHolds()
    {
        var registry = new Registry();
        registry.Add(registry.NewItem("Fish"));
        registry.Add(registry.NewItem("Fight"));
        Assert.Equal(2, registry.Count);
        var depot = registry.NewItem("Seaweed");
        registry.Add(depot);
        var found = registry.Find("Seaweed");
        Assert.Same(depot, found);
    }

    [Fact]
    public void Check4_RemovingAStrangerSaysNo()
    {
        var registry = new Registry();
        registry.Add(registry.NewItem("Fish"));
        registry.Add(registry.NewItem("Fight"));
        Assert.Equal(2, registry.Count);
        var depot = registry.NewItem("Seaweed");
        Assert.False(registry.Remove("Seaweed"));
        Assert.Equal(2, registry.Count);
    }

    [Fact]
    public void Check5_TheSameNameCannotRegisterTwice()
    {
        var registry = new Registry();
        registry.Add(registry.NewItem("Fish"));
        registry.Add(registry.NewItem("Fish"));
        Assert.Equal(1, registry.Count);
    }

    [Fact]
    public void Week8_TheRegistrySurvivesARestart()
    {
        string path = Path.Combine(Path.GetTempPath(), "randompath.json");
        File.Delete(path);

        var registry = new Registry();
        var fish = registry.NewItem("Fish");
        registry.Add(fish);
        fish.Completed();

        registry.Save(path);

        var reopened = new Registry();
        reopened.Load(path); 

        Assert.Equal(1, reopened.Count);
        
        int actual = reopened.All()[0].TimesCompleted;
        int expected = 1;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Week9_FindComesBackEmptyHanded()
    {
        var registry = new Registry();
        StardewValley task = registry.NewItem("Fish");
        registry.Add(task);

        Assert.Same(task, registry.Find("Fish"));
        Assert.Null(registry.Find("FlyinRandomCave"));
    }
}