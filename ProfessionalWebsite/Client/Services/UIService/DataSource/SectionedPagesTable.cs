namespace ProfessionalWebsite.Client.Services.UI;

public class SectionedPagesTable
{
    public static Dictionary<int, SectionedPage> GetDictionary()
    {
        Dictionary<int, SectionedPage> pages = new();
        foreach (var page in GetList())
        {
            pages.Add(page.Id, page);
        }
        return pages;
    }
    public static List<SectionedPage> GetList()
    {
        // Pages:
        int selectDemoPageId = 1;

        // Location Panels:
        int selectDemoLocationPanelId = 10;

        return new()
        {
            SectionedPage.Create(selectDemoPageId, selectDemoLocationPanelId)
        };
    }
}
