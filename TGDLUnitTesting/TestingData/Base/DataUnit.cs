namespace TGDLUnitTesting.TestingData
{
    public enum TestType
    {
        Equal,
        NotEqual,
        Fail,
    }

    public class DataUnit<TInput, TOutput> 
        where TOutput : notnull
        where TInput : notnull
    {
        public TInput Input { get; set; }
        public TOutput Output { get; set; }

        public TestType Test { get; set; } = TestType.Equal;
    }
}
