var registry = new Registry();

registry.Add(registry.NewItem("Farm"));
registry.Add(registry.NewItem("Forage"));
registry.Add(registry.NewItem("Mine"));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

// One I know something about.
StardewValley? known = registry.Find("Forage");
if (known == null)
{
    Console.WriteLine("Nothing on file by that name.");
}
else
{
    known.Completed();
    Console.WriteLine($"{known.Name} - completed {known.TimesCompleted} times");
}

// And one nobody has ever heard of.
StardewValley? missing = registry.Find("Monster Caving");
Console.WriteLine(missing == null
    ? "Nothing on file by that name."
    : "...found something that shouldn't be there.");

Console.WriteLine();
Console.Write("Take one off the books (Enter to skip): ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine(registry.Remove(name) ? "Removed." : "Nothing by that name.");
}

Console.WriteLine();
foreach (StardewValley item in registry.All())
{
    Console.WriteLine($"{item.Name}");
}
Console.WriteLine($"{registry.Count} on file.");