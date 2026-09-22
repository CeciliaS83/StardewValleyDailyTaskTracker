var registry = new Registry();

registry.Add(new StardewVally("chop down trees"));
registry.Add(new StardewVally("go mining"));
registry.Add(new StardewVally("grow the farm"));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

foreach (StardewVally item in registry.All())
{
    Console.WriteLine(item.Name);
}

Console.WriteLine();

// One I know is on the registry.
StardewVally? known = registry.Find("go mining");
Console.WriteLine(known == null ? "Nothing on file by that name." : "Found it.");

// And one nobody has ever heard of.

StardewVally? missing = registry.Find("something I never added");
Console.WriteLine(missing == null ? "Nothing on file by that name." : "...found something that shouldn't be there.");

Console.WriteLine();
Console.WriteLine(registry.Remove("go mining")
    ? "Removed."
    : "Nothing by that name.");
Console.WriteLine($"{registry.Count} on file.");