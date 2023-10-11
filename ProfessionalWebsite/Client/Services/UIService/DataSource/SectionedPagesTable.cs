namespace ProfessionalWebsite.Client.Services.UI;

public sealed class SectionedPagesTable
{
    private List<SectionedPage> sectionedPages = new()
    {
        new SectionedPage(0, 2, "projects"),  // not used
        new SectionedPage(1, 3, "knowhow"),
        new SectionedPage(2, 4, "collyn"),
        new SectionedPage(3, 5, "invent"),
        new SectionedPage(4, 6, "articles"),  // not used
    };

    public static List<SectionedPage> GetSectionedPages()
    {
        SectionedPagesTable sectionedPagesTable = new();
        return sectionedPagesTable.sectionedPages;
    }

    public static Dictionary<int, SectionedPage> GetSectionedPagesDict()
    {
        SectionedPagesTable sectionedPagesTable = new();
        Dictionary<int, SectionedPage> sectionedPagesDict = new Dictionary<int, SectionedPage>();

        foreach (SectionedPage sectionedPage in sectionedPagesTable.sectionedPages)
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
}
