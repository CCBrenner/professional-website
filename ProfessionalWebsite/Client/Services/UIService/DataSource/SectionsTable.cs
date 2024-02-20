namespace ProfessionalWebsite.Client.Services.UI;

public sealed class SectionsTable
{
    public static Dictionary<SecId, Section> GetSectionsDict()
    {
        Dictionary<SecId, Section> sectionsDict = new();

        foreach (Section section in GetSections())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    public static List<Section> GetSections() => new()
    {
        /* start "knowhow" */
        Section.Create(SecId.KnowHowOverview, 1, isFirstSectionOfPage: true),  // 0
        Section.Create(SecId.BackEnd, 1),  // 1, Back-end
        Section.Create(SecId.FrontEnd, 1),  // 3
        Section.Create(SecId.UXUI, 1),  // 4
        Section.Create(SecId.CloudAndContainers, 1),  // 5
        Section.Create(SecId.Databases, 1),  // 6
        Section.Create(SecId.Testing, 1),  // 7
        Section.Create(SecId.WhyProgramming, 1),  // 13
        Section.Create(SecId.AboutThisWebApp, 1),  // 14
        Section.Create(SecId.HideAndSeek, 1),  // 15
        Section.Create(SecId.BeeHiveMgmt, 1),  // 16
        Section.Create(SecId.MatchGame, 1),  // 33, Match Game
        /* end "knowhow" */
        /* start "collyn" */
        Section.Create(SecId.CollynIntro, 2, isFirstSectionOfPage: true),  // Introduction
        Section.Create(SecId.EmploymentHistory, 2),  // 18, Employment History
        Section.Create(SecId.EducationHistory, 2),  // 19
        Section.Create(SecId.NotableDones, 2),  // 20
        Section.Create(SecId.Inspirations, 2),  // 21
        Section.Create(SecId.Objectives, 2),  // 22
        Section.Create(SecId.Contact, 2),  // 23, Contact
        Section.Create(SecId.Feedback, 2),  // 24, Feedback
        Section.Create(SecId.Background, 2),  // 25
        /* end "collyn" */
        /* start "invent" */
        Section.Create(SecId.InventOverview, 3, isFirstSectionOfPage: true),  // 26
        Section.Create(SecId.FutureVision, 3),  // 27
        Section.Create(SecId.UnifiedFaith, 3),  // 28
        Section.Create(SecId.HangOutAvailability, 3),  // 29
        Section.Create(SecId.PhiloOrientation, 3),  // 30
        Section.Create(SecId.ControllerKeyboardProtoype, 3),  // 31
        Section.Create(SecId.AudioGames, 3),  // 32
        /* end "invent" */
    };
}
