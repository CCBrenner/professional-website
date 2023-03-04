namespace ProfessionalWebsite.Client.Services.Contracts
{
    public interface INavService
    {
        public string NavButtonAStatus { get; set; }
        public string NavButtonBStatus { get; set; }
        public string NavButtonCStatus { get; set; }
        public string NavButtonDStatus { get; set; }
        public string NavButtonEStatus { get; set; }

        public string NavPanelAStatus { get; set; }
        public string NavPanelBStatus { get; set; }
        public string NavPanelCStatus { get; set; }
        public string NavPanelDStatus { get; set; }
        public string NavPanelEStatus { get; set; }

        // For BehindPanel
        public string BehindPanel { get; set; }

        public string ContentBlur { get; set; }

        public string LayoutControls { get; set; }
        public string AnimateMain { get; set; }
        public string DiscontinueButton { get; set; }

        public bool ExternalLayoutControls { get; set; }

        // Methods:
        public void UpdateNav(char buttonId);
        public void UpdateNavFromPanel(char character);
        public void UpdateNavFromBehindPanel();
        public void UpdateNavFromDrawer(char character);
        public void UpdateNavFromAppTitle(char character);

        // Animations
        public void ShowLayoutControls(char character);
        public void PlayFirstAnimation(bool isContinuous);
        public void PlaySecondAnimation(bool isContinuous);
        public void PlayThirdAnimation(bool isContinuous);
        public void PlayFourthAnimation(bool isContinuous);
        public void PlayFifthAnimation(bool isContinuous);
        public void StopMainAnimation();
    }
}
