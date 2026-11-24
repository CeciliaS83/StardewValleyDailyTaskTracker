namespace Project.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Project.Migrations;

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
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var registry = new Registry();
        var fish = registry.NewItem("Fish");
        registry.Add(fish);
        fish.Completed();

        registry.Save(new ProjectContext(options));

        var reopened = new Registry();
        reopened.Load(new ProjectContext(options)); 

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

    [Fact]
    public void Week10_ASecondSaveChangesTheRow()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        registry.Add(registry.NewItem("Farm"));
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        StardewValley item = nextRun.Find("Farm")!;
        item.Completed();
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Equal(1, reopened.Find("Farm")!.TimesCompleted);
    }

    [Fact]
    public void Week11_ARemovedRecordStaysGone()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        StardewValley kept = registry.NewItem("a name of yours");
        StardewValley removed = registry.NewItem("another name of yours");
        registry.Add(kept);
        registry.Add(removed);
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        nextRun.Remove("another name of yours");
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Null(reopened.Find("another name of yours"));
    }

    [Fact]
    public void Week12_DetailsComeBack()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        StardewValley record = registry.NewItem("Farm");
        record.Tasks.Add(registry.NewDetail("Mine"));
        record.Tasks.Add(registry.NewDetail("Combat"));
        registry.Add(record);
        registry.Save(new ProjectContext(options));

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(2, reopened.Find("Farm")!.Tasks.Count);
    }

    [Fact]
    public void Week13_ABadFileAddsNothing()
    {
        string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        File.WriteAllText(path, """[ "Fish", "" ]""");

        Registry registry = new Registry();
        Assert.Throws<InvalidDataException>(() => registry.Import(path));
        Assert.Equal(0, registry.Count);
    }

    [Fact]
    public void Week14_TheOldRecordsGetThreeStars()
    {
        Migration migration = new EveryRecordGetsStars();

        AddColumnOperation stars = migration.UpOperations
            .OfType<AddColumnOperation>()
            .Single(op => op.Name == "Stars");

        Assert.Equal(3, stars.DefaultValue);
    }
}