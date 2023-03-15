namespace ProfessionalWebsite.Client.Pages
{
    public class CollapsingPageSection
    {
        public bool IsCollapsed { get; set; } = false;
        public string IsCollapsedHeader = "";
        public string IsCollapsedContent = "";
        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
            UpdateHeaderAndContent();
        }
        public void ToggleCollapse(bool toggle)
        {
            IsCollapsed = toggle;
            UpdateHeaderAndContent();
        }
        private void UpdateHeaderAndContent()
        {
            IsCollapsedHeader = IsCollapsed ? "collapsed-header" : "";
            IsCollapsedContent = IsCollapsed ? "collapsed-content" : "";
        }
    }
}
