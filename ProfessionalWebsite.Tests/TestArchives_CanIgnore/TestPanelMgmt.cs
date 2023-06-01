namespace ProfessionalWebsite.Tests
{
    /*
    This code is kept as a reference. It can be ingored for development purposes.
    This code tested the PanelMgmt singleton. PanelMgmt is no longer a singleton but instead is a regular class with a single instance reference
    in UIService. The same has been done with a number of other singletons, such as the other -Mgmt classes which used to be singletons. This code
    is kept as a reference to what those tests looked like. In an effort to use TDD, singeltons were found to be too cumbersome to test due to permission
    of outside code to change internal reference variables. A better solution existed, which is why UIService exists with references to all previous
    singletons referenced within it.
     */

    /*
    [TestClass]
    public class TestPanelMgmt
    {
        PanelMgmt panelMgmt;
        MockPanelMgmt mockPanelMgmt;

        public void Initialize()
        {
            if (mockPanelMgmt != null)
                Reset();

            panelMgmt = PanelMgmt.Instance;
            mockPanelMgmt = new MockPanelMgmt();

            Assert.AreEqual(7, panelMgmt.Panels.Count());

            Assert.AreEqual(mockPanelMgmt.Panels[1].PanelGroupId, panelMgmt.Panels[1].PanelGroupId);
            Assert.AreEqual(mockPanelMgmt.Panels[1].PanelStatus, panelMgmt.Panels[1].PanelStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[1].PanelButtonStatus, panelMgmt.Panels[1].PanelButtonStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[1].BlurStatus, panelMgmt.Panels[1].BlurStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[1].BehindPanelStatus, panelMgmt.Panels[1].BehindPanelStatus);

            Assert.AreEqual(mockPanelMgmt.Panels[0].PanelGroupId, panelMgmt.Panels[0].PanelGroupId);
            Assert.AreEqual(mockPanelMgmt.Panels[0].PanelStatus, panelMgmt.Panels[0].PanelStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[0].PanelButtonStatus, panelMgmt.Panels[0].PanelButtonStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[0].BlurStatus, panelMgmt.Panels[0].BlurStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[0].BehindPanelStatus, panelMgmt.Panels[0].BehindPanelStatus);

            Assert.AreEqual(mockPanelMgmt.Panels[4].PanelGroupId, panelMgmt.Panels[4].PanelGroupId);
            Assert.AreEqual(mockPanelMgmt.Panels[4].PanelStatus, panelMgmt.Panels[4].PanelStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[4].PanelButtonStatus, panelMgmt.Panels[4].PanelButtonStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[4].BlurStatus, panelMgmt.Panels[4].BlurStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[4].BehindPanelStatus, panelMgmt.Panels[4].BehindPanelStatus);

            Assert.AreEqual(mockPanelMgmt.Panels[5].PanelGroupId, panelMgmt.Panels[5].PanelGroupId);
            Assert.AreEqual(mockPanelMgmt.Panels[5].PanelStatus, panelMgmt.Panels[5].PanelStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[5].PanelButtonStatus, panelMgmt.Panels[5].PanelButtonStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[5].BlurStatus, panelMgmt.Panels[5].BlurStatus);
            Assert.AreEqual(mockPanelMgmt.Panels[5].BehindPanelStatus, panelMgmt.Panels[5].BehindPanelStatus);

            Assert.AreEqual(mockPanelMgmt.PanelGroups.Count(), panelMgmt.PanelGroups.Count());

            Assert.AreEqual(mockPanelMgmt.PanelGroups[0].Id, panelMgmt.PanelGroups[0].Id);
            Assert.AreEqual(mockPanelMgmt.PanelGroups[0].StartingLocation, panelMgmt.PanelGroups[0].StartingLocation);
            Assert.AreEqual(mockPanelMgmt.PanelGroups[0].LocationPanel, panelMgmt.PanelGroups[0].LocationPanel);
        }
        public void Reset()
        {
            mockPanelMgmt = new MockPanelMgmt();


        }

        // copy/paste data into tests to update
        // initialize method at the beginning of every test to check data consistency
        // reset method at the end of each test to reset to default values

        [TestMethod]
        public void TestDeactivateAllPanels_AllPanelsSetToDeactivatedState()
        {
            Initialize();

            // when clicked, all panels are set to their deactivated state
            panelMgmt.ActivatePanel(2);
            panelMgmt.DeactivateAllPanels();

            Assert.AreEqual("", panelMgmt.Panels[0].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[1].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[4].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BlurStatus);
        }
        [TestMethod]
        public void TestDeactivateAllPanels_AllGroupsRevertToSavedLocationForButton()
        {
            Initialize();

            // when clicked, all panels are set to their deactivated state
            panelMgmt.ActivatePanel(2);
            panelMgmt.DeactivateAllPanels();

            // duplicate tests for checking assumptions
            Assert.AreEqual("", panelMgmt.Panels[0].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[1].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[4].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BlurStatus);

            // this test's tests
            Assert.AreEqual(panelMgmt.Panels[4], panelMgmt.PanelGroups[0].LocationPanel);
            Assert.AreEqual("highlight-button", panelMgmt.PanelGroups[0].LocationPanel.PanelButtonStatus);
        }
        [TestMethod]
        public void TestDeactivateAllPanels_UpdateEventIsRaised()
        {
            Initialize();

            int eventRaisedCount = 0;
            panelMgmt.OnPanelMgmtUpdated += WhenEventRaisedIncreaseCounter;

            // when clicked, all panels are set to their deactivated state
            panelMgmt.ActivatePanel(2);
            panelMgmt.DeactivateAllPanels();

            // duplicate tests for checking assumptions
            Assert.AreEqual("", panelMgmt.Panels[0].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[0].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[1].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[1].BlurStatus);

            Assert.AreEqual("", panelMgmt.Panels[4].PanelButtonStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].PanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BehindPanelStatus);
            Assert.AreEqual("", panelMgmt.Panels[4].BlurStatus);

            Assert.AreEqual(panelMgmt.Panels[4], panelMgmt.PanelGroups[0].LocationPanel);
            Assert.AreEqual("highlight-button", panelMgmt.PanelGroups[0].LocationPanel.PanelButtonStatus);

            // the PanelMgmt event is raised
            Assert.AreEqual(2, panelMgmt.PanelGroups[0].LocationPanel.PanelButtonStatus);

        }
        [TestMethod]
        public void TestDeactivatePanel()
        {

        }
        [TestMethod]
        public void TestActivatePanel()
        {

        }
        [TestMethod]
        public void TogglePanel()
        {

        }
        [TestMethod]
        public void TestGetPanelsOfGroup()
        {

        }
        [TestMethod]
        public void Test()
        {

        }
        private void WhenEventRaisedIncreaseCounter(string meaningless) => 1;
    }
    */
}
