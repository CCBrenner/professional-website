using ProfessionalWebsite.Client.Services.SudokuService;

namespace ProfessionalWebsite.Tests.SudokuServiceTests;

[TestClass]
public class GenericTests
{
    [TestMethod]
    public void TestSettingValueOfCellRequiresValueIsAPossibilityOfTheCell()
    {
        // Arrange
        int[,] givenMatrix =
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 2, 0,   0, 0, 0 },

            { 0, 0, 0,   5, 0, 0,   0, 0, 0 },
            { 0, 0, 7,   0, 0, 0,   0, 3, 0 },
            { 0, 0, 0,   0, 0, 4,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 1, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
        int[,] superImposeMatrix =
        {
            { 1, 2, 3,   4, 5, 6,   7, 8, 9 },
            { 4, 5, 6,   7, 8, 9,   1, 2, 3 },
            { 7, 8, 9,   1, 2, 3,   4, 5, 6 },

            { 2, 3, 4,   5, 6, 7,   8, 9, 1 },
            { 5, 6, 7,   8, 9, 1,   2, 3, 4 },
            { 8, 9, 1,   2, 3, 4,   5, 6, 7 },

            { 3, 4, 5,   6, 7, 8,   9, 1, 2 },
            { 6, 7, 8,   9, 1, 2,   3, 4, 5 },
            { 9, 1, 2,   3, 4, 5,   6, 7, 8 },
        };
        int[,] seedMatrix = MatrixFactory.CreateMatrixBySuperimposition(givenMatrix, superImposeMatrix);
        Puzzle puzzle = Puzzle.Create();
        puzzle.LoadMatrixAsCellValues(seedMatrix);

        puzzle.RemoveCandidates();
        Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[7]);  // 7 is value to test w/these coordinates

        // Act
        int returnValue = puzzle.Matrix[4, 8].AssignConfirmedValue(7);

