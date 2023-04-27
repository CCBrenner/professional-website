using Microsoft.AspNetCore.Components;
using ProfessionalWebsite.Client.Pages;
using ProfessionalWebsite.Client.Services.Contracts;
using System.Collections.Generic;

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

            List<CollapsingPageSection> sectionsList = new List<CollapsingPageSection>();
            for (int i = 0; i < 17; i++)
                sectionsList.Add(new CollapsingPageSection());
            CollapsingPageSectionsLogic pageIndexOfOne = new CollapsingPageSectionsLogic("knowhow", sectionsList);

            sectionsList = new List<CollapsingPageSection>();
            for (int i = 0; i < 9; i++)
                sectionsList.Add(new CollapsingPageSection());
            CollapsingPageSectionsLogic pageIndexOfTwo = new CollapsingPageSectionsLogic("collyn", sectionsList);

            sectionsList = new List<CollapsingPageSection>();
            for (int i = 0; i < 7; i++)
                sectionsList.Add(new CollapsingPageSection());
            CollapsingPageSectionsLogic pageIndexOfThree = new CollapsingPageSectionsLogic("invent", sectionsList);

            SectionedPages = new List<CollapsingPageSectionsLogic>()
            {
                new CollapsingPageSectionsLogic("projects", new List<CollapsingPageSection>()),  // not used
                pageIndexOfOne,
                pageIndexOfTwo,
                pageIndexOfThree,
                new CollapsingPageSectionsLogic("articles", new List<CollapsingPageSection>()),  // not used
            };

            globalNavButtonState = "";
            currentButton = 2;
            BehindPanel = "";
            ContentBlur = "";
            LayoutControls = "";
            AnimateMain = "";
            DiscontinueButton = "";
        }

        public List<AssociatedNavButtonAndPanel> AssociatedNav;
        public List<CollapsingPageSectionsLogic> SectionedPages;

        // For BehindPanel (the following three members:)
        private string globalNavButtonState;
        private int currentButton;
        public string BehindPanel { get; private set; }
        public string ContentBlur { get; private set; }
        public string LayoutControls { get; private set; }
        public string AnimateMain { get; private set; }
        public string DiscontinueButton { get; private set; }
        public event Action<string> OnAnimateMain;
        public event Action<int> OnPromoSectionOfPage;

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

            BehindPanel = globalNavButtonState == "panel-visible" ? "button-on-show-behind-panel" : "";
            ContentBlur = globalNavButtonState == "panel-visible" ? "content-blur" : "";
        }
        public void UpdateNavFromBehindPanel() =>
            UpdateNav(currentButton);
        public void RouteUserAndUpdateNav(int index)
        {
            foreach (var nav in AssociatedNav)
                nav.Reset();
            AssociatedNav[index].NavButtonStatus = "highlight-button";
            AssociatedNav[index].IsThisLocation = true;
            LayoutControls = "";
            AnimateMain = "";
            BehindPanel = "";
            ContentBlur = "";
        }
        public void NavigateToCollapsibleSectionOfOtherPage(string pagePath, int sectionIndex)  // needs tests
        {
            int pageIndex = 1000;  // requirement: must be greater than SectionedPages.Count()
            for (int i = 0; i < SectionedPages.Count; i++)
            {
                if (SectionedPages[i].PagePath == pagePath)
                {
                    pageIndex = i; 
                    break;
                }
            }
            NavigateToCollapsibleSectionOfOtherPage(pageIndex, sectionIndex);
        }
        public void NavigateToCollapsibleSectionOfOtherPage(int pageIndex, int sectionIndex)  // needs tests
        {
            try
            {
                if (pageIndex < SectionedPages.Count())
                    throw new Exception();
                SectionedPages[pageIndex].CollapseAllShowOne(sectionIndex);
                RouteUserAndUpdateNav(pageIndex);
                RaiseEventOnPromoSectionOfPageIndexOne(sectionIndex);
            }
            catch
            {
                Console.WriteLine("PageIndex was greater than the number of SectionedPages in NavService."); // add to data logs (would help to be more specific (was seeing IndexOutOfRange exeptions at one point)
            }
        }
        public void RaiseEventOnPromoSectionOfPageIndexOne(int sectionIndex)
        {
            if (OnPromoSectionOfPage != null)
                OnPromoSectionOfPage?.Invoke(sectionIndex);
        }

        // Animations
        public void ShowLayoutControls(int index)
        {
            RouteUserAndUpdateNav(index);
            AssociatedNav[index].NavPanelStatus = "";
            LayoutControls = "layout-controls-on";
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
            AnimateMain = animation;
            RaiseEventOnAnimateMain(AnimateMain);
            DiscontinueButton = discontinue;
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
