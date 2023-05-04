namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public class Section
    {
        public Section()
        {
            IsCollapsed = false;
            IsCollapsedHeader = "";
            IsCollapsedContent = "";
            IsCurrentPromo = false;
        }
        public bool IsCollapsed { get; private set; }
        public string IsCollapsedHeader { get; private set; }
        public string IsCollapsedContent { get; private set; }
        public bool IsCurrentPromo { get; private set; }
        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
            UpdateHeaderAndContent();
        }
        public void ToggleCollapse(bool isCollapsed)
        {
            IsCollapsed = isCollapsed;
            if (IsCollapsed) IsCurrentPromo = false;
            UpdateHeaderAndContent();
        }
        public void CollapseAndDemote()
        {
            IsCollapsed = false;
            IsCurrentPromo = false;
        }
        public void Promote()
        {
            if (!IsCollapsed)
                IsCurrentPromo = true;
        }
        public void Demote() =>
            IsCurrentPromo = false;
        private void UpdateHeaderAndContent()
        {
            IsCollapsedHeader = IsCollapsed ? "collapsed-header" : "";
            IsCollapsedContent = IsCollapsed ? "collapsed-content" : "";
        }
    }
}
