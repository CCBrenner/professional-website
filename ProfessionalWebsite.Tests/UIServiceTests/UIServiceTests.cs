using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

[TestClass]
public class UIServiceTests
{
    public UIServiceTests()
    {
        UiService = UIService.Create(
            MockAnimationsDataSource.GetIsContinuous(),
            MockPanelGroupsDataSource.GetPanelGroupsDict(),
            MockPanelsDataSource.GetPanelsDict(),
            MockSectionedPagesDataSource.GetSectionedPagesDict(),
            MockSectionsDataSource.GetSectionsDict()
            );
    }

    private UIService UiService;

    [TestMethod]
    public void TestClickingAnimationButtonPlaysAnimationOneTimePerSpecificUseCase()
    {
        // Given a string variable for changing the presence of a class in the "main" container of the application
        // and given a string variable for making a discontinue button appear or disappear is initially off
        Assert.AreEqual(string.Empty, UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);

        // When the user presses the element that has the appropriate @onclick event handler
        UiService.ToggleAnimation(0);

        // Then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
        // the animation's class name is updated, which triggers the animation of the main container to occur one time,
        // and the discontinue button does not appear.
        Assert.AreEqual("main1", UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);
    }
    [TestMethod]
    public void TestClickingAnimationButtonPlaysAnimationContinuouslyPerSpecificUseCase()
    {
        // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
        // and given a string variable for making a discontinue button appear or disappear is initially off...
        Assert.AreEqual(string.Empty, UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);

        // ...when the user presses the element that has the appropriate @onclick event handler seen here...
        UiService.ToggleContinuousAnimation(0);

        // ...then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
        // the animation's class name is updated (triggering the animation of the main container to occur an infinite number of times),
        // and the discontinue button appears.
        Assert.AreEqual("main1-infinite", UiService.AnimateMain);
        Assert.AreEqual("discontinue-button-on", UiService.Panels[8].PanelStatus);
    }
    [TestMethod]
    public void TestClickingAnimationButtonAnimatesMainOneTimeBasedOnAnimationsSpecificConfiguration()
    {
        // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
        // and given a string variable for making a discontinue button appear or disappear is initially off based on config defaults
        // and given a value corresponding to whether the specific animation should be played continuously or not...
        Assert.AreEqual(string.Empty, UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);
        Assert.IsFalse(UiService.IsContinuous[0]);

        // ...when the user presses the element that has the appropriate @onclick event handler seen here...
        UiService.ToggleAnimation(0);

        // ...then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
        // and the animation's class name remains the same, keeping the Discontinue button invisible,
        Assert.AreEqual("main1", UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);

        // Note: IsContinuous configs are to be used in cases where a UI control changes the bool value from continuous to single-play per animation
    }
    [TestMethod]
    public void TestClickingAnimationButtonAnimatesMainContinuouslyBasedOnAnimationsSpecificConfiguration()
    {
        // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
        // and given a string variable for making a discontinue button appear or disappear is initially off based on config defaults...
        Assert.AreEqual(string.Empty, UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);
        var IsContinuousList = UiService.IsContinuous;
        IsContinuousList[0] = true;
        Assert.IsTrue(UiService.IsContinuous[0]);

        // ...when the user presses the element that has the appropriate @onclick event handler seen here (note: we are using the animation with ID of "0" above as well as here)...
        UiService.ToggleAnimation(0);

        // ...then the animation's class name is updated based on a conversion of the animation ID, triggering the animation of the main container to occur an infinite number of times,
        // and the discontinue button appears.
        Assert.AreEqual("main1-infinite", UiService.AnimateMain);
        Assert.AreEqual("discontinue-button-on", UiService.Panels[8].PanelStatus);

        // Note: IsContinuous configs are to be used in cases where a UI control changes the bool value from continuous to single-play per animation
    }
    [TestMethod]
    public void TestClickingDiscontinueButtonStopsAnimationAndHidesDiscontinueButton()
    {
        // Given a continuous animation is playing on the main container
        // and the discontinue button is visible...
        UiService.ToggleContinuousAnimation(0);
        Assert.AreEqual("main1-infinite", UiService.AnimateMain);
        Assert.AreEqual("discontinue-button-on", UiService.Panels[8].PanelStatus);

        // ...when the user clicks the discontinue button...
        UiService.DiscontinueAnimation();

        // ...then the animation stops
        // and the discontinue button becomes invisible.
        Assert.AreEqual(string.Empty, UiService.AnimateMain);
        Assert.AreEqual(string.Empty, UiService.Panels[8].PanelStatus);
    }
    [TestMethod]
    public void TestClickingAnElementThatNavigatesToASectionChangesLocationPanelIdToThePanelIdOfTheDestinationPage()
    {
        // Given a user on any given web page
        // and given the default state of the destination sectioned page relevant to this behavior test...
        Assert.AreEqual(4, UiService.PanelGroups[0].LocationPanelId);

        // ...when they click an element responsible for Blazor navigation that refers to a section (which
        //     can be assumed as existing within a sectioned page)...
        UiService.NavigateToSection(13);

        // ...then update the navigation panel group to highlight the button of the destination page.
        Assert.AreEqual(3, UiService.PanelGroups[0].LocationPanelId);
    }
    [TestMethod]
    public void TestClickingAnElementThatNavgiatesToASectionPromotesItAndDoesNotPromoteAnyOtherSections ()
    {
        // Given a user on any given web page
        // and given the default state of the destination sectioned pages relevant to this behavior test...
        Assert.IsFalse(UiService.Sections[13].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[4].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[14].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[33].IsCurrentPromo);

        // ...when the user clicks an element that refers to a section (which can be assumed as existing within a sectioned page)...
        UiService.NavigateToSection(13);

        // ...then promote the section (visually moving it to the top above all other sections)
        // and ensure all other sections of the same sectioned page are not promoted/are demoted.
        Assert.IsTrue(UiService.Sections[13].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[4].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[14].IsCurrentPromo);
        Assert.IsFalse(UiService.Sections[33].IsCurrentPromo);
    }
    [TestMethod]
    public void TestClickingHeaderOfClosedSectionWhenAllSectionsAreCollapsedSwitchesFromUsingSectionsToUsingHeaderClones()
    {
        // Given a sectioned page that does not currently contain a section that is promoted...
        Assert.IsFalse(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);

        // ...when a user clicks a navigation element that takes then to a section of that sectioned page...
        UiService.NavigateToSection(13);

        // ...then the total number of expanded sections is equal to 1. Based on this, the cloned
        // section headers appear in place of the collapsed sections.
        Assert.IsTrue(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);
    }
    [TestMethod]
    public void TestClickingPromotedSectionHidesClonesOfSectionHeadersAndDemotesThePreviouslyPromotedSection()
    {
        // Given a sectioned page that is displaying its clones of the headers of its sections...
        UiService.NavigateToSection(13);
        Assert.IsTrue(UiService.Sections[13].IsCurrentPromo);
        Assert.IsTrue(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);

        // ...when a user closes/collapses/demotes the promoted section...
        UiService.ToggleSection(13);

        // ...then the total number of promoted sections equal zero. Based on this,
        // the actual sections are used once again in place of the cloned headers of the sections, which are made invisible.
        Assert.IsFalse(UiService.Sections[13].IsCurrentPromo);
        Assert.IsFalse(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);
    }
    [TestMethod]
    public void TestClickingHeaderOfClosedSectionWhenAllOtherSectionsAreClosedPromotesTheSection()
    {
        // Given all sections of a sectioned page are closed/collapsed/demoted...
        UiService.NavigateToSection(13); // 13 is open/promoted
        UiService.ToggleSectionedPage(1);  // all open
        UiService.ToggleSectionedPage(1);  // all closed
        Assert.IsFalse(UiService.Sections[13].IsCurrentPromo);
        Assert.IsTrue(UiService.Sections[13].IsCollapsed);
        Assert.IsTrue(UiService.Sections[4].IsCollapsed);
        Assert.IsTrue(UiService.Sections[7].IsCollapsed);
        Assert.IsTrue(UiService.Sections[16].IsCollapsed);
        Assert.IsFalse(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);

        // ...when the user clicks the header of a section...
        UiService.ToggleSection(13);

        // ...then the clicked section is promoted.
        Assert.IsTrue(UiService.Sections[13].IsCurrentPromo);
    }
    [TestMethod]
    public void TestClickingHeaderOfClosedSectionWhenAllOtherSectionsAreClosedReplacesSectionsWithClonedHeaders()
    {
        // Given all sections of a sectioned page are closed/collapsed/demoted...
        UiService.NavigateToSection(13);
        UiService.ToggleSectionedPage(1);
        UiService.ToggleSectionedPage(1);
        Assert.IsFalse(UiService.Sections[13].IsCurrentPromo);
        Assert.IsTrue(UiService.Sections[13].IsCollapsed);
        Assert.IsTrue(UiService.Sections[4].IsCollapsed);
        Assert.IsTrue(UiService.Sections[7].IsCollapsed);
        Assert.IsTrue(UiService.Sections[16].IsCollapsed);
        Assert.IsFalse(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);

        // ...when the user clicks the header of a section...
        UiService.ToggleSection(13);

        // ...then the closed sections are replaced with the cloned headers of the sections.
        Assert.IsTrue(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);
    }
    [TestMethod]
    public void TestClickingMassToggleToolForSectionsWhenASectionIsPromotedSwitchesPageFromUsingClonedHeadersToUsingTheActualSections()
    {
        // Given a section of a sectioned page is currently promoted...
        UiService.NavigateToSection(13);
        Assert.IsTrue(UiService.Sections[13].IsCurrentPromo);
        Assert.IsTrue(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);

        // ...when the mass toggle tool for sections is clicked...
        UiService.ToggleSectionedPage(1);

        // ...then no sections of the sectioned page are promoted any longer
        // and the actual sections are used again instead of the cloned headers of the sections.
        Assert.IsFalse(UiService.Sections[13].IsCurrentPromo);
        Assert.IsFalse(UiService.SectionedPages[1].ASectionIsCurrentlyPromo);
    }
    [TestMethod]
    public void TestClickingMassToggleToolForSectionsWhenAllSectionsAreOpenCollapsesAllSections()
    {
        // Given all sections of a sectioned page are open...
        UiService.NavigateToSection(13);
        UiService.ToggleSectionedPage(1);
        Assert.IsFalse(UiService.Sections[1].IsCollapsed);
        Assert.IsFalse(UiService.Sections[4].IsCollapsed);
        Assert.IsFalse(UiService.Sections[7].IsCollapsed);
        Assert.IsFalse(UiService.Sections[16].IsCollapsed);

        // ...when a user clicks the mass toggle tool for sections of the page...
        UiService.ToggleSectionedPage(1);

        // ...then all sections of the sectioned page are closed/collapsed.
        Assert.IsTrue(UiService.Sections[1].IsCollapsed);
        Assert.IsTrue(UiService.Sections[4].IsCollapsed);
        Assert.IsTrue(UiService.Sections[7].IsCollapsed);
        Assert.IsTrue(UiService.Sections[16].IsCollapsed);
    }
    [TestMethod]
    public void TestClickingAnElementThatOpensAHardCodedPageMakesThePageAppear()
    {
        // Given a hardcoded page (in the form of a panel)...
        int hardcodedPagePanelId = 7;
        int navPanelButtonId = 4;

        // ...when they click an element to open a page that has been hardcoded...
        UiService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);

