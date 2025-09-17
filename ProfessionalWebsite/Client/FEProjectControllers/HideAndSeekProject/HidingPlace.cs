namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public class HidingPlace
{
    public HidingPlace(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }
    public List<Opponent> HiddenOpponents = new();
    public void Hide(Opponent opponent) => HiddenOpponents.Add(opponent);
    public void ClearOpponents() => HiddenOpponents.Clear();
    public IEnumerable<Opponent> GetHiddenOpponents() => HiddenOpponents.ToList();
}
