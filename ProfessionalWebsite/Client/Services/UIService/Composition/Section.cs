﻿namespace ProfessionalWebsite.Client.Services.UI;

public class Section
{
    private Section(int id, int sectionedPageId, bool isFirstSectionOfPage = false)
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
    public SectionedPage SectionedPage;
    public bool IsFirstSectionOfPage { get; private set; }
    public bool IsVisible => !SectionedPage.ASectionIsCurrentlyPromo || SectionedPage.ASectionIsCurrentlyPromo && IsCurrentPromo;
    public bool IsCollapsed { get; private set; }
    public string IsCollapsedHeader { get; private set; }
    public string IsCollapsedContent { get; private set; }
    public bool IsCurrentPromo { get; private set; }
    public static Section CreateRegularSection(int id, int sectionedPageId) =>
        new(id, sectionedPageId, false);
    public static Section CreateAsFirstSectionOfPage(int id, int sectionedPageId) =>
        new(id, sectionedPageId, true);
    public void SetInstanceToGroupRelationship(List<SectionedPage> sectionedPages)
    {
        try
        {
            SectionedPage = (from sectionedPage in sectionedPages
                             where sectionedPage.Id == SectionedPageId
                             select sectionedPage).FirstOrDefault();
        }
        catch (NullReferenceException nrEx)
        {
            Console.WriteLine($"Error: NullReferenceException - Could not establish Instance-Group relationship between Section and SectionedPage\n" +
                $"{nrEx.Message}\n{nrEx.StackTrace}");
        }
    }
    public void ToggleCollapse()
    {
        IsCollapsed = !IsCollapsed;
        UpdateHeaderAndContent();
    }
    public void Collapse()
    {
        IsCollapsed = true;
        IsCurrentPromo = false;
        UpdateHeaderAndContent();
    }
    public void Expand()
    {
        IsCollapsed = false;
        UpdateHeaderAndContent();
    }
    public void Promote()
    {
        //if (!IsCollapsed)  <= this was being used for something but ultimately does not belong here; there are fewer failing tests still, which is a nice outcome so far. Fix this in whatever other location it should be fixed - not here. Delete this comment when all tests are passing.
        IsCurrentPromo = true;
    }
    public void Demote() =>
        IsCurrentPromo = false;
    private void UpdateHeaderAndContent()
    {
        IsCollapsedHeader = IsCollapsed ? HEADER_COLLAPSED_CLASS_NAME : "";
        IsCollapsedContent = IsCollapsed ? CONTENT_COLLAPSED_CLASS_NAME : "";
    }
    public void SetSectionedPageReference(SectionedPage sectionedPage)
    {
        SectionedPage = sectionedPage;
    }
}
