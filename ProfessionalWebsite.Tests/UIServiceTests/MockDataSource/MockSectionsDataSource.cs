using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockSectionsDataSource
{
    public static Dictionary<int, Section> GetDictionary()
    {
        Dictionary<int, Section> sectionsDict = new();

        foreach (Section section in GetList())
            sectionsDict.Add(section.Id, section);

        return sectionsDict;
    }
    public static List<Section> GetList()
    {
        // each of these is a SectionedPage
        int knowHow = 1;
        int collyn = 2;
        int invent = 3;
        int selectDemo = 5;

        return new()
        {
            Section.CreateAsFirstSectionOfPage(1000, "Overview", knowHow),
            Section.Create(1001, "Backend", knowHow),
            Section.Create(1003, "Frontend", knowHow),
            Section.Create(1004, "UXUI", knowHow),
            Section.Create(1005, "Cloud & Containers", knowHow),
            Section.Create(1006, "Databases", knowHow),
            Section.Create(1007, "Testing", knowHow),
            Section.Create(1013, "Why Programing?", knowHow),
            Section.Create(1014, "About This Web App", knowHow),
            Section.Create(1015, "Hide & Seek", knowHow),
            Section.Create(1016, "Bee Hive Management", knowHow),
            Section.Create(1033, "Match Game", knowHow),
            Section.Create(1034, "Contact", knowHow),
            Section.Create(1035, "Counter + Spinners", knowHow),
            Section.Create(1036, "Sudoku Solver", knowHow),
            Section.CreateAsFirstSectionOfPage(2017, "Introduction", collyn),
            Section.Create(2018, "Employment History", collyn),
            Section.Create(2019, "Education History", collyn),
            Section.Create(2020, "Notable Dones", collyn),
            Section.Create(2021, "Inspirations", collyn),
            Section.Create(2022, "Objectives", collyn),
            Section.Create(2023, "Contact", collyn),
            Section.Create(2024, "Feedback", collyn),
            Section.Create(2025, "Background", collyn),
            Section.CreateAsFirstSectionOfPage(3026, "Overview", invent),
            Section.Create(3027, "Future Vision", invent),
            Section.Create(3028, "Unified Faith", invent),
            Section.Create(3029, "Hang Out Availability App", invent),
            Section.Create(3030, "Philo Orientation", invent),
            Section.Create(3031, "Controller Keyboard Prototype", invent),
            Section.Create(3032, "Audio Games", invent),
            Section.CreateAsFirstSectionOfPage(5037, "Select Demo", selectDemo),
            Section.CreateAsFirstSectionOfPage(5038, "About UI", selectDemo),
        };
    }
}

