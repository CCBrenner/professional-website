﻿namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class PanelsTable
    {
        private PanelsTable() { }
        private static PanelsTable? instance;
        private static object instanceLock = new object();
        public static PanelsTable Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PanelsTable();
                    return instance;
                }
            }
        }
        public List<Panel> Panels { get; private set; } = new List<Panel>()
        {
            new Panel(0,    // [0] Global Animations panel
                panelActiveStatusClassName: "anim-display",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel"
            ),
            new Panel(1),  // [1] BeeHive settings
            new Panel(2,    // [2] "projects" page
                panelGroupId: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(3,    // [3] "knowhow" page
                panelGroupId: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(4,    // [4] "collyn" page
                panelGroupId: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(5,    // [5] "invent" page
                panelGroupId: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(6,    // [6] "articles" page
                panelGroupId: 0,
                panelActiveStatusClassName: "panel-visible",
                blurStatusClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel",
                panelButtonClassName: "highlight-button"
            ),
            new Panel(7,    // [7] LayoutControls
                cannotBeActiveWhileOtherPanelsAreActive: false,
                panelActiveStatusClassName: "layout-controls-on",
                blurStatusClassName: "n0-cla$$_name",
                behindPanelStatusClassName: "n0-cla$$_name",
                panelButtonClassName: "n0-cla$$_name"
            ),
            new Panel(8,    // [8] Discontinue button (for animations)
                cannotBeActiveWhileOtherPanelsAreActive: false,
                panelActiveStatusClassName: "discontinue-button-on",
                blurStatusClassName: "n0-cla$$_name",
                behindPanelStatusClassName: "n0-cla$$_name",
                panelButtonClassName: "n0-cla$$_name"
            ),
        };
    }
}