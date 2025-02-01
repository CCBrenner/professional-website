namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class SectionedPagesTable
{
    internal static Dictionary<int, SectionedPage> GetDictionary()
    {
        Dictionary<int, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetList())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    internal static List<SectionedPage> GetList() => new()
    {
        // If a SectionedPage has any existing Sections, it needs to be available even if not used in UI.
        SectionedPage.Create(0, 2, "projects"),  // not currently used
        SectionedPage.Create(1, 3, "knowhow"),  // not currently used
        SectionedPage.Create(2, 4, "collyn"),  // not currently used
        SectionedPage.Create(3, 5, "invent"),  // not currently used
        SectionedPage.Create(4, 6, "articles"),  // not currently used
        SectionedPage.Create(5, 10, "select-demo"),
    };
}
