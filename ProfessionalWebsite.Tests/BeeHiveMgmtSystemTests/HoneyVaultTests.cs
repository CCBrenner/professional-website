using ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests
{
    [TestClass]
    public class HoneyVaultTests
    {
        private float honeyStartingAmount;
        private float nectarStartingAmount;
        private const float NECTAR_CONVERSION_RATIO = 0.19F;  // must match HoneyVault singleton value
        private const float LOW_LEVEL_WARNING = 10F;  // must match HoneyVault singleton value

        private HoneyVault honeyVault;

        [TestInitialize]
        public void Initialize()
        {
            honeyVault = HoneyVault.Instance;  // initialize new singleton for each test method
            honeyStartingAmount = honeyVault.Honey;
            nectarStartingAmount = honeyVault.Nectar;
        }

        [TestMethod]
        public void TestConsumeHoneyPositiveValueAddsToTotalHoney()
        {
            honeyVault.CollectNectar(23F);
            Assert.AreEqual(nectarStartingAmount + 23F, honeyVault.Nectar);

            // Revert changes for other tests

        }
        [TestMethod]
        public void TestConsumeHoneyNegativeValueCausesNoChangeToTotalHoney()
        {
            honeyVault.CollectNectar(-23F);
            // no change; subtract 0
            Assert.AreEqual(nectarStartingAmount, honeyVault.Nectar);
        }
        [TestMethod]
        public void TestConvertNectarToHoneyIfMoreToConvertThanAvailableThenConvertOnlyTheNectarThatIsAvailableNotMore()
        {
            honeyVault.ConvertNectarToHoney(133.15F);
            Assert.AreEqual(0, honeyVault.Nectar);
        }
        [TestMethod]
        public void TestConvertNectarToHoneyIfLessToConvertThanIsAvailableConvertLesserAmtAndSubtractFromTotalNectar()
        {
            honeyVault.ConvertNectarToHoney(13.15F);
            Assert.AreEqual(nectarStartingAmount - 13.15F, honeyVault.Nectar);
            Assert.AreEqual(honeyStartingAmount + (13.15F * NECTAR_CONVERSION_RATIO), honeyVault.Honey);
        }
        [TestMethod]
        public void TestConvertNectarToHoneyIfLessToConvertThanIsAvailableConvertLesserAmtAndAddConvertedHoneyAmountToTotalHoney()
        {
            honeyVault.ConvertNectarToHoney(13.15F);
            Assert.AreEqual(honeyStartingAmount + (13.15F * NECTAR_CONVERSION_RATIO), honeyVault.Honey);
        }
        [TestMethod]
        public void TestConsumerHoneyIfMoreToConsumeThanHoneyAvailableDontConsumeAndReturnFalse()
        {
            bool result = honeyVault.ConsumeHoney(30F); // greater than starting amount
            Assert.AreEqual(false, result);
            Assert.AreEqual(honeyStartingAmount, honeyVault.Honey);
        }
        [TestMethod]
        public void TestConsumerHoneyIfEnoughHoneyToConsumeThenConsumeHoneyAndReturnTrue()
        {
            bool result = honeyVault.ConsumeHoney(22F); // less than starting amount
            Assert.AreEqual(true, result);
            Assert.AreEqual(honeyStartingAmount - 22F, honeyVault.Honey);
        }
    }
}