        // ...then the hardcoded page appears.
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].PanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].PanelIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].BehindPanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].BehindPanelIsActive);
    }
    [TestMethod]
    public void TestClickingAnElementThatOpensAHardcodedPageUpdatesTheNavLocationPanelReferenceToTheHardcodedPagesDefinedPageLocation()
    {
        // Given a hardcoded page (in the form of a panel)
        // and given the ID of the initial location panel of the navigation panel group...
        int hardcodedPagePanelId = 7;
        int navPanelButtonId = 4;
        int navpanelGroupId = 0;
        UiService.NavigateToSection(3);
        Assert.AreEqual(3, UiService.PanelGroups[navpanelGroupId].LocationPanelId);

        // ...when they click an element that opens a hardcoded page...
        UiService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);

        // ...then the navigation panel group's location panel reference is updated to the specified page location of the hardcoded page. 
        Assert.AreEqual(4, UiService.PanelGroups[navpanelGroupId].LocationPanelId);
    }
    [TestMethod]
    public void TestClickingANavigationElementWhenOnAHardcodedPageClosesTheHardcodedPageAndBehindPanel()
    {
        // Given a user that is viewing a hardcoded page and an initially hidden behind panel...
        int hardcodedPagePanelId = 7;
        int navPanelButtonId = 4; 
        UiService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].PanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].PanelIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].BehindPanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].BehindPanelIsActive);

        // ...when they click an element that navigates them to another page away from the hardcoded page...
        UiService.NavigateToSection(2);

        // ...then the hardcoded page is made invisible
        // and the behind panel is closed.
        Assert.AreEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].PanelStatus);
        Assert.IsFalse(UiService.Panels[hardcodedPagePanelId].PanelIsActive);
        Assert.AreEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].BehindPanelStatus);
        Assert.IsFalse(UiService.Panels[hardcodedPagePanelId].BehindPanelIsActive);
    }
    [TestMethod]
    public void TestClickingANavigationElementWhenOnAHardcodedPageChangesTheLocationPanelOfNavToDestinationPagesLocationPanel()
    {
        // Given a user that is viewing a hardcoded page...
        int hardcodedPagePanelId = 7;
        int navPanelButtonId = 4; 
        UiService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].PanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].PanelIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[hardcodedPagePanelId].BehindPanelStatus);
        Assert.IsTrue(UiService.Panels[hardcodedPagePanelId].BehindPanelIsActive);

        // ...when they click an element that navigates them to another page away from the hardcoded page...
        UiService.NavigateToSection(3);

        // ...then the location panel of the navigation panel group is updated to the location panel of the destination page.
        Assert.AreEqual(3, UiService.PanelGroups[0].LocationPanelId);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesACooperativePanelMakesItActive()
    {
        // Given an inactive cooperative panel...
        Assert.AreEqual(string.Empty, UiService.Panels[0].PanelStatus);
        Assert.IsFalse(UiService.Panels[0].PanelIsActive);
        Assert.AreEqual(string.Empty, UiService.Panels[0].BehindPanelStatus);
        Assert.IsFalse(UiService.Panels[0].BehindPanelIsActive);
        Assert.AreEqual(string.Empty, UiService.Panels[0].BlurStatus);
        Assert.IsFalse(UiService.Panels[0].BlurIsActive);
        Assert.AreEqual(string.Empty, UiService.Panels[0].PanelButtonStatus);
        Assert.IsFalse(UiService.Panels[0].PanelButtonIsActive);

        // ...when the user clicks a panel toggle button...
        UiService.TogglePanel(0);

        // ...then all four values (and their boolean counterparts) are made active.
        Assert.AreNotEqual(string.Empty, UiService.Panels[0].PanelStatus);
        Assert.IsTrue(UiService.Panels[0].PanelIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[0].BehindPanelStatus);
        Assert.IsTrue(UiService.Panels[0].BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[0].BlurStatus);
        Assert.IsTrue(UiService.Panels[0].BlurIsActive);
        Assert.AreNotEqual(string.Empty, UiService.Panels[0].PanelButtonStatus);
        Assert.IsTrue(UiService.Panels[0].PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesACooperativePanelFromOffToOnHidesAllOtherPanels()
    {
        // Given a number of cooperative panels (for example, 3 panels)
        // and given only one cooperative panel and its behind panel are currently active...
        var panelZero = UiService.Panels[0];
        var panelOne = UiService.Panels[1];
        var panelTwo = UiService.Panels[2];

        UiService.TogglePanel(panelTwo.Id);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsFalse(panelOne.PanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsFalse(panelOne.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsFalse(panelOne.BlurIsActive);
        Assert.AreEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsFalse(panelOne.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelTwo.PanelStatus);
        Assert.IsTrue(panelTwo.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BehindPanelStatus);
        Assert.IsTrue(panelTwo.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BlurStatus);
        Assert.IsTrue(panelTwo.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.PanelButtonStatus);
        Assert.IsTrue(panelTwo.PanelButtonIsActive);

        // ...when the user clicks a panel toggle button to toggle the active panel to become inactive...
        UiService.TogglePanel(panelOne.Id);

        // ...then the single cooperative panel that was active is now hidden along with its behind panel,
        // leaving all panels inactive except for the toggled "On" panel.
        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelTwo.PanelStatus);
        Assert.IsFalse(panelTwo.PanelIsActive);
        Assert.AreEqual(string.Empty, panelTwo.BehindPanelStatus);
        Assert.IsFalse(panelTwo.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelTwo.BlurStatus);
        Assert.IsFalse(panelTwo.BlurIsActive);
        Assert.AreEqual(string.Empty, panelTwo.PanelButtonStatus);
        Assert.IsFalse(panelTwo.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesACooperativePanelFromOnToOffHidesThePanel()
    {
        // Given a cooperative panel that is currently active...
        var panelOne = UiService.Panels[1];

        UiService.TogglePanel(panelOne.Id);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);

        // ...when a user clicks a panel toggle button...
        UiService.TogglePanel(panelOne.Id);

        // ...then the cooperative panel is made inactive.
        Assert.AreEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsFalse(panelOne.PanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsFalse(panelOne.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsFalse(panelOne.BlurIsActive);
        Assert.AreEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsFalse(panelOne.PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesACooperativePanelFromOnToOffLeavesActiveIndependentPanelsActive()
    {
        // Given an independent panel that is currently active & a cooperative panel that is currently active...
        
        // Cooperative, active:
        var panelOne = UiService.Panels[1];
        UiService.TogglePanel(panelOne.Id);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);

        // Independent, active:
        var panelTwo = UiService.Panels[8];
        UiService.TogglePanel(panelTwo.Id);

        Assert.AreNotEqual(string.Empty, panelTwo.PanelStatus);
        Assert.IsTrue(panelTwo.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BehindPanelStatus);
        Assert.IsTrue(panelTwo.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BlurStatus);
        Assert.IsTrue(panelTwo.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.PanelButtonStatus);
        Assert.IsTrue(panelTwo.PanelButtonIsActive);

        // ...when a user clicks the backing panel...
        UiService.DeactivateCooperativePanels();

        // ...then the independent panel is still active.
        Assert.AreEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsFalse(panelOne.PanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsFalse(panelOne.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsFalse(panelOne.BlurIsActive);
        Assert.AreEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsFalse(panelOne.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelTwo.PanelStatus);
        Assert.IsTrue(panelTwo.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BehindPanelStatus);
        Assert.IsTrue(panelTwo.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.BlurStatus);
        Assert.IsTrue(panelTwo.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelTwo.PanelButtonStatus);
        Assert.IsTrue(panelTwo.PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesACooperativePanelFromOffToOnDoesNotCloseAnIndependentPanel()
    {
        // Given an inactive cooperative panel
        // and given an active independent panel...
        var panelZero = UiService.Panels[0];
        var panelSeven = UiService.Panels[7];
        var panelEight = UiService.Panels[8];

        UiService.TogglePanel(panelSeven.Id);
        UiService.TogglePanel(panelEight.Id);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelSeven.PanelStatus);
        Assert.IsTrue(panelSeven.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BehindPanelStatus);
        Assert.IsTrue(panelSeven.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BlurStatus);
        Assert.IsTrue(panelSeven.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.PanelButtonStatus);
        Assert.IsTrue(panelSeven.PanelButtonIsActive);

        Assert.AreNotEqual(panelSeven, panelEight);

        // ...when a user toggles the cooperative panel...
        UiService.TogglePanel(panelZero.Id);

        // ...then all independent panels that are active stay active.
        Assert.AreNotEqual(string.Empty, panelSeven.PanelStatus);
        Assert.IsTrue(panelSeven.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BehindPanelStatus);
        Assert.IsTrue(panelSeven.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BlurStatus);
        Assert.IsTrue(panelSeven.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.PanelButtonStatus);
        Assert.IsTrue(panelSeven.PanelButtonIsActive);

        Assert.AreNotEqual(panelSeven, panelEight);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesAnIndependentPanelFromOffToOnMakesBothCoopPanelsInactive()
    {
        // Given an inactive independent panel
        // and given two cooperative panels, one active and one inactive...
        var panelEight = UiService.Panels[8];  // independent
        var panelZero = UiService.Panels[0];  // cooperative
        var panelOne = UiService.Panels[1];  // cooperative

        UiService.TogglePanel(panelOne.Id);

        Assert.AreEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsFalse(panelEight.PanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsFalse(panelEight.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsFalse(panelEight.BlurIsActive);
        Assert.AreEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsFalse(panelEight.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);

        // ...when a user clicks a toggle panel button to toggle the independent panel...
        UiService.TogglePanel(panelEight.Id);

        // ...then the cooperative panels both become inactive.
        Assert.AreNotEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsTrue(panelEight.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsTrue(panelEight.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsTrue(panelEight.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsTrue(panelEight.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(panelZero, panelOne);
    }
    [TestMethod]
    public void TestClickingAnElementThatTogglesAnIndependentPanelFromOnToOffMakesBothCoopPanelsInactive()
    {
        // Given an active independent panel
        // and given two cooperative panels, one active and one inactive...
        var panelEight = UiService.Panels[8];  // independent
        var panelZero = UiService.Panels[0];  // cooperative
        var panelOne = UiService.Panels[1];  // cooperative

        UiService.TogglePanel(panelEight.Id);
        UiService.TogglePanel(panelOne.Id);

        Assert.AreNotEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsTrue(panelEight.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsTrue(panelEight.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsTrue(panelEight.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsTrue(panelEight.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);

        // ...when a user clicks a toggle panel button to toggle the independent panel...
        UiService.TogglePanel(panelEight.Id);

        // ...then the cooperative panels both become inactive.
        Assert.AreEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsFalse(panelEight.PanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsFalse(panelEight.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsFalse(panelEight.BlurIsActive);
        Assert.AreEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsFalse(panelEight.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(panelZero, panelOne);
    }
    [TestMethod]
    public void TestClickingElementThatActivatesCooperativePanelThatIsNotPartOfASpecificPanelGroupHighlightsFocusedPanelButtonOfPanelGroup()
    {
        //TestClickingAnElementThatTogglesACooperativePanelFromOffToOnDoesNotCloseAnIndependentPanel()
        // Given that all panels in panelGroups are cooperative panels, 
        // and given focused panel is panel.Id 4
        // and given panel.Id is a part of the same panelGroup as 4 and is activated...
        var panelTwo = UiService.Panels[2];  // cooperative, IN panelGroup
        var panelZero = UiService.Panels[0];  // cooperative, NOT in panelGroup
        var panelFourFocusedPanel = UiService.Panels[4];  // cooperative, IN panelGroup and is focused panel

        UiService.TogglePanel(panelTwo.Id); 

        // ...when a user toggles the cooperative panel that is NOT part of the panelGroup...
        UiService.TogglePanel(panelZero.Id);

        // ...then the button of the focused panel of the panelGroup is highlighted (panel.Id 4).
        Assert.AreNotEqual(string.Empty, panelFourFocusedPanel.PanelButtonStatus);
        Assert.IsTrue(panelFourFocusedPanel.PanelButtonIsActive);

        // extra checks on expected state:
        Assert.AreEqual(string.Empty, panelFourFocusedPanel.PanelStatus);
        Assert.IsFalse(panelFourFocusedPanel.PanelIsActive);

        Assert.AreEqual(string.Empty, panelTwo.PanelButtonStatus);
        Assert.IsFalse(panelTwo.PanelButtonIsActive);
        Assert.AreEqual(string.Empty, panelTwo.PanelStatus);
        Assert.IsFalse(panelTwo.PanelIsActive);

        Assert.AreNotEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsTrue(panelZero.PanelButtonIsActive);
        Assert.AreNotEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsTrue(panelZero.PanelIsActive);
    }
    [TestMethod]
    public void TestClickingPanelToggleForSecondIndependentPanelDoesNotHideFirstActiveIndependentPanel()
    {
        // Given an inactive and inactive independent panels...
        var panelSeven = UiService.Panels[7];  // independent
        var panelEight = UiService.Panels[8];  // independent

        UiService.TogglePanel(panelSeven.Id);

        Assert.AreNotEqual(string.Empty, panelSeven.PanelStatus);
        Assert.IsTrue(panelSeven.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BehindPanelStatus);
        Assert.IsTrue(panelSeven.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BlurStatus);
        Assert.IsTrue(panelSeven.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.PanelButtonStatus);
        Assert.IsTrue(panelSeven.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsFalse(panelEight.PanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsFalse(panelEight.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsFalse(panelEight.BlurIsActive);
        Assert.AreEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsFalse(panelEight.PanelButtonIsActive);

        // ...when the inactive independent panel is toggled from inactive to active...
        UiService.TogglePanel(panelEight.Id);

        // ...then both independent panels are active.
        Assert.AreNotEqual(string.Empty, panelSeven.PanelStatus);
        Assert.IsTrue(panelSeven.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BehindPanelStatus);
        Assert.IsTrue(panelSeven.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.BlurStatus);
        Assert.IsTrue(panelSeven.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelSeven.PanelButtonStatus);
        Assert.IsTrue(panelSeven.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsTrue(panelEight.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsTrue(panelEight.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsTrue(panelEight.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsTrue(panelEight.PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingLinkToNonSectionedPageUpdatesAllPanelsToBeingInvisible()
    {
        // Given an anchor element with an @onclick handler
        // and given all panels (cooperative and independent) where panels are currently active...
        var panelZero = UiService.Panels[0];  // cooperative
        var panelOne = UiService.Panels[1];  // cooperative
        var panelEight = UiService.Panels[8];  // independent

        UiService.TogglePanel(panelEight.Id);
        UiService.TogglePanel(panelOne.Id);

        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsTrue(panelOne.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsTrue(panelOne.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsTrue(panelOne.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsTrue(panelOne.PanelButtonIsActive);

        Assert.AreNotEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsTrue(panelEight.PanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsTrue(panelEight.BehindPanelIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsTrue(panelEight.BlurIsActive);
        Assert.AreNotEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsTrue(panelEight.PanelButtonIsActive);

        // ...when a user clicks an anchor element with the below method added in its @onclick event handler...
        UiService.UpdatePanelsWhenNavigating(1);

        // ...then all panels are closed, no matter if the panel is cooperative or indendent.
        Assert.AreEqual(string.Empty, panelZero.PanelStatus);
        Assert.IsFalse(panelZero.PanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BehindPanelStatus);
        Assert.IsFalse(panelZero.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelZero.BlurStatus);
        Assert.IsFalse(panelZero.BlurIsActive);
        Assert.AreEqual(string.Empty, panelZero.PanelButtonStatus);
        Assert.IsFalse(panelZero.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelOne.PanelStatus);
        Assert.IsFalse(panelOne.PanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BehindPanelStatus);
        Assert.IsFalse(panelOne.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelOne.BlurStatus);
        Assert.IsFalse(panelOne.BlurIsActive);
        Assert.AreEqual(string.Empty, panelOne.PanelButtonStatus);
        Assert.IsFalse(panelOne.PanelButtonIsActive);

        Assert.AreEqual(string.Empty, panelEight.PanelStatus);
        Assert.IsFalse(panelEight.PanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BehindPanelStatus);
        Assert.IsFalse(panelEight.BehindPanelIsActive);
        Assert.AreEqual(string.Empty, panelEight.BlurStatus);
        Assert.IsFalse(panelEight.BlurIsActive);
        Assert.AreEqual(string.Empty, panelEight.PanelButtonStatus);
        Assert.IsFalse(panelEight.PanelButtonIsActive);
    }
    [TestMethod]
    public void TestClickingALinkToANonSectionedpageUpdatesNavPanelGroupsLocationPanel()
    {
        // Given the location panel ID of the nav panel group...
        Assert.AreEqual(4, UiService.PanelGroups[0].LocationPanelId);

        // ...when a user clicks a link with the method below implemented in the link element
        // and when the page destination is diffferent than the starting page location...
        UiService.UpdatePanelsWhenNavigating(3);  // starting location is sectionedPageId = 2;

        // ...then the nav panel group's location panel ID is updated to the destination page, highlighting that page's panel button.
        Assert.AreEqual(3, UiService.PanelGroups[0].LocationPanelId);
    }
}
