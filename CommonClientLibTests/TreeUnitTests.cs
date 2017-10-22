using Xunit;
using CommonClientLib;

namespace CommonClientLibTests
{
    [Trait("Category", "Unit")]
    public class TreeUnitTests
    {
        [Fact]
        public void AddNode_ReturnsFalse_WhenKeyExists1()
        {
            const int TEST_KEY = 1;
            var sut = new Tree<int>(TEST_KEY);

            var added = sut.TryAddNode(TEST_KEY);

            Assert.False(added);
        }

        [Fact]
        public void AddNode_ReturnsFalse_WhenKeyExists2()
        {
            const int TEST_KEY = 5;
            var sut = new Tree<int>(1);
            sut.TryAddNode(2);
            sut.TryAddNode(3);

            var node3 = sut.GetNode(3);
            node3.TryAddNode(15);
            node3.TryAddNode(16);

            var node16 = sut.GetNode(16);
            node16.TryAddNode(16);
            node16.TryAddNode(TEST_KEY);

            var added = sut.TryAddNode(TEST_KEY);
            
            Assert.False(added);
        }

        [Fact]
        public void GetNode_ReturnsNull_WhenKeyDoesNotExist()
        {
            const int TEST_KEY = 2;
            var sut = new Tree<int>(1);
           
            var result = sut.GetNode(TEST_KEY);

            Assert.Null(result);
        }

        [Fact]
        public void GetNode_ReturnsNodeByIntKey_WhenNodeExists1()
        {
            const int TEST_KEY = 2;
            var sut = new Tree<int>(1);
            var added = sut.TryAddNode(TEST_KEY);

            Assert.True(added);

            var result = sut.GetNode(TEST_KEY);

            Assert.NotNull(result);
            Assert.Equal(TEST_KEY, result.Key);
        }

        [Fact]
        public void GetNode_ReturnsNodeByIntKey_WhenNodeExists2()
        {
            const int TEST_KEY = 1;
            var sut = new Tree<int>(TEST_KEY);

            var result = sut.GetNode(TEST_KEY);

            Assert.NotNull(result);
            Assert.Equal(TEST_KEY, result.Key);
        }


        [Fact]
        public void GetNode_ReturnsNodeByIntKey_WhenNodeExists3()
        {
            var sut = new Tree<int>(1);
            sut.TryAddNode(2);
            sut.TryAddNode(3);

            var node3 = sut.GetNode(3);
            node3.TryAddNode(15);
            node3.TryAddNode(16);

            var node16 = sut.GetNode(16);
            node16.TryAddNode(20);

            var node20 = node16.GetNode(20);

            Assert.Equal(3, node3.Key);
            Assert.Equal(16, node16.Key);
            Assert.Equal(20, node20.Key);
        }

        [Fact]
        public void GetNode_ReturnsNodeByIntKey_WhenNodeExists4()
        {
            var sut = new Tree<int>(1);
            sut.TryAddNode(2);
            sut.TryAddNode(3);

            var node3 = sut.GetNode(3);
            node3.TryAddNode(15);
            node3.TryAddNode(16);

            var node16 = sut.GetNode(16);
            node16.TryAddNode(20);

            var node20 = node16.GetNode(20);
            node20.TryAddNode(21);
            node20.TryAddNode(22);
            node20.TryAddNode(23);

            node3.TryAddNode(50);

            var node23 = node16.GetNode(23);
            node23.TryAddNode(100);

            var node50 = node20.GetNode(50);
            var node100 = sut.GetNode(100);

            Assert.Equal(1, sut.Key);
            Assert.Equal(3, node3.Key);
            Assert.Equal(16, node16.Key);
            Assert.Equal(20, node20.Key);
            Assert.Equal(23, node23.Key);
            Assert.Equal(50, node50.Key);
            Assert.Equal(100, node100.Key);
        }

        [Fact]
        public void GetNode_ReturnsNodeByStringKey_WhenNodeExists1()
        {
            const string TEST_KEY = "neatKey";
            var sut = new Tree<string>("firstKey");
            var added = sut.TryAddNode(TEST_KEY);

            Assert.True(added);

            var result = sut.GetNode(TEST_KEY);

            Assert.NotNull(result);
            Assert.Equal(TEST_KEY, result.Key);
        }
    }
}
