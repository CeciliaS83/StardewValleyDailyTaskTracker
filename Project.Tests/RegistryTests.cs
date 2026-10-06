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
}