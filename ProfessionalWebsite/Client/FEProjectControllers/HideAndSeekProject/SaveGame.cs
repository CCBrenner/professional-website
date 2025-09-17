<<<<<<< HEAD
﻿using Newtonsoft.Json;

namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public class SaveGame
{
    public Dictionary<string, string> OpponentsInHidingLocations { get; set; }
    public List<string> FoundOpponents { get; set; }
    public string CurrentLocationName { get; set; }
    public int MoveNumber { get; set; }
    public string Status { get; set; }

    public string Save(SaveData saveData, string nameForSavedFile)
    {
        nameForSavedFile = nameForSavedFile.Split('.')[0];  // if loaded with extension attached to filename, take only the filename

        Dictionary<string, string> opponentsInHidingLocations = new();

        foreach (Location location in House.Locations)
            if (location.HasHidingPlace)
                foreach (Opponent opponent in location.HidingPlace.HiddenOpponents)
                    opponentsInHidingLocations.Add(opponent.Name, location.Name);

        OpponentsInHidingLocations = opponentsInHidingLocations;

        CurrentLocationName = saveData.CurrentLocation.Name;
        MoveNumber = saveData.MoveNumber;
        Status = saveData.Status;
        List<string> foundOpponents = new();

        foreach (Opponent opponent in saveData.FoundOpponents) foundOpponents.Add(opponent.Name);
        FoundOpponents = foundOpponents;

        string savedGame = JsonConvert.SerializeObject(this);

        if (!nameForSavedFile.Contains(@"/") && !nameForSavedFile.Contains(@"\") && !nameForSavedFile.Contains(' '))
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            using (StreamWriter writer = new StreamWriter($"{folder}{Path.DirectorySeparatorChar}{nameForSavedFile}.json", false))
                writer.WriteLine(savedGame);
            return $"Saved current game to \"{nameForSavedFile}.json\"";
        }
        return $"Could not save game to \"{nameForSavedFile}.json\": " +
            $"Invalid characters detected. Please remove slashes and/or spaces from file name.";
    }
    public string Load(string nameOfFileToLoad)
    {
        nameOfFileToLoad = nameOfFileToLoad.Split('.').ToList()[0];  // if loaded with extension attached to filename, take only the filename

        if (nameOfFileToLoad.Contains(@"\") || nameOfFileToLoad.Contains(@"/") || nameOfFileToLoad.Contains(' '))
            return $"Could not load game from \"{nameOfFileToLoad}.json\": " +
                $"Invalid characters detected. Please remove slashes and/or spaces from file name.";

        string gameDataToDeserialize = "";
        try
        {
            // an alternative to the line below it:
            // string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}{Path.DirectorySeparatorChar}{nameOfFileToLoad}.json"))
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{nameOfFileToLoad}.json");
            using (StreamReader reader = new StreamReader(filePath))
            {
                gameDataToDeserialize = reader.ReadToEnd();
            }
            File.Delete(filePath);
        }
        catch
        {
            return $"Could not load game: Saved file of name \"{nameOfFileToLoad}.json\" not found";
        }

        SaveGame? loadedGame = new SaveGame();
        try
        {
            loadedGame = System.Text.Json.JsonSerializer.Deserialize<SaveGame>(gameDataToDeserialize);
            OpponentsInHidingLocations = loadedGame.OpponentsInHidingLocations;
            FoundOpponents = loadedGame.FoundOpponents;
            CurrentLocationName = loadedGame.CurrentLocationName;
            MoveNumber = loadedGame.MoveNumber;
            Status = loadedGame.Status;
        }
        catch
        {
            return $"Could not load game: Could not deserialize file with name \"{nameOfFileToLoad}.json\"";
        }
        return "";
    }
}
=======
﻿using Newtonsoft.Json;

namespace ProfessionalWebsite.Client.ProjAssets.HideAndSeekProject;

public class SaveGame
{
    // Key = Opponent.Name, Value = LocationWithHidingPlace.HidingPlaceName
    public Dictionary<string, string> OpponentsInHidingLocations { get; set; }
    public List<string> FoundOpponents { get; set; }
    public string CurrentLocationName { get; set; }
    public int MoveNumber { get; set; }
    public string Status { get; set; }

    public string Save(GameController gameController, string nameForSavedFile)
    {
        nameForSavedFile = nameForSavedFile.Split('.')[0];  // if loaded with extension attached to filename, take only the filename

        Dictionary<string, string> opponentsInHidingLocations = new();
        foreach (Location location in House.Locations)
        {
            if (location.GetType() == typeof(LocationWithHidingPlace))
            {
                var locWithHidingPlace = (LocationWithHidingPlace)location;
                foreach (Opponent opponent in locWithHidingPlace.HidingPlace)
                {
                    opponentsInHidingLocations.Add(opponent.Name, location.Name);
                }
            }
        }
        OpponentsInHidingLocations = opponentsInHidingLocations;

        CurrentLocationName = gameController.CurrentLocation.Name;
        MoveNumber = gameController.MoveNumber;
        Status = gameController.Status;
        List<string> foundOpponents = new List<string>();

        foreach (Opponent opponent in gameController.FoundOpponents) foundOpponents.Add(opponent.Name);
        FoundOpponents = foundOpponents;

        string savedGame = JsonConvert.SerializeObject(this);

        if (!nameForSavedFile.Contains(@"/") && !nameForSavedFile.Contains(@"\") && !nameForSavedFile.Contains(' '))
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            using (StreamWriter writer = new StreamWriter($"{folder}{Path.DirectorySeparatorChar}{nameForSavedFile}.json", false))
                writer.WriteLine(savedGame);
            return $"Saved current game to \"{nameForSavedFile}.json\"";
        }
        return $"Could not save game to \"{nameForSavedFile}.json\": " +
            $"Invalid characters detected. Please remove slashes and/or spaces from file name.";
    }
    public string Load(string nameOfFileToLoad)
    {
        nameOfFileToLoad = nameOfFileToLoad.Split('.').ToList()[0];  // if loaded with extension attached to filename, take only the filename

        if (nameOfFileToLoad.Contains(@"\") || nameOfFileToLoad.Contains(@"/") || nameOfFileToLoad.Contains(' '))
            return $"Could not load game from \"{nameOfFileToLoad}.json\": " +
                $"Invalid characters detected. Please remove slashes and/or spaces from file name.";

        string gameDataToDeserialize = "";
        try
        {
            // an alternative to the line below it:
            // string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}{Path.DirectorySeparatorChar}{nameOfFileToLoad}.json"))
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{nameOfFileToLoad}.json");
            using (StreamReader reader = new StreamReader(filePath))
            {
                gameDataToDeserialize = reader.ReadToEnd();
            }
            File.Delete(filePath);
        }
        catch
        {
            return $"Could not load game: Saved file of name \"{nameOfFileToLoad}.json\" not found";
        }

        SaveGame? loadedGame = new SaveGame();
        try
        {
            loadedGame = System.Text.Json.JsonSerializer.Deserialize<SaveGame>(gameDataToDeserialize);
            OpponentsInHidingLocations = loadedGame.OpponentsInHidingLocations;
            FoundOpponents = loadedGame.FoundOpponents;
            CurrentLocationName = loadedGame.CurrentLocationName;
            MoveNumber = loadedGame.MoveNumber;
            Status = loadedGame.Status;
        }
        catch
        {
            return $"Could not load game: Could not deserialize file with name \"{nameOfFileToLoad}.json\"";
        }
        return "";
    }
}
>>>>>>> dd78908e3871e0915f6cfcde1e4391618ac793a1
