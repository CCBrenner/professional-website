namespace ProfessionalWebsite.Client.Services.UI;

public class V2SectionedPagesTable
{
    public static Dictionary<int, V2SectionedPage> GetDictionary()
    {
        Dictionary<int, V2SectionedPage> pages = new();
        foreach (var page in GetList())
        {
            pages.Add(page.Id, page);
        }
        return pages;
    }
    public static List<V2SectionedPage> GetList()
    {
        // Pages:
        int selectDemoPageId = 1;

        // Location Panels:
        int selectDemoLocationPanelId = 10;

        return new()
        {
            V2SectionedPage.Create(selectDemoPageId, selectDemoLocationPanelId)
        };
    }
}
