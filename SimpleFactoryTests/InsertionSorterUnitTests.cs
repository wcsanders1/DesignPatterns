using SimpleFactory;
using System;
using Xunit;

namespace SimpleFactoryTests
{
    [Trait("Category", "Unit")]
    public class InsertionSorterUnitTests
    {
        [Fact]
        public void Sort_SortsCollection_WhenProvidedCollection()
        {
            const int arraySize = 1000;

            var randomNumGenerator = new Random();
            var arrayToSort = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                arrayToSort[i] = randomNumGenerator.Next(1, arraySize);
            }

            var sut = new InsertionSorter<int>();
            sut.Sort(arrayToSort);

            for (int i = 0; i < arraySize - 1; i++)
            {
                Assert.True(arrayToSort[i] <= arrayToSort[i + 1]);
            }
        }
    }
}
