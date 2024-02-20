namespace ProfessionalWebsite.Client.Services.UI;

public sealed class PanelsTable
{
    public static Dictionary<int, Panel> GetPanelsDict()
    {
        Dictionary<int, Panel> panelsDict = new();

        foreach (Panel panel in GetPanels())
            panelsDict.Add(panel.Id, panel);

        return panelsDict;
    }
    public static List<Panel> GetPanels() => new()
    {
        Panel.Create(0,   // Global Animations panel
            panelActiveStatusClassName: "anim-display",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel"
        ),
        Panel.Create(1),  // BeeHive settings
        Panel.Create(2,   // "projects" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.Create(3,   // "knowhow" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.Create(4,   // "collyn" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.Create(5,   // "invent" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.Create(6,   // "articles" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.Create(7,   // LayoutControls
            cannotBeActiveWhileOtherPanelsAreActive: false,
            panelActiveStatusClassName: "layout-controls-on",
            blurStatusClassName: "n0-cla$$_name",
            behindPanelStatusClassName: "n0-cla$$_name",
            panelButtonClassName: "n0-cla$$_name"
        ),
        Panel.Create(8,   // Discontinue button (for animations)
            cannotBeActiveWhileOtherPanelsAreActive: false,
            panelActiveStatusClassName: "discontinue-button-on",
            blurStatusClassName: "n0-cla$$_name",
            behindPanelStatusClassName: "n0-cla$$_name",
            panelButtonClassName: "n0-cla$$_name"
        ),
    };

}
