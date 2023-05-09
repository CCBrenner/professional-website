﻿using ProfessionalWebsite.Client.Classes.Mgmt.DataTables;

namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public partial class SectionedPage
    {
        public SectionedPage(int id, int locationPanelGroupId, string pagePath)
        {
            Id = id;
            LocationPanelGroupId = locationPanelGroupId;
            PagePath = pagePath;
            Sections = GetSectionsOfPage(Id, SectionsTable.Instance.Sections);
            ASectionIsCurrentlyPromo = false;
            SectionsStatus = SectionsStatus.AllAreOpen;
            SectionsExpanded = Sections.Count();
        }

        public readonly int Id;
        public readonly int LocationPanelGroupId;
        public string PagePath { get; private set; }
        public List<Section> Sections;

        public bool ASectionIsCurrentlyPromo { get; private set; }
        public SectionsStatus SectionsStatus { get; private set; }
        public int SectionsExpanded { get; private set; }
        public void CollapseAllShowOne(int sectionId)
        {
            DemoteAllSections();
            Section? section = GetSection(sectionId);
            if (section != null)
            {
                bool isFirstSectionOfPage = section.IsFirstSectionOfPage;
                if (isFirstSectionOfPage)
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
                    ToggleCollapseSingle(sectionId);
                    section.Promote();
                    ASectionIsCurrentlyPromo = true;
                }
                UpdateSectionsStatus();
            }
        }
        public void ToggleCollapseSingle(int sectionId)
        {
            Section? section = GetSection(sectionId);
            if (section != null)
            {
                section.ToggleCollapse(!section.IsCollapsed);
                UpdateSectionsStatus();
                PromoteIfOnlyOneExpandedSection();
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
                    promoId = Sections[i].Id;
                }
            }
            Console.WriteLine($"expandedSectionsCount: {expandedSectionsCount}\npromoId: {promoId}");
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
        public void PromoteSection(int sectionId)
        {
            DemoteAllSections();
            Section? section = GetSection(sectionId);
            if (section != null)
            {
                section.Promote();
                ASectionIsCurrentlyPromo = true;
            }
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
            try
            {
                return (from section in Sections
                        where section.Id == sectionId
                        select section).FirstOrDefault();
            }
            catch (NullReferenceException nrEx)
            {
                Console.WriteLine($"Error with GET Section (by ID)\n{nrEx.Message}\n{nrEx.StackTrace}");
            }
            return default;
        }
        private List<Section> GetSectionsOfPage(int pageId, List<Section> sectionsTableInstance)
        {
            return (from section in sectionsTableInstance
                    where section.SectionedPageId == pageId
                    select section).ToList();
        }
    }
    public enum SectionsStatus
    {
        AllAreCollapsed,
        AtLeastOneIsOpen,
        AllAreOpen,
    }
}