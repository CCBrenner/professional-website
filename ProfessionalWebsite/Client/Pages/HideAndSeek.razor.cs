using ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;
using System.Globalization;

namespace ProfessionalWebsite.Client.Pages;

public partial class HideAndSeek
{
    // Init:
    protected override void OnInitialized()
    {
        SetNewGame();
    }

    // State:
    public UiVersion CurrentUiVersion { get; private set; } = UiVersion.Two;
    private Dictionary<UiVersion, string> selectUiCSS = new()
    {
        { UiVersion.One, "btn-secondary" },
        { UiVersion.Two, "btn-primary" },
    };
    private ControlsOption currentControls = ControlsOption.Play;
    private Dictionary<Direction, string> directionsCSS = new()
    {
        { Direction.North,     "button-inactive" },
        { Direction.South,     "button-inactive" },
        { Direction.East,      "button-inactive" },
        { Direction.West,      "button-inactive" },
        { Direction.Northwest, "button-inactive" },
        { Direction.Northeast, "button-inactive" },
        { Direction.Southwest, "button-inactive" },
        { Direction.Southeast, "button-inactive" },
        { Direction.Up,        "button-inactive" },
        { Direction.Down,      "button-inactive" },
        { Direction.In,        "button-inactive" },
        { Direction.Out,       "button-inactive" },
    };

    private string status = string.Empty;
    public Location CurrentLocation { get; private set; }
    public int MoveNumber { get; set; } = 1;
    public string FileNameToBeSaved { get; set; } = string.Empty;
    public string FileNameToBeLoaded { get; set; } = string.Empty;
    public string ParsedOutput { get; private set; } = string.Empty;
    public Direction SelectedDirection { get; set; }
    public List<Opponent> FoundOpponents = new();
    public IEnumerable<Opponent> Opponents = new List<Opponent>()
    {
        new Opponent("Joe"),
        new Opponent("Bob"),
        new Opponent("Ana"),
        new Opponent("Owen"),
        new Opponent("Jimmy"),
    };

    // Methods:
    private void SetNewGame()
    {
        FoundOpponents = new();
        MoveNumber = 1;

        House.ClearHidingPlaces();
        House.NewRandomizer();
        foreach (Opponent opponent in Opponents)
            opponent.Hide();

        CurrentLocation = House.Entry;

        ResetDirections();
        SetDirections();

        StateHasChanged();
    }
    public void Move()
    {
        Move(SelectedDirection);
    }
    public void Move(Direction selectedDirection)
    {
        if (GameIsOver) return;

        Console.WriteLine(
            $"Selected Direction: {selectedDirection.ToString()}" +
            $"New Current Location: {CurrentLocation.ToString()}");

        ParsedOutput = ParseInput(selectedDirection.ToString());
        SelectedDirection = CurrentLocation.Exits.ToList()[0].Key;
        ResetDirections();
        SetDirections();
    }
    public string StatusV1 =>
        GameIsOver
            ? $"You won the game in {MoveNumber} moves!" +
                $"<br>Press \"Restart\" to restart to restart the game or press \"Quit\" to quit."
            : $"You are {HandleLandingGrammar(CurrentLocation.Name)} the {CurrentLocation.Name}. " +
                $"You see the following exits:" +
                $"<br><br>" +
                $"{string.Join($"<br>", CurrentLocation.ExitList)}" +
                $"<br>{MentionHidingPlace(CurrentLocation)}" +
                $"<br>{MoveNumber}: What do you want to do? (or click 'Check')";
    public string Status() => ParsedOutput + "<br>" + status + (GameIsOver ? "" : "<br>" + Prompt);
    public bool IsCurrentControls(ControlsOption option) => currentControls == option;
    public string GetDirectionCSS(Direction direction) => directionsCSS[direction];
    public void ResetDirections()
    {
        foreach (var key in directionsCSS.Keys)
            directionsCSS[key] = "button-inactive";
    }
    public void SetDirections()
    {
        foreach (var exitKey in CurrentLocation.Exits.Keys)
            directionsCSS[exitKey] = "button-selectable";
    }
    public void SetUi(UiVersion version)
    {
        CurrentUiVersion = version;
        UpdateSelectUiCss();
    }

    private void UpdateSelectUiCss()
    {
        if (CurrentUiVersion == UiVersion.One)
        {
            selectUiCSS[UiVersion.One] = "btn-primary";
            selectUiCSS[UiVersion.Two] = "btn-secondary";
            return;
        }
        selectUiCSS[UiVersion.One] = "btn-secondary";
        selectUiCSS[UiVersion.Two] = "btn-primary";
    }

