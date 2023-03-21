using Microsoft.AspNetCore.Components;
using ProfessionalWebsite.Client.Services.Contracts;

namespace ProfessionalWebsite.Client.Services
{
    public class NavService : INavService
    {
        public NavService() 
        {
            AssociatedNav = new List<AssociatedNavButtonAndPanel>()
            {
                new AssociatedNavButtonAndPanel() { NavButtonStatus = "", NavPanelStatus = "", IsThisLocation = false },
                new AssociatedNavButtonAndPanel() { NavButtonStatus = "", NavPanelStatus = "", IsThisLocation = false },
                new AssociatedNavButtonAndPanel() { NavButtonStatus = "highlight-button", NavPanelStatus = "", IsThisLocation = true },
                new AssociatedNavButtonAndPanel() { NavButtonStatus = "", NavPanelStatus = "", IsThisLocation = false },
                new AssociatedNavButtonAndPanel() { NavButtonStatus = "", NavPanelStatus = "", IsThisLocation = false },
            };
            globalNavButtonState = "";
            currentButton = 2;
            behindPanel = "";
            contentBlur = "";
            layoutControls = "";
            animateMain = "";
            discontinueButton = "";
            navigateToSection = "";
        }

        public List<AssociatedNavButtonAndPanel> AssociatedNav;

        [Inject]
        public NavigationManager NavigationManager { get; }

        // For BehindPanel (the following three members:)
        private string globalNavButtonState;
        private int currentButton;
        public string BehindPanel { get { return behindPanel; } }
        private string behindPanel;
        public string ContentBlur { get { return contentBlur; } }
        private string contentBlur;
        public string LayoutControls { get { return layoutControls; } }
        private string layoutControls;
        public string AnimateMain { get { return animateMain; } }
        private string animateMain;
        public string DiscontinueButton { get { return discontinueButton; } }
        private string discontinueButton;
        public string NavigateToSection { get { return navigateToSection; } }
        private string navigateToSection;
        public event Action<string> OnAnimateMain;

        // Methods:
        public void UpdateNav(int buttonId)
        {
            currentButton = buttonId;  // For UpdateNavFromBehindPanel() only

            if (AssociatedNav[buttonId].NavPanelStatus == "panel-visible")
                AssociatedNav[buttonId].NavPanelStatus = "";
            else
            {
                foreach (var nav in AssociatedNav)
                    nav.NavPanelStatus = "";
                AssociatedNav[buttonId].NavPanelStatus = "panel-visible";
            }
            globalNavButtonState = AssociatedNav[buttonId].NavPanelStatus;

            foreach (var nav in AssociatedNav)
                nav.NavButtonStatus =
                    nav.NavPanelStatus == "" && !nav.IsThisLocation 
                    || globalNavButtonState == "panel-visible" && nav.NavPanelStatus == "" && nav.IsThisLocation
                    ? ""
                    : "highlight-button";

            behindPanel = globalNavButtonState == "panel-visible" ? "button-on-show-behind-panel" : "";
            contentBlur = globalNavButtonState == "panel-visible" ? "content-blur" : "";
        }
        public void UpdateNavFromBehindPanel() =>
            UpdateNav(currentButton);
        public void RouteUserAndUpdateNav(int index)
        {
            foreach (var nav in AssociatedNav)
            {
                nav.NavButtonStatus = "";
                nav.NavPanelStatus = "";
                nav.IsThisLocation = false;
            }
            AssociatedNav[index].NavButtonStatus = "highlight-button";
            AssociatedNav[index].IsThisLocation = true;
            layoutControls = "";
            animateMain = "";
            behindPanel = "";
            contentBlur = "";
        }
        public void NavigateToCollapsibleSectionOfOtherPage(int index, string section)  // needs tests
        {
            RouteUserAndUpdateNav(index);
            navigateToSection = section;  // section is a conditional that the destination component's OnInitialized() method uses to update the page to show only the specified section's contents
        }
        public void ResetNavigateToSection() =>  // needs tests
            navigateToSection = "";

        // Animations
        public void ShowLayoutControls(int index)
        {
            RouteUserAndUpdateNav(index);
            AssociatedNav[index].NavPanelStatus = "";
            layoutControls = "layout-controls-on";
        }
        public void PlayAnimation(int animation, bool isContinuous)
        {
            if (isContinuous)
                if (AnimateMain == $"main{animation}-infinite")
                    SetAnimateMainAndDiscontinueButton("", "");
                else
                    SetAnimateMainAndDiscontinueButton($"main{animation}-infinite", "discontinue-button-on");
            else
                if (AnimateMain == $"main{animation}")
                    SetAnimateMainAndDiscontinueButton("", "");
                else
                    SetAnimateMainAndDiscontinueButton($"main{animation}", "");
        }
        private void SetAnimateMainAndDiscontinueButton(string animation, string discontinue)
        {
            animateMain = animation;
            RaiseEventOnAnimateMain(AnimateMain);
            discontinueButton = discontinue;
        }
        private void RaiseEventOnAnimateMain(string animation)
        {
            if (OnAnimateMain != null)
                OnAnimateMain?.Invoke(animation);
        }
        public void StopMainAnimation() =>
            SetAnimateMainAndDiscontinueButton("", "");
    }
}
