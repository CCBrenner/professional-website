namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class QueenBee : Bee
    {
        public QueenBee() : base(WorkerType.Queen)
        {
            AssignBee(WorkerType.HoneyManufacturer);
            AssignBee(WorkerType.NectarCollector);
            AssignBee(WorkerType.EggCare);
            CostPerShift = 2.15F;
            UpdateStatusReport();
        }

        public float EGGS_PER_SHIFT = 0.45F;
        public float HONEY_PER_UNASSIGNED_WORKER = 0.5F;

        private IWorker[] workers = {};  // another option: private IWorker[] workers = new Worker[0];
        public float Eggs { get; private set; }
        public float UnassignedWorkers { get; private set; } = 3;

        // public event PropertyChangedEventHandler PropertyChanged;  // (uses INotifyPropertyChanged)

        public string StatusReport { get; private set; }
        public override float CostPerShift { get; protected set; }

        private void AddWorker(IWorker newWorker) 
        {
            if (UnassignedWorkers >= 1)
            {
                UnassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = newWorker;
            }
        }
        public void AssignBee(WorkerType job)
        {
            if (job == WorkerType.HoneyManufacturer) AddWorker(new HoneyManufacturerBee());
            else if (job == WorkerType.NectarCollector) AddWorker(new NectarCollectorBee());
            else if (job == WorkerType.EggCare) AddWorker(new EggCareBee(this));
            UpdateStatusReport();
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (Eggs >= eggsToConvert)
            {
                Eggs -= eggsToConvert;
                UnassignedWorkers += eggsToConvert;
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
            StatusReport = $"{HoneyVault.StatusReport}" +
                $"\n\nQueen's report:" +
                $"\nEgg count: {Eggs.ToString("0")}" +
                $"\nUnassigned workers: {UnassignedWorkers.ToString("0")}" +
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
        protected override void DoJob()
        {
            // Lay egg portion
            Eggs += EGGS_PER_SHIFT;

            // Have bees do their jobs
            foreach(IWorker worker in workers)
                worker.WorkTheNextShift();

            // Feed unassigned workers
            HoneyVault.ConsumeHoney((float)Math.Floor(UnassignedWorkers) * HONEY_PER_UNASSIGNED_WORKER);

            // Give status report
            UpdateStatusReport();
        }
        public int GetWorkerCount(WorkerType workerType)
        {
            int count = 0;
            foreach (IWorker worker in workers)
                if (worker.Job == workerType) count += 1;
            return count;
        }
        public int GetWorkerCount() => workers.Length;
    }
}
