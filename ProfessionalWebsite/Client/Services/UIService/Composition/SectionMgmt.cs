namespace ProfessionalWebsite.Client.Services.UI;

public static class SectionMgmt
{
    /*
        Definitions:
            - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
            - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */
    public static void CollapseAllShowOne(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            DemoteAllSections(sections, sectionedPages);

            Section section = sections[sectionId];
            SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];
            Dictionary<int, Section> sectionsOfSectionedPage = sectionedPage.Sections;

            if (section.IsFirstSectionOfPage)
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.Expand();
                sectionedPage.ASectionIsCurrentlyPromo = false;
            }
            else
            {
                foreach (var sec in sectionsOfSectionedPage.Values)
                    sec.Collapse();
                ToggleSection(sectionId, sections, sectionedPages);
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
    public static void ToggleSection(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            Section section = sections[sectionId];
            section.ToggleCollapse();
            PromoteIfOnlyOneExpandedSection(sectionId, sections, sectionedPages);
            UpdateSectionsStatus(sectionId, sections, sectionedPages);
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
    public static void PromoteSection(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            DemoteAllSections(sections, sectionedPages);
            Section section = sections[sectionId];
            section.Promote();
            SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];
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
    public static int GetLocationPanelGroupId(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            Section section = sections[sectionId];
            SectionedPage sectionedPage = sectionedPages[section.SectionedPageId];
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
    private static void DemoteAllSections(Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        foreach (var section in sections.Values)
            section.Demote();
        foreach (SectionedPage sectionedPage in sectionedPages.Values)
            sectionedPage.ASectionIsCurrentlyPromo = false;
    }
    /// <summary>
    /// Finds the sections of the sectioned page that the specified section is a part of, counts how many sections are currently expanded, and then sets the status based on the number of expanded sections.
    /// </summary>
    /// <param name="sectionId">ID of specified section.</param>
    private static void UpdateSectionsStatus(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        Section section = sections[sectionId];
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
    private static void PromoteIfOnlyOneExpandedSection(int sectionId, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            int expandedSectionsCount = 0;
            int idOfSectionToPromote = 0;

            Section sec = sections[sectionId];
            SectionedPage sectionedPage = sectionedPages[sec.SectionedPageId];
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
                PromoteSection(idOfSectionToPromote, sections, sectionedPages);
            else
                DemoteAllSections(sections, sectionedPages);
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
        }
    }
}
