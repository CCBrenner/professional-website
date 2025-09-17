using System.Diagnostics;

namespace ProfessionalWebsite.Client.FEProjectControllers.HideAndSeekProject;

public class Opponent
{
    public Opponent(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
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
            if (currentLocation.HasHidingPlace)
            {
                currentLocation.HidingPlace.Hide(this);
                break;
            }
            else
            {
                currentLocation = House.RandomExit(currentLocation);
            }
        }

        Debug.WriteLine($"{Name} is hiding " +
            $"{currentLocation.HidingPlace.Name} " +
            $"in the {currentLocation.Name}");
    }
}
