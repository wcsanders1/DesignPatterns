using Composite;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using CommonClientLib;

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

            var sut = new Decedent("testName", TEST_ESTATE_VALUE)
            {
                Descendants = testDescendants
            };

            sut.DistributeEstate();

            var testDescendantOneInheritance = testDescendants
                .Where(d => d.Name == TEST_DESCENDANT_ONE_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            Assert.Equal(testDescendantOneInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE);
        }

        [Fact]
        public void DistributeEstate_DistributesPerStirpes_WhenDescendantHasDescendants()
        {
            const decimal TEST_ESTATE_VALUE = 90000;
            const string TEST_DESCENDANT_ONE_NAME = "Bob";
            const string TEST_DESCENDANT_TWO_NAME = "Ghent";
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
                    Deceased = true,
                    Descendants = new List<Descendant>
                    {
                        new Descendant
                        {
                            Name = TEST_DESCENDANT_TWO_NAME,
                            Deceased = false
                        },
                        new Descendant
                        {
                            Name = "Bevel",
                            Deceased = false
                        }
                    }
                }
            };

            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE = TEST_ESTATE_VALUE / 3;
            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_TWO =
                EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE / 2;

            var sut = new Decedent("testName", TEST_ESTATE_VALUE)
            {
                Descendants = testDescendants
            };

            sut.DistributeEstate();

            var allDescendants = testDescendants.GetAllNestedTypes();

            var testDescendantOneInheritance = allDescendants
                .Where(d => d.Name == TEST_DESCENDANT_ONE_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            var testDescendantTwoInheritance = allDescendants
                .Where(d => d.Name == TEST_DESCENDANT_TWO_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            Assert.Equal(testDescendantOneInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE);
            Assert.Equal(testDescendantTwoInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_TWO);
        }

        [Fact]
        public void DistributeEstate_DistributesPerStirpes_WhenDescendantOfDEscendantHasDescendants()
        {
            const decimal TEST_ESTATE_VALUE = 90000;
            const string TEST_DESCENDANT_ONE_NAME = "Bob";
            const string TEST_DESCENDANT_TWO_NAME = "Ghent";
            const string TEST_DESCENDANT_THREE_NAME = "Bowlder";
            const string TEST_DESCENDANT_FOUR_NAME = "David";
            const string TEST_DESCENDANT_FIVE_NAME = "Flosser";
            const string TEST_DESCENDANT_SIX_NAME = "Dimmerbergh";
            var testDescendants = new List<Descendant>
            {
                new Descendant
                {
                    Name = TEST_DESCENDANT_FIVE_NAME,
                    Deceased = true,
                    Descendants = new List<Descendant>
                    {
                        new Descendant
                        {
                            Name = "Trovers",
                            Deceased = false
                        },
                        new Descendant
                        {
                            Name = "Jaspers",
                            Deceased = false
                        },
                        new Descendant
                        {
                            Name = TEST_DESCENDANT_SIX_NAME,
                            Deceased = true,
                            Descendants = new List<Descendant>
                            {
                                new Descendant
                                {
                                    Name = "Vlasssel",
                                    Deceased = false,
                                },
                                new Descendant
                                {
                                    Name = TEST_DESCENDANT_THREE_NAME,
                                    Deceased = false
                                }
                            }
                        }
                    }
                },
                new Descendant
                {
                    Name = TEST_DESCENDANT_ONE_NAME,
                    Deceased = false
                },
                new Descendant
                {
                    Name = TEST_DESCENDANT_FOUR_NAME,
                    Deceased = true,
                    Descendants = new List<Descendant>
                    {
                        new Descendant
                        {
                            Name = TEST_DESCENDANT_TWO_NAME,
                            Deceased = false
                        },
                        new Descendant
                        {
                            Name = "Bevel",
                            Deceased = false
                        }
                    }
                }
            };

            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE = TEST_ESTATE_VALUE / 3;
            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_TWO =
                EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE / 2;
            const decimal EXPECTED_INHERITANCE_TEST_DESCENDANT_THREE =
                ((TEST_ESTATE_VALUE / 3) / 3) / 2;

            var sut = new Decedent("testName", TEST_ESTATE_VALUE)
            {
                Descendants = testDescendants
            };

            sut.DistributeEstate();

            var allDescendants = testDescendants.GetAllNestedTypes();

            var testDescendantOneInheritance = allDescendants
                .Where(d => d.Name == TEST_DESCENDANT_ONE_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            var testDescendantTwoInheritance = allDescendants
                .Where(d => d.Name == TEST_DESCENDANT_TWO_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            var testDescendantThreeInheritance = allDescendants
                .Where(d => d.Name == TEST_DESCENDANT_THREE_NAME)
                .Select(d => d.Inheritance)
                .FirstOrDefault();

            Assert.Equal(testDescendantOneInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_ONE);
            Assert.Equal(testDescendantTwoInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_TWO);
            Assert.Equal(testDescendantThreeInheritance, EXPECTED_INHERITANCE_TEST_DESCENDANT_THREE);
        }
    }
}
