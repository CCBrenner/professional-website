using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockSectionsDataSource
{
    internal static Dictionary<string, Section> GetSectionsDict()
    {
        Dictionary<string, Section> sectionsDict = new();

        foreach (Section section in GetSections())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    internal static List<Section> GetSections() => new()
    { 
        /* start "knowhow" (1) */
        Section.CreateAsFirstSectionOfPage("knowhow-overview", "knowhow"),  // 0
        Section.CreateRegularSection("backend", "knowhow"),  // 1: Back-end
        Section.CreateRegularSection("frontend", "knowhow"),  // 3
        Section.CreateRegularSection("uxui", "knowhow"),  // 4
        Section.CreateRegularSection("cloud-and-containers", "knowhow"),  // 5
        Section.CreateRegularSection("databases", "knowhow"),  // 6
        Section.CreateRegularSection("testing", "knowhow"),  // 7
        Section.CreateRegularSection("why-programming", "knowhow"),  // 13
        Section.CreateRegularSection("about-this-web-app", "knowhow"),  // 14
        Section.CreateRegularSection("hide-and-seek", "knowhow"),  // 15
        Section.CreateRegularSection("bee-hive-mgmt", "knowhow"),  // 16
        Section.CreateRegularSection("match-game", "knowhow"),  // 33: Match Game
        /* end "knowhow" (1) */
        /* start "collyn" (2) */
        Section.CreateAsFirstSectionOfPage("collyn-intro", "collyn"),  // 17: Introduction
        Section.CreateRegularSection("employment-history", "collyn"),  // 18: Employment History
        Section.CreateRegularSection("education-history", "collyn"),  // 19
        Section.CreateRegularSection("notable-dones", "collyn"),  // 20
        Section.CreateRegularSection("inspirations", "collyn"),  // 21
        Section.CreateRegularSection("objectives", "collyn"),  // 22
        Section.CreateRegularSection("contact", "collyn"),  // 23: Contact
        Section.CreateRegularSection("feedback", "collyn"),  // 24: Feedback
        Section.CreateRegularSection("background", "collyn"),  // 25
        /* end "collyn" (2) */
        /* start "invent" (3) */
        Section.CreateAsFirstSectionOfPage("invent-overview", "invent"),  // 26: 
        Section.CreateRegularSection("future-vision", "invent"),  // 27
        Section.CreateRegularSection("unified-faith", "invent"),  // 28
        Section.CreateRegularSection("hang-out-availability", "invent"),  // 29
        Section.CreateRegularSection("philo-orientation", "invent"),  // 30
        Section.CreateRegularSection("controller-keyboard-prototype", "invent"),  // 31
        Section.CreateRegularSection("audio-games", "invent"),  // 32
        /* end "invent" (3) */
    };
}

