﻿using System.Globalization;

namespace ProfessionalWebsite.Client.Classes.HideAndSeekProject;

public class GameController
{
    public GameController()
    {
        House.ClearHidingPlaces();
        foreach (Opponent opponent in Opponents)
            opponent.Hide();

        CurrentLocation = House.Entry;
    }
    public Location CurrentLocation { get; private set; }
    public int MoveNumber { get; set; } = 1;
    public string Prompt => $"{MoveNumber}: Which direction do you want to go? (or click 'Check')";
    public bool GameOver => Opponents.Count() == FoundOpponents.Count();
    public string FileNameToBeSaved = "";
    public string FileNameToBeLoaded = "";
    public string ParsedOutput = "";
    private string status;
    public string Status => !GameOver ? $"You are {HandleLandingGrammar(CurrentLocation.Name)} the {CurrentLocation.Name}. " +
        $"You see the following exits:" +
        $"<br><br>" +
        $"{string.Join($"<br>", CurrentLocation.ExitList)}" +
        $"<br>{MentionHidingPlace(CurrentLocation)}"
        : $"You won the game in {MoveNumber} moves!" +
        $"<br>Press \"Restart\" to restart to restart the game or press \"Quit\" to quit.";
    public string GameProgress => $"Opponents Found: {string.Join(", ", FoundOpponents)}" +
                $"<br>Hidden Opponents Remaining: {Opponents.Count() - FoundOpponents.Count()}";
    public Direction SelectedDirection;
    public List<Opponent> FoundOpponents = new List<Opponent>();
    public IEnumerable<Opponent> Opponents = new List<Opponent>()
    {
        new Opponent("Joe"),
        new Opponent("Bob"),
        new Opponent("Ana"),
        new Opponent("Owen"),
        new Opponent("Jimmy"),
    };
    private string HandleLandingGrammar(string location) => location == "Landing" ? "on" : "in";
    public bool Move(Direction exitDirection)
    {
        bool canMove = CurrentLocation.Exits.ContainsKey(exitDirection);
        if (canMove) CurrentLocation = CurrentLocation.Exits[exitDirection];
        return canMove;
    }
    private string Save(string nameForSavedFile)
    {
        SaveGame saveGame = new SaveGame();
        return saveGame.Save(this, nameForSavedFile);
    }
    private string Load(string nameOfFileToLoad)
    {
        SaveGame loadedGame = new SaveGame();
        string loadedGameResponse = loadedGame.Load(nameOfFileToLoad);
        if (loadedGameResponse != "") return loadedGameResponse;

        nameOfFileToLoad = nameOfFileToLoad.Split('.').ToList()[0];  // if loaded with extension attached to filename, take only the filename
        CurrentLocation = House.GetLocationByName(loadedGame.CurrentLocationName);
        MoveNumber = loadedGame.MoveNumber;
        status = loadedGame.Status;

        FoundOpponents.Clear();
        List<Opponent> foundOpponents = new List<Opponent>();
        foreach (string opponent in loadedGame.FoundOpponents) foundOpponents.Add(new Opponent(opponent));
        FoundOpponents = foundOpponents;

        foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
            (House.GetLocationByName(pair.Value) as LocationWithHidingPlace).OpponentsHiddenHere.Clear();
        foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
            (House.GetLocationByName(pair.Value) as LocationWithHidingPlace).OpponentsHiddenHere.Add(new Opponent(pair.Key));

        if (loadedGame != null)
            return $"Loaded current game from \"{nameOfFileToLoad}.json\"";
        else
            return "An unknown error occurred.";
    }
    public string ParseInput(string input)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        input = input.ToLower();
        List<string> inputList = input.Split(" ").ToList();

        if (inputList[0] == "save")
        {
            inputList.RemoveAt(0);
            return Save(string.Join(' ', inputList));
        }
        else if (inputList[0] == "load")
        {
            inputList.RemoveAt(0);
            return Load(string.Join(' ', inputList));
        }

        MoveNumber++;

        for (int i = 0; i < inputList.Count(); i++)
            inputList[i] = textInfo.ToTitleCase(inputList[i]);
        string formattedInput = string.Join("", inputList);

        if (formattedInput.ToLower() == "check")
        {
            if ((CurrentLocation is LocationWithHidingPlace location) && location.HidingPlace != "")
            {
                IEnumerable<Opponent> foundOpponents = location.CheckHidingPlace();
                if (foundOpponents.Count() > 0)
                {
                    FoundOpponents.AddRange(foundOpponents);
                    return $"You found {foundOpponents.Count()} opponent{House.S(foundOpponents.Count())} hiding {location.HidingPlace}.<br>";
                }
                else
                    return $"Nobody was hiding {location.HidingPlace}<br>";
            }
            else
                return $"There is no hiding place in the {CurrentLocation.Name}<br>";
        }
        else if (Enum.TryParse(formattedInput, out Direction direction))
        {
            if (Move(direction))
                return $"Moving {direction}<br>";
            else
                return "There's no exit in that direction.<br>";
        }
        else
            return "That's not a valid direction.<br>";
    }
    private string MentionHidingPlace(Location location)
    {
        if (location is LocationWithHidingPlace locationWithHidingPlace && locationWithHidingPlace.HidingPlace != "")
            return $"<br>Someone could hide {locationWithHidingPlace.HidingPlace}<br>";
        else
            return "";
    }
}
