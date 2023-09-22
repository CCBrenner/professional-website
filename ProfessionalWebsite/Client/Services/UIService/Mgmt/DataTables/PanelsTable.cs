namespace ProfessionalWebsite.Client.Services.UIService.Mgmt.DataTables;

public sealed class PanelsTable
{
    private List<Panel> panels = new()
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

    public static List<Panel> GetPanels()
    {
        PanelsTable panelsTable = new();
        return panelsTable.panels;
    }

    public static Dictionary<int, Panel> GetPanelsDict()
    {
        PanelsTable panelsTable = new();
        Dictionary<int, Panel> panelsDict = new Dictionary<int, Panel>();

        foreach (Panel panel in panelsTable.panels)
            panelsDict.Add(panel.Id, panel);

        return panelsDict;
    }
}
