namespace TGDLLib.Visitors;

public class ParseException : Exception
{
    private const string _defaultMessage = "Parsing error";
    public ParseException(Type type) : base(_defaultMessage + " " + type.FullName)
    {

    }

    public ParseException(Type type, string? message) : base(_defaultMessage + " " + type.FullName + ": " + message)
    {
    
    }

    public ParseException(string message): base(_defaultMessage + " " + message)
    {

    }
}
