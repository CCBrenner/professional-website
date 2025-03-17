

using static System.Collections.Specialized.BitVector32;

namespace ProfessionalWebsite.Client.Services.UI;

public class V2Sections
{
    private V2Sections(List<V2Section> sections, List<V2SectionedPage> sectionedPages)
    {
        Pages = new();
        foreach (V2SectionedPage page in sectionedPages)
            Pages.Add(page.Id, page);
        foreach (var section in sections)
            Pages[section.PageId].Sections.Add(section.Id, section);

        Dictionary = new();
        foreach (V2SectionedPage page in Pages.Values)
            foreach (KeyValuePair<int, V2Section> sectionPair in page.Sections)
                Dictionary.Add(sectionPair.Key, sectionPair.Value);

    }
    public Dictionary<int, V2SectionedPage> Pages { get; set; }
    public Dictionary<int, V2Section> Dictionary { get; set; }
    public static V2Sections Create(List<V2Section> sections, List<V2SectionedPage> sectionedPages) 
        => new V2Sections(sections, sectionedPages);
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
