using ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;

namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHive
    {
        public HoneyVault Vault = HoneyVault.Instance;
        public QueenBee Queen = new QueenBee(HoneyVault.Instance);

        public void Reset()
        {
            Vault.Reset();
            Queen.Reset();
        }
    }
}
