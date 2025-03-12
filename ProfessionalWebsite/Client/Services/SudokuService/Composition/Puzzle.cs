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
        List<BlockColumn> blockClumns)
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
    }

    public Cell[,] Matrix { get; private set; }
    public List<Cell> Cells { get; private set; }
    public List<Row> Rows { get; private set; }
    public List<Column> Columns { get; private set; }
    public List<Block> Blocks { get; private set; }
    public List<BlockRow> BlockRows { get; private set; }
    public List<BlockColumn> BlockClumns { get; private set; }
    //public TxnLedger Ledger { get; }
    public ISolver Solver { get; private set; }
    public int[,] StartingIntMatrix { get; }
    public int[,] StartingIntMatrixToSuperimpose { get; }
    public bool NoExpectedCellValuesInCells => GetNoExpectedCellValuesInCells();
    public Cell CurrentCell { get; private set; }
    public Cell Cell(int id) => Cells[id - 1];
    /*
    public static Puzzle CreateWithBruteForceSolver()
    {
        return Create(BruteForceSolver.Create(), TxnLedger.Create());
    }
    */
    public static Puzzle Create()
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
        Puzzle puzzle = new(cellMatrix, cellList, rows, columnList, blockList, blockRowList, blockColumnList);
        puzzle.AssignPuzzleReferenceToCells();
        //puzzle.AssignPuzzleReferenceToLedger();

        return puzzle;
    }
    public bool Solve()
    {
        ISolver solver = BruteForceSolver.Create(this);
        bool isSolvable = solver.Solve();
        return isSolvable;
    }
    /*
    private void AssignPuzzleReferenceToLedger()
    {
        Ledger.AssignPuzzleReference(this);
    }
    */
    public void RemoveCandidates()
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
        RemoveCandidates();
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
    public string GetValuesOnlyFormattedString()
    {
        string puzzleAsStr = string.Empty;
        string row;

        for (int i = 0; i < 9; i++)
        {
            row = $"[ {Matrix[i, 0].Values[0]} {Matrix[i, 1].Values[0]} {Matrix[i, 2].Values[0]} ]  " +
                  $"[ {Matrix[i, 3].Values[0]} {Matrix[i, 4].Values[0]} {Matrix[i, 5].Values[0]} ]  " +
                  $"[ {Matrix[i, 6].Values[0]} {Matrix[i, 7].Values[0]} {Matrix[i, 8].Values[0]} ]\n";
            puzzleAsStr += row;

            if (i % 3 == 2)
                puzzleAsStr += "\n";
        }

        return puzzleAsStr;
    }
    public string GetValuesAndMetaDataFormattedString()
    {
        string returnStr = string.Empty;

        string row = $"            " +
            $"  {Columns[0].GetCandidatesAsFormattedString()}  {Columns[1].GetCandidatesAsFormattedString()}  {Columns[2].GetCandidatesAsFormattedString()}    " +
            $"    {Columns[3].GetCandidatesAsFormattedString()}  {Columns[4].GetCandidatesAsFormattedString()}  {Columns[5].GetCandidatesAsFormattedString()}    " +
            $"    {Columns[6].GetCandidatesAsFormattedString()}  {Columns[7].GetCandidatesAsFormattedString()}  {Rows[8].GetCandidatesAsFormattedString()}  \n";

        returnStr += row;

        for (int i = 0; i < 9; i++)
        {
            row = $"{Rows[i].GetCandidatesAsFormattedString()}  " +
                         $"[   {Matrix[i, 0].GetAllValues()} {Matrix[i, 1].GetAllValues()} {Matrix[i, 2].GetAllValues()} ] [ " +
                         $"  {Matrix[i, 3].GetAllValues()} {Matrix[i, 4].GetAllValues()} {Matrix[i, 5].GetAllValues()} ] [ " +
                         $"  {Matrix[i, 6].GetAllValues()} {Matrix[i, 7].GetAllValues()} {Matrix[i, 8].GetAllValues()} ]\n";
            returnStr += row;

            if (i % 3 == 2)
            {
                string tenSpaces = "          ";
                row = $"{tenSpaces}  " +
                    $"    {tenSpaces}{{{Blocks[i - 2].GetCandidatesAsFormattedString()}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{Blocks[i - 1].GetCandidatesAsFormattedString()}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{Blocks[i].GetCandidatesAsFormattedString()}}}{tenSpaces}\n\n";
                returnStr += row;
            }
        }

        return returnStr;
    }
    public string GetCellPreviewAsFormattedString()
    {
        string cellPreview = string.Empty;

        foreach (var cell in Cells)
        {
            string row = $"Cell: {{ Id:{cell.Id}, Row:{cell.Row}, Column:{cell.Column}, Block:{cell.Block}, BlockRow:{cell.BlockRow}, BlockColumn:{cell.BlockColumn}\n";
            cellPreview += row;
            Console.Write(row);
        }

        return cellPreview;
    }
    /*
    public static string RenderStandardTxnInfo(Puzzle puzzle)
    {
        string returnStr;
        // Print the transactions
        returnStr = $"Number of Txns in Ledger: {puzzle.Ledger.Txns.Count:N0}\n";
        returnStr += $"Number of ValueTxns: {puzzle.Ledger.ValueTxns.Count:N0}\n";
        returnStr += $"Number of CandidateTxns: {puzzle.Ledger.CandidateTxns.Count:N0}\n\n";

        // Print proportions
        decimal valueTxnsCount = puzzle.Ledger.ValueTxns.Count;
        decimal txnsCount = puzzle.Ledger.Txns.Count;
        returnStr += $"ValueTxns % of Txns: {valueTxnsCount / txnsCount:P}\n\n";

        // Print rates
        returnStr += $"Txns/second: {puzzle.Ledger.Txns.Count / puzzle.StopwatchTime:N1}\n";
        returnStr += $"ValueTxns/second: {puzzle.Ledger.ValueTxns.Count / puzzle.StopwatchTime:N1}\n";
        returnStr += $"CandidateTxns/second: {puzzle.Ledger.CandidateTxns.Count / puzzle.StopwatchTime:N1}\n\n";

        return returnStr;
    }
    */
}