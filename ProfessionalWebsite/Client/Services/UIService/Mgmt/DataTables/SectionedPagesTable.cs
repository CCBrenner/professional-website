namespace ProfessionalWebsite.Client.Services.UIService.Mgmt.DataTables
{
    public sealed class SectionedPagesTable
    {
        public SectionedPagesTable()
        {
            SectionedPages = new List<SectionedPage>()
            {
                new SectionedPage(0, 2, "projects"),  // not used
                new SectionedPage(1, 3, "knowhow"),
                new SectionedPage(2, 4, "collyn"),
                new SectionedPage(3, 5, "invent"),
                new SectionedPage(4, 6, "articles"),  // not used
            };

            SectionedPagesDict = new Dictionary<int, SectionedPage>();
            foreach (SectionedPage sectionedPage in SectionedPages)
                SectionedPagesDict.Add(sectionedPage.Id, sectionedPage);
        }

        public List<SectionedPage> SectionedPages { get; private set; }
        public Dictionary<int, SectionedPage> SectionedPagesDict { get; private set; }
    }
}
