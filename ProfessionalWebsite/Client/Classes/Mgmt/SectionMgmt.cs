﻿using ProfessionalWebsite.Client.Classes.Mgmt.DataTables;

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

        /*
            Definitions:
                - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
                - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
        */

        /// <summary>
        /// Collapses all sections and promotes one section to the top of the sectioned page.
        /// </summary>
        /// <param name="sectionId">ID of the section that is being promoted/which has been selected.</param>
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
        /// <summary>
        /// Collapses/Expands section based on section ID.
        /// </summary>
        /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
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
        /// <summary>
        /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
        /// </summary>
        /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
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
        /// <summary>
        /// A logic check used by sections in their housing component to determine whether or not they should be showing or not. Works in conjunction with dopelganger section header which, when a section is promoted, all other sections disappear and their dopelgangers, which are located beneath all actual sections, become visible. This creates an illusion of the promoted section being brought to the top of the sectioned page.
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns>Boolean value stating whether a section is supposed to be visible or not.</returns>
        public bool SectionIsVisible(int sectionId)
        {
            SectionedPage sectionedPage = SectionedPages[Sections[sectionId].SectionedPageId];
            return !sectionedPage.ASectionIsCurrentlyPromo || sectionedPage.ASectionIsCurrentlyPromo && Sections[sectionId].IsCurrentPromo;
        }
        /// <summary>
        /// Demotes all other sections and makes specified section the promo section.
        /// </summary>
        /// <param name="sectionId">ID of section to be made promo section.</param>
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
        /// <summary>
        /// Returns the ID of the location panel of a sectioned page of the specified section using the section's ID.
        /// </summary>
        /// <param name="sectionId">ID of section used to get the sectioned page's location panel's ID</param>
        /// <returns></returns>
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
        /// <summary>
        /// Removes promo status from all sections.
        /// </summary>
        private void DemoteAllSections()
        {
            foreach (var section in Sections.Values)
                section.Demote();
            foreach (SectionedPage sectionedPage in SectionedPages.Values)
                sectionedPage.ASectionIsCurrentlyPromo = false;
        }
        /// <summary>
        /// Finds the sections of the sectioned page that the specified section is a part of, counts how many sections are currently expanded, and then sets the status based on the number of expanded sections.
        /// </summary>
        /// <param name="sectionId">ID of specified section.</param>
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
        /// <summary>
        /// Initializes sectioned page reference to its sections and vice versa. Establishes a one-to-many relationship through on-hand references.
        /// </summary>
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
        /// <summary>
        /// If at any point in time, if there is only one section in a sectioned page that is expanded, then promote that section.
        /// </summary>
        /// <param name="sectionId">ID of section used to determine the sectioned page to check for promo.</param>
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
        /// <summary>
        /// Updates the component that consumes it when a method in the PanelMgmt class that consumes this method invokes/signals that a change to the state of it has occurred.
        /// </summary>
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
