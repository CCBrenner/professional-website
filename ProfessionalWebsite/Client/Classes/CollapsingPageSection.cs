using static System.Collections.Specialized.BitVector32;

namespace ProfessionalWebsite.Client.Pages
{
    public class CollapsingPageSection
    {
        public CollapsingPageSection() 
        {
            isCollapsed = false;
            isCollapsedHeader = "";
            isCollapsedContent = "";
            isCurrentPromo = false;
        }
        public bool IsCollapsed { get { return isCollapsed; } }
        private bool isCollapsed;
        public string IsCollapsedHeader { get { return isCollapsedHeader; } }
        private string isCollapsedHeader;
        public string IsCollapsedContent { get { return isCollapsedContent; } }
        private string isCollapsedContent;
        public bool IsCurrentPromo { get { return isCurrentPromo; } }
        private bool isCurrentPromo;
        public void ToggleCollapse()
        {
            isCollapsed = !IsCollapsed;
            UpdateHeaderAndContent();
        }
        public void ToggleCollapse(bool toggle)
        {
            isCollapsed = toggle;
            if (IsCollapsed) isCurrentPromo = false;
            UpdateHeaderAndContent();
        }
        public void CollapseAndDemote()
        {
            isCollapsed = false;
            isCurrentPromo = false;
        }
        public void Promote()
        {
            if (!isCollapsed)
                isCurrentPromo = true;

        }
        public void Demote() =>
            isCurrentPromo = false;
        private void UpdateHeaderAndContent()
        {
            isCollapsedHeader = IsCollapsed ? "collapsed-header" : "";
            isCollapsedContent = IsCollapsed ? "collapsed-content" : "";
        }
    }
}
