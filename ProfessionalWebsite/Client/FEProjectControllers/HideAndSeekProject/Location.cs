namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public class Location
{
    private const string NULL_LOCATION = "Null Location";
    private const string NON_HIDING_PLACE = "19827049729943";
    private Location(string name, string nameOfHidingPlace)
    {
        Name = name;
        HidingPlace = new(nameOfHidingPlace);
        Exits = new();
    }
    public static Location Create(string name) =>
        new(name, NON_HIDING_PLACE);
    public static Location Create(string name, string nameOfHidingPlace) =>
        new(name, nameOfHidingPlace);
    public static Location CreateNullLocation() =>
        new(NULL_LOCATION, NON_HIDING_PLACE);    
    public string Name { get; set; }
    public Dictionary<Direction, Location> Exits { get; private set; }
    public HidingPlace HidingPlace { get; private set; }
    public bool IsNullLocation => Name == NULL_LOCATION;
    public bool HasHidingPlace => HidingPlace.Name != NON_HIDING_PLACE;

    public override string ToString() => Name;
    public IEnumerable<string> ExitList =>
        Exits
        .OrderBy(keyValuePair => (int)keyValuePair.Key)
        .OrderBy(keyValuePair => Math.Abs((int)keyValuePair.Key))
        .Select(keyValuePair => $" - the {keyValuePair.Value} is {ExitListDirection(keyValuePair.Key)}");
    private string ExitListDirection(Direction d) => d switch
    {
        Direction.Up => "Up",
        Direction.Down => "Down",
        Direction.In => "In",
        Direction.Out => "Out",
        _ => $"to the {d}",
    };
    public void AddExit(Direction direction, Location connectingLocation) =>
        Exits.Add(direction, connectingLocation);
    private void AddConnectingLocationExit(Direction direction, Location connectingLocation) =>
        connectingLocation.Exits.Add((Direction)(-(int)direction), this);
    public void AddExitsOfConnectedLocations(Direction direction, Location connectingLocation)
    {
        AddExit(direction, connectingLocation);
        AddConnectingLocationExit(direction, connectingLocation);
    }
    public Location GetExit(Direction d) => Exits[d];
}
