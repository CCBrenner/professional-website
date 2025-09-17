


namespace ProfessionalWebsite.Client.Services.UI;

public class SectionedPage
{
    private SectionedPage(int id, int locationPanelGroupId)
    {
        Id = id;
        LocationPanelGroupId = locationPanelGroupId;
        Sections = new();
    }
    public int Id { get; private set; }
    public Dictionary<int, Section> Sections { get; set; }
    public int LocationPanelGroupId { get; private set; }
    public static SectionedPage Create(int id, int locationPanelGroupId) 
        => new SectionedPage(id, locationPanelGroupId);
    public void ToggleSection(int sectionId) => Sections[sectionId].Toggle();
    public void OpenSection(int sectionId) => Sections[sectionId].Open();
    public void OpenAllSections()
    {
        foreach (Section section in Sections.Values)
            section.Open();
    }
    public void CloseSection(int sectionId) => Sections[sectionId].Close();
    public void CloseAllSections()
    {
        foreach (Section section in Sections.Values)
            section.Close();
    }
    public void PromoteSection(int sectionId)
    {
        CloseAllSections();
        Sections[sectionId].Open();
    }
    public bool ASectionIsCurrentlyPromo()
    {
        int counter = 0;
        foreach (var section in Sections)
            if (section.Value.IsOpen)
                counter++;
        return counter == 1;
    }
    public bool IsCurrentPromo(int sectionId)
    {
        if (ASectionIsCurrentlyPromo())
            return Sections[sectionId].IsOpen;
        return false;
    }
    public bool AllSectionsAreOpen()
    {
        foreach (Section section in Sections.Values)
            if (!section.IsOpen) return false;
        return true;
    }
    public void ToggleAllSections()
    {
        // - if some open but not all, then open all
        // - if all closed, open all
        // - if all open, close all
        int openSectionsCount = 0;
        foreach (Section section in Sections.Values) if (section.IsOpen) openSectionsCount++;
        if (openSectionsCount == Sections.Count) CloseAllSections();
        else OpenAllSections();
    }
}
