<<<<<<< HEAD
﻿namespace ProfessionalWebsite.Client.ProjAssets.HideAndSeekProject;
/*
public class LocationWithHidingPlace : Location
{
    public LocationWithHidingPlace(string name, string hidingPlace) : base(name)
    {
        HidingPlace = new(hidingPlace);
    }
    
}*/
=======
﻿namespace ProfessionalWebsite.Client.ProjAssets.HideAndSeekProject;

public class LocationWithHidingPlace : Location
{
    public LocationWithHidingPlace(string name, string hidingPlace) : base(name)
    {
        HidingPlaceName = hidingPlace;
    }
    public string HidingPlaceName { get; private set; }
    public List<Opponent> HidingPlace = new();
    public void Hide(Opponent opponent) => HidingPlace.Add(opponent);
    public IEnumerable<Opponent> GetHiddenOpponents()
    {
        List<Opponent> returnEnumerable = new();
        foreach (var opponent in HidingPlace)
            returnEnumerable.Add(opponent);
        return returnEnumerable;
    }
}
>>>>>>> dd78908e3871e0915f6cfcde1e4391618ac793a1
