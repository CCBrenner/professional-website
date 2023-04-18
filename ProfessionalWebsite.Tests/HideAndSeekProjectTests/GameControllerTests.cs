namespace ProfessionalWebsite.Client.Classes.HideAndSeekProject
{
    [TestClass]
    public class GameControllerTests
    {
        GameController gameController;

        [TestInitialize]
        public void Initialize() => gameController = new GameController();
        [TestMethod]
        public void TestMovement()
        {
            Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
            Assert.IsFalse(gameController.Move(Direction.Up));

            Assert.IsTrue(gameController.Move(Direction.East));
            Assert.AreEqual("Hallway", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Up));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Northwest));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Southeast));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);
        }
        [TestMethod]
        public void TestParseInput()
        {
            string initialStatus = gameController.Status;

            Assert.AreEqual("That's not a valid direction.<br>", gameController.ParseInput("x"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("There's no exit in that direction.<br>", gameController.ParseInput("Up"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("Moving East<br>", gameController.ParseInput("East"));
            Assert.AreEqual($"You are in the Hallway. You see the following exits:" +
                $"<br><br> - the Downstairs Bathroom is to the North" +
                $"<br> - the Living Room is to the South" +
                $"<br> - the Entry is to the West" +
                $"<br> - the Kitchen is to the Northwest" +
                $"<br> - the Landing is Up<br>", gameController.Status);
            Assert.AreEqual($"Opponents Found: " +
                $"<br>Hidden Opponents Remaining: 5", gameController.GameProgress);

            Assert.AreEqual("Moving South<br>", gameController.ParseInput("south"));
            Assert.AreEqual($"You are in the Living Room. You see the following exits:" +
                $"<br><br> - the Hallway is to the North<br>", gameController.Status);
            Assert.AreEqual($"Opponents Found: " +
                $"<br>Hidden Opponents Remaining: 5", gameController.GameProgress);

            string landingStatus = $"You are on the Landing. You see the following exits:" +
                $"<br><br> - the Pantry is to the South" +
                $"<br> - the Upstairs Bathroom is to the West" +
                $"<br> - the Nursery is to the Southwest" +
                $"<br> - the Master Bedroom is to the Northwest" +
                $"<br> - the Kids Room is to the Southeast" +
                $"<br> - the Attic is Up" +
                $"<br> - the Hallway is Down<br>";
            Assert.AreEqual($"Opponents Found: " +
                $"<br>Hidden Opponents Remaining: 5", gameController.GameProgress);

            Assert.AreEqual("Moving North<br>", gameController.ParseInput("north"));
            Assert.AreEqual("Moving Up<br>", gameController.ParseInput("UP"));
            Assert.AreEqual(landingStatus, gameController.Status);

            Assert.AreEqual("That's not a valid direction.<br>", gameController.ParseInput("Master dRoom"));
            Assert.AreEqual("There's no exit in that direction.<br>", gameController.ParseInput("NOrth"));
            Assert.AreEqual(landingStatus, gameController.Status);

            Assert.AreEqual("Moving Northwest<br>", gameController.ParseInput("Northwest"));
            Assert.AreEqual($"You are in the Master Bedroom. You see the following exits:" +
                $"<br><br> - the Master Bathroom is to the East" +
                $"<br> - the Landing is to the Southeast<br>", gameController.Status);
            Assert.AreEqual($"Opponents Found: " +
                $"<br>Hidden Opponents Remaining: 5", gameController.GameProgress);
        }
        [TestMethod]
        public void TestParseCheck()
        {
            Assert.IsFalse(gameController.GameOver);

            // Clear the hiding places and hide the opponents in different rooms
            House.ClearHidingPlaces();
            var joe = gameController.Opponents.ToList()[0];
            (House.GetLocationByName("Garage") as LocationWithHidingPlace).Hide(joe);
            var bob = gameController.Opponents.ToList()[1];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(bob);
            var ana = gameController.Opponents.ToList()[2];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(ana);
            var owen = gameController.Opponents.ToList()[3];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(owen);
            var jimmy = gameController.Opponents.ToList()[4];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(jimmy);

            // Check the Entry -- there are no hiding players there
            Assert.AreEqual(1, gameController.MoveNumber);
            Assert.AreEqual("There is no hiding place in the Entry<br>", gameController.ParseInput("Check"));
            Assert.AreEqual(2, gameController.MoveNumber);

            // Move to the Garage
            gameController.ParseInput("Out");
            Assert.AreEqual(3, gameController.MoveNumber);

            // We hid Joe in the Garage, so validate ParseInput's return value and the properties
            Assert.AreEqual("You found 1 opponent hiding behind the car.<br>", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Garage. You see the following exits:" +
                $"<br><br> - the Entry is In" +
                $"<br><br>Someone could hide behind the car<br>", gameController.Status);
            Assert.AreEqual($"Opponents Found: Joe" +
                $"<br>Hidden Opponents Remaining: 4", gameController.GameProgress);
            Assert.AreEqual("4: Which direction do you want to go? (or click 'Check')", gameController.Prompt);
            Assert.AreEqual(4, gameController.MoveNumber);

            // Move to the bathroom, where no one is hiding
            gameController.ParseInput("In");
            gameController.ParseInput("East");
            gameController.ParseInput("North");

            // Check the Bathroom to make sure no one is hiding there
            Assert.AreEqual("Nobody was hiding behind the door<br>", gameController.ParseInput("check"));
            Assert.AreEqual(8, gameController.MoveNumber);

            // Check the Donwstairs Bathroom to make sure no one is hiding there
            gameController.ParseInput("South");
            gameController.ParseInput("Northwest");
            Assert.AreEqual("You found 2 opponents hiding next to the stove.<br>", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Kitchen. You see the following exits:" +
                $"<br><br> - the Hallway is to the Southeast" +
                $"<br><br>Someone could hide next to the stove<br>", gameController.Status);
            Assert.AreEqual($"Opponents Found: Joe, Bob, Jimmy" +
                $"<br>Hidden Opponents Remaining: 2", gameController.GameProgress);
            Assert.AreEqual("11: Which direction do you want to go? (or click 'Check')", gameController.Prompt);
            Assert.AreEqual(11, gameController.MoveNumber);

            Assert.IsFalse(gameController.GameOver);

            // Head up to the Landing, then check the Pantry (no one is hiding there)
            gameController.ParseInput("Southeast");
            gameController.ParseInput("Up");
            Assert.AreEqual(13, gameController.MoveNumber);

            gameController.ParseInput("South");
            Assert.AreEqual("Nobody was hiding inside a cabinet<br>", gameController.ParseInput("check"));
            Assert.AreEqual(15, gameController.MoveNumber);

            // Check the Attic to find the last two opponents
            gameController.ParseInput("North");
            gameController.ParseInput("Up");
            Assert.AreEqual(17, gameController.MoveNumber);

            Assert.AreEqual("You found 2 opponents hiding in a trunk.<br>", gameController.ParseInput("check"));
            Assert.AreEqual($"You won the game in 18 moves!" +
                $"<br>Press \"Restart\" to restart to restart the game or press \"Quit\" to quit.", gameController.Status);
            Assert.AreEqual("18: Which direction do you want to go? (or click 'Check')", gameController.Prompt);
            Assert.AreEqual(18, gameController.MoveNumber);

            Assert.IsTrue(gameController.GameOver);
        }
    }
}
