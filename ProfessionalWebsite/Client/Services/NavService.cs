using ProfessionalWebsite.Client.Services.Contracts;

namespace ProfessionalWebsite.Client.Services
{
    public class NavService : INavService
    {
        // a button is ON when a user clicks on it and panel and behindpanel appear
        private bool buttonAIsOn = false;
        private bool buttonBIsOn = false;
        private bool buttonCIsOn = false;
        private bool buttonDIsOn = false;
        private bool buttonEIsOn = false;

        // populates "highlight-button" when respective "button_IsOn" is true
        public string NavButtonAStatus { get; set; } = "";
        public string NavButtonBStatus { get; set; } = "";
        public string NavButtonCStatus { get; set; } = "highlight-button";
        public string NavButtonDStatus { get; set; } = "";
        public string NavButtonEStatus { get; set; } = "";

        // populates "button-on-show-panel" when respective "button_IsOn" is true
        public string NavPanelAStatus { get; set; } = "";
        public string NavPanelBStatus { get; set; } = "";
        public string NavPanelCStatus { get; set; } = "";
        public string NavPanelDStatus { get; set; } = "";
        public string NavPanelEStatus { get; set; } = "";

        // "locationIs_" is used for when no buttons are ON; highlights button of current location
        private bool locationIsA = false;
        private bool locationIsB = false;
        private bool locationIsC = true;
        private bool locationIsD = false;
        private bool locationIsE = false;

        // For BehindPanel
        private bool globalNavButtonState = false;
        public string BehindPanel { get; set; } = "";
        private char CurrentButton = 'Z';

        // blurs page content; populates "content-blur" class
        public string ContentBlur { get; set; } = "";

        public string LayoutControls { get; set; } = "";
        public string AnimateMain { get; set; } = "";
        public string DiscontinueButton { get; set; } = "";

        public bool ExternalLayoutControls { get; set; } = false;
        
        // Methods:
        public void UpdateNav(char buttonId)
        {
            CurrentButton = buttonId;
            switch (buttonId)
            {
                case 'A':
                    buttonAIsOn = UpdateButtonStates(buttonAIsOn);
                    globalNavButtonState = buttonAIsOn;
                    break;
                case 'B':
                    buttonBIsOn = UpdateButtonStates(buttonBIsOn);
                    globalNavButtonState = buttonBIsOn;
                    break;
                case 'C':
                    buttonCIsOn = UpdateButtonStates(buttonCIsOn);
                    globalNavButtonState = buttonCIsOn;
                    break;
                case 'D':
                    buttonDIsOn = UpdateButtonStates(buttonDIsOn);
                    globalNavButtonState = buttonDIsOn;
                    break;
                case 'E':
                    buttonEIsOn = UpdateButtonStates(buttonEIsOn);
                    globalNavButtonState = buttonEIsOn;
                    break;
            }
            UpdateButtons();
            UpdatePanels();
            UpdateBehindPanel();
            UpdateContentBlur(globalNavButtonState);
        }
        public void UpdateNavFromBehindPanel() =>
            UpdateNav(CurrentButton);
        public void UpdateNavFromPanel(char character)
        {
            UpdateLocation(character);
            LayoutControls = "";
            AnimateMain = "";
            UpdateNav(character);
        }
        public void UpdateNavFromDrawer(char character)
        {
            UpdateNavFromPanel(character);
            NavPanelAStatus = "";
            NavPanelBStatus = "";
            NavPanelCStatus = "";
            NavPanelDStatus = "";
            NavPanelEStatus = "";
            BehindPanel = "";
            ContentBlur = "";
        }
        public void UpdateNavFromAppTitle(char character)
        {
            UpdateLocation(character);
            SetAllButtonStatesToFalse();
            UpdateButtons();
            LayoutControls = "";
            AnimateMain = "";
            switch (character)
            {
                case 'A':
                    NavButtonAStatus = "highlight-button";
                    break;
                case 'B':
                    NavButtonBStatus = "highlight-button";
                    break;
                case 'C':
                    NavButtonCStatus = "highlight-button";
                    break;
                case 'D':
                    NavButtonDStatus = "highlight-button";
                    break;
                case 'E':
                    NavButtonEStatus = "highlight-button";
                    break;
            }
        }
        private bool UpdateButtonStates(bool buttonState)
        {
            if (buttonState)
                return false;
            else
            {
                SetAllButtonStatesToFalse();
                return true;
            }
        }
        private void SetAllButtonStatesToFalse()
        {
            buttonAIsOn = false;
            buttonBIsOn = false;
            buttonCIsOn = false;
            buttonDIsOn = false;
            buttonEIsOn = false;
        }
        private void UpdateButtons()
        {
            NavButtonAStatus = UpdateButton(buttonAIsOn, locationIsA);
            NavButtonBStatus = UpdateButton(buttonBIsOn, locationIsB);
            NavButtonCStatus = UpdateButton(buttonCIsOn, locationIsC);
            NavButtonDStatus = UpdateButton(buttonDIsOn, locationIsD);
            NavButtonEStatus = UpdateButton(buttonEIsOn, locationIsE);
        }
        private string UpdateButton(bool buttonState, bool locationState)
        {
            if (!buttonState && !locationState || globalNavButtonState && locationState && !buttonState)
                return "";
            else
                return "highlight-button";
        }
        private void UpdatePanels()
        {
            NavPanelAStatus = UpdatePanel(buttonAIsOn);
            NavPanelBStatus = UpdatePanel(buttonBIsOn);
            NavPanelCStatus = UpdatePanel(buttonCIsOn);
            NavPanelDStatus = UpdatePanel(buttonDIsOn);
            NavPanelEStatus = UpdatePanel(buttonEIsOn);
        }
        private string UpdatePanel(bool buttonState) =>
            buttonState ? "button-on-show-panel" : "";
        private void UpdateBehindPanel() =>
            BehindPanel = globalNavButtonState ? "button-on-show-behind-panel" : "";
        private string UpdateContentBlur(bool buttonState) =>
            ContentBlur = buttonState ? "content-blur" : "";
        private void UpdateLocation(char character)
        {
            SetAllLocationsToFalse();
            switch (character)
            {
                case 'A':
                    locationIsA = true;
                    break;
                case 'B':
                    locationIsB = true;
                    break;
                case 'C':
                    locationIsC = true;
                    break;
                case 'D':
                    locationIsD = true;
                    break;
                case 'E':
                    locationIsE = true;
                    break;
            }
        }
        private void SetAllLocationsToFalse()
        {
            locationIsA = false;
            locationIsB = false;
            locationIsC = false;
            locationIsD = false;
            locationIsE = false;
        }

