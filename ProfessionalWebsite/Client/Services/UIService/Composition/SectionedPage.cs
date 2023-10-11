namespace ProfessionalWebsite.Client.Services.UI;

public partial class SectionedPage
{
    public SectionedPage(int id, int locationPanelGroupId, string pagePath)
    {
        Id = id;
        LocationPanelGroupId = locationPanelGroupId;
        PagePath = pagePath;
        ASectionIsCurrentlyPromo = false;
        SectionsStatus = SectionsStatus.AllAreOpen;
        SectionsExpanded = Sections.Count;
    }

    public readonly int Id;
    public readonly int LocationPanelGroupId;
    public string PagePath { get; private set; }
    public Dictionary<int, Section> Sections { get; set; } = new Dictionary<int, Section>();

    public bool ASectionIsCurrentlyPromo { get; set; }
    public SectionsStatus SectionsStatus { get; set; }
    public int SectionsExpanded { get; private set; } = 0;
}
