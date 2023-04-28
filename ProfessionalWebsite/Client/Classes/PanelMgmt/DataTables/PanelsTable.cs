namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class PanelsTable
    {
        private PanelsTable() { }
        private static PanelsTable instance;
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