        // Assert
        Assert.AreEqual(0, returnValue);
        Assert.AreEqual(4, puzzle.Matrix[4, 8].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[4, 8].Values[0]);
    }
    [TestMethod]
    public void TestIfOnePossibilityLeftThenAssignPossibilityToValue()
    {
        // Arrange
        int[,] startingMatrix =  // not from Puzzle Book
        {
            { 1, 0, 3,   4, 0, 0,   0, 2, 0 },
            { 0, 0, 0,   0, 0, 8,   0, 0, 0 },
            { 0, 0, 0,   0, 5, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 6, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 7, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
        Puzzle puzzle = Puzzle.Create();
        puzzle.LoadMatrixAsCellValues(startingMatrix);
        puzzle.RemoveCandidates();

        // Act
        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 4].Candidates.Count == 1);
        Assert.AreEqual(9, puzzle.Matrix[0, 4].Values[0]);
    }

    [TestMethod]
    public void TestCellCandidatesReturnsListOfNonZeroCandidates()
    {
        // Arrange
        int[,] startingMatrix =  // "SS, V20, P33" puzzle (1:40 to solve)
        {
            { 0, 0, 5, 0, 3, 0, 0, 8, 0 },
            { 0, 0, 3, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 9, 1 },

            { 8, 0, 0, 7, 0, 0, 0, 1, 0 },
            { 2, 0, 0, 8, 0, 3, 0, 0, 7 },
            { 0, 6, 0, 0, 0, 4, 0, 0, 9 },

            { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 0, 1, 0, 0 },
            { 0, 8, 0, 0, 5, 0, 6, 0, 0 },
        };
        Puzzle puzzle = Puzzle.Create();
        puzzle.LoadMatrixAsCellValues(startingMatrix);

        // Act
        puzzle.RemoveCandidates();

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(1));
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(2));
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(7));

        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(3));
        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(5));
        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(8));
    }
    [TestMethod]
    public void TestBruteForceSudokuSolverSolvesPuzzles()
    {
        // Arrange
        int[,] startingMatrix = new int[9, 9]  // "KSP, V338, P1"
        {
            { 5, 6, 8,   0, 9, 0,   2, 0, 0 },
            { 0, 9, 0,   0, 3, 0,   6, 0, 0 },
            { 0, 4, 3,   0, 0, 2,   5, 8, 0 },

            { 2, 0, 6,   9, 0, 7,   1, 3, 0 },
            { 0, 0, 0,   0, 2, 0,   0, 0, 0 },
            { 0, 3, 9,   1, 0, 6,   8, 0, 7 },

            { 0, 2, 4,   5, 0, 0,   3, 1, 0 },
            { 0, 0, 5,   0, 4, 0,   0, 9, 0 },
            { 0, 0, 7,   0, 1, 0,   4, 6, 5 },
        };

        Puzzle puzzle = Puzzle.Create();
        puzzle.LoadMatrixAsCellValues(startingMatrix);
        //puzzle.AssignSelfToSolver();  // Compensates for 

        // Act
        bool actualPuzzleWasSolved = puzzle.Solve();

        // Assert
        Console.Write(puzzle.GetValuesOnlyFormattedString());
        Assert.IsTrue(actualPuzzleWasSolved);
        Assert.AreEqual(2, puzzle.Matrix[8, 3].Values[0]);
        Assert.AreEqual(9, puzzle.Matrix[8, 0].Values[0]);
        Assert.AreEqual(5, puzzle.Matrix[5, 4].Values[0]);
        Assert.AreEqual(1, puzzle.Matrix[1, 8].Values[0]);
        Assert.AreEqual(6, puzzle.Matrix[7, 3].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[1, 7].Values[0]);

        /*
        Solution ("KSP, V338, P1"):
        [ 5 6 8 ]  [ 4 9 1 ]  [ 2 7 3 ]
        [ 7 9 2 ]  [ 8 3 5 ]  [ 6 4 1 ]
        [ 1 4 3 ]  [ 7 6 2 ]  [ 5 8 9 ]

        [ 2 5 6 ]  [ 9 8 7 ]  [ 1 3 4 ]
        [ 8 7 1 ]  [ 3 2 4 ]  [ 9 5 6 ]
        [ 4 3 9 ]  [ 1 5 6 ]  [ 8 2 7 ]

        [ 6 2 4 ]  [ 5 7 9 ]  [ 3 1 8 ]
        [ 3 1 5 ]  [ 6 4 8 ]  [ 7 9 2 ]
        [ 9 8 7 ]  [ 2 1 3 ]  [ 4 6 5 ]
        */
    }

    [TestMethod]
    public void TestBruteForceSudokuSolverSolvesPuzzles_R1()
    {
        // Arrange
        int[,] startingMatrix = new int[9, 9]  // "R1"
        {
            { 0, 0, 7,   4, 6, 0,   2, 0, 0 },
            { 0, 3, 0,   0, 0, 0,   4, 0, 0 },
            { 0, 9, 0,   5, 0, 0,   6, 0, 0 },

            { 2, 0, 0,   1, 0, 0,   5, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 7, 0,   6, 0, 0,   0, 9, 0 },

            { 0, 0, 3,   0, 0, 1,   0, 5, 0 },
            { 0, 1, 0,   7, 0, 0,   0, 8, 0 },
            { 0, 0, 0,   0, 3, 4,   0, 0, 0 },
        };

        Puzzle puzzle = Puzzle.Create();
        puzzle.LoadMatrixAsCellValues(startingMatrix);
        //puzzle.AssignSelfToSolver();  // Compensates for 

        // Act
        bool actualPuzzleWasSolved = puzzle.Solve();

        // Assert
        Console.Write(puzzle.GetValuesOnlyFormattedString());
        Assert.IsTrue(actualPuzzleWasSolved);
        Assert.AreEqual(9, puzzle.Matrix[8, 3].Values[0]);
        Assert.AreEqual(5, puzzle.Matrix[8, 0].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[5, 4].Values[0]);
        Assert.AreEqual(5, puzzle.Matrix[1, 8].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[7, 2].Values[0]);
        Assert.AreEqual(7, puzzle.Matrix[1, 7].Values[0]);

        Console.Write(puzzle.GetValuesOnlyFormattedString());

        /*
            Solution:
            [ 8 5 7 ]  [ 4 6 3 ]  [ 2 1 9 ]
            [ 6 3 1 ]  [ 2 9 8 ]  [ 4 7 5 ]
            [ 4 9 2 ]  [ 5 1 7 ]  [ 6 3 8 ]

            [ 2 8 6 ]  [ 1 7 9 ]  [ 5 4 3 ]
            [ 1 4 9 ]  [ 3 8 5 ]  [ 7 2 6 ]
            [ 3 7 5 ]  [ 6 4 2 ]  [ 8 9 1 ]

            [ 7 6 3 ]  [ 8 2 1 ]  [ 9 5 4 ]
            [ 9 1 4 ]  [ 7 5 6 ]  [ 3 8 2 ]
            [ 5 2 8 ]  [ 9 3 4 ]  [ 1 6 7 ]
            */
    }


    /*
    [TestMethod]
    public void TestUpdateCandidatesHydratesAndRemovesCandidatesToLeavePossibleCandidatesOnly()
    {
        // Arrange
        int[,] startingMatrix =
        {
            { 0, 0, 5, 0, 3, 0, 0, 8, 0 },
            { 0, 0, 3, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 9, 1 },

            { 8, 0, 0, 7, 0, 0, 0, 1, 0 },
            { 2, 0, 0, 8, 0, 3, 0, 0, 7 },
            { 0, 6, 0, 0, 0, 4, 0, 0, 9 },

            { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 0, 1, 0, 0 },
            { 0, 8, 0, 0, 5, 0, 6, 0, 0 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateCellMatrix(startingMatrix);
        Puzzle puzzle = Puzzle.Create(createdCellMatrix);
        puzzle.RemoveCandidates();
        puzzle.Matrix[0, 0].SetExpectedValue(1);
        puzzle.Matrix[0, 1].SetExpectedValue(2);
        puzzle.Matrix[0, 4].SetExpectedValue(4);
        puzzle.Matrix[0, 4].ResetValue();

    }/*
    [TestMethod]
    public void TestGetNextCandidateGrabsTheNextUntriedCandidateAvailableForTheCell()
    {

    }*/
}