using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockSectionsDataSource
{
    public static Dictionary<int, Section> GetSectionsDict()
    {
        Dictionary<int, Section> sectionsDict = new();

        foreach (Section section in GetSections())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    public static List<Section> GetSections() => new()
    { 
        /* start "knowhow" */
        Section.CreateAsFirstSectionOfPage(0, 1),
        Section.CreateRegularSection(1, 1),  // Back-end
        Section.CreateRegularSection(3, 1),
        Section.CreateRegularSection(4, 1),
        Section.CreateRegularSection(5, 1),
        Section.CreateRegularSection(6, 1),
        Section.CreateRegularSection(7, 1),
        Section.CreateRegularSection(13, 1),
        Section.CreateRegularSection(14, 1),
        Section.CreateRegularSection(15, 1),
        Section.CreateRegularSection(16, 1),
        Section.CreateRegularSection(33, 1),  // Match Game
        /* end "knowhow" */
        /* start "collyn" */
        Section.CreateAsFirstSectionOfPage(17, 2),  // Introduction
        Section.CreateRegularSection(18, 2),  // Employment History
        Section.CreateRegularSection(19, 2),
        Section.CreateRegularSection(20, 2),
        Section.CreateRegularSection(21, 2),
        Section.CreateRegularSection(22, 2),
        Section.CreateRegularSection(23, 2),  // Contact
        Section.CreateRegularSection(24, 2),  // Feedback
        Section.CreateRegularSection(25, 2),
        /* end "collyn" */
        /* start "invent" */
        Section.CreateAsFirstSectionOfPage(26, 3),
        Section.CreateRegularSection(27, 3),
        Section.CreateRegularSection(28, 3),
        Section.CreateRegularSection(29, 3),
        Section.CreateRegularSection(30, 3),
        Section.CreateRegularSection(31, 3),
        Section.CreateRegularSection(32, 3),
        /* end "invent" */
    };
}

