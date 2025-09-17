namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public static class House
{
    static House()
    {
        Entry =                                                    Location.Create("Entry");
        Entry.AddExitsOfConnectedLocations(Direction.East,         Location.Create("Hallway"));
        Entry.AddExitsOfConnectedLocations(Direction.Out,          Location.Create("Garage", "behind the car"));

        Location hallway = Entry.Exits[Direction.East];
        hallway.AddExitsOfConnectedLocations(Direction.Northwest,  Location.Create("Kitchen", "next to the stove"));
        hallway.AddExitsOfConnectedLocations(Direction.North,      Location.Create("Downstairs Bathroom", "behind the door"));
        hallway.AddExitsOfConnectedLocations(Direction.South,      Location.Create("Living Room"));
        hallway.AddExitsOfConnectedLocations(Direction.Up,         Location.Create("Landing"));

        Location landing = hallway.Exits[Direction.Up];
        landing.AddExitsOfConnectedLocations(Direction.Northwest,  Location.Create("Master Bedroom"));
        landing.AddExitsOfConnectedLocations(Direction.West,       Location.Create("Upstairs Bathroom"));
        landing.AddExitsOfConnectedLocations(Direction.Southwest,  Location.Create("Nursery"));
        landing.AddExitsOfConnectedLocations(Direction.South,      Location.Create("Pantry", "inside a cabinet"));
        landing.AddExitsOfConnectedLocations(Direction.Southeast,  Location.Create("Kids Room"));
        landing.AddExitsOfConnectedLocations(Direction.Up,         Location.Create("Attic", "in a trunk"));

        Location masterBedroom = landing.Exits[Direction.Northwest];
        masterBedroom.AddExitsOfConnectedLocations(Direction.East, Location.Create("Master Bathroom"));

        locationHubs = new List<Location>()
        {
            Entry,
            hallway,
            landing,
            masterBedroom,
        };

        List<Location> locationsList = new();
        foreach (Location locationHub in locationHubs)
            foreach (Location exitLocations in locationHub.Exits.Values)
                if (!locationsList.Contains(exitLocations))
                    locationsList.Add(exitLocations);
        Locations = locationsList;
    }
    public static Random Random = new Random();
    public static Location Entry { get; private set; }
    private static IEnumerable<Location> locationHubs;
    public static IEnumerable<Location> Locations = new List<Location>();
    public static string S(int count) => count == 1 ? "" : "s";
    public static Location GetLocationByName(string name)
    {
        foreach (Location location in Locations)
            if (location.Name == name)
                return location;
        return Location.CreateNullLocation();
    }
    public static Location GetLocationWithHidingPlaceByName(string name)
    {
        foreach (Location location in Locations)
            if (location.HasHidingPlace)
                if (location.HidingPlace.Name == name)
                    return location;
        return Location.CreateNullLocation();
    }
    public static Location RandomExit(Location location)
    {
        IOrderedEnumerable<Location>? locations =
            location.Exits
            .Select(exit => exit.Value)
            .OrderBy(selectLocation => selectLocation.Name);
        return locations.ToList()[Random.Next(locations.Count())];
    }
    public static void ClearHidingPlaces()
    {
        foreach (Location location in Locations)
            if (location.HasHidingPlace)
                location.HidingPlace.ClearOpponents();
    }

    public static void NewRandomizer() => Random = new Random();
}
