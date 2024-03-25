﻿using ProfessionalWebsite.Client.ProjAssets.HideAndSeekProject;
using System.Diagnostics;

namespace ProfessionalWebsite.Client.ProjAssets.HideAndSeekProject;

public class Opponent
{
    public Opponent(string name) => Name = name;
    public readonly string Name;
    public override string ToString() => Name;
    public void Hide()
    {
        Location currentLocation = House.Entry;
        int locationsToMoveThrough = House.Random.Next(10, 20);

        for (int i = 0; i < locationsToMoveThrough; i++)
            currentLocation = House.RandomExit(currentLocation);
        while (true)
        {
            if (currentLocation.GetType() == typeof(LocationWithHidingPlace))
            {
                ((LocationWithHidingPlace)currentLocation).Hide(this);
                break;
            }
            else
            {
                currentLocation = House.RandomExit(currentLocation);
            }
        }

        (currentLocation as LocationWithHidingPlace).Hide(this);

        Debug.WriteLine($"{Name} is hiding " +
            $"{(currentLocation as LocationWithHidingPlace).HidingPlace} " +
            $"in the {currentLocation.Name}");
    }
}
