namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class HoneyManufacturerBee : Bee
    {
        public HoneyManufacturerBee() : base(WorkerType.HoneyManufacturer) { }

        /*
        private Settings settings = Settings.Api;
        public float NectarProcessedPerShift =>
            settings.HoneyManufacturerNectarProcessedPerShift;
        public override float CostPerShift =>
            settings.HoneyManufacturerCostPerShift;
        */
        private HoneyVault vault = HoneyVault.Instance;

        public float NectarProcessedPerShift = 33.15F;
        public override float CostPerShift => 1.7F;

        protected override void DoJob() =>
            vault.ConvertNectarToHoney(NectarProcessedPerShift);
    }
}
