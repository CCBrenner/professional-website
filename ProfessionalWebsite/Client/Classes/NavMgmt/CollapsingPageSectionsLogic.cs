using System;

namespace ProfessionalWebsite.Client.Classes.NavMgmt
{
    public class CollapsingPageSectionsLogic
    {
        public CollapsingPageSectionsLogic(string pagePath, List<CollapsingPageSection> sections)
        {
            PagePath = pagePath;
            Sections = sections;
            ASectionIsCurrentlyPromo = false;
            SectionsStatus = SectionsStatus.AllAreOpen;
            SectionsExpanded = Sections.Count();
        }

        public string PagePath { get; private set; }
        public List<CollapsingPageSection> Sections;

        public bool ASectionIsCurrentlyPromo { get; private set; }
        public SectionsStatus SectionsStatus { get; private set; }
        public int SectionsExpanded { get; private set; }
        public void CollapseAllShowOne(int section)
        {
            DemoteAllSections();
            foreach (var sec in Sections)
                sec.ToggleCollapse(true);
            ToggleCollapseSingle(section);
            Sections[section].Promote();
            ASectionIsCurrentlyPromo = true;
            UpdateSectionsStatus();
        }
        public void ToggleCollapseSingle(int section)
        {
            Sections[section].ToggleCollapse(!Sections[section].IsCollapsed);
            UpdateSectionsStatus();
            PromoteIfOnlyOneExpandedSection();
        }
        private void PromoteIfOnlyOneExpandedSection()
        {
            int expandedSectionsCount = 0;
            int promoId = 0;
            for (int i = 0; i < Sections.Count(); i++)
            {
                if (!Sections[i].IsCollapsed)
                {
                    expandedSectionsCount++;
                    promoId = i;
                }
            }
            if (expandedSectionsCount == 1)
                PromoteSection(promoId);
            else
                DemoteAllSections();
        }
        public void ToggleAllSections()
        {
            UpdateSectionsStatus();

            if (SectionsStatus == SectionsStatus.AllAreOpen)
            {
                DemoteAllSections();
                foreach (var section in Sections)
                {
                    section.ToggleCollapse(true);
                }
                SectionsStatus = SectionsStatus.AllAreCollapsed;
            }
            else
            {
                DemoteAllSections();
                foreach (var section in Sections)
                    section.ToggleCollapse(false);
                SectionsStatus = SectionsStatus.AllAreOpen;
            }
        }
        public void PromoteSection(int index)
        {
            DemoteAllSections();
            Sections[index].Promote();
            ASectionIsCurrentlyPromo = true;
        }
        private void DemoteAllSections()
        {
            foreach (var section in Sections)
                section.Demote();
            ASectionIsCurrentlyPromo = false;
        }
        private void UpdateSectionsStatus()
        {
            int openSections = 0;
            for (int i = 0; i < Sections.Count(); i++)
                if (!Sections[i].IsCollapsed)
                    openSections++;

            if (openSections == 0)
                SectionsStatus = SectionsStatus.AllAreCollapsed;
            else if (openSections == Sections.Count())
                SectionsStatus = SectionsStatus.AllAreOpen;
            else
                SectionsStatus = SectionsStatus.AtLeastOneIsOpen;
        }
    }
    public enum SectionsStatus
    {
        AllAreCollapsed,
        AtLeastOneIsOpen,
        AllAreOpen,
    }
}
