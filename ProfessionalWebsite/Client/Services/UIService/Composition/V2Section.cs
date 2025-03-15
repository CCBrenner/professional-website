namespace ProfessionalWebsite.Client.Services.UI;

public class V2Section
{
    public V2Section(int id, string name)
    {
        Id = id;
        Name = name;
        IsOpen = true;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public bool IsOpen { get; private set; }  // or "IsVisible"
    public void Close() => IsOpen = false;
    public void Open() => IsOpen = true;
    public void Toggle() => IsOpen = !IsOpen;
}
