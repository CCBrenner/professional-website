namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

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
                if (instance == null) instance = new BeeHiveView();
                return instance;
            }
        }
    }
    public List<string> interfaceClasses = new()
    {
        "btn-secondary",
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

    public EWorkerType selectedWorkerType = EWorkerType.HoneyMaker;

    public EActiveUI VisibleUI = EActiveUI.V4;
    public bool VisibleUIIsV3 => VisibleUI == EActiveUI.V3;
    public bool VisibleUIIsV4 => VisibleUI == EActiveUI.V4;
    public string PageMainContentBlur => SettingsPanelIsShowing ? "page-maincontent-blurred" : "";
    public string SettingsStatus => SettingsPanelIsShowing ? "settings-visible" : "";
    public string SettingsBehindPanelStatus = "";  // behind-panel-visible

    public void SetVisibleUI(EActiveUI uiChoice)
    {
        VisibleUI = uiChoice;

        interfaceClasses = interfaceClasses
            .Select(x => x = "btn-secondary")
            .ToList();

        if (VisibleUI == EActiveUI.WPF)
            interfaceClasses[0] = "btn-primary";
        else if (VisibleUI == EActiveUI.V1)
            interfaceClasses[1] = "btn-primary";
        else if (VisibleUI == EActiveUI.V2)
            interfaceClasses[2] = "btn-primary";
        else if (VisibleUI == EActiveUI.V3)
            interfaceClasses[3] = "btn-primary";
    }

    public void UpdateSelectedWorkerType(EWorkerType workerType)
    {
        selectedWorkerType = workerType;

        workertypeButtonsClasses = workertypeButtonsClasses
            .Select(x => x = "workertype-inactive")
            .ToList();

        if (selectedWorkerType == EWorkerType.HoneyMaker)
            workertypeButtonsClasses[0] = "workertype-active";
        else if (selectedWorkerType == EWorkerType.NectarCollector)
            workertypeButtonsClasses[1] = "workertype-active";
        else if (selectedWorkerType == EWorkerType.EggNurse)
            workertypeButtonsClasses[2] = "workertype-active";
    }

    public void AssignBee(EWorkerType workerType)
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
