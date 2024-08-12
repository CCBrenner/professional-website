using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel.Contracts;
using System.Timers;

namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public sealed class QueenBee : Bee
{
    public QueenBee(IBeeHiveSettings settings) : base(EWorkerType.Queen)
    {
        this.settings = settings;

        UnassignedBeeCount = 3 + settings.UnassignedBeeStaringAmount;
        AssignBee(EWorkerType.HoneyMaker);
        AssignBee(EWorkerType.NectarCollector);
        AssignBee(EWorkerType.EggNurse);

        HiveIsBankrupt = false;
        Eggs = settings.StartingAmountOfEggs;
        CurrentDay = settings.StartingDay;

        EggsProducedPerShift = settings.EggsProducedPerShift;
        EggConversionProgress = settings.InitialEggConversionProgress;

        CostPerShift = settings.QueenBeeCostPerShift;
        HoneyConsumedPerUnassignedBee = settings.HoneyConsumedPerUnassignedBee;

        TimerIsBeingUsed = settings.TimerIsBeingUsed;
        TimerRunning = settings.TimerRunning;

        UpdateStatusReport();

        InitializeTimer();
    }
    public void Reset()
    {
        HiveIsBankrupt = false;
        UnassignedBeeCount = 3 + settings.UnassignedBeeStaringAmount;
        workers = new();
        AssignBee(EWorkerType.HoneyMaker);
        AssignBee(EWorkerType.NectarCollector);
        AssignBee(EWorkerType.EggNurse);
        Eggs = settings.StartingAmountOfEggs;
        EggConversionProgress = settings.InitialEggConversionProgress;
        CurrentDay = settings.StartingDay;
        UpdateStatusReport();
        InitializeTimer();
    }
    public void ResetSelfAndReferencedVault()
    {
        Reset();
        vault.Reset();
    }

    private IBeeHiveSettings settings;

    private List<IWorker> workers = new();  // another option: private IWorker[] workers = new Worker[0];
    private Vault vault = Vault.Instance;
    private System.Timers.Timer timer;

    public float EggsProducedPerShift { get; private set; }
    public float HoneyConsumedPerUnassignedBee { get; private set; }

    public int CurrentDay { get; private set; }
    public static float Eggs { get; private set; }
    public float EggConversionProgress { get; private set; }

    public int AssignedWorkersCount { get { return workers.Count; } }
    public int UnassignedBeeCount { get; private set; }
    public string StatusReport { get; private set; }
    public override float CostPerShift { get; set; }
    public bool HiveIsBankrupt { get; private set; } = false;
    public float TotalCostPerShift
    { 
        get 
        {
            float totalCost = CostPerShift + (HoneyConsumedPerUnassignedBee * UnassignedBeeCount);  // queen and unassigned workers
            foreach (Bee worker in workers) totalCost += worker.GetCostOfThisShift();  // assigned workers
            return totalCost;
        }
    }

    // Timer-related:
    public bool TimerIsBeingUsed;
    public bool TimerRunning;
    public event Action<bool> OnTimerInterval;
    // public event Action<bool> OnTimerToggleFromControls;
    // public event PropertyChangedEventHandler PropertyChanged;  // (uses INotifyPropertyChanged)

    public void AssignBee(EWorkerType job)
    {
        if (HiveIsBankrupt) return;
        
        if (job == EWorkerType.HoneyMaker)
            AddWorker(new HoneyMaker(
                settings.HoneyMakerCostPerShift, 
                settings.HoneyConsumedPerUnassignedBee,
                settings.NectarProcessedPerShift));
        else if (job == EWorkerType.NectarCollector)
            AddWorker(new NectarCollector(settings.NectarCollectorCostPerShift, settings.NectarCollectedPerShift));
        else if (job == EWorkerType.EggNurse)
            AddWorker(new EggNurse(
                this, 
                settings.EggNurseCostPerShift, 
                settings.HoneyConsumedPerUnassignedBee,
                settings.EggNurseCareProgressPerShift));
        UpdateStatusReport();
    }

    public override float GetCostOfThisShift() => CostPerShift;
    public override void WorkTheNextShift()
    {
        if (HiveIsBankrupt) return;
        if (vault.ConsumeHoney(CostPerShift)) DoJob();
        UpdateHiveIsBankrupt();
    }
    protected override void DoJob()
    {
        LayEggs();
        OrderBeesToDoTheirJobs();
        FeedUnassignedBees();
        CurrentDay++;
        UpdateStatusReport();
        if (TimerIsBeingUsed) RestartTimer();
    }
    private float LayEggs() => Eggs += EggsProducedPerShift;
    private void OrderBeesToDoTheirJobs()
    {
        foreach (IWorker worker in workers)
            worker.WorkTheNextShift();
    }

    private void FeedUnassignedBees() => vault.ConsumeHoney(UnassignedBeeCount * HoneyConsumedPerUnassignedBee);
    private void UpdateStatusReport()
    {
        StatusReport = $"{vault.StatusReport}" +
            $"\n\nQueen's report:" +
            $"\nEgg count: {Eggs}" +
            $"\nUnassigned workers: {UnassignedBeeCount}" +
            $"\n{WorkerStatus(EWorkerType.HoneyMaker)}" +
            $"\n{WorkerStatus(EWorkerType.NectarCollector)}" +
            $"\n{WorkerStatus(EWorkerType.EggNurse)}" +
            $"\nTOTAL WORKERS: {workers.Count}";
        //OnPropertyChanged("StatusReport");  // This is saying "invoke OnPropertyChanged()" and "the reference
        // is the tag that has "StatusReport" as its Binding"
    }

    public void CareForEggs(float eggsToConvert)  // Only EggNurse calls this method
    {
        EggConversionProgress += eggsToConvert;
        if (EggConversionProgress < 1) return;
        EggConversionProgress--;
        Eggs--;
        UnassignedBeeCount++;
    }
    public int GetWorkersCountByWorkerType(EWorkerType workerType)
    {
        int count = 0;
        foreach (IWorker worker in workers)
            if (worker.Job == workerType) count += 1;
        return count;
    }
    private void UpdateHiveIsBankrupt() => HiveIsBankrupt = vault.Honey < CostPerShift ? true : false;

    private void AddWorker(IWorker newWorker)
    {
        if (UnassignedBeeCount < 1) return;
        UnassignedBeeCount--;
        workers.Add(newWorker);
    }
    private string WorkerStatus(EWorkerType workerType)
    {
        int count = 0;
        foreach (IWorker worker in workers) 
            if (worker.Job == workerType) count += 1;
        string s = "s";
        if (count == 1) s = "";
        return $"{count} {workerType} bee{s}";
    }

    // Timer-related:
    private void InitializeTimer()
    {
        timer = new(1500);
        timer.Elapsed += new(PerTimerInterval);
        TimerRunning = false;
    }
    private void PerTimerInterval(object sender, ElapsedEventArgs e)
    {
        if (!TimerIsBeingUsed)
        {
            StopTimer();
            return;
        }

        WorkTheNextShift();

        if (HiveIsBankrupt) StopTimer();

        RaiseEventOnTimerInterval();
    }
    private void RestartTimer()
    {
        timer.Stop();
        InitializeTimerDuringRestart();
        timer.Start();
    }
    private void InitializeTimerDuringRestart()
    {
        timer = new(1500);
        timer.Elapsed += new(PerTimerInterval);
    }
    public void StartTimer()
    {
        timer.Start();
        TimerRunning = true;
    }
    private void StopTimer()
    {
        timer.Stop();
        TimerRunning = false;
    }
    public void RaiseEventOnTimerInterval()
    {
        // bool here is useless except to satisfy a requirement of the event type
        OnTimerInterval?.Invoke(true);
    }
    /*private void OnPropertyChanged(string name)
    {
        // This says, "if any properties have changed, then update the object in the WPF Window.Resources
        // to reflect the current properties listed here (event handling)
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }*/
}
