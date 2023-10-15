namespace ProfessionalWebsite.Client.Services.UI;

public class Panel
{
    private Panel(
        int id,
        int panelGroupId = -1,
        string panelActiveStatusClassName = "pm-panel-visible",
        string blurStatusClassName = "pm-content-blurred",
        string behindPanelStatusClassName = "pm-behindpanel-present",
        string panelButtonClassName = "pm-panelbutton-active",
        bool isCooperativePanel = true
    )
    {
        _panelActiveStatusClassName = panelActiveStatusClassName;
        _blurStatusClassName = blurStatusClassName;
        _behindPanelStatusClassName = behindPanelStatusClassName;
        _panelButtonClassName = panelButtonClassName;

        Id = id;
        PanelGroupId = panelGroupId;
        IsCooperativePanel = isCooperativePanel;

        PanelStatus = "";
        BlurStatus = "";
        BehindPanelStatus = "";
        PanelButtonStatus = "";
    }

    private string _panelActiveStatusClassName;
    private string _blurStatusClassName;
    private string _behindPanelStatusClassName;
    private string _panelButtonClassName;


    public readonly int Id;
    public int PanelGroupId { get; private set; }
    public PanelGroup PanelGroup { get; private set; }
    public bool IsCooperativePanel { get; private set; }

    public string PanelStatus { get; private set; }
    public bool PanelIsActive { get; private set; }
    public string BlurStatus { get; private set; }
    public bool BlurIsActive { get; private set; }
    public string BehindPanelStatus { get; private set; }
    public bool BehindPanelIsActive { get; private set; }
    public string PanelButtonStatus { get; private set; }
    public bool PanelButtonIsActive { get; private set; }

    public static Panel Create(
        int id,
        int panelGroupId = -1,
        string panelActiveStatusClassName = "pm-panel-visible",
        string blurStatusClassName = "pm-content-blurred",
        string behindPanelStatusClassName = "pm-behindpanel-present",
        string panelButtonClassName = "pm-panelbutton-active",
        bool isCooperativePanel = true
    ) => new(
        id,
        panelGroupId,
        panelActiveStatusClassName,
        blurStatusClassName,
        behindPanelStatusClassName,
        panelButtonClassName,
        isCooperativePanel
    );
    public void Activate()
    {
        PanelStatus = _panelActiveStatusClassName;
        BlurStatus = _blurStatusClassName;
        BehindPanelStatus = _behindPanelStatusClassName;
        PanelButtonStatus = _panelButtonClassName;
        PanelIsActive = true;
        BlurIsActive = true;
        BehindPanelIsActive = true;
        PanelButtonIsActive = true;
    }
    public void Deactivate()
    {
        PanelStatus = "";
        BlurStatus = "";
        BehindPanelStatus = "";
        PanelButtonStatus = "";
        PanelIsActive = false;
        BlurIsActive = false;
        BehindPanelIsActive = false;
        PanelButtonIsActive = false;
    }
    public void ActivateButton()
    {
        PanelButtonStatus = _panelButtonClassName;
        PanelButtonIsActive = true;
    }
    public void SetInstanceToGroupRelationship(List<PanelGroup> panelGroups)
    {
        try
        {
            PanelGroup = (from panelGroup in panelGroups
                          where panelGroup.Id == PanelGroupId
                          select panelGroup).FirstOrDefault();
        }
        catch (NullReferenceException nrEx)
        {
            Console.WriteLine($"Error: NullReferenceException - Could not establish Instance-Group relationship between Section and SectionedPage\n" +
                $"{nrEx.Message}\n{nrEx.StackTrace}");
        }
    }

    /*
    Examples of how CSS should be set up for effective transitions:
    
    // .pm-panel-visible requires defaults:
    visibility: hidden;
    opacity: 0;
    transition: visibility 0.2s, opacity 0.2s;

    .pm-panel-visible {
        visibility: visible;
        opacity: 1;
        transition: visibility 0.2s, opacity 0.2s;
    }

    // .pm-maincontent-blurred requires defaults:
    *none*

    .pm-maincontent-blurred {
        filter: blur(2.5px);
    }

    // .pm-behindpanel-present requires defaults:
    display: none;
    transition: background-color 0.2s;

    // .pm-behindpanel-present styles that should also be there (styles can differ):
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: var(--content-height);
    z-index: 3;

    .pm-behindpanel-present {
        display: block;
        background-color: rgba(0,0,60,0.06);
        transition: background-color 0.2s;
    }
    */
}
