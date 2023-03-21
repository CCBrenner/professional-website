namespace ProfessionalWebsite.Client.Pages
{
    public class CollapsingPageSectionsLogic
    {
        public CollapsingPageSectionsLogic(List<CollapsingPageSection> sections)
        {
            Sections = sections;
            allSectionsAreCollapsed = false;
            aSectionIsCurrentlyPromo = false;
            aSectionIsCurrentlyOpen = true;
            sectionsStatus = SectionsStatus.AllAreOpen;
        }
        public List<CollapsingPageSection> Sections;

        public bool AllSectionsAreCollapsed { get { return allSectionsAreCollapsed; } }
        private bool allSectionsAreCollapsed;
        public bool ASectionIsCurrentlyPromo { get { return aSectionIsCurrentlyPromo; } }
        private bool aSectionIsCurrentlyPromo;
        public bool ASectionIsCurrentlyOpen { get { return aSectionIsCurrentlyOpen; } }
        private bool aSectionIsCurrentlyOpen;
        public SectionsStatus SectionsStatus { get { return sectionsStatus; } }
        private SectionsStatus sectionsStatus;
        public void CollapseAllShowOne(int section)
        {
            CloseAll();
            ToggleCollapseSingle(section);
            Sections[section].Promote();
            aSectionIsCurrentlyPromo = true;
            UpdateASectionIsCurrentlyOpen();
            UpdateSectionsStatus();
        }
        public void ToggleCollapseSingle(int section)
        {
            DemoteAllSections();
            Sections[section].ToggleCollapse(!Sections[section].IsCollapsed);
            allSectionsAreCollapsed = true;
            foreach (var sec in Sections)
            {
                if (!sec.IsCollapsed)
                {
                    allSectionsAreCollapsed = false;
                }
            }
            IfOnlyOneSectionOpenThenMoveThatSectionToTop();
            UpdateASectionIsCurrentlyOpen();
            UpdateSectionsStatus();
        }
        private void CloseAll()
        {
            DemoteAllSections();
            foreach (var section in Sections)
            {
                section.ToggleCollapse(true);
            }
        }
        public void ToggleAllSections()
        {
            bool atleastOneSectionIsOpen = false;
            foreach (var section in Sections)
            {
                if (!section.IsCollapsed)
                {
                    atleastOneSectionIsOpen = true;
                    break;
                }
            }

            bool allSectionsAreOpen = false;
            int openSections = 0;
            foreach (var section in Sections)
            {
                if (!section.IsCollapsed)
                {
                    openSections++;
                }
            }
            if (openSections == Sections.Count()) 
                allSectionsAreOpen = true;

            if (AllSectionsAreCollapsed || atleastOneSectionIsOpen && !allSectionsAreOpen)
            {
                OpenAll();
                allSectionsAreCollapsed = false;
            }
            else
            {
                CloseAll();
                allSectionsAreCollapsed = true;
            }
            UpdateASectionIsCurrentlyOpen();
            UpdateSectionsStatus();
        }

        private void OpenAll()
        {
            DemoteAllSections();
            foreach (var section in Sections)
                section.ToggleCollapse(false);
        }
        public void PromoteSection(int index)
        {
            DemoteAllSections();
            Sections[index].Promote();
            aSectionIsCurrentlyPromo = true;
        }
        private void DemoteAllSections()
        {
            foreach(var section in Sections)
                section.Demote();
            aSectionIsCurrentlyPromo = false;
        }
        private void IfOnlyOneSectionOpenThenMoveThatSectionToTop()
        {
            int nonCollapsedCount = 0;
            int sec = 0;
            for(int i = 0; i < Sections.Count(); i++)
            {
                if (!Sections[i].IsCollapsed)
                {
                    nonCollapsedCount++;
                    sec = i;
                }
            }

            if (nonCollapsedCount == 1)
                PromoteSection(sec);
            else
                DemoteAllSections();
        }
        private void UpdateASectionIsCurrentlyOpen()
        {
            aSectionIsCurrentlyOpen = false;
            foreach(var section in Sections)
            {
                if (!section.IsCollapsed)
                {
                    aSectionIsCurrentlyOpen = true;
                    break;
                }
            }
        }
        private void UpdateSectionsStatus()
        {
            int openSections = 0;
            for (int i = 0; i < Sections.Count(); i++)
                if (!Sections[i].IsCollapsed)
                    openSections++;

            if (openSections == 0)
                sectionsStatus = SectionsStatus.AllAreCollapsed;
            else if (openSections == Sections.Count())
                sectionsStatus = SectionsStatus.AllAreOpen;
            else
                sectionsStatus = SectionsStatus.AtLeastOneIsOpen;
        }
    }
    public enum SectionsStatus
    {
        AllAreCollapsed,
        AtLeastOneIsOpen,
        AllAreOpen,
    }
}
