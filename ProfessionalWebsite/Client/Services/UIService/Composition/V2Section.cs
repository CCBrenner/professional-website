namespace ProfessionalWebsite.Client.Services.UI;

public class V2Section
{
    public V2Section(int id, string name, int pageId)
    {
        Id = id;
        Name = name;
        PageId = pageId;
        IsOpen = true;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int PageId { get; private set; }
    public bool IsOpen { get; private set; }  // or "IsVisible"
    public static V2Section Create(int id, string name, int pageId) => new V2Section(id, name, pageId);
    public void Close() => IsOpen = false;
    public void Open() => IsOpen = true;
    public void Toggle() => IsOpen = !IsOpen;
}
