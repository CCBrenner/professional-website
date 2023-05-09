namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class SectionedPagesTable
    {
        private SectionedPagesTable() { }
        private static SectionedPagesTable? instance;
        private static object instanceLock = new object();
        public static SectionedPagesTable Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new SectionedPagesTable();
                    return instance;
                }
            }
        }

        public List<SectionedPage> SectionedPages = new List<SectionedPage>()
        {
            new SectionedPage(0, 2, "projects"),  // not used
            new SectionedPage(1, 3, "knowhow"),
            new SectionedPage(2, 4, "collyn"),
            new SectionedPage(3, 5, "invent"),
            new SectionedPage(4, 6, "articles"),  // not used
        };
    }
}
