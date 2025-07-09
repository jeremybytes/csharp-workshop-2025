namespace explicit_implementation;

class Program
{
    static void Main(string[] args)
    {
        Catalog catalog = new Catalog();
        string result = catalog.Save();
        Console.WriteLine(result);

        ISaveable saveable = new Catalog();
        result = saveable.Save();
        Console.WriteLine(result);

        saveable = (ISaveable)catalog;
        result = saveable.Save();
        Console.WriteLine(result);
    }
}