        // Animations
        public void ShowLayoutControls(char character)
        {
            UpdateLocation(character);
            UpdateNav(character);
            LayoutControls = "layout-controls-on";
        }
        public void SetExternalLayoutControlsToTrue() =>
            ExternalLayoutControls = true;
        public void PlayFirstAnimation(bool isContinuous)
        {
            if (isContinuous)
            {
                if (AnimateMain == "main1-infinite")
                {
                    AnimateMain = "";
                    DiscontinueButton = "";
                }
                else
                {
                    AnimateMain = "main1-infinite";
                    DiscontinueButton = "discontinue-button-on";
                }
            }
            else
            {
                if (AnimateMain == "main1")
                    AnimateMain = "";
                else
                {
                    AnimateMain = "main1";
                    DiscontinueButton = "";
                }
            }
        }
        public void PlaySecondAnimation(bool isContinuous)
        {
            if (isContinuous)
            {
                if (AnimateMain == "main2-infinite")
                {
                    AnimateMain = "";
                    DiscontinueButton = "";
                }
                else
                {
                    AnimateMain = "main2-infinite";
                    DiscontinueButton = "discontinue-button-on";
                }
            }
            else
            {
                if (AnimateMain == "main2")
                    AnimateMain = "";
                else
                {
                    AnimateMain = "main2";
                    DiscontinueButton = "";
                }
            }
        }
        public void PlayThirdAnimation(bool isContinuous)
        {
            if (isContinuous)
            {
                if (AnimateMain == "main3-infinite")
                {
                    AnimateMain = "";
                    DiscontinueButton = "";
                }
                else
                {
                    AnimateMain = "main3-infinite";
                    DiscontinueButton = "discontinue-button-on";
                }
            }
            else
            {
                if (AnimateMain == "main3")
                    AnimateMain = "";
                else
                {
                    AnimateMain = "main3";
                    DiscontinueButton = "";
                }
            }
        }
        public void PlayFourthAnimation(bool isContinuous)
        {
            if (isContinuous)
            {
                if (AnimateMain == "main4-infinite")
                {
                    AnimateMain = "";
                    DiscontinueButton = "";
                }
                else
                {
                    AnimateMain = "main4-infinite";
                    DiscontinueButton = "discontinue-button-on";
                }
            }
            else
            {
                if (AnimateMain == "main4")
                    AnimateMain = "";
                else
                {
                    AnimateMain = "main4";
                    DiscontinueButton = "";
                }
            }
        }
        public void PlayFifthAnimation(bool isContinuous)
        {
            if (isContinuous)
            {
                if (AnimateMain == "main5-infinite")
                {
                    AnimateMain = "";
                    DiscontinueButton = "";
                }
                else
                {
                    AnimateMain = "main5-infinite";
                    DiscontinueButton = "discontinue-button-on";
                }
            }
            else
            {
                if (AnimateMain == "main5")
                    AnimateMain = "";
                else
                {
                    AnimateMain = "main5";
                    DiscontinueButton = "";
                }
            }
        }
        public void StopMainAnimation()
        {
            AnimateMain = "";
            DiscontinueButton = "";
        }
    }
}
