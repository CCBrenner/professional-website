namespace ProfessionalWebsite.Client.Services.UI;

public class SectionMgmt
{
    private SectionMgmt(Dictionary<int, SectionedPage> sectionedPagesDictionary, Dictionary<int, Section> sectionsDictionary)
    {
        _sections = sectionsDictionary;
        _sectionedPages = sectionedPagesDictionary;
        SetInstanceToGroupReferences();
    }

    private Dictionary<int, Section> _sections;
    private Dictionary<int, SectionedPage> _sectionedPages;

    public event Action<string> OnSectionMgmtChanged;

    /*
        Definitions:
            - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
            - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */

    public static SectionMgmt Create(
        Dictionary<int, SectionedPage> sectionedPagesDictionary, 
        Dictionary<int, Section> sectionsDictionary) =>
        new(sectionedPagesDictionary, sectionsDictionary);

    /// <summary>
    /// Collapses all sections and promotes one section to the top of the sectioned page.
    /// </summary>
    /// <param name="sectionId">ID of the section that is being promoted/which has been selected.</param>
    public void CollapseAllShowOne(int sectionId)
    {
        try
        {
            DemoteAllSections();
            Section section = _sections[sectionId];
            SectionedPage sectionedPage = _sectionedPages[section.SectionedPageId];
            Dictionary<int, Section> sectionsOfSectionedPage = sectionedPage.Sections;
            if (section.IsFirstSectionOfPage)
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.SetToIsNotCollapsed();
                sectionedPage.ASectionIsCurrentlyPromo = false;
            }
            else
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.SetToIsCollapsed();
                ToggleSection(sectionId);
                section.Promote();
                sectionedPage.ASectionIsCurrentlyPromo = true;
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"{knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId)
    {
        try
        {
            Section section = _sections[sectionId];
            //section.ToggleCollapse(!section.IsCollapsed);
            /*if (section.IsCollapsed)
            {
                section.SetToIsCollapsed();
            }
            else
            {
                section.SetToIsNotCollapsed();
            }*/
            section.ToggleCollapse();
            PromoteIfOnlyOneExpandedSection(sectionId);
            UpdateSectionsStatus(sectionId);
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
    /// <summary>
    /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
    /// </summary>
    /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
    public void ToggleAllSections(int pageId)
    {
        try
        {
            int sectionId = _sectionedPages[pageId].Sections.Keys.FirstOrDefault();
            UpdateSectionsStatus(sectionId);

            DemoteAllSections();
            if (_sectionedPages[pageId].SectionsStatus == SectionsStatus.AllAreOpen)
            {
                foreach (var section in _sections.Values)
                    section.SetToIsCollapsed();
                _sectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreCollapsed;
            }
            else
            {
                foreach (var section in _sections.Values)
                    section.SetToIsNotCollapsed();
                _sectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreOpen;
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }

    }
    /// <summary>
    /// Demotes all other sections and makes specified section the promo section.
    /// </summary>
    /// <param name="sectionId">ID of section to be made promo section.</param>
    public void PromoteSection(int sectionId)
    {
        try
        {
            DemoteAllSections();
            Section section = _sections[sectionId];
            section.Promote();
            SectionedPage sectionedPage = _sectionedPages[section.SectionedPageId];
            sectionedPage.ASectionIsCurrentlyPromo = true;
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
    /// <summary>
    /// Returns the ID of the location panel of a sectioned page of the specified section using the section's ID.
    /// </summary>
    /// <param name="sectionId">ID of section used to get the sectioned page's location panel's ID</param>
    /// <returns></returns>
    public int GetLocationPanelGroupId(int sectionId)
    {
        try
        {
            Section section = _sections[sectionId];
            SectionedPage sectionedPage = _sectionedPages[section.SectionedPageId];
            return sectionedPage.LocationPanelGroupId;
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
        return -1;
    }
    /// <summary>
    /// Removes promo status from all sections.
    /// </summary>
    private void DemoteAllSections()
    {
        foreach (var section in _sections.Values)
            section.Demote();
        foreach (SectionedPage sectionedPage in _sectionedPages.Values)
            sectionedPage.ASectionIsCurrentlyPromo = false;
    }
    /// <summary>
    /// Finds the sections of the sectioned page that the specified section is a part of, counts how many sections are currently expanded, and then sets the status based on the number of expanded sections.
    /// </summary>
    /// <param name="sectionId">ID of specified section.</param>
    private void UpdateSectionsStatus(int sectionId)
    {
        Section section = _sections[sectionId];
        SectionedPage sectionedPage = _sectionedPages[section.SectionedPageId];

        int openSections = 0;
        foreach (Section sec in sectionedPage.Sections.Values)
            if (!sec.IsCollapsed)
                openSections++;

        if (openSections == 0)
            sectionedPage.SectionsStatus = SectionsStatus.AllAreCollapsed;
        else if (openSections == sectionedPage.Sections.Count)
            sectionedPage.SectionsStatus = SectionsStatus.AllAreOpen;
        else
            sectionedPage.SectionsStatus = SectionsStatus.AtLeastOneIsOpen;
    }
    /// <summary>
    /// Initializes sectioned page reference to its sections and vice versa. Establishes a one-to-many relationship through on-hand references.
    /// </summary>
    private void SetInstanceToGroupReferences()
    {
        foreach (Section section in _sections.Values)
            section.SetInstanceToGroupRelationship(_sectionedPages.Values.ToList());

        List<Section> sectionsOfPage = new List<Section>();
        foreach (Section section in _sections.Values)
        {
            _sectionedPages[section.SectionedPageId]
                .Sections
                .Add(section.Id, section);
        }
    }
    /// <summary>
    /// If at any point in time, if there is only one section in a sectioned page that is expanded, then promote that section.
    /// </summary>
    /// <param name="sectionId">ID of section used to determine the sectioned page to check for promo.</param>
    private void PromoteIfOnlyOneExpandedSection(int sectionId)
    {
        try
        {
            int expandedSectionsCount = 0;
            int idOfSectionToPromote = 0;

            Section sec = _sections[sectionId];
            SectionedPage sectionedPage = _sectionedPages[sec.SectionedPageId];
            Dictionary<int, Section> sectionsOfSectionedPage = sectionedPage.Sections;

            foreach (Section section in sectionsOfSectionedPage.Values)
            {
                if (!section.IsCollapsed)
                {
                    expandedSectionsCount++;
                    idOfSectionToPromote = section.Id;
                }
            }

            if (expandedSectionsCount == 1)
                PromoteSection(idOfSectionToPromote);
            else
                DemoteAllSections();
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
}
