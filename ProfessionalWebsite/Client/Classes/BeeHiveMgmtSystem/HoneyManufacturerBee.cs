namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    class HoneyManufacturerBee : Bee
    {
        public HoneyManufacturerBee() : base(WorkerType.HoneyManufacturer)
        {
            CostPerShift = 1.7F;
        }

        public const float NECTAR_PROCESSED_PER_SHIFT = 33.15F;

        public override float CostPerShift { get; protected set; }

        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }
    }
}
