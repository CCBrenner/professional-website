namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    class NectarCollectorBee : Bee
    {
        public NectarCollectorBee() : base(WorkerType.NectarCollector)
        {
            CostPerShift = 1.95F;
        }

        public const float NECTAR_COLLECTED_PER_SHIFT = 33.25F;

        public override float CostPerShift { get; protected set; }

        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }
    }
}
