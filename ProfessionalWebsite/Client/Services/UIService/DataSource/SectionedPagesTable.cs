namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class SectionedPagesTable
{
    internal static Dictionary<string, SectionedPage> GetSectionedPagesDict()
    {
        Dictionary<string, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetSectionedPages())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    internal static List<SectionedPage> GetSectionedPages() => new()
    {
        SectionedPage.Create("projects", 2, "projects"),  // not used
        SectionedPage.Create("knowhow", 3, "knowhow"),
        SectionedPage.Create("collyn", 4, "collyn"),
        SectionedPage.Create("invent", 5, "invent"),
        SectionedPage.Create("articles", 6, "articles"),  // not used
    };
}
