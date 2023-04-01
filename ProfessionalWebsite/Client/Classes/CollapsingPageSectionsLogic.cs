using System;

namespace ProfessionalWebsite.Client.Pages
{
    public class CollapsingPageSectionsLogic
    {
        public CollapsingPageSectionsLogic(List<CollapsingPageSection> sections)
        {
            Sections = sections;
            aSectionIsCurrentlyPromo = false;
            sectionsStatus = SectionsStatus.AllAreOpen;
            sectionsExpanded = Sections.Count();
        }
        public List<CollapsingPageSection> Sections;

        public bool ASectionIsCurrentlyPromo { get { return aSectionIsCurrentlyPromo; } }
        private bool aSectionIsCurrentlyPromo;
        public SectionsStatus SectionsStatus { get { return sectionsStatus; } }
        private SectionsStatus sectionsStatus;
        public int SectionsExpanded { get { return sectionsExpanded; } }
        private int sectionsExpanded;
        public void CollapseAllShowOne(int section)
        {
            DemoteAllSections();
            foreach (var sec in Sections)
                sec.ToggleCollapse(true);
            ToggleCollapseSingle(section);
            Sections[section].Promote();
            aSectionIsCurrentlyPromo = true;
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
                sectionsStatus = SectionsStatus.AllAreCollapsed;
            }
            else
            {
                DemoteAllSections();
                foreach (var section in Sections)
                    section.ToggleCollapse(false);
                sectionsStatus = SectionsStatus.AllAreOpen;
            }
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
