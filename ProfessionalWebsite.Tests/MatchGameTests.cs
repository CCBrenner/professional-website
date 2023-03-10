using ProfessionalWebsite.Client.Pages;
using System.Reflection;

namespace ProfessionalWebsite.Tests
{
    [TestClass]
    public class MatchGameTests
    {
        // start "UserSelectsBlock()" test methods
        [TestMethod]
        public void TestClickingBlockMakesItVisible()
        {
            MatchGame matchGame = new MatchGame();
            matchGame.Blocks[2].AnimalEmoji = "🦊";
            matchGame.Blocks[5].AnimalEmoji = "🐋";
            matchGame.UserSelectsBlock(matchGame.Blocks[2]);  // user clicks row 1, column 3
            Assert.AreEqual("block-showing", matchGame.Blocks[2].Visibility);
            Assert.AreEqual(false, matchGame.Blocks[2].IsMatched);
        }
        [TestMethod]
        public void TestClickingSecondBlockNotMatchingFirstBlockMakesBothHidden()
        {
            MatchGame matchGame = new MatchGame();
            matchGame.Blocks[2].AnimalEmoji = "🦊";
            matchGame.Blocks[5].AnimalEmoji = "🐋";
            matchGame.UserSelectsBlock(matchGame.Blocks[2]);  // user clicks row 1, column 3
            matchGame.UserSelectsBlock(matchGame.Blocks[5]);  // user clicks row 2, column 2; it's not a match
            Assert.AreEqual("", matchGame.Blocks[2].Visibility);
            Assert.AreEqual(false, matchGame.Blocks[2].IsMatched);
            Assert.AreEqual("", matchGame.Blocks[5].Visibility);
            Assert.AreEqual(false, matchGame.Blocks[5].IsMatched);
        }
        [TestMethod]
        public void TestInitalMatchMakesBothBlocksVisible()
        {
            MatchGame matchGame = new MatchGame();
            matchGame.Blocks[2].AnimalEmoji = "🦊";
            matchGame.Blocks[9].AnimalEmoji = "🦊";
            matchGame.UserSelectsBlock(matchGame.Blocks[2]);  // user clicks row 1, column 3
            matchGame.UserSelectsBlock(matchGame.Blocks[9]);  // user clicks row 3, column 2; it's a match
            Assert.AreEqual("block-showing", matchGame.Blocks[2].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[2].IsMatched);
            Assert.AreEqual("block-showing", matchGame.Blocks[9].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[9].IsMatched);
        }
        [TestMethod]
        public void TestClickingAMatchedBlockAllowsForNextClickToBeInitialBlock()
        {
            MatchGame matchGame = new MatchGame();
            matchGame.Blocks[2].AnimalEmoji = "🦊";
            matchGame.Blocks[9].AnimalEmoji = "🦊";
            matchGame.UserSelectsBlock(matchGame.Blocks[2]);  // user clicks row 1, column 3
            matchGame.UserSelectsBlock(matchGame.Blocks[9]);  // user clicks row 3, column 2; it's a match
            Assert.AreEqual("block-showing", matchGame.Blocks[2].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[2].IsMatched);
            Assert.AreEqual("block-showing", matchGame.Blocks[9].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[9].IsMatched);

            matchGame.UserSelectsBlock(matchGame.Blocks[2]);  // user clicks row 1, column 3 - a previously matched block (visible) - nothing happens
            Assert.AreEqual("block-showing", matchGame.Blocks[2].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[2].IsMatched);

            matchGame.Blocks[0].AnimalEmoji = "🐋";
            matchGame.Blocks[13].AnimalEmoji = "🐋";
            matchGame.UserSelectsBlock(matchGame.Blocks[0]);  // initial block
            matchGame.UserSelectsBlock(matchGame.Blocks[13]);  // compare block; it's a match
            Assert.AreEqual("block-showing", matchGame.Blocks[0].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[0].IsMatched);
            Assert.AreEqual("block-showing", matchGame.Blocks[13].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[13].IsMatched);
        }
        [TestMethod]
        public void TestEightMatchesDisplaysReplayPrompt()
        {
            MatchGame matchGame = new MatchGame();
            foreach (Block block in matchGame.Blocks)
                block.AnimalEmoji = "🐋";

            Assert.AreEqual(MatchGameStatus.Ready, matchGame.GameStatus);

            // back-to-back calls of all blocks causes 8 matches (since all are "🐋")
            foreach (Block block in matchGame.Blocks)
                matchGame.UserSelectsBlock(block);

            // all blocks have been matched & are visible
            foreach (Block block in matchGame.Blocks)
            {
                Assert.AreEqual("block-showing", block.Visibility);
                Assert.AreEqual(true, block.IsMatched);
            }
            Assert.AreEqual(MatchGameStatus.Done, matchGame.GameStatus);
        }
        [TestMethod]
        public void TestUserCantDoubleClickSameBlockAndGetMatch()
        {
            MatchGame matchGame = new MatchGame();
            matchGame.Blocks[0].AnimalEmoji = "🐋";
            matchGame.UserSelectsBlock(matchGame.Blocks[0]);  // initial block
            matchGame.UserSelectsBlock(matchGame.Blocks[0]);  // supposed match block - but should keep [0] as initial block
            Assert.AreEqual("block-showing", matchGame.Blocks[0].Visibility);
            Assert.AreEqual(false, matchGame.Blocks[0].IsMatched);

            matchGame.Blocks[5].AnimalEmoji = "🐋";
            matchGame.UserSelectsBlock(matchGame.Blocks[5]);  // match block
            Assert.AreEqual("block-showing", matchGame.Blocks[0].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[0].IsMatched);
            Assert.AreEqual("block-showing", matchGame.Blocks[5].Visibility);
            Assert.AreEqual(true, matchGame.Blocks[5].IsMatched);
        }
        // end "UserSelectsBlock()" test methods
        // start "UserSelectsNewGamePrompt()" test methods
        [TestMethod]
        public void TestUserSelectesNewGamePromptMethod()
        {
            MatchGame matchGame = new MatchGame();
            foreach (Block block in matchGame.Blocks)
                block.AnimalEmoji = "🐋";

            // back-to-back calls of all blocks causes 8 matches (since all are "🐋")
            foreach (Block block in matchGame.Blocks)
                matchGame.UserSelectsBlock(block);

            // all blocks have been matched & are visible
            foreach (Block block in matchGame.Blocks)
            {
                Assert.AreEqual("block-showing", block.Visibility);
                Assert.AreEqual(true, block.IsMatched);
            }

            matchGame.UserSelectsNewGamePrompt();
            // Blocks list is reset, all blocks have been made invisible & are not matched
            foreach (Block block in matchGame.Blocks)
            {
                Assert.AreEqual("", block.Visibility);
                Assert.AreEqual(false, block.IsMatched);
            }
            Assert.AreEqual(MatchGameStatus.Ready, matchGame.GameStatus);
        }
        // end "UserSelectsNewGamePrompt()" test methods
    }

    public class MockRandom : Random
    {
        public int ValueToReturn { get; set; } = 0;
        public override int Next() => ValueToReturn;
        public override int Next(int maxValue) => ValueToReturn;
        public override int Next(int minValue, int maxValue) => ValueToReturn;
    }

    /*
    public class PrivateObject
    {
        private readonly object o;

        public PrivateObject(object o)
        {
            this.o = o;
        }

        public object Invoke(string methodName, params object[] args)
        {
            var methodInfo = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new Exception($"Method'{methodName}' not found is class '{o.GetType()}'");
            }
            return methodInfo.Invoke(o, args);
        }
    }*/
}