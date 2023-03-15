namespace ProfessionalWebsite.Client.Pages
{
    public class CollapsingPageSectionsLogic
    {
        public CollapsingPageSectionsLogic(List<CollapsingPageSection> sections) 
        {
            Sections = sections;
        }
        public List<CollapsingPageSection> Sections;

        public bool AllSectionsAreCollapsed = false;

        public bool IsCollapsed { get; set; } = true;
        public void CollapseAllShowOne(int section)
        {
            CloseAll();
            ToggleCollapseSingle(section);
        }
        public void ToggleCollapseSingle(int sec)
        {
            Sections[sec].ToggleCollapse(!Sections[sec].IsCollapsed);
            AllSectionsAreCollapsed = true;
            foreach (var section in Sections)
            {
                if (!section.IsCollapsed)
                {
                    AllSectionsAreCollapsed = false;
                }
            }
        }
        private void CloseAll()
        {
            foreach (var section in Sections)
                section.ToggleCollapse(true);
        }
        public void ToggleAllSections()
        {
            if (AllSectionsAreCollapsed)
            {
                OpenAll();
                AllSectionsAreCollapsed = false;
            }
            else
            {
                CloseAll();
                AllSectionsAreCollapsed = true;
            }
        }
        private void OpenAll()
        {
            foreach (var section in Sections)
                section.ToggleCollapse(false);
        }
    }
}
