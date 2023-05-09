using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public partial class SectionedPage0
    {
        public SectionedPage0(int id, string pagePath, List<Section> sections)
        {
            Id = id;
            PagePath = pagePath;
            Sections = sections;
            ASectionIsCurrentlyPromo = false;
            SectionsStatus = SectionsStatus.AllAreOpen;
            SectionsExpanded = Sections.Count();
        }

        public readonly int Id;
        public string PagePath { get; private set; }
        public List<Section> Sections;

        public bool ASectionIsCurrentlyPromo { get; private set; }
        public SectionsStatus SectionsStatus { get; private set; }
        public int SectionsExpanded { get; private set; }
        public void CollapseAllShowOne(int section)
        {
            try
            {
                DemoteAllSections();
                if (section == 0)
                {
                    // section[0] always has all sections open
                    foreach (var sec in Sections)
                        sec.ToggleCollapse(false);
                    ASectionIsCurrentlyPromo = false;
                }
                else
                {
                    foreach (var sec in Sections)
                        sec.ToggleCollapse(true);
                    ToggleCollapseSingle(section);
                    Sections[section].Promote();
                    ASectionIsCurrentlyPromo = true;
                }
                UpdateSectionsStatus();
            }
            catch (ArgumentOutOfRangeException aoorEx)
            {
                Console.WriteLine($"{aoorEx.Message} - origin method: SectionedPage.CollapseAllShowOne()");
            }
        }
        public void ToggleCollapseSingle(int sectionIndex)
        {
            Sections[sectionIndex].ToggleCollapse(!Sections[sectionIndex].IsCollapsed);
            UpdateSectionsStatus();
            PromoteIfOnlyOneExpandedSection();
        }
        public void ToggleCollapseSingleById(int sectionId)
        {
            try
            {
                Section? section = GetSection(sectionId);
                section.ToggleCollapse(!section.IsCollapsed);

                UpdateSectionsStatus();
                PromoteIfOnlyOneExpandedSection();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"NullReferenceException error. Section does not exist - Section ID: {sectionId}");
            }
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
        private Section? GetSection(int sectionId)
        {
            return (from section in Sections
                    where section.Id == sectionId
                    select section).FirstOrDefault();
        }
    }
    /*
    public enum SectionsStatus
    {
        AllAreCollapsed,
        AtLeastOneIsOpen,
        AllAreOpen,
    }*/
}
