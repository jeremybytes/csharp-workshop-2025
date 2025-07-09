namespace explicit_implementation;

public interface ISaveable
{
    public string Save();
}

public class Catalog : ISaveable
{
    public string Save()
    {
        return "Catalog Save";
    }

}