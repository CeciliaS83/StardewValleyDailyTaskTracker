var registry = new Registry();

string registryFile = "registry.json";

registry.Add(registry.NewItem("Farm"));
registry.Add(registry.NewItem("Forage"));
registry.Add(registry.NewItem("Mine"));

// Week 7's rule, visible: Add called twice with the same name, and the
// second one refused. The count is the only thing that tells you.
registry.Add(registry.NewItem("Forage"));
Console.WriteLine($"Tried to register \"Forage\" twice - {registry.Count} on file.");

// One I know something about. Find hands back the record the registry is
// holding, so the change lands on the real one.
StardewValley? known = registry.Find("Forage");
if (known != null)
{
    known.Completed();
}

Console.Write("Take one off the books (Enter to skip): ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine(registry.Remove(name) ? "Removed." : "Nothing by that name.");
}

Console.WriteLine();

// One loop. It knows about exactly one thing, and that thing is not a class.
foreach (IListed StardewValley in registry.Everything())
{
    Console.WriteLine($"{StardewValley.Kind,-12}{StardewValley.Line()}");
}

registry.Save(registryFile);
Console.WriteLine($"{registry.Count} on file, saved to {registryFile}.");