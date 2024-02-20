namespace ProfessionalWebsite.Client.Classes.SudokuSolverProject;

public static class MatrixFactory
{
    public static int[,] GetSuperImposeMatrix()
    {
        // for used as default with the Puzzle.CreateWithBruteForceSolver() factory method; DI is the goal
        return new int[,]
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
    }
    public static int[,] GetBlankMatrix()
    {
        return new int[,]
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
    }
    public static int[,] CreateMatrixBySuperimposition(int[,] originalMatrix, int[,] superimposeMatrix)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (originalMatrix[i,j] == 0)
                {
                    originalMatrix[i,j] = superimposeMatrix[i,j];
                }
            }

        }
        return originalMatrix;
    }
}

