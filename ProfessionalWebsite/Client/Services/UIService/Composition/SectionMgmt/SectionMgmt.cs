namespace ProfessionalWebsite.Client.Services.UI;

public class SectionMgmt : ISectionMgmt
{
    private SectionMgmt(Dictionary<int, SectionedPage> sectionedPagesDictionary, Dictionary<string, Section> sectionsDictionary)
    {
        Sections = sectionsDictionary;
        SectionedPages = sectionedPagesDictionary;
        SetInstanceToGroupReferences();
    }

    public Dictionary<string, Section> Sections { get; private set; }
    public Dictionary<int, SectionedPage> SectionedPages { get; private set; }  // key == SectionPage.Id

    /*
        Definitions:
            - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
            - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */

    public static SectionMgmt Create(Dictionary<int, SectionedPage> sectionedPagesDictionary, Dictionary<string, Section> sectionsDictionary) =>
        new(sectionedPagesDictionary, sectionsDictionary);
    /// <summary>
    /// Collapses all sections and promotes one section to the top of the sectioned page.
    /// </summary>
    /// <param name="sectionId">ID of the section that is being promoted/which has been selected.</param>
    public void CollapseAllShowOne(string sectionId)
    {
        try
        {
            DemoteAllSections();
            Section section = Sections[sectionId];
            SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
            Dictionary<string, Section> sectionsOfSectionedPage = sectionedPage.Sections;
            if (section.IsFirstSectionOfPage)
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.ToggleCollapse(false);
                sectionedPage.ASectionIsCurrentlyPromo = false;
            }
            else
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.ToggleCollapse(true);
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
    public void ToggleSection(string sectionId)
    {
        try
        {
            Section section = Sections[sectionId];
            section.ToggleCollapse(!section.IsCollapsed);
            PromoteIfOnlyOneExpandedSection(sectionId);
            UpdateSectionsStatus(sectionId);
            //RaiseEventOnSectionMgmtChanged();
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
            string sectionId = SectionedPages[pageId].Sections.Keys.FirstOrDefault();
            UpdateSectionsStatus(sectionId);

            DemoteAllSections();
            if (SectionedPages[pageId].SectionsStatus == SectionsStatus.AllAreOpen)
            {
                foreach (var section in Sections.Values)
                    section.ToggleCollapse(true);
                SectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreCollapsed;
            }
            else
            {
                foreach (var section in Sections.Values)
                    section.ToggleCollapse(false);
                SectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreOpen;
            }
            //RaiseEventOnSectionMgmtChanged();
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
    public void PromoteSection(string sectionId)
    {
        try
        {
            DemoteAllSections();
            Section section = Sections[sectionId];
            section.Promote();
            SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
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
    public int GetLocationPanelGroupId(string sectionId)
    {
        try
        {
            Section section = Sections[sectionId];
            SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
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
        foreach (var section in Sections.Values)
            section.Demote();
        foreach (SectionedPage sectionedPage in SectionedPages.Values)
            sectionedPage.ASectionIsCurrentlyPromo = false;
    }
    /// <summary>
    /// Finds the sections of the sectioned page that the specified section is a part of, counts how many sections are currently expanded, and then sets the status based on the number of expanded sections.
    /// </summary>
    /// <param name="sectionId">ID of specified section.</param>
    private void UpdateSectionsStatus(string sectionId)
    {
        Section section = Sections[sectionId];
        SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];

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
        foreach (Section section in Sections.Values)
            section.SetInstanceToGroupRelationship(SectionedPages.Values.ToList());

        List<Section> sectionsOfPage = new();
        foreach (Section section in Sections.Values)
        {
            SectionedPages[section.SectionedPageId]
                .Sections
                .Add(section.Id, section);
        }
    }

    /// <summary>
    /// If at any point in time, if there is only one section in a sectioned page that is expanded, then promote that section.
    /// </summary>
    /// <param name="sectionId">ID of section used to determine the sectioned page to check for promo.</param>
    private void PromoteIfOnlyOneExpandedSection(string sectionId)
    {
        try
        {
            int expandedSectionsCount = 0;
            string idOfSectionToPromote = string.Empty;

            Section sec = Sections[sectionId];
            SectionedPage sectionedPage = SectionedPages[sec.SectionedPageId];
            Dictionary<string, Section> sectionsOfSectionedPage = sectionedPage.Sections;

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

    /// <summary>
    /// Updates the component that consumes it when a method in the _panel class that consumes this method invokes/signals that a change to the state of it has occurred.
    /// </summary>
    /*
    private void RaiseEventOnSectionMgmtChanged()
    {
        OnSectionMgmtChanged?.Invoke("");
    }
    */
}
