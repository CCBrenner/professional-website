using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests
{
    [TestClass]
    public class UIServiceTests
    {
        public UIServiceTests()
        {
            uIService = new UIService();
        }

        private UIService uIService;

        [TestMethod]
        public void TestClickingAnimationButtonPlaysAnimationOneTimePerSpecificUseCase()
        {
            // Given a string variable for changing the presence of a class in the "main" container of the application
            // and given a string variable for making a discontinue button appear or disappear is initially off
            Assert.AreEqual("", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);

            // When the user presses the element that has the appropriate @onclick event handler
            uIService.PlayAnimation(0, false);

            // Then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
            // the animation's class name is updated, which triggers the animation of the main container to occur one time,
            // and the discontinue button does not appear.
            Assert.AreEqual("main1", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);
        }

        [TestMethod]
        public void TestClickingAnimationButtonPlaysAnimationContinuouslyPerSpecificUseCase()
        {
            // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
            // and given a string variable for making a discontinue button appear or disappear is initially off...
            Assert.AreEqual("", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);

            // ...when the user presses the element that has the appropriate @onclick event handler seen here...
            uIService.PlayAnimation(0, true);

            // ...then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
            // the animation's class name is updated (triggering the animation of the main container to occur an infinite number of times),
            // and the discontinue button appears.
            Assert.AreEqual("main1-infinite", uIService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", uIService.Panels[8].PanelStatus);
        }

        [TestMethod]
        public void TestClickingAnimationButtonAnimatesMainOneTimeBasedOnAnimationsSpecificConfiguration()
        {
            // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
            // and given a string variable for making a discontinue button appear or disappear is initially off based on config defaults
            // and given a value corresponding to whether the specific animation should be played continuously or not...
            Assert.AreEqual("", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);
            Assert.AreEqual(false, uIService.IsContinuous[0]);

            // ...when the user presses the element that has the appropriate @onclick event handler seen here...
            uIService.PlayAnimation(0);

            // ...then the animation id is converted to one of the defined class names with defined associated CSS @keyframe,
            // and the animation's class name remains the same, keeping the Discontinue button invisible,
            Assert.AreEqual("main1", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);

            // Note: IsContinuous configs are to be used in cases where a UI control changes the bool value from continuous to single-play per animation
        }

        [TestMethod]
        public void TestClickingAnimationButtonAnimatesMainContinuouslyBasedOnAnimationsSpecificConfiguration()
        {
            // Given a string variable for changing the presence of a class in the class property of the "main" container of the application
            // and given a string variable for making a discontinue button appear or disappear is initially off based on config defaults...
            Assert.AreEqual("", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);
            var IsContinuousList = uIService.IsContinuous;
            IsContinuousList[0] = true;
            Assert.AreEqual(true, uIService.IsContinuous[0]);

            // ...when the user presses the element that has the appropriate @onclick event handler seen here (note: we are using the animation with ID of "0" above as well as here)...
            uIService.PlayAnimation(0);

            // ...then the animation's class name is updated based on a conversion of the animation ID, triggering the animation of the main container to occur an infinite number of times,
            // and the discontinue button appears.
            Assert.AreEqual("main1-infinite", uIService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", uIService.Panels[8].PanelStatus);

            // Note: IsContinuous configs are to be used in cases where a UI control changes the bool value from continuous to single-play per animation
        }

        [TestMethod]
        public void TestClickingDiscontinueButtonStopsAnimationAndHidesDiscontinueButton()
        {
            // Given a continuous animation is playing on the main container
            // and the discontinue button is visible...
            uIService.PlayAnimation(0, true);
            Assert.AreEqual("main1-infinite", uIService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", uIService.Panels[8].PanelStatus);

            // ...when the user clicks the discontinue button...
            uIService.DiscontinueAnimation();

            // ...then the animation stops
            // and the discontinue button becomes invisible.
            Assert.AreEqual("", uIService.AnimateMain);
            Assert.AreEqual("", uIService.Panels[8].PanelStatus);
        }

        [TestMethod]
        public void TestClickingAnElementThatNavigatesToASectionChangesLocationPanelIdToThePanelIdOfTheDestinationPage()
        {
            // Given a user on any given web page
            // and given the default state of the destination sectioned page relevant to this behavior test...
            Assert.AreEqual(4, uIService.PanelGroups[0].LocationPanelId);

            // ...when they click an element responsible for Blazor navigation that refers to a section (which
            //     can be assumed as existing within a sectioned page)...
            uIService.NavigateToSection(13);

            // ...then update the navigation panel group to highlight the button of the destination page.
            Assert.AreEqual(3, uIService.PanelGroups[0].LocationPanelId);
        }

        [TestMethod]
        public void TestClickingAnElementThatNavgiatesToASectionPromotesItAndDoesNotPromoteAnyOtherSections ()
        {
            // Given a user on any given web page
            // and given the default state of the destination sectioned pages relevant to this behavior test...
            Assert.AreEqual(false, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[14].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[33].IsCurrentPromo);

            // ...when the user clicks an element that refers to a section (which can be assumed as existing within a sectioned page)...
            uIService.NavigateToSection(13);

            // ...then promote the section (visually moving it to the top above all other sections)
            // and ensure all other sections of the same sectioned page are not promoted/are demoted.
            Assert.AreEqual(true, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[4].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[14].IsCurrentPromo);
            Assert.AreEqual(false, uIService.Sections[33].IsCurrentPromo);
        }

        [TestMethod]
        public void TestClickingHeaderOfClosedSectionWhenAllSectionsAreCollapsedSwitchesFromUsingSectionsToUsingHeaderClones()
        {
            // Given a sectioned page that does not currently contain a section that is promoted...
            Assert.AreEqual(false, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);

            // ...when a user clicks a navigation element that takes then to a section of that sectioned page...
            uIService.NavigateToSection(13);

            // ...then the total number of expanded sections is equal to 1. Based on this, the cloned
            // section headers appear in place of the collapsed sections.
            Assert.AreEqual(true, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);
        }

        [TestMethod]
        public void TestClickingPromotedSectionHidesClonesOfSectionHeadersAndDemotesThePreviouslyPromotedSection()
        {
            // Given a sectioned page that is displaying its clones of the headers of its sections...
            uIService.NavigateToSection(13);
            Assert.AreEqual(true, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(true, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);

            // ...when a user closes/collapses/demotes the promoted section...
            uIService.ToggleSection(13);

            // ...then the total number of promoted sections equal zero. Based on this,
            // the actual sections are used once again in place of the cloned headers of the sections, which are made invisible.
            Assert.AreEqual(false, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(false, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);
        }

        [TestMethod]
        public void TestClickingHeaderOfClosedSectionWhenAllOtherSectionsAreClosedPromotesTheSection()
        {
            // Given all sections of a sectioned page are closed/collapsed/demoted...
            uIService.NavigateToSection(13);
            uIService.ToggleAllSections(1);
            uIService.ToggleAllSections(1);
            Assert.AreEqual(false, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(true, uIService.Sections[13].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[4].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[7].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[16].IsCollapsed);
            Assert.AreEqual(false, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);

            // ...when the user clicks the header of a section...
            uIService.ToggleSection(13);

            // ...then the clicked section is promoted.
            Assert.AreEqual(true, uIService.Sections[13].IsCurrentPromo);
        }

        [TestMethod]
        public void TestClickingHeaderOfClosedSectionWhenAllOtherSectionsAreClosedReplacesSectionsWithClonedHeaders()
        {
            // Given all sections of a sectioned page are closed/collapsed/demoted...
            uIService.NavigateToSection(13);
            uIService.ToggleAllSections(1);
            uIService.ToggleAllSections(1);
            Assert.AreEqual(false, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(true, uIService.Sections[13].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[4].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[7].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[16].IsCollapsed);
            Assert.AreEqual(false, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);

            // ...when the user clicks the header of a section...
            uIService.ToggleSection(13);

            // ...then the closed sections are replaced with the cloned headers of the sections.
            Assert.AreEqual(true, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);
        }

        [TestMethod]
        public void TestClickingMassToggleToolForSectionsWhenASectionIsPromotedSwitchesPageFromUsingClonedHeadersToUsingTheActualSections()
        {
            // Given a section of a sectioned page is currently promoted...
            uIService.NavigateToSection(13);
            Assert.AreEqual(true, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(true, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);

            // ...when the mass toggle tool for sections is clicked...
            uIService.ToggleAllSections(1);

            // ...then no sections of the sectioned page are promoted any longer
            // and the actual sections are used again instead of the cloned headers of the sections.
            Assert.AreEqual(false, uIService.Sections[13].IsCurrentPromo);
            Assert.AreEqual(false, uIService.SectionedPages[1].ASectionIsCurrentlyPromo);
        }

        [TestMethod]
        public void TestClickingMassToggleToolForSectionsWhenAllSectionsAreOpenCollapsesAllSections()
        {
            // Given all sections of a sectioned page are open...
            uIService.NavigateToSection(13);
            uIService.ToggleAllSections(1);
            Assert.AreEqual(false, uIService.Sections[1].IsCollapsed);
            Assert.AreEqual(false, uIService.Sections[4].IsCollapsed);
            Assert.AreEqual(false, uIService.Sections[7].IsCollapsed);
            Assert.AreEqual(false, uIService.Sections[16].IsCollapsed);

            // ...when a user clicks the mass toggle tool for sections of the page...
            uIService.ToggleAllSections(1);

            // ...then all sections of the sectioned page are closed/collapsed.
            Assert.AreEqual(true, uIService.Sections[1].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[4].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[7].IsCollapsed);
            Assert.AreEqual(true, uIService.Sections[16].IsCollapsed);
        }
        
        [TestMethod]
        public void TestClickingAnElementThatOpensAHardCodedPageMakesThePageAppear()
        {
            // Given a hardcoded page (in the form of a panel)...
            int hardcodedPagePanelId = 7;
            int navPanelButtonId = 4;

            // ...when they click an element to open a page that has been hardcoded...
            uIService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);

            // ...then the hardcoded page appears.
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].PanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].PanelIsActive);
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].BehindPanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].BehindPanelIsActive);
        }

        [TestMethod]
        public void TestClickingAnElementThatOpensAHardcodedPageUpdatesTheNavLocationPanelReferenceToTheHardcodedPagesDefinedPageLocation()
        {
            // Given a hardcoded page (in the form of a panel)
            // and given the ID of the initial location panel of the navigation panel group...
            int hardcodedPagePanelId = 7;
            int navPanelButtonId = 4;
            int navpanelGroupId = 0;
            uIService.NavigateToSection(3);
            Assert.AreEqual(3, uIService.PanelGroups[navpanelGroupId].LocationPanelId);

            // ...when they click an element that opens a hardcoded page...
            uIService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);

            // ...then the navigation panel group's location panel reference is updated to the specified page location of the hardcoded page. 
            Assert.AreEqual(4, uIService.PanelGroups[navpanelGroupId].LocationPanelId);
        }

        [TestMethod]
        public void TestClickingANavigationElementWhenOnAHardcodedPageClosesTheHardcodedPageAndBehindPanel()
        {
            // Given a user that is viewing a hardcoded page and an initially hidden behind panel...
            int hardcodedPagePanelId = 7;
            int navPanelButtonId = 4; 
            uIService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].PanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].PanelIsActive);
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].BehindPanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].BehindPanelIsActive);

            // ...when they click an element that navigates them to another page away from the hardcoded page...
            uIService.NavigateToSection(2);

            // ...then the hardcoded page is made invisible
            // and the behind panel is closed.
            Assert.AreEqual("", uIService.Panels[hardcodedPagePanelId].PanelStatus);
            Assert.AreEqual(false, uIService.Panels[hardcodedPagePanelId].PanelIsActive);
            Assert.AreEqual("", uIService.Panels[hardcodedPagePanelId].BehindPanelStatus);
            Assert.AreEqual(false, uIService.Panels[hardcodedPagePanelId].BehindPanelIsActive);
        }

        [TestMethod]
        public void TestClickingANavigationElementWhenOnAHardcodedPageChangesTheLocationPanelOfNavToDestinationPagesLocationPanel()
        {
            // Given a user that is viewing a hardcoded page...
            int hardcodedPagePanelId = 7;
            int navPanelButtonId = 4; 
            uIService.NavigateToHardCodedPage(hardcodedPagePanelId, navPanelButtonId);
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].PanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].PanelIsActive);
            Assert.AreNotEqual("", uIService.Panels[hardcodedPagePanelId].BehindPanelStatus);
            Assert.AreEqual(true, uIService.Panels[hardcodedPagePanelId].BehindPanelIsActive);

            // ...when they click an element that navigates them to another page away from the hardcoded page...
            uIService.NavigateToSection(3);

            // ...then the location panel of the navigation panel group is updated to the location panel of the destination page.
            Assert.AreEqual(3, uIService.PanelGroups[0].LocationPanelId);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesACooperativePanelMakesItActive()
        {
            // Given an inactive cooperative panel...
            Assert.AreEqual("", uIService.Panels[0].PanelStatus);
            Assert.AreEqual(false, uIService.Panels[0].PanelIsActive);
            Assert.AreEqual("", uIService.Panels[0].BehindPanelStatus);
            Assert.AreEqual(false, uIService.Panels[0].BehindPanelIsActive);
            Assert.AreEqual("", uIService.Panels[0].BlurStatus);
            Assert.AreEqual(false, uIService.Panels[0].BlurIsActive);
            Assert.AreEqual("", uIService.Panels[0].PanelButtonStatus);
            Assert.AreEqual(false, uIService.Panels[0].PanelButtonIsActive);

            // ...when the user clicks a panel toggle button...
            uIService.TogglePanel(0);

            // ...then all four values (and their boolean counterparts) are made active.
            Assert.AreNotEqual("", uIService.Panels[0].PanelStatus);
            Assert.AreEqual(true, uIService.Panels[0].PanelIsActive);
            Assert.AreNotEqual("", uIService.Panels[0].BehindPanelStatus);
            Assert.AreEqual(true, uIService.Panels[0].BehindPanelIsActive);
            Assert.AreNotEqual("", uIService.Panels[0].BlurStatus);
            Assert.AreEqual(true, uIService.Panels[0].BlurIsActive);
            Assert.AreNotEqual("", uIService.Panels[0].PanelButtonStatus);
            Assert.AreEqual(true, uIService.Panels[0].PanelButtonIsActive);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesACooperativePanelFromOffToOnHidesAllOtherPanels()
        {
            // Given a number of cooperative panels (for example, 3 panels)
            // and given only one cooperative panel and its behind panel are currently active...
            var panelZero = uIService.Panels[0];
            var panelOne = uIService.Panels[1];
            var panelTwo = uIService.Panels[2];

            uIService.TogglePanel(panelTwo.Id);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreEqual("", panelOne.PanelStatus);
            Assert.AreEqual(false, panelOne.PanelIsActive);
            Assert.AreEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(false, panelOne.BehindPanelIsActive);
            Assert.AreEqual("", panelOne.BlurStatus);
            Assert.AreEqual(false, panelOne.BlurIsActive);
            Assert.AreEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(false, panelOne.PanelButtonIsActive);

            Assert.AreNotEqual("", panelTwo.PanelStatus);
            Assert.AreEqual(true, panelTwo.PanelIsActive);
            Assert.AreNotEqual("", panelTwo.BehindPanelStatus);
            Assert.AreEqual(true, panelTwo.BehindPanelIsActive);
            Assert.AreNotEqual("", panelTwo.BlurStatus);
            Assert.AreEqual(true, panelTwo.BlurIsActive);
            Assert.AreNotEqual("", panelTwo.PanelButtonStatus);
            Assert.AreEqual(true, panelTwo.PanelButtonIsActive);

            // ...when the user clicks a panel toggle button to toggle the active panel to become inactive...
            uIService.TogglePanel(panelOne.Id);

            // ...then the single cooperative panel that was active is now hidden along with its behind panel,
            // leaving all panels inactive except for the toggled "On" panel.
            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreEqual("", panelTwo.PanelStatus);
            Assert.AreEqual(false, panelTwo.PanelIsActive);
            Assert.AreEqual("", panelTwo.BehindPanelStatus);
            Assert.AreEqual(false, panelTwo.BehindPanelIsActive);
            Assert.AreEqual("", panelTwo.BlurStatus);
            Assert.AreEqual(false, panelTwo.BlurIsActive);
            Assert.AreEqual("", panelTwo.PanelButtonStatus);
            Assert.AreEqual(false, panelTwo.PanelButtonIsActive);

            Assert.AreNotEqual("", panelOne.PanelStatus);
            Assert.AreEqual(true, panelOne.PanelIsActive);
            Assert.AreNotEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(true, panelOne.BehindPanelIsActive);
            Assert.AreNotEqual("", panelOne.BlurStatus);
            Assert.AreEqual(true, panelOne.BlurIsActive);
            Assert.AreNotEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(true, panelOne.PanelButtonIsActive);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesACooperativePanelFromOnToOffHidesThePanel()
        {
            // Given a cooperative panel that is currently active...
            var panelOne = uIService.Panels[1];

            uIService.TogglePanel(panelOne.Id);

            Assert.AreNotEqual("", panelOne.PanelStatus);
            Assert.AreEqual(true, panelOne.PanelIsActive);
            Assert.AreNotEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(true, panelOne.BehindPanelIsActive);
            Assert.AreNotEqual("", panelOne.BlurStatus);
            Assert.AreEqual(true, panelOne.BlurIsActive);
            Assert.AreNotEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(true, panelOne.PanelButtonIsActive);

            // ...when a user clicks a panel toggle button...
            uIService.TogglePanel(panelOne.Id);

            // ...then the cooperative panel is made inactive.
            Assert.AreEqual("", panelOne.PanelStatus);
            Assert.AreEqual(false, panelOne.PanelIsActive);
            Assert.AreEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(false, panelOne.BehindPanelIsActive);
            Assert.AreEqual("", panelOne.BlurStatus);
            Assert.AreEqual(false, panelOne.BlurIsActive);
            Assert.AreEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(false, panelOne.PanelButtonIsActive);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesACooperativePanelFromOffToOnDoesNotCloseAnIndependentPanel()
        {
            // Given an inactive cooperative panel
            // and given an active independent panel...
            var panelZero = uIService.Panels[0];
            var panelSeven = uIService.Panels[7];
            var panelEight = uIService.Panels[8];

            uIService.TogglePanel(panelSeven.Id);
            uIService.TogglePanel(panelEight.Id);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual("", panelSeven.PanelStatus);
            Assert.AreEqual(true, panelSeven.PanelIsActive);
            Assert.AreNotEqual("", panelSeven.BehindPanelStatus);
            Assert.AreEqual(true, panelSeven.BehindPanelIsActive);
            Assert.AreNotEqual("", panelSeven.BlurStatus);
            Assert.AreEqual(true, panelSeven.BlurIsActive);
            Assert.AreNotEqual("", panelSeven.PanelButtonStatus);
            Assert.AreEqual(true, panelSeven.PanelButtonIsActive);

            Assert.AreNotEqual(panelSeven, panelEight);

            // ...when a user toggles the cooperative panel...
            uIService.TogglePanel(panelZero.Id);

            // ...then all independent panels that are active stay active.
            Assert.AreNotEqual("", panelSeven.PanelStatus);
            Assert.AreEqual(true, panelSeven.PanelIsActive);
            Assert.AreNotEqual("", panelSeven.BehindPanelStatus);
            Assert.AreEqual(true, panelSeven.BehindPanelIsActive);
            Assert.AreNotEqual("", panelSeven.BlurStatus);
            Assert.AreEqual(true, panelSeven.BlurIsActive);
            Assert.AreNotEqual("", panelSeven.PanelButtonStatus);
            Assert.AreEqual(true, panelSeven.PanelButtonIsActive);

            Assert.AreNotEqual(panelSeven, panelEight);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesAnIndependentPanelFromOffToOnMakesBothCoopPanelsInactive()
        {
            // Given an inactive independent panel
            // and given two cooperative panels, one active and one inactive...
            var panelEight = uIService.Panels[8];  // independent
            var panelZero = uIService.Panels[0];  // cooperative
            var panelOne = uIService.Panels[1];  // cooperative

            uIService.TogglePanel(panelOne.Id);

            Assert.AreEqual("", panelEight.PanelStatus);
            Assert.AreEqual(false, panelEight.PanelIsActive);
            Assert.AreEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(false, panelEight.BehindPanelIsActive);
            Assert.AreEqual("", panelEight.BlurStatus);
            Assert.AreEqual(false, panelEight.BlurIsActive);
            Assert.AreEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(false, panelEight.PanelButtonIsActive);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual("", panelOne.PanelStatus);
            Assert.AreEqual(true, panelOne.PanelIsActive);
            Assert.AreNotEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(true, panelOne.BehindPanelIsActive);
            Assert.AreNotEqual("", panelOne.BlurStatus);
            Assert.AreEqual(true, panelOne.BlurIsActive);
            Assert.AreNotEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(true, panelOne.PanelButtonIsActive);

            // ...when a user clicks a toggle panel button to toggle the independent panel...
            uIService.TogglePanel(panelEight.Id);

            // ...then the cooperative panels both become inactive.
            Assert.AreNotEqual("", panelEight.PanelStatus);
            Assert.AreEqual(true, panelEight.PanelIsActive);
            Assert.AreNotEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(true, panelEight.BehindPanelIsActive);
            Assert.AreNotEqual("", panelEight.BlurStatus);
            Assert.AreEqual(true, panelEight.BlurIsActive);
            Assert.AreNotEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(true, panelEight.PanelButtonIsActive);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual(panelZero, panelOne);
        }

        [TestMethod]
        public void TestClickingAnElementThatTogglesAnIndependentPanelFromOnToOffMakesBothCoopPanelsInactive()
        {
            // Given an active independent panel
            // and given two cooperative panels, one active and one inactive...
            var panelEight = uIService.Panels[8];  // independent
            var panelZero = uIService.Panels[0];  // cooperative
            var panelOne = uIService.Panels[1];  // cooperative

            uIService.TogglePanel(panelEight.Id);
            uIService.TogglePanel(panelOne.Id);

            Assert.AreNotEqual("", panelEight.PanelStatus);
            Assert.AreEqual(true, panelEight.PanelIsActive);
            Assert.AreNotEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(true, panelEight.BehindPanelIsActive);
            Assert.AreNotEqual("", panelEight.BlurStatus);
            Assert.AreEqual(true, panelEight.BlurIsActive);
            Assert.AreNotEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(true, panelEight.PanelButtonIsActive);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual("", panelOne.PanelStatus);
            Assert.AreEqual(true, panelOne.PanelIsActive);
            Assert.AreNotEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(true, panelOne.BehindPanelIsActive);
            Assert.AreNotEqual("", panelOne.BlurStatus);
            Assert.AreEqual(true, panelOne.BlurIsActive);
            Assert.AreNotEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(true, panelOne.PanelButtonIsActive);

            // ...when a user clicks a toggle panel button to toggle the independent panel...
            uIService.TogglePanel(panelEight.Id);

            // ...then the cooperative panels both become inactive.
            Assert.AreEqual("", panelEight.PanelStatus);
            Assert.AreEqual(false, panelEight.PanelIsActive);
            Assert.AreEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(false, panelEight.BehindPanelIsActive);
            Assert.AreEqual("", panelEight.BlurStatus);
            Assert.AreEqual(false, panelEight.BlurIsActive);
            Assert.AreEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(false, panelEight.PanelButtonIsActive);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual(panelZero, panelOne);
        }

        [TestMethod]
        public void TestClickingPanelToggleForSecondIndependentPanelDoesNotHideFirstActiveIndependentPanel()
        {
            // Given an inactive and inactive independent panels...
            var panelSeven = uIService.Panels[7];  // independent
            var panelEight = uIService.Panels[8];  // independent

            uIService.TogglePanel(panelSeven.Id);

            Assert.AreNotEqual("", panelSeven.PanelStatus);
            Assert.AreEqual(true, panelSeven.PanelIsActive);
            Assert.AreNotEqual("", panelSeven.BehindPanelStatus);
            Assert.AreEqual(true, panelSeven.BehindPanelIsActive);
            Assert.AreNotEqual("", panelSeven.BlurStatus);
            Assert.AreEqual(true, panelSeven.BlurIsActive);
            Assert.AreNotEqual("", panelSeven.PanelButtonStatus);
            Assert.AreEqual(true, panelSeven.PanelButtonIsActive);

            Assert.AreEqual("", panelEight.PanelStatus);
            Assert.AreEqual(false, panelEight.PanelIsActive);
            Assert.AreEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(false, panelEight.BehindPanelIsActive);
            Assert.AreEqual("", panelEight.BlurStatus);
            Assert.AreEqual(false, panelEight.BlurIsActive);
            Assert.AreEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(false, panelEight.PanelButtonIsActive);

            // ...when the inactive independent panel is toggled from inactive to active...
            uIService.TogglePanel(panelEight.Id);

            // ...then both independent panels are active.
            Assert.AreNotEqual("", panelSeven.PanelStatus);
            Assert.AreEqual(true, panelSeven.PanelIsActive);
            Assert.AreNotEqual("", panelSeven.BehindPanelStatus);
            Assert.AreEqual(true, panelSeven.BehindPanelIsActive);
            Assert.AreNotEqual("", panelSeven.BlurStatus);
            Assert.AreEqual(true, panelSeven.BlurIsActive);
            Assert.AreNotEqual("", panelSeven.PanelButtonStatus);
            Assert.AreEqual(true, panelSeven.PanelButtonIsActive);

            Assert.AreNotEqual("", panelEight.PanelStatus);
            Assert.AreEqual(true, panelEight.PanelIsActive);
            Assert.AreNotEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(true, panelEight.BehindPanelIsActive);
            Assert.AreNotEqual("", panelEight.BlurStatus);
            Assert.AreEqual(true, panelEight.BlurIsActive);
            Assert.AreNotEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(true, panelEight.PanelButtonIsActive);
        }

        [TestMethod]
        public void TestClickingLinkToNonSectionedPageUpdatesAllPanelsToBeingInvisible()
        {
            // Given an anchor element with an @onclick handler
            // and given all panels (cooperative and independent) where panels are currently active...
            var panelZero = uIService.Panels[0];  // cooperative
            var panelOne = uIService.Panels[1];  // cooperative
            var panelEight = uIService.Panels[8];  // independent

            uIService.TogglePanel(panelEight.Id);
            uIService.TogglePanel(panelOne.Id);

            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreNotEqual("", panelOne.PanelStatus);
            Assert.AreEqual(true, panelOne.PanelIsActive);
            Assert.AreNotEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(true, panelOne.BehindPanelIsActive);
            Assert.AreNotEqual("", panelOne.BlurStatus);
            Assert.AreEqual(true, panelOne.BlurIsActive);
            Assert.AreNotEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(true, panelOne.PanelButtonIsActive);

            Assert.AreNotEqual("", panelEight.PanelStatus);
            Assert.AreEqual(true, panelEight.PanelIsActive);
            Assert.AreNotEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(true, panelEight.BehindPanelIsActive);
            Assert.AreNotEqual("", panelEight.BlurStatus);
            Assert.AreEqual(true, panelEight.BlurIsActive);
            Assert.AreNotEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(true, panelEight.PanelButtonIsActive);

            // ...when a user clicks an anchor element with the below method added in its @onclick event handler...
            uIService.UpdatePanelsWhenNavigating(1);

            // ...then all panels are closed, no matter if the panel is cooperative or indendent.
            Assert.AreEqual("", panelZero.PanelStatus);
            Assert.AreEqual(false, panelZero.PanelIsActive);
            Assert.AreEqual("", panelZero.BehindPanelStatus);
            Assert.AreEqual(false, panelZero.BehindPanelIsActive);
            Assert.AreEqual("", panelZero.BlurStatus);
            Assert.AreEqual(false, panelZero.BlurIsActive);
            Assert.AreEqual("", panelZero.PanelButtonStatus);
            Assert.AreEqual(false, panelZero.PanelButtonIsActive);

            Assert.AreEqual("", panelOne.PanelStatus);
            Assert.AreEqual(false, panelOne.PanelIsActive);
            Assert.AreEqual("", panelOne.BehindPanelStatus);
            Assert.AreEqual(false, panelOne.BehindPanelIsActive);
            Assert.AreEqual("", panelOne.BlurStatus);
            Assert.AreEqual(false, panelOne.BlurIsActive);
            Assert.AreEqual("", panelOne.PanelButtonStatus);
            Assert.AreEqual(false, panelOne.PanelButtonIsActive);

            Assert.AreEqual("", panelEight.PanelStatus);
            Assert.AreEqual(false, panelEight.PanelIsActive);
            Assert.AreEqual("", panelEight.BehindPanelStatus);
            Assert.AreEqual(false, panelEight.BehindPanelIsActive);
            Assert.AreEqual("", panelEight.BlurStatus);
            Assert.AreEqual(false, panelEight.BlurIsActive);
            Assert.AreEqual("", panelEight.PanelButtonStatus);
            Assert.AreEqual(false, panelEight.PanelButtonIsActive);
        }

        [TestMethod]
        public void TestClickingALinkToANonSectionedpageUpdatesNavPanelGroupsLocationPanel()
        {
            // Given the location panel ID of the nav panel group...
            Assert.AreEqual(4, uIService.PanelGroups[0].LocationPanelId);

            // ...when a user clicks a link with the method below implemented in the link element
            // and when the page destination is diffferent than the starting page location...
            uIService.UpdatePanelsWhenNavigating(3);  // starting location is sectionedPageId = 2;

            // ...then the nav panel group's location panel ID is updated to the destination page, highlighting that page's panel button.
            Assert.AreEqual(3, uIService.PanelGroups[0].LocationPanelId);
        }
    }
}
