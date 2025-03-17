namespace ProfessionalWebsite.Client.Services.UI;

public class V2SectionsTable
{
    public static Dictionary<int, V2Section> GetDictionary()
    {
        Dictionary<int, V2Section> sections = new();
        foreach (var section in GetList())
        {
            sections.Add(section.Id, section);
        }
        return sections;
    }
    public static List<V2Section> GetList()
    {
        // Pages and their IDs:
        int selectDemoPage = 1;

        return new()
        {
            V2Section.Create(5037, "Select Demo", selectDemoPage),
            V2Section.Create(5038, "About UI", selectDemoPage),
        };
    }
}

