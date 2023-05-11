using ProfessionalWebsite.Client.Classes.Mgmt.DataTables;

namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class SectionMgmt
    {
        private SectionMgmt()
        {
            foreach (Section section in SectionsTable.Instance.Sections)
                Sections.Add(section.Id, section);
            foreach (SectionedPage sectionedPage in SectionedPagesTable.Instance.SectionedPages)
                SectionedPages.Add(sectionedPage.Id, sectionedPage);
            SetInstanceToGroupReferences();
        }
        private static SectionMgmt? instance;
        private static object lockObject = new object();
        public static SectionMgmt Instance
        {
            get
            {
                lock(lockObject)
                {
                    if(instance == null)
                        instance = new SectionMgmt();
                    return instance;
                }
            }
        }

        public Dictionary<int, Section> Sections = new Dictionary<int, Section>();
        public Dictionary<int, SectionedPage> SectionedPages = new Dictionary<int, SectionedPage>();  // key == SectionPage.Id

        public bool ASectionIsCurrentlyPromo { get; private set; }
        public event Action<string> OnSectionMgmtChanged;
        public void CollapseAllShowOne(int sectionId)
        {
            try
            {
                DemoteAllSections();
                Section section = Sections[sectionId];
                SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
                Dictionary<int, Section> sectionsOfSectionedPage = sectionedPage.Sections;
                if (section.IsFirstSectionOfPage)
                {
                    foreach (var sec in sectionsOfSectionedPage.Values)
                        sec.ToggleCollapse(false);
                    sectionedPage.ASectionIsCurrentlyPromo = false;
                }
                else
                {
                    foreach (var sec in sectionsOfSectionedPage.Values)
                        sec.ToggleCollapse(true);
                    ToggleCollapseSingle(sectionId);
                    section.Promote();
                    sectionedPage.ASectionIsCurrentlyPromo = true;
                }
                UpdateSectionsStatus(sectionId);
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"{knfEx.Message}\n{knfEx.StackTrace}");
            }
        }
        public void ToggleCollapseSingle(int sectionId)
        {
            try
            {
                Section section = Sections[sectionId];
                section.ToggleCollapse(!section.IsCollapsed);
                UpdateSectionsStatus(sectionId);
                PromoteIfOnlyOneExpandedSection(sectionId);
                RaiseEventOnSectionMgmtChanged();
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
            }
        }
        public void ToggleAllSections(int pageId)
        {
            try
            {
                int sectionId = SectionedPages[pageId].Sections.Keys.FirstOrDefault();
                UpdateSectionsStatus(sectionId);

                DemoteAllSections();
                if (SectionedPages[pageId].SectionsStatus == SectionsStatus.AllAreOpen)
                {
                    foreach (var section in Sections.Values)
                        section.ToggleCollapse(true);
                    SectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreCollapsed;
                }
                else
                {
                    foreach (var section in Sections.Values)
                        section.ToggleCollapse(false);
                    SectionedPages[pageId].SectionsStatus = SectionsStatus.AllAreOpen;
                }
                RaiseEventOnSectionMgmtChanged();
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
            }

        }
        public bool SectionIsExpanded(int sectionId)
        {
            SectionedPage sectionedPage = SectionedPages[Sections[sectionId].SectionedPageId];
            return !sectionedPage.ASectionIsCurrentlyPromo || sectionedPage.ASectionIsCurrentlyPromo && Sections[sectionId].IsCurrentPromo;
        }
        public void PromoteSection(int sectionId)
        {
            try
            {
                DemoteAllSections();
                Section section = Sections[sectionId];
                section.Promote();
                SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
                sectionedPage.ASectionIsCurrentlyPromo = true;
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
            }
        }
        public int GetLocationPanelGroupId(int sectionId)
        {
            try
            {
                Section section = Sections[sectionId];
                SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];
                return sectionedPage.LocationPanelGroupId;
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
            }
            return -1;
        }
        private void DemoteAllSections()
        {
            foreach (var section in Sections.Values)
                section.Demote();
            foreach (SectionedPage sectionedPage in SectionedPages.Values)
                sectionedPage.ASectionIsCurrentlyPromo = false;
        }
        private void UpdateSectionsStatus(int sectionId)
        {
            Section section = Sections[sectionId];
            SectionedPage sectionedPage = SectionedPages[section.SectionedPageId];

            int openSections = 0;
            foreach (Section sec in sectionedPage.Sections.Values)
                if (!sec.IsCollapsed)
                    openSections++;

            if (openSections == 0)
                sectionedPage.SectionsStatus = SectionsStatus.AllAreCollapsed;
            else if (openSections == sectionedPage.Sections.Count)
                sectionedPage.SectionsStatus = SectionsStatus.AllAreOpen;
            else
                sectionedPage.SectionsStatus = SectionsStatus.AtLeastOneIsOpen;
        }
        private void SetInstanceToGroupReferences()
        {
            foreach (Section section in Sections.Values)
                section.SetInstanceToGroupRelationship(SectionedPages.Values.ToList());

            List<Section> sectionsOfPage = new List<Section>();
            foreach (Section section in Sections.Values)
            {
                SectionedPages[section.SectionedPageId]
                    .Sections
                    .Add(section.Id, section);
            }
        }
        private void PromoteIfOnlyOneExpandedSection(int sectionId)
        {
            try
            {
                int expandedSectionsCount = 0;
                int idOfSectionToPromote = 0;

                Section sec = Sections[sectionId];
                SectionedPage sectionedPage = SectionedPages[sec.SectionedPageId];
                Dictionary<int, Section> sectionsOfSectionedPage = sectionedPage.Sections;

                foreach (Section section in sectionsOfSectionedPage.Values)
                {
                    if (!section.IsCollapsed)
                    {
                        expandedSectionsCount++;
                        idOfSectionToPromote = section.Id;
                    }
                }

                if (expandedSectionsCount == 1)
                    PromoteSection(idOfSectionToPromote);
                else
                    DemoteAllSections();
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"Error: {knfEx.Message}\n{knfEx.StackTrace}");
            }
        }
        private void RaiseEventOnSectionMgmtChanged()
        {
            OnSectionMgmtChanged?.Invoke("");
        }
    }
    public enum SectionsStatus
    {
        AllAreCollapsed,
        AtLeastOneIsOpen,
        AllAreOpen,
    }
}
