namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public sealed class HoneyVault  // sealed so that it is the only instance due to no classes being able to inherit from it
    {
        private HoneyVault()  // private constructor; not externally accessible
        {
            /*
            nectarConversionRatio = settings.VaultNectarConversionRatio;
            lowLevelWarning = settings.VaultLowLevelWarning;
            Honey = settings.VaultHoney;
            Nectar  = settings.VaultNectar;
            */
            NectarConversionRatio = 0.19F;
            lowLevelWarning = 10F;
            Honey = 25F;
            Nectar = 100F;
        }  
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
        public void Reset()
        {
            /*
            Honey = settings.VaultHoney;
            Nectar = settings.VaultNectar;
            */
            Honey = 25F;
            Nectar = 100F;
        }

        // private Settings settings = Settings.Instance;
        private float lowLevelWarning;

        public float Honey { get; private set; }
        public float Nectar { get; private set; }
        public float NectarConversionRatio { get; private set; }
        public string Notifications
        { 
            get 
            {
                var result = "";
                if (Honey < lowLevelWarning)
                    result += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (Nectar < lowLevelWarning)
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
            Honey += amountToConvert * NectarConversionRatio;
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
    }
}
