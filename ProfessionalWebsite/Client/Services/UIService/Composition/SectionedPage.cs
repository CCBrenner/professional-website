namespace ProfessionalWebsite.Client.Services.UI;

public partial class SectionedPage
{
    private SectionedPage(int id, int locationPanelGroupId, string pagePath)
    {
        Id = id;
        LocationPanelGroupId = locationPanelGroupId;
        PagePath = pagePath;
        ASectionIsCurrentlyPromo = false;
        Status = SectionedPageStatus.AllAreOpen;
        SectionsExpanded = Sections.Count;
    }

    public readonly int Id;
    public readonly int LocationPanelGroupId;
    public string PagePath { get; private set; }
    public Dictionary<int, Section> Sections { get; set; } = new();

    public bool ASectionIsCurrentlyPromo { get; set; }
    public SectionedPageStatus Status { get; set; }
    public int SectionsExpanded { get; private set; } = 0;
    public static SectionedPage Create(int id, int locationPanelGroupId, string pagePath) =>
        new(id, locationPanelGroupId, pagePath);
    public void Toggle()
    {
        try
        {
            UpdateStatus();  // this change doesn't last the full length of this method

            DemoteSections();

            if (Status == SectionedPageStatus.AllAreOpen)
            {
                foreach (var section in Sections.Values)
                    section.Collapse();
                Status = SectionedPageStatus.AllAreCollapsed;
            }
            else
            {
                foreach (var section in Sections.Values)
                    section.Expand();
                Status = SectionedPageStatus.AllAreOpen;
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
    private void DemoteSections()
    {
        foreach (var section in Sections.Values)
            section.Demote();
        ASectionIsCurrentlyPromo = false;
    }
    private void UpdateStatus()
    {
        int openSections = 0;

        // get open section count:
        foreach (Section sec in Sections.Values)
            if (!sec.IsCollapsed)
                openSections++;

        // set status based on count of expanded sections
        if (openSections == 0)
            Status = SectionedPageStatus.AllAreCollapsed;
        else if (openSections == Sections.Count)
            Status = SectionedPageStatus.AllAreOpen;
        else
            Status = SectionedPageStatus.AtLeastOneIsOpen;
    }
    public void AddSectionReference(Section section)
    {
        Sections.Add(section.Id, section);
    }
}
