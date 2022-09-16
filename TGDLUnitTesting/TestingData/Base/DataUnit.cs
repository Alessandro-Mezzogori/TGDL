
namespace TGDLUnitTesting.TestingData;

public enum TestType
{
    Equal,
    NotEqual,
    Fail,
}

public interface IDataUnit<out TInput, out TOutput>
{
    public TInput Input { get; }
    public TOutput? Output { get; }

    public TestType Test { get; set; } 
}

public class DataUnit<TInput, TOutput> : IDataUnit<TInput, TOutput>
{
    public TInput Input { get; set; }
    public TOutput? Output { get; set; }

    public TestType Test { get; set; } = TestType.Equal;
}
