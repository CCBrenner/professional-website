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

        /// <summary>
        /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
        /// </summary>
        /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
        public void PlayAnimation(int animationIndex) =>
            PlayAnimation(animationIndex, IsContinuous[animationIndex]);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
        /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
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
        /// <summary>
        /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
        /// </summary>
        public void DiscontinueAnimation() =>
            SetAnimateMainAndDiscontinueButton("", "");
        /// <summary>
        /// Sets the appropriate animation and discontinue class values.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="discontinue"></param>
        private void SetAnimateMainAndDiscontinueButton(string animation, string discontinue)
        {
            AnimateMain = animation;
            if (discontinue == "")
                panelMgmt.DeactivatePanel(8);
            else
                panelMgmt.ActivatePanel(8);
        }
        /// <summary>
        /// Updates the component that consumes it when a method in the AnimMgmt class that consumes this method invokes/signals that a change to the state of it has occurred.
        /// </summary>
        private void RaiseEventOnAnimMgmtChanged()
        {
            OnAnimMgmtChanged?.Invoke("");
        }
    }
}
