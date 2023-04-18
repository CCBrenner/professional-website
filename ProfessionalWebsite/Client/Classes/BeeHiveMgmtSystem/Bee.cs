namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public abstract class Bee : IWorker
    {
        public Bee(WorkerType title)
        {
            Job = title;
        }

        public WorkerType Job { get; set; }
        public abstract float CostPerShift { get; protected set; }

        public void WorkTheNextShift()
        {
            HoneyVault honeyVault = HoneyVault.Instance;
            if (honeyVault.ConsumeHoney(CostPerShift))
                DoJob(honeyVault);
        }
        protected abstract void DoJob();
    }
}
