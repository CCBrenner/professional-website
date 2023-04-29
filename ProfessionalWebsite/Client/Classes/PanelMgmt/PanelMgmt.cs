using System.Text.RegularExpressions;
using System.Linq;

namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class PanelMgmt
    {
        private PanelMgmt()
        {
            PanelGroupsTable panelGroupsTable = PanelGroupsTable.Instance;
            PanelsTable panelTable = PanelsTable.Instance;

            PanelGroups = panelGroupsTable.PanelGroups;
            Panels = panelTable.Panels;

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

        public void DeactivateAllPanels(bool locationButtonsAreHighlighted)
        {
            foreach (Panel panel in Panels)
            {
                panel.Deactivate();
                if (locationButtonsAreHighlighted)
                    ActivateLocationButtonsOfGroups();
            }
            RaiseEventOnPanelMgmtUpdated();
        }
        public void DeactivatePanel(int selectedPanelId)
        {
            Panels[selectedPanelId].Deactivate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ActivatePanel(int selectedPanelId)
        {
            DeactivateAllPanels(false);
            ActivateLocationButtonsOfGroups(selectedPanelId);
            Panels[selectedPanelId].Activate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public void TogglePanel(int selectedPanelId)
        {
            if (Panels[selectedPanelId].PanelStatus == "")
            {
                DeactivateAllPanels(false);
                ActivateLocationButtonsOfGroups(selectedPanelId);
                Panels[selectedPanelId].Activate();
            }
            else
            {
                Panels[selectedPanelId].TogglePanel();
                ActivateLocationButtonsOfGroups();
            }

            RaiseEventOnPanelMgmtUpdated();
        }
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }
        private void ActivateLocationButtonsOfGroups(int idOfPanelBeingActivated)
        {
            if (AllPanelsAreDeactivated())
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
                        Panels[panelId].ActivateButton();
                    }
                }
            }
        }
        private void ActivateLocationButtonsOfGroups()
        {
            if (AllPanelsAreDeactivated())
            {
                foreach (PanelGroup panelGroup in PanelGroups)
                {
                    int panelId = panelGroup.LocationPanelId;
                    Panels[panelId].ActivateButton();
                }
            }
        }
        private bool AllPanelsAreDeactivated()
        {
            bool panelsAreDeactivated = true;
            foreach (Panel panel in Panels)
            {
                if (panel.PanelStatus != "")
                {
                    panelsAreDeactivated = false;
                    break;
                }
            }
            return panelsAreDeactivated;
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
    }
}
