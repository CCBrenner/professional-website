namespace ProfessionalWebsite.Client.Services.UI;

public class Sections
{
    private Sections(List<Section> sections, List<SectionedPage> sectionedPages)
    {
        Pages = new();
        foreach (SectionedPage page in sectionedPages)
            Pages.Add(page.Id, page);
        foreach (var section in sections)
            Pages[section.PageId].Sections.Add(section.Id, section);

        Dictionary = new();
        foreach (SectionedPage page in Pages.Values)
            foreach (KeyValuePair<int, Section> sectionPair in page.Sections)
                Dictionary.Add(sectionPair.Key, sectionPair.Value);

    }
    public Dictionary<int, SectionedPage> Pages { get; set; }
    public Dictionary<int, Section> Dictionary { get; set; }
    public static Sections Create(List<Section> sections, List<SectionedPage> sectionedPages) 
        => new Sections(sections, sectionedPages);
    public string IsOpenCSS(int sectionId) => Dictionary[sectionId].IsOpen ? "" : "collapsed-header";  // for section header

    public bool IsCurrentPromo(int sectionId)
    {
        int pageId = Dictionary[sectionId].PageId;
        return Pages[pageId].IsCurrentPromo(sectionId);
    }
    public bool ASectionIsCurrentlyPromo(int pageId) => Pages[pageId].ASectionIsCurrentlyPromo();
    public bool AllSectionsAreOpen(int pageId) => Pages[pageId].AllSectionsAreOpen();
    public void ToggleAllSections(int pageId) => Pages[pageId].ToggleAllSections();
    public void PromoteSection(int sectionId)
    {
        int pageId = Dictionary[sectionId].PageId;
        Pages[pageId].PromoteSection(sectionId);
    }
    public void OpenAllSections(int idOfsectionedPageBeingLoaded) 
        => Pages[idOfsectionedPageBeingLoaded].OpenAllSections();
    public int GetLocationPanelGroupId(int sectionId)
    {
        int pageId = Dictionary[sectionId].PageId;
        return Pages[pageId].LocationPanelGroupId;
    }
}
