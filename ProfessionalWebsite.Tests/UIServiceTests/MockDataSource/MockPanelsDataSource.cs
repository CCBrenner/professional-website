using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockPanelsDataSource
{
    public static Dictionary<int, Panel> GetDictionary()
    {
        Dictionary<int, Panel> panelsDict = new();

        foreach (Panel panel in GetPanels())
            panelsDict.Add(panel.Id, panel);

        return panelsDict;
    }
    public static List<Panel> GetPanels() => new()
    {
        Panel.CreateCooperativePanel(0,   // Global Animations panel
            panelActiveStatusClassName: "anim-display",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel"
        ),
        Panel.CreateIndependentPanel(1),  // BeeHive settings
        Panel.CreateCooperativePanel(2,   // "projects" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.CreateCooperativePanel(3,   // "knowhow" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.CreateCooperativePanel(4,   // "collyn" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.CreateCooperativePanel(5,   // "invent" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.CreateCooperativePanel(6,   // "articles" page
            panelGroupId: 0,
            panelActiveStatusClassName: "panel-visible",
            blurStatusClassName: "content-blur",
            behindPanelStatusClassName: "button-on-show-behind-panel",
            panelButtonClassName: "highlight-button"
        ),
        Panel.CreateIndependentPanel(7,   // LayoutControls
            panelActiveStatusClassName: "layout-controls-on",
            blurStatusClassName: "n0-cla$$_name",
            behindPanelStatusClassName: "n0-cla$$_name",
            panelButtonClassName: "n0-cla$$_name"
        ),
        Panel.CreateIndependentPanel(8,   // Discontinue button (for animations)
            panelActiveStatusClassName: "discontinue-button-on",
            blurStatusClassName: "n0-cla$$_name",
            behindPanelStatusClassName: "n0-cla$$_name",
            panelButtonClassName: "n0-cla$$_name"
        ),
    };
}
