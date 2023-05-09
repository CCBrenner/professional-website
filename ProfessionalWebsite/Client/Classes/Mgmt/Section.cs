namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public class Section
    {
        public Section(int id, int sectionedPageId, bool isFirstSectionOfPage = false)
        {
            Id = id;
            SectionedPageId = sectionedPageId;
            IsFirstSectionOfPage = isFirstSectionOfPage;
            IsCollapsed = false;
            IsCollapsedHeader = "";
            IsCollapsedContent = "";
            IsCurrentPromo = false;
        }
        private const string HEADER_COLLAPSED_CLASS_NAME = "collapsed-header";
        private const string CONTENT_COLLAPSED_CLASS_NAME = "collapsed-content";

        public readonly int Id;
        public readonly int SectionedPageId;
        public bool IsFirstSectionOfPage { get; private set; }
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
            IsCollapsedHeader = IsCollapsed ? HEADER_COLLAPSED_CLASS_NAME : "";
            IsCollapsedContent = IsCollapsed ? CONTENT_COLLAPSED_CLASS_NAME : "";
        }
    }
}
