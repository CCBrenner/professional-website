namespace ProfessionalWebsite.Client.Services.UI;

public interface ISectionMgmt
{
    void ToggleSection(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages);
    void CollapseAllShowOne(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages);
    int GetLocationPanelGroupId(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages);

}
