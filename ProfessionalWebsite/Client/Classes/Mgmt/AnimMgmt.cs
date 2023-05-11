namespace ProfessionalWebsite.Client.Classes.Mgmt
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
        private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";

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
                    SetAnimateMainAndDiscontinueButton($"main{animationIndex + 1}-infinite", DISCONTINUE_BTN_ACTIVE_CLASS_NAME);
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
        }
        private void RaiseEventOnAnimMgmtChanged()
        {
            OnAnimMgmtChanged?.Invoke("");
        }
    }
}
