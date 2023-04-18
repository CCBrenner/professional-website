namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public sealed class HoneyVault  // sealed so that it is the only instance due to no classes being able to inherit from it
    {
        private HoneyVault() { }  // private constructor; not externally accessible
        private static HoneyVault? instance = null;  // single instance definable only during the first GET of the instance
        private static readonly object lockObject = new object();  // used for lock so that only one thread has access to the instance at a time
        public static HoneyVault Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new HoneyVault();
                    return instance;
                }
            }
        }

        private const float NECTAR_CONVERSION_RATIO = 0.19F;
        private const float LOW_LEVEL_WARNING = 10F;

        public float Honey { get; private set; } = 25F;
        public float Nectar { get; private set; } = 100F;
        public string Notifications 
        { 
            get 
            {
                var result = "";
                if (Honey < LOW_LEVEL_WARNING)
                    result += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (Nectar < LOW_LEVEL_WARNING)
                    result += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                return result;
            }
        }

        public string StatusReport
        {
            get
            {
                return $"\nVault report:" +
                    $"\n{Honey} units of Honey" +
                    $"\n{Nectar} units of Nectar" +
                    $"{Notifications}";
            }
        }

        public void CollectNectar(float nectarToStore) { Nectar += nectarToStore > 0F ? nectarToStore : 0F; }
        public void ConvertNectarToHoney(float amountToConvert)
        {
            amountToConvert = amountToConvert > Nectar ? Nectar : amountToConvert;
            Nectar -= amountToConvert;
            Honey += amountToConvert * NECTAR_CONVERSION_RATIO;
        }
        public bool ConsumeHoney(float consumptionAmount)
        {
            if (consumptionAmount < Honey)
            {
                Honey -= consumptionAmount;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            Honey = 25F;
            Nectar = 100F;
        }
    }
}
