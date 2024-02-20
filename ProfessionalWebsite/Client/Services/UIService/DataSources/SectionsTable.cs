namespace ProfessionalWebsite.Client.Services.UI;

public sealed class SectionsTable
{
    public static Dictionary<string, Section> GetSectionsDict()
    {
        Dictionary<string, Section> sectionsDict = new();

        foreach (Section section in GetSections())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    public static List<Section> GetSections() => new()
    {
        /* start "knowhow" */
        Section.Create("knowHowOverview", 1, isFirstSectionOfPage: true),  // 0
        Section.Create("backEnd", 1),  // 1, Back-end
        Section.Create("frontEnd", 1),  // 3
        Section.Create("uxui", 1),  // 4
        Section.Create("cloudAndContainers", 1),  // 5
        Section.Create("databases", 1),  // 6
        Section.Create("testing", 1),  // 7
        Section.Create("whyProgramming", 1),  // 13
        Section.Create("aboutThisWebApp", 1),  // 14
        Section.Create("hideAndSeek", 1),  // 15
        Section.Create("beeHiveMgmt", 1),  // 16
        Section.Create("matchGame", 1),  // 33, Match Game
        /* end "knowhow" */
        /* start "collyn" */
        Section.Create("collynIntro", 2, isFirstSectionOfPage: true),  // Introduction
        Section.Create("employmentHistory", 2),  // 18, Employment History
        Section.Create("educationHistory", 2),  // 19
        Section.Create("notableDones", 2),  // 20
        Section.Create("inspirations", 2),  // 21
        Section.Create("objectives", 2),  // 22
        Section.Create("contact", 2),  // 23, Contact
        Section.Create(24, 2),  // 24, Feedback
        Section.Create(25, 2),  // 25
        /* end "collyn" */
        /* start "invent" */
        Section.Create(26, 3, isFirstSectionOfPage: true),  // 26
        Section.Create(27, 3),  // 27
        Section.Create(28, 3),  // 28
        Section.Create(29, 3),  // 29
        Section.Create(30, 3),  // 30
        Section.Create(31, 3),  // 31
        Section.Create(32, 3),  // 32
        /* end "invent" */
    };
}
