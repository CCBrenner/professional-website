namespace ProfessionalWebsite.Tests
{
    /*
    The tests below were used when "SectionedPage" was "CollapsingPageSectionsLogic"; much has changed around how 
    this is used, making these tests obsolete.
    These are kept as artifacts of what SectionedPage/CollapsingPageSectionsLogic tests looked like when it existed; 
    it has since been transformed.
    */

    /*
    [TestClass]
    public class CollapsingPageSectionsLogicTests
    {
        private SectionedPage TestConfiguration()
        {
            return new CollapsingPageSectionsLogic(new List<_section>()
            {
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
                new _section(),
            });
        }
        // start CollapseAllShowOne() tests
        [TestMethod]
        public void TestCollapseAllShowOneCollapsesAllSectionsExecptSpecifiedSection()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
        }
        [TestMethod]
        public void TestCollapseAllShowOneSetsSingleOpenSectionToBeingPromoted()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
        }
        [TestMethod]
        public void TestCollapseAllShowOneSectionsStatusIsUpdated()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);
        }
        // end CollapseAllShowOne() tests
        // start ToggleCollapseSingle() tests
        [TestMethod]
        public void TestToggleCollapseSingleIfOpenThenCollapse()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
        }
        [TestMethod]
        public void TestToggleCollapseSingleIfCollapsedThenOpen()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
                        Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
        }
        [TestMethod]
        public void TestToggleCollapseSingleSingleOpenSectionMeansPromoteOpenSection()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);

            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);

            // Both open and then both close in same order, leaving as only section 9 open = promote
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(5);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleCollapseSingleZeroOrGreaterThanOneSectionsOpenMeansNoneArePromoted()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[11].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(4);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[11].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[11].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCurrentPromo);
        }
        [TestMethod]
        public void TestToggleCollapseSingleSectionsStatusIsUpdated()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

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
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AllAreOpen, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

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
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[12].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[3].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[3].IsCurrentPromo);
            Assert.AreEqual(true, sectionsLogic.Section[8].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[8].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(12);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.Section[12].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCurrentPromo);

            sectionsLogic.ToggleCollapseSingle(7);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(SectionsStatus.AtLeastOneIsOpen, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[7].IsCurrentPromo);
        }
        // end ToggleCollapseSingle() tests
        // start ToggleAllSections() tests
        [TestMethod]
        public void TestToggleAllSectionsIfAnySectionsOpenThenOpenAllSections()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            // Three sections open:
            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(6);
            sectionsLogic.ToggleCollapseSingle(9);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[8].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[8].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsIfAllSectionsOpenThenCollapseAllSections()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

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
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[1].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[3].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[8].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[10].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[12].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(true, sectionsLogic.Section[0].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[1].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[3].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[7].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[8].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[10].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[12].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsIfAllSectionsCollapsedThenOpenAllSections()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.CollapseAllShowOne(4);
            sectionsLogic.ToggleCollapseSingle(4);
            Assert.AreEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[8].IsCollapsed);

            sectionsLogic.ToggleAllSections();
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[6].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[2].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[8].IsCollapsed);
        }
        [TestMethod]
        public void TestToggleAllSectionsSectionsStatusIsUpdated()
        {
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

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
            SectionedPage sectionsLogic = TestConfiguration();
            Assert.AreEqual(13, sectionsLogic.Section.Count());
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(false, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCurrentPromo);

            sectionsLogic.PromoteSection(4);
            Assert.AreNotEqual(SectionsStatus.AllAreCollapsed, sectionsLogic.SectionsStatus);
            Assert.AreEqual(true, sectionsLogic.ASectionIsCurrentlyPromo);
            Assert.AreEqual(false, sectionsLogic.Section[4].IsCollapsed);
            Assert.AreEqual(true, sectionsLogic.Section[4].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[5].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[1].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[0].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[6].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[7].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[11].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[9].IsCurrentPromo);
            Assert.AreEqual(false, sectionsLogic.Section[2].IsCurrentPromo);
        }
        // end PromoteSection() tests
    }
    */
}
