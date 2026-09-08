var registry = new Registry();

registry.Add(new StardewVally("the first one"));
registry.Add(new StardewVally("the second one"));
registry.Add(new StardewVally("the third one"));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

foreach (StardewVally item in registry.All())
{
    Console.WriteLine(item.Name);
}