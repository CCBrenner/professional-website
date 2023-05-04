﻿namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class AnimMgmt
    {
        private AnimMgmt()
        {
            panelMgmt = PanelMgmt.Instance;
            AnimateMain = "";
            IsContinuous = AnimationsTable.Instance.IsContinuous;
            DiscontinueButtonStatus = "";
        }
        private static AnimMgmt instance;
        private static object instanceLock = new object();
        public static AnimMgmt Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new AnimMgmt();
                    return instance;
                }
            }
        }

        private PanelMgmt panelMgmt;

        public string AnimateMain { get; private set; }
        public List<bool> IsContinuous { get; private set; }
        public string DiscontinueButtonStatus { get; private set; }
        public event Action<string> OnAnimMgmtChanged;

        public void PlayAnimation(int animationIndex) =>
            PlayAnimation(animationIndex, IsContinuous[animationIndex]);
        public void PlayAnimation(int animationIndex, bool isContinuous)
        {
            if (AnimateMain == $"main{animationIndex + 1}-infinite" || AnimateMain == $"main{animationIndex + 1}")
                SetAnimateMainAndDiscontinueButton("", "");
            else
            {
                if (isContinuous)
                    SetAnimateMainAndDiscontinueButton($"main{animationIndex + 1}-infinite", "discontinue-button-on");
                else
                    SetAnimateMainAndDiscontinueButton($"main{animationIndex + 1}", "");
            }
            RaiseEventOnAnimMgmtChanged();
        }
        public void SetAnimateMainAndDiscontinueButton(string animation, string discontinue)
        {
            AnimateMain = animation;
            if (discontinue == "")
                panelMgmt.DeactivatePanel(8);
            else
                panelMgmt.ActivatePanel(8);
            // DiscontinueButtonStatus = discontinue;
        }
        private void RaiseEventOnAnimMgmtChanged()
        {
            OnAnimMgmtChanged?.Invoke("");
        }
    }
}
