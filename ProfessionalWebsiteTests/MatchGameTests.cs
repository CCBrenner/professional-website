using ProfessionalWebsite.Client.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfessionalWebsiteTests
{
    [TestClass]
    public class MatchGameTests
    {
        [TestMethod]
        public void TestInitializeGame()
        {
            Random random = new Random();
            List<string> animalEmoji = new List<string>()
            {
                "🦍", "🦍",
                "🦊", "🦊",
                "🦄", "🦄",
                "🐖", "🐖",
                "🦥", "🦥",
                "🦆", "🦆",
                "🐋", "🐋",
                "🐌", "🐌"
            };

            var matchGame = new MatchGame();
            matchGame.InitializeGame();
        }
        /*
        [TestMethod]
        public void TestUserSelectsBlock()
        {
        }

        [TestMethod]
        public void TestUserSelectsNewGamePrompt()
        {

        }*/
    }
}