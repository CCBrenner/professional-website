namespace ProfessionalWebsite.Client.Classes.Mgmt.DataTables
{
    public class PanelsTable
    {
        public PanelsTable()
        {
            Panels = new List<Panel>()
            {
                new Panel(0,   // Global Animations panel
                    panelActiveStatusClassName: "anim-display",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel"
                ),
                new Panel(1),  // BeeHive settings
                new Panel(2,   // "projects" page
                    panelGroupId: 0,
                    panelActiveStatusClassName: "panel-visible",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel",
                    panelButtonClassName: "highlight-button"
                ),
                new Panel(3,   // "knowhow" page
                    panelGroupId: 0,
                    panelActiveStatusClassName: "panel-visible",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel",
                    panelButtonClassName: "highlight-button"
                ),
                new Panel(4,   // "collyn" page
                    panelGroupId: 0,
                    panelActiveStatusClassName: "panel-visible",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel",
                    panelButtonClassName: "highlight-button"
                ),
                new Panel(5,   // "invent" page
                    panelGroupId: 0,
                    panelActiveStatusClassName: "panel-visible",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel",
                    panelButtonClassName: "highlight-button"
                ),
                new Panel(6,   // "articles" page
                    panelGroupId: 0,
                    panelActiveStatusClassName: "panel-visible",
                    blurStatusClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel",
                    panelButtonClassName: "highlight-button"
                ),
                new Panel(7,   // LayoutControls
                    cannotBeActiveWhileOtherPanelsAreActive: false,
                    panelActiveStatusClassName: "layout-controls-on",
                    blurStatusClassName: "n0-cla$$_name",
                    behindPanelStatusClassName: "n0-cla$$_name",
                    panelButtonClassName: "n0-cla$$_name"
                ),
                new Panel(8,   // Discontinue button (for animations)
                    cannotBeActiveWhileOtherPanelsAreActive: false,
                    panelActiveStatusClassName: "discontinue-button-on",
                    blurStatusClassName: "n0-cla$$_name",
                    behindPanelStatusClassName: "n0-cla$$_name",
                    panelButtonClassName: "n0-cla$$_name"
                ),
            };

            PanelsDict = new Dictionary<int, Panel>();
            foreach (Panel panel in Panels)
                PanelsDict.Add(panel.Id, panel);
        }

        public List<Panel> Panels { get; private set; }
        public Dictionary<int, Panel> PanelsDict { get; private set; }
    }
}
