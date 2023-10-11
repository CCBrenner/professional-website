namespace ProfessionalWebsite.Client.Services.UI;

public sealed class SectionedPagesTable
{
    public static Dictionary<int, SectionedPage> GetSectionedPagesDict()
    {
        Dictionary<int, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetSectionedPages())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    public static List<SectionedPage> GetSectionedPages() => new()
    {
        new SectionedPage(0, 2, "projects"),  // not used
        new SectionedPage(1, 3, "knowhow"),
        new SectionedPage(2, 4, "collyn"),
        new SectionedPage(3, 5, "invent"),
        new SectionedPage(4, 6, "articles"),  // not used
    };
}
