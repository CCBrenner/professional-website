﻿namespace ProfessionalWebsite.Client.Services.UIService.Mgmt.DataTables;

public sealed class SectionsTable
{
    private List<Section> sections = new()
    {
        /* start "knowhow" */
        new Section(0, 1, isFirstSectionOfPage: true),
        new Section(1, 1),  // Back-end
        new Section(3, 1),
        new Section(4, 1),
        new Section(5, 1),
        new Section(6, 1),
        new Section(7, 1),
        new Section(13, 1),
        new Section(14, 1),
        new Section(15, 1),
        new Section(16, 1),
        new Section(33, 1),  // Match Game
        /* end "knowhow" */
        /* start "collyn" */
        new Section(17, 2, isFirstSectionOfPage: true),  // Introduction
        new Section(18, 2),  // Employment History
        new Section(19, 2),
        new Section(20, 2),
        new Section(21, 2),
        new Section(22, 2),
        new Section(23, 2),  // Contact
        new Section(24, 2),  // Feedback
        new Section(25, 2),
        /* end "collyn" */
        /* start "invent" */
        new Section(26, 3, isFirstSectionOfPage: true),
        new Section(27, 3),
        new Section(28, 3),
        new Section(29, 3),
        new Section(30, 3),
        new Section(31, 3),
        new Section(32, 3),
        /* end "invent" */
    };

    public static List<Section> GetSections()
    {
        SectionsTable sectionsTable = new();
        return sectionsTable.sections;
    }
    public static Dictionary<int, Section> GetSectionsDict()
    {
        SectionsTable sectionsTable = new();
        Dictionary<int, Section> sectionsDict = new Dictionary<int, Section>();

        foreach (Section section in sectionsTable.sections)
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
}