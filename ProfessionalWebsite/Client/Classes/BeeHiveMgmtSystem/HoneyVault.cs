namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    static class HoneyVault
    {
        public const float NECTAR_CONVERTION_RATIO = 0.19F;
        public const float LOW_LEVEL_WARNING = 10F;

        public static float Honey { get; private set; } = 25F;
        public static float Nectar { get; private set; } = 100F;
        public static string Notifications 
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

        public static string StatusReport
        {
            get
            {
                return $"\nVault report:" +
                    $"\n{Honey.ToString("0.#")} units of Honey" +
                    $"\n{Nectar.ToString("0.#")} units of Nectar" +
                    $"{Notifications}";
            }
        }

        public static void CollectNectar(float nectarToStore) { Nectar += nectarToStore > 0F ? nectarToStore : 0F; }
        public static void ConvertNectarToHoney(float amountToConvert)
        {
            amountToConvert = amountToConvert > Nectar ? Nectar : amountToConvert;
            Nectar -= amountToConvert;
            Honey += amountToConvert * NECTAR_CONVERTION_RATIO;
        }
        public static bool ConsumeHoney(float consumptionAmount)
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
