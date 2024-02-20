namespace ProfessionalWebsite.Client.Services.UI;

public interface ISectionMgmt
{
    Dictionary<string, Section> Sections { get; }
    Dictionary<int, SectionedPage> SectionedPages { get; }
    void ToggleAllSections(int pageId);
    void ToggleSection(string sectionId);
    void CollapseAllShowOne(string sectionId);
    int GetLocationPanelGroupId(string sectionId);
}