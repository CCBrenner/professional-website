﻿namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public class Panel
    {
        public Panel(
            int id,
            int panelGroupId = -1,
            string panelActiveStatusClassName = "pm-panel-visible",
            string blurStatusClassName = "pm-content-blurred",
            string behindPanelStatusClassName = "pm-behindpanel-present",
            string panelButtonClassName = "pm-panelbutton-active",
            bool cannotBeActiveWhileOtherPanelsAreActive = true
        )
        {
            this.panelActiveStatusClassName = panelActiveStatusClassName;
            this.blurStatusClassName = blurStatusClassName;
            this.behindPanelStatusClassName = behindPanelStatusClassName;
            this.panelButtonClassName = panelButtonClassName;

            Id = id;
            PanelGroupId = panelGroupId;
            CannotBeActiveWhileOtherPanelsAreActive = cannotBeActiveWhileOtherPanelsAreActive;

            PanelStatus = "";
            BlurStatus = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
        }

        private string panelActiveStatusClassName;
        private string blurStatusClassName;
        private string behindPanelStatusClassName;
        private string panelButtonClassName;


        public readonly int Id;
        public int PanelGroupId { get; private set; }
        public bool CannotBeActiveWhileOtherPanelsAreActive { get; private set; }

        public string PanelStatus { get; private set; }
        public bool PanelIsActive { get; private set; }
        public string BlurStatus { get; private set; }
        public bool BlurIsActive { get; private set; }
        public string BehindPanelStatus { get; private set; }
        public bool BehindPanelIsActive { get; private set; }
        public string PanelButtonStatus { get; private set; }
        public bool PanelButtonIsActive { get; private set; }

        public async Task<Panel> Activate()
        {
            PanelStatus = panelActiveStatusClassName;
            BlurStatus = blurStatusClassName;
            BehindPanelStatus = behindPanelStatusClassName;
            PanelButtonStatus = panelButtonClassName;
            PanelIsActive = true;
            BlurIsActive = true;
            BehindPanelIsActive = true;
            PanelButtonIsActive = true;
            return this;
        }
        public Panel Deactivate()
        {
            PanelStatus = "";
            BlurStatus = "";
            BehindPanelStatus = "";
            PanelButtonStatus = "";
            PanelIsActive = false;
            BlurIsActive = false;
            BehindPanelIsActive = false;
            PanelButtonIsActive = false;
            return this;
        }
        public Panel ActivateButton()
        {
            PanelButtonStatus = panelButtonClassName;
            PanelButtonIsActive = true;
            return this;
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
        */
    }
}
