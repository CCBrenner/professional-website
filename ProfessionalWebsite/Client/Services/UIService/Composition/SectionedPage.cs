namespace ProfessionalWebsite.Client.Services.UI;

public partial class SectionedPage
{
    private SectionedPage(string id, int locationPanelGroupId, string pagePath)
    {
        Id = id.ToLower();
        LocationPanelGroupId = locationPanelGroupId;
        PagePath = pagePath;
        ASectionIsCurrentlyPromo = false;
        SectionsStatus = SectionsStatus.AllAreOpen;
        SectionsExpanded = Sections.Count;
    }

    public readonly string Id;
    public readonly int LocationPanelGroupId;
    public string PagePath { get; private set; }
    public Dictionary<string, Section> Sections { get; set; } = new();

    public bool ASectionIsCurrentlyPromo { get; set; }
    public SectionsStatus SectionsStatus { get; set; }
    public int SectionsExpanded { get; private set; } = 0;
    public static SectionedPage Create(string id, int locationPanelGroupId, string pagePath) =>
        new(id, locationPanelGroupId, pagePath);
}
