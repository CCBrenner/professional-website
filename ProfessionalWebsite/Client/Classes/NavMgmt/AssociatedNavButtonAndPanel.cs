namespace ProfessionalWebsite.Client.Classes.NavMgmt
{
    public class AssociatedNavButtonAndPanel
    {
        public string NavButtonStatus;
        public string NavPanelStatus;
        public bool IsThisLocation;

        public void Reset()
        {
            NavButtonStatus = "";
            NavPanelStatus = "";
            IsThisLocation = false;
        }
    }
}
