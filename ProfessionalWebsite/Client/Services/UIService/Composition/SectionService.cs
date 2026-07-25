namespace ProfessionalWebsite.Client.Services.UI;

public class SectionService
{
    private SectionService()
    {
        Pages = new();
        foreach (SectionedPage page in SectionedPagesTable.GetList())
            Pages.Add(page.Id, page);
        foreach (var section in SectionsTable.GetList())
            Pages[section.PageId].Sections.Add(section.Id, section);

        Dictionary = new();
        foreach (SectionedPage page in Pages.Values)
            foreach (KeyValuePair<int, Section> sectionPair in page.Sections)
                Dictionary.Add(sectionPair.Key, sectionPair.Value);

    }
    public static SectionService Create()
    {
        return new();
    }
    public Dictionary<int, SectionedPage> Pages { get; set; }
    public Dictionary<int, Section> Dictionary { get; set; }
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
