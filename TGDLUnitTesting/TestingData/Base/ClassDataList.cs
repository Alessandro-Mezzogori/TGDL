using System.Collections;

namespace TGDLUnitTesting.TestingData
{
    internal abstract class ClassDataList<TInput, TOutput> : IEnumerable<object[]>
        where TInput : notnull
        where TOutput : notnull
    {
        public abstract List<DataUnit<TInput, TOutput>> DataList { get; }
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var unit in DataList)
                yield return new object[] { unit };  
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
