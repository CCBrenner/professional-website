using ProfessionalWebsite.Client.Classes.Mgmt;
using ProfessionalWebsite.Client.Classes.Mgmt.DataTables;
using ProfessionalWebsite.Client.Services.Contracts;

namespace ProfessionalWebsite.Client.Services
{
    public class UIService : IUIService
    {
        public UIService()
        {
            var AnimationsIsContinuousIntializations = new AnimationsTable().IsContinuous;
            AnimMgmt = new AnimMgmt(AnimationsIsContinuousIntializations);

            NavMgmt = new NavMgmt();

            var PanelGroupsDictionary = new PanelGroupsTable().PanelGroupsDict;
            var PanelsDictionary = new PanelsTable().PanelsDict;
            PanelMgmt = new PanelMgmt(PanelGroupsDictionary, PanelsDictionary);

            var SectionedPagesDictionary = new SectionedPagesTable().SectionedPagesDict;
            var SectionsDictionary = new SectionsTable().SectionsDict;
            SectionMgmt = new SectionMgmt(SectionedPagesDictionary, SectionsDictionary);
        }

        public AnimMgmt AnimMgmt { get; private set; }
        public NavMgmt NavMgmt { get; private set; }
        public PanelMgmt PanelMgmt { get; private set; }
        public SectionMgmt SectionMgmt { get; private set; }

        /// <summary>
        /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
        /// </summary>
        /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
        public void PlayAnimation(int animationIndex) => 
            AnimMgmt.PlayAnimation(animationIndex, PanelMgmt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
        /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
        public void PlayAnimation(int animationIndex, bool isContinuous) =>
            AnimMgmt.PlayAnimation(animationIndex, isContinuous, PanelMgmt);

        /// <summary>
        /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
        /// </summary>
        public void DiscontinueAnimation() =>
            AnimMgmt.DiscontinueAnimation(PanelMgmt);

        /// <summary>
        /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
        /// </summary>
        /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume NavMgmt to update themselves because of a state change in NavMgmt. Components must subscribe to the event to receive update commands.</param>
        public void NavigateToSection(int sectionId, bool triggersOnPanelMgmtUpdated = true) =>
            NavMgmt.NavigateToSection(sectionId, PanelMgmt, SectionMgmt, triggersOnPanelMgmtUpdated);

        /// <summary>
        /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
        /// </summary>
        /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
        public void NavigateToHardCodedPage(int panelId) =>
            NavMgmt.NavigateToHardCodedPage(panelId, PanelMgmt);

        /// <summary>
        /// Sets all panels to their default configurations (usually an "off" state).
        /// </summary>
        /// <param name="setActivePanelGroupToLocationPanel">Sets the button highlight of the current location when all panels in the panel group have been deactivated.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default to "true", this tells components that consume PanelMgmt properties to update (based on changes to state). Component must subscribe to the event to receive update commands.</param>
        /// <param name="includeIndependentPanels">Independent panels exist outside of the deactivation logic by default. If for whatever reason they should also be deactivated, then this can be set to "true".</param>
        public void DeactivateAllPanels(
            bool setActivePanelGroupToLocationPanel,
            bool triggersOnPanelMgmtUpdated = true,
            bool includeIndependentPanels = false
        ) =>
            PanelMgmt.DeactivateAllPanels(
            setActivePanelGroupToLocationPanel,
            triggersOnPanelMgmtUpdated,
            includeIndependentPanels
        );

        /// <summary>
        /// Deactivates a panel based on the panel's ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be deactivated.</param>
        public void DeactivatePanel(int selectedPanelId) =>
            PanelMgmt.DeactivatePanel(selectedPanelId);

        /// <summary>
        /// Activates a panel based on the panel's ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be activated.</param>
        /// <returns></returns>
        public Panel ActivatePanel(int selectedPanelId) =>
            PanelMgmt.ActivatePanel(selectedPanelId);

        /// <summary>
        /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
        /// <returns></returns>
        public Panel? TogglePanel(int selectedPanelId) =>
            PanelMgmt.TogglePanel(selectedPanelId);

        /// <summary>
        /// Sets the location panel of a panel group using only the ID of a given panel. If the panel is not part of a panel group, then nothing happens. 
        /// </summary>
        /// <param name="panelId">ID of panel to be made the location panel of the panel's panel group.</param>
        public void UpdateGroupLocationPanel(int panelId) =>
            PanelMgmt.UpdateGroupLocationPanel(panelId);

        /// <summary>
        /// When navigating (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
        /// </summary>
        /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume PanelMgmt to update. Component must subscribe to the event to receive update commands from PanelMgmt.</param>
        public void UpdatePanelsWhenNavigating(int panelId, bool triggersOnPanelMgmtUpdated = true) =>
            PanelMgmt.UpdatePanelsWhenNavigating(panelId, triggersOnPanelMgmtUpdated);

        /// <summary>
        /// Collapses all sections and promotes one section to the top of the sectioned page.
        /// </summary>
        /// <param name="sectionId">ID of the section that is being promoted/which has been selected.</param>
        public void CollapseAllShowOne(int sectionId) =>
            SectionMgmt.CollapseAllShowOne(sectionId);

        /// <summary>
        /// Collapses/Expands section based on section ID.
        /// </summary>
        /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
        public void ToggleCollapseSingle(int sectionId) =>
            SectionMgmt.ToggleCollapseSingle(sectionId);

        /// <summary>
        /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
        /// </summary>
        /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
        public void ToggleAllSections(int pageId) =>
            SectionMgmt.ToggleAllSections(pageId);

        /// <summary>
        /// A logic check used by sections in their housing component to determine whether or not they should be showing or not. Works in conjunction with dopelganger section header which, when a section is promoted, all other sections disappear and their dopelgangers, which are located beneath all actual sections, become visible. This creates an illusion of the promoted section being brought to the top of the sectioned page.
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns>Boolean value stating whether a section is supposed to be visible or not.</returns>
        public bool SectionIsVisible(int sectionId) =>
            SectionMgmt.SectionIsVisible(sectionId);

        /// <summary>
        /// Demotes all other sections and makes specified section the promo section.
        /// </summary>
        /// <param name="sectionId">ID of section to be made promo section.</param>
        public void PromoteSection(int sectionId) =>
            SectionMgmt.PromoteSection(sectionId);

        /// <summary>
        /// Returns the ID of the location panel of a sectioned page of the specified section using the section's ID.
        /// </summary>
        /// <param name="sectionId">ID of section used to get the sectioned page's location panel's ID</param>
        /// <returns></returns>
        public int GetLocationPanelGroupId(int sectionId) =>
            SectionMgmt.GetLocationPanelGroupId(sectionId);
    }
}
