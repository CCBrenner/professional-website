using ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests
{
    [TestClass]
    public class HoneyVaultTests
    {
        /// <summary>
        /// unit testing for this class requires reset of honey and nectar values after each test
        /// this means a broken test causes all following tests to not have proper setup for tests
        /// tests must be resolved in order; this is due to high level of encapsulation
        /// </summary>

        private float honeyStartingAmount = 25F;
        private float nectarStartingAmount = 100F;
        private const float NECTAR_CONVERSION_RATIO = 0.19F;  // must match HoneyVault singleton value
        private const float LOW_LEVEL_WARNING = 10F;  // must match HoneyVault singleton value

        private HoneyVault honeyVault = HoneyVault.Instance;

        [TestInitialize]
        public void Initialize()
        {
            honeyVault.Reset();
            Assert.AreEqual(honeyStartingAmount, honeyVault.Honey);
            Assert.AreEqual(nectarStartingAmount, honeyVault.Nectar);
        }

        [TestMethod]
        public void TestConsumeHoneyPositiveValueAddsToTotalHoney()
        {
            honeyVault.CollectNectar(23F);
            Assert.AreEqual(nectarStartingAmount + 23F, honeyVault.Nectar);
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
        [TestMethod]
        public void TestResetInitializesHoneyAndNectarAmountsForANewAttempt()
        {
            bool result = honeyVault.ConsumeHoney(22F);
            honeyVault.ConvertNectarToHoney(45F);
            // these need to pass as starting assumptions
            Assert.AreEqual(true, result);
            Assert.AreNotEqual(honeyStartingAmount, honeyVault.Honey);
            Assert.AreNotEqual(nectarStartingAmount, honeyVault.Nectar);

            honeyVault.Reset();
            // tests
            Assert.AreEqual(honeyStartingAmount, honeyVault.Honey);
            Assert.AreEqual(nectarStartingAmount, honeyVault.Nectar);
        }
        [TestMethod]
        public void TestNotificationsReturnsNothingWhenHoneyAndNectarAreAboveNotifcationThreshold()
        {
            // default values are above notification threshold
            Assert.AreEqual(false, honeyVault.Honey < LOW_LEVEL_WARNING);
            Assert.AreEqual(false, honeyVault.Nectar < LOW_LEVEL_WARNING);
            Assert.AreEqual("", honeyVault.Notifications);
        }
        [TestMethod]
        public void TestNotificationsReturnsHoneyLowIfHoneyIsBelowNotifcationThreshold()
        {
            Assert.AreEqual(25F, honeyVault.Honey);
            honeyVault.ConsumeHoney(20F);
            // honey low and not nectar
            /*
            Assert.AreEqual(true, honeyVault.Honey < LOW_LEVEL_WARNING);
            Assert.AreEqual(false, honeyVault.Nectar < LOW_LEVEL_WARNING);*/
            Assert.AreEqual("\nLOW HONEY - ADD A HONEY MANUFACTURER", honeyVault.Notifications);
        }
        [TestMethod]
        public void TestNotificationsReturnsNectarLowIfNectarIsBelowNotifcationThreshold()
        {
            Assert.AreEqual(nectarStartingAmount, honeyVault.Nectar);
            honeyVault.ConvertNectarToHoney(95F);
            // nectar low and not honey
            /*
            Assert.AreEqual(true, honeyVault.Nectar < LOW_LEVEL_WARNING);
            Assert.AreEqual(false, honeyVault.Honey < LOW_LEVEL_WARNING);*/
            Assert.AreEqual("\nLOW NECTAR - ADD A NECTAR COLLECTOR", honeyVault.Notifications);
        }
        [TestMethod]
        public void TestNotificationsReturnsNectarAndHoneyLowIfBothAreBelowNotifcationThreshold()
        {
            Assert.AreEqual(honeyStartingAmount, honeyVault.Honey);
            Assert.AreEqual(nectarStartingAmount, honeyVault.Nectar);

            honeyVault.ConvertNectarToHoney(95F);
            honeyVault.ConsumeHoney(40F);
            
            Assert.AreEqual(true, honeyVault.Honey < LOW_LEVEL_WARNING);
            Assert.AreEqual(true, honeyVault.Nectar < LOW_LEVEL_WARNING);
            string expected = "\nLOW HONEY - ADD A HONEY MANUFACTURER\nLOW NECTAR - ADD A NECTAR COLLECTOR";
            Assert.AreEqual(expected, honeyVault.Notifications);
        }
        [TestMethod]
        public void TestStatusReportCorrectlyRepresentsHoneyAndNectarInStatusWithoutNotifications()
        {
            // intentionally not testing for notifications because other test accounts for that
            string expected = $"\nVault report:" +
                $"\n{honeyVault.Honey} units of Honey" +
                $"\n{honeyVault.Nectar} units of Nectar";
            Assert.AreEqual(expected, honeyVault.StatusReport);
        }
    }
}
