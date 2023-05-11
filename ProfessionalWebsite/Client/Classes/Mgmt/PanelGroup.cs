namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public class PanelGroup
    {
        public PanelGroup(int id, int startingLocation)
        {
            Id = id;
            LocationPanelId = startingLocation;
        }

        public readonly int Id;
        public int LocationPanelId;
        public Dictionary<int, Panel> Panels = new Dictionary<int, Panel>();
    }
}
