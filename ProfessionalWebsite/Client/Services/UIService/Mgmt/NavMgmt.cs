namespace ProfessionalWebsite.Client.Services.UIService.Mgmt
{
    public class NavMgmt
    {
        public event Action<string> OnNavMgmtUpdated;

        /*
            Definitions:
                - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
                - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
        */

        /// <summary>
        /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
        /// </summary>
        /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume NavMgmt to update themselves because of a state change in NavMgmt. Components must subscribe to the event to receive update commands.</param>
        public void NavigateToSection(int sectionId, PanelMgmt panelMgmt, SectionMgmt sectionMgmt, bool triggersOnPanelMgmtUpdated = true)
        {
            panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            try
            {
                sectionMgmt.CollapseAllShowOne(sectionId);
                int locationPanelGroupId = sectionMgmt.GetLocationPanelGroupId(sectionId);
                if (locationPanelGroupId != -1)
                    panelMgmt.UpdateGroupLocationPanel(locationPanelGroupId);
            }
            catch (NullReferenceException nrEx)
            {
                Console.WriteLine(nrEx.Message + nrEx.StackTrace);
            }
        }

        /// <summary>
        /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
        /// </summary>
        /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
        public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, PanelMgmt panelMgmt)
        {
            panelMgmt.UpdateGroupLocationPanel(navGroupPanelId);
            panelMgmt.ActivatePanel(hardcodedPanelId);
        }

        /// <summary>
        /// Updates the component that consumes it when a method in the NavMgmt class that consumes this method invokes/signals that a change to the state of it has occurred.
        /// </summary>
        private void RaiseEventOnNavMgmtUpdated()
        {
            if (OnNavMgmtUpdated != null)
                OnNavMgmtUpdated?.Invoke("");
            RaiseEventOnNavMgmtUpdated();
        }
    }
}
