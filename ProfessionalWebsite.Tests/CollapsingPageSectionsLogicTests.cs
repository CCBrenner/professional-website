using ProfessionalWebsite.Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalWebsite.Tests
{
    [TestClass]
    public class CollapsingPageSectionsLogicTests
    {
        private CollapsingPageSectionsLogic TestConfiguration()
        {
            return new CollapsingPageSectionsLogic(new List<CollapsingPageSection>()
            {
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
                new CollapsingPageSection(),
            });
        }
        // start CollapseAllShowOne() tests
        [TestMethod]
        public void TestCollapseAllShowOneCollapsesAllSectionsExecptSpecifiedSection()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
        }
        [TestMethod]
        public void TestCollapseAllShowOneSetsSingleOpenSectionToBeingPromoted()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
        }
        [TestMethod]
        public void TestCollapseAllShowOneSectionsStatusIsUpdated()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);
        }
        // end CollapseAllShowOne() tests
        // start ToggleCollapseSingle() tests
        [TestMethod]
        public void TestToggleCollapseSingleIfOpenThenCollapse()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
        }
        [TestMethod]
        public void TestToggleCollapseSingleIfCollapsedThenOpen()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
                        Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
        }
        [TestMethod]
        public void TestToggleCollapseSingleSingleOpenSectionMeansPromoteOpenSection()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);

            // Both open and then both close in same order, leaving as only section 9 open = promote
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleCollapseSingleZeroOrGreaterThanOneSectionsOpenMeansNoneArePromoted()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[11].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[11].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[11].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCurrentPromo);
        }
        [TestMethod]
        public void TestToggleCollapseSingleSectionsStatusIsUpdated()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(0);
            sectionsLogic.ToggleCollapseSingle(1);
            sectionsLogic.ToggleCollapseSingle(2);
            sectionsLogic.ToggleCollapseSingle(3);
            sectionsLogic.ToggleCollapseSingle(5);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(7);
            sectionsLogic.ToggleCollapseSingle(8);
            sectionsLogic.ToggleCollapseSingle(9);
            sectionsLogic.ToggleCollapseSingle(10);
            sectionsLogic.ToggleCollapseSingle(11);
            sectionsLogic.ToggleCollapseSingle(12);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(0);
            sectionsLogic.ToggleCollapseSingle(1);
            sectionsLogic.ToggleCollapseSingle(2);
            sectionsLogic.ToggleCollapseSingle(3);
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(7);
            sectionsLogic.ToggleCollapseSingle(8);
            sectionsLogic.ToggleCollapseSingle(9);
            sectionsLogic.ToggleCollapseSingle(10);
            sectionsLogic.ToggleCollapseSingle(11);
            sectionsLogic.ToggleCollapseSingle(12);
            Assert.AreEqual(SectionsStatus.AllAreOpen, sectionsLogic.SectionsStatus);
        }
        [TestMethod]
        public void TestToggleCollapseSingleStartingAllOpenAndEachClosedPromotesTheLastSectionThatIsOpen()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AllAreOpen, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            // 13 sections in TestConfiguration()
            sectionsLogic.ToggleCollapseSingle(0);
            sectionsLogic.ToggleCollapseSingle(1);
            sectionsLogic.ToggleCollapseSingle(2);
            sectionsLogic.ToggleCollapseSingle(3);
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(7);
            sectionsLogic.ToggleCollapseSingle(8);
            sectionsLogic.ToggleCollapseSingle(9);
            sectionsLogic.ToggleCollapseSingle(10);
            sectionsLogic.ToggleCollapseSingle(11);
            // index [12] as only section left open
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[12].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[3].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[3].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[8].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[8].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(12);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.Sections[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(7);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[7].IsCurrentPromo);
        }
        // end ToggleCollapseSingle() tests
        // start ToggleAllSections() tests
        [TestMethod]
        public void TestToggleAllSectionsIfAnySectionsOpenThenOpenAllSections()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            // Three sections open:
            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[8].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[8].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsIfAllSectionsOpenThenCollapseAllSections()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            // Three sections open:
            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(0);
            sectionsLogic.ToggleCollapseSingle(1);
            sectionsLogic.ToggleCollapseSingle(2);
            sectionsLogic.ToggleCollapseSingle(3);
            sectionsLogic.ToggleCollapseSingle(5);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(7);
            sectionsLogic.ToggleCollapseSingle(8);
            sectionsLogic.ToggleCollapseSingle(9);
            sectionsLogic.ToggleCollapseSingle(10);
            sectionsLogic.ToggleCollapseSingle(11);
            sectionsLogic.ToggleCollapseSingle(12);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[1].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[3].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[8].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[10].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[12].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Sections[0].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[1].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[3].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[8].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[10].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[12].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsIfAllSectionsCollapsedThenOpenAllSections()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[8].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[8].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsSectionsStatusIsUpdated()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleAllSections();
            Assert.AreEqual(SectionsStatus.AllAreOpen, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleAllSections();
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleAllSections();
            Assert.AreEqual(SectionsStatus.AllAreOpen, sectionsLogic.SectionsStatus);
        }
        // end ToggleAllSections() tests
        // start PromoteSection() tests
        [TestMethod]
        public void TestPromoteSectionShowsOnlyOneSpecifiedSectionAndBringsItToTheTopBySupressingOtherSectionsWhileShowingDecoySectionHeaders()
        {
            CollapsingPageSectionsLogic sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Sections.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCurrentPromo);

            sectionsLogic.PromoteSection(4);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[1].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[0].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[6].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[7].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[11].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Sections[2].IsCurrentPromo);
        }
        // end PromoteSection() tests
    }
}
