namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Puzzle : IPuzzle
{
    private Puzzle(
        Cell[,] puzzleMatrix,
        List<Cell> cells,
        List<Row> rows,
        List<Column> columns,
        List<Block> blocks,
        List<BlockRow> blockRows,
        List<BlockColumn> blockClumns,
        TxnLedger ledger,
        ISolver solver)
    {
        Matrix = puzzleMatrix;
        StartingIntMatrix = MatrixFactory.GetBlankMatrix();
        StartingIntMatrixToSuperimpose = MatrixFactory.GetBlankMatrix();
        Cells = cells;
        Rows = rows;
        Columns = columns;
        Blocks = blocks;
        BlockRows = blockRows;
        BlockClumns = blockClumns;
        Ledger = ledger;
        _solver = solver;
    }

    public Cell[,] Matrix { get; private set; }
    public List<Cell> Cells { get; private set; }
    public List<Row> Rows { get; private set; }
    public List<Column> Columns { get; private set; }
    public List<Block> Blocks { get; private set; }
    public List<BlockRow> BlockRows { get; private set; }
    public List<BlockColumn> BlockClumns { get; private set; }
    public TxnLedger Ledger { get; }
    private ISolver _solver;
    public decimal StopwatchTime => _solver.StopwatchTime;
    public bool SolveHasStarted => _solver.SolveHasStarted;
    public int ProgressPercentage => _solver.ProgressPercentage;
    public int[,] StartingIntMatrix { get; }
    public int[,] StartingIntMatrixToSuperimpose { get; }
    public bool NoExpectedCellValuesInCells => GetNoExpectedCellValuesInCells();
    public Cell CurrentCell { get; private set; }
    public Cell Cell(int id) => Cells[id - 1];
    public static Puzzle CreateWithBruteForceSolver()
    {
        return Create(BruteForceSolver.Create(), TxnLedger.Create());
    }
    public static Puzzle Create(ISolver solver, TxnLedger ledger)
    {
        // CreateWithBruteForceSolver cellMatrix of cellList instead of ints from starting point
        Cell[,] cellMatrix = Services.SudokuService.Cell.CreateCellMatrix();

        // CreateWithBruteForceSolver list of references to cellList of previously created cellMatrix
        List<Cell> cellList = cellMatrix.CreateCellList();

        // CreateWithBruteForceSolver rows array of cell references from cellList list (one-to-many relationships)
        List<Row> rows = cellList.CreateRowList();
        rows.AssignRowsToCells();

        // CreateWithBruteForceSolver column array of cell references from cellList list (one-to-many relationships)
        List<Column> columnList = cellList.CreateColumnList();
        columnList.AssignColumnsToCells();

        // CreateWithBruteForceSolver block array of cell references from cellList list (one-to-many relationships)
        List<Block> blockList = cellList.CreateBlockList();
        blockList.AssignBlocksToCells();

        // CreateWithBruteForceSolver blockrows array of block references from blockList array (one-to-many relationships)
        List<BlockRow> blockRowList = blockList.CreateBlockRowList();
        blockRowList.AssignBlockRowsToBlocks();

        // CreateWithBruteForceSolver blockcolumns array of block references from blockList array (one-to-many relationships)
        List<BlockColumn> blockColumnList = blockList.CreateBlockColumnList();
        blockColumnList.AssignBlockColumnstoBlocks();

        // Inject and create new Puzzle
        Puzzle puzzle = new(cellMatrix, cellList, rows, columnList, blockList, blockRowList, blockColumnList, ledger, solver);
        puzzle.AssignPuzzleReferenceToCells();
        puzzle.AssignPuzzleReferenceToLedger();

        return puzzle;
    }
    public bool Solve()
    {
        bool isSolvable = _solver.Solve(this);
        return isSolvable;
    }
    private void AssignPuzzleReferenceToLedger()
    {
        Ledger.AssignPuzzleReference(this);
    }
    public void PerformCandidateElimination()
    {
        // Eliminate candidates for Given and Confirmed cells
        EliminateCandidatesForGivenAndConfirmedCells();

        // Eliminate by distinct in neighborhood
        Row.EliminateCandidatesByDistinctInNeighborhood(Rows);
        Column.EliminateCandidatesByDistinctInNeighborhood(Columns);
        Block.EliminateCandidatesByDistinctInNeighborhood(Blocks);

        // Eliminate by candidate lines
        Block.EliminateCandidatesByCandidateLines(Blocks);

        // Eliminate by double pairs

        // Eliminate by multiple lines

        // Eliminate by naked pairs, triples, quadruples

        // Eliminate by hidden pairs, triples, quadruples

        // Eliminate by X-wings

        // Eliminate by Swordfish
    }
    private void EliminateCandidatesForGivenAndConfirmedCells()
    {
        foreach (Cell cell in Cells)
        {
            if (cell.ValueStatus == ValueStatus.Given || cell.ValueStatus == ValueStatus.Confirmed)
            {
                cell.EliminateAllNonValueCandidates();
            }
        }
    }
    public void UpdateCellValuesBasedOnSingleCandidate()
    {
        foreach (var cell in Matrix)
        {
            cell.UpdateValueBasedOnSingleCandidate();
        }
    }
    private void AssignPuzzleReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignPuzzleReference(this);
            }
        }
    }
    private bool GetNoExpectedCellValuesInCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                if (cell.ValueStatus == ValueStatus.Expected)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void UpdateCandidates()
    {
        RehydrateCandidatesOfCells();
        PerformCandidateElimination();
    }
    private void RehydrateCandidatesOfCells()
    {
        foreach (var cell in Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                cell.RehydrateCandidates();
            }
        }
    }
    public void LoadMatrixAsCellValues(int[,] matrixToLoad)
    {
        foreach (var cell in Cells)
        {
            cell.ResetValue();
            cell.ResetValueStatus();
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Cell((i * 9) + j + 1).Value = matrixToLoad[i, j];
            }
        }
        foreach (var cell in Cells)
        {
            if (cell.Value != 0)
            {
                cell.SetValueStatusToGiven();
                cell.EliminateAllNonValueCandidates();  // <= stopped here, about to explore this method
            }
        }
    }
}