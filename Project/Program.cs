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