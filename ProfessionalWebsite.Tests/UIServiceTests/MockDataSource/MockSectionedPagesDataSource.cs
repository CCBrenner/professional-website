using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockSectionedPagesDataSource
{
    public static Dictionary<int, SectionedPage> GetDictionary()
    {
        Dictionary<int, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetSectionedPages())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    public static List<SectionedPage> GetSectionedPages() => new()
    {
        SectionedPage.Create(0, 2, "projects"),  // not used
        SectionedPage.Create(1, 3, "knowhow"),  // not used
        SectionedPage.Create(2, 4, "collyn"),  // not used
        SectionedPage.Create(3, 5, "invent"),  // not used
        SectionedPage.Create(4, 6, "articles"),  // not used
        SectionedPage.Create(5, 10, "select-demo"),
    };
}
