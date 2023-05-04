using System.Text.RegularExpressions;
using System.Linq;

namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class PanelMgmt
    {
        private PanelMgmt()
        {
            PanelGroups = PanelGroupsTable.Instance.PanelGroups;
            Panels = PanelsTable.Instance.Panels;

            InitializeGroups();
        }

        private static PanelMgmt instance;
        private static object instanceLock = new object();

        public static PanelMgmt Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PanelMgmt();
                    return instance;
                }
            }
        }

        public event Action<string> OnPanelMgmtUpdated;

        public List<PanelGroup> PanelGroups { get; private set; }
        public List<Panel> Panels { get; private set; }

        public void DeactivateAllPanels()
        {
            foreach (Panel panel in Panels)
            {
                if (panel.CannotBeActiveWhileOtherPanelsAreActive)
                {
                    panel.Deactivate();
                    ActivateLocationButtonsOfGroups();
                }
            }
            RaiseEventOnPanelMgmtUpdated();
        }
        public void DeactivateAllPanels(bool setActivePanelGroupToLocationPanel, bool triggersOnPanelMgmtUpdated = true, bool includeIndependentPanels = false)
        {
            foreach (Panel panel in Panels)
            {
                if (panel.CannotBeActiveWhileOtherPanelsAreActive || includeIndependentPanels)
                {
                    panel.Deactivate();
                    if (setActivePanelGroupToLocationPanel)
                        ActivateLocationButtonsOfGroups();
                }
            }
            if (triggersOnPanelMgmtUpdated)
                RaiseEventOnPanelMgmtUpdated();
        }
        public void DeactivatePanel(int selectedPanelId)
        {
            Panels[selectedPanelId].Deactivate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public async Task<Panel> ActivatePanel(int selectedPanelId)
        {
            DeactivateAllPanels(false);
            ActivateLocationButtonsOfGroups(selectedPanelId);
            await Panels[selectedPanelId].Activate();
            RaiseEventOnPanelMgmtUpdated();
            return Panels[selectedPanelId];
        }
        public Panel TogglePanel(int selectedPanelId)
        {
            if (Panels[selectedPanelId].PanelStatus == "")
            {
                DeactivateAllPanels(false);
                ActivateLocationButtonsOfGroups(selectedPanelId);
                Panels[selectedPanelId].Activate();
            }
            else
            {
                DeactivateAllPanels(true);
                DeactivatePanel(selectedPanelId);
            }
            RaiseEventOnPanelMgmtUpdated();
            return Panels[selectedPanelId];
        }
        public Panel UpdateGroupLocationPanel(int panelId)
        {
            try
            {
                int pgId = Panels[panelId].PanelGroupId;
                int lpId = PanelGroups[pgId].LocationPanelId;
                Panels[lpId].Deactivate();
                PanelGroups[pgId].LocationPanelId = panelId;
                Panels[panelId].ActivateButton();
                RaiseEventOnPanelMgmtUpdated();
            }
            catch (ArgumentOutOfRangeException aoorEx)
            {
                Console.WriteLine($"{aoorEx.Message} - origin method: PanelMgmt.UpdateGroupLocationPanel()");
            }
            return Panels[panelId];

        }
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }
        private Panel? ActivateLocationButtonsOfGroups(int idOfPanelBeingActivated)
        {
            if (AllCooperativePanelsAreDeactivated())
            {
                int groupOfActivatedPanel = -1;

                foreach (PanelGroup panelGroup in PanelGroups)
                    foreach (int panelId in panelGroup.Panels)
                        if (idOfPanelBeingActivated == panelId)
                            groupOfActivatedPanel = panelGroup.Id;


                foreach (PanelGroup panelGroup in PanelGroups)
                {
                    if (groupOfActivatedPanel == -1)
                    {
                        int panelId = panelGroup.LocationPanelId;
                        return Panels[panelId].ActivateButton();
                    }
                }
            }
            return null;
        }
        private List<Panel> ActivateLocationButtonsOfGroups()
        {
            List<Panel> locationPanelsActivated = new List<Panel>();
            if (AllCooperativePanelsAreDeactivated())
            {
                foreach (PanelGroup panelGroup in PanelGroups)
                {
                    int panelId = panelGroup.LocationPanelId;
                    Panel highlightedPanel = Panels[panelId].ActivateButton();
                    locationPanelsActivated.Add(highlightedPanel);
                }
            }
            return locationPanelsActivated;
        }
        private bool AllCooperativePanelsAreDeactivated()
        {
            foreach (Panel panel in Panels)
                if (panel.PanelStatus != "" && panel.CannotBeActiveWhileOtherPanelsAreActive)
                    return false;
            return true;
        }
        private void InitializeGroups()
        {
            SetAllPanelGroupPanelReferences();
            ActivateLocationButtonsOfGroups();
        }
        private void SetAllPanelGroupPanelReferences()
        {
            for (int i = 0; i < PanelGroups.Count(); i++)
                foreach (Panel panel in Panels)
                    if (panel.PanelGroupId == PanelGroups[i].Id)
                        PanelGroups[i].Panels.Add(panel.Id);
        }

        /* DataTable Queries */
        public List<Panel> GetPanelsOfGroup(int groupId)
        {
            List<Panel> panelsOfGroup = new List<Panel>();

            foreach (Panel panel in Panels)
                if (panel.PanelGroupId == groupId)
                    panelsOfGroup.Add(panel);

            return panelsOfGroup;
        }
        public Panel? GetPanel(int id) =>
            Panels
            .Where(panel => panel.Id == id)
            .Select(panel => panel)
            .FirstOrDefault();
    }
}
