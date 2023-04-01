namespace ProfessionalWebsite.Client.Services.Contracts
{
    public interface INavService
    {
        // For BehindPanel
        public string BehindPanel { get; }
        public string ContentBlur { get; }

        public string LayoutControls { get; }
        public string AnimateMain { get; }
        public string DiscontinueButton { get; }


        // Methods:
        public void UpdateNav(int buttonId);
        public void UpdateNavFromBehindPanel();
        public void RouteUserAndUpdateNav(int index);

        // Animations
        public void ShowLayoutControls(int index);
        public void PlayAnimation(int animation, bool isContinuous);
        public void StopMainAnimation();
    }
}
