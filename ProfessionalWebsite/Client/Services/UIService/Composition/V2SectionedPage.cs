


namespace ProfessionalWebsite.Client.Services.UI;

public class V2SectionedPage
{
    public V2SectionedPage()
    {
        Sections = InitSections(new()
        {
            new V2Section(5037, "placeholder5037"),
            new V2Section(5038, "placeholder5038"),
        });
    }
    public Dictionary<int, V2Section> Sections { get; set; }
    private Dictionary<int, V2Section> InitSections(List<V2Section> sections)
    {
        Dictionary<int, V2Section> dictionary = new();
        foreach (var section in sections)
            dictionary.Add(section.Id, section);
        return dictionary;
    }
    public void ToggleSection(int sectionId) => Sections[sectionId].Toggle();
    public void OpenSection(int sectionId) => Sections[sectionId].Open();
    public void OpenAllSections()
    {
        foreach (V2Section section in Sections.Values)
            section.Open();
    }
    public void CloseSection(int sectionId) => Sections[sectionId].Close();
    public void CloseAllSections()
    {
        foreach (V2Section section in Sections.Values)
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
    public string IsOpenCSS(int sectionId) => Sections[sectionId].IsOpen ? "" : "collapsed-header";  // for section header
    public bool AllSectionsAreOpen()
    {
        foreach (V2Section section in Sections.Values)
            if (!section.IsOpen) return false;
        return true;
    }
    public void ToggleAllSections()
    {
        // - if some open but not all, then open all
        // - if all closed, open all
        // - if all open, close all
        int openSectionsCount = 0;
        foreach (V2Section section in Sections.Values) if (section.IsOpen) openSectionsCount++;
        if (openSectionsCount == Sections.Count) CloseAllSections();
        else OpenAllSections();
    }
}
