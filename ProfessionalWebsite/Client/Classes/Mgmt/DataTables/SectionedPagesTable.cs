namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class SectionedPagesTable
    {
        private SectionedPagesTable()
        {
            List<Section> sectionsList = new List<Section>();
            for (int i = 0; i < 17; i++)
                sectionsList.Add(new Section());
            SectionedPage pageIndexOfOne = new SectionedPage("knowhow", sectionsList);

            sectionsList = new List<Section>();
            for (int i = 0; i < 9; i++)
                sectionsList.Add(new Section());
            SectionedPage pageIndexOfTwo = new SectionedPage("collyn", sectionsList);

            sectionsList = new List<Section>();
            for (int i = 0; i < 7; i++)
                sectionsList.Add(new Section());
            SectionedPage pageIndexOfThree = new SectionedPage("invent", sectionsList);

            SectionedPages = new List<SectionedPage>()
            {
                new SectionedPage("projects", new List<Section>()),  // not used
                pageIndexOfOne,
                pageIndexOfTwo,
                pageIndexOfThree,
                new SectionedPage("articles", new List<Section>()),  // not used
            };
        }
        private static SectionedPagesTable instance;
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

        public List<SectionedPage> SectionedPages;
    }
}
