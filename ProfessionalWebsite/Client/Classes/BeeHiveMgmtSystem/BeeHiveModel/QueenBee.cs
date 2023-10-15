using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class QueenBee : Bee
    {
        public QueenBee() : base(WorkerType.Queen)
        {
            //UnassignedWorkersCount = 3F + settings.QueenUnassignedWorkersCount;
            UnassignedWorkersCount = 3F;
            AssignBee(WorkerType.HoneyManufacturer);
            AssignBee(WorkerType.NectarCollector);
            AssignBee(WorkerType.EggCare);

            HiveIsBankrupt = false;
            //Eggs = settings.QueenEggs;
            Eggs =  0F;
            CurrentDay = 1;

            TimerIsBeingUsed = false;
            TimerRunning = false;

            UpdateStatusReport();

            InitializeTimer();
        }
        public void Reset()
        {
            HiveIsBankrupt = false;
            //UnassignedWorkersCount = 3F + settings.QueenUnassignedWorkersCount;
            UnassignedWorkersCount = 3F;
            workers = new IWorker[0];
            AssignBee(WorkerType.HoneyManufacturer);
            AssignBee(WorkerType.NectarCollector);
            AssignBee(WorkerType.EggCare);
            //Eggs = settings.QueenEggs;
            Eggs = 0F;
            CurrentDay = 1;
            UpdateStatusReport();
            InitializeTimer();
        }
        public void ResetSelfAndReferencedVault()
        {
            Reset();
            vault.Reset();
        }

        private IWorker[] workers = { };  // another option: private IWorker[] workers = new Worker[0];
        /*
        private Settings settings = Settings.Instance;
        public float EggsPerShift => settings.QueenEggsPerShift;
        public float HoneyPerUnassignedWorker => settings.QueenHoneyPerUnassignedWorker;
        */
        private HoneyVault vault = HoneyVault.Instance;
        private System.Timers.Timer timer;

        public float EggsPerShift => 0.45F;
        public float HoneyPerUnassignedWorker => 0.5F;

        public int CurrentDay { get; private set; }
        public float Eggs { get; private set; }
        public int AssignedWorkersCount { get { return workers.Length; } }
        public float UnassignedWorkersCount { get; private set; }
        public string StatusReport { get; private set; }
        //public override float CostPerShift => settings.QueenCostPerShift;
        public override float CostPerShift => 2.15F;
        public bool HiveIsBankrupt { get; private set; }
        // public event PropertyChangedEventHandler PropertyChanged;  // (uses INotifyPropertyChanged)
        public bool TimerIsBeingUsed;
        public bool TimerRunning;
        public event Action<bool> OnTimerInterval;
        //public event Action<bool> OnTimerToggleFromControls;
        public float TotalCostPerShift
        { 
            get 
            {
                float totalCost = 0F;
                totalCost += CostPerShift;  // the Queen
                foreach (Bee worker in workers)
                    totalCost += worker.CostPerShift;  // assigned workers
                totalCost += (HoneyPerUnassignedWorker * (float)Math.Floor(UnassignedWorkersCount));  // unassigned workers
                return totalCost;
            }
        }
        public override void WorkTheNextShift()
        {
            if (!HiveIsBankrupt)
            {
                if (vault.ConsumeHoney(CostPerShift))
                {
                    DoJob();
                    UpdateHiveIsBankrupt();
                }
            }
        }
        protected override void DoJob()
        {
            LayEggs();
            OrderBeesToDoTheirJobs();
            FeedUnassignedWorkers();
            CurrentDay++;
            UpdateStatusReport();
            if (TimerIsBeingUsed)
                RestartTimer();
        }
        public void AssignBee(WorkerType job)
        {
            if (!HiveIsBankrupt)
            {
                if (job == WorkerType.HoneyManufacturer) AddWorker(new HoneyManufacturerBee());
                else if (job == WorkerType.NectarCollector) AddWorker(new NectarCollectorBee());
                else if (job == WorkerType.EggCare) AddWorker(new EggCareBee(this));
                UpdateStatusReport();
            }
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (Eggs >= eggsToConvert)
            {
                Eggs -= eggsToConvert;
                UnassignedWorkersCount += eggsToConvert;
            }
        }
        public int GetWorkersCountByWorkerType(WorkerType workerType)
        {
            int count = 0;
            foreach (IWorker worker in workers)
                if (worker.Job == workerType) count += 1;
            return count;
        }
        private void UpdateHiveIsBankrupt() =>
            HiveIsBankrupt = vault.Honey < CostPerShift ? true : false;
        private float LayEggs() => 
            Eggs += EggsPerShift;
        private void OrderBeesToDoTheirJobs()
        {
            foreach (IWorker worker in workers)
                worker.WorkTheNextShift();
        }
        private void FeedUnassignedWorkers() => 
            vault.ConsumeHoney((float)Math.Floor(UnassignedWorkersCount) * HoneyPerUnassignedWorker);
        private void AddWorker(IWorker newWorker)
        {
            if (UnassignedWorkersCount >= 1)
            {
                UnassignedWorkersCount--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = newWorker;
            }
        }
        private string WorkerStatus(WorkerType workerType)
        {
            int count = 0;
            foreach (IWorker worker in workers) 
                if (worker.Job == workerType) count += 1;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {workerType} bee{s}";
        }
        private void UpdateStatusReport()
        { 
            StatusReport = $"{vault.StatusReport}" +
                $"\n\nQueen's report:" +
                $"\nEgg count: {Eggs}" +
                $"\nUnassigned workers: {UnassignedWorkersCount}" +
                $"\n{WorkerStatus(WorkerType.HoneyManufacturer)}" +
                $"\n{WorkerStatus(WorkerType.NectarCollector)}" +
                $"\n{WorkerStatus(WorkerType.EggCare)}" +
                $"\nTOTAL WORKERS: {workers.Length}";
            //OnPropertyChanged("StatusReport");  // This is saying "invoke OnPropertyChanged()" and "the reference
                                                // is the tag that has "StatusReport" as its Binding"
        }
        /*private void OnPropertyChanged(string name)
        {
            // This says, "if any properties have changed, then update the object in the WPF Window.Resources
            // to reflect the current properties listed here (event handling)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }*/
        private void InitializeTimer()
        {
            timer = new(1500);
            timer.Elapsed += new(PerTimerInterval);
            TimerRunning = false;
        }

        private void PerTimerInterval(object sender, ElapsedEventArgs e)
        {
            if (!TimerIsBeingUsed)
                StopTimer();
            else
            {
                WorkTheNextShift();

                if (HiveIsBankrupt)
                    StopTimer();

                RaiseEventOnTimerInterval();
            }
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
    }
}
