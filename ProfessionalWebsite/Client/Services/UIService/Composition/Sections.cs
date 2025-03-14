namespace ProfessionalWebsite.Client.Services.UI;

public class Sections
{
    /*
        Definitions:
            - "sectioned page" : a page that implements according Dictionary (collapse/expand) & utilizes SectionsMgmt for the handling logic of those Dictionary
            - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other Dictionary of the page
    */
    public Sections(Dictionary<int, Section> initSections)
    {
        Dictionary = initSections;
    }
    public Dictionary<int, Section> Dictionary { get; private set; }
    public void CollapseAllShowOne(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        DemoteAllSections(Dictionary, sectionedPages);

        Section section = Dictionary[sectionId];
        SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];
        Dictionary<int, Section> DictionaryOfSectionedPage = sectionedPage.Sections;

        if (section.IsFirstSectionOfPage)
        {
            foreach (var sec in DictionaryOfSectionedPage.Values)
                sec.Expand();
            sectionedPage.ASectionIsCurrentlyPromo = false;
        }
        else
        {
            foreach (var sec in DictionaryOfSectionedPage.Values)
                sec.Collapse();
            ToggleSection(sectionId, sectionedPages);
            section.Promote();
            sectionedPage.ASectionIsCurrentlyPromo = true;
        }
    }
    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        Section section = Dictionary[sectionId];
        section.ToggleCollapse();
        PromoteIfOnlyOneExpandedSection(sectionId, sectionedPages);
        UpdateSectionsStatus(sectionId, sectionedPages);
    }
    /// <summary>
    /// Demotes all other Dictionary and makes specified section the promo section.
    /// </summary>
    /// <param name="sectionId">ID of section to be made promo section.</param>
    public void PromoteSection(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        DemoteAllSections(Dictionary, sectionedPages);
        Section section = Dictionary[sectionId];
        section.Promote();
        SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];
        sectionedPage.ASectionIsCurrentlyPromo = true;
    }
    /// <summary>
    /// Returns the ID of the location panel of a sectioned page of the specified section using the section's ID.
    /// </summary>
    /// <param name="sectionId">ID of section used to get the sectioned page's location panel's ID</param>
    /// <returns></returns>
    public int GetLocationPanelGroupId(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        return sectionedPages[Dictionary[sectionId].SectionedPageId].LocationPanelGroupId;
    }
    public void SetBiDirectionalReferencesForSectionedPagesAndSections(Dictionary<int, SectionedPage> sectionedPages)
    {
        foreach (var section in Dictionary.Values)
        {
            var sectionedPageId = section.SectionedPageId;
            section.SetSectionedPageReference(sectionedPages[sectionedPageId]);
            section.SectionedPage.AddSectionReference(section);
        }
    }
    /// <summary>
    /// Removes promo status from all Dictionary.
    /// </summary>
    private void DemoteAllSections(Dictionary<int, Section> Dictionary, Dictionary<int, SectionedPage> sectionedPages)
    {
        foreach (var section in Dictionary.Values)
            section.Demote();
        foreach (SectionedPage sectionedPage in sectionedPages.Values)
            sectionedPage.ASectionIsCurrentlyPromo = false;
    }
    /// <summary>
    /// Finds the Dictionary of the sectioned page that the specified section is a part of, counts how many Dictionary are currently expanded, and then sets the status based on the number of expanded Dictionary.
    /// </summary>
    /// <param name="sectionId">ID of specified section.</param>
    private void UpdateSectionsStatus(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        Section section = Dictionary[sectionId];
        SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];

        int openSections = 0;

        foreach (Section sec in sectionedPage.Sections.Values)
            if (!sec.IsCollapsed)
                openSections++;

        if (openSections == 0)
            sectionedPage.Status = SectionedPageStatus.AllAreCollapsed;
        else if (openSections == sectionedPage.Sections.Count)
            sectionedPage.Status = SectionedPageStatus.AllAreOpen;
        else
            sectionedPage.Status = SectionedPageStatus.AtLeastOneIsOpen;
    }
    /// <summary>
    /// If at any point in time, if there is only one section in a sectioned page that is expanded, then promote that section.
    /// </summary>
    /// <param name="sectionId">ID of section used to determine the sectioned page to check for promo.</param>
    private void PromoteIfOnlyOneExpandedSection(int sectionId, Dictionary<int, SectionedPage> sectionedPages)
    {
        int expandedSectionsCount = 0;
        int idOfSectionToPromote = 0;

        Section sec = Dictionary[sectionId];
        SectionedPage sectionedPage = sectionedPages[sec.SectionedPageId];
        Dictionary<int, Section> DictionaryOfSectionedPage = sectionedPage.Sections;

        foreach (Section section in DictionaryOfSectionedPage.Values)
        {
            if (!section.IsCollapsed)
            {
                expandedSectionsCount++;
                idOfSectionToPromote = section.Id;
            }
        }

        if (expandedSectionsCount == 1)
            PromoteSection(idOfSectionToPromote, sectionedPages);
        else
            DemoteAllSections(Dictionary, sectionedPages);
    }
}