    public void ShowSaveOptions() =>
        currentControls = currentControls == ControlsOption.SaveGame
            ? ControlsOption.Play
            : ControlsOption.SaveGame;
    public void ShowLoadOptions() =>
        currentControls = currentControls == ControlsOption.LoadGame
            ? ControlsOption.Play
            : ControlsOption.LoadGame;
    public void ShowGameControls() => currentControls = ControlsOption.Play;
    public event Action<string> OnGameChanged;
    public void RaiseEventOnGameChanged() => OnGameChanged?.Invoke("Game has changed.");
    public string Prompt => $"{MoveNumber}: Which direction do you want to go? (or click 'Check')";
    public bool GameIsOver => Opponents.Count() <= FoundOpponents.Count();
    public string GameProgress => $"Opponents Found: {string.Join(", ", FoundOpponents)}" +
                $"<br>Hidden Opponents Remaining: {Opponents.Count() - FoundOpponents.Count()}";
    private string HandleLandingGrammar(string location) => location == "Landing" ? "on" : "in";
    private bool IfCanMoveThenMove(Direction exitDirection)
    {
        if (CurrentLocation.Exits.TryGetValue(exitDirection, out Location? location))
        {
            CurrentLocation = location;
            RaiseEventOnGameChanged();
            return true;
        }
        return false;
    }
    public void Save()
    {
        ParsedOutput = ParseInput($"save {FileNameToBeSaved}");
        RaiseEventOnGameChanged();
    }
    private string Save(string nameForSavedFile)
    {
        SaveGame saveGame = new();
        SaveData newSaveData = new(CurrentLocation, MoveNumber, status, FoundOpponents);
        return saveGame.Save(newSaveData, nameForSavedFile);
    }
    public void Load()
    {
        ParsedOutput = ParseInput($"load {FileNameToBeLoaded}");
        RaiseEventOnGameChanged();
    }
    private string Load(string nameOfFileToLoad)
    {
        SaveGame loadedGame = new();
        string loadedGameResponse = loadedGame.Load(nameOfFileToLoad);
        if (loadedGameResponse != string.Empty) return loadedGameResponse;

        nameOfFileToLoad = nameOfFileToLoad.Split('.').ToList()[0];  // if loaded with extension attached to filename, take only the filename
        CurrentLocation = House.GetLocationByName(loadedGame.CurrentLocationName);
        MoveNumber = loadedGame.MoveNumber;
        status = loadedGame.Status;

        FoundOpponents.Clear();
        List<Opponent> foundOpponents = new();
        foreach (string opponent in loadedGame.FoundOpponents) foundOpponents.Add(new Opponent(opponent));
        FoundOpponents = foundOpponents;

        foreach (Location location in House.Locations)
            if (location.HasHidingPlace)
                location.HidingPlace.ClearOpponents();
        foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
            House
                .GetLocationWithHidingPlaceByName(pair.Value)
                .HidingPlace
                .HiddenOpponents
                .Add(new Opponent(pair.Key));

        if (loadedGame != null)
            return $"Loaded current game from \"{nameOfFileToLoad}.json\"";
        else
            return "An unknown error occurred.";
    }
    private string ParseInput(string input)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        input = input.ToLower();
        var inputList = input.Split(" ").ToList();

        if (inputList[0] == "save")
        {
            inputList.RemoveAt(0);
            return Save(string.Join(' ', inputList));
        }
        if (inputList[0] == "load")
        {
            inputList.RemoveAt(0);
            return Load(string.Join(' ', inputList));
        }

        MoveNumber++;

        for (int i = 0; i < inputList.Count(); i++)
            inputList[i] = textInfo.ToTitleCase(inputList[i]);
        string formattedInput = string.Join(string.Empty, inputList);

        if (formattedInput.ToLower() == "check" && CurrentLocation.HasHidingPlace)
        {
            IEnumerable<Opponent> foundOpponents = CurrentLocation.HidingPlace.GetHiddenOpponents();
            if (foundOpponents.Count() > 0)
            {
                FoundOpponents.AddRange(foundOpponents);
                CurrentLocation.HidingPlace.ClearOpponents();
                return $"You found {foundOpponents.Count()} opponent{House.S(foundOpponents.Count())} hiding {CurrentLocation.HidingPlace.Name}.<br>";
            }
            else
                return $"Nobody was hiding {CurrentLocation.HidingPlace.Name}<br>";
        }
        else if (Enum.TryParse(formattedInput, out Direction direction))
        {
            if (IfCanMoveThenMove(direction))
                return $"Moving {direction}<br>";
            else
                return "There's no exit in that direction.<br>";
        }
        else
            return "That's not a valid direction.<br>";
    }
    private string MentionHidingPlace(Location location) =>
        location.HasHidingPlace ? $"<br>Someone could hide {location.HidingPlace.Name}<br>" : string.Empty;
    public void CheckForOpponents()
    {
        Console.WriteLine("Checked for Opponents");
        if (!GameIsOver)
            ParsedOutput = ParseInput("check");
        RaiseEventOnGameChanged();
    }
}
