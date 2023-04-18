namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    class EggCareBee : Bee
    {
        public EggCareBee(QueenBee queen) : base(WorkerType.EggCare)
        {
            CostPerShift = 1.53F;
            this.queen = queen;
        }

        public const float CARE_PROGRESS_PER_SHIFT = 0.15F;

        public override float CostPerShift { get; protected set; }

        private QueenBee queen;

        protected override void DoJob(HoneyVault honeyVault)
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }
    }
}
