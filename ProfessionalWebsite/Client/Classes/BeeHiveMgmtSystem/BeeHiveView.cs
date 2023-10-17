namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public sealed class BeeHiveView
    {
        private BeeHiveView() { }
        private static BeeHiveView instance = null;
        private static readonly object lockObject = new object();
        private BeeHiveController beeHiveController = BeeHiveController.Instance;
        private bool SettingsPanelIsShowing = false;
        public static BeeHiveView Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new BeeHiveView();
                    return instance;
                }
            }
        }
        public List<string> interfaceClasses = new()
        {
            "btn-secondary",
            "btn-secondary",
            "btn-secondary",
            "btn-primary",
        };
        public List<string> workertypeButtonsClasses = new()
        {
            "workertype-active",
            "workertype-inactive",
            "workertype-inactive"
        };
        public string assignButtons = "assign-buttons-inactive";

        public WorkerType selectedWorkerType = WorkerType.HoneyManufacturer;

        public ActiveUI VisibleUI = ActiveUI.V3;
        public bool VisibleUIIsV3 => VisibleUI == ActiveUI.V3;
        public string PageMainContentBlur => SettingsPanelIsShowing ? "page-maincontent-blurred" : "";
        public string SettingsStatus => SettingsPanelIsShowing ? "settings-visible" : "";
        public string SettingsBehindPanelStatus = "";  // behind-panel-visible

        public void SetVisibleUI(ActiveUI uiChoice)
        {
            VisibleUI = uiChoice;

            interfaceClasses = interfaceClasses
                .Select(x => x = "btn-secondary")
                .ToList();

            if (VisibleUI == ActiveUI.WPF)
                interfaceClasses[0] = "btn-primary";
            else if (VisibleUI == ActiveUI.V1)
                interfaceClasses[1] = "btn-primary";
            else if (VisibleUI == ActiveUI.V2)
                interfaceClasses[2] = "btn-primary";
            else if (VisibleUI == ActiveUI.V3)
                interfaceClasses[3] = "btn-primary";
        }

        public void UpdateSelectedWorkerType(WorkerType workerType)
        {
            selectedWorkerType = workerType;

            workertypeButtonsClasses = workertypeButtonsClasses
                .Select(x => x = "workertype-inactive")
                .ToList();

            if (selectedWorkerType == WorkerType.HoneyManufacturer)
                workertypeButtonsClasses[0] = "workertype-active";
            else if (selectedWorkerType == WorkerType.NectarCollector)
                workertypeButtonsClasses[1] = "workertype-active";
            else if (selectedWorkerType == WorkerType.EggCare)
                workertypeButtonsClasses[2] = "workertype-active";
        }

        public void AssignBee(WorkerType workerType)
        {
            beeHiveController.AssignBee(workerType);
            UpdateAssignButtons();
        }

        public void WorkShift()
        {
            beeHiveController.WorkTheNextShift();
            UpdateAssignButtons();
        }
        public void UpdateAssignButtons() =>
            assignButtons = (beeHiveController.UnassignedWorkersCount >= 1)
                ? "assign-buttons-active"
                : "assign-buttons-inactive";
        public void StartTimer()
        {
            beeHiveController.Reset();
            beeHiveController.StartTimer();
        }
    }
}
