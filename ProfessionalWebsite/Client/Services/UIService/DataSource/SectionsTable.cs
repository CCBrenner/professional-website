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
        Section.CreateAsFirstSectionOfPage(0, 1),  // 0: Overview (ProjectInfo (used to be KnowHow))
        Section.CreateRegularSection(1, 1),  // 1: Backend
        Section.CreateRegularSection(3, 1),  // 3: Frontend
        Section.CreateRegularSection(4, 1),  // 4: UXUI
        Section.CreateRegularSection(5, 1),  // 5: Cloud & Containers
        Section.CreateRegularSection(6, 1),  // 6: Databases
        Section.CreateRegularSection(7, 1),  // 7: Testing
        Section.CreateRegularSection(13, 1),  // 13: Why Programing?
        Section.CreateRegularSection(14, 1),  // 14: About This Web App
        Section.CreateRegularSection(15, 1),  // 15: Hide & Seek
        Section.CreateRegularSection(16, 1),  // 16: Bee Hive Management
        Section.CreateRegularSection(33, 1),  // 33: Match Game
        Section.CreateRegularSection(34, 1),  // 34: Contact (Projects-Only version only)
        Section.CreateRegularSection(35, 1),  // 35: Counter + Spinners
        Section.CreateRegularSection(36, 1),  // 36: Sudoku Solver
        /* end "knowhow" */
        /* start "collyn" */
        Section.CreateAsFirstSectionOfPage(17, 2),  // 17: Introduction (Collyn)
        Section.CreateRegularSection(18, 2),  // 18: Employment History
        Section.CreateRegularSection(19, 2),  // 19: Education History
        Section.CreateRegularSection(20, 2),  // 20: Notable Dones
        Section.CreateRegularSection(21, 2),  // 21: Inspirations
        Section.CreateRegularSection(22, 2),  // 22: Objectives
        Section.CreateRegularSection(23, 2),  // 23: Contact
        Section.CreateRegularSection(24, 2),  // 24: Feedback
        Section.CreateRegularSection(25, 2),  // 25: Background
        /* end "collyn" */
        /* start "invent" */
        Section.CreateAsFirstSectionOfPage(26, 3),  // 26: Overview (Invent)
        Section.CreateRegularSection(27, 3),  // 27: Future Vision
        Section.CreateRegularSection(28, 3),  // 28: Unified Faith
        Section.CreateRegularSection(29, 3),  // 29: Hang Out Availability App
        Section.CreateRegularSection(30, 3),  // 30: Philo Orientation
        Section.CreateRegularSection(31, 3),  // 31: Controller Keyboard Prototype
        Section.CreateRegularSection(32, 3),  // 32: Audio Games
        /* end "invent" */
    };
}
