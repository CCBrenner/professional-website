namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    /*
    public class Panel1
    {
        public Panel1(
            string panelActiveStatusClassName = "pm-panel-visible", 
            string mainContentClassName = "pm-maincontent-blurred",
            string behindPanelStatusClassName = "pm-behindpanel-present",
            string panelButtonClassName = "pm-panelbutton-active"
        )
        {
            this.panelActiveStatusClassName = panelActiveStatusClassName;
            this.mainContentClassName = mainContentClassName;
            this.behindPanelStatusClassName = behindPanelStatusClassName;
            this.panelButtonClassName = panelButtonClassName;

            PanelStatus = "";
            MainContent = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
        }

        private string panelActiveStatusClassName;
        private string mainContentClassName;
        private string behindPanelStatusClassName;
        private string panelButtonClassName;

        public string PanelStatus { get; private set; }
        public string MainContent { get; private set; }
        public string BehindPanelStatus { get; private set; }
        public string PanelButtonStatus { get; private set; }

        public void TogglePanel()
        {
            if (PanelStatus == "")
                Activate();
            else
                Deactivate();
        }
        public void SetPanelIsActive(bool isActive)
        {
            if (isActive)
                Activate();
            else
                Deactivate();
        }
        private void Activate()
        {
            PanelStatus = panelActiveStatusClassName;
            MainContent = mainContentClassName;
            BehindPanelStatus = behindPanelStatusClassName;
            PanelButtonStatus = panelButtonClassName;
        }
        private void Deactivate()
        {
            PanelStatus = "";
            MainContent = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
        }
        public void SetPanelButtonIsActive(bool isShowingAsCurrentLocation) =>
            PanelButtonStatus = isShowingAsCurrentLocation ? panelButtonClassName : "";

        public string GetPropValue(PanelProps key)
        {
            if (key == PanelProps.PanelStatus)
                return PanelStatus;
            else if (key == PanelProps.PanelButtonStatus)
                return PanelButtonStatus;
            else if (key == PanelProps.BlurStatus)
                return MainContent;
            else if (key == PanelProps.BehindPanelStatus)
                return BehindPanelStatus;
            return "";
        }

        /*
        Examples of how CSS should be set up for effective transitions:
        
        // .pm-panel-visible requires defaults:
        visibility: hidden;
        opacity: 0;
        transition: visibility 0.2s, opacity 0.2s;

        .pm-panel-visible {
            visibility: visible;
            opacity: 1;
            transition: visibility 0.2s, opacity 0.2s;
        }

        // .pm-maincontent-blurred requires defaults:
        *none*

        .pm-maincontent-blurred {
            filter: blur(2.5px);
        }

        // .pm-behindpanel-present requires defaults:
        display: none;
        transition: background-color 0.2s;

        // .pm-behindpanel-present styles that should also be there (styles can differ):
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: var(--content-height);
        z-index: 3;

        .pm-behindpanel-present {
            display: block;
            background-color: rgba(0,0,60,0.06);
            transition: background-color 0.2s;
        }
    }*/
}
