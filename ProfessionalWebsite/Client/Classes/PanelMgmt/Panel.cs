namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class Panel
    {
        public Panel(
            int panelGroupid = -1,
            string panelActiveStatusClassName = "pm-panel-visible",
            string blurStatusClassName = "pm-maincontent-blurred",
            string behindPanelStatusClassName = "pm-behindpanel-present",
            string panelButtonClassName = "pm-panelbutton-active"
        )
        {
            this.panelActiveStatusClassName = panelActiveStatusClassName;
            this.blurStatusClassName = blurStatusClassName;
            this.behindPanelStatusClassName = behindPanelStatusClassName;
            this.panelButtonClassName = panelButtonClassName;

            PanelGroupId = panelGroupid;
            PanelStatus = "";
            BlurStatus = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
        }

        private string panelActiveStatusClassName;
        private string blurStatusClassName;
        private string behindPanelStatusClassName;
        private string panelButtonClassName;

        public int PanelGroupId { get; private set; }
        public string PanelStatus { get; private set; }
        public string BlurStatus { get; private set; }
        public string BehindPanelStatus { get; private set; }
        public string PanelButtonStatus { get; private set; }

        public void Activate()
        {
            PanelStatus = panelActiveStatusClassName;
            BlurStatus = blurStatusClassName;
            BehindPanelStatus = behindPanelStatusClassName;
            PanelButtonStatus = panelButtonClassName;
        }
        public void Deactivate()
        {
            PanelStatus = "";
            BlurStatus = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
        }
        public void TogglePanel()
        {
            if (PanelStatus == "")
                Activate();
            else
                Deactivate();
        }
        public void ActivateButton() =>
            PanelButtonStatus = panelButtonClassName;

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
        */
    }
}
