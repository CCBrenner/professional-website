namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public class SaveData
{
    public SaveData(Location currentLocation, int moveNumber, string status, List<Opponent> foundOpponents)
    {
        CurrentLocation = currentLocation;
        MoveNumber = moveNumber;
        Status = status;
        FoundOpponents = foundOpponents;
    }
    public Location CurrentLocation { get; private set; }
    public int MoveNumber { get; private set; }
    public string Status { get; private set; }
    public List<Opponent> FoundOpponents { get; private set; }
}
