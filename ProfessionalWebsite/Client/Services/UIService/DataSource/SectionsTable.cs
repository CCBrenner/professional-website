namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class SectionsTable
{
    internal static Dictionary<int, Section> GetSectionsDict()
    {
        Dictionary<int, Section> sectionsDict = new();

        foreach (Section section in GetSections())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    internal static List<Section> GetSections() => new()
    { 
        /* start "knowhow" */
        Section.CreateAsFirstSectionOfPage(0, 1),
        Section.Create(1, 1),  // Back-end
        Section.Create(3, 1),
        Section.Create(4, 1),
        Section.Create(5, 1),
        Section.Create(6, 1),
        Section.Create(7, 1),
        Section.Create(13, 1),
        Section.Create(14, 1),
        Section.Create(15, 1),
        Section.Create(16, 1),
        Section.Create(33, 1),  // Match Game
        /* end "knowhow" */
        /* start "collyn" */
        Section.CreateAsFirstSectionOfPage(17, 2),  // Introduction
        Section.Create(18, 2),  // Employment History
        Section.Create(19, 2),
        Section.Create(20, 2),
        Section.Create(21, 2),
        Section.Create(22, 2),
        Section.Create(23, 2),  // Contact
        Section.Create(24, 2),  // Feedback
        Section.Create(25, 2),
        /* end "collyn" */
        /* start "invent" */
        Section.CreateAsFirstSectionOfPage(26, 3),
        Section.Create(27, 3),
        Section.Create(28, 3),
        Section.Create(29, 3),
        Section.Create(30, 3),
        Section.Create(31, 3),
        Section.Create(32, 3),
        /* end "invent" */
    };
}
