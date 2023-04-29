using ProfessionalWebsite.Client.Classes.PanelMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalWebsite.Tests.PanelMgmtTests
{
    public class MockPanelMgmt
    {
        public MockPanelMgmt()
        {
            MockPanelGroupsTable panelGroupsTable = new MockPanelGroupsTable();
            MockPanelsTable panelTable = new MockPanelsTable();

            PanelGroups = panelGroupsTable.PanelGroups;
            Panels = panelTable.Panels;

            InitializeGroups();
        }

        public List<PanelGroup> PanelGroups { get; private set; }
        public List<Panel> Panels { get; private set; }

        private void InitializeGroups()
        {
            // set starting location button of each group to "active"
            foreach (PanelGroup panelGroup in PanelGroups)
            {
                List<Panel> panelsOfGroup = GetPanelsOfGroup(panelGroup.Id);
                if (panelsOfGroup != null && panelsOfGroup.Count() > panelGroup.StartingLocation)
                    panelsOfGroup[panelGroup.StartingLocation].ActivateButton();
            }
        }
        public List<Panel> GetPanelsOfGroup(int groupId)
        {
            List<Panel> panelsOfGroup = new List<Panel>();

            foreach (Panel panel in Panels)
                if (panel.PanelGroupId == groupId)
                    panelsOfGroup.Add(panel);

            return panelsOfGroup;
        }
    }
    public class MockPanelGroupsTable
    {
        public List<PanelGroup> PanelGroups { get; set; } = new List<PanelGroup>()
        {
            new PanelGroup(0, 2),  // [0] NavMgmt panels
        };
    }
    public class MockPanelsTable
    {
        public List<Panel> Panels { get; private set; } = new List<Panel>()
        {
            new Panel(    // [0] Global Animations for Main
                panelActiveStatusClassName: "anim-display",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel"
            ),
            new Panel(),  // [1] BeeHive settings
            new Panel(    // [2] "projects" page
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(    // [3] "knowhow" page
                panelGroupid: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(    // [4] "collyn" page
                panelGroupid: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(    // [5] "invent" page
                panelGroupid: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(    // [6] "articles" page
                panelGroupid: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
        };
    }

}
