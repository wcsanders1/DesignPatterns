using Composite;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace CompositeTests
{
    [Trait("Category", "Unit")]
    public class DecedentUnitTests
    {
        [Fact]
        public void DistributeEstate_DistributesPerStirpes_WhenGivenDescendantsAndEstateValue()
        {
            const decimal TEST_ESTATE_VALUE = 90000;
            const string TEST_DESCENDANT_ONE_NAME = "Bob";
            var testDescendants = new List<Descendant>
            {
                new Descendant
                {
                    Name = TEST_DESCENDANT_ONE_NAME,
                    Deceased = false
                },
                new Descendant
                {
                    Name = "Charles",
                    Deceased = false
                },
                new Descendant
                {
                    Name = "David",
                    Deceased = false
                }
            };

            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE = TEST_ESTATE_VALUE / 3;

            var sut = new Decedent(TEST_ESTATE_VALUE, testDescendants);

            sut.DistributeEstate();

            var testDescendantOneInheritance = testDescendants
                .Where(d => d.Name == TEST_DESCENDANT_ONE_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            Assert.Equal(testDescendantOneInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE);
        }
    }
}
