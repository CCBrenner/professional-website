using ProfessionalWebsite.Client.Services.UI;

namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockSectionedPagesDataSource
{
    public static Dictionary<int, SectionedPage> GetDictionary()
    {
        Dictionary<int, SectionedPage> sectionedPagesDict = new();

        foreach (SectionedPage sectionedPage in GetList())
            sectionedPagesDict.Add(sectionedPage.Id, sectionedPage);

        return sectionedPagesDict;
    }
    public static List<SectionedPage> GetList() => new()
    {
        SectionedPage.Create(5, 10),
    };
}
