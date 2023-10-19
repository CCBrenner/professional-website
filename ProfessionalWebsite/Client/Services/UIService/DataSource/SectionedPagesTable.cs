namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class SectionedPagesTable
{
    internal static Dictionary<int, SectionedPage> GetSectionedPagesDict()
    {
        Dictionary<int, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetSectionedPages())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    internal static List<SectionedPage> GetSectionedPages() => new()
    {
        SectionedPage.Create(0, 2, "projects"),  // not used
        SectionedPage.Create(1, 3, "knowhow"),
        SectionedPage.Create(2, 4, "collyn"),
        SectionedPage.Create(3, 5, "invent"),
        SectionedPage.Create(4, 6, "articles"),  // not used
    };
}
