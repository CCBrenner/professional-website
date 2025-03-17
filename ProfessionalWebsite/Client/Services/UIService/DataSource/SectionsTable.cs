namespace ProfessionalWebsite.Client.Services.UI;

public class SectionsTable
{
    public static Dictionary<int, Section> GetDictionary()
    {
        Dictionary<int, Section> sections = new();
        foreach (var section in GetList())
        {
            sections.Add(section.Id, section);
        }
        return sections;
    }
    public static List<Section> GetList()
    {
        // Pages and their IDs:
        int selectDemoPage = 1;

        return new()
        {
            Section.Create(5037, "Select Demo", selectDemoPage),
            Section.Create(5038, "About UI", selectDemoPage),
        };
    }
}

